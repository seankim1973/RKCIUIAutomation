using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Test;
using RKCIUIAutomation.Tools;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;

namespace RKCIUIAutomation.Page.Workflows
{
    public class QaRcrdCtrl_QaDIR_WF : QaRcrdCtrl_QaDIR_WF_Impl
    {
        public QaRcrdCtrl_QaDIR_WF()
        {
        }

        public QaRcrdCtrl_QaDIR_WF(IWebDriver driver) => this.Driver = driver;

        internal void CreateNew_and_PopulateRequiredFields()
        {
            LogDebug($"-->---> CreateNew_and_PopulateRequiredFields <---<--");

            QaRcrdCtrl_QaDIR.ClickBtn_CreateNew();
            QaRcrdCtrl_QaDIR.SetDirNumber();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();            
            string dirNumber = QaRcrdCtrl_QaDIR.GetDirNumber();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInCreate(dirNumber), "VerifyDirIsDisplayedInCreate as DIRTech");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WaitForPageReady();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir(), "VerifyReqFieldErrorsForNewDir");
            QaRcrdCtrl_QaDIR.PopulateRequiredFields();
        }

        internal void Verify_DIR_then_Approve(TableTab tableTab, string dirNumber)
        {
            string tableTabName = tableTab.ToString();
            LogDebug($"---> Verify_DIR_then_Approve_in {tableTabName} <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber), $"VerifyDirIsDisplayed(TableTab.{tableTabName})");
            ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
        }

        internal bool DIR_DeleteOrApproveNoError(TableTab tableTab, string dirNumber, bool delete = false, bool approveDIR = true)
        {
            string assertMsg = string.Empty;
            bool isDisplayed = false;

            if (delete)
            {
                //ClickEditBtnForRow();
                //QaRcrdCtrl_QaDIR.ClickBtn_Cancel();
                isDisplayed = WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete(tableTab, dirNumber, delete);
                assertMsg = $"Set to Delete DIR";
            }
            else
            {
                isDisplayed = QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber);

                if (isDisplayed)
                {
                    ClickEditBtnForRow();

                    if (approveDIR)
                    {
                        WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
                    }
                    else
                    {
                        By latestWfStatusLocator = By.XPath("//div[@class='CommentsDiv']//table//tbody/tr[1]/td[4]");
                        string latestStatus = GetText(latestWfStatusLocator);

                        LogInfo($"Activity Log Latest Workflow Status from Authorization : {latestStatus}", isDisplayed);
                    }
                }

                assertMsg = $"Verify DIR isDisplayed";
            }

            AddAssertionToList(isDisplayed, $"DIR_DeleteOrApproveNoError ({tableTab.ToString()}) {assertMsg}");
            return isDisplayed;
        }

        internal void VerifyColumnFilterInTab(TableTab tableTab, UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
        {
            LogDebug($"---> VerifyColumnFilterInTab <---<br>TableTab {tableTab.ToString()}<br>Current User {currentUser}<br>DIR# {dirNumber}<br>DIR Rev {dirRev}<br> KickedBack {kickedBack}<br>Use QA Field Menu {useQaFieldMenu}");

            string dirTechEmail = "testDirTech@rkci.com";
            string dirMgrEmail = "testDirMgr@rkci.com";

            string sentBy = dirMgrEmail;
            string lockedBy = dirMgrEmail;

            try
            {
                switch (tableTab)
                {
                    case TableTab.Creating:
                        sentBy = dirTechEmail;
                        lockedBy = dirTechEmail;
                        break;
                    case TableTab.Create_Revise:
                        sentBy = kickedBack 
                            ? dirMgrEmail 
                            : dirTechEmail;
                        lockedBy = kickedBack 
                            ? dirMgrEmail 
                            : dirTechEmail;
                        break;
                    case TableTab.QC_Review:
                        sentBy = kickedBack
                            ? dirMgrEmail
                            : useQaFieldMenu
                                ? dirTechEmail
                                : dirMgrEmail;
                        break;
                }

                if (tableTab == TableTab.Attachments)
                {
                    WF_QaRcrdCtrl_QaDIR.LoginToDirPage(currentUser);
                }

                string[] columnFilterValues = new string[] { dirNumber, sentBy, lockedBy, dirRev };
                IterateColumnsByFilter(tableTab, currentUser, columnFilterValues, useQaFieldMenu);

                if (tableTab != TableTab.To_Be_Closed)
                {
                    ClickEditBtnForRow();
                }

                if (tableTab == TableTab.Creating || tableTab == TableTab.Create_Revise)
                {
                    QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
                }
                else if (tableTab == TableTab.QC_Review)
                {
                    if (kickedBack)
                    {
                        WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();
                        QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
                        QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
                    }
                    else
                    {
                        WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
                    }
                }
                else if (tableTab == TableTab.Authorization)
                {
                    WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();
                    QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
                    QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
                }
                else if (tableTab == TableTab.Attachments)
                {
                    if (kickedBack)
                    {
                        WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();
                    }
                    else
                    {
                        QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
                    }
                }
                else if (tableTab == TableTab.Revise)
                {
                    QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
                }
                else if (tableTab == TableTab.To_Be_Closed)
                {
                    bool isDisplayedInClosed = QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.To_Be_Closed, dirNumber);

                    if (isDisplayedInClosed)
                    {
                        SelectCheckboxForRow(dirNumber);
                        QaRcrdCtrl_QaDIR.ClickBtn_Close_Selected();
                    }

                    AddAssertionToList(isDisplayedInClosed, $"Verify DIR ({dirNumber}) is displayed in Closed Tab");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }

        /// <summary>
        /// >Use order for all table tabs (except 'Create Packages' and 'Packages') ::
        /// string[] columnFilterValues {dirNumber, sentBy, lockedBy, dirRev}
        /// <para>>Use order for 'Create Packages' table tab :: string[] columnFilterValues {weekStart, weekEnd, newDirCount, newDIRs}</para>
        /// <para>>Use order for 'Packages' table tab :: string[] columnFilterValues {weekStart, weekEnd, packageNumber, DIRs}</para>
        /// </summary>
        /// <param name="tableTab"></param>
        /// <param name="currentUser"></param>
        /// <param name="columnFilterValues"></param>
        /// <param name="useQaFieldMenu"></param>
        internal void IterateColumnsByFilter(TableTab tableTab, UserType currentUser, string[] columnFilterValues, bool useQaFieldMenu = false)
        {
            try
            {
                if (tableTab != TableTab.Create_Packages || tableTab != TableTab.Packages)
                {
                    string dirNumber = columnFilterValues[0];
                    string sentBy = columnFilterValues[1];
                    string lockedBy = columnFilterValues[2];
                    string dirRev = columnFilterValues[3];
                    string dirTechEmail = "testDirTech@rkci.com";

                    LogDebug($"---> IterateColumnsByFilter <---<br>TableTab {tableTab.ToString()}<br>DIR# {dirNumber}<br>SentBy {sentBy}<br>LockedBy {lockedBy}<br>DIR Rev {dirRev}<br>Use Qa Field Menu {useQaFieldMenu}");

                    AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber), $"\nIterateColumnsByFilter VerifyDirIsDisplayed {tableTab.ToString()}, before DIR Locked");

                    if (tableTab == TableTab.To_Be_Closed)
                    {
                        ClickEditBtnForRow(dirNumber, true, true);
                    }
                    else
                    {
                        ClickEditBtnForRow();
                    }

                    WF_QaRcrdCtrl_QaDIR.LoginToDirPage(currentUser, useQaFieldMenu); //locks DIR
                    AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber), $"IterateColumnsByFilter VerifyDirIsDisplayed {tableTab.ToString()}, after DIR Locked");
                    ClearTableFilters();
                    AddAssertionToList(true, $"#### IterateColumnsByFilter ####\nTableTab : {tableTab.ToString()}, Use QAField Menu : {useQaFieldMenu}, DIR# : {dirNumber}, SentBy : {sentBy}, LockedBy : {lockedBy}, DIR Rev : {dirRev}");
                    AddAssertionToList(VerifyRecordIsDisplayed(ColumnName.Revision, dirRev), $"IterateColumnsByFilter Column Revision");
                    AddAssertionToList(VerifyRecordIsDisplayed(ColumnName.Created_By, dirTechEmail), $"IterateColumnsByFilter Column Created_By");
                    AddAssertionToList(VerifyRecordIsDisplayed(ColumnName.Sent_By, sentBy, TableType.MultiTab, false, FilterOperator.IsAfterOrEqualTo), $"IterateColumnsByFilter Column Sent_By");
                    AddAssertionToList(VerifyRecordIsDisplayed(ColumnName.Sent_Date, GetShortDate(), TableType.MultiTab, false, Page.FilterOperator.IsAfterOrEqualTo), $"IterateColumnsByFilter Column Sent_Date");
                    AddAssertionToList(VerifyRecordIsDisplayed(ColumnName.Locked_By, lockedBy), $"IterateColumnsByFilter Column Locked_By");
                    AddAssertionToList(VerifyRecordIsDisplayed(ColumnName.Locked_Date, GetShortDate(), TableType.MultiTab, false, Page.FilterOperator.IsAfterOrEqualTo), $"IterateColumnsByFilter Column Locked Date");
                    AddAssertionToList(QaRcrdCtrl_QaDIR.GetDirNumberForRow(dirTechEmail).Equals(dirNumber), $"IterateColumnsByFilter DIR# for Filtered Row Matches Expected: ");
                }
                else
                {
                    string wkStart = columnFilterValues[0];
                    string wkEnd = columnFilterValues[1];
                    string newDirCountOrPkgNumber = columnFilterValues[2];
                    string newDIRsOrDIRs = columnFilterValues[3];
                    string column3Name = "Package Number";
                    string column4Name = "DIRs";
                    PackagesColumnName newDirCountOrPkgNumberCol = PackagesColumnName.Package_Number;
                    PackagesColumnName newDIRsOrDIRsCol = PackagesColumnName.DIRs;

                    if (tableTab == TableTab.Create_Packages)
                    {
                        column3Name = "New DIR Count";
                        column4Name = "New DIRs";
                        newDirCountOrPkgNumberCol = PackagesColumnName.New_DIR_Count;
                        newDIRsOrDIRsCol = PackagesColumnName.New_DIRs;
                    }

                    if (newDIRsOrDIRs.Contains(","))
                    {
                        string[] splitNewDIRs = new string[] { };
                        splitNewDIRs = Regex.Split(newDIRsOrDIRs, ", ");
                        newDIRsOrDIRs = splitNewDIRs[0];
                    }

                    AddAssertionToList(VerifyRecordIsDisplayed(PackagesColumnName.Week_Start, wkStart), $"IterateColumnsByFilter Column Week Start");
                    AddAssertionToList(VerifyRecordIsDisplayed(PackagesColumnName.Week_End, wkEnd), $"IterateColumnsByFilter Column Week End");
                    AddAssertionToList(VerifyRecordIsDisplayed(newDirCountOrPkgNumberCol, newDirCountOrPkgNumber), $"IterateColumnsByFilter Column {column3Name}");
                    AddAssertionToList(VerifyRecordIsDisplayed(newDIRsOrDIRsCol, newDIRsOrDIRs), $"IterateColumnsByFilter Column {column4Name}");
                }

                int totalRows = GetTableRowCount();
                bool filteredDownToSingleRow = totalRows.Equals(1);

                string logMsg = filteredDownToSingleRow
                    ? "Filtered table down to single row as expected"
                    : $"Expected To Filter Table to 1 Row, but found {totalRows.ToString()} rows";
                AddAssertionToList(filteredDownToSingleRow, $"IterateColumnsByFilter - Total Number of Rows after Filter Equals 1");
                bool[] results = new bool[] { filteredDownToSingleRow, totalRows >= 1 };
                LogInfo(logMsg, results);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw e;
            }
        }

        internal bool Verify_DirRevision_inTblRow_then_Approve(TableTab tableTab, string dirNumber, string expectedDirRev)
        {
            LogDebug($"---> Verify_DirRevision_inTblRow_then_Approve in {tableTab.ToString()} - Expected DIR Rev: {expectedDirRev} <---");

            bool dirRevMatches = false;
            string ifFalseLog = string.Empty;
            bool isDisplayed = false;

            try
            {
                if (string.IsNullOrEmpty(expectedDirRev))
                {
                    bool dirRevFound = VerifyRecordIsDisplayed(QADIRs.ColumnName.Revision, expectedDirRev);
                    AddAssertionToList(dirRevFound, $"VerifyRecordIsDisplayed - DIR Revision: {expectedDirRev}");
                    LogInfo($"Successfully filtered table by expected DIR Revision: {expectedDirRev}", dirRevFound);
                }

                isDisplayed = (tableTab == TableTab.Creating || tableTab == TableTab.Create_Revise)
                    ? WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber)
                    : QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber, false);

                string actualDirRev = isDisplayed ? GetColumnValueForRow(dirNumber, "Revision") : "DIR Not Found";
                dirRevMatches = actualDirRev.Equals(expectedDirRev);
                ifFalseLog = dirRevMatches ? "" : $"<br>Actual DIR Rev: {actualDirRev}";

                if (isDisplayed)
                {
                    ClickEditBtnForRow();

                    if (tableTab == TableTab.Creating || tableTab == TableTab.Create_Revise)
                    {
                        QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
                    }
                    else
                    {
                        WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
            }

            LogInfo($"Expected DIR Rev: {expectedDirRev}{ifFalseLog}<br>Actual Matches Expected Rev? : {dirRevMatches}", dirRevMatches);
            return dirRevMatches;
        }

        internal string[] GetDirIdForRow<T>(T textInColumnForRowOrRowIndex)
        {
            string href = GetPdfHref(textInColumnForRowOrRowIndex, true, true);
            string[] dirID = Regex.Split(href, "ViewDirPDF\\?DirId=");
            return dirID;
        }

        internal bool Verify_ViewDirPDF(TableTab tableTab, string dirNumber = "")
        {
            QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber);
            return VerifyViewPdfReport(dirNumber);
        }

        internal string ClickViewSelectedDirPDFs(bool selectNoneForMultiView)
        {
            string expectedUrl = "No Checkboxes Selected (Expected URL is Empty)";
            int rowCount = 0;

            try
            {
                if (!selectNoneForMultiView)
                {
                    rowCount = GetTableRowCount();
                    rowCount = rowCount > 3 ? 3 : rowCount;

                    expectedUrl = $"{GetDirIdForRow(1)[0]}ViewMultiDirPDF?";

                    for (int i = 0; i < rowCount; i++)
                    {
                        int rowIndex = i + 1;
                        string dirID = $"DirIds[{i}]={GetDirIdForRow(rowIndex)[1]}&";
                        expectedUrl = $"{expectedUrl}{dirID}";
                        SelectCheckboxForRow(rowIndex);
                    }

                    expectedUrl = expectedUrl.TrimEnd('&');
                }

                QaRcrdCtrl_QaDIR.ClickBtn_View_Selected();

                
                LogStep(
                    selectNoneForMultiView
                    ? "Clicked View Selected button without row selection"
                    : $"Clicked View Selected button after selecting top {rowCount} rows"
                );
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }

            return expectedUrl;
        }

    }

    public interface IQaRcrdCtrl_QaDIR_WF
    {
        /// <summary>
        /// [bool]useQaFieldMenu arg defaults to false and is ignored for Simple Workflow Tenants or when using [UserType]DIRTechQA
        /// </summary>
        /// <param name="userType"></param>
        /// <param name="useQaFieldMenu"></param>
        void LoginToDirPage(UserType userType, bool useQaFieldMenu = false);

        void LoginToQaFieldDirPage(UserType userType);

        void LoginToRcrdCtrlDirPage(UserType userType);

        void ClickTab_Create();

        string Create_and_SaveForward_DIR();

        string Create_and_SaveOnly_DIR();

        string Create_DirRevision_For_Package_Recreate_ComplexWF_EndToEnd(string weekStartDate, string packageNumber, string[] dirNumbers);

        void FilterRecreateColumnWithoutButtonAscending();

        /// <summary>
        /// Returns string[] array: [0] dirNumber of newly created DIR, [1] dirNumber of Previous Failing Report
        /// </summary>
        string[] Create_and_SaveForward_DIR_with_Failed_Inspection_and_PreviousFailingReports();

        void Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber);

        void Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(string dirNumber);

        void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber);

        void Modify_Cancel_Verify_inCreateRevise(string dirNumber);

        void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber);

        void LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr(string dirNumber);

        void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber);

        void Verify_DIR_then_Approve_inReview(string dirNumber);

        void Verify_DIR_then_Approve_inAuthorization(string dirNumber);

        bool VerifyDirIsDisplayedInCreate(string dirNumber);

        bool VerifyDirIsDisplayedInRevise(string dirNumber);

        bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber);

        bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision);

        bool Verify_DIR_Delete(TableTab tableTab, string dirNumber, bool acceptAlert = true);

        void Verify_Column_Filter_ForTabs_In_QaFieldMenu(UserType currentUser, string dirNumber, string dirRev = "A");

        bool Verify_DIR_Delete_or_ApproveNoError_inQcReview(string dirNumber, bool delete = false);

        bool Verify_DIR_Delete_or_ApproveNoError_inAuthorization(string dirNumber, bool delete = false, bool approveDIR = true);

        void ClickBtn_ApproveOrNoError();

        void ClickBtn_KickBackOrRevise();

        bool Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise(string dirNumber, string expectedDirRev);

        bool Verify_DirRevision_inTblRow_then_Approve_inQcReview(string dirNumber, string expectedDirRev);

        bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev);

        void Edit_DIR_inCreate_and_Verify_AutoSaveTimerRefresh_then_Save(string dirNumber);

        void RefreshAutoSaveTimer();

        void IterateColumnsByFilterInRevise(UserType currentUser, string dirNumber, string sentBy, string lockedBy, string dirRev, bool useQaFieldMenu = false);

        void Verify_Column_Filter_In_Create(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);

        void Verify_Column_Filter_In_Revise(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);

        void Verify_Column_Filter_In_QcReview(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);

        void Verify_Column_Filter_In_Authorization(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);

        void Verify_Column_Filter_In_Attachments(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);

        void Verify_Column_Filter_In_ToBeClosed(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);

        bool Verify_ViewReport_forDIR_inCreate(string dirNumber);

        bool Verify_ViewReport_forDIR_inRevise(string dirNumber);

        bool Verify_ViewReport_forDIR_inQcReview(string dirNumber);

        bool Verify_ViewReport_forDIR_inAuthorization(string dirNumber);

        bool Verify_ViewMultiDirPDF(bool selectNoneForMultiView = false);

        bool VerifyDbCleanupForDIR(string dirnumber, string revision = "A", bool setAsDeleted = true);

        void VerifyDbCleanupForCreatePackages(bool isPkgCreated, string weekStartDate, string[] dirNumbers);
    }

    public abstract class QaRcrdCtrl_QaDIR_WF_Impl : TestBase, IQaRcrdCtrl_QaDIR_WF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IQaRcrdCtrl_QaDIR_WF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IQaRcrdCtrl_QaDIR_WF instance = new QaRcrdCtrl_QaDIR_WF(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_LAX(driver);
            }
            return instance;
        }

        internal QaRcrdCtrl_QaDIR_WF QaDirWF_Base => new QaRcrdCtrl_QaDIR_WF(Driver);

        public virtual void LoginToDirPage(UserType userType, bool useQaFieldMenu = false)
        {
            bool QaFieldDIR = false;
            string expectedPageTitle = "List of Inspector's Daily Report"; //(default) QcRecordControl page title
            string logMsg = useQaFieldMenu ? "QaField" : "RecordControl";
            LogDebug($"---> LoginToDirPage : {logMsg} <---");

            try
            {
                LoginAs(userType);
                WaitForPageReady();
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
            finally
            {
                if (userType == UserType.DIRTechQA || userType == UserType.DIRMgrQA)
                {
                    string tenant = tenantName.ToString();

                    if (tenant.Equals("SGWay") || tenant.Equals("SH249") || tenant.Equals("Garnet"))
                    {
                        QaFieldDIR = userType == UserType.DIRTechQA 
                            ? true
                            : useQaFieldMenu
                                ? true
                                : false;

                        if (QaFieldDIR)
                        {
                            expectedPageTitle = "IQF Field > List of Daily Inspection Reports";
                            NavigateToPage.QAField_QA_DIRs();
                        }
                        else
                        {
                            expectedPageTitle = "IQF Record Control > List of Daily Inspection Reports";
                            NavigateToPage.QARecordControl_QA_DIRs();
                        }
                    }
                    else
                    {
                        NavigateToPage.QARecordControl_QA_DIRs();
                    }
                }
                else if (userType == UserType.DIRTechQC || userType == UserType.DIRMgrQC)
                {
                    NavigateToPage.QCRecordControl_QC_DIRs();
                }

                AddAssertionToList(VerifyPageHeader(expectedPageTitle), $"VerifyPageTitle - Expected Page Title: {expectedPageTitle}");
            }
        }

        public virtual void LoginToQaFieldDirPage(UserType userType)
            => LoginToDirPage(userType, true);

        public virtual void LoginToRcrdCtrlDirPage(UserType userType)
            => LoginToDirPage(userType);

        //SH249, SG, Garnet - Creating
        //LAX, I15Tech, I15SB, GLX - Create/Revise
        public virtual void ClickTab_Create() => QaRcrdCtrl_QaDIR.ClickTab_Create_Revise();

        //GLX, LAX, I15SB, I15Tech
        public virtual bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber);

        public virtual bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Create_Revise, dirNumber);

        //GLX,
        public virtual string Create_and_SaveForward_DIR()
        {
            LogDebug($"---> Create_and_SaveForward_DIR <---");

            QaDirWF_Base.CreateNew_and_PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        public virtual string Create_and_SaveOnly_DIR()
        {
            LogDebug($"---> Create_and_SaveOnly_DIR <---");

            QaDirWF_Base.CreateNew_and_PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        public virtual string[] Create_and_SaveForward_DIR_with_Failed_Inspection_and_PreviousFailingReports()
        {
            LogDebug($"---> Create_and_SaveForward_Failed_Inspection_DIR_with_PreviousFailingReports <---");

            QaDirWF_Base.CreateNew_and_PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            string previousFailedDirNumber = QaRcrdCtrl_QaDIR.CreatePreviousFailingReport();
            string dirNumber = QaRcrdCtrl_QaDIR.GetDirNumber();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInCreate(dirNumber), $"VerifyDirIsDisplayedInCreate DirNo. {dirNumber}");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyPreviousFailingDirEntry(previousFailedDirNumber), $"VerifyPreviousFailingDirEntry in Create: {previousFailedDirNumber}");
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            string[] dirNumbers = new string[2]
                {
                    dirNumber,
                    previousFailedDirNumber
                };
            return dirNumbers;
        }

        public virtual string Create_DirRevision_For_Package_Recreate_ComplexWF_EndToEnd(string weekStartDate, string packageNumber, string[] dirNumbers)
        {
            //TODO - can't use weekStartDate as inspectDate - DIRs in package may not always be on weekStart date
            //TODO arg needs to be an existing DIR for TechID of DIRQA user
            QaRcrdCtrl_QaDIR.EnterText_InspectionDate(weekStartDate);
            QaRcrdCtrl_QaDIR.ClickBtn_CreateRevision();
            QaRcrdCtrl_QaDIR.SetDirNumber();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();

            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        public virtual void FilterRecreateColumnWithoutButtonAscending()
        {
            SortColumnAscending(PackagesColumnName.Week_Start);
            FilterTableColumnByValue(PackagesColumnName.Recreate, "false");
            FilterTableColumnByValue(PackagesColumnName.DIRs, QaRcrdCtrl_QaDIR.GetTechIdForDirUserAcct(true), TableType.MultiTab, FilterOperator.Contains);
        }

        public virtual void Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab kickBackfromTableTab, string dirNumber)
        {
            LogDebug($"---> KickBack_DIR_ForRevise_From{kickBackfromTableTab.ToString()}Tab_then_Edit_inCreateReview <---");
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(kickBackfromTableTab, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            ClickBtn_KickBackOrRevise();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
            QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
            WaitForPageReady();
            AddAssertionToList(VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            ClickEditBtnForRow();
        }

        public virtual void Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(string dirNumber)
            => WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.QC_Review, dirNumber);

        public virtual void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
        {
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
        }

        private void Upload_Cancel_Verify_inAttachments(string dirNumber)
        {
            LogDebug($"---> Upload_Cancel_Verify_inAttachments <---");

            UploadFile();
        }

        public virtual void Modify_Cancel_Verify_inCreateRevise(string dirNumber)
        {
            LogDebug($"---> Modify_Cancel_Verify_inCreateRevise <---");

            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Cancel();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Inspection_Result_P), "VerifyChkBoxRdoBtnSelection Inspection_Result_P");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Deficiencies_No), "VerifyChkBoxRdoBtnSelection AnyDeficiencies_No");
            AddAssertionToList(VerifyTextAreaField(InputFields.Deficiency_Description, true), "VerifyTextAreaField Deficiency_Description - Should Be Empty");
        }

        public virtual void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber)
        {
            LogDebug($"---> Modify_Save_Verify_and_SaveForward_inCreateRevise <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDeficiencySelectionPopupMessages(), "VerifyDeficiencySelectionPopupMessages");
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Inspection_Result_F), "VerifyChkBoxRdoBtnSelection(Inspection_Result_F)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnsAndCheckboxes.Deficiencies_Yes), "VerifyChkBoxRdoBtnSelection(Deficiencies_Yes)");
            AddAssertionToList(VerifyTextAreaField(InputFields.Deficiency_Description), "VerifyTextAreaField(Deficiency_Description)");
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
        }

        public virtual void LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr(string dirNumber)
        {
            LogDebug($"---> LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr <---");

            LogoutToLoginPage();
            LoginAs(UserType.DIRTechQA);
            NavigateToPage.QAField_QA_DIRs();

            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_E(false); //Edit 'Results' checkbox in Revise
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();

            LogoutToLoginPage();
            LoginAs(UserType.DIRMgrQA);
            NavigateToPage.QAField_QA_DIRs();
        }

        //GLX, I15SB, I15Tech, LAX
        public virtual void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            LogDebug($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFields.Engineer_Comments));
            //WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
            //AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
        }

        public virtual void Verify_DIR_then_Approve_inReview(string dirNumber)
            => QaDirWF_Base.Verify_DIR_then_Approve(TableTab.QC_Review, dirNumber);

        public virtual void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
            => QaDirWF_Base.Verify_DIR_then_Approve(TableTab.Authorization, dirNumber);

        public virtual bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Closed);

        public virtual bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Closed, true, expectedRevision);

        public virtual bool Verify_DIR_Delete_or_ApproveNoError_inQcReview(string dirNumber, bool delete = false)
            => QaDirWF_Base.DIR_DeleteOrApproveNoError(TableTab.QC_Review, dirNumber, delete, true);

        //SimpleWF tenants
        public virtual bool Verify_DIR_Delete_or_ApproveNoError_inAuthorization(string dirNumber, bool delete = false, bool approveDIR = false)
            => QaDirWF_Base.DIR_DeleteOrApproveNoError(TableTab.Authorization, dirNumber, delete, approveDIR);

        public virtual void ClickBtn_ApproveOrNoError()
            => QaRcrdCtrl_QaDIR.ClickBtn_Approve();

        public virtual void ClickBtn_KickBackOrRevise()
            => QaRcrdCtrl_QaDIR.ClickBtn_KickBack();

        public virtual bool Verify_DIR_Delete(TableTab tableTab, string dirNumber, bool acceptAlert = true)
        {
            LogDebug($"---> Verify_DIR_Delete in {tableTab.ToString()} - Accept Alert: {acceptAlert} <---");

            bool isDisplayed = false;
            string actionPerformed = string.Empty;
            string resultAfterAction = string.Empty;
            bool result = false;

            try
            {
                isDisplayed = QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber);
                AddAssertionToList(isDisplayed, $"VerifyDirIsDisplayed in {tableTab.ToString()}, before interacting with delete dialog");

                if (isDisplayed)
                {
                    ClickDeleteBtnForRow();
                    if (acceptAlert)
                    {
                        try
                        {
                            AcceptAlertMessage();
                            AcceptAlertMessage();
                        }
                        catch (Exception e)
                        {
                            log.Debug(e.Message);
                        }
                        finally
                        {
                            actionPerformed = "Accepted";
                            resultAfterAction = "Displayed After Accepting Delete Dialog: ";
                        }
                    }
                    else
                    {
                        try
                        {
                            DismissAlertMessage();
                            DismissAlertMessage();
                        }
                        catch (Exception e)
                        {
                            log.Debug(e.Message);
                        }
                        finally
                        {
                            actionPerformed = "Dismissed";
                            resultAfterAction = "Displayed After Dismissing Delete Dialog: ";
                        }
                    }

                    var verifyResult = QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber, acceptAlert);

                    result = acceptAlert ? !verifyResult : verifyResult;

                    AddAssertionToList(result, $"VerifyDirIsDisplayed in {tableTab.ToString()}, after {actionPerformed} delete dialog");
                    LogInfo($"Performed Action: {actionPerformed} delete dialog<br>{resultAfterAction}{isDisplayed}", result);
                }
                else
                {
                    LogError($"Unable to find DIR No. {dirNumber} in {tableTab.ToString()} Tab");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return result;
        }

        public virtual bool Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise(string dirNumber, string expectedDirRev)
            => QaDirWF_Base.Verify_DirRevision_inTblRow_then_Approve(TableTab.Create_Revise, dirNumber, expectedDirRev);

        public virtual bool Verify_DirRevision_inTblRow_then_Approve_inQcReview(string dirNumber, string expectedDirRev)
            => QaDirWF_Base.Verify_DirRevision_inTblRow_then_Approve(TableTab.QC_Review, dirNumber, expectedDirRev);

        public virtual bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev)
            => QaDirWF_Base.Verify_DirRevision_inTblRow_then_Approve(TableTab.Authorization, dirNumber, expectedDirRev);

        public virtual void Edit_DIR_inCreate_and_Verify_AutoSaveTimerRefresh_then_Save(string dirNumber)
        {
            LogDebug("---> Edit_DIR_inCreate_and_Verify_AutoSaveTimerRefresh_then_Save <---");

            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInCreate(dirNumber), "VerifyDirIsDisplayedInCreate (Verify AutoSaveTimer Refresh)");
            ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyAutoSaveTimerRefresh(), "VerifyAutoSaveTimerRefresh");
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
        }

        public virtual void RefreshAutoSaveTimer()
            => QaRcrdCtrl_QaDIR.ClickBtn_Refresh();

        public virtual void IterateColumnsByFilterInRevise(UserType currentUser, string dirNumber, string sentBy, string lockedBy, string dirRev, bool useQaFieldMenu = false)
            => QaDirWF_Base.IterateColumnsByFilter(TableTab.Create_Revise, currentUser, new string[] { dirNumber, sentBy, lockedBy, dirRev }, useQaFieldMenu);

        //GLX, LAX, I15SB, I15Tech
        public virtual void Verify_Column_Filter_In_Create(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.Create_Revise, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);
            
        public virtual void Verify_Column_Filter_In_QcReview(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.QC_Review, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public virtual void Verify_Column_Filter_In_Authorization(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.Authorization, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public virtual void Verify_Column_Filter_In_Revise(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.Create_Revise, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        //SG
        public virtual void Verify_Column_Filter_ForTabs_In_QaFieldMenu(UserType currentUser, string dirNumber, string dirRev = "A")
        {
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_QcReview(currentUser, dirNumber, dirRev, false, true); //kickback in SH249
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Authorization(currentUser, dirNumber, dirRev, true, true); //kickback in SGWay
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Revise(currentUser, dirNumber, dirRev, true, true); //SaveFwd
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inQcReview(dirNumber, false);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inAuthorization(dirNumber, false, true);
        }

        //SG & SH249
        public virtual void Verify_Column_Filter_In_Attachments(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.Attachments, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public virtual void Verify_Column_Filter_In_ToBeClosed(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.To_Be_Closed, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        //GLX, LAX, I15SB, I15Tech
        public virtual bool Verify_ViewReport_forDIR_inCreate(string dirNumber)
            => QaDirWF_Base.Verify_ViewDirPDF(TableTab.Create_Revise, dirNumber);

        public virtual bool Verify_ViewReport_forDIR_inRevise(string dirNumber)
            => QaDirWF_Base.Verify_ViewDirPDF(TableTab.Create_Revise, dirNumber);
        
        public virtual bool Verify_ViewReport_forDIR_inQcReview(string dirNumber)
            => QaDirWF_Base.Verify_ViewDirPDF(TableTab.QC_Review, dirNumber);

        public virtual bool Verify_ViewReport_forDIR_inAuthorization(string dirNumber)
            => QaDirWF_Base.Verify_ViewDirPDF(TableTab.Authorization, dirNumber);

        public virtual bool Verify_ViewMultiDirPDF(bool selectNoneForMultiView = false)
            => VerifyViewPdfReport("", true, selectNoneForMultiView);

        public virtual bool VerifyDbCleanupForDIR(string dirNumber, string dirRevision = "A", bool setAsDeleted = true)
        {
            bool cleanupSuccessful = false;

            try
            {
                DirDbAccess db = new DirDbAccess();
                db.SetDIR_DIRNO_IsDeleted(dirNumber, dirRevision, setAsDeleted);

                DirDbData data = new DirDbData();
                data = db.GetDirData(dirNumber, dirRevision);
                int isDeleted = data.IsDeleted;

                //if isDeleted = 1 && setAsDeleted = true --> success
                //if isDeleted = 0 && setAsDeleted = false --> success
                //if isDeleted = 0 && setAsDeleted = true --> fail
                //if isDeleted = 1 && setAsDeleted = false --> fail
                cleanupSuccessful = setAsDeleted
                    ? isDeleted == 1
                        ? true : false
                    : isDeleted == 0
                        ? true : false;
                LogStep($"Performed DB Cleanup for DIR# {dirNumber}");
            }
            catch (Exception e)
            {
                log.Error($"Error occured during VerifyDbCleanupForDIR : {e.Message}");
            }

            return cleanupSuccessful;
        }

        public virtual void VerifyDbCleanupForCreatePackages(bool isPkgCreated, string weekStartDate, string[] dirNumbers)
        {
            if (isPkgCreated)
            {
                string packageNumber = QaRcrdCtrl_QaDIR.CalculateDirPackageNumber(weekStartDate);

                DirDbAccess db = new DirDbAccess();
                db.SetDIRPackageNo_AndReferences_AsDeleted(packageNumber);

                DirDbData data = new DirDbData();
                data = db.GetDirPackageData(packageNumber);
                int isDeleted = data.IsDeleted;

                bool result = isDeleted == 1 ? true : false;
                string resultLog = isDeleted == 0 ? " NOT" : "";
                AddAssertionToList(result, $"PackageNo ({packageNumber}) IsDeleted has{resultLog} been updated)");

                List<bool> dirPkgIdResetResults = new List<bool>();
                foreach (string dir in dirNumbers)
                {
                    bool dirPkgIdisNull = false;

                    data = db.GetDirData(dir);
                    var dirPkgId = data.DIRPackageID;

                    if (dirPkgId == null)
                    {
                        dirPkgIdisNull = true;
                    }
                    else
                    {
                        AddAssertionToList(dirPkgIdisNull, $"DIR ({dir}) should have NULL value for DIRPackageID, but has value of: ({dirPkgId.ToString()})");
                    }

                    dirPkgIdResetResults.Add(dirPkgIdisNull);
                }

                result = !dirPkgIdResetResults.Contains(false);
                resultLog = result ? "" : " NOT";
                AddAssertionToList(result, $"DB cleanup for DIRPackageID references for the associated DIRs are as{resultLog} expected.");
            }
            else
            {
                AddAssertionToList(false, $"Task, DB cleanup for DIRPackageID references, was not performed because the Package was not created successfully in the previous step.");
            }
        }
    }

    internal class QaRcrdCtrl_QaDIR_WF_GLX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_QaDIR_WF_Garnet : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_Garnet(IWebDriver driver) : base(driver)
        {
        }

        public override void ClickTab_Create()
            => QaRcrdCtrl_QaDIR.ClickTab_Creating();
    }

    internal class QaRcrdCtrl_QaDIR_WF_I15Tech : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_QaDIR_WF_I15South : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_QaDIR_WF_SH249 : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void ClickTab_Create()
            => QaRcrdCtrl_QaDIR.ClickTab_Creating();

        public override bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber);

        public override bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Creating, dirNumber);

        public override void Verify_Column_Filter_In_Revise(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.Revise, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void Verify_Column_Filter_In_Create(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.Creating, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void IterateColumnsByFilterInRevise(UserType currentUser, string dirNumber, string sentBy, string lockedBy, string dirRev, bool useQaFieldMenu = false)
            => QaDirWF_Base.IterateColumnsByFilter(TableTab.Revise, currentUser, new string[] { dirNumber, sentBy, lockedBy, dirRev }, useQaFieldMenu);

        public override void Verify_Column_Filter_In_Authorization(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => LogStep("---> Verify_Column_Filter_In_Authorization <---<br>Step skipped for Tenant SH249");

        public override void Verify_Column_Filter_ForTabs_In_QaFieldMenu(UserType currentUser, string dirNumber, string dirRev = "A")
        {
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_QcReview(currentUser, dirNumber, dirRev, true, true);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Revise(currentUser, dirNumber, dirRev, true, true);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inQcReview(dirNumber, false);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inAuthorization(dirNumber, false, true);
        }

        public override void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
            => LogStep("---> KickBack_DIR_ForRevise_FromAuthorization_then_Edit <---<br>Step skipped for Tenant SH249");

        public override void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
            => LogStep("Step skipped for Tenant SH249: Verify_DIR_then_Approve_inAuthorization");

        public override bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev)
        {
            LogStep("---> Verify_DirRevision_inTblRow_then_Approve_inAuthorization <---<br>Step skipped for Tenant SH249");
            return true;
        }

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment);

        public override bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment, true, expectedRevision);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inQcReview(string dirNumber, bool delete = false)
            => QaDirWF_Base.DIR_DeleteOrApproveNoError(TableTab.QC_Review, dirNumber, delete, true);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inAuthorization(string dirNumber, bool delete = false, bool approveDIR = true)
        {
            LogStep($"---> Skipped Test Step for Tenant SH249<br>Verify_DIR_Delete_or_ApproveNoError_inAuthorization<---");
            return true;
        }

        public override bool Verify_ViewReport_forDIR_inAuthorization(string dirNumber)
        {
            LogStep($"---> Skipped Test Step for Tenant SH249<br>Verify_ViewReport_forDIR_inAuthorization<---");
            return true;
        }

        public override void ClickBtn_ApproveOrNoError()
            => QaRcrdCtrl_QaDIR.ClickBtn_NoError();

        public override void ClickBtn_KickBackOrRevise()
            => QaRcrdCtrl_QaDIR.ClickBtn_Revise();

        public override void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            LogStep($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFields.Engineer_Comments));
            ClickBtn_ApproveOrNoError();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            ClickBtn_ApproveOrNoError();
        }

        public override bool Verify_ViewReport_forDIR_inCreate(string dirNumber)
            => QaDirWF_Base.Verify_ViewDirPDF(TableTab.Creating, dirNumber);

        public override bool Verify_ViewReport_forDIR_inRevise(string dirNumber)
            => QaDirWF_Base.Verify_ViewDirPDF(TableTab.Revise, dirNumber);
    }

    internal class QaRcrdCtrl_QaDIR_WF_SGWay : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void ClickTab_Create() => QaRcrdCtrl_QaDIR.ClickTab_Creating();

        public override bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Revise, dirNumber);

        public override bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Creating, dirNumber);

        public override void Verify_Column_Filter_In_Revise(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.Revise, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void Verify_Column_Filter_In_Create(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => QaDirWF_Base.VerifyColumnFilterInTab(TableTab.Creating, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void IterateColumnsByFilterInRevise(UserType currentUser, string dirNumber, string sentBy, string lockedBy, string dirRev, bool useQaFieldMenu = false)
            => QaDirWF_Base.IterateColumnsByFilter(TableTab.Revise, currentUser, new string[] { dirNumber, sentBy, lockedBy, dirRev }, useQaFieldMenu);

        public override void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
        {
            Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(TableTab.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            Verify_DIR_then_Approve_inReview(dirNumber);

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.Authorization, dirNumber), "VerifyDirIsDisplayed");
            ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Back_To_QC_Review();
            Verify_DIR_then_Approve_inReview(dirNumber);
        }

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment);

        public override bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment, true, expectedRevision);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inQcReview(string dirNumber, bool delete = false)
            => QaDirWF_Base.DIR_DeleteOrApproveNoError(TableTab.QC_Review, dirNumber, delete, true);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inAuthorization(string dirNumber, bool delete = false, bool approveDIR = true)
            => QaDirWF_Base.DIR_DeleteOrApproveNoError(TableTab.Authorization, dirNumber, delete, approveDIR);

        public override void ClickBtn_ApproveOrNoError()
            => QaRcrdCtrl_QaDIR.ClickBtn_NoError();

        public override void ClickBtn_KickBackOrRevise()
            => QaRcrdCtrl_QaDIR.ClickBtn_Revise();

        public override void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            LogStep($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(TableTab.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFields.Engineer_Comments));
            ClickBtn_ApproveOrNoError();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            ClickBtn_ApproveOrNoError();
        }

        public override bool Verify_ViewReport_forDIR_inCreate(string dirNumber)
            => QaDirWF_Base.Verify_ViewDirPDF(TableTab.Creating, dirNumber);

        public override bool Verify_ViewReport_forDIR_inRevise(string dirNumber)
            => QaDirWF_Base.Verify_ViewDirPDF(TableTab.Revise, dirNumber);
    }

    internal class QaRcrdCtrl_QaDIR_WF_LAX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}