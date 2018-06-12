using NUnit.Framework;
using System.Threading;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.PageObjects.RMCenter.Search;

using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Config.ProjectProperties;


namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class Smoke : TestBase
    {
        //[Test]
        //[Category("")]
        [Property("TC#", "ELVS2345")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can login successfully using project - admin account")]
        public void VerifyUserCanLogin_ProjAdmin()
        {
            LoginAs(UserType.ProjAdmin);
        }


        //[Test]
        //[Category("")]
        [Property("TC#", "ELVS1234")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void VerifyUserCanLogin_ProjUser()
        {
            LoginAs(UserType.ProjAdmin);
        }

        //[Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS3456")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void GenericTest()
        {
            LoginAs(UserType.ProjAdmin);
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");            
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Action, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Segment_Area, 1);
            //ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Location, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Feature, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Grade, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Supplier, 1);
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Specification, 1);
            EnterText(SubmittalDetails.Input_Quantity, "50");
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.QuantityUnit, 1);
            ClickElement(SubmittalDetails.Btn_SelectFiles);
            UploadFile("test.xlsx");
            ClickSubmitForward();

            Thread.Sleep(5000);
        }

        //[Test]
        //[Category("")]
        [Property("TC#", "ELVS8988")]
        [Property("Priority", "Priority 1")]
        [Description("Verify proper required fields show error when clicking Save button without Submittal Name and Title")]
        public void VerifyRequiredFieldErrorsClickingSaveWithoutNameAndTitle()
        {
            LoginAs(UserType.ProjAdmin);
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);
            ClickSave();
            Assert.Multiple(testDelegate: () =>
               {
                   Assert.True(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_Name));
                   Assert.True(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_SubmittalTitle));
                   
                   /**uncomment Assert statement below to fail test*/
                   //Assert.True(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_DDListAction));  
               });

            Thread.Sleep(5000);
        }

        //[Test]
        //[Category("")]
        [Property("TC#", "ELVS1111")]
        [Property("Priority", "Priority 1")]
        [Description("Verify proper required fields show error when clicking Save button with Submittal Name and Title")]
        public void VerifyRequiredFieldErrorsClickingSaveWithNameAndTitle()
        {
            LoginAs(UserType.ProjAdmin);
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ClickSave();
            Assert.True(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_DDListAction));

            Thread.Sleep(5000);
        }


        //[Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify success message is shown when clicking Save button with Submittal Name, Title and Action DDL")]
        public void VerifySuccessMsgClickingSaveWithNameTitleAndActionDDL()
        {
            LoginAs(UserType.ProjAdmin);
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Action, 1);
            ClickSave();
            Assert.True(VerifySuccessMessageIsDisplayed());

            Thread.Sleep(5000);
        }

        //[Test]
        [Category(Component.Control_Point)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyComponentTestIsSkipped()
        {
            LogInfo($"Control Point component test - This test should be skipped");
            LoginAs(UserType.Bhoomi);
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);
            Thread.Sleep(5000);
        }

        //[Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyComponentTestRuns()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.ProjAdmin);
            Navigate.Menu(NavMenu.Project.Menu.My_Details);
            Assert.True(VerifyPageTitle("Account Details"));
            Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Roles);
            Assert.True(VerifyPageTitle("Roles"));
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Gradations);
            Assert.True(VerifyPageTitle("Gradations"));
        }

        //[Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void TestNewGWQALabMenu()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Forecast);
            Navigate.Menu(NavMenu.QALab.Menu.Cylinder_PickUp_List);
            Navigate.Menu(NavMenu.QALab.Menu.Early_Break_Calendar);
        }

        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void LatestTest()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Search();
            RMCenter_SearchPage.PopulateAllSearchCriteriaFields();
            Assert.True(VerifyPageTitle("RM Center Search"));
        }

        //[Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- Link Covereage")]
        public void VerifyLinkCoverage()
        {
            LoginAs(UserType.Bhoomi);

            //Project Menu
            NavigateToPage.My_Details();
            Assert.True(VerifyPageTitle("My Details"));

            NavigateToPage.Qms_Document();
            Assert.True(VerifyPageTitle("QMS Documents"));
            
            
            //Project>Administration
            Navigate.Menu(NavMenu.Project.Administration.Menu.Project_Details);
            Navigate.Menu(NavMenu.Project.Administration.Menu.Companies);
            Navigate.Menu(NavMenu.Project.Administration.Menu.Contracts);
            Navigate.Menu(NavMenu.Project.Administration.Menu.Menu_Editor);
            Navigate.Menu(NavMenu.Project.Administration.Menu.Support);

            //Project>Administration>User Management
            Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Roles);
            Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Users);
            Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Access_Rights);

            //Project>Administration>System Configuration
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Disciplines);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Actions);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Requirements);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Submittal_Types);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.CVL_Lists);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.CVL_List_Items);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Notifications);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Sieves);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Menu.Gradations);

            //Project>Administration>System Configuration>Equipment
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Makes);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Types);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.Equipment.Menu.Equipment_Models);

            //Project>Administration>System Configuration>Grade Management
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grade_Types);
            Navigate.Menu(NavMenu.Project.Administration.SystemConfiguration.GradeManagement.Menu.Grades);

            //Project>Administration>Admin Tools
            Navigate.Menu(NavMenu.Project.Administration.AdminTools.Menu.Locked_Records);


            //QA Lab Menu
            Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Creation);
            Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Legacy);
            Navigate.Menu(NavMenu.QALab.Menu.Equipment_Management);
            Navigate.Menu(NavMenu.QALab.Menu.Technician_Random);

            //QA Records Control Menu
            Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Original_Report);
            Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_Correction_Report);
            Navigate.Menu(NavMenu.QARecordControl.Menu.QA_Test_All);
            Navigate.Menu(NavMenu.QARecordControl.Menu.QA_DIRs);
            Navigate.Menu(NavMenu.QARecordControl.Menu.General_NCR);
            Navigate.Menu(NavMenu.QARecordControl.Menu.General_CDR);
            Navigate.Menu(NavMenu.QARecordControl.Menu.Retaining_Wall_BackFill_Quantity_Tracker);
            Navigate.Menu(NavMenu.QARecordControl.Menu.Concrete_Paving_Quantity_Tracker);
            Navigate.Menu(NavMenu.QARecordControl.Menu.MPL_Tracker);
            Navigate.Menu(NavMenu.QARecordControl.Menu.Girder_Tracker);
            Navigate.Menu(NavMenu.QARecordControl.Menu.Qms_Document); //DONE - Create a method to go to QMS, for ex: GotoQMSDocs()

            //QA Engineer Menu
            Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Lab_Supervisor_Review);
            Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Field_Supervisor_Review);
            Navigate.Menu(NavMenu.QAEngineer.Menu.QA_Test_Authorization);
            Navigate.Menu(NavMenu.QAEngineer.Menu.DIR_QA_Review_Approval);

            //Reports & Notices Menu
            Navigate.Menu(NavMenu.ReportsNotices.Menu.General_NCR);
            Navigate.Menu(NavMenu.ReportsNotices.Menu.General_DN);

            //QA Search
            Navigate.Menu(NavMenu.QASearch.Menu.QA_Tests);
            Navigate.Menu(NavMenu.QASearch.Menu.QA_Test_Summary_Search);
            Navigate.Menu(NavMenu.QASearch.Menu.QA_Guide_Schedule_Summary_Report);
            Navigate.Menu(NavMenu.QASearch.Menu.Inspection_Deficiency_Log_Report);
            Navigate.Menu(NavMenu.QASearch.Menu.Daily_Inspection_Report);
            Navigate.Menu(NavMenu.QASearch.Menu.DIR_Summary_Report);
            Navigate.Menu(NavMenu.QASearch.Menu.DIR_Checklist_Search); //DONE - Add DIR Checklist Search under this menu)
            Navigate.Menu(NavMenu.QASearch.Menu.Ncr_Log_View); //DONE - Add Ncr Log View - I15South, I15Tech
            Navigate.Menu(NavMenu.QASearch.Menu.QMS_Document_Search); //DONE - Add QMS Document Search under this menu)

            //QA Field Menu
            Navigate.Menu(NavMenu.QAField.Menu.QA_Test);
            Navigate.Menu(NavMenu.QAField.Menu.QA_DIRs);
            Navigate.Menu(NavMenu.QAField.Menu.QA_Technician_Random_Search);
            Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Monitoring); //DONE - Add Weekly Environmental Inspection
            Navigate.Menu(NavMenu.QAField.Menu.Daily_Environmental_Inspection); //DONE - Add Weekly Envorinmental Monitoring
            Navigate.Menu(NavMenu.QAField.Menu.Weekly_Environmental_Inspection); //DONE - Add Daily Environmental Inspection

            //Control Point Menu
            Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Log);
            Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Scheduler);

            //Owner Menu
            Navigate.Menu(NavMenu.Owner.Menu.Owner_DIRs);
            Navigate.Menu(NavMenu.Owner.Menu.Owner_NCRs);

            //Material Mix Code Menu
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_PCC);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_HMA);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_JMF); //DONE - Add mix design JMF menu item.
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Sieve_Analyses_IOC); //DONE - Add Mix design IOC menu item
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Base_Aggregate);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Concrete_Aggregate);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_HMA_Aggregate);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Raw_Material);

            //RM Center Menu
            Navigate.Menu(NavMenu.RMCenter.Menu.Search);
            Navigate.Menu(NavMenu.RMCenter.Menu.Design_Documents); //DONE - Add Design Document Menu Item
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_Owner_Submittal);
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_DEV_Submittal);
            Navigate.Menu(NavMenu.RMCenter.Menu.DOT_Project_Correspondence_Log);
            Navigate.Menu(NavMenu.RMCenter.Menu.Review_Revise_Submittal);
            Navigate.Menu(NavMenu.RMCenter.Menu.RFC_Management);
            Navigate.Menu(NavMenu.RMCenter.Menu.Project_Transmittal_Log);
            Navigate.Menu(NavMenu.RMCenter.Menu.Project_Correspondence_Log);
            Navigate.Menu(NavMenu.RMCenter.Menu.Comment_Summary);

            Thread.Sleep(5000);
        }


        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void TestDynamicNavigation()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Qms_Document();           
            Assert.True(VerifyPageTitle("QMS Documents"));
        }

        [Test]
        [Category(Component.Project_Configuration)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for Project Configuration Menu only")]
        public void NavigateToVerifyProjectConfigurationMenu()
        {
            LogInfo($"Other component test - This test should run");
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
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
          

            NavigateToPage.QALab_Technician_Random();
            Assert.True(VerifyPageTitle("Technician Random"));
            NavigateToPage.QALab_BreakSheet_Creation();
            Assert.True(VerifyPageTitle("Create Break Sheet"));
            NavigateToPage.QALab_BreakSheet_Legacy();
            Assert.True(VerifyPageTitle("Break Sheet Legacy"));
            NavigateToPage.QALab_Equipment_Management();
            Assert.True(VerifyPageTitle("Equipment"));
            //MIssing Breaksheet Forcastmenuitem In SG
            // NavigateToPage.Brea();
            //  Assert.True(VerifyPageTitle("Break Sheet Forecast"));
            //missing Cylinder PIck up
            //NavigateToPage.QALab_Technician_Random();
            //Assert.True(VerifyPageTitle("Cylinder Pick-Up Status:"));
            //Early Break Calendar
            //NavigateToPage.QALab_Technician_Random();
            //Assert.True(VerifyPageTitle(""));
           
        }

        [Test]
        [Category(Component.Project_Configuration)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- for QA Record Control Menu only")]
        public void NavigateToVerifyQARecordControlMenu()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            

          
        }
    }
}
