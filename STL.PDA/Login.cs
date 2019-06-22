using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Reflection;
using System.Net;
using System.Security.Cryptography;
namespace STL.PDA
{
    public partial class Login : Form
    {
        protected NetworkCredential nc = new NetworkCredential(WebServiceInstants.GetURL(ServiceType.userName).ToString(), WebServiceInstants.GetURL(ServiceType.password).ToString());
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUserID.Text = WebServiceInstants.GetURL(ServiceType.remUser);
            if (bool.TrueString.Equals(WebServiceInstants.GetURL(ServiceType.RememberMe)))
            {
                chkRememberMe.Checked = true;
                txtPassword.Text = WebServiceInstants.GetURL(ServiceType.remPassword);
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void SaveToConfig()
        {
            try
            {
                string ApplicationPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(ApplicationPath + "\\config.xml");
                xmlDoc.SelectSingleNode("/config/remUser").InnerText = txtUserID.Text.Trim();
                xmlDoc.SelectSingleNode("/config/RememberMe").InnerText = chkRememberMe.Checked.ToString();
                if (chkRememberMe.Checked)
                {
                    xmlDoc.SelectSingleNode("/config/remPassword").InnerText = txtPassword.Text.Trim();
                }
                xmlDoc.Save(ApplicationPath + "\\config.xml");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btn_login.Focus();
            }
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
                login();
        }

        private void login()
        {
            if (!txtUserID.Text.Equals("") && !txtPassword.Text.Equals(""))
            {
                //MD5
                string pws = STL.PDA.Util.common.EncryptMD5(txtPassword.Text);

                try
                {
                    UserLogin.SI_MM028_PDA2SAP_LOGIN_OUTService svc = new UserLogin.SI_MM028_PDA2SAP_LOGIN_OUTService(); ;
                    svc.Url = WebServiceInstants.GetURL(ServiceType.LoginURL);
                    svc.Credentials = nc.GetCredential(new Uri(svc.Url), "Basic");
                    UserLogin.ZFIS_PDA_LOGIN_REQ login_req = new STL.PDA.UserLogin.ZFIS_PDA_LOGIN_REQ();
                    login_req.ZDATA = new STL.PDA.UserLogin.ZFIS_PDA_LOGIN_REQDATA();
                    login_req.ZDATA.PASSWORD = pws;
                    login_req.ZDATA.USERID = txtUserID.Text;
                    UserLogin.ZFIS_PDA_LOGIN_RES login_res = svc.SI_MM028_PDA2SAP_LOGIN_OUT(login_req);
                    if (login_res.IF_STATU.Equals("01"))
                    {
                        MainForm mfm = new MainForm();
                        mfm.Show();
                        this.Hide();
                        SaveToConfig();
                    }
                    else
                    {
                        MessageBox.Show(login_res.IFMSG, "登陆失败");
                    }
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Status + ";" + ex.Response);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please enter User Name and password.");
            }
        }

        private void libSet_Click(object sender, EventArgs e)
        {
            frmSetting frm = new frmSetting();
            frm.ShowDialog();
        }


        private void txtUserID_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
            {
                txtPassword.Focus();
            }
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btn_login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                login();
            }
        }
    }
}