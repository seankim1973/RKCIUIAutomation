using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using System.Threading;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class Smoke : TestBase
    {
        //[Test, Property("TC#", "ELVS2345"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify user can login successfully using project - admin account")]
        public void VerifyUserCanLogin_ProjAdmin()
        {
            LoginPg.LoginUser(UserType.ProjAdmin);
        }


        //[Test, Property("TC#","ELVS1234"), Property("Priority", "Priority 2")]
        [Category("QA Test")]
        [Description("Verify user can login successfully using project - user account")]
        public void VerifyUserCanLogin_ProjUser()
        {
            LoginPg.LoginUser(UserType.ProjAdmin);
        }

        [Test, Property("TC#", "ELVS3456"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify user can login successfully using project - user account")]
        public void GenericTest()
        {
            LoginPg.LoginUser(UserType.ProjAdmin);
            Navigate.Menu(NavEnums.RMCenter_e.Upload_QA_Submittal);
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

        [Test, Property("TC#", "ELVS3456"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify proper required fields show error when clicking Save button without Submittal Name and Title")]
        public void VerifyRequiredFieldErrorsClickingSaveWithoutNameAndTitle()
        {
            LoginPg.LoginUser(UserType.ProjAdmin);
            Navigate.Menu(NavEnums.RMCenter_e.Upload_QA_Submittal);
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

        [Test, Property("TC#", "ELVS3456"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify proper required fields show error when clicking Save button with Submittal Name and Title")]
        public void VerifyRequiredFieldErrorsClickingSaveWithNameAndTitle()
        {
            LoginPg.LoginUser(UserType.ProjAdmin);
            Navigate.Menu(NavEnums.RMCenter_e.Upload_QA_Submittal);
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ClickSave();
            Assert.True(VerifyFieldErrorIsDisplayed(SubmittalDetails.Err_DDListAction));

            Thread.Sleep(5000);
        }


        [Test, Property("TC#", "ELVS3456"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify success message is shown when clicking Save button with Submittal Name, Title and Action DDL")]
        public void VerifySuccessMsgClickingSaveWithNameTitleAndActionDDL()
        {
            LoginPg.LoginUser(UserType.ProjAdmin);
            Navigate.Menu(NavEnums.RMCenter_e.Upload_QA_Submittal);
            EnterText(SubmittalDetails.Input_Name, "Test Name");
            EnterText(SubmittalDetails.Input_SubmittalTitle, "Test Title");
            ExpandAndSelectFromDDList(SubmittalDetails.DDListID.Action, 1);
            ClickSave();
            Assert.True(VerifySuccessMessageIsDisplayed());

            Thread.Sleep(5000);
        }

    }
}
