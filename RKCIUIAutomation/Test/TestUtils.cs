using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using static RKCIUIAutomation.Config.ConfigUtils;


namespace RKCIUIAutomation.Test
{
    public class TestUtils : PageBase
    {
        private static string baseTempFolder;
        private static string fileName;
        private static string dateString;
        public static string fullTempFileName;

        public TestUtils()
        {
            baseTempFolder = $"{GetCodeBasePath()}\\Temp";
            fileName = tenantName.ToString();
            dateString = GetDateString();
        }

        private string GetDateString()
        {
            string[] shortDate = (DateTime.Today.ToShortDateString()).Split('/');
            string month = shortDate[0];
            if (month.Length < 1)
            {
                month = $"0{month}";
            }
            return $"{month}{shortDate[1]}{shortDate[2]}";
        }
       

        private static List<string> pageUrlList;

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

        /// <summary>
        /// Location to project Temp folder with Tenant name as filename
        /// -- Specify file type extention (i.e. - .xml)
        /// </summary>
        private static void WriteToFile(string msg, string fileExt = ".txt", bool overwriteExisting = false)
        {
            fullTempFileName = $"{baseTempFolder}\\{fileName}({dateString})";

            Directory.CreateDirectory(baseTempFolder);
            string path = $"{fullTempFileName}{fileExt}";
            StreamWriter workflow = null;

            if (overwriteExisting.Equals(true))
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                workflow = File.CreateText(path);
            }
            else
            {
                workflow = File.AppendText(path);
            }

            using (StreamWriter sw = workflow )
            {
                if (msg.Contains("<br>"))
                {
                    string[] message = Regex.Split(msg, "<br>&nbsp;&nbsp;");
                    sw.WriteLine(message[0]);
                    sw.WriteLine(message[1]);
                }
                else
                    sw.WriteLine(msg);
            }
        }

