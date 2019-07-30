using NUnit.Framework;
using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Page;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using static RKCIUIAutomation.Base.Factory;
using System.Text.RegularExpressions;

namespace RKCIUIAutomation.Test
{
    public class TestUtils : PageHelper, ITestUtils//, IPageInteraction
    {   
        public TestUtils()
        {
        }

        //public IPageInteraction _pgAction { get; set; } = PageAction;

        public TestUtils(IWebDriver driver) => this.Driver = driver;

        [ThreadStatic]
        private List<string> pageUrlList;

        private string GetElemATagAttribute(IWebElement rootElement, string attributeName)
        {
            IWebElement anchorElem = null;
            string attribValue = string.Empty;

            try
            {
                anchorElem = rootElement.FindElement(By.XPath("./a"));
                attribValue = anchorElem.GetAttribute(attributeName);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return attribValue;
        }

        private string GetInnerText(IWebElement listElement)
            => GetElemATagAttribute(listElement, "innerText");

        private string GetElementHref(IWebElement listElement)
            => GetElemATagAttribute(listElement, "href");

        public void LoopThroughNavMenu()
        {
            try
            {
                string pageUrl = string.Empty;
                WriteToFile($"{tenantName} Navigation Menu", ".txt", true); //create {overwrite existing} txt file
                WriteToFile(Environment.NewLine);

                IList<IWebElement> elements = new List<IWebElement>();
                elements = PageAction.GetElements(By.XPath("//ul[@class='nav navbar-nav']/li[@class='dropdown']"));  //MainNav Elements
                if (elements.Any())
                {
                    foreach (IWebElement mainNavElem in elements)
                    {
                        string mainNavMenuText = mainNavElem.FindElement(By.XPath("./a")).Text;
                        string mainNavMsg = $">{mainNavMenuText}";
                        //log.Info(mainNavMsg); //i.e. Project
                        WriteToFile(mainNavMsg); //create {overwrite existing} txt file

                        IList<IWebElement> subMainNavElements = mainNavElem.FindElements(By.XPath("./ul/li"));
                        foreach (IWebElement subMainNavElem in subMainNavElements)
                        {
                            string subMainNavMsg;

                            if (!subMainNavElem.GetAttribute("class").Contains("dropdown-submenu"))
                            {
                                if (GetInnerText(subMainNavElem).HasValue())
                                {
                                    pageUrl = GetElementHref(subMainNavElem);
                                    pageUrlList.Add(pageUrl);

                                    subMainNavMsg = $"  --{GetInnerText(subMainNavElem)} ({pageUrl})";
                                    //log.Info(subMainNavMsg); //i.e. Project>>MyDetails
                                    WriteToFile(subMainNavMsg); //write to txt file
                                }
                            }
                            else
                            {
                                subMainNavMsg = $"  > {GetInnerText(subMainNavElem)}";
                                //log.Info(subMainNavMsg); //i.e. Project>>Administration
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
                                        //log.Info(subMenuMsg); //i.e. Project>>Administration>>Project Details
                                        WriteToFile(subMenuMsg); //write to txt file
                                    }
                                    else
                                    {
                                        subMenuMsg = $"    > {GetInnerText(subMenuElem)}";
                                        //log.Info(subMenuMsg); //i.e. Project>>Administration>>User Management
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
                                                //log.Info(subSubMenuMsg); //i.e. Project>>Administration>>System Configuration>>Disciplines
                                                WriteToFile(subSubMenuMsg); //write to txt file
                                            }
                                            else
                                            {
                                                subSubMenuMsg = $"       > {GetInnerText(subSubMenuElem)}";
                                                //log.Info(subSubMenuMsg); //i.e. Project>>Administration>>System Configuration>>Equipment
                                                WriteToFile(subSubMenuMsg); //write to txt file

                                                IList<IWebElement> subSubMenuItems = subSubMenuElem.FindElements(By.XPath("./ul/li"));
                                                foreach (IWebElement subSubMenuItem in subSubMenuItems)
                                                {
                                                    pageUrl = GetElementHref(subSubMenuItem);
                                                    pageUrlList.Add(pageUrl);

                                                    string subSubMenuItemMsg = $"         --{GetInnerText(subSubMenuItem)} ({pageUrl})";
                                                    //log.Info(subSubMenuItemMsg); //i.e. Project>>Administration>>System Configuration>>Equipment>>Equipment Makes
                                                    WriteToFile(subSubMenuItemMsg); //write to txt file
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Report.Error("!!! Unable to retrieve navigation menu URLs", false);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
            WriteToFile(Environment.NewLine);
        }

        public List<string> GetNavMenuUrlList()
        {
            pageUrlList = new List<string>();
            LoopThroughNavMenu();
            return pageUrlList;
        }

        [ThreadStatic]
        private static IList<KeyValuePair<string, bool>> assertionList;

        public void AddAssertionToList_VerifyPageHeader(string expectedPageHeader, string additionalDetails = "")
        {
            additionalDetails = additionalDetails.HasValue()
                ? $" - {additionalDetails}"
                : "";

            PageAction.WaitForPageReady();
            AddAssertionToList(PageAction.VerifyPageHeader(expectedPageHeader), $"VerifyPageTitle('{expectedPageHeader}'){additionalDetails}");
        }

        public void AddAssertionToList(bool assertion, string details = "")
        {
            KeyValuePair<string, bool> assertionKVPair = new KeyValuePair<string, bool>(details, assertion);

            if (assertionList == null)
            {
                log.Info("...Created new assertion list");
                assertionList = new List<KeyValuePair<string, bool>>();
            }

            log.Info("...added an assertion to the list");
            assertionList.Add(assertionKVPair);
        }

        public void AssertAll()
        {
            Assert.Multiple(testDelegate: () =>
            {
                int Assertions = 0;

                if (assertionList != null)
                {
                    foreach (KeyValuePair<string, bool> assertion in assertionList)
                    {
                        Assertions++;
                        try
                        {
                            log.Debug($"{assertion.Key} : {assertion.Value}");
                            Assert.That(assertion.Value, Is.True);
                        }
                        catch (Exception e)
                        {
                            Report.Error($"{e.Message}\n{e.StackTrace}");
                        }
                    }

                    log.Debug($"Total Number of Assertions in Test Case: {Assertions}");
                }
                else
                    log.Debug($"Did not find any assertions in the list to verify");
            });
        }

        public void HttpResponse()
        {
            string fileUrl = "";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(fileUrl);
            httpRequest.Method = WebRequestMethods.Http.Get;

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream httpResponseStream = httpResponse.GetResponseStream();
        }

        /// <summary>
        /// Location to project Temp folder with Tenant name as filename
        /// -- Specify file type extention (i.e. - .xml)
        /// </summary>
        public static void WriteToFile(string msg, string fileExt = ".txt", bool overwriteExisting = false)
        {
            try
            {
                string fileName = BaseUtil.GetTenantName();
                string dateString = BaseUtil.GetDateString();
                string tmpFolder = BaseUtil.GetBaseTempFolder();
                string fullFilePath = $"{tmpFolder}\\{fileName}({dateString})";

                Directory.CreateDirectory(tmpFolder);
                string path = $"{fullFilePath}{fileExt}";

                if (overwriteExisting == true)
                {
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }
                }

                StreamWriter streamWriter = File.Exists(path) ? File.AppendText(path) : File.CreateText(path);
                using (StreamWriter sw = streamWriter)
                {
                    if (msg.HasValue())
                    {
                        if (msg.Contains("<br>"))
                        {
                            string[] message = Regex.Split(msg, "<br>");
                            sw.WriteLine(message[0]);
                            sw.WriteLine(message[1]);
                        }
                        else
                        {
                            sw.WriteLine(msg);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }
    }
}