using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Config.ProjectProperties;
using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Base;

namespace RKCIUIAutomation.Test
{
    #region NUnit Test Case Methods class
    [TestFixture]
    public class LinkCoverage : LinkCoverage_Impl
    {
        ILinkCoverage Instance => SetClass<ILinkCoverage>();


        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Project Configuration Menu only")]
        public override void NavigateToVerifyProjectConfigurationMenu() => Instance._NavigateToVerifyProjectConfigurationMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("Component2", Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for QA Lab Menu only")]
        public override void NavigateToVerifyQALabMenu() => Instance._NavigateToVerifyQALabMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for QA Record Control Menu only")]
        public override void NavigateToVerifyQARecordControlMenu() => Instance._NavigateToVerifyQARecordControlMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("Component2", Component.OV_Test)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for OV Menu only")]
        public override void NavigateToOVMenu() => Instance._NavigateToOVMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for QA Engineer Menu only")]
        public override void NavigateToVerifyQAEngineerMenu() => Instance._NavigateToVerifyQAEngineerMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Reports & Notices Menu only")]
        public override void NavigateToReportsAndNoticesMenu() => Instance._NavigateToReportsAndNoticesMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public override void NavigateToQASearchMenu() => Instance._NavigateToQASearchMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public override void NavigateToQAFieldMenu() => Instance._NavigateToQAFieldMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public override void NavigateToControlPointMenu() => Instance._NavigateToControlPointMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public override void NavigateToOwnerMenu() => Instance._NavigateToOwnerMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public override void NavigateToMaterialMixCodeMenu() => Instance._NavigateToMaterialMixCodeMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public override void NavigateToRMCenterMenu() => Instance._NavigateToRMCenterMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public override void NavigateToRFIMenu() => Instance._NavigateToRFIMenu();


    }
    #endregion <-- end of Test Case Methods class


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
        public static T SetClass<T>() => (T)SetPageClassBasedOnTenant();
        private static ILinkCoverage SetPageClassBasedOnTenant()
        {
            ILinkCoverage instance = new LinkCoverage();

            if (projectName == ProjectName.SGWay)
            {
                LogInfo($"###### using LinkCoverage_SGWay instance ###### ");
                instance = new LinkCoverage_SGWay();
            }
            else if (projectName == ProjectName.SH249)
            {
                LogInfo($"###### using LinkCoverage_SH249 instance ###### ");
                instance = new LinkCoverage_SH249();
            }
            else if (projectName == ProjectName.Garnet)
            {
                LogInfo($"###### using LinkCoverage_Garnet instance ###### ");
                instance = new LinkCoverage_Garnet();
            }
            else if (projectName == ProjectName.GLX)
            {
                LogInfo($"###### using LinkCoverage_GLX instance ###### ");
                instance = new LinkCoverage_GLX();
            }
            else if (projectName == ProjectName.I15South)
            {
                LogInfo($"###### using LinkCoverage_I15South instance ###### ");
                instance = new LinkCoverage_I15South();
            }
            else if (projectName == ProjectName.I15Tech)
            {
                LogInfo($"###### using LinkCoverage_I15Tech instance ###### ");
                instance = new LinkCoverage_I15Tech();
            }

            return instance;
        }



        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public abstract void NavigateToVerifyQARecordControlMenu();
        public virtual void _NavigateToVerifyQARecordControlMenu()
        {
            LoginAs(UserType.Bhoomi);
            //QA Test-Original Report
            //QA Test- Correction Report (Not in Garnet, glx, I15SB
            //QA test- All
            //QA Test- Retest Report
            //QA DIRs
            //General NCR
            //General CDR
            //Retaining Wall Backfill Quantity Tracker
            //Concrete Paving Quantity Tracker
            //MPL Tracker
            //Girder Tracker
            //QMS Document(Not in Garnet, glx, sh249, I15tech, I15SB
            //Environmental Document (Not in Garnet, glx, sh249,I15tech, I15SB

            NavigateToPage.QARecordControl_QA_Test_Original_Report();
            bool qa_Test_Original_Report_FormIsDisplayed = TestDetails.VerifyTestDetailsFormIsDisplayed();
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
                Assert.True(qa_Test_Original_Report_FormIsDisplayed);
                Assert.True(qa_Test_AllTitleDisplayed);
                Assert.True(qa_Test_Retest_ReportTitleDisplayed);
                Assert.True(qa_DIRsTitleDisplayed);
                Assert.True(general_NCRTitleDisplayed);
                Assert.True(general_CDRTitleDisplayed);
                Assert.True(retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed);
                Assert.True(concrete_Paving_Quantity_TrackerTitleDisplayed);
                Assert.True(mpl_TrackerTitleDisplayed);
                Assert.True(girder_TrackerTitleDisplayed);
            });


        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public abstract void NavigateToVerifyQAEngineerMenu();
        public virtual void _NavigateToVerifyQAEngineerMenu()
        {
            LogInfo($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QAEngineer_QA_Test_Lab_Supervisor_Review();
            NavigateToPage.QAEngineer_QA_Test_Field_Supervisor_Review();
            NavigateToPage.QAEngineer_QA_Test_Authorization();
            NavigateToPage.QAEngineer_DIR_QA_Review_Approval();
            //Proctor curve menu item

        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public abstract void NavigateToVerifyProjectConfigurationMenu();
        public virtual void _NavigateToVerifyProjectConfigurationMenu()
        {
            LogInfo($"Project Configuration component test - This test should run");
            LoginAs(UserType.Bhoomi);


            NavigateToPage.My_Details();
            Assert.True(VerifyPageTitle("Account Details"));
            //Qms Document - based on Tenant
            NavigateToPage.Qms_Document();
            Assert.True(VerifyPageTitle("QMS Documents"));

            //Project>Administration
            NavigateToPage.Admin_Project_Details();
            Assert.True(VerifyPageTitle("Project"));
            NavigateToPage.Admin_Companies();
            Assert.True(VerifyPageTitle("Companies"));
            NavigateToPage.Admin_Contracts();
            Assert.True(VerifyPageTitle("Contracts"));
            NavigateToPage.Admin_Menu_Editor();
            Assert.True(VerifyPageTitle("Menu Editor"));
            NavigateToPage.Admin_Suport();
            Assert.True(VerifyPageTitle("Cache Management"));


            //Project>Administration>User Management
            NavigateToPage.UserMgmt_Roles();
            Assert.True(VerifyPageTitle("Roles"));
            NavigateToPage.UserMgmt_Users();
            Assert.True(VerifyPageTitle("Users"));
            NavigateToPage.UserMgmt_Access_Rights();
            Assert.True(VerifyPageTitle("Access Rights"));

            //Project>Administration>System Configuration
            NavigateToPage.SysConfig_Disciplines();
            Assert.True(VerifyPageTitle("Disciplines"));
            NavigateToPage.SysConfig_Submittal_Actions();
            Assert.True(VerifyPageTitle("SubmittalActions"));
            NavigateToPage.SysConfig_Submittal_Requirements();
            Assert.True(VerifyPageTitle("Submittal Requirments"));
            NavigateToPage.SysConfig_Submittal_Types();
            Assert.True(VerifyPageTitle("SubmittalTypes"));
            NavigateToPage.SysConfig_CVL_Lists();
            Assert.True(VerifyPageTitle("CVL List"));
            NavigateToPage.SysConfig_CVL_List_Items();
            Assert.True(VerifyPageTitle("CVL List Items"));
            NavigateToPage.SysConfig_Notifications();
            Assert.True(VerifyPageTitle("Notifications"));
            NavigateToPage.SysConfig_Sieves();
            Assert.True(VerifyPageTitle("Sieves"));
            NavigateToPage.SysConfig_Gradations();
            Assert.True(VerifyPageTitle("Gradations"));

            //Project>Administration>System Configuration>Equipment
            NavigateToPage.SysConfig_Equipment_Makes();
            Assert.True(VerifyPageTitle("Makes"));
            NavigateToPage.SysConfig_Equipment_Models();
            Assert.True(VerifyPageTitle("EquipmentModels"));
            NavigateToPage.SysConfig_Equipment_Types();
            Assert.True(VerifyPageTitle("Equipment Types"));

            //Project>Administration>System Configuration>Grade Management
            NavigateToPage.SysConfig_Grade_Types();
            Assert.True(VerifyPageTitle("Grade Type"));
            NavigateToPage.SysConfig_Grades();
            Assert.True(VerifyPageTitle("Grades"));


            //Project>Administration>Admin Tools
            NavigateToPage.AdminTools_Locked_Records();
            Assert.True(VerifyPageTitle("Locked Records"));
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, & SH249
        /// </summary>
        public abstract void NavigateToVerifyQALabMenu();
        public virtual void _NavigateToVerifyQALabMenu()
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
            NavigateToPage.QALab_Early_Break_Calendar();
            bool earlyBreakCalendarDisplayed = VerifySchedulerIsDisplayed();

            Assert.Multiple(testDelegate: () =>
            {
                Assert.True(techRandomTitleDisplayed);
                Assert.True(breaksheetCreationTitleDisplayed);
                Assert.True(breaksheetLegacyTitleDisplayed);
                Assert.True(equipMgmtTitleDisplayed);
                Assert.True(breaksheetForecastTitleDisplayed);
                Assert.True(earlyBreakCalendarDisplayed);
            });
        }

        /// <summary>
        /// Common workflow method for Tenants: GLX, I15South, I15Tech, SGWay, & SH249
        /// </summary>
        public abstract void NavigateToRMCenterMenu();
        public virtual void _NavigateToRMCenterMenu()
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

            Assert.Multiple(testDelegate: () =>
            {
                Assert.True(RMCenter_Search_TitleDisplayed);
                Assert.True(RMCenter_Design_Documents_TitleDisplayed);
                Assert.True(RMCenter_Upload_QA_Submittal_TitleDisplayed);
                Assert.True(RMCenter_Upload_Owner_Submittal_TitleDisplayed);
                Assert.True(RMCenter_Upload_DEV_Submittal_TitleDisplayed);
                Assert.True(RMCenter_DOT_Project_Correspondence_Log_TitleDisplayed);
                Assert.True(RMCenter_Review_Revise_Submittal_TitleDisplayed);
                Assert.True(RMCenter_RFC_Management_TitleDisplayed);
                Assert.True(RMCenter_Project_Correspondence_Log_TitleDisplayed);
            });
        }

        public abstract void NavigateToOVMenu();
        public virtual void _NavigateToOVMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.OV_Create_OV_Test();
            bool OV_Create_OV_Test_FormIsDisplayed = TestDetails.VerifyTestDetailsFormIsDisplayed();
            NavigateToPage.OV_OV_Test();
            bool OV_OV_Test_TitleDisplayed = VerifyPageTitle("OV Tests");

            Assert.Multiple(testDelegate: () =>
            {
                Assert.True(OV_Create_OV_Test_FormIsDisplayed);
                Assert.True(OV_OV_Test_TitleDisplayed);
            });
        }

