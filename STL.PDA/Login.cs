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
        protected NetworkCredential nc = new NetworkCredential(WebServiceInstants.GetURL(ServiceType.userName).ToString(), WebServiceInstants.GetURL(ServiceType.password).ToString(), WebServiceInstants.GetURL(ServiceType.domain).ToString());
        public Login()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtUserID.Text = WebServiceInstants.GetURL(ServiceType.remUser);
            txtPassword.Text = WebServiceInstants.GetURL(ServiceType.remPassword);
        }

        private void SaveToConfig()
        {
            try
            {
                if (chkRememberMe.Checked)
                {
                    string ApplicationPath = Path.GetDirectoryName(
                        Assembly.GetExecutingAssembly().GetName().CodeBase);

                    XmlDocument xmlDoc = new XmlDocument();
                    xmlDoc.Load(ApplicationPath + "\\config.xml");
                    xmlDoc.SelectSingleNode("/config/remUser").InnerText = txtUserID.Text.Trim();
                    xmlDoc.SelectSingleNode("/config/remPassword").InnerText = txtPassword.Text.Trim();

                    xmlDoc.Save(ApplicationPath + "\\config.xml");
                }
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
                if (!txtUserID.Text.Equals("") && !txtPassword.Text.Equals(""))
                {
                    MD5 md5 = new MD5CryptoServiceProvider();
                    byte[] result = md5.ComputeHash(Encoding.UTF8.GetBytes(txtPassword.Text));
                    StringBuilder sBuilder = new StringBuilder();
                    foreach (byte t in result)
                    {
                        sBuilder.Append(t.ToString("x2"));
                    }

                    UserLogin.SI_MM028_PDA2SAP_LOGIN_OUTService svc = new UserLogin.SI_MM028_PDA2SAP_LOGIN_OUTService();
                    svc.Credentials = nc;
                    svc.Url = WebServiceInstants.GetURL(ServiceType.LoginURL);
                    UserLogin.ZFIS_PDA_LOGIN_REQ login_req = new STL.PDA.UserLogin.ZFIS_PDA_LOGIN_REQ();
                    login_req.ZDATA.PASSWORD = sBuilder.ToString();
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
                else
                {
                    MessageBox.Show("Please enter User Name and password.");
                }
            }
        }
    }
}