using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace RKCIUIAutomation.Tools
{
    public class XMLUtil
    {        
        public void XMLDocumentTool()
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml("TestResult.xml");
            
        }
    }
}
