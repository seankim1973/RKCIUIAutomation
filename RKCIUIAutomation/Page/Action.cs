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

namespace RKCIUIAutomation.Page
{
    public class Action : BaseClass, IAction
    {
        //private PageHelper PgHelper => new PageHelper();

        public Action() { }

        public Action(IWebDriver driver) => this.Driver = driver;

        public enum JSAction
        {
            [StringValue("arguments[0].click();")] Click,

            [StringValue("var evObj = document.createEvent('MouseEvents');" +
                    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                    "arguments[0].dispatchEvent(evObj);")]
            Hover
        }

        public void ExecuteJsAction(JSAction jsAction, By elementByLocator)
        {
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;

            try
            {
                string javaScript = jsAction.GetString();
                IWebElement element = GetElement(elementByLocator);
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

            WaitForPageReady();
        }

        public void JsHover(By elementByLocator)
        {
            ExecuteJsAction(JSAction.Hover, elementByLocator);
            Thread.Sleep(1000);
        }

        public WebDriverWait GetStandardWait(IWebDriver driver, int timeOutInSeconds = 10, int pollingInterval = 500)
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
            catch (UnhandledAlertException)
            { }
            catch (Exception e)
            {
                log.Error($"Error occured in method GetStandardWait : {e.Message}");
            }

            return wait;
        }

