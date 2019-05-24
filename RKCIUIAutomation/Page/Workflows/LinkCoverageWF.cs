using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Page.PageObjects.QAField;
using RKCIUIAutomation.Test;
using System;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.Workflows
{
    #region LinkCoverage Generic class

    public class LinkCoverageWF : LinkCoverageWF_Impl
    {
        public LinkCoverageWF()
        {
        }

        public LinkCoverageWF(IWebDriver driver) => this.Driver = driver;

        /// <summary>
        /// Common pageObjects and workflows are inherited from abstract _Impl class
        /// </summary>
    }

    #endregion LinkCoverage Generic class

    #region Workflow Interface class

    public interface ILinkCoverageWF
    {
        void _NavigateToVerifyProjectConfigurationMenu();

        void _NavigateToVerifyQALabMenu();

        void _NavigateToOVMenu();

        void _NavigateToVerifyQARecordControlMenu();

        void _NavigateToVerifyQAEngineerMenu();

        void _NavigateToReportsAndNoticesMenu();

        void _NavigateToQASearchMenu();

        void _NavigateToQAFieldMenu();

        void _NavigateToControlPointMenu();

        void _NavigateToOwnerMenu();

        void _NavigateToMaterialMixCodeMenu();

        void _NavigateToRMCenterMenu();

        void _NavigateToRFIMenu();

        void _NavigateToVerifyQCLabMenu();

        void _NavigateToVerifyQCRecordControlMenu();

        void _NavigateToVerifyQCEngineerMenu();

        void _NavigateToQCSearchMenu();
    }

    #endregion Workflow Interface class

    #region Common Workflow Implementation class

    public abstract class LinkCoverageWF_Impl : TestBase, ILinkCoverageWF
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private ILinkCoverageWF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            ILinkCoverageWF instance = new LinkCoverageWF(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using LinkCoverage_SGWay instance ###### ");
                instance = new LinkCoverageWF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using LinkCoverage_SH249 instance ###### ");
                instance = new LinkCoverageWF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using LinkCoverage_Garnet instance ###### ");
                instance = new LinkCoverageWF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using LinkCoverage_GLX instance ###### ");
                instance = new LinkCoverageWF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using LinkCoverage_I15South instance ###### ");
                instance = new LinkCoverageWF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using LinkCoverage_I15Tech instance ###### ");
                instance = new LinkCoverageWF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using LinkCoverage_LAX instance ###### ");
                instance = new LinkCoverageWF_LAX(driver);
            }
            return instance;
        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public virtual void _NavigateToVerifyProjectConfigurationMenu()
        {
            Report.Step($"Project Configuration component test - This test should run");
            LoginAs(UserType.Bhoomi);

            NavigateToPage.My_Details();
            AddAssertionToList(VerifyPageHeader("Account Details"), "Verify Page Title : My_Details");
            //Qms Document - based on Tenant
            NavigateToPage.Qms_Document();
            AddAssertionToList(VerifyPageHeader("QMS Documents"), "Verify Page Title : Qms_Document");

            //Project>Administration
            NavigateToPage.Admin_Project_Details();
            AddAssertionToList(VerifyPageHeader("Project"), "Verify Page Title : Admin_Project_Details");
            NavigateToPage.Admin_Companies();
            AddAssertionToList(VerifyPageHeader("Companies"), "Verify Page Title : Admin_Companies");
            NavigateToPage.Admin_Contracts();
            AddAssertionToList(VerifyPageHeader("Contracts"), "Verify Page Title : Admin_Contracts");
            NavigateToPage.Admin_Menu_Editor();
            AddAssertionToList(VerifyPageHeader("Menu Editor"), "Verify Page Title : Admin_Menu_Editor");
            NavigateToPage.Admin_Suport();
            AddAssertionToList(VerifyPageHeader("Cache Management"), "Verify Page Title : Admin_Suport");

            //Project>Administration>User Management
            NavigateToPage.UserMgmt_Roles();
            AddAssertionToList(VerifyPageHeader("Roles"), "Verify Page Title : UserMgmt_Roles");
            NavigateToPage.UserMgmt_Users();
            AddAssertionToList(VerifyPageHeader("Users"), "Verify Page Title : UserMgmt_Users");
            NavigateToPage.UserMgmt_Access_Rights();
            AddAssertionToList(VerifyPageHeader("Access Rights"), "Verify Page Title : UserMgmt_Access_Rights");

            //Project>Administration>System Configuration
            NavigateToPage.SysConfig_Disciplines();
            AddAssertionToList(VerifyPageHeader("Disciplines"), "Verify Page Title : SysConfig_Disciplines");
            NavigateToPage.SysConfig_Submittal_Actions();
            AddAssertionToList(VerifyPageHeader("SubmittalActions"), "Verify Page Title : SysConfig_Submittal_Actions");
            NavigateToPage.SysConfig_Submittal_Requirements();
            AddAssertionToList(VerifyPageHeader("Submittal Requirments"), "Verify Page Title : SysConfig_Submittal_Requirements");
            NavigateToPage.SysConfig_Submittal_Types();
            AddAssertionToList(VerifyPageHeader("SubmittalTypes"), "Verify Page Title : SysConfig_Submittal_Types");
            NavigateToPage.SysConfig_CVL_Lists();
            AddAssertionToList(VerifyPageHeader("CVL List"), "Verify Page Title : SysConfig_CVL_Lists");
            NavigateToPage.SysConfig_CVL_List_Items();
            AddAssertionToList(VerifyPageHeader("CVL List Items"), "Verify Page Title : SysConfig_CVL_List_Items");
            NavigateToPage.SysConfig_Notifications();
            AddAssertionToList(VerifyPageHeader("Notifications"), "Verify Page Title : SysConfig_Notifications");
            NavigateToPage.SysConfig_Sieves();
            AddAssertionToList(VerifyPageHeader("Sieves"), "Verify Page Title : SysConfig_Sieves");
            NavigateToPage.SysConfig_Gradations();
            AddAssertionToList(VerifyPageHeader("Gradations"), "Verify Page Title : SysConfig_Gradations");

            //Project>Administration>System Configuration>Equipment
            NavigateToPage.SysConfig_Equipment_Makes();
            AddAssertionToList(VerifyPageHeader("Makes"), "Verify Page Title : SysConfig_Equipment_Makes");
            NavigateToPage.SysConfig_Equipment_Models();
            AddAssertionToList(VerifyPageHeader("EquipmentModels"), "Verify Page Title : SysConfig_Equipment_Models");
            NavigateToPage.SysConfig_Equipment_Types();
            AddAssertionToList(VerifyPageHeader("Equipment Types"), "Verify Page Title : SysConfig_Equipment_Types");

            //Project>Administration>System Configuration>Grade Management
            NavigateToPage.SysConfig_Grade_Types();
            AddAssertionToList(VerifyPageHeader("Grade Type"), "Verify Page Title : SysConfig_Grade_Types");
            NavigateToPage.SysConfig_Grades();
            AddAssertionToList(VerifyPageHeader("Grades"), "Verify Page Title : SysConfig_Grades");

            //Project>Administration>Admin Tools
            NavigateToPage.AdminTools_Locked_Records();
            AddAssertionToList(VerifyPageHeader("Locked Records"), "Verify Page Title : AdminTools_Locked_Records");
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToVerifyQALabMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QALab_Technician_Random();
            AddAssertionToList(VerifyPageHeader("Technician Random"));
            NavigateToPage.QALab_BreakSheet_Creation();
            AddAssertionToList(VerifyPageHeader("Create Break Sheet"));
            NavigateToPage.QALab_BreakSheet_Legacy();
            AddAssertionToList(VerifyPageHeader("Break Sheet Legacy"));
            NavigateToPage.QALab_Equipment_Management();
            AddAssertionToList(VerifyPageHeader("Equipment"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QALab_BreakSheet_Forecast();
            AddAssertionToList(VerifyPageHeader("Break Sheet Forecast"));
            NavigateToPage.QALab_Early_Break_Calendar();
            AddAssertionToList(VerifySchedulerIsDisplayed());

            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants:
        /// </summary>
        public virtual void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_QA_Test_Original_Report();
            AddAssertionToList(TestDetails.VerifyTestDetailsFormIsDisplayed());
            NavigateToPage.QARecordControl_QA_Test_All();
            AddAssertionToList(VerifyPageHeader("Lab Tests"));
            NavigateToPage.QARecordControl_QA_Test_Retest_Report();
            AddAssertionToList(VerifyPageHeader("Create Retest Report"));
            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(VerifyPageHeader("IQF Record Control > List of Daily Inspection Reports"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_General_NCR();
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertionToList(VerifyPageHeader("List of CDR Reports"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Retaining Wall Backfill Quantity Tracker"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Concrete Paving Quantity Tracker"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_MPL_Tracker();
            AddAssertionToList(VerifyPageHeader("MPL Tracker"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_Girder_Tracker();
            AddAssertionToList(VerifyPageHeader("Girder Tracker"));
            ClickNew();
            ClickCancel();
            AssertAll();
        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public virtual void _NavigateToVerifyQAEngineerMenu()
        {
            Report.Step($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QAEngineer_QA_Test_Lab_Supervisor_Review();
            AddAssertionToList(VerifyPageHeader("Lab Supervisor Review"));
            NavigateToPage.QAEngineer_QA_Test_Field_Supervisor_Review(); //not in 249
            AddAssertionToList(VerifyPageHeader(""));
            NavigateToPage.QAEngineer_QA_Test_Authorization();
            AddAssertionToList(VerifyPageHeader("Authorizations"));
            NavigateToPage.QAEngineer_DIR_QA_Review_Approval();
            AddAssertionToList(VerifyPageHeader("IQF Engineer > List of Daily Inspection Reports"));
            NavigateToPage.QA_Test_Proctor_Curve_Controller();
            AddAssertionToList(VerifyPageHeader("Proctor Curve Information"));
            AssertAll();
        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public virtual void _NavigateToReportsAndNoticesMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_NCR(); //TODO: Is this correct?
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_General_CDR(); //TODO: Is this correct?
            AddAssertionToList(VerifyPageHeader("List of CDR Reports"));
            ClickNew();
            ClickCancel();
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants:I15South, I15Tech, & SH249
        /// </summary>

        public virtual void _NavigateToOVMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.OV_Create_OV_Test();
            AddAssertionToList(TestDetails.VerifyTestDetailsFormIsDisplayed());
            NavigateToPage.OV_OV_Test();
            AddAssertionToList(VerifyPageHeader("OV Tests"));

            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToQASearchMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QASearch_QA_Tests();
            AddAssertionToList(VerifyPageHeader("Test Search"));
            NavigateToPage.QASearch_QA_Test_Summary_Search();
            AddAssertionToList(VerifyPageHeader("Test Summary Report Search"));
            NavigateToPage.QASearch_QA_Guide_Schedule_Summary_Report();
            AddAssertionToList(VerifyPageHeader("Guide Schedule of Sampling and Testing"));
            NavigateToPage.QASearch_Inspection_Deficiency_Log_Report();
            AddAssertionToList(VerifyPageHeader("Inspection Deficiency Log Search"));
            NavigateToPage.QASearch_Daily_Inspection_Report();
            AddAssertionToList(VerifyPageHeader("Daily Inspection Report Search"));
            NavigateToPage.QASearch_DIR_Summary_Report();
            AddAssertionToList(VerifyPageHeader("DIR Summary Report Search"));
            NavigateToPage.QASearch_DIR_Checklist_Search();
            AddAssertionToList(VerifyPageHeader("DIR Checklist Search"));
            NavigateToPage.QASearch_QMS_Document_Search();
            AddAssertionToList(VerifyPageHeader("QMS Documents Search"));
            NavigateToPage.Environmental_Document_Search();//not in 249
            AddAssertionToList(VerifyPageHeader(""));
            NavigateToPage.QAQO_Test_Proctor_Curve_Report();
            AddAssertionToList(VerifyPageHeader("ProctorCurveSummary"));
            NavigateToPage.QAQO_Test_Proctor_Curve_Summary();
            AddAssertionToList(VerifyPageHeader("ProctorCurveReport"));
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToQAFieldMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QAField_QA_Test();
            AddAssertionToList(VerifyPageHeader("Field Tests"));
            //ClickCreate(); TODO - Create button is not present in page
            //ClickCancel();

            NavigateToPage.QAField_QA_DIRs();
            AddAssertionToList(VerifyPageHeader("IQF Field > List of Daily Inspection Reports"));
            //ClickCreate(); //TODO - Error Msg - "Invalid Technician ID for DIR No."
            //ClickCancel();

            // NavigateToPage.QAField_QA_Technician_Random_Search();//page not implemented
            // AddAssertionToList(VerifyPageTitle("");

            NavigateToPage.QAField_Weekly_Environmental_Monitoring();
            AddAssertionToList(VerifyPageHeader("Week Environmental Monitoring Reports"));
            ClickNew();
            AddAssertionToList(VerifyAlertMessage("Week Ending Date is required!"));
            AcceptAlertMessage();
            EnterText(WeeklyEnvMonitoring.WeekEndingDateField, DateTime.Now.ToShortDateString()); //TODO - Create method
            ClickNew();
            ClickCancel();

            NavigateToPage.QAField_Daily_Environmental_Inspection();
            AddAssertionToList(VerifyPageHeader("List of Daily Environmental Inspection Reports"));
            ClickNew();
            AddAssertionToList(VerifyActiveModalTitle("DEI Creation"));
            CloseActiveModalWindow();

            NavigateToPage.QAField_Weekly_Environmental_Inspection();
            AddAssertionToList(VerifyPageHeader("List of Weekly Environmental Inspection Reportse"));
            ClickNew();
            AddAssertionToList(VerifyActiveModalTitle("Weekly Environmental Inspection Creation"));
            CloseActiveModalWindow();

            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToControlPointMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Control_Point_Scheduler();
            AddAssertionToList(VerifyPageHeader("Control Point List"));
            ClickNew();
            ClickCancel();
            NavigateToPage.Control_Point_Log();
            AddAssertionToList(VerifyPageHeader("Control Point Log Search"));
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToOwnerMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Owner_DIRs();
            AddAssertionToList(VerifyPageHeader("DOT > List of Daily Inspection Reports"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.Owner_NCRs();
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"));
            ClickCreate();
            ClickCancel();
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToMaterialMixCodeMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.MaterialMixCodes_Mix_Design_PCC();
            AddAssertionToList(VerifyPageHeader("Pcc Mix Design"));
            ClickNew();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Mix_Design_HMA();
            AddAssertionToList(VerifyPageHeader("Hma Mix Design"));
            VerifyPageIsLoaded();
            ClickNew();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Sieve_Analyses_JMF();
            AddAssertionToList(VerifyPageHeader("Sieve Analysis JMF"));
            ClickNew();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Sieve_Analyses_IOC();
            AddAssertionToList(VerifyPageHeader("Sieve Analysis IOC"));
            ClickNew();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Material_Code_Concrete_Aggregate();
            AddAssertionToList(VerifyPageHeader("Aggregate A"));
            ClickNew();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Material_Code_Base_Aggregate();
            AddAssertionToList(VerifyPageHeader("Aggregate E"));
            ClickNew();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Material_Code_HMA_Aggregate();
            AddAssertionToList(VerifyPageHeader("Aggregate F"));
            ClickNew();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Material_Code_Raw_Material();
            AddAssertionToList(VerifyPageHeader("Raw Materials"));
            ClickNew();
            ClickCancel();
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX
        /// </summary>
        public virtual void _NavigateToRFIMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.RFI_Create();
            AddAssertionToList(VerifyPageHeader("Request For Information"));
            NavigateToPage.RFI_List();
            //TODO: verify if test should fail if no title is seen on the page
            AddAssertionToList(VerifyPageHeader(""));//dont see title
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX only
        /// </summary>
        public virtual void _NavigateToVerifyQCLabMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QCLab_BreakSheet_Creation();
            AddAssertionToList(VerifyPageHeader("Create Break Sheet"));
            NavigateToPage.QCLab_BreakSheet_Legacy();
            AddAssertionToList(VerifyPageHeader("Break Sheet Legacy"));
            NavigateToPage.QCLab_Equipment_Management();
            AddAssertionToList(VerifyPageHeader("Equipment"));
            ClickCreate();
            ClickCancel();
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX only
        /// </summary>
        public virtual void _NavigateToVerifyQCRecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QCRecordControl_QC_Test_Original_Report();
            AddAssertionToList(TestDetails.VerifyTestDetailsFormIsDisplayed());
            NavigateToPage.QCRecordControl_QC_Test_All();
            AddAssertionToList(VerifyPageHeader("Lab Tests"));
            NavigateToPage.QCRecordControl_QC_Test_Correction_Report();
            AddAssertionToList(VerifyPageHeader("Create Correction Test Report"));
            NavigateToPage.QCRecordControl_QC_DIRs();
            AddAssertionToList(VerifyPageHeader("List of Inspector's Daily Report"));
            ClickNew();
            ClickCancel();
            AssertAll();
        }

        /// <summary>
        /// TODO - implement common workflow: GLX only
        /// </summary>
        public virtual void _NavigateToVerifyQCEngineerMenu()
        {
            Report.Step($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QCEngineer_QC_Test_Lab_Supervisor_Review();
            AddAssertionToList(VerifyPageHeader("Lab Supervisor Review"));
            NavigateToPage.QCEngineer_QC_Test_Authorization();
            AddAssertionToList(VerifyPageHeader("Authorizations"));

            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX only
        /// </summary>
        public virtual void _NavigateToQCSearchMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QCSearch_QC_Tests_Search();
            AddAssertionToList(VerifyPageHeader("Test Search"));
            NavigateToPage.QASearch_QA_Test_Summary_Search();
            AddAssertionToList(VerifyPageHeader("Test Summary Report Search"));
            NavigateToPage.QCSearch_Daily_Inspection_Report();
            AddAssertionToList(VerifyPageHeader("Daily Inspection Report Search"));
            NavigateToPage.QCSearch_DIR_Summary_Report();
            AddAssertionToList(VerifyPageHeader("DIR Summary Report Search"));

            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, SGWay, & SH249
        /// </summary>
        public virtual void _NavigateToRMCenterMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.RMCenter_Search();
            AddAssertionToList(VerifyPageHeader("RM Center Search"));
            NavigateToPage.RMCenter_Design_Documents();
            AddAssertionToList(VerifyPageHeader("Design Document"));
            // ClickCreate();
            //ClickCancel();
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            AddAssertionToList(VerifyPageHeader("Submittal Details"));
            ClickCancel();
            NavigateToPage.RMCenter_Upload_Owner_Submittal();
            AddAssertionToList(VerifyPageHeader("New Submittal"));
            ClickCancel();
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
            AddAssertionToList(VerifyPageHeader("Submittal Details"));
            ClickCancel();
            NavigateToPage.RMCenter_DOT_Project_Correspondence_Log();
            AddAssertionToList(VerifyPageHeader("Transmissions"));
            ClickNew();
            ClickCancel();
            NavigateToPage.RMCenter_Review_Revise_Submittal();
            AddAssertionToList(VerifyPageHeader("Review / Revise Submittals"));
            NavigateToPage.RMCenter_RFC_Management();
            AddAssertionToList(VerifyPageHeader("RFC List"));
            NavigateToPage.RMCenter_Project_Correspondence_Log();
            AddAssertionToList(VerifyPageHeader("Transmissions"));
            ClickNew();
            ClickCancel();
            AssertAll();
        }
    }

    #endregion Common Workflow Implementation class

    /// <summary>
    /// Tenant specific implementation of LinkCoverage
    /// </summary>

    #region Implementation specific to Garnet

    public class LinkCoverageWF_Garnet : LinkCoverageWF
    {
        public LinkCoverageWF_Garnet(IWebDriver driver) : base(driver)
        {
        }

        public override void _NavigateToVerifyQALabMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QALab_Technician_Random();
            AddAssertionToList(VerifyPageHeader("Technician Random"));
            NavigateToPage.QALab_BreakSheet_Creation();
            AddAssertionToList(VerifyPageHeader("Create Break Sheet"));
            NavigateToPage.QALab_BreakSheet_Legacy();
            AddAssertionToList(VerifyPageHeader("Break Sheet Legacy"));
            NavigateToPage.QALab_Equipment_Management();
            AddAssertionToList(VerifyPageHeader("Equipment"));
            NavigateToPage.QALab_BreakSheet_Forecast();
            AddAssertionToList(VerifyPageHeader("Break Sheet Forecast"));

            AssertAll();
        }
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to GLX

    public class LinkCoverageWF_GLX : LinkCoverageWF
    {
        public LinkCoverageWF_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to GLX

    #region Implementation specific to SH249

    public class LinkCoverageWF_SH249 : LinkCoverageWF
    {
        public LinkCoverageWF_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QARecordControl_QA_Test_Original_Report();
            AddAssertionToList(TestDetails.VerifyTestDetailsFormIsDisplayed());
            NavigateToPage.QARecordControl_QA_Test_Correction_Report();
            AddAssertionToList(VerifyPageHeader("Create Correction Test Report"));
            NavigateToPage.QARecordControl_QA_Test_All();
            AddAssertionToList(VerifyPageHeader("Lab Tests"));
            NavigateToPage.QARecordControl_QA_Test_Retest_Report();
            AddAssertionToList(VerifyPageHeader("Create Retest Report"));
            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(VerifyPageHeader("IQF Record Control > List of Daily Inspection Reports"));
            NavigateToPage.QARecordControl_General_NCR();
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"));
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertionToList(VerifyPageHeader("List of CDR Reports"));
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Retaining Wall Backfill Quantity Tracker"));
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Concrete Paving Quantity Tracker"));
            NavigateToPage.QARecordControl_MPL_Tracker();
            AddAssertionToList(VerifyPageHeader("MPL Tracker"));
            NavigateToPage.QARecordControl_Girder_Tracker();
            AddAssertionToList(VerifyPageHeader("Girder Tracker"));

            AssertAll();
        }
    }

    #endregion Implementation specific to SH249

    #region Implementation specific to SGWay

    public class LinkCoverageWF_SGWay : LinkCoverageWF
    {
        public LinkCoverageWF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void _NavigateToVerifyQALabMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QALab_Technician_Random();
            AddAssertionToList(VerifyPageHeader("Technician Random"));
            NavigateToPage.QALab_BreakSheet_Creation();
            AddAssertionToList(VerifyPageHeader("Create Break Sheet"));
            NavigateToPage.QALab_BreakSheet_Legacy();
            AddAssertionToList(VerifyPageHeader("Break Sheet Legacy"));
            NavigateToPage.QALab_Equipment_Management();
            AddAssertionToList(VerifyPageHeader("Equipment"));
            NavigateToPage.QALab_BreakSheet_Forecast();
            AddAssertionToList(VerifyPageHeader("Break Sheet Forecast"));
            NavigateToPage.QALab_Cylinder_PickUp_List();
            AddAssertionToList(VerifyPageHeader("Cylinder Pick-Up Status:"));
            NavigateToPage.QALab_Early_Break_Calendar();
            AddAssertionToList(VerifySchedulerIsDisplayed());

            AssertAll();
        }

        public override void _NavigateToRMCenterMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.RMCenter_Search();
            AddAssertionToList(VerifyPageHeader("RM Center Search"));
            NavigateToPage.RMCenter_Design_Documents();
            AddAssertionToList(VerifyPageHeader("Design Document"));
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            AddAssertionToList(VerifyPageHeader("Submittal Details"));
            NavigateToPage.RMCenter_Upload_Owner_Submittal();
            AddAssertionToList(VerifyPageHeader("New Submittal"));
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
            AddAssertionToList(VerifyPageHeader("Submittal Details"));
            NavigateToPage.RMCenter_DOT_Project_Correspondence_Log();
            AddAssertionToList(VerifyPageHeader("Transmissions"));
            NavigateToPage.RMCenter_Review_Revise_Submittal();
            AddAssertionToList(VerifyPageHeader("Review / Revise Submittals"));
            NavigateToPage.RMCenter_RFC_Management();
            AddAssertionToList(VerifyPageHeader("RFC List"));
            NavigateToPage.RMCenter_Project_Correspondence_Log();
            AddAssertionToList(VerifyPageHeader("Transmissions"));
            NavigateToPage.RMCenter_Comment_Summary();
            AddAssertionToList(VerifyPageHeader("Comment Summary Search"));

            AssertAll();
        }

        public override void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QARecordControl_QA_Test_Original_Report();
            AddAssertionToList(TestDetails.VerifyTestDetailsFormIsDisplayed());
            NavigateToPage.QARecordControl_QA_Test_Correction_Report();
            AddAssertionToList(VerifyPageHeader("Create Correction Test Report"));
            NavigateToPage.QARecordControl_QA_Test_All();
            AddAssertionToList(VerifyPageHeader("Lab Tests"));
            NavigateToPage.QARecordControl_QA_Test_Retest_Report();
            AddAssertionToList(VerifyPageHeader("Create Retest Report"));
            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(VerifyPageHeader("IQF Record Control > List of Daily Inspection Reports"));
            NavigateToPage.QARecordControl_General_NCR();
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"));
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertionToList(VerifyPageHeader("List of CDR Reports"));
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Retaining Wall Backfill Quantity Tracker"));
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Concrete Paving Quantity Tracker"));
            NavigateToPage.QARecordControl_MPL_Tracker();
            AddAssertionToList(VerifyPageHeader("MPL Tracker"));
            NavigateToPage.QARecordControl_Girder_Tracker();
            AddAssertionToList(VerifyPageHeader("Girder Tracker"));
            NavigateToPage.Qms_Document();
            AddAssertionToList(VerifyPageHeader("QMS Documents"));
            NavigateToPage.QARecordControl_Environmental_Document();
            AddAssertionToList(VerifyPageHeader("Environmental Documents"));

            AssertAll();
        }
    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to I15South

    public class LinkCoverageWF_I15South : LinkCoverageWF
    {
        public LinkCoverageWF_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class LinkCoverageWF_I15Tech : LinkCoverageWF
    {
        public LinkCoverageWF_I15Tech(IWebDriver driver) : base(driver)
        {
        }

        public override void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QARecordControl_QA_Test_Original_Report();
            AddAssertionToList(TestDetails.VerifyTestDetailsFormIsDisplayed());
            NavigateToPage.QARecordControl_QA_Test_Correction_Report();
            AddAssertionToList(VerifyPageHeader("Create Correction Test Report"));
            NavigateToPage.QARecordControl_QA_Test_All();
            AddAssertionToList(VerifyPageHeader("Lab Tests"));
            NavigateToPage.QARecordControl_QA_Test_Retest_Report();
            AddAssertionToList(VerifyPageHeader("Create Retest Report"));
            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(VerifyPageHeader("IQF Record Control > List of Daily Inspection Reports"));
            NavigateToPage.QARecordControl_General_NCR();
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"));
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertionToList(VerifyPageHeader("List of CDR Reports"));
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Retaining Wall Backfill Quantity Tracker"));
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Concrete Paving Quantity Tracker"));
            NavigateToPage.QARecordControl_MPL_Tracker();
            AddAssertionToList(VerifyPageHeader("MPL Tracker"));
            NavigateToPage.QARecordControl_Girder_Tracker();
            AddAssertionToList(VerifyPageHeader("Girder Tracker"));

            AssertAll();
        }
    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class LinkCoverageWF_LAX : LinkCoverageWF
    {
        public LinkCoverageWF_LAX(IWebDriver driver) : base(driver)
        {
        }

        /// <summary>
        /// Common workflow method for Tenants:
        /// </summary>
        public override void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(VerifyPageHeader("IQF Record Control > List of Inspector's Daily Report"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_General_NCR();
            AddAssertionToList(VerifyPageHeader("List of NCR Reports"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertionToList(VerifyPageHeader("List of CDR Reports"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Retaining Wall Backfill Quantity Tracker"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            AddAssertionToList(VerifyPageHeader("Concrete Paving Quantity Tracker"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_MPL_Tracker();
            AddAssertionToList(VerifyPageHeader("MPL Tracker"));
            ClickNew();
            ClickCancel();
            NavigateToPage.QARecordControl_Girder_Tracker();
            AddAssertionToList(VerifyPageHeader("Girder Tracker"));
            ClickNew();
            ClickCancel();
            AssertAll();
        }

    }

    #endregion Implementation specific to LAX
}