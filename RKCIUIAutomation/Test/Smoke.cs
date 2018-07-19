using NUnit.Framework;
using System.Threading;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.RMCenter;

namespace RKCIUIAutomation.Test.Smoke
{
    [TestFixture]
    public class Verify_LinkCoverage_Level1 : TestBase
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2345")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Site Pages Load Successfully")]
        public void LinkCoverage_Level1()
        {
            LoginAs(UserType.Bhoomi);

            List<string> pageUrls = new List<string>();
            pageUrls = GetNavMenuUrlList();

            List<bool> results = new List<bool>();
            foreach (var url in pageUrls)
            {
                results.Add(VerifyUrlIsLoaded(url));
            }

            Assert.Multiple(() =>
            {
                foreach (var result in results)
                {
                    Assert.True(result);
                }
            });
        }
    }

    [TestFixture]
    public class Verify_CancelOutOfOwnerSubmittalPage : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS1234")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can cancel out of Owner Submittal Page Successfully")]
        public void VerifyCancelOutOfOwnerSubmittalPage()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_Owner_Submittal();
            AddAssertionToList(VerifyPageTitle("New Submittal"));
            ClickCancel();
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
            AddAssertionToList(VerifyPageTitle("Submittal Details"));
            AssertAll();
        }
    }

    [TestFixture]
    public class Test_Generic : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS3456")]
        [Property("Priority", "Priority 1")]
        [Description("Verify user can login successfully using project - user account")]
        public void Generic()
        {
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
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
    }

    [TestFixture]
    public class Test_RequiredFieldErrorsClickingSaveWithoutNameAndTitle : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS8988")]
        [Property("Priority", "Priority 1")]
        [Description("Verify proper required fields show error when clicking Save button without Submittal Name and Title")]
        public void VerifyRequiredFieldErrorsClickingSaveWithoutNameAndTitle()
        {
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            ClickSave();
            AddAssertionToList(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_Name));
            AddAssertionToList(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_SubmittalTitle));

            /**uncomment Assert statement below to fail test*/
            //AddAssertionToList(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_DDListAction));

            AssertAll();
            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    public class Test_RequiredFieldErrorsClickingSaveWithNameAndTitle : TestBase
    {
        [Test]
        [Category("")]
        [Property("TC#", "ELVS1111")]
        [Property("Priority", "Priority 1")]
        [Description("Verify proper required fields show error when clicking Save button with Submittal Name and Title")]
        public void VerifyRequiredFieldErrorsClickingSaveWithNameAndTitle()
        {
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ClickSave();
            Assert.True(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_DDListAction));

            Thread.Sleep(5000);
        }

    }

    [TestFixture]
    public class Test_SuccessMsgClickingSaveWithNameTitleAndActionDDL : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify success message is shown when clicking Save button with Submittal Name, Title and Action DDL")]
        public void VerifySuccessMsgClickingSaveWithNameTitleAndActionDDL()
        {
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Action, 1);
            ClickSave();
            Assert.True(VerifySuccessMessageIsDisplayed());

            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    public class Test_ComponentTestIsSkipped : TestBase
    {
        [Test]
        [Category(Component.Control_Point)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyComponentTestIsSkipped()
        {
            LogInfo($"Control Point component test - This test should be skipped");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            Thread.Sleep(5000);
        }
    }

    [TestFixture]
    public class Test_ComponentTestRuns : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void VerifyComponentTestRuns()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.ProjAdmin);
            NavigateToPage.My_Details();
            Assert.True(VerifyPageTitle("Account Details"));
            NavigateToPage.UserMgmt_Roles();
            Assert.True(VerifyPageTitle("Roles"));
            NavigateToPage.SysConfig_Gradations();
            Assert.True(VerifyPageTitle("Gradations"));
        }
    }

    [TestFixture]
    public class Test_NewGWQALabMenu : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void NewGWQALabMenu()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QALab_BreakSheet_Forecast();
            NavigateToPage.QALab_Cylinder_PickUp_List();
            NavigateToPage.QALab_Early_Break_Calendar();
        }

    }

    [TestFixture]
    public class Test_DynamicNavigation : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void DynamicNavigation()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.Qms_Document();           
            Assert.True(VerifyPageTitle("QMS Documents"));
        }
    }

    [TestFixture]
    public class Test_CommentReviewPartialFunctions : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void DynamicNavigation()
        {
            LogInfo($"Other component test - This test should run");
            LoginAs(UserType.IQFUse);//testing glx comment review
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Documents"));
            RMCenter_DesignDocumentCommentReview.CreateDocument();
            Thread.Sleep(5000);
        }
    }

}
