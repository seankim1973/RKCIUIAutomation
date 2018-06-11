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



    }
}
