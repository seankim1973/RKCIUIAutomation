using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System.IO;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Test
{
    public static class TestUtils
    {
        private static string GetInnerText(IWebElement listElement)
        {
            IWebElement anchorElem = listElement.FindElement(By.XPath("./a"));
            return $"{anchorElem.GetAttribute("innerText")}";
        }
        private static string GetElementHref(IWebElement listElement)
        {
            IWebElement anchorElem = listElement.FindElement(By.XPath("./a"));
            return $"{anchorElem.GetAttribute("href")}";
        }


        private static void WriteToFile(string fileName, string text)
        {
            string path = $"c:\\Temp\\{fileName}.txt";
            if (!File.Exists(path))
            {
                // Create a file to write to.
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(text);
                }
            }
            else
            {
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(text);
                }
            }
        }


        public static void LoopThroughNavMenu(string fileName)
        {
            IList<IWebElement> elements = new List<IWebElement>();
            elements = WebDriverFactory.Driver.FindElements(By.XPath("//ul[@class='nav navbar-nav']/li[@class='dropdown']"));  //MainNav Elements

            foreach (IWebElement mainNavElem in elements)
            {
                string mainNavMenuText = mainNavElem.FindElement(By.XPath("./a")).Text;
                string mainNavMsg = $">{mainNavMenuText}";
                Console.WriteLine(mainNavMsg); //i.e. Project
                WriteToFile(fileName, mainNavMsg);
                IList<IWebElement> subMainNavElements = mainNavElem.FindElements(By.XPath("./ul/li"));

                foreach (IWebElement subMainNavElem in subMainNavElements)
                {
                    string subMainNavMsg;

                    if (!subMainNavElem.GetAttribute("class").Contains("dropdown-submenu"))
                    {
                        subMainNavMsg = $"  --{GetInnerText(subMainNavElem)} ({GetElementHref(subMainNavElem)})";
                        Console.WriteLine(subMainNavMsg); //i.e. Project>>MyDetails
                        WriteToFile(fileName, subMainNavMsg);
                    }
                    else
                    {
                        subMainNavMsg = $"  > {GetInnerText(subMainNavElem)}";
                        Console.WriteLine(); //i.e. Project>>Administration 
                        WriteToFile(fileName, subMainNavMsg);

                        IList<IWebElement> subMenuElements = subMainNavElem.FindElements(By.XPath("./ul/li"));
                        foreach (IWebElement subMenuElem in subMenuElements)
                        {
                            string subMenuMsg;

                            if (!subMenuElem.GetAttribute("class").Contains("dropdown-submenu"))
                            {
                                subMenuMsg = $"    --{GetInnerText(subMenuElem)} ({GetElementHref(subMenuElem)})";
                                Console.WriteLine(subMenuMsg); //i.e. Project>>Administration>>Project Details
                                WriteToFile(fileName, subMenuMsg);
                            }
                            else
                            {
                                subMenuMsg = $"    > {GetInnerText(subMenuElem)}";
                                Console.WriteLine(subMenuMsg); //i.e. Project>>Administration>>User Management
                                WriteToFile(fileName, subMenuMsg);

                                IList<IWebElement> subSubMenuElements = subMenuElem.FindElements(By.XPath("./ul/li"));
                                foreach (IWebElement subSubMenuElem in subSubMenuElements)
                                {
                                    string subSubMenuMsg;

                                    if (!subSubMenuElem.GetAttribute("class").Contains("dropdown-submenu"))
                                    {
                                        subSubMenuMsg = $"       --{GetInnerText(subSubMenuElem)} ({GetElementHref(subSubMenuElem)})";
                                        Console.WriteLine(subSubMenuMsg);
                                        WriteToFile(fileName, subSubMenuMsg);
                                    }
                                    else
                                    {
                                        subSubMenuMsg = $"       > {GetInnerText(subSubMenuElem)}";
                                        Console.WriteLine(subSubMenuMsg);
                                        WriteToFile(fileName, subSubMenuMsg);

                                        IList<IWebElement> subSubMenuItems = subSubMenuElem.FindElements(By.XPath("./ul/li"));
                                        foreach (IWebElement subSubMenuItem in subSubMenuItems)
                                        {
                                            string subSubMenuItemMsg = $"         --{GetInnerText(subSubMenuItem)} ({GetElementHref(subSubMenuItem)})";
                                            Console.WriteLine(subSubMenuItemMsg);
                                            WriteToFile(fileName, subSubMenuItemMsg);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }


    public class DocxUtil
    {
        public static void CreateDoc()
        {
            var stream = new MemoryStream();
            using (WordprocessingDocument doc = WordprocessingDocument.Create(stream, WordprocessingDocumentType.Document, true))
            {
                MainDocumentPart mainPart = doc.AddMainDocumentPart();

                new Document(new Body()).Save(mainPart);

                Body body = mainPart.Document.Body;
                body.Append(new Paragraph(
                            new Run(
                                new Text("Hello World!"))));

                mainPart.Document.Save();

                //if you don't use the using you should close the WordprocessingDocument here
                doc.Close();
            }

            stream.Seek(0, SeekOrigin.Begin);
            //return File(stream, "application/vnd.openxmlformats-officedocument.wordprocessingml.document", "test.docx");

        }

    }


}
