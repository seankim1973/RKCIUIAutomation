﻿using static RKCIUIAutomation.Page.Action;
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
        [Property("Component2",Component.OV_Test)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for OV Menu only")]
        public override void NavigateToOVMenu() => Instance._NavigateToOVMenu();

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
        [Description("Verify Page Title for Project Configuration Menu only")]
        public override void NavigateToVerifyProjectConfigurationMenu() => Instance._NavigateToVerifyProjectConfigurationMenu();

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
        [Description("Verify Page Title for QA Lab Menu only")]
        public override void NavigateToVerifyQALabMenu() => Instance._NavigateToVerifyQALabMenu();

        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for QA Record Control Menu only")]
        public override void NavigateToVerifyQARecordControlMenu() => Instance._NavigateToVerifyQARecordControlMenu();
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
        void _NavigateToRMCenterMenu();
    }
    #endregion <-- end of Workflow Interface class


    #region Common Workflow Implementation class
    public abstract class LinkCoverage_Impl : TestBase, ILinkCoverage
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public static T SetClass<T>() => (T)SetPageClassBasedOnTenant();
        public static ILinkCoverage SetPageClassBasedOnTenant()
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
            LogInfo($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);
            //Navigate.Menu(Navigate)
            //Navigate.Menu(.Menu.QA_Test_Original_Report);
            //Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Correction_Report);


            // Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_All);
            // Navigate.Menu(NavMenu.QARecordControl.Menu.QA_DIRs);

            // Navigate.Menu(NavMenu.QARecordControl.Menu.General_NCR);
            // Navigate.Menu(NavMenu.QARecordControl.Menu.General_CDR);
            // Navigate.Menu(NavMenu.QARecordControl.Menu.Retaining_Wall_BackFill_Quantity_Tracker);
            // Navigate.Menu(NavMenu.QARecordControl.Menu.Concrete_Paving_Quantity_Tracker);
            // Navigate.Menu(NavMenu.QARecordControl.Menu.MPL_Tracker);
            // Navigate.Menu(NavMenu.QARecordControl.Menu.Girder_Tracker);

            // Navigate.Menu(NavMenu.QARecordControl.Menu.Qms_Document); //DONE - Create a method to go to QMS, for ex: GotoQMSDocs()


        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public abstract void NavigateToVerifyQAEngineerMenu();
        public virtual void _NavigateToVerifyQAEngineerMenu()
        {
            LogInfo($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);


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


    #region Implementation specific to SGWay
    public class LinkCoverage_SH249 : LinkCoverage
    {
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
    }

    #endregion
}
