﻿using Microsoft.Win32;
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
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.StaticHelpers;
using RKCIUIAutomation.Test;
using static RKCIUIAutomation.Page.PageInteraction;

namespace RKCIUIAutomation.Page
{
    public class PageInteraction : PageInteraction_Impl
    {
        readonly By logInLinkLocator = By.XPath("//header//a[contains(text(),'Login')]");

        readonly By logOutLinkLocator = By.XPath("//header//a[contains(text(),'Log out')]");

        public PageInteraction()
        {
        }

        public PageInteraction(IWebDriver driver) => this.Driver = driver;

        public enum JSAction
        {
            [StringValue("arguments[0].click();")] Click,
            [StringValue("var evObj = document.createEvent('MouseEvents');" +
                    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                    "arguments[0].dispatchEvent(evObj);")]
            Hover
        }

        string ActiveModalXpath => "//div[contains(@style,'opacity: 1')]";

        string ModalCloseBtn => $"{ActiveModalXpath}//a[@aria-label='Close']";

        string ModalTitle => $"{ActiveModalXpath}//div[contains(@class,'k-header')]";

        private string GetPageErrorLogMsg() => BaseUtil.GetVar(testEnv);

        private bool IsPageLoadedSuccessfully()
        {
            bool isPageLoaded = true;
            string logMsg = string.Empty;
            string pageUrl = GetPageUrl();
            IWebElement pageErrElement = null;

            try
            {
                By serverErrorH1Tag = By.XPath("//h1[contains(text(),'Server Error')]");
                By resourceNotFoundH2Tag = By.XPath("//h2/i[text()='The resource cannot be found.']");
                By stackTraceTagByLocator = By.XPath("//b[text()='Stack Trace:']");


                pageErrElement = GetElement(serverErrorH1Tag)
                    ?? GetElement(resourceNotFoundH2Tag);

                if ((bool)pageErrElement?.Displayed)
                {
                    isPageLoaded = false;
                    Report.Error(GetText(stackTraceTagByLocator));
                    logMsg = $"!!! Error at {pageUrl}";
                }
                else
                {
                    logMsg = $"!!! Page Error - {pageErrElement.Text}<br>{pageUrl}";
                }

                //logMsgKey = "logMsgKey";
                BaseUtil.CreateVar("logMsgKey", logMsg);
            }
            catch (NoSuchElementException nse)
            {
                log.Debug(nse.Message);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            return isPageLoaded;
        }

        internal void ExecuteJsScript(string jsToBeExecuted)
        {
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            executor.ExecuteScript(jsToBeExecuted);
        }

        internal object ExecuteJsScriptGet(string jsToBeExecuted)
        {
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            return executor.ExecuteScript(jsToBeExecuted);
        }

        public override string AcceptAlertMessage()
        {
            string alertMsg = string.Empty;

            try
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
                alert = driver.SwitchTo().Alert();
                alertMsg = alert.Text;
                alert.Accept();
                Report.Step($"Accepted browser alert: '{alertMsg}'");
            }
            catch (UnhandledAlertException)
            {
                throw;
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            return alertMsg;
        }

        public override void ClearText(By elementByLocator)
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
                throw;
            }
        }

        public override void ClickCancel()
        {
            try
            {
                IWebElement cancelBtnElem = null;
                cancelBtnElem = GetElement(PgHelper.GetButtonByLocator("Cancel"))
                    ?? GetElement(PgHelper.GetInputButtonByLocator("Cancel"))
                    ?? GetElement(By.Id("CancelSubmittal"));

                if (cancelBtnElem != null)
                {
                    cancelBtnElem.Click();
                    Report.Step("Clicked Cancel");
                }

                WaitForPageReady();
            }
            catch (Exception e)
            {
                Report.Error(e.Message);
                throw;
            }
        }

        public override void ClickCreate()
            => ClickElement(By.Id("btnCreate"));

        public override void ClickElement(By elementByLocator)
        {
            IWebElement elem = null;

            try
            {
                elem = ScrollToElement(elementByLocator);
                //elem = GetElement(elementByLocator);
                elem?.Click();

                bool elemNotNull = elem != null
                    ? true
                    : false;

                string logMsg = elemNotNull
                    ? "Clicked"
                    : "Null";

                Report.Step($"{logMsg} element: - {elementByLocator}");
            }
            catch (Exception e)
            {
                Report.Error(e.Message);
                throw;
            }
        }

        public override void ClickElementByID(Enum elementIdEnum)
            => ClickElement(By.Id(elementIdEnum.GetString()));

