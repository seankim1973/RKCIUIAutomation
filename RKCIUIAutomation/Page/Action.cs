﻿using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
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
        private BaseUtils baseUtils = new BaseUtils();

        public Action()
        {
        }

        public Action(IWebDriver driver) => this.Driver = driver;

        private enum JSAction
        {
            [StringValue("arguments[0].click();")] Click,

            [StringValue("var evObj = document.createEvent('MouseEvents');" +
                    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                    "arguments[0].dispatchEvent(evObj);")]
            Hover,

            [StringValue("")] GetCssValue
        }

        private void ExecuteJsAction(JSAction jsAction, By elementByLocator)
        {
            IWebElement element = null;

            try
            {
                string javaScript = jsAction.GetString();
                element = GetElement(elementByLocator);
                IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
                executor.ExecuteScript(javaScript, element);
                log.Info($"{jsAction.ToString()}ed on javascript element: - {elementByLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
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

        private void WaitForElement(By elementByLocator, int timeOutInSeconds = 5, int pollingInterval = 500)
        {
            try
            {
                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
            finally
            {
                try
                {
                    log.Info($"...waiting for element: - {elementByLocator}");
                    WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutInSeconds))
                    {
                        PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
                    };
                    wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                    wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                    wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                    wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
                    IWebElement webElem = wait.Until(x => x.FindElement(elementByLocator));
                }
                catch (Exception e)
                {
                    log.Error($"WaitForElement timeout occurred for element: - {elementByLocator}\n{e.Message}");
                }
            }
        }

        internal void WaitForPageReady(int timeOutInSeconds = 20, int pollingInterval = 500)
        {
            IJavaScriptExecutor javaScriptExecutor = null;
            WebDriverWait wait = null;

            try
            {
                javaScriptExecutor = Driver as IJavaScriptExecutor;
                wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutInSeconds))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
                };
                bool readyCondition(IWebDriver webDriver) => (bool)javaScriptExecutor.ExecuteScript("return (document.readyState == 'complete' && jQuery.active == 0)");
                wait.Until(readyCondition);
            }
            catch (InvalidOperationException)
            {
                wait.Until(wd => javaScriptExecutor.ExecuteScript("return document.readyState").ToString() == "complete");
            }

            log.Info($"...Waiting for page to be in Ready state #####");
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
                elem = Driver.FindElement(elementByLocator);
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
                elements = new List<IWebElement>();
                elements = Driver.FindElements(elementByLocator);
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
            try
            {
                IWebElement elem = GetElement(elementByLocator);
                ScrollToElement(elementByLocator);
                elem?.Click();
                bool elemNotNull = elem != null ? true : false;
                string logMsg = elemNotNull ? "Clicked" : "Null";
                LogInfo($"{logMsg} element: - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogError(e.StackTrace);
            }
        }

        public void ClickElement(IWebElement webElement)
        {
            try
            {
                if (webElement != null)
                {
                    webElement.Click();
                    LogInfo($"Clicked {webElement.Text}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public string GetAttribute(By elementByLocator, string attributeName) => GetElement(elementByLocator).GetAttribute(attributeName);

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
                GetElement(commentTypeLocator).SendKeys(text);
                LogInfo($"Entered '{text}' in field - {commentTypeLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
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
                }
            }
        }

        public string GetPageTitle()
        {
            string pageTitle = string.Empty;

            try
            {
                Thread.Sleep(1000);
                WaitForPageReady();
            }
            catch (Exception)
            {
            }
            finally
            {
                pageTitle = Driver.Title;
            }

            return pageTitle;
        }

        public string GetPageUrl()
        {
            string pageUrl = string.Empty;

            try
            {
                Thread.Sleep(1000);
                WaitForPageReady();
            }
            catch (Exception)
            {
            }
            finally
            {
                pageUrl = Driver.Url;
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
                    bool validText = !string.IsNullOrEmpty(text);
                    string logMsg = validText ? $"Retrieved '{text}'" : $"Unable to retrieve text {text}";
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
            //var _ddListID = (ddListID.GetType() == typeof(string)) ? ConvertToType<string>(ddListID) : ConvertToType<Enum>(ddListID).GetString();
            By locator = pgHelper.GetExpandDDListButtonByLocator(ddListID);
            try
            {
                ClickElement(locator);
                Thread.Sleep(1000);
                log.Info($"Expanded DDList - {ddListID.ToString()}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName)
        {
            //var _ddListID = (ddListID.GetType() == typeof(string)) ? ConvertToType<string>(ddListID) : ConvertToType<Enum>(ddListID).GetString();
            ExpandDDL(ddListID);
            ClickElement(pgHelper.GetDDListItemsByLocator(ddListID, itemIndexOrName));
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
                By uploadInput_ByLocator = By.XPath("//input[@id='UploadFiles_0_']");
                //EnterText(uploadInput_ByLocator, filePath, false);
                Driver.FindElement(uploadInput_ByLocator).SendKeys(filePath);
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
                        expectedName = ConvertToType<string>(expectedFileName);
                    }
                    else if (argType == typeof(IList<string>))
                    {
                        expectedNamesList = new List<string>();
                        expectedNamesList = ConvertToType<IList<string>>(expectedFileName);
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

        public string ConfirmActionDialog(bool confirmYes = true)
        {
            IAlert alert = null;
            string logMsg = string.Empty;
            string alertText = string.Empty;

            try
            {
                alert = new WebDriverWait(Driver, TimeSpan.FromSeconds(2)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
                alert = Driver.SwitchTo().Alert();
                alertText = alert.Text;

                if (confirmYes)
                {
                    alert.Accept();
                    logMsg = "Accepted";
                }
                else
                {
                    alert.Dismiss();
                    logMsg = "Dismissed";
                }
            }
            catch (Exception e)
            {
                log.Error($"ConfirmActionDialog - {e.Message}");
            }

            return $"{logMsg} Confirmation Dialog: {alertText}";
        }

        public string GetAlertMessage()
        {
            string alertMsg = string.Empty;

            try
            {
                IAlert alert = Driver.SwitchTo().Alert();
                alertMsg = alert.Text;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return alertMsg;
        }

        public void AcceptAlertMessage()
        {
            try
            {
                Driver.SwitchTo().Alert().Accept();
                LogInfo("Accepted browser alert message");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        public bool VerifyAlertMessage(string expectedMessage)
        {
            string actualAlertMsg = Driver.SwitchTo().Alert().Text;
            bool msgMatch = (actualAlertMsg).Contains(expectedMessage) ? true : false;
            LogInfo($"## Expected Alert Message: {expectedMessage}<br>## Actual Alert Message: {actualAlertMsg}", msgMatch);
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
                isDisplayed = GetElement(elementByLocator)?.Displayed == true ? true : false;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isDisplayed;
        }

        [ThreadStatic]
        private string PageTitle;

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

            //headingElement = By.XPath($"//h3[contains(text(),\"{expectedPageTitle}\")]");
            //isDisplayed = IsHeadingDisplayed(headingElement);
            //if (!isDisplayed)
            //{
            //    headingElement = By.XPath($"//h2[contains(text(),\"{expectedPageTitle}\")]");
            //    isDisplayed = IsHeadingDisplayed(headingElement);
            //    if (!isDisplayed)
            //    {
            //        LogDebug($"Page Title element with h2 or h3 tag containing text \"{expectedPageTitle}\" was not found.");

            headingElement = By.TagName("h3");
            isDisplayed = IsHeadingDisplayed(headingElement);
            if (!isDisplayed)
            {
                headingElement = By.TagName("h2");
                isDisplayed = IsHeadingDisplayed(headingElement);
                if (!isDisplayed)
                {
                    string logMsg = $"Could not find any Page element with h2 or h3 tag";
                    BaseHelper.InjectTestStatus(TestStatus.Failed, logMsg);
                }
            }
            //    }
            //}

            isMatchingTitle = PageTitle.Equals(expectedPageTitle);

            if (isDisplayed)
            {
                LogInfo($"## Expect Page Title: {expectedPageTitle}<br>## Actual Page Title: {PageTitle}", isMatchingTitle);

                if (!isMatchingTitle)
                {
                    string logMsg = $"Page titles did not match: Expected: {expectedPageTitle}, Actual: {PageTitle}";
                    BaseHelper.InjectTestStatus(TestStatus.Failed, logMsg);
                }
            }

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
                pageErrElement = GetElement(serverErrorH1Tag) ?? GetElement(stackTraceTagByLocator) ?? GetElement(resourceNotFoundH2Tag);

                isPageLoaded = pageErrElement.Displayed ? false : true;
                //Console.WriteLine($"##### IsPageLoadedSuccessfully - IsPageLoaded: {isPageLoaded}");

                string logMsg = isPageLoaded ? $"!!! Page Error - {pageErrElement.Text}<br>{pageUrl}" : $"!!! Error at {pageUrl}";
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
                Driver.Navigate().GoToUrl(pageUrl);

                pageTitle = GetPageTitle();

                isLoaded = pageTitle.Contains("ELVIS PMC") ? true : IsPageLoadedSuccessfully();
                logMsg = isLoaded ? $">>> Page Loaded Successfully <<<<br>{pageUrl}" : GetPageErrorLogMsg();

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
            string expectedPageTitle = (checkingLoginPage == false) ? "ELVIS PMC" : "Log in";
            string logMsg = string.Empty;

            try
            {
                WaitForPageReady();
                pageTitle = Driver.Title;
                isLoaded = (pageTitle.Contains(expectedPageTitle)) ? true : IsPageLoadedSuccessfully();
                logMsg = isLoaded ? ">>> Page Loaded Successfully <<<" : GetPageErrorLogMsg();
                LogInfo(logMsg, isLoaded);

                if (!isLoaded)
                {
                    if (continueTestIfPageNotLoaded == true)
                    {
                        LogInfo(">>> Attempting to navigate back to previous page to continue testing <<<");
                        Driver.Navigate().Back();
                        pageTitle = Driver.Title;
                        isLoaded = pageTitle.Contains("ELVIS PMC") ? true : IsPageLoadedSuccessfully();
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

        public bool VerifyTextAreaField(Enum textAreaField, bool shouldFieldBeEmpty = false)
        {
            string text = string.Empty;
            bool isResultExpected = false;
            try
            {
                text = GetText(GetTextAreaFieldByLocator(textAreaField));

                bool isFieldEmpty = string.IsNullOrEmpty(text) ? true : false;
                isResultExpected = shouldFieldBeEmpty.Equals(isFieldEmpty);
                string expected = shouldFieldBeEmpty ? "empty field" : $"retrieved text: {text}";
                string logMsg = isResultExpected ? "" : "not ";
                LogInfo($"Result {logMsg}as expected:<br>{expected}", isResultExpected);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isResultExpected;
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

        public IWebElement ScrollToElement(By elementByLocator)
        {
            IWebElement elem = null;

            try
            {
                elem = GetElement(elementByLocator);

                if (elem.Enabled)
                {
                    //if (!elem.Displayed)
                    //{
                    Actions actions = new Actions(Driver);
                    actions.MoveToElement(elem);
                    actions.Perform();
                    log.Info($"Scrolled to element - {elementByLocator}");
                    //}
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
                //if (element.Enabled)
                //{
                if (!element.Displayed)
                {
                    Actions actions = new Actions(Driver);
                    actions.MoveToElement(element);
                    actions.Perform();
                    log.Info($"Scrolled to WebElement");
                }
                //}
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
    }
}