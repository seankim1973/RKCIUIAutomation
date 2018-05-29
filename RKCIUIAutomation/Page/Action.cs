using AutoIt;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Threading;

namespace RKCIUIAutomation.Page
{
    public class Action : BaseClass
    {
        private bool WaitForElement(By elementByLocator)
        {
            try
            {
                LogInfo($"...waiting for element {elementByLocator}");
                WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(20))
                {
                    PollingInterval = TimeSpan.FromMilliseconds(500)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                wait.IgnoreExceptionTypes(typeof(ElementNotVisibleException));
                IWebElement webElem = wait.Until(x => x.FindElement(elementByLocator));
                return true;
            }
            catch (Exception e)
            {
                LogInfo($"WaitForElement timeout occured for element - {elementByLocator}", e);
            }
            return false;
        }

        public IWebElement GetElement(By elementByLocator)
        {
            IWebElement elem = null;
            if (WaitForElement(elementByLocator))
            {
                try
                {
                    elem = Driver.FindElement(elementByLocator);
                    return elem;
                }
                catch (Exception e)
                {
                    LogInfo($"Unable to locate element - {elementByLocator}", e);
                }
            }
            return elem;
        }

        public IList<IWebElement> GetElements(By elementByLocator)
        {
            IList<IWebElement> elements = null;
            if (WaitForElement(elementByLocator))
            {
                try
                {
                    elements = new List<IWebElement>();
                    elements = Driver.FindElements(elementByLocator);
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
            try
            {
                string javaScript = "var evObj = document.createEvent('MouseEvents');" +
                    "evObj.initMouseEvent(\"mouseover\",true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);" +
                    "arguments[0].dispatchEvent(evObj);";

                IJavaScriptExecutor executor = Driver as IJavaScriptExecutor;
                executor.ExecuteScript(javaScript, GetElement(elementByLocator));
                LogInfo($"Hover mouse over element - {elementByLocator}");
                Thread.Sleep(2000);
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

        public string GetTextFromDDL(Enum ddListID) => $"{GetText(PageHelper.GetDDListByLocator(ddListID))}//span[@class='k-input']";
        public void ExpandDDL(Enum ddListID)
        {
            By locator = null;
            try
            {
                locator = PageHelper.GetExpandDDListButtonByLocator(ddListID);
                ClickElement(locator);
                Thread.Sleep(1000);
                LogInfo($"Expanded DDList - {ddListID}");
            }
            catch (Exception e)
            {
                LogInfo($"Unable to expand drop down list - {locator}", e);
            }
        }

        public void ExpandAndSelectFromDDList(Enum ddListID, int selectItemIndex)
        {
            ExpandDDL(ddListID);
            ClickElement(PageHelper.GetDDListItemsByLocator(ddListID, selectItemIndex));
        }

        private void UploadUsingAutoItX(string filePath)
        {
            AutoItX.WinWaitActive("Open");
            AutoItX.ControlSend("Open", "", "Edit1", filePath);
            AutoItX.ControlClick("Open", "&Open", "Button1");
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
                IAllowsFileDetection allowsFileDetection = Driver as IAllowsFileDetection;
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
            var not = String.Empty;

            if (!elementDisplayed)
            {
                not = " not";
            }
                LogInfo($"Field error is{not} displayed for - {elementByLocator}", elementDisplayed);

            return elementDisplayed;
        }

        public bool VerifySuccessMessageIsDisplayed()
        {
            By elementByLocator = By.XPath("//div[contains(@class,'bootstrap-growl')]");
            IWebElement msg = GetElement(elementByLocator);
            bool elementDisplayed = msg.Displayed;
            var not = String.Empty;

            if (!elementDisplayed)
            {
                not = " not";
            }
            LogInfo($"Success Message is{not} displayed for - {elementByLocator}", elementDisplayed);

            return elementDisplayed;
        }
    }
}


//AutoItX.Run("notepad.exe", "C:\\Windows");
//AutoItX.WinWaitActive("Untitled");
//AutoItX.Send("Testing 1 2 3 4 5");
//IntPtr winHandle = AutoItX.WinGetHandle("Untitled");
//AutoItX.WinKill(winHandle);