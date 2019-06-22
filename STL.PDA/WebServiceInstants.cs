using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Net;

namespace STL.PDA
{
    enum ServiceType
    {
        userName,
        password,
        remUser,
        remPassword,
        domain,
        LoginURL
    }
    class WebServiceInstants
    {
        public static DateTime EscalateDate(string DateInString)
        {
            string[] dtime = DateInString.Split(new char[] { '/' });
            DateTime WorkDate = new DateTime(Convert.ToInt16(dtime[2]), Convert.ToInt16(dtime[1]), Convert.ToInt16(dtime[0]));
            return WorkDate;
        }
        public static string GetURL(ServiceType ServiceType)
        {
            XmlDocument xmlDoc;
            string xmlFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase) + "\\config.xml";

            if (!System.IO.File.Exists(xmlFile))
            {
                MessageBox.Show("The config.xml file is not exist.\n Cannot Start the Application!");
                //Application.Exit();
                return "";
            }
            else
            {
                xmlDoc = new XmlDocument();
                xmlDoc.Load(xmlFile);
                switch (ServiceType)
                {
                    case ServiceType.password:
                        {
                            return xmlDoc.SelectSingleNode("/config/password").InnerText;
                        }
                    case ServiceType.userName:
                        {
                            return xmlDoc.SelectSingleNode("/config/userName").InnerText;
                        }
                    case ServiceType.remUser:
                        {
                            return xmlDoc.SelectSingleNode("/config/remUser").InnerText;
                        }
                    case ServiceType.remPassword:
                        {
                            return xmlDoc.SelectSingleNode("/config/remPassword").InnerText;
                        }
                    case ServiceType.domain:
                        {
                            return xmlDoc.SelectSingleNode("/config/domain").InnerText;
                        }
                    case ServiceType.LoginURL:
                        {
                            return xmlDoc.SelectSingleNode("/config/LoginURL").InnerText;
                        }
                        
                    default: return "";
                }
            }
        }

        public static UserLogin.SI_MM028_PDA2SAP_LOGIN_OUTService svc_UserLogin()
        {
            UserLogin.SI_MM028_PDA2SAP_LOGIN_OUTService svc = new UserLogin.SI_MM028_PDA2SAP_LOGIN_OUTService();
            svc.Credentials = WebServiceAuthentication();
            return svc;
        }

        public static NetworkCredential WebServiceAuthentication()
        {
            MainControl mainControl = new MainControl();
            NetworkCredential nc = new NetworkCredential();
            nc.Domain = mainControl.DomainName;
            nc.UserName = mainControl.ServiceUser;
            nc.Password = mainControl.Servicepassword;
            return nc;
        }
    }
}
