using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Test;
using RKCIUIAutomation.Tools;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QADIRs;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;
using System.Threading;

namespace RKCIUIAutomation.Page.Workflows
{
    public class QaRcrdCtrl_QaDIR_WF : QaRcrdCtrl_QaDIR_WF_Impl
    {
        public QaRcrdCtrl_QaDIR_WF()
        {
        }

        public QaRcrdCtrl_QaDIR_WF(IWebDriver driver) => this.Driver = driver;

        public T SetClass<T>(IWebDriver driver)
        {
            IQaRcrdCtrl_QaDIR_WF instance = new QaRcrdCtrl_QaDIR_WF(driver);

            if (tenantName == TenantNameType.SGWay)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_SGWay(driver);
            }
            else if (tenantName == TenantNameType.SH249)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_SH249(driver);
            }
            else if (tenantName == TenantNameType.Garnet)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_Garnet(driver);
            }
            else if (tenantName == TenantNameType.GLX)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_GLX(driver);
            }
            else if (tenantName == TenantNameType.I15South)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_I15South(driver);
            }
            else if (tenantName == TenantNameType.I15Tech)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantNameType.LAX)
            {
                log.Info($"###### using QaRcrdCtrl_QaDIR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_QaDIR_WF_LAX(driver);
            }
            return (T)instance;
        }

        internal void CreateNew_and_PopulateRequiredFields()
        {
            Report.Step($"CreateNew_and_PopulateRequiredFields", true);

            QaRcrdCtrl_QaDIR.ClickBtn_CreateNew(true);
            QaRcrdCtrl_QaDIR.SetDirNumber();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            string dirNumber = QaRcrdCtrl_QaDIR.GetDirNumber();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInCreate(dirNumber), "VerifyDirIsDisplayedInCreate as DIRTech");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WaitForPageReady();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyReqFieldErrorsForNewDir(), "VerifyReqFieldErrorsForNewDir");
            QaRcrdCtrl_QaDIR.PopulateRequiredFields();
        }

        internal void Verify_DIR_then_Approve(GridTabType tableTab, string dirNumber)
        {
            string tableTabName = tableTab.ToString();
            Report.Step($"---> Verify_DIR_then_Approve_in {tableTabName} <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber), $"VerifyDirIsDisplayed(TableTab.{tableTabName})");
            GridHelper.ClickEditBtnForRow();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
        }

        internal bool DIR_DeleteOrApproveNoError(GridTabType tableTab, string dirNumber, bool delete = false, bool approveDIR = true)
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
                    GridHelper.ClickEditBtnForRow();

                    if (approveDIR)
                    {
                        WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
                    }
                    else
                    {
                        By latestWfStatusLocator = By.XPath("//div[@class='CommentsDiv']//table//tbody/tr[1]/td[4]");
                        string latestStatus = GetText(latestWfStatusLocator);

                        Report.Info($"Activity Log Latest Workflow Status from Authorization : {latestStatus}", isDisplayed);
                    }
                }

                assertMsg = $"Verify DIR isDisplayed";
            }

            AddAssertionToList(isDisplayed, $"DIR_DeleteOrApproveNoError ({tableTab.ToString()}) {assertMsg}");
            return isDisplayed;
        }

        internal void VerifyColumnFilterInTab(GridTabType tableTab, UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
        {
            Report.Step($"---> VerifyColumnFilterInTab <---<br>TableTab {tableTab.ToString()}<br>Current User {currentUser}<br>DIR# {dirNumber}<br>DIR Rev {dirRev}<br> KickedBack {kickedBack}<br>Use QA Field Menu {useQaFieldMenu}");

            string dirTechEmail = "testDirTech@rkci.com";
            string dirMgrEmail = "testDirMgr@rkci.com";

            string sentBy = dirMgrEmail;
            string lockedBy = dirMgrEmail;

            try
            {
                switch (tableTab)
                {
                    case GridTabType.Creating:
                        sentBy = dirTechEmail;
                        lockedBy = dirTechEmail;
                        break;
                    case GridTabType.Create_Revise:
                        sentBy = kickedBack 
                            ? dirMgrEmail 
                            : dirTechEmail;
                        lockedBy = kickedBack 
                            ? dirMgrEmail 
                            : dirTechEmail;
                        break;
                    case GridTabType.QC_Review:
                        sentBy = kickedBack
                            ? dirMgrEmail
                            : useQaFieldMenu
                                ? dirTechEmail
                                : dirMgrEmail;
                        break;
                }

                if (tableTab == GridTabType.Attachments)
                {
                    WF_QaRcrdCtrl_QaDIR.LoginToDirPage(currentUser);
                }

                string[] columnFilterValues = new string[] { dirNumber, sentBy, lockedBy, dirRev };
                IterateColumnsByFilter(tableTab, currentUser, columnFilterValues, useQaFieldMenu);

                if (tableTab != GridTabType.To_Be_Closed)
                {
                    GridHelper.ClickEditBtnForRow();
                }

                if (tableTab == GridTabType.Creating || tableTab == GridTabType.Create_Revise)
                {
                    QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
                }
                else if (tableTab == GridTabType.QC_Review)
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
                else if (tableTab == GridTabType.Authorization)
                {
                    WF_QaRcrdCtrl_QaDIR.ClickBtn_KickBackOrRevise();
                    QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
                    QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
                }
                else if (tableTab == GridTabType.Attachments)
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
                else if (tableTab == GridTabType.Revise)
                {
                    QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
                }
                else if (tableTab == GridTabType.To_Be_Closed)
                {
                    bool isDisplayedInClosed = QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.To_Be_Closed, dirNumber);

                    if (isDisplayedInClosed)
                    {
                        GridHelper.SelectCheckboxForRow(dirNumber);
                        QaRcrdCtrl_QaDIR.ClickBtn_Close_Selected();
                    }

                    AddAssertionToList(isDisplayedInClosed, $"Verify DIR ({dirNumber}) is displayed in Closed Tab");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
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
        internal void IterateColumnsByFilter(GridTabType tableTab, UserType currentUser, string[] columnFilterValues, bool useQaFieldMenu = false)
        {
            try
            {
                if (tableTab != GridTabType.Create_Packages || tableTab != GridTabType.Packages)
                {
                    string dirNumber = columnFilterValues[0];
                    string sentBy = columnFilterValues[1];
                    string lockedBy = columnFilterValues[2];
                    string dirRev = columnFilterValues[3];
                    string dirTechEmail = "testDirTech@rkci.com";

                    Report.Step($"---> IterateColumnsByFilter <---<br>TableTab {tableTab.ToString()}<br>DIR# {dirNumber}<br>SentBy {sentBy}<br>LockedBy {lockedBy}<br>DIR Rev {dirRev}<br>Use Qa Field Menu {useQaFieldMenu}");

                    AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber), $"\nIterateColumnsByFilter VerifyDirIsDisplayed {tableTab.ToString()}, before DIR Locked");

                    if (tableTab == GridTabType.To_Be_Closed)
                    {
                        GridHelper.ClickEditBtnForRow(dirNumber, true, true);
                    }
                    else
                    {
                        GridHelper.ClickEditBtnForRow();
                    }

                    WF_QaRcrdCtrl_QaDIR.LoginToDirPage(currentUser, useQaFieldMenu); //locks DIR
                    AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber), $"IterateColumnsByFilter VerifyDirIsDisplayed {tableTab.ToString()}, after DIR Locked");
                    GridHelper.ClearTableFilters();
                    AddAssertionToList(true, $"#### IterateColumnsByFilter ####\nTableTab : {tableTab.ToString()}, Use QAField Menu : {useQaFieldMenu}, DIR# : {dirNumber}, SentBy : {sentBy}, LockedBy : {lockedBy}, DIR Rev : {dirRev}");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnNameType.Revision, dirRev), $"IterateColumnsByFilter Column Revision");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnNameType.Created_By, dirTechEmail), $"IterateColumnsByFilter Column Created_By");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnNameType.Sent_By, sentBy, TableType.MultiTab, false, FilterOperator.IsAfterOrEqualTo), $"IterateColumnsByFilter Column Sent_By");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnNameType.Sent_Date, GetShortDate(), TableType.MultiTab, false, Page.FilterOperator.IsAfterOrEqualTo), $"IterateColumnsByFilter Column Sent_Date");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnNameType.Locked_By, lockedBy), $"IterateColumnsByFilter Column Locked_By");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(ColumnNameType.Locked_Date, GetShortDate(), TableType.MultiTab, false, Page.FilterOperator.IsAfterOrEqualTo), $"IterateColumnsByFilter Column Locked Date");
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
                    PackagesColumnNameType newDirCountOrPkgNumberCol = PackagesColumnNameType.Package_Number;
                    PackagesColumnNameType newDIRsOrDIRsCol = PackagesColumnNameType.DIRs;

                    if (tableTab == GridTabType.Create_Packages)
                    {
                        column3Name = "New DIR Count";
                        column4Name = "New DIRs";
                        newDirCountOrPkgNumberCol = PackagesColumnNameType.New_DIR_Count;
                        newDIRsOrDIRsCol = PackagesColumnNameType.New_DIRs;
                    }

                    if (newDIRsOrDIRs.Contains(","))
                    {
                        string[] splitNewDIRs = new string[] { };
                        splitNewDIRs = Regex.Split(newDIRsOrDIRs, ", ");
                        newDIRsOrDIRs = splitNewDIRs[0];
                    }

                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(PackagesColumnNameType.Week_Start, wkStart), $"IterateColumnsByFilter Column Week Start");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(PackagesColumnNameType.Week_End, wkEnd), $"IterateColumnsByFilter Column Week End");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(newDirCountOrPkgNumberCol, newDirCountOrPkgNumber), $"IterateColumnsByFilter Column {column3Name}");
                    AddAssertionToList(GridHelper.VerifyRecordIsDisplayed(newDIRsOrDIRsCol, newDIRsOrDIRs), $"IterateColumnsByFilter Column {column4Name}");
                }

                int totalRows = GridHelper.GetTableRowCount();
                bool filteredDownToSingleRow = totalRows.Equals(1);

                string logMsg = filteredDownToSingleRow
                    ? "Filtered table down to single row as expected"
                    : $"Expected To Filter Table to 1 Row, but found {totalRows.ToString()} rows";
                AddAssertionToList(filteredDownToSingleRow, $"IterateColumnsByFilter - Total Number of Rows after Filter Equals 1");
                bool[] results = new bool[] { filteredDownToSingleRow, totalRows >= 1 };
                Report.Info(logMsg, results);
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
                throw;
            }
        }

        internal bool Verify_DirRevision_inTblRow_then_Approve(GridTabType tableTab, string dirNumber, string expectedDirRev)
        {
            Report.Step($"---> Verify_DirRevision_inTblRow_then_Approve in {tableTab.ToString()} - Expected DIR Rev: {expectedDirRev} <---");

            bool dirRevMatches = false;
            string ifFalseLog = string.Empty;
            bool isDisplayed = false;

            try
            {
                if (string.IsNullOrEmpty(expectedDirRev))
                {
                    bool dirRevFound = GridHelper.VerifyRecordIsDisplayed(QADIRs.ColumnNameType.Revision, expectedDirRev);
                    AddAssertionToList(dirRevFound, $"VerifyRecordIsDisplayed - DIR Revision: {expectedDirRev}");
                    Report.Info($"Successfully filtered table by expected DIR Revision: {expectedDirRev}", dirRevFound);
                }

                isDisplayed = (tableTab == GridTabType.Creating || tableTab == GridTabType.Create_Revise)
                    ? WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber)
                    : QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber, false);

                string actualDirRev = isDisplayed ? GridHelper.GetColumnValueForRow(dirNumber, "Revision") : "DIR Not Found";
                dirRevMatches = actualDirRev.Equals(expectedDirRev);
                ifFalseLog = dirRevMatches ? "" : $"<br>Actual DIR Rev: {actualDirRev}";

                if (isDisplayed)
                {
                    GridHelper.ClickEditBtnForRow();

                    if (tableTab == GridTabType.Creating || tableTab == GridTabType.Create_Revise)
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

            Report.Info($"Expected DIR Rev: {expectedDirRev}{ifFalseLog}<br>Actual Matches Expected Rev? : {dirRevMatches}", dirRevMatches);
            return dirRevMatches;
        }

        internal string[] GetDirIdForRow<T>(T textInColumnForRowOrRowIndex)
        {
            string href = GridHelper.GetPdfHref(textInColumnForRowOrRowIndex, true, true);
            string[] dirID = Regex.Split(href, "ViewDirPDF\\?DirId=");
            return dirID;
        }

        internal bool Verify_ViewDirPDF(GridTabType tableTab, string dirNumber = "")
        {
            QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber);
            return GridHelper.VerifyViewPdfReport(dirNumber);
        }

        public override string ClickViewSelectedDirPDFs(bool selectNoneForMultiView)
        {
            string expectedUrl = "No Checkboxes Selected (Expected URL is Empty)";
            int rowCount = 0;

            try
            {
                if (!selectNoneForMultiView)
                {
                    rowCount = GridHelper.GetTableRowCount();
                    rowCount = rowCount > 3 ? 3 : rowCount;

                    expectedUrl = $"{GetDirIdForRow(1)[0]}ViewMultiDirPDF?";

                    for (int i = 0; i < rowCount; i++)
                    {
                        int rowIndex = i + 1;
                        string dirID = $"DirIds[{i}]={GetDirIdForRow(rowIndex)[1]}&";
                        expectedUrl = $"{expectedUrl}{dirID}";
                        GridHelper.SelectCheckboxForRow(rowIndex);
                    }

                    expectedUrl = expectedUrl.TrimEnd('&');
                }

                QaRcrdCtrl_QaDIR.ClickBtn_View_Selected();

                
                Report.Step(
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

        public override bool VerifyDbCleanupForDIR(string dirNumber, string dirRevision = "A", bool setAsDeleted = true)
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
                Report.Step($"Performed DB Cleanup for DIR# {dirNumber}", cleanupSuccessful);
            }
            catch (Exception e)
            {
                log.Warn($"Error occurred during VerifyDbCleanupForDIR : {e.Message}");
            }

            return cleanupSuccessful;
        }

        public override void VerifyDbCleanupForCreatePackages(bool isPkgCreated, string weekStartDate, string[] dirNumbers)
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

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Closed);

        public override bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Closed, true, expectedRevision);

        public override void LoginToDirPage(UserType userType, bool useQaFieldMenu = false)
        {
            bool QaFieldDIR = false;
            string expectedPageTitle = "List of Inspector's Daily Report"; //(default) QcRecordControl page title
            string logMsg = useQaFieldMenu ? "QaField" : "RecordControl";
            Report.Step($"---> LoginToDirPage : {logMsg} <---");

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

        public override void LoginToQaFieldDirPage(UserType userType)
            => LoginToDirPage(userType, true);

        public override void LoginToRcrdCtrlDirPage(UserType userType)
            => LoginToDirPage(userType);

        //SH249, SG, Garnet - Creating
        //LAX, I15Tech, I15SB, GLX - Create/Revise
        public override void ClickTab_Create() => QaRcrdCtrl_QaDIR.ClickTab_Create_Revise();

        //GLX, LAX, I15SB, I15Tech
        public override bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.Create_Revise, dirNumber);

        public override bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.Create_Revise, dirNumber);

        //GLX,
        public override string Create_and_SaveForward_DIR()
        {
            Report.Step($"Create_and_SaveForward_DIR", true);

            CreateNew_and_PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        public override string Create_and_SaveOnly_DIR()
        {
            Report.Step($"Create_and_SaveOnly_DIR", true);

            CreateNew_and_PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        public override string[] Create_and_SaveForward_DIR_with_Failed_Inspection_and_PreviousFailingReports()
        {
            Report.Step($"Create_and_SaveForward_Failed_Inspection_DIR_with_PreviousFailingReports", true);

            CreateNew_and_PopulateRequiredFields();
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            string previousFailedDirNumber = QaRcrdCtrl_QaDIR.CreatePreviousFailingReport();
            string dirNumber = QaRcrdCtrl_QaDIR.GetDirNumber();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInCreate(dirNumber), $"VerifyDirIsDisplayedInCreate DirNo. {dirNumber}");
            GridHelper.ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyPreviousFailingDirEntry(previousFailedDirNumber), $"VerifyPreviousFailingDirEntry in Create: {previousFailedDirNumber}");
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            string[] dirNumbers = new string[2]
                {
                    dirNumber,
                    previousFailedDirNumber
                };
            return dirNumbers;
        }

        public override string Create_DirRevision_For_Package_Recreate_ComplexWF_EndToEnd(string weekStartDate, string packageNumber, string[] dirNumbers)
        {
            //TODO - can't use weekStartDate as inspectDate - DIRs in package may not always be on weekStart date
            //TODO arg needs to be an existing DIR for TechID of DIRQA user
            QaRcrdCtrl_QaDIR.EnterText_InspectionDate(weekStartDate);
            QaRcrdCtrl_QaDIR.ClickBtn_CreateRevision();
            QaRcrdCtrl_QaDIR.SetDirNumber();
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();

            return QaRcrdCtrl_QaDIR.GetDirNumber();
        }

        public override void FilterRecreateColumnWithoutButtonAscending()
        {
            GridHelper.SortColumnAscending(PackagesColumnNameType.Week_Start);
            GridHelper.FilterTableColumnByValue(PackagesColumnNameType.Recreate, "false");
            GridHelper.FilterTableColumnByValue(PackagesColumnNameType.DIRs, QaRcrdCtrl_QaDIR.GetTechIdForDirUserAcct(true), TableType.MultiTab, FilterOperator.Contains);
        }

        public override void Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(GridTabType kickBackfromTableTab, string dirNumber)
        {
            Report.Step($"Workflow Step: KickBack_DIR_ForRevise_From{kickBackfromTableTab.ToString()}Tab_then_Edit_inCreateReview", true);
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(kickBackfromTableTab, dirNumber), "VerifyDirIsDisplayed");
            GridHelper.ClickEditBtnForRow();
            ClickBtn_KickBackOrRevise();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_SendEmailForRevise_No();
            QaRcrdCtrl_QaDIR.ClickBtn_SubmitRevise();
            WaitForPageReady();
            AddAssertionToList(VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            GridHelper.ClickEditBtnForRow();
        }

        public override void Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(string dirNumber)
            => WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(GridTabType.QC_Review, dirNumber);

        public override void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
        {
            WF_QaRcrdCtrl_QaDIR.Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(GridTabType.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_then_Approve_inReview(dirNumber);
        }

        private void Upload_Cancel_Verify_inAttachments(string dirNumber)
        {
            Report.Step($"Upload_Cancel_Verify_inAttachments", true);

            UploadFile();
        }

        public override void Modify_Cancel_Verify_inCreateRevise(string dirNumber)
        {
            Report.Step($"Modify_Cancel_Verify_inCreateRevise", true);

            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Cancel();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed");
            GridHelper.ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnCheckboxType.Inspection_Result_P), "VerifyChkBoxRdoBtnSelection Inspection_Result_P");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnCheckboxType.Deficiencies_No), "VerifyChkBoxRdoBtnSelection AnyDeficiencies_No");
            AddAssertionToList(VerifyTextAreaField(InputFieldType.Deficiency_Description, false), "VerifyTextAreaField Deficiency_Description - Should Be Empty");
        }

        public override void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber)
        {
            Report.Step($"---> Modify_Save_Verify_and_SaveForward_inCreateRevise <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDeficiencySelectionPopupMessages(), "VerifyDeficiencySelectionPopupMessages");
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_F();
            QaRcrdCtrl_QaDIR.SelectRdoBtn_Deficiencies_Yes();
            QaRcrdCtrl_QaDIR.EnterText_DeficiencyDescription();
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            GridHelper.ClickEditBtnForRow();
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnCheckboxType.Inspection_Result_F), "VerifyChkBoxRdoBtnSelection(Inspection_Result_F)");
            AddAssertionToList(VerifyChkBoxRdoBtnSelection(RadioBtnCheckboxType.Deficiencies_Yes), "VerifyChkBoxRdoBtnSelection(Deficiencies_Yes)");
            AddAssertionToList(VerifyTextAreaField(InputFieldType.Deficiency_Description), "VerifyTextAreaField(Deficiency_Description)");
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
        }

        public override void LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr(string dirNumber)
        {
            Report.Step($"---> LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr <---");

            LogoutToLoginPage();
            LoginAs(UserType.DIRTechQA);
            NavigateToPage.QAField_QA_DIRs();

            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInRevise(dirNumber), "VerifyDirIsDisplayed(TableTab.Create_Revise)");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.SelectChkbox_InspectionResult_E(false); //Edit 'Results' checkbox in Revise
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();

            LogoutToLoginPage();
            LoginAs(UserType.DIRMgrQA);
            NavigateToPage.QAField_QA_DIRs();
        }

        //GLX, I15SB, I15Tech, LAX
        public override void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            Report.Step($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            GridHelper.ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFieldType.Engineer_Comments));
            //WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
            //AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            WF_QaRcrdCtrl_QaDIR.ClickBtn_ApproveOrNoError();
        }

        public override void Verify_DIR_then_Approve_inReview(string dirNumber)
            => Verify_DIR_then_Approve(GridTabType.QC_Review, dirNumber);

        public override void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
            => Verify_DIR_then_Approve(GridTabType.Authorization, dirNumber);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inQcReview(string dirNumber, bool delete = false)
            => DIR_DeleteOrApproveNoError(GridTabType.QC_Review, dirNumber, delete, true);

        //SimpleWF tenants
        public override bool Verify_DIR_Delete_or_ApproveNoError_inAuthorization(string dirNumber, bool delete = false, bool approveDIR = false)
            => DIR_DeleteOrApproveNoError(GridTabType.Authorization, dirNumber, delete, approveDIR);

        public override void ClickBtn_ApproveOrNoError()
            => QaRcrdCtrl_QaDIR.ClickBtn_Approve();

        public override void ClickBtn_KickBackOrRevise()
            => QaRcrdCtrl_QaDIR.ClickBtn_KickBack();

        public override bool Verify_DIR_Delete(GridTabType tableTab, string dirNumber, bool acceptAlert = true)
        {
            Report.Step($"---> Verify_DIR_Delete in {tableTab.ToString()} - Accept Alert: {acceptAlert} <---");

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
                    try
                    {
                        GridHelper.ClickDeleteBtnForRow();

                        if (acceptAlert)
                        {
                            actionPerformed = "Accepted";
                            resultAfterAction = "Displayed After Accepting Delete Dialog: ";
                            try
                            {
                                PageAction.AcceptAlertMessage();
                                Thread.Sleep(3000);
                                PageAction.AcceptAlertMessage();
                            }
                            catch (UnhandledAlertException ex)
                            {
                                log.Debug(ex.Message);
                            }
                        }
                        else
                        {
                            actionPerformed = "Dismissed";
                            resultAfterAction = "Displayed After Dismissing Delete Dialog: ";
                            try
                            {
                                PageAction.DismissAlertMessage();
                                Thread.Sleep(3000);
                                PageAction.DismissAlertMessage();
                            }
                            catch (UnhandledAlertException ex)
                            {
                                log.Debug(ex.Message);
                            }
                        }
                    }
                    catch (UnhandledAlertException)
                    {
                        Thread.Sleep(3000);
                    }
                    finally
                    {
                        WaitForPageReady();

                        var verifyResult = QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(tableTab, dirNumber, acceptAlert);

                        if (acceptAlert)
                        {
                            result = !verifyResult;
                        }
                        else
                        {
                            result = verifyResult;
                        }

                        AddAssertionToList(result, $"VerifyDirIsDisplayed in {tableTab.ToString()}, after {actionPerformed} delete dialog");
                        Report.Info($"Performed Action: {actionPerformed} delete dialog<br>{resultAfterAction}{isDisplayed}", result);
                    }
                }
                else
                {
                    Report.Error($"Unable to find DIR No. {dirNumber} in {tableTab.ToString()} Tab");
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
            }

            return result;
        }

        public override bool Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise(string dirNumber, string expectedDirRev)
            => Verify_DirRevision_inTblRow_then_Approve(GridTabType.Create_Revise, dirNumber, expectedDirRev);

        public override bool Verify_DirRevision_inTblRow_then_Approve_inQcReview(string dirNumber, string expectedDirRev)
            => Verify_DirRevision_inTblRow_then_Approve(GridTabType.QC_Review, dirNumber, expectedDirRev);

        public override bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev)
            => Verify_DirRevision_inTblRow_then_Approve(GridTabType.Authorization, dirNumber, expectedDirRev);

        public override void Edit_DIR_inCreate_and_Verify_AutoSaveTimerRefresh_then_Save(string dirNumber)
        {
            Report.Step("---> Edit_DIR_inCreate_and_Verify_AutoSaveTimerRefresh_then_Save <---");

            AddAssertionToList(WF_QaRcrdCtrl_QaDIR.VerifyDirIsDisplayedInCreate(dirNumber), "VerifyDirIsDisplayedInCreate (Verify AutoSaveTimer Refresh)");
            GridHelper.ClickEditBtnForRow();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyAutoSaveTimerRefresh(), "VerifyAutoSaveTimerRefresh");
            QaRcrdCtrl_QaDIR.ClickBtn_Save();
        }

        public override void RefreshAutoSaveTimer()
            => QaRcrdCtrl_QaDIR.ClickBtn_Refresh();

        public override void IterateColumnsByFilterInRevise(UserType currentUser, string dirNumber, string sentBy, string lockedBy, string dirRev, bool useQaFieldMenu = false)
            => IterateColumnsByFilter(GridTabType.Create_Revise, currentUser, new string[] { dirNumber, sentBy, lockedBy, dirRev }, useQaFieldMenu);

        //GLX, LAX, I15SB, I15Tech
        public override void Verify_Column_Filter_In_Create(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.Create_Revise, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void Verify_Column_Filter_In_QcReview(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.QC_Review, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void Verify_Column_Filter_In_Authorization(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.Authorization, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void Verify_Column_Filter_In_Revise(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.Create_Revise, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        //SG
        public override void Verify_Column_Filter_ForTabs_In_QaFieldMenu(UserType currentUser, string dirNumber, string dirRev = "A")
        {
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_QcReview(currentUser, dirNumber, dirRev, false, true); //kickback in SH249
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Authorization(currentUser, dirNumber, dirRev, true, true); //kickback in SGWay
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Revise(currentUser, dirNumber, dirRev, true, true); //SaveFwd
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inQcReview(dirNumber, false);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inAuthorization(dirNumber, false, true);
        }

        //SG & SH249
        public override void Verify_Column_Filter_In_Attachments(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.Attachments, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void Verify_Column_Filter_In_ToBeClosed(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.To_Be_Closed, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        //GLX, LAX, I15SB, I15Tech
        public override bool Verify_ViewReport_forDIR_inCreate(string dirNumber)
            => Verify_ViewDirPDF(GridTabType.Create_Revise, dirNumber);

        public override bool Verify_ViewReport_forDIR_inRevise(string dirNumber)
            => Verify_ViewDirPDF(GridTabType.Create_Revise, dirNumber);

        public override bool Verify_ViewReport_forDIR_inQcReview(string dirNumber)
            => Verify_ViewDirPDF(GridTabType.QC_Review, dirNumber);

        public override bool Verify_ViewReport_forDIR_inAuthorization(string dirNumber)
            => Verify_ViewDirPDF(GridTabType.Authorization, dirNumber);

        public override bool Verify_ViewMultiDirPDF(bool selectNoneForMultiView = false)
            => GridHelper.VerifyViewPdfReport("", true, selectNoneForMultiView, ClickViewSelectedDirPDFs(selectNoneForMultiView));
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

        void Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(GridTabType kickBackfromTableTab, string dirNumber);

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

        bool Verify_DIR_Delete(GridTabType tableTab, string dirNumber, bool acceptAlert = true);

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

        string ClickViewSelectedDirPDFs(bool selectNoneForMultiView);

        bool VerifyDbCleanupForDIR(string dirnumber, string revision = "A", bool setAsDeleted = true);

        void VerifyDbCleanupForCreatePackages(bool isPkgCreated, string weekStartDate, string[] dirNumbers);
    }

    public abstract class QaRcrdCtrl_QaDIR_WF_Impl : TestBase, IQaRcrdCtrl_QaDIR_WF
    {
        public abstract bool VerifyDbCleanupForDIR(string dirnumber, string revision = "A", bool setAsDeleted = true);
        public abstract void VerifyDbCleanupForCreatePackages(bool isPkgCreated, string weekStartDate, string[] dirNumbers);
        public abstract string ClickViewSelectedDirPDFs(bool selectNoneForMultiView);
        public abstract void LoginToDirPage(UserType userType, bool useQaFieldMenu = false);
        public abstract void LoginToQaFieldDirPage(UserType userType);
        public abstract void LoginToRcrdCtrlDirPage(UserType userType);
        public abstract void ClickTab_Create();
        public abstract string Create_and_SaveForward_DIR();
        public abstract string Create_and_SaveOnly_DIR();
        public abstract string Create_DirRevision_For_Package_Recreate_ComplexWF_EndToEnd(string weekStartDate, string packageNumber, string[] dirNumbers);
        public abstract void FilterRecreateColumnWithoutButtonAscending();
        public abstract string[] Create_and_SaveForward_DIR_with_Failed_Inspection_and_PreviousFailingReports();
        public abstract void Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(GridTabType kickBackfromTableTab, string dirNumber);
        public abstract void Return_DIR_ForRevise_FromQcReview_then_Edit_SaveForward(string dirNumber);
        public abstract void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber);
        public abstract void Modify_Cancel_Verify_inCreateRevise(string dirNumber);
        public abstract void Modify_Save_Verify_and_SaveForward_inCreateRevise(string dirNumber);
        public abstract void LogoutLoginAsQaTech_Edit_Result_SaveForward_then_LogoutLoginAsQaMgr(string dirNumber);
        public abstract void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber);
        public abstract void Verify_DIR_then_Approve_inReview(string dirNumber);
        public abstract void Verify_DIR_then_Approve_inAuthorization(string dirNumber);
        public abstract bool VerifyDirIsDisplayedInCreate(string dirNumber);
        public abstract bool VerifyDirIsDisplayedInRevise(string dirNumber);
        public abstract bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber);
        public abstract bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision);
        public abstract bool Verify_DIR_Delete(GridTabType tableTab, string dirNumber, bool acceptAlert = true);
        public abstract void Verify_Column_Filter_ForTabs_In_QaFieldMenu(UserType currentUser, string dirNumber, string dirRev = "A");
        public abstract bool Verify_DIR_Delete_or_ApproveNoError_inQcReview(string dirNumber, bool delete = false);
        public abstract bool Verify_DIR_Delete_or_ApproveNoError_inAuthorization(string dirNumber, bool delete = false, bool approveDIR = true);
        public abstract void ClickBtn_ApproveOrNoError();
        public abstract void ClickBtn_KickBackOrRevise();
        public abstract bool Verify_DirRevision_inTblRow_then_SaveForward_inCreateRevise(string dirNumber, string expectedDirRev);
        public abstract bool Verify_DirRevision_inTblRow_then_Approve_inQcReview(string dirNumber, string expectedDirRev);
        public abstract bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev);
        public abstract void Edit_DIR_inCreate_and_Verify_AutoSaveTimerRefresh_then_Save(string dirNumber);
        public abstract void RefreshAutoSaveTimer();
        public abstract void IterateColumnsByFilterInRevise(UserType currentUser, string dirNumber, string sentBy, string lockedBy, string dirRev, bool useQaFieldMenu = false);
        public abstract void Verify_Column_Filter_In_Create(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);
        public abstract void Verify_Column_Filter_In_Revise(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);
        public abstract void Verify_Column_Filter_In_QcReview(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);
        public abstract void Verify_Column_Filter_In_Authorization(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);
        public abstract void Verify_Column_Filter_In_Attachments(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);
        public abstract void Verify_Column_Filter_In_ToBeClosed(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false);
        public abstract bool Verify_ViewReport_forDIR_inCreate(string dirNumber);
        public abstract bool Verify_ViewReport_forDIR_inRevise(string dirNumber);
        public abstract bool Verify_ViewReport_forDIR_inQcReview(string dirNumber);
        public abstract bool Verify_ViewReport_forDIR_inAuthorization(string dirNumber);
        public abstract bool Verify_ViewMultiDirPDF(bool selectNoneForMultiView = false);
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
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.Revise, dirNumber);

        public override bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.Creating, dirNumber);

        public override void Verify_Column_Filter_In_Revise(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.Revise, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void Verify_Column_Filter_In_Create(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.Creating, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void IterateColumnsByFilterInRevise(UserType currentUser, string dirNumber, string sentBy, string lockedBy, string dirRev, bool useQaFieldMenu = false)
            => IterateColumnsByFilter(GridTabType.Revise, currentUser, new string[] { dirNumber, sentBy, lockedBy, dirRev }, useQaFieldMenu);

        public override void Verify_Column_Filter_In_Authorization(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => Report.Step("---> Verify_Column_Filter_In_Authorization <---<br>Step skipped for Tenant SH249");

        public override void Verify_Column_Filter_ForTabs_In_QaFieldMenu(UserType currentUser, string dirNumber, string dirRev = "A")
        {
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_QcReview(currentUser, dirNumber, dirRev, true, true);
            WF_QaRcrdCtrl_QaDIR.Verify_Column_Filter_In_Revise(currentUser, dirNumber, dirRev, true, true);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inQcReview(dirNumber, false);
            WF_QaRcrdCtrl_QaDIR.Verify_DIR_Delete_or_ApproveNoError_inAuthorization(dirNumber, false, true);
        }

        public override void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
            => Report.Step("---> KickBack_DIR_ForRevise_FromAuthorization_then_Edit <---<br>Step skipped for Tenant SH249");

        public override void Verify_DIR_then_Approve_inAuthorization(string dirNumber)
            => Report.Step("Step skipped for Tenant SH249: Verify_DIR_then_Approve_inAuthorization");

        public override bool Verify_DirRevision_inTblRow_then_Approve_inAuthorization(string dirNumber, string expectedDirRev)
        {
            Report.Step("---> Verify_DirRevision_inTblRow_then_Approve_inAuthorization <---<br>Step skipped for Tenant SH249");
            return true;
        }

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment);

        public override bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment, true, expectedRevision);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inQcReview(string dirNumber, bool delete = false)
            => DIR_DeleteOrApproveNoError(GridTabType.QC_Review, dirNumber, delete, true);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inAuthorization(string dirNumber, bool delete = false, bool approveDIR = true)
        {
            Report.Step($"---> Skipped Test Step for Tenant SH249<br>Verify_DIR_Delete_or_ApproveNoError_inAuthorization<---");
            return true;
        }

        public override bool Verify_ViewReport_forDIR_inAuthorization(string dirNumber)
        {
            Report.Step($"---> Skipped Test Step for Tenant SH249<br>Verify_ViewReport_forDIR_inAuthorization<---");
            return true;
        }

        public override void ClickBtn_ApproveOrNoError()
            => QaRcrdCtrl_QaDIR.ClickBtn_NoError();

        public override void ClickBtn_KickBackOrRevise()
            => QaRcrdCtrl_QaDIR.ClickBtn_Revise();

        public override void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            Report.Step($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            GridHelper.ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFieldType.Engineer_Comments));
            ClickBtn_ApproveOrNoError();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            ClickBtn_ApproveOrNoError();
        }

        public override bool Verify_ViewReport_forDIR_inCreate(string dirNumber)
            => Verify_ViewDirPDF(GridTabType.Creating, dirNumber);

        public override bool Verify_ViewReport_forDIR_inRevise(string dirNumber)
            => Verify_ViewDirPDF(GridTabType.Revise, dirNumber);
    }

    internal class QaRcrdCtrl_QaDIR_WF_SGWay : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void ClickTab_Create() => QaRcrdCtrl_QaDIR.ClickTab_Creating();

        public override bool VerifyDirIsDisplayedInRevise(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.Revise, dirNumber);

        public override bool VerifyDirIsDisplayedInCreate(string dirNumber)
            => QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.Creating, dirNumber);

        public override void Verify_Column_Filter_In_Revise(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.Revise, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void Verify_Column_Filter_In_Create(UserType currentUser, string dirNumber, string dirRev = "A", bool kickedBack = false, bool useQaFieldMenu = false)
            => VerifyColumnFilterInTab(GridTabType.Creating, currentUser, dirNumber, dirRev, kickedBack, useQaFieldMenu);

        public override void IterateColumnsByFilterInRevise(UserType currentUser, string dirNumber, string sentBy, string lockedBy, string dirRev, bool useQaFieldMenu = false)
            => IterateColumnsByFilter(GridTabType.Revise, currentUser, new string[] { dirNumber, sentBy, lockedBy, dirRev }, useQaFieldMenu);

        public override void Return_DIR_ForRevise_FromAuthorization_then_ForwardToAuthorization(string dirNumber)
        {
            Return_DIR_ForRevise_FromTab_then_Edit_inCreateRevise(GridTabType.Authorization, dirNumber);
            QaRcrdCtrl_QaDIR.ClickBtn_Save_Forward();
            Verify_DIR_then_Approve_inReview(dirNumber);

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.Authorization, dirNumber), "VerifyDirIsDisplayed");
            GridHelper.ClickEditBtnForRow();
            QaRcrdCtrl_QaDIR.ClickBtn_Back_To_QC_Review();
            Verify_DIR_then_Approve_inReview(dirNumber);
        }

        public override bool VerifyWorkflowLocationAfterSimpleWF(string dirNumber)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment);

        public override bool VerifyWorkflowLocationAfterSimpleWF_forDirRevision(string dirNumber, string expectedRevision)
            => QaSearch_DIR.VerifyDirWorkflowLocationByTblFilter(dirNumber, WorkflowLocation.Attachment, true, expectedRevision);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inQcReview(string dirNumber, bool delete = false)
            => DIR_DeleteOrApproveNoError(GridTabType.QC_Review, dirNumber, delete, true);

        public override bool Verify_DIR_Delete_or_ApproveNoError_inAuthorization(string dirNumber, bool delete = false, bool approveDIR = true)
            => DIR_DeleteOrApproveNoError(GridTabType.Authorization, dirNumber, delete, approveDIR);

        public override void ClickBtn_ApproveOrNoError()
            => QaRcrdCtrl_QaDIR.ClickBtn_NoError();

        public override void ClickBtn_KickBackOrRevise()
            => QaRcrdCtrl_QaDIR.ClickBtn_Revise();

        public override void Enter_EngineerComments_and_Approve_inQcReview(string dirNumber)
        {
            Report.Step($"---> Verify_EngineerComments_and_Approve_inQcReview <---");

            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyDirIsDisplayed(GridTabType.QC_Review, dirNumber), "VerifyDirIsDisplayed in QC Review");
            GridHelper.ClickEditBtnForRow();
            ClearText(GetTextAreaFieldByLocator(InputFieldType.Engineer_Comments));
            ClickBtn_ApproveOrNoError();
            AddAssertionToList(QaRcrdCtrl_QaDIR.VerifyEngineerCommentsReqFieldErrors(), "VerifyEngineerCommentsReqFieldErrors");
            QaRcrdCtrl_QaDIR.EnterText_EngineerComments();
            ClickBtn_ApproveOrNoError();
        }

        public override bool Verify_ViewReport_forDIR_inCreate(string dirNumber)
            => Verify_ViewDirPDF(GridTabType.Creating, dirNumber);

        public override bool Verify_ViewReport_forDIR_inRevise(string dirNumber)
            => Verify_ViewDirPDF(GridTabType.Revise, dirNumber);
    }

    internal class QaRcrdCtrl_QaDIR_WF_LAX : QaRcrdCtrl_QaDIR_WF
    {
        public QaRcrdCtrl_QaDIR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}