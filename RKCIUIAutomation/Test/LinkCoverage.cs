using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Config.ProjectProperties;
using NUnit.Framework;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Test
{
    //NUnit Test Case Methods
    [TestFixture]
    public class LinkCoverage : LinkCoverage_Impl
    {
        ILinkCoverage Instance => SetClass<ILinkCoverage>();

        [Test]
        [Category(Component.Project_Configuration)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for Project Configuration Menu only")]
        public override void NavigateToVerifyProjectConfigurationMenu() => Instance._NavigateToVerifyProjectConfigurationMenu();

        [Test]
        [Category(Component.Testing_Module)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for QA Engineer Menu only")]
        public override void NavigateToVerifyQAEngineerMenu() => Instance._NavigateToVerifyQAEngineerMenu();

        [Test]
        [Category(Component.Project_Configuration)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for QA Lab Menu only")]
        public override void NavigateToVerifyQALabMenu() => Instance._NavigateToVerifyQALabMenu();

        [Test]
        [Category(Component.Project_Configuration)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for QA Record Control Menu only")]
        public override void NavigateToVerifyQARecordControlMenu() => Instance._NavigateToVerifyQARecordControlMenu();
    }



    //Workflow interfaces
    public interface ILinkCoverage
    {
        void _NavigateToVerifyProjectConfigurationMenu();
        void _NavigateToVerifyQALabMenu();
        void _NavigateToVerifyQARecordControlMenu();
        void _NavigateToVerifyQAEngineerMenu();
    }



    // Common workflow implementations
    public abstract class LinkCoverage_Impl : TestBase, ILinkCoverage
    {
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


        public abstract void NavigateToVerifyQAEngineerMenu();
        public virtual void _NavigateToVerifyQAEngineerMenu()
        {
            LogInfo($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);


        }


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
        /// Common workflow methof for Tenants: GLX, I15South, I15Tech, & SH249
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
                LogInfo($"###### using LinkCoverage_SH249 instance ###### ");
                instance = new LinkCoverage_Garnet();
            }
            else if (projectName == ProjectName.GLX)
            {
                LogInfo($"###### using LinkCoverage_SH249 instance ###### ");
                instance = new LinkCoverage_GLX();
            }
            else if (projectName == ProjectName.I15South)
            {
                LogInfo($"###### using LinkCoverage_SH249 instance ###### ");
                instance = new LinkCoverage_I15South();
            }
            else if (projectName == ProjectName.I15Tech)
            {
                LogInfo($"###### using LinkCoverage_SH249 instance ###### ");
                instance = new LinkCoverage_I15Tech();
            }

            return instance;
        }
    }



    // Tenant based implementations
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

    public class LinkCoverage_GLX : LinkCoverage
    {
    }

    public class LinkCoverage_SH249 : LinkCoverage
    {
    }

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
    }

    public class LinkCoverage_I15South : LinkCoverage
    {
    }

    public class LinkCoverage_I15Tech : LinkCoverage
    {
    }
}