        public void LoopThroughNavMenu()
        {
            string pageUrl = string.Empty;
            WriteToFile($"{tenantName} Navigation Menu", ".txt", true); //create {overwrite existing} txt file
            WriteToFile(Environment.NewLine);

            IList<IWebElement> elements = new List<IWebElement>();
            elements = driver.FindElements(By.XPath("//ul[@class='nav navbar-nav']/li[@class='dropdown']"));  //MainNav Elements
            foreach (IWebElement mainNavElem in elements)
            {
                string mainNavMenuText = mainNavElem.FindElement(By.XPath("./a")).Text;
                string mainNavMsg = $">{mainNavMenuText}";
                log.Info(mainNavMsg); //i.e. Project
                WriteToFile(mainNavMsg); //create {overwrite existing} txt file

                IList<IWebElement> subMainNavElements = mainNavElem.FindElements(By.XPath("./ul/li"));
                foreach (IWebElement subMainNavElem in subMainNavElements)
                {
                    string subMainNavMsg;

                    if (!subMainNavElem.GetAttribute("class").Contains("dropdown-submenu"))
                    {
                        if (!string.IsNullOrEmpty(GetInnerText(subMainNavElem)))
                        {
                            pageUrl = GetElementHref(subMainNavElem);
                            pageUrlList.Add(pageUrl);

                            subMainNavMsg = $"  --{GetInnerText(subMainNavElem)} ({pageUrl})";
                            log.Info(subMainNavMsg); //i.e. Project>>MyDetails
                            WriteToFile(subMainNavMsg); //write to txt file
                        }
                    }
                    else
                    {
                        subMainNavMsg = $"  > {GetInnerText(subMainNavElem)}";
                        log.Info(subMainNavMsg); //i.e. Project>>Administration 
                        WriteToFile(subMainNavMsg); //write to txt file

                        IList<IWebElement> subMenuElements = subMainNavElem.FindElements(By.XPath("./ul/li"));
                        foreach (IWebElement subMenuElem in subMenuElements)
                        {
                            string subMenuMsg;

                            if (!subMenuElem.GetAttribute("class").Contains("dropdown-submenu"))
                            {
                                pageUrl = GetElementHref(subMenuElem);
                                pageUrlList.Add(pageUrl);

                                subMenuMsg = $"    --{GetInnerText(subMenuElem)} ({pageUrl})";
                                log.Info(subMenuMsg); //i.e. Project>>Administration>>Project Details
                                WriteToFile(subMenuMsg); //write to txt file
                            }
                            else
                            {
                                subMenuMsg = $"    > {GetInnerText(subMenuElem)}";
                                log.Info(subMenuMsg); //i.e. Project>>Administration>>User Management
                                WriteToFile(subMenuMsg); //write to txt file

                                IList<IWebElement> subSubMenuElements = subMenuElem.FindElements(By.XPath("./ul/li"));
                                foreach (IWebElement subSubMenuElem in subSubMenuElements)
                                {
                                    string subSubMenuMsg;

                                    if (!subSubMenuElem.GetAttribute("class").Contains("dropdown-submenu"))
                                    {
                                        pageUrl = GetElementHref(subSubMenuElem);
                                        pageUrlList.Add(pageUrl);

                                        subSubMenuMsg = $"       --{GetInnerText(subSubMenuElem)} ({pageUrl})";
                                        log.Info(subSubMenuMsg); //i.e. Project>>Administration>>System Configuration>>Disciplines
                                        WriteToFile(subSubMenuMsg); //write to txt file
                                    }
                                    else
                                    {
                                        subSubMenuMsg = $"       > {GetInnerText(subSubMenuElem)}";
                                        log.Info(subSubMenuMsg); //i.e. Project>>Administration>>System Configuration>>Equipment
                                        WriteToFile(subSubMenuMsg); //write to txt file

                                        IList<IWebElement> subSubMenuItems = subSubMenuElem.FindElements(By.XPath("./ul/li"));
                                        foreach (IWebElement subSubMenuItem in subSubMenuItems)
                                        {
                                            pageUrl = GetElementHref(subSubMenuItem);
                                            pageUrlList.Add(pageUrl);

                                            string subSubMenuItemMsg = $"         --{GetInnerText(subSubMenuItem)} ({pageUrl})";
                                            log.Info(subSubMenuItemMsg); //i.e. Project>>Administration>>System Configuration>>Equipment>>Equipment Makes
                                            WriteToFile(subSubMenuItemMsg); //write to txt file
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            WriteToFile(Environment.NewLine);
        }

        public List<string> GetNavMenuUrlList()
        {
            pageUrlList = new List<string>();
            LoopThroughNavMenu();
            return pageUrlList;
        }

        public bool VerifyUrlIsLoaded(string pageUrl)
        {
            List<string> errorMsgs = new List<string>
            {
                "The resource cannot be found."
            };

            bool isLoaded = false;
            string pageTitle = string.Empty;
            try
            {
                driver.Navigate().GoToUrl(pageUrl);
                pageTitle = driver.Title;

                if (!errorMsgs.Contains(pageTitle))
                {
                    isLoaded = true;
                }
            }
            finally
            {
                string pageTitleMsg = $"{pageUrl}<br>&nbsp;&nbsp;PageTitle: {pageTitle}";
                WriteToFile(pageTitleMsg, "_PageTitle.txt");
                LogInfo(pageTitleMsg, isLoaded);
            }

            return isLoaded;
        }


        private List<bool> assertionList;
        public void AddAssertionToList(bool assertion)
        {
            if (assertionList?.Any() != true)
            {
                assertionList = new List<bool>();
            }
            else
                assertionList.Add(assertion);
        }
        public void AssertAll()
        {
            Assert.Multiple(testDelegate: () =>
            {
                foreach(bool assertion in assertionList)
                Assert.True(assertion);
            });
        }

    }



    public class XMLUtil : TestUtils
    {
        public XmlSerializer xs;
        List<Navigation> ls;

        public void WriteXmlFile(string fileName)
        {

            ls = new List<Navigation>();
            xs = new XmlSerializer(typeof(List<Navigation>));

            FileStream fs = new FileStream(GetFilePath(fileName), FileMode.Create, FileAccess.Write);
            Navigation linklist = new Navigation
            {
                MainNavMenu = "",
                SubMainNavMenu = "",
                SubMenu = "",
                SubSubMenu = "",
                SubSubMenuItem = ""
            };
            ls.Add(linklist);

            xs.Serialize(fs, ls);
            fs.Close();
        }

        public void ReadXmlFile(string fileName)
        {
            FileStream fs = new FileStream(GetFilePath(fileName), FileMode.Open, FileAccess.Read);
            ls = (List<Navigation>)xs.Deserialize(fs);
            fs.Close();
        }

        string GetFilePath(string fileName) => $"{fullTempFileName}.xml";
    }

    public class Navigation
    {
        public string MainNavMenu { get; set; }
        public string SubMainNavMenu { get; set; }
        public string SubMenu { get; set; }
        public string SubSubMenu { get; set; }      
        public string SubSubMenuItem { get; set; }

    }

    
}
