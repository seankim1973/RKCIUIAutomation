using NUnit.Framework;
using RKCIUIAutomation.Config;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.Search;

namespace RKCIUIAutomation.Test.UploadSubmittals
{
    [Parallelizable]
    [TestFixture]
    public class RMCenter_UploadQA_Submittals_Test : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187500)]
        [Property(Priority, "High")]
        [Description("End to end flow for Upload QA Submittals")]
        public void Submit_And_Forward_End_To_End()
        {
            UploadQASubmittal.LogintoSubmittal(UserType.Bhoomi);

            //Enter Name and Title
            var valuePair = UploadQASubmittal.PopulateFields();

            //Filter record by Number
            AddAssertionToList(UploadQASubmittal.VerifySubmittalNumberIsDisplayed(valuePair[Page.PageObjects.RMCenter.UploadQASubmittal.ColumnName.SubmittalNumber]), "VerifySubmittalNumberIsDisplayed - ReviseReviewSubmittal");
            //Click on Edit
            PageAction.WaitForPageReady();
            GridHelper.ClickButtonForRow(Page.TableHelper.TableButton.Edit, string.Empty, false);
            ClickSubmitForward();

            //Go to RMCenter > Search
            NavigateToPage.RMCenter_Search();
            //Filter record by Number
            AddAssertionToList(UploadQASubmittal.VerifySubmittalNumberIsDisplayed(valuePair[Page.PageObjects.RMCenter.UploadQASubmittal.ColumnName.SubmittalNumber], true), "VerifySubmittalNumberIsDisplayed - Search");

            //Validate all assertions
            AssertAll();
        }

        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187500)]
        [Property(Priority, "High")]
        [Description("End to end flow for Upload QA Submittals using Save button")]
        public void Save_Submit_And_Forward_End_To_End()
        {
            UploadQASubmittal.LogintoSubmittal(UserType.Bhoomi);

            //Enter Name and Title
            var valuePair = UploadQASubmittal.PopulateFields(true);

            //Filter record by Number and Validate for "New" Status
            AddAssertionToList(UploadQASubmittal.VerifySubmittalNumberIsDisplayed(valuePair[Page.PageObjects.RMCenter.UploadQASubmittal.ColumnName.SubmittalNumber], false, true), "VerifySubmittalNumberIsDisplayed - ReviseReviewSubmittal");

            //Click on Edit in Revise Review page
            PageAction.WaitForPageReady();
            GridHelper.ClickButtonForRow(Page.TableHelper.TableButton.Edit, string.Empty, false);
            ClickSubmitForward();

            //Filter record by Number and Validate for "In Progress" Status
            AddAssertionToList(UploadQASubmittal.VerifySubmittalNumberIsDisplayed(valuePair[Page.PageObjects.RMCenter.UploadQASubmittal.ColumnName.SubmittalNumber], false, false), "VerifySubmittalNumberIsDisplayed - ReviseReviewSubmittal");

            //Click on Edit in Revise Review page
            PageAction.WaitForPageReady();
            GridHelper.ClickButtonForRow(Page.TableHelper.TableButton.Edit, string.Empty, false);
            ClickSubmitForward();

            //Go to RMCenter > Search
            NavigateToPage.RMCenter_Search();
            //Filter record by Number
            AddAssertionToList(UploadQASubmittal.VerifySubmittalNumberIsDisplayed(valuePair[Page.PageObjects.RMCenter.UploadQASubmittal.ColumnName.SubmittalNumber], true), "VerifySubmittalNumberIsDisplayed - Search");

            //Validate all assertions
            AssertAll();
        }

        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187525)]
        [Property(Priority, "High")]
        [Description("Verify filtering of Submittals table by column names.")]
        public void Review_Revise_Filters()
        {
            UploadQASubmittal.LogintoSubmittal(UserType.Bhoomi);
            
            var values = UploadQASubmittal.PopulateFields();

            //Verify column values are filtering properly
            UploadQASubmittal.VerifyIfRecordsAreDisplayedByFilter(values);

            AssertAll();
        }

        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187523)]
        [Property(Priority, "High")]
        [Description("Search behavior validation")]
        public void RM_Center_Search()
        {
            UploadQASubmittal.LogintoSubmittal(UserType.Bhoomi);

            var valuePair = UploadQASubmittal.PopulateFields();

            //Go to RMCenter > Search
            NavigateToPage.RMCenter_Search();

            RMCenterSearch.VerifySearchResultByCriteria(
                new KeyValuePair<string, string>(
                    valuePair[Page.PageObjects.RMCenter.UploadQASubmittal.ColumnName.SubmittalTitle], 
                    valuePair[Page.PageObjects.RMCenter.UploadQASubmittal.ColumnName.SubmittalNumber]), 
                SearchCriteria.Title);

            AssertAll();
        }
    }

    
}
