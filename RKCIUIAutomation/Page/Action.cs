using NUnit.Framework;
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
        PageHelper pgHelper = new PageHelper();

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
            Hover
        }

        private void ExecuteJsAction(JSAction jsAction, By elementByLocator)
        {
            try
            {
                string javaScript = jsAction.GetString();
                IWebElement element = GetElement(elementByLocator);
                IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
                executor.ExecuteScript(javaScript, element);
                LogInfo($"{jsAction.ToString()}ed on javascript element: - {elementByLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        public void JsClickElement(By elementByLocator) => ExecuteJsAction(JSAction.Click, elementByLocator);


        public void JsHover(By elementByLocator)
        {
            ExecuteJsAction(JSAction.Hover, elementByLocator);
            Thread.Sleep(1000);
        }

        private void WaitForElement(By elementByLocator, int timeOutInSeconds = 5, int pollingInterval = 500)
        {
            WaitForPageReady();

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
                log.Error($"WaitForElement timeout occurred for element: - {elementByLocator}", e);
            }
        }

        internal void WaitForPageReady(int timeOutInSeconds = 20, int pollingInterval = 1000)
        {
            var javaScriptExecutor = Driver as IJavaScriptExecutor;
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutInSeconds));

            try
            {
                Func<IWebDriver, bool> readyCondition = webDriver => (bool)javaScriptExecutor.ExecuteScript("return (document.readyState == 'complete' && jQuery.active == 0)");
                wait.Until(readyCondition);
            }
            catch (InvalidOperationException)
            {
                wait.Until(wd => javaScriptExecutor.ExecuteScript("return document.readyState").ToString() == "complete");
            }

            log.Debug($"##### Waited for page to be in Ready state #####");
        }

        internal IWebElement GetElement(By elementByLocator)
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
            IWebElement elem = null;
            try
            {
                elem = GetElement(elementByLocator);
                elem?.Click();
                bool elemNotNull = elem != null ? true : false;
                string logMsg = elemNotNull ? "Clicked" : "Null element";
                LogInfo($"{logMsg} element: - {elementByLocator}", elemNotNull);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
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

        public void EnterText(By elementByLocator, string text)
        {
            try
            {
                IWebElement textField = GetElement(elementByLocator);
                textField.Clear();
                textField.SendKeys(text);
                LogInfo($"Entered '{text}' in field - {elementByLocator}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public string GetText(By elementByLocator)
        {
            string text = string.Empty;
            try
            {
                ScrollToElement(elementByLocator);
                text = GetElement(elementByLocator)?.Text;
                bool textNotEmpty = !string.IsNullOrEmpty(text);
                string logMsg = textNotEmpty ? $"Retrieved '{text}'" : "Unable to retrieve text";
                LogInfo($"{logMsg} from element - {elementByLocator}");
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
            var _ddListID = (ddListID.GetType() == typeof(string)) ? ConvertToType<string>(ddListID) : ConvertToType<Enum>(ddListID).GetString();
            By locator = pgHelper.GetExpandDDListButtonByLocator(_ddListID);
            try
            {
                ClickElement(locator);
                Thread.Sleep(2000);
                log.Info($"Expanded DDList - {_ddListID}");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
        }

        public void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName)
        {
            var _ddListID = (ddListID.GetType() == typeof(string)) ? ConvertToType<string>(ddListID) : ConvertToType<Enum>(ddListID).GetString();
            ExpandDDL(_ddListID);
            ClickElement(pgHelper.GetDDListItemsByLocator(_ddListID, itemIndexOrName));
        }

        public void SelectRadioBtnOrChkbox(Enum chkbxOrRadioBtn, bool toggleChkBoxIfAlreadySelected = true)
        {
            string chkbxOrRdoBtn = chkbxOrRadioBtn.GetString();
            By locator = By.Id(chkbxOrRdoBtn);
            ScrollToElement(locator);
            if (toggleChkBoxIfAlreadySelected)
            {
                ScrollToElement(locator);
                JsClickElement(locator);
                LogInfo($"Selected: {chkbxOrRdoBtn}");
            }
            else
            {
                log.Info("Specified not to toggle checkbox, if already selected");

                if (!GetElement(locator).Selected)
                {
                    ScrollToElement(locator);
                    JsClickElement(locator);
                    LogInfo($"Selected: {chkbxOrRdoBtn}");
                }
                else
                {
                    LogInfo($"Did not select element, because it is already selected: {chkbxOrRdoBtn}");
                }
            }
        }

        public void UploadFile(string fileName)
        {
            string filePath = (testPlatform == TestPlatform.Local) ? $"{GetCodeBasePath()}\\UploadFiles\\{fileName}" : $"/home/seluser/UploadFiles/{fileName}";

            try
            {
                By uploadInput_ByLocator = By.Id("UploadFiles_0_");
                EnterText(uploadInput_ByLocator, filePath);
                log.Info($"Entered {filePath}' for file upload");
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }
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
                isDisplayed = GetElement(elementByLocator).Displayed ? true : false;
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

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

            headingElement = By.XPath($"//h3[contains(text(),\"{expectedPageTitle}\")]");
            isDisplayed = IsHeadingDisplayed(headingElement);
            if (!isDisplayed)
            {
                headingElement = By.XPath($"//h2[contains(text(),\"{expectedPageTitle}\")]");
                isDisplayed = IsHeadingDisplayed(headingElement);
                if (!isDisplayed)
                {
                    LogDebug($"Page Title element with h2 or h3 tag containing text \"{expectedPageTitle}\" was not found.");

                    headingElement = By.XPath("//h3");
                    isDisplayed = IsHeadingDisplayed(headingElement);
                    if (!isDisplayed)
                    {
                        headingElement = By.XPath("//h2");
                        isDisplayed = IsHeadingDisplayed(headingElement);
                        if (!isDisplayed)
                        {
                            string logMsg = $"Could not find any Page element with h2 or h3 tag";
                            BaseHelper.InjectTestStatus(TestStatus.Failed, logMsg);
                        }
                    }
                }
            }

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

        private string pageErrLogMsg = string.Empty;

        private bool FoundKnownPageErrors()
        {
            By serverErrorH1Tag = By.XPath("//h1[contains(text(),'Server Error')]");
            By resourceNotFoundH2Tag = By.XPath("//h2/i[text()='The resource cannot be found.']");
            By stackTraceTagByLocator = By.XPath("//b[text()='Stack Trace:']");

            IWebElement pageErrElement = Driver.FindElement(serverErrorH1Tag) ?? Driver.FindElement(stackTraceTagByLocator) ?? Driver.FindElement(resourceNotFoundH2Tag);
            bool foundKnownError = pageErrElement.Displayed ? true : false;
            string pageUrl = $"<br>&nbsp;&nbsp;@URL: {Driver.Url}";
            pageErrLogMsg = foundKnownError ? $"!!! Page Error - {pageErrElement.Text}{pageUrl}" : $"!!! Page did not load as expected.{pageUrl}";
            return false;
        }

        public bool VerifyUrlIsLoaded(string pageUrl)
        {
            bool isLoaded = false;
            string pageTitle = string.Empty;

            try
            {
                Driver.Navigate().GoToUrl(pageUrl);
                pageTitle = Driver.Title;

                isLoaded = (pageTitle.Contains("ELVIS PMC")) ? true : FoundKnownPageErrors();
            }
            finally
            {
                string pageTitleMsg = $"{pageUrl} - Page Title: {pageTitle}<br>&nbsp;&nbsp;";
                string logMsg = isLoaded ? ">>> Page Loaded Successfully <<<" : pageErrLogMsg;

                LogInfo($"{logMsg}<br>&nbsp;&nbsp;{pageTitleMsg}", isLoaded);
                if (!isLoaded)
                {
                    LogDebug($"Page Error seen at URL: {Driver.Url}");
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
                WaitForPageReady();
                pageTitle = Driver.Title;
                isLoaded = (pageTitle.Contains(expectedPageTitle)) ? true : FoundKnownPageErrors();
                logMsg = isLoaded ? ">>> Page Loaded Successfully <<<" : pageErrLogMsg;
                LogInfo(logMsg, isLoaded);

                if (!isLoaded)
                {
                    if (continueTestIfPageNotLoaded == true)
                    {
                        LogInfo(">>> Attempting to navigate back to previous page to continue testing <<<");
                        Driver.Navigate().Back();
                        pageTitle = Driver.Title;
                        isLoaded = pageTitle.Contains("ELVIS PMC") ? true : FoundKnownPageErrors();
                        if (isLoaded)
                        {
                            LogInfo(">>> Navigated to previous page successfully <<<");
                        }
                        else
                        {
                            LogInfo($"!!! Page did not load properly, when navigating to the previous page !!!<br>{pageErrLogMsg}");
                            Assert.Fail();
                        }
                    }
                    else
                    {
                        Assert.Fail();
                    }

                    BaseHelper.InjectTestStatus(TestStatus.Failed, pageErrLogMsg);
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

        public void ScrollToElement(By elementByLocator)
        {
            try
            {
                IWebElement elem = GetElement(elementByLocator);

                if (elem != null)
                {
                    Actions actions = new Actions(Driver);
                    actions.MoveToElement(elem);
                    actions.Perform();
                    log.Info($"Scrolled to element - {elementByLocator}");
                }
            }
            catch (Exception e)
            {
                LogError("Exception occured in ScrollToElement method", true, e);
            }
        }

        public void LogoutToLoginPage()
        {
            ClickLogoutLink();
            ClickLoginLink();
        }

        public void ClickLoginLink()
        {
            GetElement(By.XPath("//a[contains(text(),'Login')]")).Click();
        }

        public void ClickLogoutLink()
        {
            GetElement(By.XPath("//a[contains(text(),'Log out')]")).Click();
        }

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