        public abstract void NavigateToReportsAndNoticesMenu();
        public virtual void _NavigateToReportsAndNoticesMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_NCR();
            NavigateToPage.QARecordControl_General_CDR();
        }

        public abstract void NavigateToQASearchMenu();
        public virtual void _NavigateToQASearchMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QASearch_QA_Tests();
            NavigateToPage.QASearch_QA_Test_Summary_Search();
            NavigateToPage.QASearch_QA_Guide_Schedule_Summary_Report();
            NavigateToPage.QASearch_Inspection_Deficiency_Log_Report();
            NavigateToPage.QASearch_Daily_Inspection_Report();
            NavigateToPage.QASearch_DIR_Summary_Report();
            NavigateToPage.QASearch_DIR_Checklist_Search();
            NavigateToPage.QASearch_QMS_Document_Search();
            //environmental doc search menu
            //Procutor curve report menu
            //proctor curve sumary report menu
        }

        public abstract void NavigateToQAFieldMenu();
        public virtual void _NavigateToQAFieldMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QAField_QA_Test();
            NavigateToPage.QAField_QA_DIRs();
            NavigateToPage.QAField_QA_Technician_Random_Search();
            NavigateToPage.QAField_Weekly_Environmental_Monitoring();
            NavigateToPage.QAField_Daily_Environmental_Inspection();
            NavigateToPage.QAField_Weekly_Environmental_Inspection();
        }

        public abstract void NavigateToControlPointMenu();
        public virtual void _NavigateToControlPointMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Control_Point_Log();
            NavigateToPage.Control_Point_Scheduler();
        }

        public abstract void NavigateToOwnerMenu();
        public virtual void _NavigateToOwnerMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Owner_DIRs();
            NavigateToPage.Owner_NCRs();
        }

        public abstract void NavigateToMaterialMixCodeMenu();
        public virtual void _NavigateToMaterialMixCodeMenu()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.MaterialMixCodes_Mix_Design_PCC();
            NavigateToPage.MaterialMixCodes_Mix_Design_HMA();
            NavigateToPage.MaterialMixCodes_Sieve_Analyses_JMF();
            NavigateToPage.MaterialMixCodes_Sieve_Analyses_IOC();
            NavigateToPage.MaterialMixCodes_Material_Code_Concrete_Aggregate();
            NavigateToPage.MaterialMixCodes_Material_Code_Base_Aggregate();
            NavigateToPage.MaterialMixCodes_Material_Code_HMA_Aggregate();
            NavigateToPage.MaterialMixCodes_Material_Code_Raw_Material();
        }

        public abstract void NavigateToRFIMenu();
        public virtual void _NavigateToRFIMenu()
        {
            LoginAs(UserType.Bhoomi);

            NavigateToPage.RFI_Create();
            NavigateToPage.RFI_List();
        }
    }
    #endregion <-- end of common implementation class




    /// <summary>
    /// Tenant specific implementation of LinkCoverage
    /// </summary>

    #region Implementation specific to Garnet
    public class LinkCoverage_Garnet : LinkCoverage
    {
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
                Assert.True(techRandomTitleDisplayed);
                Assert.True(breaksheetCreationTitleDisplayed);
                Assert.True(breaksheetLegacyTitleDisplayed);
                Assert.True(equipMgmtTitleDisplayed);
                Assert.True(breaksheetForecastTitleDisplayed);
            });
        }

    }
    #endregion


    #region Implementation specific to GLX
    public class LinkCoverage_GLX : LinkCoverage
    {
    }
    #endregion


    #region Implementation specific to SH249
    public class LinkCoverage_SH249 : LinkCoverage
    {
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
                Assert.True(qa_Test_Original_Report_FormIsDisplayed);
                Assert.True(qa_Test_Correction_ReportTitleDisplayed);
                Assert.True(qa_Test_AllTitleDisplayed);
                Assert.True(qa_Test_Retest_ReportTitleDisplayed);
                Assert.True(qa_DIRsTitleDisplayed);
                Assert.True(general_NCRTitleDisplayed);
                Assert.True(general_CDRTitleDisplayed);
                Assert.True(retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed);
                Assert.True(concrete_Paving_Quantity_TrackerTitleDisplayed);
                Assert.True(mpl_TrackerTitleDisplayed);
                Assert.True(girder_TrackerTitleDisplayed);
            });


        }
    }
    #endregion


    #region Implementation specific to SGWay
    public class LinkCoverage_SGWay : LinkCoverage
    {
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
                Assert.True(techRandomTitleDisplayed);
                Assert.True(breaksheetCreationTitleDisplayed);
                Assert.True(breaksheetLegacyTitleDisplayed);
                Assert.True(equipMgmtTitleDisplayed);
                Assert.True(breaksheetForecastTitleDisplayed);
                Assert.True(cylinderPickupListTitleDisplayed);
                Assert.True(earlyBreakCalendarDisplayed);
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
                Assert.True(RMCenter_Search_TitleDisplayed);
                Assert.True(RMCenter_Design_Documents_TitleDisplayed);
                Assert.True(RMCenter_Upload_QA_Submittal_TitleDisplayed);
                Assert.True(RMCenter_Upload_Owner_Submittal_TitleDisplayed);
                Assert.True(RMCenter_Upload_DEV_Submittal_TitleDisplayed);
                Assert.True(RMCenter_DOT_Project_Correspondence_Log_TitleDisplayed);
                Assert.True(RMCenter_Review_Revise_Submittal_TitleDisplayed);
                Assert.True(RMCenter_RFC_Management_TitleDisplayed);
                Assert.True(RMCenter_Project_Correspondence_Log_TitleDisplayed);
                Assert.True(RMCenter_Comment_Summary_TitleDisplayed);
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
                Assert.True(qa_Test_Original_Report_FormIsDisplayed);
                Assert.True(qa_Test_Correction_ReportTitleDisplayed);
                Assert.True(qa_Test_AllTitleDisplayed);
                Assert.True(qa_Test_Retest_ReportTitleDisplayed);
                Assert.True(qa_DIRsTitleDisplayed);
                Assert.True(general_NCRTitleDisplayed);
                Assert.True(general_CDRTitleDisplayed);
                Assert.True(retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed);
                Assert.True(concrete_Paving_Quantity_TrackerTitleDisplayed);
                Assert.True(mpl_TrackerTitleDisplayed);
                Assert.True(girder_TrackerTitleDisplayed);
                Assert.True(qmsDocumentTitleDisplayed);
                Assert.True(environmental_DocumentTitleDisplayed);
            });


        }
    }
    #endregion


    #region Implementation specific to I15South
    public class LinkCoverage_I15South : LinkCoverage
    {

    }
    #endregion


    #region Implementation specific to I15Tech
    public class LinkCoverage_I15Tech : LinkCoverage
    {
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
                Assert.True(qa_Test_Original_Report_FormIsDisplayed);
                Assert.True(qa_Test_Correction_ReportTitleDisplayed);
                Assert.True(qa_Test_AllTitleDisplayed);
                Assert.True(qa_Test_Retest_ReportTitleDisplayed);
                Assert.True(qa_DIRsTitleDisplayed);
                Assert.True(general_NCRTitleDisplayed);
                Assert.True(general_CDRTitleDisplayed);
                Assert.True(retaining_Wall_BackFill_Quantity_TrackerTitleDisplayed);
                Assert.True(concrete_Paving_Quantity_TrackerTitleDisplayed);
                Assert.True(mpl_TrackerTitleDisplayed);
                Assert.True(girder_TrackerTitleDisplayed);
            });


        }
    }

    #endregion
}
