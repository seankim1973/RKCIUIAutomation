﻿using NUnit.Framework;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.Factory;
using System.Collections.Generic;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.Search;

namespace RKCIUIAutomation.Test.UploadSubmittals
{
    [Parallelizable]
    [TestFixture]
    public class RMCenter_UploadQASubmittals_Test : TestBase
    {
        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187500)]
        [Property(Priority, "High")]
        [Description("End to end flow for Upload QA Submittals")]
        public void QASubmittals_End_To_End()
        {
            UploadQASubmittal.LogintoSubmittal(UserType.Bhoomi);
            ClickSubmitForward();

            //Enter Name and Title
            var valuePair = UploadQASubmittal.PopulateFields();

            //Filter record by Number
            AddAssertionToList(UploadQASubmittal.VerifySubmittalNumberIsDisplayed(valuePair.Value), "VerifySubmittalNumberIsDisplayed - ReviseReviewSubmittal");
            //Click on Edit
            PageAction.WaitForPageReady();
            GridHelper.ClickButtonForRow(Page.TableHelper.TableButton.Edit, string.Empty, false);
            ClickSubmitForward();

            //Go to RMCenter > Search
            NavigateToPage.RMCenter_Search();
            //Filter record by Number
            AddAssertionToList(UploadQASubmittal.VerifySubmittalNumberIsDisplayed(valuePair.Value, true), "VerifySubmittalNumberIsDisplayed - Search");

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
            UploadQASubmittal.LogintoSubmittal(UserType.Bhoomi);
            var valuePair = UploadQASubmittal.PopulateFields();
            AddAssertionToList(UploadQASubmittal.VerifySubmittalNumberIsDisplayed(valuePair.Value, true), "VerifySubmittalNumberIsDisplayed - Search");
            AssertAll();
        }

        [Test]
        [Category(Component.Submittals)]
        [Property(TestCaseNumber, 2187523)]
        [Property(Priority, "High")]
        [Description("Search behavior validation")]
        public void QASubmittals_Search()
        {
            UploadQASubmittal.LogintoSubmittal(UserType.Bhoomi);
            ClickSubmitForward();

            var valuePair = UploadQASubmittal.PopulateFields();

            //Go to RMCenter > Search
            NavigateToPage.RMCenter_Search();

            RMCenterSearch.VerifySearchResultByCriteria(valuePair, SearchCriteria.Title);
            AssertAll();
        }
        /*
        private bool VerifyTransmittalLogIsDisplayedByGridColumnFilter()
        {
            bool result = false;
            string logMsg = string.Empty;
            IList<bool> resultsList = new List<bool>();

            expectedEntryFieldsForTblColumns = GetTenantEntryFieldsForTableColumns();

            foreach (EntryField entryField in expectedEntryFieldsForTblColumns)
            {
                ColumnName column = GetMatchingColumnNameForEntryField(entryField);

                //tenantAllEntryFieldKeyValuePairs list is generated by PopulateAllFields() method called by CreateNewAndPopulateFields() method
                string value = (from kvp in tenantAllEntryFieldKeyValuePairs where kvp.Key == entryField select kvp.Value).FirstOrDefault();

                //if (entryField.Equals(EntryField.DocumentType) || entryField.Equals(EntryField.Via))
                //{
                //    value = value.ReplaceSpacesWithUnderscores();
                //}

                bool isDisplayed = GridHelper.VerifyRecordIsDisplayed(column, value, Page.TableHelper.TableType.Single);

                resultsList.Add(isDisplayed);

                logMsg = isDisplayed
                    ? ""
                    : " NOT";

                Report.Info($"Column '{column}' in the grid was{logMsg} filtered successfully by value {value}", isDisplayed);
                TestUtility.AddAssertionToList(isDisplayed, $"VerifyTransmittalLogIsDisplayedByGridColumnFilter [Column : {column}]");

                GridHelper.ClearTableFilters(tenantTableType);
                PageAction.WaitForPageReady();
            }

            result = resultsList.Contains(false)
                ? false
                : true;

            return result;
        }

        private ColumnName GetMatchingColumnNameForEntryField(EntryField entryField)
        {
            ColumnName columnName = ColumnName.SubmittalNumber;

            switch (entryField)
            {
                case EntryField.SubmittalNo:
                    columnName = ColumnName.SubmittalNumber;
                    break;
                case EntryField.SubmittalTitle:
                    columnName = ColumnName.SubmittalTitle;
                    break;
            }

            return columnName;
        }
        */
    }

    
}
