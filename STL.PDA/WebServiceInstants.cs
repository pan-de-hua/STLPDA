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
        RememberMe,
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
            string app_path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().GetName().CodeBase);
            string xmlFile = app_path + "\\config.xml";

            //MessageBox.Show(Path.GetFullPath(Assembly.GetExecutingAssembly().GetName().CodeBase));
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
                            return xmlDoc.SelectSingleNode("/config/WebService/password").InnerText;
                        }
                    case ServiceType.userName:
                        {
                            return xmlDoc.SelectSingleNode("/config/WebService/userName").InnerText;
                        }
                    case ServiceType.remUser:
                        {
                            return xmlDoc.SelectSingleNode("/config/remUser").InnerText;
                        }
                    case ServiceType.remPassword:
                        {
                            return xmlDoc.SelectSingleNode("/config/remPassword").InnerText;
                        }
                    case ServiceType.RememberMe:
                        {
                            return xmlDoc.SelectSingleNode("/config/RememberMe").InnerText;
                        }
                    case ServiceType.domain:
                        {
                            return xmlDoc.SelectSingleNode("/config/WebService/domain").InnerText;
                        }
                    case ServiceType.LoginURL:
                        {
                            StringBuilder s = new StringBuilder();
                            s.Append(xmlDoc.SelectSingleNode("/config/WebService/domain").InnerText);
                            s.Append(xmlDoc.SelectSingleNode("/config/WebService/LoginURL").InnerText);

                            return s.ToString() ;
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
