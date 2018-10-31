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
        public Action()
        {
        }

        public Action(IWebDriver driver) => Driver = driver;

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
                LogError($"Unable to perform {jsAction.ToString()} action on javascript element: - {elementByLocator}", true, e);
                throw;
            }
        }

        public void JsClickElement(By elementByLocator)
        {
            //ScrollToElement(elementByLocator);
            ExecuteJsAction(JSAction.Click, elementByLocator);
        }

        public void JsHover(By elementByLocator)
        {
            ExecuteJsAction(JSAction.Hover, elementByLocator);
            Thread.Sleep(1000);
        }

        private void WaitForElement(By elementByLocator, int timeOutInSeconds = 5, int pollingInterval = 250)
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
                log.Error($"WaitForElement timeout occurred for element: - {elementByLocator}", e);
            }
        }

        internal void WaitForPageReady(int timeOutInSeconds = 20, int pollingInterval = 500)
        {
            WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(timeOutInSeconds))
            {
                PollingInterval = TimeSpan.FromMilliseconds(pollingInterval)
            };

            wait.Until(Driver =>
            {
                bool isLoaderHidden = (bool)((IJavaScriptExecutor)Driver).
                    ExecuteScript("return $('.k-loading-image').is(':visible') == false");
                bool isAjaxFinished = (bool)((IJavaScriptExecutor)Driver).
                    ExecuteScript("return jQuery.active == 0");
                return isAjaxFinished && isLoaderHidden;
            });
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
                LogError($"Unable to GetElement: - {elementByLocator}\n{e.StackTrace}");
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
                LogDebug($"Unable to locate elements: - {elementByLocator}", e);
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
                if (elem != null)
                {
                    LogInfo($"Clicked element: - {elementByLocator}");
                }
                else
                {
                    LogError($"Null element: - {elementByLocator}");
                }
            }
            catch (Exception e)
            {
                LogError(e.Message);
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

        public string GetAttribute(By elementByLocator, string attributeName) => GetElement(elementByLocator).GetAttribute(attributeName);

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
                LogError($"Unable to enter text in field - {commentTypeLocator}", true, e);
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
                LogError($"Unable to enter text in field - {elementByLocator}", true, e);
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
                By locator = new PageHelper().GetDDListByLocator(ddListID);
                ScrollToElement(locator);
                text = $"{GetText(locator)}//span[@class='k-input']";
                LogInfo($"Retrieved text '{text}' from element - {ddListID?.GetString()}");
            }
            catch (Exception e)
            {
                LogError($"Unable to retrieve text from drop-down element - {ddListID?.GetString()}", true, e);
            }
            return text;
        }

        public void ExpandDDL<E>(E ddListID)
        {
            var _ddListID = (ddListID.GetType() == typeof(string)) ? ConvertToType<string>(ddListID) : ConvertToType<Enum>(ddListID).GetString();
            By locator = new PageHelper().GetExpandDDListButtonByLocator(_ddListID);
            try
            {
                ClickElement(locator);
                Thread.Sleep(2000);
                LogInfo($"Expanded DDList - {_ddListID}");
            }
            catch (Exception e)
            {
                LogError($"Unable to expand drop down list - {locator}", true, e);
            }
        }

        public void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName)
        {
            var _ddListID = (ddListID.GetType() == typeof(string)) ? ConvertToType<string>(ddListID) : ConvertToType<Enum>(ddListID).GetString();
            ExpandDDL(_ddListID);
            ClickElement(new PageHelper().GetDDListItemsByLocator(_ddListID, itemIndexOrName));
        }

        public void UploadFile(string fileName)
        {
            string filePath = (testPlatform == TestPlatform.Local) ? $"{GetCodeBasePath()}\\UploadFiles\\{fileName}" : $"/home/seluser/UploadFiles/{fileName}";

            try
            {
                By uploadInput_ByLocator = By.Id("UploadFiles_0_");
                EnterText(uploadInput_ByLocator, filePath);
                LogInfo($"Entered {filePath}' for file upload");
            }
            catch (Exception e)
            {
                LogError("Exception occured during file upload", true, e);
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
                LogError($"Unable to accept browser alert message", true, e);
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
                IWebElement element = GetElement(elementByLocator);
                isDisplayed = (element != null) ? true : false;
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
            //Driver.Navigate().GoToUrl($"{siteUrl}/Account/LogIn");
        }

        public void ClickLogoutLink()
        {
            GetElement(By.XPath("//a[contains(text(),'Log out')]")).Click();
            //Driver.Navigate().GoToUrl($"{siteUrl}/Account/LogOut");
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