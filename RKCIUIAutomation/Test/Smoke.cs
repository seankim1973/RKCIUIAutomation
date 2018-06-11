using NUnit.Framework;
using System.Threading;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects.RMCenter;

using static RKCIUIAutomation.Page.Action;
using static RKCIUIAutomation.Config.ProjectProperties;
using System.Collections.Generic;
using OpenQA.Selenium;
using System;
using RKCIUIAutomation.Page.PageObjects.RMCenter.Search;
using RKCIUIAutomation.Page;

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
            LoginPg.LoginUser(UserType.ProjAdmin);
        }


        //[Test]
        //[Category("")]
        [Property("TC#", "ELVS1234")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void VerifyUserCanLogin_ProjUser()
        {
            LoginPg.LoginUser(UserType.ProjAdmin);
        }

        //[Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS3456")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void GenericTest()
        {
            LoginPg.LoginUser(UserType.ProjAdmin);
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
            LoginPg.LoginUser(UserType.ProjAdmin);
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
            LoginPg.LoginUser(UserType.ProjAdmin);
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
            LoginPg.LoginUser(UserType.ProjAdmin);
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
            LoginPg.LoginUser(UserType.Bhoomi);
            Navigate.Menu(NavMenu.RMCenter.Menu.Upload_QA_Submittal);
            Thread.Sleep(5000);
        }

        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyComponentTestRuns()
        {
            LogInfo($"Other component test - This test should run");
            LoginPg.LoginUser(UserType.ProjAdmin);
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
            LoginPg.LoginUser(UserType.Bhoomi);
            Navigate.Menu(NavMenu.QALab.Menu.BreakSheet_Forecast);
            Navigate.Menu(NavMenu.QALab.Menu.Cylinder_PickUp_List);
            Navigate.Menu(NavMenu.QALab.Menu.Early_Break_Calendar);
        }

        //[Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void LatestTest()
        {
            LogInfo($"Other component test - This test should run");
            LoginPg.LoginUser(UserType.ProjAdmin);
            Navigate.Menu(NavMenu.RMCenter.Menu.Search);

            var search = Search.SetClass<ISearch>(); 
            search.PopulateAllSearchCriteriaFields();
            Assert.True(VerifyPageTitle("RM Center Search"));
        }

        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name- Link Covereage")]
        public void VerifyLinkCoverage()
        {
           
            LoginPg.LoginUser(UserType.Bhoomi);

            //Project Menu
            Navigate.Menu(NavMenu.Project.Menu.My_Details);
            Navigate.Menu(NavMenu.Project.Administration.Menu.Companies);
            Navigate.Menu(NavMenu.Project.Administration.Menu.Contracts);
            Navigate.Menu(NavMenu.Project.Administration.Menu.Project_Details);
            Navigate.Menu(NavMenu.Project.Administration.UserManagement.Menu.Access_Rights);

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
            Navigate.Menu(NavMenu.Project.Menu.Qms_Document); //Create a method to go to QMS, for ex: GotoQMSDocs()

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
            // Navigate.Menu(NavMenu.QASearch.Menu);//Add DIR Checklist Search under this menu)
            //Navigate.Menu(NavMenu.QASearch.Menu.Qm);--Add QMS Document Search under this menu)

            //QA Field Menu
            Navigate.Menu(NavMenu.QAField.Menu.QA_Test);
            Navigate.Menu(NavMenu.QAField.Menu.QA_DIRs);
            Navigate.Menu(NavMenu.QAField.Menu.QA_Technician_Random_Search);
            //Add Weekly Environmental Inspection, Weekly Envorinmental Monitoring, Daily Environmental Inspection

            //Control Point Menu
            Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Log);
            Navigate.Menu(NavMenu.ControlPoint.Menu.Control_Point_Scheduler);

            //Owner Menu
            Navigate.Menu(NavMenu.Owner.Menu.Owner_DIRs);
            Navigate.Menu(NavMenu.Owner.Menu.Owner_NCRs);

            //Material Mix Code Menu
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_PCC);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix_Design_HMA);
           // Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Mix); Add mix design JMF menu item.
           // Navigate.Menu(NavMenu.MaterialMixCodes.Menu.m); -- Add Mix design IOC menu item
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Base_Aggregate);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Concrete_Aggregate);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_HMA_Aggregate);
            Navigate.Menu(NavMenu.MaterialMixCodes.Menu.Material_Code_Raw_Material);

            //RM Center Menu
            Navigate.Menu(NavMenu.RMCenter.Menu.Search);
            //Navigate.Menu(NavMenu.RMCenter.Menu.);-- Add Design Document Menu Item
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

    }
}
