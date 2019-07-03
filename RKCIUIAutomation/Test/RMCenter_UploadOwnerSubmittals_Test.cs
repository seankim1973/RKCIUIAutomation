using NUnit.Framework;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.Factory;


namespace RKCIUIAutomation.Test.UploadSubmittals
{
    [Parallelizable]
    [TestFixture]
    public class RMCenter_UploadOwnerSubmittals_Test : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187500)]
        [Property(Priority, "High")]
        [Description("End to end flow for Upload QA Submittals")]
        public void QASubmittals_End_To_End()
        {
            UploadOwnerSubmittal.LogintoQASubmittal(UserType.Bhoomi);
            ClickSubmitForward();

            //Enter Name and Title
            string submittalNumber = UploadOwnerSubmittal.PopulateFields(1);
            ClickSubmitForward();

            //Enter other required fields - Action, Segment/Area, Feature, Grade, Specification, Attachment
            UploadOwnerSubmittal.PopulateFields(2);
            ClickSubmitForward();

            //Filter record by Number
            AddAssertionToList(UploadOwnerSubmittal.VerifySubmittalNumberIsDisplayed(submittalNumber), "VerifySubmittalNumberIsDisplayed - ReviseReviewSubmittal");
            //Click on Edit
            PageAction.WaitForPageReady();
            GridHelper.ClickButtonForRow(Page.TableHelper.TableButton.Edit, string.Empty, false);
            ClickSubmitForward();

            //Go to RMCenter > Search
            NavigateToPage.RMCenter_Search();
            //Filter record by Number
            AddAssertionToList(UploadOwnerSubmittal.VerifySubmittalNumberIsDisplayed(submittalNumber, true), "VerifySubmittalNumberIsDisplayed - Search");

            //Validate all assertions
            AssertAll();
        }
    }
}