        public override void ClickInMainBodyAwayFromField()
        {
            try
            {
                WaitForPageReady();
                By mainBodyLocator = By.Id("main");
                ClickElement(mainBodyLocator);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        public override void ClickLoginLink()
            => JsClickElement(logInLinkLocator);

        public override void ClickLogoutLink()
        {
            bool loggedOutSuccessfully = false;

            do
            {
                JsClickElement(logOutLinkLocator);
                loggedOutSuccessfully = ElementIsDisplayed(logInLinkLocator);
            }
            while (!loggedOutSuccessfully);
        }

        public override void ClickNew(bool multipleBtnInstances = false)
        {
            try
            {
                IWebElement newBtnElem = null;

                if (multipleBtnInstances)
                {
                    newBtnElem = GetElement(By.XPath("//div[@class='k-content k-state-active']//a[text()='New']"));
                }
                else
                {
                    newBtnElem = GetElement(PgHelper.GetButtonByLocator("New"))
                        ?? GetElement(PgHelper.GetInputButtonByLocator("Create New"));
                }

                if (newBtnElem != null)
                {
                    newBtnElem.Click();
                    Report.Step("Clicked New");
                }

                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }

        public override void ClickSave()
        {
            try
            {
                (GetElement(By.XPath("//button[text()='Save']"))
                    ?? GetElement(By.Id("SaveSubmittal"))
                    ?? GetElement(By.Id("SaveItem"))
                    ).Click();

                Report.Step("Clicked Save");
                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        public override void ClickSaveForward()
            => ClickElement(By.Id("SaveForwardItem"));

        public override void ClickSubmitForward()
        {
            try
            {
                (GetElement(By.XPath("//button[text()='Submit & Forward']"))
                    ?? GetElement(By.Id("SaveForwardSubmittal"))
                    ?? GetElement(By.Id("SaveForwardItem"))
                    ).Click();

                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        public override void CloseActiveModalWindow()
        {
            JsClickElement(By.XPath(ModalCloseBtn));
            Thread.Sleep(500);
        }

        public override void ConfirmActionDialog(bool confirmYes = true)
        {
            string actionMsg = string.Empty;

            try
            {
                //IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
                WebDriverWait wait = GetStandardWait(driver);
                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                alert = driver.SwitchTo().Alert();

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

                Report.Step($"Confirmation Dialog: {actionMsg}");
            }
            catch (WebDriverTimeoutException)
            {
                log.Debug("Attempted to find Alert Dialog, but was not present.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override string DismissAlertMessage()
        {
            string alertMsg = string.Empty;

            try
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
                alert = driver.SwitchTo().Alert();
                alertMsg = alert.Text;
                alert.Dismiss();
                Report.Step($"Dismissed browser alert: '{alertMsg}'");
            }
            catch (UnhandledAlertException)
            {
                throw;
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            return alertMsg;
        }

        public override bool ElementIsDisplayed(By elementByLocator)
        {
            try
            {
                GetElement(elementByLocator);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public override void EnterSignature()
        {
            IWebElement signCanvas = GetElement(By.Id("mycanvas"), false);

            Actions drawOnCanvas = new Actions(driver);
            drawOnCanvas
                //.Click(signCanvas)
                .MoveToElement(signCanvas, 8, 8)
                .ClickAndHold(signCanvas)
                .MoveByOffset(50, 10)
                .MoveByOffset(-10, -50)
                .MoveByOffset(-50, -10)
                .Release(signCanvas)
                .Build();
            drawOnCanvas.Perform();
        }

        public override void EnterText(By elementByLocator, string text, bool clearField = true)
        {
            IWebElement textField = null;
            string logMsg = string.Empty;

            try
            {
                textField = GetElement(elementByLocator);

                if (clearField)
                {
                    textField.Clear();
                }

                textField.Click();
                textField.SendKeys(text);

                logMsg = $"Entered '{text}' in field - {elementByLocator}";
                Report.Step(logMsg);
            }
            catch (Exception e)
            {
                Report.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        public override void ExecuteJsAction(JSAction jsAction, By elementByLocator)
        {
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            string javaScript = jsAction.GetString();
            IWebElement element = GetElement(elementByLocator);
            executor.ExecuteScript(javaScript, element);
        }

        public override void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName, bool useContainsOperator = false, bool isMultiSelectDDList = false)
        {
            ExpandDDL(ddListID, isMultiSelectDDList);
            ClickElement(PgHelper.GetDDListItemsByLocator(ddListID, itemIndexOrName, useContainsOperator));
        }

        public override void ExpandDDL<E>(E ddListID, bool isMultiSelectDDList = false)
        {
            By locator = PgHelper.GetExpandDDListButtonByLocator(ddListID, isMultiSelectDDList);

            try
            {
                if (isMultiSelectDDList)
                {
                    ClickElement(locator);
                }
                else
                {
                    JsClickElement(locator);
                }

                Thread.Sleep(1000);
                Report.Step($"Expanded DDList - {ddListID.ToString()}");
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
        }

        public override string GetAttribute(By elementByLocator, string attributeName)
            => GetElement(elementByLocator)?.GetAttribute(attributeName);

        public override IList<string> GetAttributes<T>(T elementByLocator, string attributeName)
        {
            object argObject = null;
            Type argType = elementByLocator.GetType();
            IList<string> attributes = new List<string>();
            IList<IWebElement> elements = new List<IWebElement>();

            try
            {
                if (argType.Equals(typeof(By)))
                {
                    argObject = BaseUtil.ConvertToType<By>(elementByLocator);
                    elements = GetElements((By)argObject);
                }
                else if (argType.Equals(typeof(List<By>)))
                {
                    argObject = BaseUtil.ConvertToType<List<By>>(elementByLocator);

                    foreach (By locator in (List<By>)argObject)
                    {
                        IList<IWebElement> tmpElements = GetElements(locator);

                        foreach (IWebElement tmpElem in tmpElements)
                        {
                            elements.Add(tmpElem);
                        }
                    }
                }
                else
                {
                    log.Error($"Unsupported argument type {argType}");
                }

                foreach (IWebElement elem in elements)
                {
                    string attrib = elem.GetAttribute(attributeName);
                    attributes.Add(attrib);
                }
            }
            catch (NoSuchElementException e)
            {
                log.Debug(e.Message);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            return attributes;
        }

        public override string GetCurrentUser(bool getFullName = false)
        {
            string userAcct = string.Empty;
            string splitPattern = string.Empty;

            By locator = By.XPath("//a[@href='/Project/Account/']");

            try
            {
                userAcct = GetText(locator);
                bool acctNameContainsX = userAcct.Contains("X");

                if (userAcct.Contains("Test"))
                {
                    splitPattern = getFullName
                        ? "Welcome "
                        : "Welcome Test ";

                    if (acctNameContainsX)
                    {
                        userAcct = Regex.Split((Regex.Split(userAcct, splitPattern)[1]), " X")[0];
                    }
                    else
                    {
                        userAcct = Regex.Split(userAcct, "Welcome Test ")[1];
                    }
                }
                else
                {
                    if (acctNameContainsX)
                    {
                        userAcct = Regex.Split((Regex.Split(userAcct, "Welcome ")[1]), " X")[0];
                    }
                    else
                    {
                        userAcct = Regex.Split(userAcct, "Welcome ")[1];
                    }
                }

                log.Info($"Getting current user: {userAcct}");
            }
            catch (Exception e)
            {
                Report.Error(e.Message);
                throw;
            }

            return userAcct;
        }

        public override IWebElement GetElement(By elementByLocator, bool waitForLoading = true)
        {
            IWebElement elem = null;

            try
            {
                elem = driver.FindElement(elementByLocator);
            }
            catch (NoSuchElementException)
            {
                WaitForElement(elementByLocator, waitForLoading: waitForLoading);
                elem = driver.FindElement(elementByLocator);
            }
            catch (Exception)
            {
                throw;
            }

            return elem;
        }

        public override IList<IWebElement> GetElements(By elementByLocator, bool waitForLoading = true)
        {
            IList<IWebElement> elements = new List<IWebElement>();

            try
            {
                elements = driver.FindElements(elementByLocator);
            }
            catch (NoSuchElementException)
            {
                WaitForElement(elementByLocator, waitForLoading: waitForLoading);
                elements = driver.FindElements(elementByLocator);
            }
            catch (Exception)
            {
                throw;
            }

            return elements;
        }

        public override int GetElementsCount(By elementByLocator)
            => GetElements(elementByLocator).Count;

        public override string GetPageTitle(int timeOutInSeconds = 10, int pollingInterval = 500)
        {
            try
            {
                pageTitle = string.Empty;
                WaitForPageReady();

                log.Debug($"...waiting for page title");
                WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
                wait.Until(x => x.Title.HasValue());
                pageTitle = driver.Title;
                //LogInfo($"...Page Title displayed as : {pageTitle}", pageTitle.HasValue());
            }
            catch (Exception e)
            {
                log.Error($"timed out while waiting for page title\n{e.Message}");
            }

            return pageTitle;
        }

        public override string GetPageUrl(int timeOutInSeconds = 10, int pollingInterval = 500)
        {
            string pageUrl = string.Empty;

            try
            {
                log.Debug($"...waiting for page title");
                WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
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

        public override WebDriverWait GetStandardWait(IWebDriver driver, int timeOutInSeconds = 10, int pollingInterval = 500)
        {
            WebDriverWait wait = null;

            try
            {
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeOutInSeconds))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                wait.IgnoreExceptionTypes(typeof(ElementClickInterceptedException));
                wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
                wait.IgnoreExceptionTypes(typeof(ElementNotSelectableException));
            }
            catch (UnhandledAlertException ae)
            {
                log.Debug(ae.Message);
            }
            catch (Exception e)
            {
                log.Error($"Error occured in method GetStandardWait : {e.Message}");
                throw;
            }

            return wait;
        }

        public override string GetText(By elementByLocator, bool shouldReturnValue = true, bool logReport = true)
        {
            bool textHasValue = false;
            bool resultIsAsExpected = false;
            IWebElement element = null;
            string text = string.Empty;
            string logMsg = $"Unable to retrieve text";

            try
            {
                element = ScrollToElement(elementByLocator);

                if ((bool)element?.Displayed || (bool)element?.Enabled)
                {
                    text = element.Text;
                    textHasValue = text.HasValue();

                    if (!textHasValue)
                    {
                        text = element.GetAttribute("value");
                        textHasValue = text.HasValue();
                    }

                    if (textHasValue)
                    {
                        logMsg = $"Retrieved '{text}'";

                        if (shouldReturnValue)
                        {
                            logMsg = $"{logMsg} [As Expected]";
                            resultIsAsExpected = true;
                        }
                        else
                        {
                            logMsg = $"Unexpectedly, {logMsg}";
                        }
                    }
                    else
                    {
                        if (!shouldReturnValue)
                        {
                            logMsg = $"{logMsg} [As Expected]";
                            resultIsAsExpected = true;
                        }
                        else
                        {
                            logMsg = $"Unexpectedly, {logMsg}";
                        }
                    }

                    if (logReport)
                    {
                        Report.Info($"{logMsg} from element - {elementByLocator}", resultIsAsExpected);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return text;
        }

        public override IList<string> GetTextForElements(By elementByLocator)
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

        public override string GetTextFromDDL<T>(T ddListID, bool isMultiSelectDDList = false)
        {
            string textFromDDList = string.Empty;

            try
            {
                if (isMultiSelectDDList)
                {
                    textFromDDList = string.Join("::", PageAction.GetTextFromMultiSelectDDL(ddListID).ToArray());
                }
                else
                {
                    textFromDDList = GetText(PgHelper.GetDDListCurrentSelectionByLocator(ddListID));
                }
            }
            catch (NoSuchElementException)
            {
                throw;
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            return textFromDDList;
        }

        public override string GetTextFromDDListInActiveTab(Enum ddListID)
            => GetText(PgHelper.GetDDListCurrentSelectionInActiveTabByLocator(ddListID));

        public override IList<string> GetTextFromMultiSelectDDL<T>(T multiSelectDDListID)
            => GetTextForElements(PgHelper.GetMultiSelectDDListCurrentSelectionByLocator(multiSelectDDListID));

        public override string GetUserDownloadFolderPath()
            => Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", string.Empty).ToString();

        public override void JsClickElement(By elementByLocator)
        {
            try
            {
                ScrollToElement(elementByLocator);
                ExecuteJsAction(JSAction.Click, elementByLocator);
                Report.Step($"Clicked {elementByLocator}");
            }
            finally
            {
                WaitForPageReady(waitForLoading: false);
            }
        }

        public override string JsGetPageTitle(string windowHandle = "")
        {
            string title = string.Empty;

            try
            {
                if (!windowHandle.HasValue())
                {
                    driver.SwitchTo().Window(windowHandle);
                }
                IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
                title = (string)executor.ExecuteScript("return document.title");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return title;
        }

        public override void JsHover(By elementByLocator)
        {
            ExecuteJsAction(JSAction.Hover, elementByLocator);
            Thread.Sleep(1000);
        }

        public override void LoginAs(UserType user)
            => LoginPage.LoginUser(user);

        public override void LogoutToLoginPage()
        {
            Thread.Sleep(3000);
            WaitForPageReady();
            ClickLogoutLink();
            WaitForPageReady();
            ClickLoginLink();
        }

        public override void RefreshWebPage()
        {
            try
            {
                driver.Navigate().Refresh();
                log.Info("Refreshed Web Page");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        public override void ScrollPageToBottom()
            => ScrollToElement(By.Id("footer"));

        public override void ScrollPageToTop()
            => ScrollToElement(By.Id("pageContent"));

        public override void ScrollCommentTabInToView()
            => ScrollToElement(By.Id("tabComments_ts_active"));

        public override IWebElement ScrollToElement<T>(T elementOrLocator)
        {
            Type argType = elementOrLocator.GetType();
            IWebElement elem = null;

            try
            {
                if (argType == typeof(By))
                {
                    By locator = ConvertToType<By>(elementOrLocator);
                    elem = GetElement(locator);
                }
                else if (argType == typeof(IWebElement))
                {
                    elem = ConvertToType<IWebElement>(elementOrLocator);
                }
                else if (argType == typeof(string))
                {
                    string locatorString = ConvertToType<string>(elementOrLocator);
                    elem = GetElement(By.Id(locatorString)) ?? GetElement(By.XPath($"//{locatorString}"));
                }

                if (elem != null)
                {
                    WaitForPageReady(waitForLoading:false);
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(elem);
                    actions.Perform();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return elem;
        }

        public override void SelectRadioBtnOrChkbox<T>(T chkbxOrRadioBtnID, bool toggleChkBoxIfAlreadyChecked = true)
        {
            string chkbxOrRdoBtnIdValue = string.Empty;
            string chkbxOrRdoBtnNameAndId = string.Empty;

            if (chkbxOrRadioBtnID is Enum)
            {
                chkbxOrRdoBtnIdValue = ConvertToType<Enum>(chkbxOrRadioBtnID).GetString();
                chkbxOrRdoBtnNameAndId = $"{chkbxOrRadioBtnID} (ID: {chkbxOrRdoBtnIdValue})";
            }
            else if (chkbxOrRadioBtnID.GetType().Equals(typeof(string)))
            {
                chkbxOrRdoBtnIdValue = ConvertToType<string>(chkbxOrRadioBtnID);
                chkbxOrRdoBtnNameAndId = $"ID: {chkbxOrRdoBtnIdValue}";
            }

            By locator = By.Id(chkbxOrRdoBtnIdValue);

            try
            {
                IWebElement element = GetElement(locator, false);

                try
                {
                    if (element.Enabled)
                    {
                        if (toggleChkBoxIfAlreadyChecked)
                        {
                            JsClickElement(locator);
                            Report.Step($"Selected: {chkbxOrRdoBtnNameAndId}");
                        }
                        else
                        {
                            log.Info("Specified not to toggle checkbox, if already selected");

                            if (!element.Selected)
                            {
                                JsClickElement(locator);
                                Report.Info($"Selected: {chkbxOrRdoBtnNameAndId}");
                            }
                            else
                            {
                                Report.Info($"Did not select element, because it is already selected: {chkbxOrRdoBtnNameAndId}");
                            }
                        }
                    }
                }
                catch (ElementNotInteractableException)
                {
                    Report.Error($"Element {chkbxOrRadioBtnID}, is not selectable", true);
                }
            }
            catch (UnhandledAlertException e)
            {
                log.Debug(e.Message);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }
        }

        public override string UploadFile(string fileName = "")
        {
            string filePath = string.Empty;

            if (!fileName.HasValue())
            {
                fileName = "test.xlsx";
            }

            if (testPlatform == TestPlatformType.Local)
            {
                filePath = $"{CodeBasePath}\\UploadFiles\\{fileName}";                
            }
            else
            {
                filePath = $"/home/seluser/UploadFiles/{fileName}";
            }

            try
            {
                PageAction.WaitForPageReady();
                ScrollToElement(By.XPath("//div[contains(@class, 'k-upload k-header')]"));
                driver.FindElement(By.XPath("//input[@id='UploadFiles_0_']")).SendKeys(filePath);
                Report.Step($"Entered {filePath}' for file upload");
                PageAction.WaitForPageReady();
            }
            catch (Exception e)
            {
                Report.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
            finally
            {
                By uploadStatusLabel = By.XPath("//strong[@class='k-upload-status k-upload-status-total']");
                bool uploadStatus = ElementIsDisplayed(uploadStatusLabel);

                Report.Info($"File Upload {(uploadStatus ? "Successful" : "Failed")}.", uploadStatus);
            }

            return fileName;
        }

        public override bool VerifyActiveModalTitle(string expectedModalTitle)
        {
            string actualTitle = GetText(By.XPath(ModalTitle));
            bool titlesMatch = actualTitle.Contains(expectedModalTitle) ? true : false;
            Report.Info($"## Expected Modal Title: {expectedModalTitle}<br>## Actual Modal Title: {actualTitle}", titlesMatch);
            return titlesMatch;
        }

        public override bool VerifyAndAcceptAlertMessage(string expectedMessage)
        {
            bool msgMatch = false;

            try
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
                string actualAlertMsg = driver.SwitchTo().Alert().Text;
                msgMatch = (actualAlertMsg).Contains(expectedMessage)
                    ? true
                    : false;

                Report.Info($"## Expected Alert Message: {expectedMessage}<br>## Actual Alert Message: {actualAlertMsg}", msgMatch);

                AcceptAlertMessage();
            }
            catch (UnhandledAlertException)
            {
                throw;
            }
            catch (Exception e)
            {
                log.Debug($"{e.Message}\n{e.StackTrace}");
            }

            return msgMatch;
        }

        public override bool VerifyChkBoxRdoBtnSelection(Enum rdoBtnOrChkBox, bool shouldBeSelected = true)
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

                Report.Info($"{rdoBtnOrChkBox.ToString()} :<br>Selection Expected: {shouldBeSelected}<br>Selection Actual: {isSelected}", isResultExpected);

                return isResultExpected;
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                return false;
            }
        }

        public override bool VerifyDDListSelectedValue(Enum ddListId, string expectedDDListValue)
        {
            bool meetsExpectation = false;

            try
            {
                string currentDDListValue = GetTextFromDDL(ddListId);
                meetsExpectation = currentDDListValue.Equals(expectedDDListValue) ? true : false;

                Report.Info($"Expected drop-down field value: {expectedDDListValue}" +
                    $"<br>Actual drop-down field value: {currentDDListValue}", meetsExpectation);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return meetsExpectation;
        }

        public override bool VerifyExpectedList(IList<string> actualList, IList<string> expectedList, string verificationMethodName = "")
        {
            verificationMethodName = verificationMethodName.HasValue()
                ? verificationMethodName.SplitCamelCase()
                : "Verify Expected List";

            int expectedCount = 0;
            int actualCount = 0;
            bool countsMatch = false;
            bool fieldsMatch = false;

            try
            {
                IList<bool> results = new List<bool>();
                expectedCount = expectedList.Count;
                actualCount = actualList.Count;
                countsMatch = expectedCount == actualCount;

                string logMsg = countsMatch
                    ? ""
                    : "NOT ";

                Report.Info($"Expected and Actual Required Fields count are {logMsg}equal.<br>Actual Count: {actualCount}<br>Expected Count: {expectedCount}", countsMatch);

                int logTblRowIndex = 0;
                string[][] logTable = new string[expectedCount + 2][];
                logTable[logTblRowIndex] = new string[2] { $"{verificationMethodName}<br>|  Expected  | ", $"<br> |  Found Matching Actual  | " };

                for (int i = 0; i < expectedCount; i++)
                {
                    bool actualHasValue = false;
                    bool expectedHasValue = false;
                    string expected = string.Empty;
                    string expectedLabel = string.Empty;
                    string actualValueLogMsg = string.Empty;

                    logTblRowIndex++;
                    expected = expectedList[i];

                    expectedHasValue = expected.HasValue();

                    if (expectedHasValue && expected.Contains("::"))
                    {
                        string[] splitExpected = Regex.Split(expected, "::");
                        expectedLabel = $"{splitExpected[0]}<br>";
                        expected = splitExpected[1];
                    }

                    IList<bool> compareResults = new List<bool>();

                    bool actualAndExpectedMatch = false;
                    string tblLogMsg = $"Expected Value : {expected}";
                    string failedDefaultMsg = "";

                    foreach (string actual in actualList)
                    {
                        actualHasValue = actual.HasValue();
                        actualAndExpectedMatch = expectedHasValue //if Expected has value
                            ? actualHasValue //if Expected has value, check if Actual has value
                                ? actual.Contains(expected) || expected.Contains(actual) || actual.Equals(expected)
                                    ? true //if Expected and Actual have values and actual/expected contain or equal one another
                                    : false //if Expected and Actual have values and actual/expected DO NOT contain or equal one another
                                : false //if Expected has value and Actual DO NOT have value
                            : actualHasValue //if Expected DO NOT have value, check if Actual has value
                                ? false //if Expected DO NOT have value and Actual has value
                                : true; //if Expected DO NOT have value and Actual DO NOT have value

                        compareResults.Add(actualAndExpectedMatch);

                        if (actualAndExpectedMatch)
                        {
                            break;
                        }
                    }

                    fieldsMatch = compareResults.Contains(true)
                        ? true
                        : false;

                    tblLogMsg = actualAndExpectedMatch
                        ? tblLogMsg
                        : actualHasValue
                            ? $"{tblLogMsg}<br>Could not find matching actual value."
                            : $"{tblLogMsg}<br>Actual Value is Empty!";

                    if (actualHasValue.Equals(false) && expectedHasValue.Equals(false))
                    {
                        tblLogMsg = "Both Expected and Actual values are empty!";
                        fieldsMatch = false;
                    }

                    results.Add(fieldsMatch);

                    string logTblRowNumber = logTblRowIndex.ToString();
                    logTblRowNumber = logTblRowNumber.Length == 1
                        ? $"0{logTblRowNumber}"
                        : logTblRowNumber;

                    logTable[logTblRowIndex] = new string[2] { $"{logTblRowNumber}: {expectedLabel}{tblLogMsg}{failedDefaultMsg}", $"{fieldsMatch.ToString()}" };
                    failedDefaultMsg = "";
                }

                fieldsMatch = results.Contains(false)
                    ? false
                    : true;

                logTable[logTblRowIndex + 1] = new string[2] { "Total Required Fields:", (results.Count).ToString() };
                Report.Info(logTable, fieldsMatch);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return fieldsMatch;
        }

        public override bool VerifyFieldErrorIsDisplayed(By elementByLocator)
        {
            IWebElement elem = GetElement(elementByLocator);
            bool isDisplayed = elem.Displayed;
            string not = !isDisplayed ? " not" : "";
            Report.Info($"Field error is{not} displayed for - {elementByLocator}", isDisplayed);
            return isDisplayed;
        }

        public override bool VerifyInputField(Enum inputField, bool shouldFieldBeEmpty = false)
        {
            string text = string.Empty;
            bool isResultExpected = false;
            try
            {
                text = GetAttribute(By.XPath($"//input[@id='{inputField.GetString()}']"), "value");

                bool isFieldEmpty = string.IsNullOrEmpty(text) ? true : false;
                string logMsg = isFieldEmpty ? "Empty Field: Unable to retrieve text" : $"Retrieved '{text}'";

                isResultExpected = shouldFieldBeEmpty.Equals(isFieldEmpty);
                Report.Info($"{logMsg} from field - {inputField.ToString()}", isResultExpected);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return isResultExpected;
        }

        public override bool VerifyPageHeader(string expectedPageHeading)
        {
            bool pageHeadingsMatch = false;
            bool isDisplayed = false;
            string actualHeading = string.Empty;
            string logMsg = string.Empty;
            IWebElement headingElem = null;

            try
            {
                pageTitle = GetPageTitle();

                if (pageTitle.Contains("ELVIS PMC"))
                {
                    if (!pageTitle.Contains("Home Page"))
                    {
                        try
                        {
                            headingElem = driver.FindElement(By.XPath("//h3"))
                                ?? driver.FindElement(By.XPath("//h2"))
                                ?? driver.FindElement(By.XPath("//h4"));
                        }
                        catch (NoSuchElementException nse)
                        {
                            log.Debug(nse.Message);
                        }

                        if ((bool)headingElem?.Displayed)
                        {
                            isDisplayed = true;
                        }

                        if (isDisplayed)
                        {
                            actualHeading = headingElem.Text;
                            pageHeadingsMatch = actualHeading.Equals(expectedPageHeading);

                            logMsg = pageHeadingsMatch
                                ? $"## Page Heading is as expected: {actualHeading}"
                                : $"Page Headings did not match: Expected: {expectedPageHeading}, Actual: {actualHeading}";

                            if (!pageHeadingsMatch)
                            {
                                InjectTestStatus(TestStatus.Failed, logMsg);
                            }
                        }
                        else
                        {
                            logMsg = $"Could not find page heading with h2, h3 or h4 tag";
                            InjectTestStatus(TestStatus.Failed, logMsg);
                        }

                        Report.Info(logMsg, pageHeadingsMatch);
                    }
                }
                else
                {
                    VerifyPageIsLoaded(false, false);
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            return pageHeadingsMatch;
        }

        public override void VerifyPageIsLoaded(bool checkingLoginPage = false, bool continueTestIfPageNotLoaded = true)
        {
            bool isLoaded = false;
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
                    ? $">>> Page Loaded Successfully <<< <br>Page Title : {pageTitle}"
                    : GetPageErrorLogMsg();

                Report.Info(logMsg, isLoaded);

                if (!isLoaded)
                {
                    if (continueTestIfPageNotLoaded == true)
                    {
                        Report.Info(">>> Attempting to navigate back to previous page to continue testing <<<");
                        driver.Navigate().Back();
                        WaitForPageReady();
                        pageTitle = GetPageTitle();
                        isLoaded = pageTitle.Contains("ELVIS PMC")
                            ? true
                            : IsPageLoadedSuccessfully();

                        if (isLoaded)
                        {
                            Report.Info(">>> Navigated to previous page successfully <<<");
                        }
                        else
                        {
                            Report.Info($"!!! Page did not load properly, when navigating to the previous page !!!<br>{logMsg}");
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        Assert.Fail();
                    }

                    InjectTestStatus(TestStatus.Failed, logMsg);
                }
            }
            catch (Exception e)
            {
                log.Error($"Error in VerifyPageIsLoaded() : {e.StackTrace}");
                throw;
            }
        }

        public override bool VerifyRequiredFieldErrorLabelIsDisplayed<T>(T fieldInputIdEnumOrString)
        {
            Type argType = fieldInputIdEnumOrString.GetType();
            string inputFieldId = string.Empty;
            string logMsg = string.Empty;
            bool isDisplayed = false;

            if (fieldInputIdEnumOrString is Enum)
            {
                inputFieldId = ConvertToType<Enum>(fieldInputIdEnumOrString).GetString();
            }
            else if (argType.Equals(typeof(string)))
            {
                inputFieldId = ConvertToType<string>(fieldInputIdEnumOrString);
            }
            else
            {
                throw new ArgumentException($"Unsupported argument type was provided [{argType}].\nSupported argument types are type of string OR Enum (with StringValueAttribute) of the input field ID.");
            }

            try
            {
                PageAction.GetElement(By.XPath($"//input[@id='{inputFieldId}']/preceding-sibling::span[text()='Required']"));
                isDisplayed = true;
            }
            catch (NoSuchElementException)
            {
                logMsg = "not ";
            }
            catch (Exception)
            {
                throw;
            }

            Report.Info($"Required field error label is {logMsg}displayed.", isDisplayed);

            return isDisplayed;
        }

        public override bool VerifySchedulerIsDisplayed() //TODO - move to Early Break Calendar class when more test cases are created
        {
            IWebElement scheduler = GetElement(By.Id("scheduler"));
            bool isDisplayed = scheduler.Displayed;
            string not = isDisplayed == false ? " not" : "";
            Report.Info($"Scheduler is{not} displayed", isDisplayed);
            return isDisplayed;
        }

        public override bool VerifySuccessMessageIsDisplayed()
        {
            By elementByLocator = By.XPath("//div[contains(@class,'bootstrap-growl')]");
            IWebElement msg = GetElement(elementByLocator);
            bool isDisplayed = msg.Displayed;
            string not = !isDisplayed ? " not" : "";
            Report.Info($"Success Message is{not} displayed", isDisplayed);
            return isDisplayed;
        }

        public override bool VerifyTextAreaField(Enum textAreaField, bool textFieldShouldHaveValue = true)
        {

            bool textAreaHasValue = false;
            bool resultIsAsExpected = false;
            string logMsg = string.Empty;
            string expectedMsg = string.Empty;
            string textAreaValue = string.Empty;

            try
            {
                textAreaValue = GetText(PgHelper.GetTextAreaFieldByLocator(textAreaField), textFieldShouldHaveValue);
                textAreaHasValue = textAreaValue.HasValue();
                string logMsgTextArea = $"textArea [{textAreaField}]";

                if (textAreaHasValue)
                {
                    if (textFieldShouldHaveValue)
                    {
                        resultIsAsExpected = true;
                        expectedMsg = $"retrieved text from {logMsgTextArea} : {textAreaValue}";                       
                    }
                    else
                    {
                        logMsg = "NOT ";
                        expectedMsg = $"{logMsgTextArea} should be empty, but retrieved text: {textAreaValue}";
                    }
                }
                else
                {
                    if (textFieldShouldHaveValue)
                    {
                        logMsg = "NOT ";
                        expectedMsg = $"{logMsgTextArea} should have value, but is empty";
                    }
                    else
                    {
                        resultIsAsExpected = true;
                        expectedMsg = $"{logMsgTextArea} is empty [As Expected]";
                    }
                }

                Report.Info($"Result is {logMsg}as expected:<br>{expectedMsg}", resultIsAsExpected);
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            return resultIsAsExpected;
        }

        public override bool VerifyUploadedFileNames<T>(T expectedFileName, bool beforeSubmitBtnAction = false, bool forDIR = true, int dirEntryNumber = 1)
        {
            int actualCount = 0;
            bool fileNamesMatch = false;
            string logMsg = string.Empty;
            string actualName = string.Empty;
            string expectedName = string.Empty;
            string actualFileNameLocatorXPath = string.Empty;
            IList<bool> assertList = null;
            IList<string> expectedNames = null;
            IList<IWebElement> actualFileNameList = null;

            Type argType = expectedFileName.GetType();

            try
            {
                By testListDIV = By.XPath("//h5/b[contains(text(),'Test List')]");
                ScrollToElement(testListDIV);

                if (argType == typeof(string) || argType == typeof(List<string>))
                {
                    bool uploadedFileExpected = false;

                    if (argType == typeof(string))
                    {
                        expectedName = BaseUtil.ConvertToType<string>(expectedFileName);
                        uploadedFileExpected = expectedName.HasValue();
                    }
                    else if (argType == typeof(List<string>))
                    {
                        expectedNames = new List<string>();
                        expectedNames = BaseUtil.ConvertToType<IList<string>>(expectedFileName);
                        uploadedFileExpected = expectedNames != null;
                    }

                    bool fileNameIsAsExpected = false;
                    assertList = new List<bool>();
                    actualFileNameList = new List<IWebElement>();

                    string entryNumberXPath = string.Empty;

                    if (forDIR)
                    {
                        entryNumberXPath = beforeSubmitBtnAction
                            ? $"//div[@id='FileManagerDiv_{dirEntryNumber - 1}']"
                            : $"//div[@id='FilesListDiv_{dirEntryNumber - 1}']";
                    }

                    actualFileNameLocatorXPath = beforeSubmitBtnAction
                        ? "//ul[@class='k-upload-files k-reset']/li/div/span[@class='k-file-name']"
                        : "//div[contains(@class,'fileList')]//div[contains(@class,'file-name-and-size')]";

                    By actualFileNameLocator = By.XPath($"{entryNumberXPath}{actualFileNameLocatorXPath}");

                    if (uploadedFileExpected)
                    {
                        actualFileNameList = GetElements(actualFileNameLocator);

                        if (actualFileNameList.Any())
                        {
                            actualCount = actualFileNameList.Count;
                        }

                        if (actualCount > 0)
                        {
                            foreach (IWebElement actualNameElem in actualFileNameList)
                            {
                                actualName = actualNameElem.Text.Trim();

                                Report.Step($"Found file name : {actualName}");

                                fileNameIsAsExpected = argType == typeof(string)
                                    ? actualName.Contains(expectedName)
                                    : expectedNames.Contains(actualName);

                                string actualExpected = fileNameIsAsExpected
                                    ? ""
                                    : "DID NOT ";

                                logMsg = argType == typeof(string)
                                    ? $"Expected and Uploaded file names {actualExpected}match<br>EXPECTED: {expectedName}<br>ACTUAL: {actualName}"
                                    : $"Expected File Names List {actualExpected}contain {actualName}";

                                assertList.Add(fileNameIsAsExpected);
                                Report.Info($"{logMsg}", fileNameIsAsExpected);
                            }
                        }
                        else
                        {
                            assertList.Add(false);
                            Report.Info($"Expected uploaded file(s), but no uploaded file names are seen on the page", fileNameIsAsExpected);
                        }
                    }
                    else
                    {
                        bool result = false;

                        try
                        {
                            result = GetElements(actualFileNameLocator).Any() == false;
                            logMsg = result
                                ? ""
                                : " but found uploaded file(s)";
                        }
                        catch (Exception)
                        {
                            log.Debug($"Checking for unexpected uploaded file names");
                        }

                        assertList.Add(result);
                        Report.Info($"No uploaded file(s) are expected on the page{logMsg}", result);
                    }
                }
                else
                {
                    assertList.Add(false);
                    Report.Error($"Arg type should be string or IList<string> - Unexpected expectedFileName type: {argType}");
                }
            }
            catch (Exception e)
            {
                log.Error($"Exception occured at VerifyUploadedFileNames : {e.StackTrace}");
            }
            finally
            {
                if (expectedName.HasValue() || expectedNames != null)
                {
                    bool countsMatch = false;

                    if (argType == typeof(List<string>))
                    {
                        countsMatch = expectedNames.Count.Equals(actualCount)
                            ? true
                            : false;

                        assertList.Add(countsMatch);
                    }
                    else if (argType == typeof(string))
                    {
                        countsMatch = actualCount.Equals(1)
                            ? true
                            : false;
                    }

                    assertList.Add(countsMatch);
                }

                fileNamesMatch = assertList.Contains(false)
                    ? false
                    : true;
            }

            return fileNamesMatch;
        }

        public override bool VerifyUrlIsLoaded(string pageUrl)
        {
            bool isLoaded = false;
            string logMsg = string.Empty;

            try
            {
                driver.Navigate().GoToUrl(pageUrl);
                WaitForPageReady();
                pageTitle = GetPageTitle();
                isLoaded = pageTitle.Contains("ELVIS PMC")
                    ? true
                    : IsPageLoadedSuccessfully();

                logMsg = isLoaded
                    ? $">>> Page Loaded Successfully <<< <br>{pageUrl}"
                    : GetPageErrorLogMsg();

                Report.Info(logMsg, isLoaded);

                TestUtils.WriteToFile(logMsg, "_PageTitle.txt");
            }
            catch (Exception e)
            {
                log.Error($"Error in VerifyUrlIsLoaded() : {e.StackTrace}");
            }

            return isLoaded;
        }

        public override void WaitForElement(By elementByLocator, int timeOutInSeconds = 20, int pollingInterval = 500, bool waitForLoading = true)
        {
            try
            {
                WaitForPageReady(waitForLoading: waitForLoading);
                WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
                wait.Until(x => driver.FindElement(elementByLocator));
                log.Debug($"...waiting for element: - {elementByLocator}");
            }
            catch (WebDriverTimeoutException)
            {
                throw new NoSuchElementException();
            }
        }

        

        public override void WaitForElementToClear(By elementByLocator)
        {
            bool isDisplayed = false;

            do
            {
                try
                {
                    driver.FindElement(elementByLocator);
                    isDisplayed = true;
                }
                catch (NoSuchElementException)
                {
                    isDisplayed = false;
                }
                catch (Exception )
                {
                    throw;
                }

            } while (isDisplayed);
        }

        public override void WaitForLoading()
        {
            try
            {
                IList<By> loadingElems = new List<By>()
                {
                    By.ClassName("k-loading-image"),
                    By.XPath("//div[@id='overlay_div'][@style='display: block;']")
                };

                foreach (By elem in loadingElems)
                {
                    WaitForElementToClear(elem);
                }
            }
            catch (UnhandledAlertException e)
            {
                Report.Debug($"Alert Message Displayed: {e.Message}");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public override void WaitForPageReady(int timeOutInSeconds = 60, int pollingInterval = 10000, bool waitForLoading = true)
        {
            if (waitForLoading)
            {
                WaitForLoading();
            }

            IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;
            bool pageIsReady = false;

            try
            {
                try
                {
                    if ((bool)javaScriptExecutor.ExecuteScript("return window.jQuery != undefined && jQuery.active === 0"))
                    {
                        pageIsReady = true;
                    }
                }
                catch (InvalidOperationException)
                {
                    if((bool)javaScriptExecutor.ExecuteScript("return document.readyState == 'complete'"))
                    {
                        pageIsReady = true;
                    }
                }

                if (!pageIsReady)
                {
                    log.Debug("...waiting for page to be in Ready state");

                    //try
                    //{
                        WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
                        wait.Until(x => (bool)javaScriptExecutor.ExecuteScript("return document.readyState == 'complete'"));
                    //}
                    //catch (UnhandledAlertException)
                    //{
                    //    throw;
                    //}
                }
            }
            catch (InvalidOperationException)
            {
                //try
                //{
                    WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
                    wait.Until(x => (bool)javaScriptExecutor.ExecuteScript("return window.jQuery != undefined && jQuery.active === 0"));
                //}
                //catch (UnhandledAlertException)
                //{
                //    throw;
                //}
            }
            //catch (UnhandledAlertException ae)
            //{
            //    log.Debug(ae.Message);
            //}
            catch (Exception)
            {
                throw;
            }
        }
    }

    public abstract class PageInteraction_Impl : TenantProperties, IPageInteraction
    {
        public abstract string AcceptAlertMessage();
        public abstract void ClearText(By elementByLocator);
        public abstract void ClickCancel();
        public abstract void ClickCreate();
        public abstract void ClickElement(By elementByLocator);
        public abstract void ClickElementByID(Enum elementIdEnum);
        public abstract void ClickInMainBodyAwayFromField();
        public abstract void ClickLoginLink();
        public abstract void ClickLogoutLink();
        public abstract void ClickNew(bool multipleBtnInstances = false);
        public abstract void ClickSave();
        public abstract void ClickSaveForward();
        public abstract void ClickSubmitForward();
        public abstract void CloseActiveModalWindow();
        public abstract void ConfirmActionDialog(bool confirmYes = true);
        public abstract string DismissAlertMessage();
        public abstract bool ElementIsDisplayed(By elementByLocator);
        public abstract void EnterSignature();
        public abstract void EnterText(By elementByLocator, string text, bool clearField = true);
        public abstract void ExecuteJsAction(JSAction jsAction, By elementByLocator);
        public abstract void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName, bool useContainsOperator = false, bool isMultiSelectDDList = false);
        public abstract void ExpandDDL<E>(E ddListID, bool isMultiSelectDDList = false);
        public abstract string GetAttribute(By elementByLocator, string attributeName);
        public abstract IList<string> GetAttributes<T>(T elementByLocator, string attributeName);
        public abstract string GetCurrentUser(bool getFullName = false);
        public abstract IWebElement GetElement(By elementByLocator, bool waitForLoading = true);
        public abstract IList<IWebElement> GetElements(By elementByLocator, bool waitForLoading = true);
        public abstract int GetElementsCount(By elementByLocator);
        public abstract string GetPageTitle(int timeOutInSeconds = 10, int pollingInterval = 500);
        public abstract string GetPageUrl(int timeOutInSeconds = 10, int pollingInterval = 500);
        public abstract WebDriverWait GetStandardWait(IWebDriver driver, int timeOutInSeconds = 10, int pollingInterval = 500);
        public abstract string GetText(By elementByLocator, bool shouldReturnValue = true, bool logReport = true);
        public abstract IList<string> GetTextForElements(By elementByLocator);
        public abstract string GetTextFromDDL<T>(T ddListID, bool isMultiSelectDDList = false);
        public abstract string GetTextFromDDListInActiveTab(Enum ddListID);
        public abstract IList<string> GetTextFromMultiSelectDDL<T>(T multiSelectDDListID);
        public abstract string GetUserDownloadFolderPath();
        public abstract void JsClickElement(By elementByLocator);
        public abstract string JsGetPageTitle(string windowHandle = "");
        public abstract void JsHover(By elementByLocator);
        public abstract void LoginAs(UserType user);
        public abstract void LogoutToLoginPage();
        public abstract void RefreshWebPage();
        public abstract void ScrollCommentTabInToView();
        public abstract IWebElement ScrollToElement<T>(T elementOrLocator);
        public abstract void ScrollPageToBottom();
        public abstract void ScrollPageToTop();
        public abstract void SelectRadioBtnOrChkbox<T>(T chkbxOrRadioBtnID, bool toggleChkBoxIfAlreadyChecked = true);
        public abstract string UploadFile(string fileName = "");
        public abstract bool VerifyActiveModalTitle(string expectedModalTitle);
        public abstract bool VerifyAndAcceptAlertMessage(string expectedMessage);
        public abstract bool VerifyChkBoxRdoBtnSelection(Enum rdoBtnOrChkBox, bool shouldBeSelected = true);
        public abstract bool VerifyDDListSelectedValue(Enum ddListId, string expectedDDListValue);
        public abstract bool VerifyExpectedList(IList<string> actualList, IList<string> expectedList, string verificationMethodName = "");
        public abstract bool VerifyFieldErrorIsDisplayed(By elementByLocator);
        public abstract bool VerifyInputField(Enum inputField, bool shouldFieldBeEmpty = false);
        public abstract bool VerifyPageHeader(string expectedPageHeading);
        public abstract void VerifyPageIsLoaded(bool checkingLoginPage = false, bool continueTestIfPageNotLoaded = true);
        public abstract bool VerifyRequiredFieldErrorLabelIsDisplayed<T>(T fieldInputIdEnumOrString);
        public abstract bool VerifySchedulerIsDisplayed();
        public abstract bool VerifySuccessMessageIsDisplayed();
        public abstract bool VerifyTextAreaField(Enum textAreaField, bool textFieldShouldHaveValue = true);
        public abstract bool VerifyUploadedFileNames<T>(T expectedFileName, bool beforeSubmitBtnAction = false, bool forDIR = true, int dirEntryNumber = 1);
        public abstract bool VerifyUrlIsLoaded(string pageUrl);
        public abstract void WaitForElement(By elementByLocator, int timeOutInSeconds = 10, int pollingInterval = 500, bool waitForLoading = true);
        public abstract void WaitForElementToClear(By locator);
        public abstract void WaitForLoading();
        public abstract void WaitForPageReady(int timeOutInSeconds = 60, int pollingInterval = 10000, bool waitForLoading = true);
    }
}