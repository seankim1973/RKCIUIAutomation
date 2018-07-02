using AutoIt;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RKCIUIAutomation.Page
{
    public class Action : PageHelper
    {
        private bool WaitForElement(By elementByLocator)
        {
            try
            {
                LogInfo($"...waiting for element {elementByLocator}");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(250)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
                IWebElement webElem = wait.Until(x => x.FindElement(elementByLocator));
                return true;
            }
            catch (Exception e)
            {
                LogInfo($"WaitForElement timeout occured for element - {elementByLocator}", e);
            }
            return false;
        }

        private IWebElement GetElement(By elementByLocator)
        {
            IWebElement elem = null;
            if (WaitForElement(elementByLocator))
            {
                try
                {
                    elem = driver.FindElement(elementByLocator);
                    return elem;
                }
                catch (Exception e)
                {
                    LogInfo($"Unable to locate element - {elementByLocator}", e);
                }
            }
            return elem;
        }

        private IList<IWebElement> GetElements(By elementByLocator)
        {
            IList<IWebElement> elements = null;
            if (WaitForElement(elementByLocator))
            {
                try
                {
                    elements = new List<IWebElement>();
                    elements = driver.FindElements(elementByLocator);
                }
                catch (Exception e)
                {
                    LogInfo($"Unable to locate elements - {elementByLocator}", e);
                }
            }
            return elements;
        }

        public void ClickElement(By elementByLocator)
        {
            try
            {
                GetElement(elementByLocator).Click();
                LogInfo($"Clicked element - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogInfo($"Unable to click element - {elementByLocator}", e);
            }
        }

        public void HoverAndClick(By elemByToHover, By elemByToClick)
        {
            Hover(elemByToHover);
            ClickElement(elemByToClick);
        }

        public void Hover(By elementByLocator)
        {
            IWebElement element = GetElement(elementByLocator);
            try
            {
                string javaScript = "var evObj = document.createEvent('MouseEvents');" +
                    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                    "arguments[0].dispatchEvent(evObj);";

                IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
                executor.ExecuteScript(javaScript, element);
                LogInfo($"Hover mouse over element - {elementByLocator}");
                Thread.Sleep(1000);
            }
            catch (Exception e)
            {
                LogInfo($"Unsuccessful attempt to hover mouse over element {elementByLocator}", e);
            }
        }

        public void EnterText(By elementByLocator, string text)
        {
            try
            {
                GetElement(elementByLocator).SendKeys(text);
                LogInfo($"Entered '{text}' in field - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogInfo($"Unable to enter text in field - {elementByLocator}", e);
            }
        }

        public string GetText(By elementByLocator)
        {
            string text = String.Empty;
            try
            {
                text = GetElement(elementByLocator).Text;
                LogInfo($"Retrieved text '{text}' from element - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogInfo($"Unable to retrieve text from element - {elementByLocator}", e);
            }
            
            return text;
        }


        public string GetTextFromDDL(Enum ddListID) => $"{GetText(new PageHelper().GetDDListByLocator(ddListID))}//span[@class='k-input']";
        public void ExpandDDL(Enum ddListID)
        {
            By locator = new PageHelper().GetExpandDDListButtonByLocator(ddListID);
            try
            {
                ClickElement(locator);
                Thread.Sleep(2000);
                LogInfo($"Expanded DDList - {ddListID}");
            }
            catch (Exception e)
            {
                LogInfo($"Unable to expand drop down list - {locator}", e);
            }
        }

        public void ExpandAndSelectFromDDList<T>(Enum ddListID, T itemIndexOrName)
        {
            ExpandDDL(ddListID);
            ClickElement(new PageHelper().GetDDListItemsByLocator(ddListID, itemIndexOrName));
        }

        private void UploadUsingAutoItX(string filePath)
        {
            AutoItX.WinWaitActive("Open");
            LogInfo("...Waiting for File Open Dialog Window");
            AutoItX.ControlSend("Open", "", "Edit1", filePath);
            LogInfo($"Entered filepath {filePath}");
            AutoItX.ControlClick("Open", "&Open", "Button1");
            LogInfo("Clicked Open button on the File Open Dialog Window");
        }

        public void UploadFile(string fileName)
        {
            string filePath = null;
            if (testPlatform == TestPlatform.Local)
            {
                filePath = GetUploadFilePath(fileName);
                LogInfo("Uploading files in local environment");
                UploadUsingAutoItX(filePath);
            }
            else
            {
                filePath = GetUploadFilePath(fileName, true); //TODO - check if path format needs to change in docker environment
                LogInfo("Uploading files in remote environment");
                IAllowsFileDetection allowsFileDetection = driver as IAllowsFileDetection;
                allowsFileDetection.FileDetector = new LocalFileDetector();
                UploadUsingAutoItX(filePath);
            }
        }

        public string GetUploadFilePath(string fileName, bool isRemoteUpload = false)
        {
            string uploadPath = string.Empty;
            if (isRemoteUpload == false)
            {
                uploadPath = $"{GetCodeBasePath()}\\UploadFiles\\{fileName}";
            }
            else
            {
                uploadPath = ""; //TODO - check if path format needs to change in docker environment
            }

            return uploadPath;
        }

        public bool VerifyFieldErrorIsDisplayed(By elementByLocator )
        {
            IWebElement elem = GetElement(elementByLocator);
            bool elementDisplayed = elem.Displayed;
            string not = !elementDisplayed ? " not" : "";
            LogInfo($"Field error is{not} displayed for - {elementByLocator}", elementDisplayed);
            return elementDisplayed;
        }

        public bool VerifySuccessMessageIsDisplayed()
        {
            By elementByLocator = By.XPath("//div[contains(@class,'bootstrap-growl')]");
            IWebElement msg = GetElement(elementByLocator);
            bool elementDisplayed = msg.Displayed;
            string not = !elementDisplayed ? " not" : "";
            LogInfo($"Success Message is{not} displayed", elementDisplayed);
            return elementDisplayed;
        }

        private string PageTitle = string.Empty;
        public bool IsElementDisplayed(By elementByLocator)
        {
            IWebElement element = GetElement(elementByLocator);
            bool isDisplayed = false;

            if (element != null)
            {
                try
                {
                    isDisplayed = element.Displayed;
                    if (isDisplayed)
                        PageTitle = element.Text;
                }
                catch (Exception e)
                {
                    log.Debug(e.Message);
                }
            }

            return isDisplayed;
        }

        public bool VerifyPageTitle(string expectedPageTitle)
        {
            bool isMatchingTitle = false;
            bool isDisplayed = false;
            By headingElement = null;

            headingElement = By.XPath($"//h3[contains(text(),'{expectedPageTitle}')]");
            isDisplayed = IsElementDisplayed(headingElement);
            if (!isDisplayed)
            {
                headingElement = By.XPath($"//h2[contains(text(),'{expectedPageTitle}')]");
                isDisplayed = IsElementDisplayed(headingElement);
                if (!isDisplayed)
                {
                    LogDebug($"Page Title element with h2 or h3 tag containing text '{expectedPageTitle}' was not found.");

                    headingElement = By.XPath("//h3");
                    isDisplayed = IsElementDisplayed(headingElement);
                    if (!isDisplayed)
                    {
                        headingElement = By.XPath("//h2");
                        isDisplayed = IsElementDisplayed(headingElement);
                    }
                }
                else
                    isMatchingTitle = true;
            }
            else
                isMatchingTitle = true;

            if (isDisplayed)
            {
                LogInfo($"## Expect Page Title: {expectedPageTitle} <br>&nbsp;&nbsp;## Actual Page Title: {PageTitle}", isMatchingTitle);
            }
            else
            {
                LogDebug($"Could not find any Page element with h2 or h3 tag");
            }
            return isMatchingTitle;
        }

        public bool VerifySchedulerIsDisplayed() //TODO - move to Early Break Calendar class when more test cases are created
        {
            IWebElement scheduler = GetElement(By.Id("scheduler"));
            bool isDisplayed = scheduler.Displayed;
            string not = isDisplayed == false ? " not" : "";
            LogInfo($"Scheduler is{not} displayed", isDisplayed);
            return isDisplayed;
        }

        private readonly By stackTraceTagByLocator = By.XPath("//b[text()='Stack Trace:']");

        public bool VerifyUrlIsLoaded(string pageUrl)
        {
            bool isLoaded = false;
            string pageTitle = string.Empty;
            
            try
            {
                driver.Navigate().GoToUrl(pageUrl);
                pageTitle = driver.Title;

                if (pageTitle.Contains("ELVIS PMC"))
                {
                    LogInfo(">>> Page Loaded Successfully <<<");
                    isLoaded = true;
                }
                else
                {
                    IWebElement stackTraceTag = GetElement(stackTraceTagByLocator);
                    if(stackTraceTag?.Displayed == true)
                    {
                        LogError(">>> Page Did Not Load Successfully <<<");
                    }
                }                    
            }
            finally
            {
                string pageTitleMsg = $"{pageUrl}<br>&nbsp;&nbsp;PageHeader: {pageTitle}";
                WriteToFile(pageTitleMsg, "_PageTitle.txt");
                LogInfo(pageTitleMsg, isLoaded);
            }
            return isLoaded;
        }

        public void VerifyPageIsLoaded(bool checkingLoginPage = false, bool continueTestIfPageNotLoaded = true)
        {
            string pageTitle = driver.Title;
            string expectedPageTitle = checkingLoginPage == false ? "ELVIS PMC" : "Log in";

            try
            {
                if (pageTitle.Contains(expectedPageTitle))
                {
                    LogInfo(">>> Page Loaded Successfully <<<");
                }
                else
                {
                    IWebElement stackTraceTag = null;
                    stackTraceTag = GetElement(stackTraceTagByLocator);
                    if (stackTraceTag?.Displayed != true)
                    {
                        LogError(">>> Page did not load properly <<<");

                        if (continueTestIfPageNotLoaded == true)
                        {
                            driver.Navigate().Back();
                            LogDebug(">>> Navigating back to previous page to continue test <<<");
                            stackTraceTag = GetElement(stackTraceTagByLocator);
                            if (stackTraceTag?.Displayed == true)
                            {
                                Assert.True(false);
                                LogError(">>> Page did not load properly, when navigating to the previous page <<<");
                            }
                        }
                        else
                            Assert.True(false);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private readonly By Btn_Cancel = By.Id("CancelSubmittal");
        private readonly By Btn_Save = By.Id("SaveSubmittal");
        private readonly By Btn_SubmitForward = By.Id("SaveForwardSubmittal");
        private static readonly By Btn_Create = By.Id("btnCreate");

        public void ClickCancel()
        {
            VerifyPageIsLoaded();
            ClickElement(Btn_Cancel);
        }
        public void ClickSave()
        {
            VerifyPageIsLoaded();
            ClickElement(Btn_Save);
        }
        public void ClickSubmitForward()
        {
            VerifyPageIsLoaded();
            ClickElement(Btn_SubmitForward);
        }
        public void ClickCreate()
        {
            VerifyPageIsLoaded();
            ClickElement(Btn_Create);
        }
        public void ClickNew()
        {
            VerifyPageIsLoaded();
            ClickElement(GetButtonByLocator("New"));
        }
        public void ClickNew_InputBtn()
        {
            VerifyPageIsLoaded();
            ClickElement(GetInputFieldByLocator("Create New"));
        }
        public void ClickCancel_ATag()
        {
            VerifyPageIsLoaded();
            ClickElement(GetButtonByLocator("Cancel"));
        }
        public void ClickCancel_InputBtn()
        {
            VerifyPageIsLoaded();
            ClickElement(GetInputFieldByLocator("Cancel"));
        }

    }
}


//AutoItX.Run("notepad.exe", "C:\\Windows");
//AutoItX.WinWaitActive("Untitled");
//AutoItX.Send("Testing 1 2 3 4 5");
//IntPtr winHandle = AutoItX.WinGetHandle("Untitled");
//AutoItX.WinKill(winHandle);