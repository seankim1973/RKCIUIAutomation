using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Page.PageInteraction;

namespace RKCIUIAutomation.Page
{
    public interface IPageInteraction
    {
        string AcceptAlertMessage();

        void ClearText(By elementByLocator);

        void ClickCancel();

        void ClickCreate();

        void ClickElement(By elementByLocator);

        void ClickElementByID(Enum elementIdEnum);

        /// <summary>
        /// Mouse click in main webpage body.
        /// <para>(Example: clicks away from input field to initiate auto-save feature in Comment Grid in LAX)</para>
        /// </summary>
        void ClickInMainBodyAwayFromField();

        void ClickLoginLink();

        void ClickLogoutLink();

        void ClickNew(bool multipleBtnInstances = false);

        void ClickSave();

        void ClickSaveForward();

        void ClickSubmitForward();

        void CloseActiveModalWindow();

        void ConfirmActionDialog(bool confirmYes = true);

        string DismissAlertMessage();

        bool ElementIsDisplayed(By elementByLocator);

        void EnterSignature();

        void EnterText(By elementByLocator, string text, bool clearField = true);

        void ExecuteJsAction(JSAction jsAction, By elementByLocator);

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
        /// <param name="useContainsOperator"></param>
        void ExpandAndSelectFromDDList<E, T>(E ddListID, T itemIndexOrName, bool useContains = false, bool isMultiSelectDDList = false);

        void ExpandDDL<E>(E ddListID, bool isMultiSelectDDList = false);

        string GetAttributeForElement(By elementByLocator, string attributeName);

        IList<string> GetAttributeForElements<T>(T elementByLocator, string attributeName);

        string GetCurrentUser(bool getFullName = false);

        IWebElement GetElement(By elementByLocator, bool waitForLoading = true);

        IList<IWebElement> GetElements(By elementByLocator, bool waitForLoading = true);

        int GetElementsCount(By elementByLocator);

        string GetPageTitle(int timeOutInSeconds = 10, int pollingInterval = 500);

        string GetPageUrl(int timeOutInSeconds = 10, int pollingInterval = 500);

        WebDriverWait GetStandardWait(IWebDriver driver, int timeOutInSeconds = 10, int pollingInterval = 500);

        string GetText(By elementByLocator, bool shouldReturnValue = true, bool logReport = true);

        IList<string> GetTextForElements(By elementByLocator);

        string GetTextFromDDL<T>(T ddListID, bool isMultiSelectDDList = false);

        string GetTextFromDDListInActiveTab(Enum ddListID);

        IList<string> GetTextFromMultiSelectDDL<T>(T multiSelectDDListID);

        string GetUserDownloadFolderPath();

        void JsClickElement(By elementByLocator);

        string JsGetPageTitle(string windowHandle = "");

        void JsHover(By elementByLocator);

        void LoginAs(UserType user);

        void LogoutToLoginPage();

        void RefreshWebPage();

        void ScrollCommentTabInToView();

        void ScrollPageToBottom();

        void ScrollPageToTop();

        IWebElement ScrollToElement<T>(T elementOrLocator);

        void SelectRadioBtnOrChkbox<T>(T chkbxOrRadioBtnID, bool toggleChkBoxIfAlreadyChecked = true);

        string UploadFile(string fileName = "");

        bool VerifyActiveModalTitle(string expectedModalTitle);

        bool VerifyAndAcceptAlertMessage(string expectedMessage);

        bool VerifyChkBoxRdoBtnSelection(Enum rdoBtnOrChkBox, bool shouldBeSelected = true);

        bool VerifyDDListSelectedValue(Enum ddListId, string expectedDDListValue);

        bool VerifyExpectedList(IList<string> actualList, IList<string> expectedList, string verificationMethodName = "");

        bool VerifyFieldErrorIsDisplayed(By elementByLocator);

        bool VerifyInputField(Enum inputField, bool shouldFieldBeEmpty = false);

        bool VerifyPageHeader(string expectedPageHeading);

        void VerifyPageIsLoaded(bool checkingLoginPage = false, bool continueTestIfPageNotLoaded = true);

        /// <summary>
        /// Gets a value indicating whether or not a 'Required' input field error label is displayed.
        /// </summary>
        /// <remarks>
        /// System.ArgumentException.ArgumentException: Thrown when the provided argument is not of supported types - Enum or string value of input field ID.
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldInputIdEnumOrString"></param>
        /// <returns></returns>
        bool VerifyRequiredFieldErrorLabelIsDisplayed<T>(T fieldInputIdEnumOrString);

        bool VerifySchedulerIsDisplayed();

        bool VerifySuccessMessageIsDisplayed();

        bool VerifyTextAreaField(Enum textAreaField, bool textFieldShouldHaveValue = true);

        /// <summary>
        /// Provide string or IList<string> of expected file names to verify is seen in the Attachments section of the Details Page
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="expectedFileName"></param>
        /// <returns></returns>
        bool VerifyUploadedFileNames<T>(T expectedFileName, bool beforeSubmitBtnAction = false, bool forDIR = true, int dirEntryNumber = 1);

        bool VerifyUrlIsLoaded(string pageUrl);

        void WaitForElement(By elementByLocator, int timeOutInSeconds = 10, int pollingInterval = 500, bool waitForLoading = true);

        void WaitForElementToClear(By locator);

        void WaitForLoading();

        void WaitForPageReady(int timeOutInSeconds = 60, int pollingInterval = 10000, bool waitForLoading = true);
    }
}