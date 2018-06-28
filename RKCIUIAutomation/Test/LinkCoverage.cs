using static RKCIUIAutomation.Config.ProjectProperties;
using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using OpenQA.Selenium;
using RKCIUIAutomation.Base;

namespace RKCIUIAutomation.Test
{

    #region LinkCoverage Generic class
    public class LinkCoverage : LinkCoverage_Impl
    {
        public LinkCoverage(){}
        public LinkCoverage(IWebDriver driver) => this.driver = driver;

        /// <summary>
        /// Common pageObjects and workflows are inherited from abstract _Impl class
        /// </summary>




    }
    #endregion <-- end of LinkCoverage Generic class


    #region Workflow Interface class
    public interface ILinkCoverage
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
    }
    #endregion <-- end of Workflow Interface class


    #region Common Workflow Implementation class
    public abstract class LinkCoverage_Impl : TestBase, ILinkCoverage
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);
        private ILinkCoverage SetPageClassBasedOnTenant(IWebDriver driver)
        {
            ILinkCoverage instance = new LinkCoverage(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using LinkCoverage_SGWay instance ###### ");
                instance = new LinkCoverage_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using LinkCoverage_SH249 instance ###### ");
                instance = new LinkCoverage_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using LinkCoverage_Garnet instance ###### ");
                instance = new LinkCoverage_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using LinkCoverage_GLX instance ###### ");
                instance = new LinkCoverage_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using LinkCoverage_I15South instance ###### ");
                instance = new LinkCoverage_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using LinkCoverage_I15Tech instance ###### ");
                instance = new LinkCoverage_I15Tech(driver);
            }

            return instance;
        }



        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public virtual void _NavigateToVerifyProjectConfigurationMenu()
        {
            LogInfo($"Project Configuration component test - This test should run");
            LoginAs(UserType.Bhoomi);


            NavigateToPage.My_Details();
            AddAssertionToList(VerifyPageTitle("Account Details"));
            //Qms Document - based on Tenant
            NavigateToPage.Qms_Document();
            AddAssertionToList(VerifyPageTitle("QMS Documents"));

            //Project>Administration
            NavigateToPage.Admin_Project_Details();
            AddAssertionToList(VerifyPageTitle("Project"));
            NavigateToPage.Admin_Companies();
            AddAssertionToList(VerifyPageTitle("Companies"));
            NavigateToPage.Admin_Contracts();
            AddAssertionToList(VerifyPageTitle("Contracts"));
            NavigateToPage.Admin_Menu_Editor();
            AddAssertionToList(VerifyPageTitle("Menu Editor"));
            NavigateToPage.Admin_Suport();
            AddAssertionToList(VerifyPageTitle("Cache Management"));


            //Project>Administration>User Management
            NavigateToPage.UserMgmt_Roles();
            AddAssertionToList(VerifyPageTitle("Roles"));
            NavigateToPage.UserMgmt_Users();
            AddAssertionToList(VerifyPageTitle("Users"));
            NavigateToPage.UserMgmt_Access_Rights();
            AddAssertionToList(VerifyPageTitle("Access Rights"));

            //Project>Administration>System Configuration
            NavigateToPage.SysConfig_Disciplines();
            AddAssertionToList(VerifyPageTitle("Disciplines"));
            NavigateToPage.SysConfig_Submittal_Actions();
            AddAssertionToList(VerifyPageTitle("SubmittalActions"));
            NavigateToPage.SysConfig_Submittal_Requirements();
            AddAssertionToList(VerifyPageTitle("Submittal Requirments"));
            NavigateToPage.SysConfig_Submittal_Types();
            AddAssertionToList(VerifyPageTitle("SubmittalTypes"));
            NavigateToPage.SysConfig_CVL_Lists();
            AddAssertionToList(VerifyPageTitle("CVL List"));
            NavigateToPage.SysConfig_CVL_List_Items();
            AddAssertionToList(VerifyPageTitle("CVL List Items"));
            NavigateToPage.SysConfig_Notifications();
            AddAssertionToList(VerifyPageTitle("Notifications"));
            NavigateToPage.SysConfig_Sieves();
            AddAssertionToList(VerifyPageTitle("Sieves"));
            NavigateToPage.SysConfig_Gradations();
            AddAssertionToList(VerifyPageTitle("Gradations"));

            //Project>Administration>System Configuration>Equipment
            NavigateToPage.SysConfig_Equipment_Makes();
            AddAssertionToList(VerifyPageTitle("Makes"));
            NavigateToPage.SysConfig_Equipment_Models();
            AddAssertionToList(VerifyPageTitle("EquipmentModels"));
            NavigateToPage.SysConfig_Equipment_Types();
            AddAssertionToList(VerifyPageTitle("Equipment Types"));

            //Project>Administration>System Configuration>Grade Management
            NavigateToPage.SysConfig_Grade_Types();
            AddAssertionToList(VerifyPageTitle("Grade Type"));
            NavigateToPage.SysConfig_Grades();
            AddAssertionToList(VerifyPageTitle("Grades"));


            //Project>Administration>Admin Tools
            NavigateToPage.AdminTools_Locked_Records();
            AddAssertionToList(VerifyPageTitle("Locked Records"));
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToVerifyQALabMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QALab_Technician_Random();
            AddAssertionToList(VerifyPageTitle("Technician Random"));
            NavigateToPage.QALab_BreakSheet_Creation();
            AddAssertionToList(VerifyPageTitle("Create Break Sheet"));
            NavigateToPage.QALab_BreakSheet_Legacy();
            AddAssertionToList(VerifyPageTitle("Break Sheet Legacy"));
            NavigateToPage.QALab_Equipment_Management();
            AddAssertionToList(VerifyPageTitle("Equipment"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QALab_BreakSheet_Forecast();
            AddAssertionToList(VerifyPageTitle("Break Sheet Forecast"));
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
            AddAssertionToList(VerifyPageTitle("Lab Tests"));
            NavigateToPage.QARecordControl_QA_Test_Retest_Report();
            AddAssertionToList(VerifyPageTitle("Create Retest Report"));
            NavigateToPage.QARecordControl_QA_DIRs();
            AddAssertionToList(VerifyPageTitle("IQF Record Control > List of Daily Inspection Reports"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QARecordControl_General_NCR();
            AddAssertionToList(VerifyPageTitle("List of NCR Reports"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QARecordControl_General_CDR();
            AddAssertionToList(VerifyPageTitle("List of CDR Reports"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            AddAssertionToList(VerifyPageTitle("Retaining Wall Backfill Quantity Tracker"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            AddAssertionToList(VerifyPageTitle("Concrete Paving Quantity Tracker"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QARecordControl_MPL_Tracker();
            AddAssertionToList(VerifyPageTitle("MPL Tracker"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QARecordControl_Girder_Tracker();
            AddAssertionToList(VerifyPageTitle("Girder Tracker"));
            ClickCreate();
            ClickCancel();
            AssertAll();

        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public virtual void _NavigateToVerifyQAEngineerMenu()
        {
            LogInfo($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QAEngineer_QA_Test_Lab_Supervisor_Review();
            AddAssertionToList(VerifyPageTitle("Lab Supervisor Review"));
            NavigateToPage.QAEngineer_QA_Test_Field_Supervisor_Review(); //not in 249
            AddAssertionToList(VerifyPageTitle(""));
            NavigateToPage.QAEngineer_QA_Test_Authorization();
            AddAssertionToList(VerifyPageTitle("Authorizations"));
            NavigateToPage.QAEngineer_DIR_QA_Review_Approval();
            AddAssertionToList(VerifyPageTitle("IQF Engineer > List of Daily Inspection Reports"));
            NavigateToPage.QA_Test_Proctor_Curve_Controller();
            AddAssertionToList(VerifyPageTitle("Proctor Curve Information"));
            AssertAll();
        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public virtual void _NavigateToReportsAndNoticesMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_NCR(); //TODO: Is this correct?
            AddAssertionToList(VerifyPageTitle("List of NCR Reports"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QARecordControl_General_CDR(); //TODO: Is this correct?
            AddAssertionToList(VerifyPageTitle("List of CDR Reports"));
            ClickCreate();
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
            AddAssertionToList(VerifyPageTitle("OV Tests"));

            AssertAll();
        }


        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToQASearchMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QASearch_QA_Tests();
            AddAssertionToList(VerifyPageTitle("Test Search"));
            NavigateToPage.QASearch_QA_Test_Summary_Search();
            AddAssertionToList(VerifyPageTitle("Test Summary Report Search"));
            NavigateToPage.QASearch_QA_Guide_Schedule_Summary_Report();
            AddAssertionToList(VerifyPageTitle("Guide Schedule of Sampling and Testing"));
            NavigateToPage.QASearch_Inspection_Deficiency_Log_Report();
            AddAssertionToList(VerifyPageTitle("Inspection Deficiency Log Search"));
            NavigateToPage.QASearch_Daily_Inspection_Report();
            AddAssertionToList(VerifyPageTitle("Daily Inspection Report Search"));
            NavigateToPage.QASearch_DIR_Summary_Report();
            AddAssertionToList(VerifyPageTitle("DIR Summary Report Search"));
            NavigateToPage.QASearch_DIR_Checklist_Search();
            AddAssertionToList(VerifyPageTitle("DIR Checklist Search"));
            NavigateToPage.QASearch_QMS_Document_Search();
            AddAssertionToList(VerifyPageTitle("QMS Documents Search"));
            NavigateToPage.Environmental_Document_Search();//not in 249
            AddAssertionToList(VerifyPageTitle(""));
            NavigateToPage.QAQO_Test_Proctor_Curve_Report();
            AddAssertionToList(VerifyPageTitle("ProctorCurveSummary"));
            NavigateToPage.QAQO_Test_Proctor_Curve_Summary();
            AddAssertionToList(VerifyPageTitle("ProctorCurveReport"));
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToQAFieldMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QAField_QA_Test();
            AddAssertionToList(VerifyPageTitle("Field Tests"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QAField_QA_DIRs();
            AddAssertionToList(VerifyPageTitle("IQF Field > List of Daily Inspection Reports"));
            ClickCreate();
            ClickCancel();
            // NavigateToPage.QAField_QA_Technician_Random_Search();//page not implemented
            // AddAssertionToList(VerifyPageTitle("");
            NavigateToPage.QAField_Weekly_Environmental_Monitoring();
            AddAssertionToList(VerifyPageTitle("Week Environmental Monitoring Reports"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QAField_Daily_Environmental_Inspection();
            AddAssertionToList(VerifyPageTitle("List of Daily Environmental Inspection Reports"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.QAField_Weekly_Environmental_Inspection();
            AddAssertionToList(VerifyPageTitle("List of Weekly Environmental Inspection Reportse"));
            ClickCreate();
            ClickCancel();
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToControlPointMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Control_Point_Log();
            AddAssertionToList(VerifyPageTitle("Control Point List"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.Control_Point_Scheduler();
            AddAssertionToList(VerifyPageTitle("Control Point Log Search"));
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public virtual void _NavigateToOwnerMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Owner_DIRs();
            AddAssertionToList(VerifyPageTitle("DOT > List of Daily Inspection Reports"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.Owner_NCRs();
            AddAssertionToList(VerifyPageTitle("List of NCR Reports"));
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
            AddAssertionToList(VerifyPageTitle("Pcc Mix Design"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Mix_Design_HMA();
            AddAssertionToList(VerifyPageTitle("Hma Mix Design"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Sieve_Analyses_JMF();
            AddAssertionToList(VerifyPageTitle("Sieve Analysis JMF"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Sieve_Analyses_IOC();
            AddAssertionToList(VerifyPageTitle("Sieve Analysis IOC"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Material_Code_Concrete_Aggregate();
            AddAssertionToList(VerifyPageTitle("Aggregate A"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Material_Code_Base_Aggregate();
            AddAssertionToList(VerifyPageTitle("Aggregate E"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Material_Code_HMA_Aggregate();
            AddAssertionToList(VerifyPageTitle("Aggregate F"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.MaterialMixCodes_Material_Code_Raw_Material();
            AddAssertionToList(VerifyPageTitle("Raw Materials"));
            ClickCreate();
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
            AddAssertionToList(VerifyPageTitle("Request For Information"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.RFI_List();
            //TODO: verify if test should fail if no title is seen on the page
            AddAssertionToList(VerifyPageTitle(""));//dont see title
            AssertAll();
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, SGWay, & SH249
        /// </summary>
        public virtual void _NavigateToRMCenterMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.RMCenter_Search();
            AddAssertionToList(VerifyPageTitle("RM Center Search"));
            NavigateToPage.RMCenter_Design_Documents();
            AddAssertionToList(VerifyPageTitle("Design Document"));
            ClickCreate();
            ClickCancel();
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            AddAssertionToList(VerifyPageTitle("Submittal Details"));
            NavigateToPage.RMCenter_Upload_Owner_Submittal();
            AddAssertionToList(VerifyPageTitle("New Submittal"));
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
            AddAssertionToList(VerifyPageTitle("Submittal Details"));
            NavigateToPage.RMCenter_DOT_Project_Correspondence_Log();
            AddAssertionToList(VerifyPageTitle("Transmissions"));
            NavigateToPage.RMCenter_Review_Revise_Submittal();
            AddAssertionToList(VerifyPageTitle("Review / Revise Submittals"));
            NavigateToPage.RMCenter_RFC_Management();
            AddAssertionToList(VerifyPageTitle("RFC List"));
            NavigateToPage.RMCenter_Project_Correspondence_Log();
            AddAssertionToList(VerifyPageTitle("Transmissions"));

            AssertAll();
        }
    }
    #endregion <-- end of common implementation class




    /// <summary>
    /// Tenant specific implementation of LinkCoverage
    /// </summary>

    #region Implementation specific to Garnet
    public class LinkCoverage_Garnet : LinkCoverage
    {
        public LinkCoverage_Garnet(IWebDriver driver) : base(driver) { }

        public override void _NavigateToVerifyQALabMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QALab_Technician_Random();
            bool techRandomTitleDisplayed = VerifyPageTitle("Technician Random");
            NavigateToPage.QALab_BreakSheet_Creation();
            bool breaksheetCreationTitleDisplayed = VerifyPageTitle("Create Break Sheet");
            NavigateToPage.QALab_BreakSheet_Legacy();
            bool breaksheetLegacyTitleDisplayed = VerifyPageTitle("Break Sheet Legacy");
            NavigateToPage.QALab_Equipment_Management();
            bool equipMgmtTitleDisplayed = VerifyPageTitle("Equipment");
            NavigateToPage.QALab_BreakSheet_Forecast();
            bool breaksheetForecastTitleDisplayed = VerifyPageTitle("Break Sheet Forecast");

            Assert.Multiple(testDelegate: () =>
            {
                AddAssertionToList(techRandomTitleDisplayed);
                AddAssertionToList(breaksheetCreationTitleDisplayed);
                AddAssertionToList(breaksheetLegacyTitleDisplayed);
                AddAssertionToList(equipMgmtTitleDisplayed);
                AddAssertionToList(breaksheetForecastTitleDisplayed);
            });
        }

    }
    #endregion


    #region Implementation specific to GLX
    public class LinkCoverage_GLX : LinkCoverage
    {
        public LinkCoverage_GLX(IWebDriver driver) : base(driver) { }
    }
    #endregion


    #region Implementation specific to SH249
    public class LinkCoverage_SH249 : LinkCoverage
    {
        public LinkCoverage_SH249(IWebDriver driver) : base(driver) { }

        public override void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);
            
            NavigateToPage.QARecordControl_QA_Test_Original_Report();
            bool qa_Test_Original_Report_FormIsDisplayed = TestDetails.VerifyTestDetailsFormIsDisplayed();
            NavigateToPage.QARecordControl_QA_Test_Correction_Report();
            bool qa_Test_Correction_ReportTitleDisplayed = VerifyPageTitle("Create Correction Test Report");
            NavigateToPage.QARecordControl_QA_Test_All();
            bool qa_Test_AllTitleDisplayed = VerifyPageTitle("Lab Tests");
            NavigateToPage.QARecordControl_QA_Test_Retest_Report();
            bool qa_Test_Retest_ReportTitleDisplayed = VerifyPageTitle("Create Retest Report");
            NavigateToPage.QARecordControl_QA_DIRs();
            bool qa_DIRsTitleDisplayed = VerifyPageTitle("IQF Record Control > List of Daily Inspection Reports");
            NavigateToPage.QARecordControl_General_NCR();
            bool general_NCRTitleDisplayed = VerifyPageTitle("List of NCR Reports");
            NavigateToPage.QARecordControl_General_CDR();
            bool general_CDRTitleDisplayed = VerifyPageTitle("List of CDR Reports");
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            bool retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed = VerifyPageTitle("Retaining Wall Backfill Quantity Tracker");
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            bool concrete_Paving_Quantity_TrackerTitleDisplayed = VerifyPageTitle("Concrete Paving Quantity Tracker");
            NavigateToPage.QARecordControl_MPL_Tracker();
            bool mpl_TrackerTitleDisplayed = VerifyPageTitle("MPL Tracker");
            NavigateToPage.QARecordControl_Girder_Tracker();
            bool girder_TrackerTitleDisplayed = VerifyPageTitle("Girder Tracker");


            Assert.Multiple(testDelegate: () =>
            {
                AddAssertionToList(qa_Test_Original_Report_FormIsDisplayed);
                AddAssertionToList(qa_Test_Correction_ReportTitleDisplayed);
                AddAssertionToList(qa_Test_AllTitleDisplayed);
                AddAssertionToList(qa_Test_Retest_ReportTitleDisplayed);
                AddAssertionToList(qa_DIRsTitleDisplayed);
                AddAssertionToList(general_NCRTitleDisplayed);
                AddAssertionToList(general_CDRTitleDisplayed);
                AddAssertionToList(retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed);
                AddAssertionToList(concrete_Paving_Quantity_TrackerTitleDisplayed);
                AddAssertionToList(mpl_TrackerTitleDisplayed);
                AddAssertionToList(girder_TrackerTitleDisplayed);
            });


        }
    }
    #endregion


    #region Implementation specific to SGWay
    public class LinkCoverage_SGWay : LinkCoverage
    {
        public LinkCoverage_SGWay(IWebDriver driver) : base(driver) { }

        public override void _NavigateToVerifyQALabMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QALab_Technician_Random();
            bool techRandomTitleDisplayed = VerifyPageTitle("Technician Random");
            NavigateToPage.QALab_BreakSheet_Creation();
            bool breaksheetCreationTitleDisplayed = VerifyPageTitle("Create Break Sheet");
            NavigateToPage.QALab_BreakSheet_Legacy();
            bool breaksheetLegacyTitleDisplayed = VerifyPageTitle("Break Sheet Legacy");
            NavigateToPage.QALab_Equipment_Management();
            bool equipMgmtTitleDisplayed = VerifyPageTitle("Equipment");
            NavigateToPage.QALab_BreakSheet_Forecast();
            bool breaksheetForecastTitleDisplayed = VerifyPageTitle("Break Sheet Forecast");
            NavigateToPage.QALab_Cylinder_PickUp_List();
            bool cylinderPickupListTitleDisplayed = VerifyPageTitle("Cylinder Pick-Up Status:");
            NavigateToPage.QALab_Early_Break_Calendar();
            bool earlyBreakCalendarDisplayed = VerifySchedulerIsDisplayed();

            Assert.Multiple(testDelegate: () =>
            {
                AddAssertionToList(techRandomTitleDisplayed);
                AddAssertionToList(breaksheetCreationTitleDisplayed);
                AddAssertionToList(breaksheetLegacyTitleDisplayed);
                AddAssertionToList(equipMgmtTitleDisplayed);
                AddAssertionToList(breaksheetForecastTitleDisplayed);
                AddAssertionToList(cylinderPickupListTitleDisplayed);
                AddAssertionToList(earlyBreakCalendarDisplayed);
            });
        }

        public override void _NavigateToRMCenterMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.RMCenter_Search();
            bool RMCenter_Search_TitleDisplayed = VerifyPageTitle("RM Center Search");
            NavigateToPage.RMCenter_Design_Documents();
            bool RMCenter_Design_Documents_TitleDisplayed = VerifyPageTitle("Design Document");
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            bool RMCenter_Upload_QA_Submittal_TitleDisplayed = VerifyPageTitle("Submittal Details");
            NavigateToPage.RMCenter_Upload_Owner_Submittal();
            bool RMCenter_Upload_Owner_Submittal_TitleDisplayed = VerifyPageTitle("New Submittal");
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
            bool RMCenter_Upload_DEV_Submittal_TitleDisplayed = VerifyPageTitle("Submittal Details");
            NavigateToPage.RMCenter_DOT_Project_Correspondence_Log();
            bool RMCenter_DOT_Project_Correspondence_Log_TitleDisplayed = VerifyPageTitle("Transmissions");
            NavigateToPage.RMCenter_Review_Revise_Submittal();
            bool RMCenter_Review_Revise_Submittal_TitleDisplayed = VerifyPageTitle("Review / Revise Submittals");
            NavigateToPage.RMCenter_RFC_Management();
            bool RMCenter_RFC_Management_TitleDisplayed = VerifyPageTitle("RFC List");
            NavigateToPage.RMCenter_Project_Correspondence_Log();
            bool RMCenter_Project_Correspondence_Log_TitleDisplayed = VerifyPageTitle("Transmissions");
            NavigateToPage.RMCenter_Comment_Summary();
            bool RMCenter_Comment_Summary_TitleDisplayed = VerifyPageTitle("Comment Summary Search");

            Assert.Multiple(testDelegate: () =>
            {
                AddAssertionToList(RMCenter_Search_TitleDisplayed);
                AddAssertionToList(RMCenter_Design_Documents_TitleDisplayed);
                AddAssertionToList(RMCenter_Upload_QA_Submittal_TitleDisplayed);
                AddAssertionToList(RMCenter_Upload_Owner_Submittal_TitleDisplayed);
                AddAssertionToList(RMCenter_Upload_DEV_Submittal_TitleDisplayed);
                AddAssertionToList(RMCenter_DOT_Project_Correspondence_Log_TitleDisplayed);
                AddAssertionToList(RMCenter_Review_Revise_Submittal_TitleDisplayed);
                AddAssertionToList(RMCenter_RFC_Management_TitleDisplayed);
                AddAssertionToList(RMCenter_Project_Correspondence_Log_TitleDisplayed);
                AddAssertionToList(RMCenter_Comment_Summary_TitleDisplayed);
            });
        }

        public override void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);
       

            NavigateToPage.QARecordControl_QA_Test_Original_Report();
            bool qa_Test_Original_Report_FormIsDisplayed = TestDetails.VerifyTestDetailsFormIsDisplayed();
            NavigateToPage.QARecordControl_QA_Test_Correction_Report();
            bool qa_Test_Correction_ReportTitleDisplayed = VerifyPageTitle("Create Correction Test Report");
            NavigateToPage.QARecordControl_QA_Test_All();
            bool qa_Test_AllTitleDisplayed = VerifyPageTitle("Lab Tests");
            NavigateToPage.QARecordControl_QA_Test_Retest_Report();
            bool qa_Test_Retest_ReportTitleDisplayed = VerifyPageTitle("Create Retest Report");
            NavigateToPage.QARecordControl_QA_DIRs();
            bool qa_DIRsTitleDisplayed = VerifyPageTitle("IQF Record Control > List of Daily Inspection Reports");
            NavigateToPage.QARecordControl_General_NCR();
            bool general_NCRTitleDisplayed = VerifyPageTitle("List of NCR Reports");
            NavigateToPage.QARecordControl_General_CDR();
            bool general_CDRTitleDisplayed = VerifyPageTitle("List of CDR Reports");
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            bool retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed = VerifyPageTitle("Retaining Wall Backfill Quantity Tracker");
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            bool concrete_Paving_Quantity_TrackerTitleDisplayed = VerifyPageTitle("Concrete Paving Quantity Tracker");
            NavigateToPage.QARecordControl_MPL_Tracker();
            bool mpl_TrackerTitleDisplayed = VerifyPageTitle("MPL Tracker");
            NavigateToPage.QARecordControl_Girder_Tracker();
            bool girder_TrackerTitleDisplayed = VerifyPageTitle("Girder Tracker");
            NavigateToPage.Qms_Document();
            bool qmsDocumentTitleDisplayed = VerifyPageTitle("QMS Documents");
            NavigateToPage.QARecordControl_Environmental_Document();
            bool environmental_DocumentTitleDisplayed = VerifyPageTitle("Environmental Documents");



            Assert.Multiple(testDelegate: () =>
            {
                AddAssertionToList(qa_Test_Original_Report_FormIsDisplayed);
                AddAssertionToList(qa_Test_Correction_ReportTitleDisplayed);
                AddAssertionToList(qa_Test_AllTitleDisplayed);
                AddAssertionToList(qa_Test_Retest_ReportTitleDisplayed);
                AddAssertionToList(qa_DIRsTitleDisplayed);
                AddAssertionToList(general_NCRTitleDisplayed);
                AddAssertionToList(general_CDRTitleDisplayed);
                AddAssertionToList(retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed);
                AddAssertionToList(concrete_Paving_Quantity_TrackerTitleDisplayed);
                AddAssertionToList(mpl_TrackerTitleDisplayed);
                AddAssertionToList(girder_TrackerTitleDisplayed);
                AddAssertionToList(qmsDocumentTitleDisplayed);
                AddAssertionToList(environmental_DocumentTitleDisplayed);
            });


        }
    }
    #endregion


    #region Implementation specific to I15South
    public class LinkCoverage_I15South : LinkCoverage
    {
        public LinkCoverage_I15South(IWebDriver driver) : base(driver) { }
    }
    #endregion


    #region Implementation specific to I15Tech
    public class LinkCoverage_I15Tech : LinkCoverage
    {
        public LinkCoverage_I15Tech(IWebDriver driver) : base(driver) { }

        public override void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.QARecordControl_QA_Test_Original_Report();
            bool qa_Test_Original_Report_FormIsDisplayed = TestDetails.VerifyTestDetailsFormIsDisplayed();
            NavigateToPage.QARecordControl_QA_Test_Correction_Report();
            bool qa_Test_Correction_ReportTitleDisplayed = VerifyPageTitle("Create Correction Test Report");
            NavigateToPage.QARecordControl_QA_Test_All();
            bool qa_Test_AllTitleDisplayed = VerifyPageTitle("Lab Tests");
            NavigateToPage.QARecordControl_QA_Test_Retest_Report();
            bool qa_Test_Retest_ReportTitleDisplayed = VerifyPageTitle("Create Retest Report");
            NavigateToPage.QARecordControl_QA_DIRs();
            bool qa_DIRsTitleDisplayed = VerifyPageTitle("IQF Record Control > List of Daily Inspection Reports");
            NavigateToPage.QARecordControl_General_NCR();
            bool general_NCRTitleDisplayed = VerifyPageTitle("List of NCR Reports");
            NavigateToPage.QARecordControl_General_CDR();
            bool general_CDRTitleDisplayed = VerifyPageTitle("List of CDR Reports");
            NavigateToPage.QARecordControl_Retaining_Wall_BackFill_Quantity_Tracker();
            bool retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed = VerifyPageTitle("Retaining Wall Backfill Quantity Tracker");
            NavigateToPage.QARecordControl_Concrete_Paving_Quantity_Tracker();
            bool concrete_Paving_Quantity_TrackerTitleDisplayed = VerifyPageTitle("Concrete Paving Quantity Tracker");
            NavigateToPage.QARecordControl_MPL_Tracker();
            bool mpl_TrackerTitleDisplayed = VerifyPageTitle("MPL Tracker");
            NavigateToPage.QARecordControl_Girder_Tracker();
            bool girder_TrackerTitleDisplayed = VerifyPageTitle("Girder Tracker");


            Assert.Multiple(testDelegate: () =>
            {
                AddAssertionToList(qa_Test_Original_Report_FormIsDisplayed);
                AddAssertionToList(qa_Test_Correction_ReportTitleDisplayed);
                AddAssertionToList(qa_Test_AllTitleDisplayed);
                AddAssertionToList(qa_Test_Retest_ReportTitleDisplayed);
                AddAssertionToList(qa_DIRsTitleDisplayed);
                AddAssertionToList(general_NCRTitleDisplayed);
                AddAssertionToList(general_CDRTitleDisplayed);
                AddAssertionToList(retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed);
                AddAssertionToList(concrete_Paving_Quantity_TrackerTitleDisplayed);
                AddAssertionToList(mpl_TrackerTitleDisplayed);
                AddAssertionToList(girder_TrackerTitleDisplayed);
            });


        }
    }

    #endregion
}
