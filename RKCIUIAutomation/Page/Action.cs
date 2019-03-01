using Microsoft.Win32;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;

namespace RKCIUIAutomation.Page
{
    public class Action : PageHelper
    {
        private PageHelper pgHelper = new PageHelper();

        public Action() { }

        public Action(IWebDriver driver) => this.Driver = driver;

        private enum JSAction
        {
            [StringValue("arguments[0].click();")] Click,

            [StringValue("var evObj = document.createEvent('MouseEvents');" +
                    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                    "arguments[0].dispatchEvent(evObj);")]
            Hover
        }

        private void ExecuteJsAction(JSAction jsAction, By elementByLocator)
        {
            IWebElement element = null;

            try
            {
                driver = Driver;
                string javaScript = jsAction.GetString();
                element = GetElement(elementByLocator);
                IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
                executor.ExecuteScript(javaScript, element);
                log.Info($"{jsAction.ToString()}ed on javascript element: - {elementByLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        public string JsGetPageTitle(string windowHandle = "")
        {
            string title = string.Empty;

            try
            {
                if (!windowHandle.Equals(""))
                {
                    Driver.SwitchTo().Window(windowHandle);
                }
                IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
                title = (string)executor.ExecuteScript("return document.title");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return title;
        }

        public void JsClickElement(By elementByLocator)
        {
            try
            {
                ScrollToElement(elementByLocator);
                ExecuteJsAction(JSAction.Click, elementByLocator);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void JsHover(By elementByLocator)
        {
            ExecuteJsAction(JSAction.Hover, elementByLocator);
            Thread.Sleep(1000);
        }

        internal void WaitForElement(By elementByLocator, int timeOutInSeconds = 5, int pollingInterval = 500)
        {
            try
            {
                WaitForPageReady();

                try
                {
                    driver = Driver;
                    log.Info($"...waiting for element: - {elementByLocator}");
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    {
                        PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
                    };
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                    wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                    wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
                    wait.Until(x => x.FindElement(elementByLocator));
                }
                catch (Exception e)
                {
                    log.Error($"WaitForElement timeout occurred for element: - {elementByLocator}\n{e.Message}");
                    throw e;
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        internal void WaitForPageReady(int timeOutInSeconds = 20, int pollingInterval = 500)
        {
            log.Info($"...Waiting for page to be in Ready state #####");

            try
            {
                driver = Driver;
                IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;

                try
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds)) { };
                    wait.Until(x => (bool)javaScriptExecutor.ExecuteScript("return window.jQuery != undefined && jQuery.active === 0"));
                }
                catch (InvalidOperationException ex)
                {
                    log.Error(ex);

                    try
                    {
                        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds)) { };
                        wait?.Until(wd => javaScriptExecutor.ExecuteScript("return document.readyState").ToString() == "complete");
                    }
                    catch (Exception e)
                    {
                        log.Error(e.Message);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        public void RefreshWebPage()
        {
            try
            {
                Driver.Navigate().Refresh();
                log.Info("Refreshed Web Page");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public IWebElement GetElement(By elementByLocator)
        {
            IWebElement elem = null;
            WaitForElement(elementByLocator);

            try
            {
                driver = Driver;
                elem = driver.FindElement(elementByLocator);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return elem;
        }

        public IList<IWebElement> GetElements(By elementByLocator)
        {
            IList<IWebElement> elements = null;
            WaitForElement(elementByLocator);

            try
            {
                driver = Driver;
                elements = new List<IWebElement>();
                elements = driver.FindElements(elementByLocator);
                log.Info($"Getting list of WebElements: {elementByLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
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
            IWebElement elem = null;

            try
            {
                elem = GetElement(elementByLocator);
                ScrollToElement(elementByLocator);
                elem?.Click();
                bool elemNotNull = elem != null ? true : false;
                string logMsg = elemNotNull ? "Clicked" : "Null";
                LogStep($"{logMsg} element: - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogError(e.StackTrace);
                throw e;
            }
        }

        public void ClickElement(IWebElement webElement)
        {
            try
            {
                if (webElement != null)
                {
                    webElement?.Click();
                    LogInfo($"Clicked {webElement.Text}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }

        public string GetAttribute(By elementByLocator, string attributeName)
            => GetElement(elementByLocator)?.GetAttribute(attributeName);

        public IList<string> GetAttributes(By elementByLocator, string attributeName)
        {
            IList<IWebElement> elements = GetElements(elementByLocator);
            IList<string> attributes = new List<string>();

            foreach (IWebElement elem in elements)
            {
                string attrib = elem.GetAttribute(attributeName);
                attributes.Add(attrib);
            }

            return attributes;
        }

        public void EnterComment(CommentType commentType, int commentTabNumber = 1)
        {
            By commentTypeLocator = By.Id($"{commentType.GetString()}{commentTabNumber - 1}_");
            ScrollToElement(commentTypeLocator);

            try
            {
                string text = "Comment 123";
                IWebElement element = GetElement(commentTypeLocator);
                element.SendKeys(text);
                LogInfo($"Entered '{text}' in field - {commentTypeLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }

        public void EnterComment(By elementByLocator)
        {
            ScrollToElement(elementByLocator);

            try
            {
                string text = "Comment 123";
                GetElement(elementByLocator).SendKeys(text);
                LogInfo($"Entered '{text}' in field - {elementByLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }

        public void ClearText(By elementByLocator)
        {
            IWebElement textField = null;

            try
            {
                textField = GetElement(elementByLocator);
                textField.Clear();
                log.Debug($"Cleared text : {elementByLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }

        public void EnterText(By elementByLocator, string text, bool clearField = true)
        {
            IWebElement textField = null;

            try
            {
                textField = GetElement(elementByLocator);

                if (textField.Enabled)
                {
                    if (!textField.Displayed)
                    {
                        ScrollToElement(textField);
                    }

                    if (clearField)
                    {
                        textField.Clear();
                    }
                }
                else
                {
                    LogError($"Text field {elementByLocator.ToString()}, is disabled");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
            finally
            {
                try
                {
                    string logMsg = string.Empty;
                    bool validField = textField != null;

                    if (validField)
                    {
                        textField.Click();
                        textField.SendKeys(text);

                        logMsg = $"Entered '{text}' in field - {elementByLocator}";
                    }
                    else
                    {
                        logMsg = $"Element: {elementByLocator} is null";
                    }

                    LogInfo(logMsg, validField);
                }
                catch (Exception e)
                {
                    LogError(e.Message);
                    throw e;
                }
            }
        }

        public string GetPageTitle(int timeOutInSeconds = 10, int pollingInterval = 500)
        {
            string pageTitle = string.Empty;
            WaitForPageReady();

            try
            {
                driver = Driver;
                log.Info($"...waiting for page title");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
                wait.Until(x => driver.Title.HasValue());
            }
            catch (Exception e)
            {
                log.Error($"timed out while waiting for page title\n{e.Message}");
            }
            finally
            {
                pageTitle = driver.Title;
            }

            return pageTitle;
        }

        public string GetPageUrl(int timeOutInSeconds = 10, int pollingInterval = 500)
        {
            string pageUrl = string.Empty;
            WaitForPageReady();

            try
            {
                driver = Driver;
                log.Info($"...waiting for page title");
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
                wait.Until(x => driver.Url.HasValue());
            }
            catch (Exception e)
            {
                log.Error($"timed out while waiting for page url\n{e.Message}");
            }
            finally
            {
                pageUrl = driver.Url;
            }

            return pageUrl;
        }

        public string GetText(By elementByLocator)
        {
            IWebElement element = null;
            string text = string.Empty;

            try
            {
                element = GetElement(elementByLocator);
                if (element != null)
                {
                    ScrollToElement(element);
                    text = element.Text;
                    string logMsg = text.HasValue()
                        ? $"Retrieved '{text}'"
                        : $"Unable to retrieve text {text}";
                    LogDebug($"{logMsg} from element - {elementByLocator}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return text;
        }

        public IList<string> GetTextForElements(By elementByLocator)
        {
            IList<IWebElement> elements = GetElements(elementByLocator);

            IList<string> elementTextList = new List<string>();

            foreach (IWebElement elem in elements)
            {
                string elemText = elem.Text;
                elementTextList.Add(elemText);
            }

            return elementTextList;
        }

        public string GetTextFromDDL(Enum ddListID)
            => GetText(pgHelper.GetDDListCurrentSelectionByLocator(ddListID));

        public void ExpandDDL<E>(E ddListID)
        {
            By locator = pgHelper.GetExpandDDListButtonByLocator(ddListID);
            try
            {
                JsClickElement(locator);
                Thread.Sleep(1000);
                log.Info($"Expanded DDList - {ddListID.ToString()}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        /// <summary>
        /// Use [bool] useContains arg when selecting a DDList item with partial value for [T](string)itemIndexOrName
        /// <para>[bool] useContains arg defaults to false and is ignored if arg [I]itemIndexOrName is int type</para>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="ddListID"></param>
        /// <param name="itemIndexOrName"></param>
        /// <param name="useContains"></param>
        /// <returns></returns>
        public void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName, bool useContains = false)
        {
            ExpandDDL(ddListID);
            ClickElement(pgHelper.GetDDListItemsByLocator(ddListID, itemIndexOrName, useContains));
        }

        public void SelectRadioBtnOrChkbox(Enum chkbxOrRadioBtn, bool toggleChkBoxIfAlreadyChecked = true)
        {
            string chkbxOrRdoBtn = chkbxOrRadioBtn.GetString();
            string chkbxOrRdoBtnNameAndId = $"{chkbxOrRadioBtn.ToString()} (ID: {chkbxOrRdoBtn})";
            By locator = By.Id(chkbxOrRdoBtn);

            try
            {
                IWebElement element = GetElement(locator);

                if (element.Enabled)
                {
                    if (toggleChkBoxIfAlreadyChecked)
                    {
                        JsClickElement(locator);
                        LogInfo($"Selected: {chkbxOrRdoBtnNameAndId}");
                    }
                    else
                    {
                        log.Info("Specified not to toggle checkbox, if already selected");

                        if (!element.Selected)
                        {
                            JsClickElement(locator);
                            LogInfo($"Selected: {chkbxOrRdoBtnNameAndId}");
                        }
                        else
                        {
                            LogInfo($"Did not select element, because it is already selected: {chkbxOrRdoBtnNameAndId}");
                        }
                    }
                }
                else
                {
                    LogError($"Element {chkbxOrRadioBtn.ToString()}, is not selectable", true);
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public string UploadFile(string fileName = "")
        {
            fileName = fileName.Equals("") ? "test.xlsx" : fileName;
            string filePath = (testPlatform == TestPlatform.Local) ? $"{GetCodeBasePath()}\\UploadFiles\\{fileName}" : $"/home/seluser/UploadFiles/{fileName}";

            try
            {
                driver = Driver;
                By uploadInput_ByLocator = By.XPath("//input[@id='UploadFiles_0_']");
                driver.FindElement(uploadInput_ByLocator).SendKeys(filePath);
                log.Info($"Entered {filePath}' for file upload");

                By uploadStatusLabel = By.XPath("//strong[@class='k-upload-status k-upload-status-total']");
                WaitForElement(uploadStatusLabel);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return fileName;
        }

        /// <summary>
        /// Provide string or IList<string> of expected file names to verify is seen in the Attachments section of the Details Page
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expectedFileName"></param>
        /// <returns></returns>
        public bool VerifyUploadedFileNames<T>(T expectedFileName, bool beforeSubmitBtnAction = false)
        {
            By testListDIV = By.XPath("//h5/b[contains(text(),'Test List')]");
            ScrollToElement(testListDIV);

            bool fileNamesMatch = false;

            IList<IWebElement> actualFileNameList = null;

            IList<string> expectedNamesList = null;

            IList<bool> assertList = null;

            string expectedName = string.Empty;

            string logMsg = string.Empty;

            int actualCount = 0;

            Type argType = expectedFileName.GetType();
            BaseUtils baseUtils = new BaseUtils();

            try
            {
                if (argType != typeof(string) && argType != typeof(IList<string>))
                {
                    LogError($"Arg type should be string or IList<string> - Unexpected expectedFileName type: {argType}");
                }
                else
                {
                    string xpath = beforeSubmitBtnAction
                        ? "//ul[@class='k-upload-files k-reset']/li/div/span[@class='k-file-name']"
                        : "//div[contains(@class,'fileList')]";

                    By actualFileNameLocator = By.XPath(xpath);

                    actualFileNameList = new List<IWebElement>();
                    actualFileNameList = GetElements(actualFileNameLocator);
                    actualCount = actualFileNameList.Count;
                    assertList = new List<bool>();
                    bool fileNameIsExpected = false;

                    if (argType == typeof(string))
                    {
                        expectedName = baseUtils.ConvertToType<string>(expectedFileName);
                    }
                    else if (argType == typeof(IList<string>))
                    {
                        expectedNamesList = new List<string>();
                        expectedNamesList = baseUtils.ConvertToType<IList<string>>(expectedFileName);
                    }

                    if (actualCount > 0)
                    {
                        for (int i = 0; i < actualCount; i++)
                        {
                            IWebElement actualElem = actualFileNameList[i];
                            string actualName = string.Empty;
                            int afterSaveIndex = i + 1;

                            if (beforeSubmitBtnAction)
                            {
                                actualName = actualElem.Text;
                            }
                            else
                            {
                                actualElem = actualElem.FindElement(By.XPath($"{xpath}[{afterSaveIndex}]/descendant::span[1]"));
                                actualName = actualElem.Text;
                                string[] splitName = Regex.Split(actualName, " \\(");
                                actualName = splitName[0];
                            }

                            if (argType == typeof(string))
                            {
                                fileNameIsExpected = actualName.Equals(expectedName);
                                logMsg = $"Expected and Uploaded File Names Matched: {actualName} - {fileNameIsExpected}";
                            }
                            else if (argType == typeof(IList<string>))
                            {
                                fileNameIsExpected = expectedNamesList.Contains(actualName);
                                logMsg = $"Uploaded File Name: {actualName} in Expected File Names List - {fileNameIsExpected}";
                            }

                            assertList.Add(fileNameIsExpected);
                            LogInfo($"{logMsg}", fileNameIsExpected);
                        }
                    }
                    else
                    {
                        fileNameIsExpected = expectedFileName.Equals("") ? true : false;
                        assertList.Add(fileNameIsExpected);
                        LogInfo($"No uploaded file names are seen on the page<br>{actualFileNameLocator}", fileNameIsExpected);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            finally
            {
                if (expectedNamesList != null)
                {
                    bool countsMatch = expectedNamesList.Count == actualCount ? true : false;
                    assertList.Add(countsMatch);
                }

                fileNamesMatch = assertList.Contains(false) ? false : true;
            }

            return fileNamesMatch;
        }

        public void ConfirmActionDialog(bool confirmYes = true)
        {
            string alertMsg = string.Empty;
            string actionMsg = string.Empty;

            try
            {
                WaitForPageReady();
            }
            catch (UnhandledAlertException)
            {
                try
                {
                    driver = Driver;
                    IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(2)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                    alert = driver.SwitchTo().Alert();
                    alertMsg = alert.Text;

                    if (confirmYes)
                    {
                        alert.Accept();
                        actionMsg = "Accepted";
                    }
                    else
                    {
                        alert.Dismiss();
                        actionMsg = "Dismissed";
                    }

                    LogStep($"{actionMsg} Confirmation Dialog: {alertMsg}");
                }
                catch (NoAlertPresentException e)
                {
                    log.Error($"NoAlertPresentException Msg: {e.Message}");
                }
            }
        }

        public string AcceptAlertMessage()
        {
            string alertMsg = string.Empty;

            try
            {
                WaitForPageReady();
            }
            catch (UnhandledAlertException)
            {
                try
                {
                    driver = Driver;
                    IAlert alert = driver.SwitchTo().Alert();
                    alertMsg = alert.Text;
                    alert.Accept();
                    LogStep($"Accepted browser alert: '{alertMsg}'");
                }
                catch (NoAlertPresentException e)
                {
                    log.Error($"NoAlertPresentException Msg: {e.Message}");
                }
            }

            return alertMsg;
        }

        public string DismissAlertMessage()
        {
            string alertMsg = string.Empty;

            try
            {
                WaitForPageReady();
            }
            catch (UnhandledAlertException)
            {
                try
                {
                    driver = Driver;
                    IAlert alert = driver.SwitchTo().Alert();
                    alertMsg = alert.Text;
                    alert.Dismiss();
                    LogStep($"Dismissed browser alert: '{alertMsg}'");
                }
                catch (NoAlertPresentException e)
                {
                    log.Error($"NoAlertPresentException Msg: {e.Message}");
                }
            }

            return alertMsg;
        }

        public bool VerifyAlertMessage(string expectedMessage)
        {
            bool msgMatch = false;

            try
            {
                driver = Driver;
                string actualAlertMsg = driver.SwitchTo().Alert().Text;
                msgMatch = (actualAlertMsg).Contains(expectedMessage) ? true : false;
                LogInfo($"## Expected Alert Message: {expectedMessage}<br>## Actual Alert Message: {actualAlertMsg}", msgMatch);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            
            return msgMatch;
        }

        public bool VerifyFieldErrorIsDisplayed(By elementByLocator)
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
            bool isDisplayed = false;

            try
            {
                isDisplayed = ScrollToElement(elementByLocator)?.Displayed == true ? true : false;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isDisplayed;
        }

        public bool VerifyPageTitle(string expectedPageTitle)
        {
            bool isMatchingTitle = false;
            bool isDisplayed = false;
            string actualHeading = string.Empty;
            string logMsg = string.Empty;
            IWebElement headingElem = null;

            try
            {
                WaitForPageReady();
            }
            catch (Exception)
            {
            }
            finally
            {
                driver = Driver;
                headingElem = driver.FindElement(By.XPath("//h3"))
                    ?? driver.FindElement(By.XPath("//h2"))
                    ?? driver.FindElement(By.XPath("//h4"));

                isDisplayed = headingElem?.Displayed == true ? true : false;

                if (isDisplayed)
                {
                    actualHeading = headingElem.Text;
                    isMatchingTitle = actualHeading.Equals(expectedPageTitle);

                    logMsg = isMatchingTitle
                        ? $"## Page Title is as expected: {actualHeading}"
                        : $"Page titles did not match: Expected: {expectedPageTitle}, Actual: {actualHeading}";

                    if (!isMatchingTitle)
                    {
                        BaseHelper.InjectTestStatus(TestStatus.Failed, logMsg);
                    }
                }
                else
                {
                    logMsg = $"Could not find page title with h2, h3 or h4 tag";
                    BaseHelper.InjectTestStatus(TestStatus.Failed, logMsg);
                }
            }

            LogInfo(logMsg, isMatchingTitle);

            return isMatchingTitle;
        }

        [ThreadStatic]
        private string logMsgKey;

        private bool IsPageLoadedSuccessfully()
        {
            bool isPageLoaded = true;
            string pageUrl = GetPageUrl();
            //Console.WriteLine($"##### IsPageLoadedSuccessfully - Page URL: {pageUrl}");
            try
            {
                By serverErrorH1Tag = By.XPath("//h1[contains(text(),'Server Error')]");
                By resourceNotFoundH2Tag = By.XPath("//h2/i[text()='The resource cannot be found.']");
                By stackTraceTagByLocator = By.XPath("//b[text()='Stack Trace:']");

                IWebElement pageErrElement = null;
                pageErrElement = GetElement(serverErrorH1Tag)
                    ?? GetElement(stackTraceTagByLocator)
                    ?? GetElement(resourceNotFoundH2Tag);

                isPageLoaded = pageErrElement.Displayed
                    ? false
                    : true;
                //Console.WriteLine($"##### IsPageLoadedSuccessfully - IsPageLoaded: {isPageLoaded}");

                string logMsg = isPageLoaded 
                    ? $"!!! Page Error - {pageErrElement.Text}<br>{pageUrl}"
                    : $"!!! Error at {pageUrl}";
                //Console.WriteLine($"##### IsPageLoadedSuccessfully - LogMsg: {logMsg}");

                logMsgKey = $"{testEnv}{tenantName}{GetTestName()}";
                PgBaseHelper.CreateVar(logMsgKey, logMsg);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isPageLoaded;
        }

        private string GetPageErrorLogMsg() => PgBaseHelper.GetVar(logMsgKey);

        public bool VerifyUrlIsLoaded(string pageUrl)
        {
            bool isLoaded = false;
            string logMsg = string.Empty;
            string pageTitle = string.Empty;

            try
            {
                driver = Driver;
                driver.Navigate().GoToUrl(pageUrl);

                pageTitle = GetPageTitle();
                isLoaded = pageTitle.Contains("ELVIS PMC")
                    ? true
                    : IsPageLoadedSuccessfully();

                logMsg = isLoaded
                    ? $">>> Page Loaded Successfully <<<<br>{pageUrl}"
                    : GetPageErrorLogMsg();

                LogInfo(logMsg, isLoaded);

                WriteToFile(logMsg, "_PageTitle.txt");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isLoaded;
        }

        public void VerifyPageIsLoaded(bool checkingLoginPage = false, bool continueTestIfPageNotLoaded = true)
        {
            bool isLoaded = false;
            string pageTitle = null;
            string logMsg = string.Empty;

            string expectedPageTitle = checkingLoginPage
                ? "Log in"
                : "ELVIS PMC";

            try
            {
                pageTitle = GetPageTitle();
                isLoaded = pageTitle.Contains(expectedPageTitle)
                    ? true 
                    : IsPageLoadedSuccessfully();

                logMsg = isLoaded 
                    ? ">>> Page Loaded Successfully <<<"
                    : GetPageErrorLogMsg();

                LogInfo(logMsg, isLoaded);

                if (!isLoaded)
                {
                    if (continueTestIfPageNotLoaded == true)
                    {
                        driver = Driver;
                        LogInfo(">>> Attempting to navigate back to previous page to continue testing <<<");
                        driver.Navigate().Back();
                        WaitForPageReady();

                        pageTitle = GetPageTitle();
                        isLoaded = pageTitle.Contains("ELVIS PMC")
                            ? true
                            : IsPageLoadedSuccessfully();

                        if (isLoaded)
                        {
                            LogInfo(">>> Navigated to previous page successfully <<<");
                        }
                        else
                        {
                            LogInfo($"!!! Page did not load properly, when navigating to the previous page !!!<br>{logMsg}");
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        Assert.Fail();
                    }

                    BaseHelper.InjectTestStatus(TestStatus.Failed, logMsg);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }

        public bool VerifyChkBoxRdoBtnSelection(Enum rdoBtnOrChkBox, bool shouldBeSelected = true)
        {
            try
            {
                //string selectionId = rdoBtnOrChkBox.GetString();
                By locator = By.Id(rdoBtnOrChkBox.GetString());
                ScrollToElement(locator);
                //IWebElement rdoBtnOrChkBoxElement = GetElement(locator);

                bool isSelected = GetElement(locator).Selected ? true : false;
                bool isResultExpected = isSelected.Equals(shouldBeSelected) ? true : false;

                string expected = shouldBeSelected ? " " : " Not ";
                string actual = isSelected ? " " : " Not ";

                LogInfo($"{rdoBtnOrChkBox.ToString()} :<br>Selection Expected: {shouldBeSelected}<br>Selection Actual: {isSelected}", isResultExpected);

                return isResultExpected;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                return false;
            }
        }

        public bool VerifyDDListSelectedValue(Enum ddListId, string expectedDDListValue)
        {
            bool meetsExpectation = false;

            try
            {
                string currentDDListValue = GetTextFromDDL(ddListId);
                meetsExpectation = currentDDListValue.Equals(expectedDDListValue) ? true : false;

                LogInfo($"Expected drop-down field value: {expectedDDListValue}" +
                    $"<br>Actual drop-down field value: {currentDDListValue}", meetsExpectation);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return meetsExpectation;
        }

        public bool VerifyInputField(Enum inputField, bool shouldFieldBeEmpty = false)
        {
            string text = string.Empty;
            bool isResultExpected = false;
            try
            {
                text = GetAttribute(By.XPath($"//input[@id='{inputField.GetString()}']"), "value");

                bool isFieldEmpty = string.IsNullOrEmpty(text) ? true : false;
                string logMsg = isFieldEmpty ? "Empty Field: Unable to retrieve text" : $"Retrieved '{text}'";

                isResultExpected = shouldFieldBeEmpty.Equals(isFieldEmpty);
                LogInfo($"{logMsg} from field - {inputField.ToString()}", isResultExpected);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isResultExpected;
        }

        public bool VerifyTextAreaField(Enum textAreaField, bool emptyFieldExpected = false)
        {
            bool result = false;

            try
            {
                string text = GetText(GetTextAreaFieldByLocator(textAreaField));

                if (text.HasValue())
                {
                    if (!emptyFieldExpected)
                    {
                        result = true;
                    }
                }
                else
                {
                    if (emptyFieldExpected)
                    {
                        result = true;
                    }
                }

                string expected = emptyFieldExpected 
                    ? "text field should be empty" 
                    : $"retrieved text: {text}";
                string logMsg = result 
                    ? "" 
                    : "NOT ";

                LogInfo($"Result is {logMsg}as expected:<br>{expected}", result);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return result;
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
            LogInfo($"## Expected Modal Title: {expectedModalTitle}<br>## Actual Modal Title: {actualTitle}", titlesMatch);
            return titlesMatch;
        }

        public void ClickCancel()
        {
            //VerifyPageIsLoaded();
            WaitForPageReady();
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
            //VerifyPageIsLoaded();
            WaitForPageReady();
            ClickElement(By.Id("SaveSubmittal"));
        }

        public void ClickSubmitForward()
        {
            //VerifyPageIsLoaded();
            WaitForPageReady();
            ClickElement(By.Id("SaveForwardSubmittal"));
        }

        public void ClickCreate()
        {
            //VerifyPageIsLoaded();
            WaitForPageReady();
            ClickElement(By.Id("btnCreate"));
        }

        public void ClickNew()
        {
            //VerifyPageIsLoaded();
            WaitForPageReady();
            IWebElement newBtn = GetElement(GetButtonByLocator("New")) ?? GetElement(GetInputButtonByLocator("Create New"));
            ClickElement(newBtn);
        }

        public IWebElement ScrollToElement(By elementByLocator)
        {
            IWebElement elem = null;

            try
            {
                driver = Driver;
                elem = GetElement(elementByLocator);

                //if (elem.Enabled)
                if (elem != null)
                {
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(elem);
                    actions.Perform();
                    log.Info($"Scrolled to element - {elementByLocator}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return elem;
        }

        public void ScrollToElement(IWebElement element)
        {
            try
            {
                driver = Driver;
                if (element != null)
                {
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(element);
                    actions.Perform();
                    log.Info($"Scrolled to WebElement");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void LogoutToLoginPage()
        {
            ClickLogoutLink();
            ClickLoginLink();
        }

        public void ClickLoginLink() => GetElement(By.XPath("//a[contains(text(),'Login')]")).Click();

        public void ClickLogoutLink() => GetElement(By.XPath("//a[contains(text(),'Log out')]")).Click();

        public string GetCurrentUser()
        {
            string[] acct = null;
            By locator = By.XPath("//a[@href='/Project/Account/']");
            string userAcct = GetText(locator);

            try
            {
                if (userAcct.Contains("X"))
                {
                    acct = Regex.Split(userAcct, "Welcome Test ");
                    acct = Regex.Split(acct[1], " X");
                    userAcct = acct[0];
                }
                else
                {
                    acct = Regex.Split(userAcct, "Welcome ");
                    userAcct = acct[1];
                }

                log.Info($"Getting current user: {userAcct}");
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }

            return userAcct;
        }

        public string GetUserDownloadFolderPath()
            => Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", string.Empty).ToString();
    }
}