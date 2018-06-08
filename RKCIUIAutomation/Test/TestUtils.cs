using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;
using System.IO;

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

        public static void LoopThroughNavMenu()
        {
            string fileName = BaseClass.projectName.ToString();
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
}
