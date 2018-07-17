using AutoIt;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
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
            [StringValue("arguments[0].isDisplayed();")] IsDisplayed,
            [StringValue("arguments[0].getAttribute")] GetAttribute,
            [StringValue("arguments[0].hasAttribute")] HasAttribute,
            [StringValue("arguments[0].click();")] Click,
            [StringValue("var evObj = document.createEvent('MouseEvents');" +
                    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                    "arguments[0].dispatchEvent(evObj);")] Hover
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
        private object ExecuteJsAsync(IWebDriver driver, JSAction jsAction, By elementByLocator, string attribute = null)
        {
            object result = null;
            string javaScript = (jsAction == JSAction.HasAttribute || jsAction == JSAction.GetAttribute) ? $"{jsAction.GetString()}(\"{attribute}\");" : jsAction.GetString();
                        
            IWebElement element = GetElement(elementByLocator);
            if (element != null)
            {
                IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
                driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(10);
                result = executor.ExecuteAsyncScript(javaScript, element);
                LogInfo($"{jsAction.ToString()}ed on javascript element - {elementByLocator}");
            }
            else
            {
                LogDebug("Error during ExecuteJsAsync method");
            }
            return result;
        }
        public string JsGetAttribute(IWebDriver driver, By elementByLocator, string attribute)
        {
            var result = ExecuteJsAsync(driver, JSAction.GetAttribute, elementByLocator, attribute);
            if (result != null)
            {
                return (string)result;

            }
            else
            {
                LogDebug("Attribute returned null value");
                return string.Empty;
            }
                
        }
        public bool JsIsDisplayed(IWebDriver driver, By elementByLocator)
        {
            var result = ExecuteJsAsync(driver, JSAction.IsDisplayed, elementByLocator);
            if (result != null)
            {
                return (bool)result;

            }
            else
            {
                LogDebug("Element not found");
                return false;
            }
                
        }
        public bool JsHasAttribute(IWebDriver driver, By elementByLocator, string attribute)
        {
            var result = ExecuteJsAsync(driver, JSAction.HasAttribute, elementByLocator, attribute);
            if (result != null)
            {
                return (bool)result;
            }
            else
            {
                LogDebug("Element not found");
                return false;
            }
                
        }
        public void JsClickElement(By elementByLocator) => ExecuteJsAction(JSAction.Click, elementByLocator);
        public void JsHover(By elementByLocator)
        {
            ExecuteJsAction(JSAction.Hover, elementByLocator);
            Thread.Sleep(1000);
        }

        private bool WaitForElement(By elementByLocator, int timeOutInSeconds = 2, int pollingInterval = 250)
        {
            try
            {
                LogInfo($"...waiting for element {elementByLocator}");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
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
        internal void WaitForTableToLoad(IWebDriver driver, int timeOutInSeconds = 10, int pollingInterval = 500)
        {
            By activeTblBodyLocator = By.XPath("//div[contains(@style,'opacity: 1;')]//tbody");
            bool spinnerIsDisplayed = true;
            do
            {
                Thread.Sleep(500);
                spinnerIsDisplayed = JsIsDisplayed(driver, By.XPath("//div[@class='k-loading-image']"));
                log.Info("...waiting for loading spinner to clear");
            }
            while (spinnerIsDisplayed);

            bool tableBodyIsDisplayed = false;
            do
            {
                Thread.Sleep(500);
                tableBodyIsDisplayed = JsIsDisplayed(driver, activeTblBodyLocator);
                log.Info("...waiting for table body to become visible");
            }
            while (!tableBodyIsDisplayed);
        }

        internal IWebElement GetElement(By elementByLocator)
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
        public int GetElementsCount(By elementByLocator)
        {
            IList<IWebElement> elements = GetElements(elementByLocator);
            return elements.Count;
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
                    buttonTxt = webElement.Text;
                    webElement.Click();
                }               
                LogInfo($"Clicked {buttonTxt}");
            }
            catch (Exception e)
            {
                LogError($"Unable to click element", true, e);
            }
        }

        public string GetAttribute(By elementByLocator, string attributeName) => GetElement(elementByLocator).GetProperty(attributeName);

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
        public bool VerifySchedulerIsDisplayed() //TODO - move to Early Break Calendar class when more test cases are created
        {
            IWebElement scheduler = GetElement(By.Id("scheduler"));
            bool isDisplayed = scheduler.Displayed;
            string not = isDisplayed == false ? " not" : "";
            LogInfo($"Scheduler is{not} displayed", isDisplayed);
            return isDisplayed;
        }
        public bool ElementIsDisplayed(By elementByLocator)
        {
            IWebElement element = GetElement(elementByLocator);
            bool isDisplayed = (element != null) ? true : false;
            return isDisplayed;
        }

        private string PageTitle = string.Empty;
        private bool IsHeadingDisplayed(By elementByLocator)
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
            isDisplayed = IsHeadingDisplayed(headingElement);
            if (!isDisplayed)
            {
                headingElement = By.XPath($"//h2[contains(text(),'{expectedPageTitle}')]");
                isDisplayed = IsHeadingDisplayed(headingElement);
                if (!isDisplayed)
                {
                    LogDebug($"Page Title element with h2 or h3 tag containing text '{expectedPageTitle}' was not found.");

                    headingElement = By.XPath("//h3");
                    isDisplayed = IsHeadingDisplayed(headingElement);
                    if (!isDisplayed)
                    {
                        headingElement = By.XPath("//h2");
                        isMatchingTitle = IsHeadingDisplayed(headingElement) ? (GetElement(headingElement).Text).Contains(expectedPageTitle) : false;
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

        private string pageErrLogMsg = string.Empty;
        private bool FoundKnownPageErrors()
        {            
            By serverErrorH1Tag = By.XPath("//h1[contains(text(),'Server Error')]");
            By resourceNotFoundH2Tag = By.XPath("//h2/i[text()='The resource cannot be found.']");
            By stackTraceTagByLocator = By.XPath("//b[text()='Stack Trace:']");

            IWebElement pageErrElement = driver.FindElement(serverErrorH1Tag) ?? driver.FindElement(stackTraceTagByLocator) ?? driver.FindElement(resourceNotFoundH2Tag);
            bool foundKnownError = pageErrElement.Displayed ? true : false;
            string pageUrl = $"<br>&nbsp;&nbsp;@URL: {driver.Url}";
            pageErrLogMsg = foundKnownError ? $"!!! Page Error - {pageErrElement.Text}{pageUrl}" : $"!!! Page did not load as expected.{pageUrl}";
            return false;
        }

        public bool VerifyUrlIsLoaded(string pageUrl)
        {
            bool isLoaded = false;
            string pageTitle = string.Empty;
            
            try
            {
                driver.Navigate().GoToUrl(pageUrl);
                pageTitle = driver.Title;

                isLoaded = (pageTitle.Contains("ELVIS PMC")) ? true : FoundKnownPageErrors();                 
            }
            finally
            {
                string pageTitleMsg = $"{pageUrl} - Page Title: {pageTitle}<br>&nbsp;&nbsp;";
                string logMsg = isLoaded ? ">>> Page Loaded Successfully <<<" : pageErrLogMsg;            
                
                LogInfo($"{logMsg}<br>&nbsp;&nbsp;{pageTitleMsg}", isLoaded);
                if (!isLoaded)
                {
                    LogDebug($"Page Error seen at URL: {driver.Url}");
                }
                WriteToFile(pageTitleMsg, "_PageTitle.txt");
            }
            return isLoaded;
        }
        public void VerifyPageIsLoaded(bool checkingLoginPage = false, bool continueTestIfPageNotLoaded = true)
        {
            string pageTitle = null;
            string expectedPageTitle = (checkingLoginPage == false) ? "ELVIS PMC" : "Log in";
            string logMsg = string.Empty;
            bool isLoaded = false;

            try
            {
                pageTitle = driver.Title;
                isLoaded = (pageTitle.Contains(expectedPageTitle)) ? true : FoundKnownPageErrors();
                logMsg = isLoaded ? ">>> Page Loaded Successfully <<<" : pageErrLogMsg;
                LogInfo(logMsg, isLoaded);

                if (!isLoaded)
                {
                    if (continueTestIfPageNotLoaded == true)
                    {
                        LogInfo(">>> Attempting to navigate back to previous page to continue testing <<<");
                        driver.Navigate().Back();
                        pageTitle = driver.Title;
                        isLoaded = pageTitle.Contains("ELVIS PMC") ? true : FoundKnownPageErrors();
                        if (isLoaded)
                        {
                            LogInfo(">>> Navigated to previous page successfully <<<");
                        }
                        else
                        {
                            LogInfo($"!!! Page did not load properly, when navigating to the previous page !!!<br>&nbsp;&nbsp;{pageErrLogMsg}");
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        Assert.Fail();
                    }

                    InjectTestStatus(GetTestName(), TestStatus.Failed, pageErrLogMsg);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        private static string ActiveModalXpath => "//div[contains(@style,'opacity: 1')]";
        private static string ModalTitle => $"{ActiveModalXpath}//div[contains(@class,'k-header')]";
        private static string ModalCloseBtn => $"{ActiveModalXpath}//a[@aria-label='Close']";
        public void CloseActiveModalWindow()
        {
            JsClickElement(By.XPath(ModalCloseBtn));
            Thread.Sleep(500);
        }
        public bool VerifyActiveModalTitle(string expectedModalTitle)
        {
            string actualTitle = GetText(By.XPath(ModalTitle));
            bool titlesMatch = actualTitle.Contains(expectedModalTitle) ? true : false;
            LogInfo($"## Expected Modal Title: {expectedModalTitle} <br>&nbsp;&nbsp;## Actual Modal Title: {actualTitle}", titlesMatch);
            return titlesMatch;
        }

        public void ClickCancel()
        {
            VerifyPageIsLoaded();
            IWebElement cancelBtn = GetElement(GetButtonByLocator("Cancel")) ?? GetElement(GetInputButtonByLocator("Cancel")) ?? GetElement(By.Id("CancelSubmittal"));

            if (cancelBtn.Displayed)
            {
                cancelBtn.Click();
                LogInfo("Clicked Cancel");
            }
            else
            {
                LogError("Unable to click Cancel");
            }
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
        public void ClickTwice(By elementByLocator)
        {
            ClickElement(elementByLocator);
            Thread.Sleep(250);
            ClickElement(elementByLocator);
        }

    }
}