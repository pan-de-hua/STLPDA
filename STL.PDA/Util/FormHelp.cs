using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Drawing;
using System.Runtime.InteropServices;

namespace STL.PDA.Util
{
    public class FormHelp
    {
        /// <summary>
        /// 窗体自动缩放
        /// </summary>
        /// <param name="frm"></param>
        public static void FormAutoSize(Form frm)
        {
            FormHelp asc = new FormHelp();

            frm.Load += new EventHandler((o, e) =>
            {
                asc.controllInitializeSize(frm);
                frm.Resize += new EventHandler((c, v) => { asc.controlAutoSize(frm); });
                if (System.Environment.OSVersion.Platform == PlatformID.WinCE)
                {
                    frm.WindowState = FormWindowState.Maximized;
                }
            });

        }


        //(1).声明结构,只记录窗体和其控件的初始位置和大小。
        public struct controlRect
        {
            public int Left;
            public int Top;
            public int Width;
            public int Height;
        }
        //(2).声明 1个对象
        //注意这里不能使用控件列表记录 List nCtrl;，因为控件的关联性，记录的始终是当前的大小。
        //      public List oldCtrl= new List();//这里将西文的大于小于号都过滤掉了，只能改为中文的，使用中要改回西文
        public List<controlRect> oldCtrl = new List<controlRect>();
        int ctrlNo = 0;//1;
        //(3). 创建两个函数
        //(3.1)记录窗体和其控件的初始位置和大小,
        public void controllInitializeSize(Control mForm)
        {
            controlRect cR;
            cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
            oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
            AddControl(mForm);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            //this.WindowState = (System.Windows.Forms.FormWindowState)(2);//记录完控件的初始位置和大小后，再最大化
            //0 - Normalize , 1 - Minimize,2- Maximize
        }
        private void AddControl(Control ctl)
        {
            foreach (Control c in ctl.Controls)
            {  //**放在这里，是先记录控件的子控件，后记录控件本身
                //if (c.Controls.Count > 0)
                //    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                controlRect objCtrl;
                objCtrl.Left = c.Left; objCtrl.Top = c.Top; objCtrl.Width = c.Width; objCtrl.Height = c.Height;
                oldCtrl.Add(objCtrl);
                //**放在这里，是先记录控件本身，后记录控件的子控件
                if (c.Controls.Count > 0)
                    AddControl(c);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }
        //(3.2)控件自适应大小,
        public void controlAutoSize(Control mForm)
        {
            if (ctrlNo == 0)
            { //*如果在窗体的Form1_Load中，记录控件原始的大小和位置，正常没有问题，但要加入皮肤就会出现问题，因为有些控件如dataGridView的的子控件还没有完成，个数少
                //*要在窗体的Form1_SizeChanged中，第一次改变大小时，记录控件原始的大小和位置,这里所有控件的子控件都已经形成
                controlRect cR;
                //  cR.Left = mForm.Left; cR.Top = mForm.Top; cR.Width = mForm.Width; cR.Height = mForm.Height;
                cR.Left = 0; cR.Top = 0; cR.Width = mForm.Width; cR.Height = mForm.Height;

                oldCtrl.Add(cR);//第一个为"窗体本身",只加入一次即可
                AddControl(mForm);//窗体内其余控件可能嵌套其它控件(比如panel),故单独抽出以便递归调用
            }
            float wScale = (float)mForm.Width / (float)oldCtrl[0].Width;//新旧窗体之间的比例，与最早的旧窗体
            float hScale = (float)mForm.Height / (float)oldCtrl[0].Height;//.Height;
            ctrlNo = 1;//进入=1，第0个为窗体本身,窗体内的控件,从序号1开始
            AutoScaleControl(mForm, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
        }
        private void AutoScaleControl(Control ctl, float wScale, float hScale)
        {
            int ctrLeft0, ctrTop0, ctrWidth0, ctrHeight0;
            //int ctrlNo = 1;//第1个是窗体自身的 Left,Top,Width,Height，所以窗体控件从ctrlNo=1开始
            foreach (Control c in ctl.Controls)
            { //**放在这里，是先缩放控件的子控件，后缩放控件本身
                //if (c.Controls.Count > 0)
                //   AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
                ctrLeft0 = oldCtrl[ctrlNo].Left;
                ctrTop0 = oldCtrl[ctrlNo].Top;
                ctrWidth0 = oldCtrl[ctrlNo].Width;
                ctrHeight0 = oldCtrl[ctrlNo].Height;
                //c.Left = (int)((ctrLeft0 - wLeft0) * wScale) + wLeft1;//新旧控件之间的线性比例
                //c.Top = (int)((ctrTop0 - wTop0) * h) + wTop1;
                c.Left = (int)((ctrLeft0) * wScale);//新旧控件之间的线性比例。控件位置只相对于窗体，所以不能加 + wLeft1
                c.Top = (int)((ctrTop0) * hScale);//
                c.Width = (int)(ctrWidth0 * wScale);//只与最初的大小相关，所以不能与现在的宽度相乘 (int)(c.Width * w);
                c.Height = (int)(ctrHeight0 * hScale);//
                ctrlNo++;//累加序号
                //**放在这里，是先缩放控件本身，后缩放控件的子控件
                if (c.Controls.Count > 0)
                    AutoScaleControl(c, wScale, hScale);//窗体内其余控件还可能嵌套控件(比如panel),要单独抽出,因为要递归调用
            }
        }



        /// <summary>
        /// DataGrid自动匹配宽度
        /// </summary>
        /// <param name="dataGrid"></param>
        /// <param name="dt"></param>
        public static void AutoSizeGrid(DataGrid dataGrid, DataTable dt)
        {

            dataGrid.DataSource = dt;
            DataGridTableStyle myts = new DataGridTableStyle();

            myts.MappingName = dt.TableName;

            dataGrid.TableStyles.Clear();

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                DataColumn col = dt.Columns[i];

                int maxStrLenght = GetStrWidth(col.ColumnName, dataGrid);
                foreach (DataRow row in dt.Rows)
                {
                    int newStrLenght = GetStrWidth(row[i].ToString(), dataGrid);
                    if (newStrLenght > maxStrLenght)
                    {
                        maxStrLenght = newStrLenght;
                    }
                }

                myts.GridColumnStyles.Add(new DataGridTextBoxColumn() { Width = maxStrLenght + 6, MappingName = col.ColumnName, HeaderText = col.ColumnName });
            }

            dataGrid.TableStyles.Add(myts);
            dataGrid.DataSource = dt;
        }

        public static int GetStrWidth(string str, DataGrid con)
        {
            using (Graphics graphics = con.CreateGraphics())
            {
                SizeF sizeF = graphics.MeasureString(str, con.Font);
                return (int)sizeF.Width;
            }
        }


        /// <summary>
        /// 变更提示，并播放提示音
        /// </summary>
        /// <param name="lab">提示的Lab</param>
        /// <param name="message">提示的文字</param>
        /// <param name="sounds">提示音集合</param>
        public static void SetTsLab(Control lab, string message, List<string> sounds, Color color)
        {
            lab.Text = "提示: " + message;
            lab.ForeColor = color;
            Airplay(sounds);
        }

        /// <summary>
        /// 传入语音名称的集合,依次播放所有语音
        /// </summary>
        /// <param name="yuYin">语音名称的集合</param>
        public static void Airplay(List<string> yuYin)
        {
            foreach (var item in yuYin)
            {
                System.Media.SoundPlayer sndPlayer;
                string sound = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().ManifestModule.FullyQualifiedName) + "\\sound\\" + item + ".wav";
                sndPlayer = new System.Media.SoundPlayer(sound);
                sndPlayer.PlaySync();
            }
        }



        [DllImport("coredll", EntryPoint = "SipShowIM")]
        private static extern bool SipShowIM(IntPtr SIP_STATUS);
        private static readonly IntPtr SIPF_OFF = (IntPtr)0x0;
        private static readonly IntPtr SIPF_ON = (IntPtr)0x1;
        ///
        /// 显示/隐藏软键盘
        ///
        /// 是否显示
        ///
        public static bool SipShowIM(bool isShow)
        {
            return SipShowIM(isShow ? SIPF_ON : SIPF_OFF);
        }
    }

}
