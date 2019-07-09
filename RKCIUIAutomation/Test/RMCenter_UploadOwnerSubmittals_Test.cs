using NUnit.Framework;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.Search;

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
            UploadOwnerSubmittal.LogintoSubmittal(UserType.Bhoomi);
            ClickSubmitForward();

            //Enter Name and Title
            var valuePair = UploadOwnerSubmittal.PopulateFields();

            //Filter record by Number
            AddAssertionToList(UploadOwnerSubmittal.VerifySubmittalNumberIsDisplayed(valuePair.Value), "VerifySubmittalNumberIsDisplayed - ReviseReviewSubmittal");
            //Click on Edit
            PageAction.WaitForPageReady();
            GridHelper.ClickButtonForRow(Page.TableHelper.TableButton.Edit, string.Empty, false);
            ClickSubmitForward();

            //Go to RMCenter > Search
            NavigateToPage.RMCenter_Search();
            //Filter record by Number
            AddAssertionToList(UploadOwnerSubmittal.VerifySubmittalNumberIsDisplayed(valuePair.Value, true), "VerifySubmittalNumberIsDisplayed - Search");

            //Validate all assertions
            AssertAll();
        }

        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187525)]
        [Property(Priority, "High")]
        [Description("Verify filtering of Submittals table by column names.")]
        public void QASubmittals_Filters()
        {
            UploadOwnerSubmittal.LogintoSubmittal(UserType.Bhoomi);
            var valuePair = UploadOwnerSubmittal.PopulateFields();
            AddAssertionToList(UploadOwnerSubmittal.VerifySubmittalNumberIsDisplayed(valuePair.Value, true), "VerifySubmittalNumberIsDisplayed - Search");
            AssertAll();
        }

        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187523)]
        [Property(Priority, "High")]
        [Description("Search behavior validation")]
        public void QASubmittals_Search()
        {
            UploadOwnerSubmittal.LogintoSubmittal(UserType.Bhoomi);
            ClickSubmitForward();

            var valuePair = UploadOwnerSubmittal.PopulateFields();

            //Go to RMCenter > Search
            NavigateToPage.RMCenter_Search();

            RMCenterSearch.VerifySearchResultByCriteria(valuePair, SearchCriteria.Title);
            AssertAll();
        }
    }
}
