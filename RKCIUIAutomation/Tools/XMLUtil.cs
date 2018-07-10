using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace RKCIUIAutomation.Tools
{
    public class XMLUtil : NUnitClass
    {        
        public void XMLDocumentTool()
        {
            string filePath = @"C:\Users\schong\source\repos\RKCIUIAutomation\RKCIUIAutomation\Tools\TestResult.xml";
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlElement elem = doc.DocumentElement;

            XmlNode testRunNode = elem.SelectSingleNode("//test-run");
            string result = testRunNode.Attributes["result"].Value;
            string total = testRunNode.Attributes["total"].Value;
            string pass = testRunNode.Attributes["passed"].Value;
            string failed = testRunNode.Attributes["failed"].Value;
            string skipped = testRunNode.Attributes["skipped"].Value;
            string inconclusive = testRunNode.Attributes["inconclusive"].Value;

            Console.WriteLine($"Result: {result}");
            Console.WriteLine($"Total: {total}");
            Console.WriteLine($"Passed: {pass}");
            Console.WriteLine($"Failed: {failed}");
            Console.WriteLine($"Skipped: {skipped}");
            Console.WriteLine($"Inconclusive: {inconclusive}\n");


            XmlNodeList tcList = elem.SelectNodes("//test-case");
            foreach (XmlNode tc in tcList)
            {
                string name = tc.Attributes["name"].Value;
                string tcResult = tc.Attributes["result"].Value;
                string time = tc.Attributes["duration"].Value;
                double.TryParse(time, out double duration);
                double remainder = duration % 60;
                duration = duration - remainder;
                int mins = Convert.ToInt32(duration / 60);
                int secs = Convert.ToInt32(remainder);
                
                Console.WriteLine($"Name: {name} \nResult: {tcResult} \nDuration: {mins} mins {remainder.ToString("00")} secs\n");

                XmlNode outputNode = tc.SelectSingleNode("output");
                string outputString = outputNode.InnerText;
                string line1 = outputString.Split(new[] { '\r', '\n' })[0];//.FirstOrDefault();
                Console.WriteLine(line1);
            }

            XmlNode testSuiteOutput = elem.SelectSingleNode("//test-run/test-suite/test-suite/test-suite/test-suite/test-suite/output");
            string[] output = Regex.Split(testSuiteOutput.InnerText, "  :  ");
            Console.WriteLine(output[1] + "\n");

        }
    }


}
