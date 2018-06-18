using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using static RKCIUIAutomation.Base.BaseUtils;
using static RKCIUIAutomation.Base.WebDriverFactory;

namespace RKCIUIAutomation.Test
{
    public static class TestUtils
    {
        private static List<string> pageUrlList;
        private static List<Navigation> ls;
        public static XmlSerializer xs;

        private static readonly string fileName = BaseClass.projectName.ToString();
        private static string xmlFilePath = $"c:\\Temp\\{fileName}.xml";


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


        public static void LoopThroughNavMenu()
        {
            ls = new List<Navigation>();
            xs = new XmlSerializer(typeof(List<Navigation>));
            pageUrlList = new List<string>();

            string pageUrl = string.Empty;

            FileStream fs = new FileStream(xmlFilePath, FileMode.Create, FileAccess.Write);
            Navigation linklist = new Navigation();

            IList<IWebElement> elements = new List<IWebElement>();
            elements = Driver.FindElements(By.XPath("//ul[@class='nav navbar-nav']/li[@class='dropdown']"));  //MainNav Elements
            foreach (IWebElement mainNavElem in elements)
            {
                string mainNavMenuText = mainNavElem.FindElement(By.XPath("./a")).Text;
                string mainNavMsg = $">{mainNavMenuText}";
                log.Info(mainNavMsg); //i.e. Project
                WriteToFile(fileName, mainNavMsg); //write to txt file
                linklist.MainNavMenu = mainNavMenuText; //write to xml file
                ls.Add(linklist);

                IList<IWebElement> subMainNavElements = mainNavElem.FindElements(By.XPath("./ul/li"));
                foreach (IWebElement subMainNavElem in subMainNavElements)
                {
                    string subMainNavMsg;
                    linklist = new Navigation();

                    pageUrl = GetElementHref(subMainNavElem);
                    pageUrlList.Add(pageUrl);

                    if (!subMainNavElem.GetAttribute("class").Contains("dropdown-submenu"))
                    {
                        subMainNavMsg = $"  --{GetInnerText(subMainNavElem)} ({pageUrl})";
                        log.Info(subMainNavMsg); //i.e. Project>>MyDetails
                        WriteToFile(fileName, subMainNavMsg); //write to txt file
                        linklist.SubMainNavMenu = GetInnerText(subMainNavElem); //write to xml file
                    }
                    else
                    {
                        subMainNavMsg = $"  > {GetInnerText(subMainNavElem)}";
                        log.Info(subMainNavMsg); //i.e. Project>>Administration 
                        WriteToFile(fileName, subMainNavMsg); //write to txt file
                        linklist.SubMainNavMenu = GetInnerText(subMainNavElem); //write to xml file

                        IList<IWebElement> subMenuElements = subMainNavElem.FindElements(By.XPath("./ul/li"));
                        foreach (IWebElement subMenuElem in subMenuElements)
                        {
                            string subMenuMsg;
                            linklist = new Navigation();

                            pageUrl = GetElementHref(subMenuElem);
                            pageUrlList.Add(pageUrl);

                            if (!subMenuElem.GetAttribute("class").Contains("dropdown-submenu"))
                            {                                
                                subMenuMsg = $"    --{GetInnerText(subMenuElem)} ({pageUrl})";
                                log.Info(subMenuMsg); //i.e. Project>>Administration>>Project Details
                                WriteToFile(fileName, subMenuMsg); //write to txt file
                                linklist.SubMenu = GetInnerText(subMenuElem); //write to xml file
                            }
                            else
                            {
                                subMenuMsg = $"    > {GetInnerText(subMenuElem)}";
                                log.Info(subMenuMsg); //i.e. Project>>Administration>>User Management
                                WriteToFile(fileName, subMenuMsg); //write to txt file
                                linklist.SubMenu = GetInnerText(subMenuElem); //write to xml file

                                IList<IWebElement> subSubMenuElements = subMenuElem.FindElements(By.XPath("./ul/li"));
                                foreach (IWebElement subSubMenuElem in subSubMenuElements)
                                {
                                    string subSubMenuMsg;
                                    linklist = new Navigation();

                                    pageUrl = GetElementHref(subSubMenuElem);
                                    pageUrlList.Add(pageUrl);

                                    if (!subSubMenuElem.GetAttribute("class").Contains("dropdown-submenu"))
                                    {
                                        subSubMenuMsg = $"       --{GetInnerText(subSubMenuElem)} ({pageUrl})";
                                        log.Info(subSubMenuMsg); //i.e. Project>>Administration>>System Configuration>>Disciplines
                                        WriteToFile(fileName, subSubMenuMsg); //write to txt file
                                        linklist.SubSubMenu = GetInnerText(subSubMenuElem); //write to xml file
                                    }
                                    else
                                    {
                                        subSubMenuMsg = $"       > {GetInnerText(subSubMenuElem)}";
                                        log.Info(subSubMenuMsg); //i.e. Project>>Administration>>System Configuration>>Equipment
                                        WriteToFile(fileName, subSubMenuMsg); //write to txt file
                                        linklist.SubSubMenu = GetInnerText(subSubMenuElem); //write to xml file

                                        IList<IWebElement> subSubMenuItems = subSubMenuElem.FindElements(By.XPath("./ul/li"));
                                        foreach (IWebElement subSubMenuItem in subSubMenuItems)
                                        {
                                            pageUrl = GetElementHref(subSubMenuElem);
                                            pageUrlList.Add(pageUrl);

                                            string subSubMenuItemMsg = $"         --{GetInnerText(subSubMenuItem)} ({pageUrl})";
                                            linklist = new Navigation();
                                            log.Info(subSubMenuItemMsg); //i.e. Project>>Administration>>System Configuration>>Equipment>>Equipment Makes
                                            WriteToFile(fileName, subSubMenuItemMsg); //write to txt file
                                            linklist.SubSubMenuItem = GetInnerText(subSubMenuItem); //write to xml file
                                            ls.Add(linklist);
                                        }
                                    }
                                    ls.Add(linklist);
                                }
                            }
                            ls.Add(linklist);
                        }
                    }
                    ls.Add(linklist);
                }
            }

            xs.Serialize(fs, ls);
            fs.Close();
        }


        public static void GetPageTitleForNavPages()
        {
            LoopThroughNavMenu();
            foreach (string url in pageUrlList)
            {
                Driver.Navigate().GoToUrl(url);
                string pageTitle = Driver.Title;
                string pageTitleMsg = $"PageTitle: {pageTitle}    ##PageURL: {url}";
                WriteToFile($"{fileName}_PageTitle", pageTitleMsg);
                log.Info(pageTitleMsg);
            }
        }

    }



    public class XMLUtil
    {
        public XmlSerializer xs;
        List<Navigation> ls;

        public void WriteXmlFile(string fileName)
        {

            ls = new List<Navigation>();
            xs = new XmlSerializer(typeof(List<Navigation>));

            FileStream fs = new FileStream(GetFilePath(fileName), FileMode.Create, FileAccess.Write);
            Navigation linklist = new Navigation();
            linklist.MainNavMenu = "";
            linklist.SubMainNavMenu = "";
            linklist.SubMenu = "";
            linklist.SubSubMenu = "";
            linklist.SubSubMenuItem = "";
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

        string GetFilePath(string fileName) => $"c:\\Temp\\{fileName}.xml";
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
