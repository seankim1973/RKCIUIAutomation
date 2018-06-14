using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Config.ProjectProperties;
using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.Navigation;

namespace RKCIUIAutomation.Test
{
    [TestFixture]

    public class LinkCoverage :TestBase
    {
        [Test]
        [Category(Component.Project_Configuration)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for Project Configuration Menu only")]
        public void NavigateToVerifyProjectConfigurationMenu()
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

        [Test]
        [Category(Component.Project_Configuration)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for QA Lab Menu only")]
        public void NavigateToVerifyQALabMenu()
        {
            LogInfo($"Project Configuration component test - This test should run");
            LoginAs(UserType.Bhoomi);


            NavigateToPage.QALab_Technician_Random();
            Assert.True(VerifyPageTitle("Technician Random"));
            NavigateToPage.QALab_BreakSheet_Creation();
            Assert.True(VerifyPageTitle("Create Break Sheet"));
            NavigateToPage.QALab_BreakSheet_Legacy();
            Assert.True(VerifyPageTitle("Break Sheet Legacy"));
            NavigateToPage.QALab_Equipment_Management();
            Assert.True(VerifyPageTitle("Equipment"));
    
            NavigateToPage.QALab_BreakSheet_Forecast();
            Assert.True(VerifyPageTitle("Break Sheet Forecast"));
           
            NavigateToPage.QALab_Cylinder_PickUp_List();
           Assert.True(VerifyPageTitle("Cylinder Pick-Up Status:"));
   
            NavigateToPage.QALab_Early_Break_Calendar();
            Assert.True(VerifyPageTitle(""));

        }

        [Test]
        [Category(Component.Project_Configuration)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for QA Record Control Menu only")]
        public void NavigateToVerifyQARecordControlMenu()
        {
            LogInfo($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);
           //Navigate.Menu(Navigate)
   //avigate.Menu(.Menu.QA_Test_Original_Report);
           Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Correction_Report);
           
    
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

        [Test]
        [Category(Component.Testing_Module)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for QA Engineer Menu only")]
        public void NavigateToVerifyQAEngineerMenu()
        {
            LogInfo($"Testing module, DIR, NCR, CDR and trackers component - This test should run");
            LoginAs(UserType.Bhoomi);
            

        }

    }
}
