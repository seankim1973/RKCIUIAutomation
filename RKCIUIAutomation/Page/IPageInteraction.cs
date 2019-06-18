using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using static RKCIUIAutomation.Page.PageInteraction;

namespace RKCIUIAutomation.Page
{
    public interface IPageInteraction
    {
        void EnterSignature();
        string AcceptAlertMessage();
        void ClearText(By elementByLocator);
        void ClickCancel();
        void ClickCreate();
        void ClickElement(By elementByLocator);
        void ClickLoginLink();
        void ClickLogoutLink();
        void ClickNew(bool multipleBtnInstances = false);
        void ClickSave();
        void ClickSaveForward();
        void ClickSubmitForward();
        void CloseActiveModalWindow();
        void ConfirmActionDialog(bool confirmYes = true);
        string DismissAlertMessage();
        bool CheckIfElementIsDisplayed(By elementByLocator);
        void EnterText(By elementByLocator, string text, bool clearField = true);
        void ExecuteJsAction(JSAction jsAction, By elementByLocator);
        void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName, bool useContains = false, bool isMultiSelectDDList = false);
        void ExpandDDL<E>(E ddListID, bool isMultiSelectDDList = false);
        string GetAttribute(By elementByLocator, string attributeName);
        IList<string> GetAttributes<T>(T elementByLocator, string attributeName);
        string GetCurrentUser(bool getFullName = false);
        IWebElement GetElement(By elementByLocator);
        IList<IWebElement> GetElements(By elementByLocator);
        int GetElementsCount(By elementByLocator);
        string GetPageUrl(int timeOutInSeconds = 10, int pollingInterval = 500);
        WebDriverWait GetStandardWait(IWebDriver driver, int timeOutInSeconds = 10, int pollingInterval = 500);
        string GetText(By elementByLocator);
        IList<string> GetTextForElements(By elementByLocator);
        string GetTextFromDDL(Enum ddListID);
        /// <summary>
        /// Mouse click in main webpage body.
        /// <para>(Example: clicks away from input field to initiate auto-save feature in Comment Grid in LAX)</para>
        /// </summary>
        void ClickInMainBodyAwayFromField();
        string GetTextFromDDListInActiveTab(Enum ddListID);
        IList<string> GetTextFromMultiSelectDDL(Enum multiSelectDDListID);
        string GetUserDownloadFolderPath();
        void JsClickElement(By elementByLocator);
        string JsGetPageTitle(string windowHandle = "");
        void JsHover(By elementByLocator);
        void LogoutToLoginPage();
        void RefreshWebPage();
        IWebElement ScrollToElement<T>(T elementOrLocator);
        void SelectRadioBtnOrChkbox(Enum chkbxOrRadioBtn, bool toggleChkBoxIfAlreadyChecked = true);
        string GetPageTitle(int timeOutInSeconds = 10, int pollingInterval = 500);
        string UploadFile(string fileName = "");
        bool VerifyActiveModalTitle(string expectedModalTitle);
        bool VerifyAlertMessage(string expectedMessage);
        bool VerifyChkBoxRdoBtnSelection(Enum rdoBtnOrChkBox, bool shouldBeSelected = true);
        bool VerifyDDListSelectedValue(Enum ddListId, string expectedDDListValue);
        bool VerifyExpectedList(IList<string> actualList, IList<string> expectedList, string verificationMethodName = "");
        bool VerifyFieldErrorIsDisplayed(By elementByLocator);
        bool VerifyInputField(Enum inputField, bool shouldFieldBeEmpty = false);
        bool VerifyPageHeader(string expectedPageHeading);
        void VerifyPageIsLoaded(bool checkingLoginPage = false, bool continueTestIfPageNotLoaded = true);
        bool VerifySchedulerIsDisplayed();
        bool VerifySuccessMessageIsDisplayed();
        bool VerifyTextAreaField(Enum textAreaField, bool emptyFieldExpected = false);
        bool VerifyUploadedFileNames<T>(T expectedFileName, bool beforeSubmitBtnAction = false, bool forDIR = true, int dirEntryNumber = 1);
        bool VerifyUrlIsLoaded(string pageUrl);
        void WaitForElement(By elementByLocator, int timeOutInSeconds = 10, int pollingInterval = 500);
        void WaitForElementToClear(By locator);
        void WaitForLoading();
        void WaitForPageReady(int timeOutInSeconds = 60, int pollingInterval = 10000, bool checkForLoader = true);
    }
}