        public void WaitForElement(By elementByLocator, int timeOutInSeconds = 10, int pollingInterval = 500)
        {
            try
            {
                WaitForPageReady();
                WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
                wait.Until(x => driver.FindElement(elementByLocator));
                log.Debug($"...waiting for element: - {elementByLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }

        public void WaitForElementToClear(By locator, int timeOutInSeconds = 60, int pollingInterval = 500)
        {
            try
            {
                WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
                wait.Until(x => ExpectedConditions.InvisibilityOfElementLocated(locator));
            }
            catch (Exception)
            {
                //log.Error(e.Message);
            }
        }

        public void WaitForLoading(int timeOutInSeconds = 60, int pollingInterval = 500)
        {
            try
            {
                string[] classNames = new string[]
                {
                    "k-overlay",
                    "k-loading-mask",
                    "k-loading-image"
                };

                foreach (string className in classNames)
                {
                    WaitForElementToClear(By.ClassName(className), timeOutInSeconds, pollingInterval);
                }
            }
            catch (Exception)
            {
            }
            finally
            {
                Thread.Sleep(1000);
            }
        }

        public void WaitForPageReady(int timeOutInSeconds = 60, int pollingInterval = 10000)
        {
            WaitForLoading();
            IJavaScriptExecutor javaScriptExecutor = driver as IJavaScriptExecutor;
            bool pageIsReady = false;

            try
            {
                try
                {
                    pageIsReady = (bool)javaScriptExecutor.ExecuteScript("return window.jQuery != undefined && jQuery.active === 0");
                }
                catch (InvalidOperationException)
                {
                    pageIsReady = (bool)javaScriptExecutor.ExecuteScript("return document.readyState == 'complete'");
                }

                if (!pageIsReady)
                {
                    log.Debug("...waiting for page to be in Ready state");

                    try
                    {
                        WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
                        wait.Until(x => (bool)javaScriptExecutor.ExecuteScript("return document.readyState == 'complete'"));
                    }
                    catch (UnhandledAlertException)
                    { }
                }
            }
            catch (InvalidOperationException)
            {
                try
                {
                    WebDriverWait wait = GetStandardWait(driver, timeOutInSeconds, pollingInterval);
                    wait.Until(x => (bool)javaScriptExecutor.ExecuteScript("return window.jQuery != undefined && jQuery.active === 0"));
                }
                catch (UnhandledAlertException)
                { }

            }
            catch (UnhandledAlertException)
            { }
            catch (Exception e)
            {
                log.Warn($"Error in WaitForPageReady method : {e.Message}");
            }

        }

        public void RefreshWebPage()
        {
            try
            {
                driver.Navigate().Refresh();
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

            try
            {
                WaitForElement(elementByLocator);
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
            => GetElements(elementByLocator).Count;
        
        public void ClickElement(By elementByLocator)
        {
            IWebElement elem = null;

            try
            {
                ScrollToElement(elementByLocator);
                elem = GetElement(elementByLocator);
                elem?.Click();

                bool elemNotNull = elem != null
                    ? true
                    : false;

                string logMsg = elemNotNull
                    ? "Clicked"
                    : "Null";

                LogStep($"{logMsg} element: - {elementByLocator}");
            }
            catch (Exception e)
            {
                LogError(e.StackTrace);
                throw;
            }
        }


        public string GetAttribute(By elementByLocator, string attributeName)
            => GetElement(elementByLocator)?.GetAttribute(attributeName);            

        //public IList<string> GetAttributes(By elementByLocator, string attributeName)
        //{
        //    IList<IWebElement> elements = GetElements(elementByLocator);
        //    IList<string> attributes = new List<string>();

        //    foreach (IWebElement elem in elements)
        //    {
        //        string attrib = elem.GetAttribute(attributeName);
        //        attributes.Add(attrib);
        //    }

        //    return attributes;
        //}

        public IList<string> GetAttributes<T>(T elementByLocator, string attributeName)
        {
            object argObject = null;
            Type argType = elementByLocator.GetType();
            IList<string> attributes = new List<string>();
            IList<IWebElement> elements = new List<IWebElement>();

            try
            {
                if (argType.Equals(typeof(By)))
                {
                    argObject = ConvertToType<By>(elementByLocator);
                    elements = GetElements((By)argObject);
                }
                else if (argType.Equals(typeof(List<By>)))
                {
                    argObject = ConvertToType<List<By>>(elementByLocator);

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
                log.Error(e.StackTrace);
            }

            return attributes;
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
                throw;
            }
        }

        public void EnterText(By elementByLocator, string text, bool clearField = true)
        {
            string logMsg = string.Empty;

            try
            {
                IWebElement textField = GetElement(elementByLocator);    
                bool validField = textField != null;

                if (validField)
                {
                    if (clearField)
                    {
                        textField.Clear();
                    }

                    textField.Click();
                    textField.SendKeys(text);

                    logMsg = $"Entered '{text}' in field - {elementByLocator}";
                    LogStep(logMsg);
                }
                else
                {
                    logMsg = $"Element: {elementByLocator} is null";
                }

                LogInfo(logMsg, validField);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        public string SetPageTitleVar(int timeOutInSeconds = 10, int pollingInterval = 500)
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

        public string GetPageUrl(int timeOutInSeconds = 10, int pollingInterval = 500)
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

        public string GetText(By elementByLocator)
        {
            IWebElement element = null;
            string text = string.Empty;
            bool hasValue = false;

            try
            {
                //element = GetElement(elementByLocator);
                element = ScrollToElement(elementByLocator);

                if (element.Displayed)
                {
                    text = element.Text;
                    hasValue = text.HasValue();

                    if (!hasValue)
                    {
                        text = element.GetAttribute("value");
                        hasValue = text.HasValue();
                    }

                    string logMsg = hasValue
                        ? $"Retrieved '{text}'"
                        : $"Unable to retrieve text";
                    LogStep($"{logMsg} from element - {elementByLocator}", false, hasValue);
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
            => GetText(PgHelper.GetDDListCurrentSelectionByLocator(ddListID));

        public IList<string> GetTextFromMultiSelectDDL(Enum multiSelectDDListID)
            => GetTextForElements(PgHelper.GetMultiSelectDDListCurrentSelectionByLocator(multiSelectDDListID));

        public void ExpandDDL<E>(E ddListID, bool isMultiSelectDDList = false)
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
                LogStep($"Expanded DDList - {ddListID.ToString()}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Use (bool)useContains arg when selecting a DDList item with partial value for [T](string)itemIndexOrName
        /// <para>
        /// (bool)useContains arg defaults to false and is ignored if arg [T]itemIndexOrName is an Integer
        /// </para>
        /// </summary>
        /// <typeparam name="E"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="ddListID"></param>
        /// <param name="itemIndexOrName"></param>
        /// <param name="useContains"></param>
        public void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName, bool useContains = false, bool isMultiSelectDDList = false)
        {
            ExpandDDL(ddListID, isMultiSelectDDList);
            ClickElement(PgHelper.GetDDListItemsByLocator(ddListID, itemIndexOrName, useContains));
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
            catch (UnhandledAlertException)
            { }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public string UploadFile(string fileName = "")
        {
            fileName = fileName.HasValue()
                ? fileName
                :"test.xlsx";

            string filePath = (testPlatform == TestPlatform.Local)
                ? $"{GetCodeBasePath()}\\UploadFiles\\{fileName}"
                : $"/home/seluser/UploadFiles/{fileName}";

            try
            {
                ScrollToElement(By.XPath("//h4[text()='Attachments']"));
                IWebElement uploadInputElem = GetElement(By.XPath("//input[@id='UploadFiles_0_']"));
                uploadInputElem.SendKeys(filePath);
                WaitForLoading();
                log.Info($"Entered {filePath}' for file upload");

                By uploadStatusLabel = By.XPath("//strong[@class='k-upload-status k-upload-status-total']");
                bool uploadStatus = ElementIsDisplayed(uploadStatusLabel);

                LogInfo($"File Upload {(uploadStatus ? "Successful" : "Failed")}.", uploadStatus);
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
        public bool VerifyUploadedFileNames<T>(T expectedFileName, bool beforeSubmitBtnAction = false, bool forDIR = true, int dirEntryNumber = 1)
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
            BaseUtils baseUtils = new BaseUtils();

            try
            {
                By testListDIV = By.XPath("//h5/b[contains(text(),'Test List')]");
                ScrollToElement(testListDIV);

                if (argType == typeof(string) || argType == typeof(List<string>))
                {                    
                    bool uploadedFileExpected = false;

                    if (argType == typeof(string))
                    {
                        expectedName = baseUtils.ConvertToType<string>(expectedFileName);
                        uploadedFileExpected = expectedName.HasValue();
                    }
                    else if (argType == typeof(List<string>))
                    {
                        expectedNames = new List<string>();
                        expectedNames = baseUtils.ConvertToType<IList<string>>(expectedFileName);
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

                                LogStep($"Found file name : {actualName}");

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
                                LogInfo($"{logMsg}", fileNameIsAsExpected);
                            }
                        }
                        else
                        {
                            assertList.Add(false);
                            LogInfo($"Expected uploaded file(s), but no uploaded file names are seen on the page", fileNameIsAsExpected);
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
                        LogInfo($"No uploaded file(s) are expected on the page{logMsg}", result);
                    }
                }
                else
                {
                    assertList.Add(false);
                    LogError($"Arg type should be string or IList<string> - Unexpected expectedFileName type: {argType}");
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

        public void ConfirmActionDialog(bool confirmYes = true)
        {
            string actionMsg = string.Empty;

            try
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
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

                LogStep($"Confirmation Dialog: {actionMsg}");
            }
            catch (UnhandledAlertException)
            { }
            catch (Exception e)
            {
                log.Debug(e.Message);
            }
        }

        public string AcceptAlertMessage()
        {
            string alertMsg = string.Empty;

            try
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
                alert = driver.SwitchTo().Alert();
                alertMsg = alert.Text;
                alert.Accept();
                LogStep($"Accepted browser alert: '{alertMsg}'");
            }
            catch (UnhandledAlertException)
            { }
            catch (Exception e)
            {
                log.Debug(e.Message);
            }

            return alertMsg;
        }

        public string DismissAlertMessage()
        {
            string alertMsg = string.Empty;

            try
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
                alert = driver.SwitchTo().Alert();
                alertMsg = alert.Text;
                alert.Dismiss();
                LogStep($"Dismissed browser alert: '{alertMsg}'");
            }
            catch (UnhandledAlertException)
            { }
            catch (Exception e)
            {
                log.Debug(e.Message);
            }

            return alertMsg;
        }

        public bool VerifyAlertMessage(string expectedMessage)
        {
            bool msgMatch = false;

            try
            {
                IAlert alert = new WebDriverWait(driver, TimeSpan.FromSeconds(5)).Until(ExpectedConditions.AlertIsPresent());
                string actualAlertMsg = driver.SwitchTo().Alert().Text;
                msgMatch = (actualAlertMsg).Contains(expectedMessage)
                    ? true
                    : false;

                LogInfo($"## Expected Alert Message: {expectedMessage}<br>## Actual Alert Message: {actualAlertMsg}", msgMatch);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
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
            IWebElement elem = null;

            try
            {
                elem = GetElement(elementByLocator);
            }
            catch (NoSuchElementException nse)
            {
                log.Debug($"NoSuchElementException in ElementIsDisplayed(): {nse.Message}");
            }
            catch (Exception e)
            {
                log.Error($"Error in ElementIsDisplayed(): {e.StackTrace}");
            }

            return elem.Displayed;
        }

        public bool VerifyPageHeader(string expectedPageHeading)
        {
            bool pageHeadingsMatch = false;
            bool isDisplayed = false;
            string actualHeading = string.Empty;
            string logMsg = string.Empty;
            IWebElement headingElem = null;

            try
            {
                pageTitle = SetPageTitleVar();

                if (pageTitle.Contains("ELVIS PMC"))
                {
                    if (!pageTitle.Contains("Home Page"))
                    {
                        headingElem = driver.FindElement(By.XPath("//h3"))
                            ?? driver.FindElement(By.XPath("//h2"))
                            ?? driver.FindElement(By.XPath("//h4"));

                        isDisplayed = headingElem?.Displayed == true
                            ? true
                            : false;

                        if (isDisplayed)
                        {
                            actualHeading = headingElem.Text;
                            pageHeadingsMatch = actualHeading.Equals(expectedPageHeading);

                            logMsg = pageHeadingsMatch
                                ? $"## Page Heading is as expected: {actualHeading}"
                                : $"Page Headings did not match: Expected: {expectedPageHeading}, Actual: {actualHeading}";

                            if (!pageHeadingsMatch)
                            {
                                BaseHelper.InjectTestStatus(TestStatus.Failed, logMsg);
                            }
                        }
                        else
                        {
                            logMsg = $"Could not find page heading with h2, h3 or h4 tag";
                            BaseHelper.InjectTestStatus(TestStatus.Failed, logMsg);
                        }

                        LogInfo(logMsg, pageHeadingsMatch);
                    }
                }
                else
                {
                    VerifyPageIsLoaded(false, false);
                }
            }
            catch (Exception)
            {
            }

            return pageHeadingsMatch;
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
                    ?? GetElement(resourceNotFoundH2Tag);

                isPageLoaded = pageErrElement.Displayed
                    ? false
                    : true;

                if (!isPageLoaded)
                {
                    LogError(GetText(stackTraceTagByLocator));
                }

                //Console.WriteLine($"##### IsPageLoadedSuccessfully - IsPageLoaded: {isPageLoaded}");

                string logMsg = isPageLoaded 
                    ? $"!!! Page Error - {pageErrElement.Text}<br>{pageUrl}"
                    : $"!!! Error at {pageUrl}";
                //Console.WriteLine($"##### IsPageLoadedSuccessfully - LogMsg: {logMsg}");

                logMsgKey = "logMsgKey";
                PgBaseHelper.CreateVar(logMsgKey, logMsg);
            }
            catch (Exception e)
            {
                log.Error($"Error in IsPageLoadedSuccessfully() : {e.StackTrace}");
            }

            return isPageLoaded;
        }

        private string GetPageErrorLogMsg() => PgBaseHelper.GetVar(testEnv);

        public bool VerifyUrlIsLoaded(string pageUrl)
        {
            bool isLoaded = false;
            string logMsg = string.Empty;

            try
            {
                driver.Navigate().GoToUrl(pageUrl);
                WaitForPageReady();
                pageTitle = SetPageTitleVar();
                isLoaded = pageTitle.Contains("ELVIS PMC")
                    ? true
                    : IsPageLoadedSuccessfully();

                logMsg = isLoaded
                    ? $">>> Page Loaded Successfully <<< <br>{pageUrl}"
                    : GetPageErrorLogMsg();

                LogInfo(logMsg, isLoaded);

                WriteToFile(logMsg, "_PageTitle.txt");
            }
            catch (Exception e)
            {
                log.Error($"Error in VerifyUrlIsLoaded() : {e.StackTrace}");
            }

            return isLoaded;
        }

        public void VerifyPageIsLoaded(bool checkingLoginPage = false, bool continueTestIfPageNotLoaded = true)
        {
            bool isLoaded = false;
            string logMsg = string.Empty;

            string expectedPageTitle = checkingLoginPage
                ? "Log in"
                : "ELVIS PMC";

            try
            {
                pageTitle = SetPageTitleVar();
                isLoaded = pageTitle.Contains(expectedPageTitle)
                    ? true 
                    : IsPageLoadedSuccessfully();

                logMsg = isLoaded 
                    ? $">>> Page Loaded Successfully <<< <br>Page Title : {pageTitle}"
                    : GetPageErrorLogMsg();

                LogInfo(logMsg, isLoaded);

                if (!isLoaded)
                {
                    if (continueTestIfPageNotLoaded == true)
                    {
                        LogInfo(">>> Attempting to navigate back to previous page to continue testing <<<");
                        driver.Navigate().Back();
                        WaitForPageReady();
                        SetPageTitleVar();
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
                log.Error($"Error in VerifyPageIsLoaded() : {e.StackTrace}");
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

        public bool VerifyExpectedList(IList<string> actualList, IList<string> expectedList, string verificationMethodName = "")
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

                LogInfo($"Expected and Actual Required Fields count are {logMsg}equal.<br>Actual Count: {actualCount}<br>Expected Count: {expectedCount}", countsMatch);

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
                LogInfo(logTable, fieldsMatch);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return fieldsMatch;
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
            try
            {
                (GetElement(GetButtonByLocator("Cancel"))
                    ?? GetElement(GetInputButtonByLocator("Cancel"))
                    ?? GetElement(By.Id("CancelSubmittal"))
                    ).Click();

                LogStep("Clicked Cancel");
                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }

        public void ClickSave()
        {
            try
            {
                (GetElement(By.XPath("//button[text()='Save']"))
                    ?? GetElement(By.Id("SaveSubmittal"))
                    ?? GetElement(By.Id("SaveItem"))
                    ).Click();

                LogStep("Clicked Save");
                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }
        }

        public void ClickSubmitForward()
        {
            try
            {
                (GetElement(By.XPath("//button[text()='Save & Forward']"))
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

        public void ClickCreate()
            => ClickElement(By.Id("btnCreate"));

        public void ClickNew(bool multipleBtnInstances = false)
        {
            try
            {
                if (multipleBtnInstances)
                {
                    ClickElement(By.XPath("//div[@class='k-content k-state-active']//a[text()='New']"));
                }
                else
                {
                    (GetElement(GetButtonByLocator("New"))
                        ?? GetElement(GetInputButtonByLocator("Create New"))
                        ).Click();
                }
                LogStep("Clicked New");
                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                throw;
            }
        }

        public void ClickSaveForward()
            => ClickElement(By.Id("SaveForwardItem"));

        public IWebElement ScrollToElement<T>(T elementOrLocator)
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
                    elem = GetElement(By.Id(locatorString)) ?? GetElement(By.XPath(locatorString));
                }

                if (elem != null)
                {
                    Actions actions = new Actions(driver);
                    actions.MoveToElement(elem);
                    actions.Perform();
                    log.Info($"Scrolled to element - {elementOrLocator}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return elem;
        }

        public void LogoutToLoginPage()
        {
            Thread.Sleep(3000);
            WaitForPageReady();
            ClickLogoutLink();
            WaitForPageReady();
            ClickLoginLink();
        }

        public void ClickLoginLink()
            => JsClickElement(By.XPath("//header//a[contains(text(),'Login')]"));

        public void ClickLogoutLink()
            => JsClickElement(By.XPath("//header//a[contains(text(),'Log out')]"));

        public string GetCurrentUser()
        {
            string userAcct = string.Empty;
            By locator = By.XPath("//a[@href='/Project/Account/']");
            
            try
            {
                userAcct = GetText(locator);
                userAcct = userAcct.Contains("Test")
                    ? userAcct.Contains("X")
                        ?Regex.Split((Regex.Split(userAcct, "Welcome Test ")[1]), "X")[0]
                        :Regex.Split(userAcct, "Welcome Test ")[1]
                    : userAcct.Contains("X")
                        ? Regex.Split((Regex.Split(userAcct, "Welcome ")[1]), " X")[0]
                        : Regex.Split(userAcct, "Welcome ")[1];

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