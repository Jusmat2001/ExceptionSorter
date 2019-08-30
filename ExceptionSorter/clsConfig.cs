using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace ExceptionSorter
{
    public class clsConfig
    {
        public string sDataDBName = "";
        public string sDataServer = "";
        public string sUID = "";
        public string sPWD = "";
        public string sWindowsLogin = "";
        public string sSQLDBName = "";
        public string sToDir = "";

        public clsConfig(string sXMLConfig)
        {
            try
            {
                if (!(System.IO.File.Exists(sXMLConfig)))
                {
                    throw new Exception(sXMLConfig + " file is missing - it must be in the RUN directory for " + Application.ProductName);
                }
                DirectoryInfo di = new DirectoryInfo(".");
                string sStartupPath = Path.Combine(di.FullName, sXMLConfig);
                XmlTextReader objXmlTextReader = new XmlTextReader(sStartupPath);
                string sName = "";
                while (objXmlTextReader.Read())
                {
                    switch (objXmlTextReader.NodeType)
                    {
                        case XmlNodeType.Element:
                            sName = objXmlTextReader.Name;
                            break;
                        case XmlNodeType.Text:
                            if (sName == "UID") { sUID = objXmlTextReader.Value; break; }
                            if (sName == "Sel") { string sPWDs = objXmlTextReader.Value; sPWD = sPWDs.Substring(1, 7); break; }
                            if (sName == "DBName") { sDataDBName = objXmlTextReader.Value; break; }
                            if (sName == "DBServer") { sDataServer = objXmlTextReader.Value; break; }
                            if (sName == "SQLDBName") { sSQLDBName = objXmlTextReader.Value; break; }
                            if (sName == "ToDir") { sToDir = objXmlTextReader.Value; break; }
                            break;
                        default:
                            break;
                    }
                }
                objXmlTextReader.Close();
                sWindowsLogin = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Serious error starting: " + ex.Message, "Serious Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
