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
        private enum JSAction
        {
            [StringValue("arguments[0].click();")]Click,
            [StringValue("var evObj = document.createEvent('MouseEvents');" +
                    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                    "arguments[0].dispatchEvent(evObj);")]Hover
        }
        private void ExecuteJsAction(JSAction jsAction, By elementByLocator)
        {
            try
            {
                string javaScript = jsAction.GetString();
                IWebElement element = GetElement(elementByLocator);
                IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
                executor.ExecuteScript(javaScript, element);
                LogInfo($"{jsAction.ToString()}ed on javascript element - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogError($"Unable to perform {jsAction.ToString()} action on javascript element - {elementByLocator}", true, e);
                throw;
            }
        }

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
                LogError($"Unable to click element - {elementByLocator}", true, e);
            }
        }
        public void ClickElement(IWebElement webElement)
        {
            try
            {
                string buttonTxt = string.Empty;
                if (webElement != null)
                {
                    buttonTxt = webElement.GetAttribute("value");
                    webElement.Click();
                }               
                LogInfo($"Clicked {buttonTxt}");
            }
            catch (Exception e)
            {
                LogError($"Unable to click element", true, e);
            }
        }

        public void ClickJsElement(By elementByLocator) => ExecuteJsAction(JSAction.Click, elementByLocator);

        public void Hover(By elementByLocator)
        {
            ExecuteJsAction(JSAction.Hover, elementByLocator);
            Thread.Sleep(1000);
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
                LogError($"Unable to enter text in field - {elementByLocator}", true, e);
            }
        }

        public string GetText(By elementByLocator)
        {
            string text = string.Empty;
            try
            {
                text = GetElement(elementByLocator).Text;
                LogInfo($"Retrieved text, '{text}', from element - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogError($"Unable to retrieve text from element - {elementByLocator}", true, e);
            }
            
            return text;
        }

        public string GetTextFromDDL(Enum ddListID)
        {
            string text = string.Empty;
            try
            {
                text = $"{GetText(new PageHelper().GetDDListByLocator(ddListID))}//span[@class='k-input']";
                LogInfo($"Retrieved text '{text}' from element - {ddListID?.GetString()}");
            }
            catch (Exception e)
            {
                LogError($"Unable to retrieve text from drop-down element - {ddListID?.GetString()}", true, e);
            }
            return text;
        }

        public void ExpandDDL(Enum ddListID)
        {
            By locator = new PageHelper().GetExpandDDListButtonByLocator(ddListID);
            try
            {
                ClickElement(locator);
                Thread.Sleep(2000);
                LogInfo($"Expanded DDList - {ddListID?.GetString()}");
            }
            catch (Exception e)
            {
                LogError($"Unable to expand drop down list - {locator}", true, e);
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
            if (!isRemoteUpload)
            {
                uploadPath = $"{GetCodeBasePath()}\\UploadFiles\\{fileName}";
            }
            else
            {
                uploadPath = ""; //TODO - check if path format needs to change in docker environment
            }

            return uploadPath;
        }

        public void AcceptAlertMessage()
        {
            try
            {
                driver.SwitchTo().Alert().Accept();
                LogInfo("Accepted browser alert message");
            }
            catch (Exception e)
            {
                LogError($"Unable to accept browser alert message", true, e);
                throw;
            }          
        }

        public bool VerifyAlertMessage(string expectedMessage)
        {            
            string actualAlertMsg = driver.SwitchTo().Alert().Text;
            bool msgMatch = (actualAlertMsg).Contains(expectedMessage) ? true : false;
            LogInfo($"## Expected Alert Message: {expectedMessage} <br>&nbsp;&nbsp;## Actual Alert Message: {actualAlertMsg}", msgMatch);
            return msgMatch;
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
                PageTitle = element.Text;
                isDisplayed = true;
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
                        isMatchingTitle = IsElementDisplayed(headingElement) ? (GetElement(headingElement).Text).Contains(expectedPageTitle) : false;
                    }
                    else
                    {
                        isMatchingTitle = (GetElement(headingElement).Text).Contains(expectedPageTitle);
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
            string pageTitle = null;
            string expectedPageTitle = checkingLoginPage == false ? "ELVIS PMC" : "Log in";

            try
            {
                pageTitle = driver.Title;

                if (pageTitle.Contains(expectedPageTitle))
                {
                    LogInfo(">>> Page loaded ...No error seen on page <<<");
                }
                else
                {
                    IWebElement stackTraceTag = null;
                    stackTraceTag = GetElement(stackTraceTagByLocator);
                    if (stackTraceTag?.Displayed == true)
                    {
                        LogError("!!! Page did not load properly !!!");

                        if (continueTestIfPageNotLoaded == true)
                        {
                            driver.Navigate().Back();
                            LogDebug(">>> Navigating back to previous page to continue test <<<");
                            pageTitle = driver.Title;
                            if (pageTitle.Contains("ELVIS PMC"))
                            {
                                LogDebug(">>> Navigated to previous page successfully <<<");
                            }
                            else
                            {
                                stackTraceTag = GetElement(stackTraceTagByLocator);
                                if (stackTraceTag?.Displayed == true)
                                {
                                    Assert.True(false);
                                    LogError("!!! Page did not load properly, when navigating to the previous page !!!");
                                }
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

        
        private static string activeModalXpath => "//div[contains(@style,'opacity: 1')]";
        private static string modalTitle => $"{activeModalXpath}//div[contains(@class,'k-header')]";
        private static string modalCloseBtn => $"{activeModalXpath}//a[@aria-label='Close']";
        public void CloseActiveModalWindow()
        {
            ClickJsElement(By.XPath(modalCloseBtn));
            Thread.Sleep(500);
        }
        public bool VerifyActiveModalTitle(string expectedModalTitle)
        {
            string actualTitle = GetText(By.XPath(modalTitle));
            bool titlesMatch = actualTitle.Contains(expectedModalTitle) ? true : false;
            LogInfo($"## Expected Modal Title: {expectedModalTitle} <br>&nbsp;&nbsp;## Actual Modal Title: {actualTitle}", titlesMatch);
            return titlesMatch;
        }


        public void ClickCancel()
        {
            VerifyPageIsLoaded();
            IWebElement cancelBtn = GetElement(GetButtonByLocator("Cancel")) ??  GetElement(GetInputButtonByLocator("Cancel")) ?? GetElement(By.Id("CancelSubmittal"));
            ClickElement(cancelBtn);
        }
        public void ClickSave()
        {
            VerifyPageIsLoaded();
            ClickElement(By.Id("SaveSubmittal"));
        }
        public void ClickSubmitForward()
        {
            VerifyPageIsLoaded();
            ClickElement(By.Id("SaveForwardSubmittal"));
        }
        public void ClickCreate()
        {
            VerifyPageIsLoaded();
            ClickElement(By.Id("btnCreate"));
        }
        public void ClickNew()
        {
            VerifyPageIsLoaded();
            IWebElement newBtn = GetElement(GetButtonByLocator("New")) ?? GetElement(GetInputButtonByLocator("Create New"));
            ClickElement(newBtn);
        }

    }
}