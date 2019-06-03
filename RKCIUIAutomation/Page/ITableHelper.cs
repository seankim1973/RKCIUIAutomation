using System;
using OpenQA.Selenium;

namespace RKCIUIAutomation.Page
{
    public interface ITableHelper
    {
        void ClickCommentTab(int commentNumber);
        string GetCurrentTableTabName();
        string GetPdfHref<T>(T textInColumnForRowOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbx = false);
        void ClearTableFilters(TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void ClickButtonForRow<T>(TableHelper.TableButton tableButton, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        void ClickCloseDirBtnForRow(string dirNumber = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = true);
        void ClickCreateBtnForRow(int textInRowForAnyColumn = 1, bool isMultiTabGrid = true);
        void ClickDeleteBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        void ClickDownloadBtnForRow(int rowIndex = 1, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        void ClickEditBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        void ClickEnterBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        void ClickQMSViewAttachmentsForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        void ClickRecreateBtnForRow(int textInRowForAnyColumn = 1, bool isMultiTabGrid = true);
        void ClickReviseBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        void ClickTab(Enum tblTabEnum);
        void ClickTab(string tblTabName);
        void ClickViewAttachmentsForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        By GetTableBtnLocator<T>(TableHelper.TableButton tableButton, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        void ClickViewBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbox = false);
        string ClickViewReportBtnForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true, bool rowEndsWithChkbx = false);
        void ClickViewWebFormForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        string DetermineTblRowBtnXPathExt(TableHelper.TableButton tblBtn, bool rowEndsWithChkbx = false);
        void FilterColumn(Enum columnName, string filterValue, TableHelper.TableType tableType = TableHelper.TableType.Unknown, FilterOperator filterOperator = FilterOperator.EqualTo, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo);
        void FilterTableColumnByValue(Enum columnName, string recordNameOrNumber, TableHelper.TableType tableType = TableHelper.TableType.Unknown, FilterOperator filterOperator = FilterOperator.EqualTo);
        string GetColumnValueForRow<T, C>(T textInRowForAnyColumnOrRowIndex, C getValueFromColumnName, bool isMultiTabGrid = true);
        int GetCurrentPageNumber();
        int GetCurrentViewItemsPerPageSize();
        By GetGoToTblPgBtn_ByLocator(TableHelper.TableButton tblPageNavBtn);
        string GetGridTypeXPath(bool isMultiTabGrid);
        int GetTableRowCount(bool isMultiTabGrid = true);
        By GetTblRowBtn_ByLocator<T>(TableHelper.TableButton tblRowBtn, T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true, bool rowEndsWithChkbx = false);
        By GetTblRow_ByLocator(string textInRowForAnyColumn, bool isMultiTabGrid, bool useContainsOperator = false);
        string GetXPathForTblRowBasedOnTextInRowOrRowIndex<T>(T textInRowForAnyColumnOrRowIndex, bool useContainsOperator = false);
        void GoToFirstPage();
        void GoToLastPage();
        void GoToNextPage();
        void GoToPageNumber(int pageNumber);
        void GoToPreviousPage();
        void RefreshTable();
        void SelectCheckboxForRow<T>(T textInRowForAnyColumnOrRowIndex, bool isMultiTabGrid = true);
        void SetViewItemsPerPageSize(int newSize);
        void SortColumnAscending(Enum columnName, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void SortColumnDescending(Enum columnName, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void SortColumnToDefault(Enum columnName, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void ToggleCheckBoxForRow(string textInRowForAnyColumn = "", bool isMultiTabGrid = true);
        bool VerifyRecordIsDisplayed(Enum columnName, string recordNameOrNumber, TableHelper.TableType tableType = TableHelper.TableType.Unknown, bool noRecordsExpected = false, FilterOperator filterOperator = FilterOperator.EqualTo);
        bool VerifyViewPdfReport(string textInRowForAnyColumn = "", bool isMultiViewPDF = false, bool selectNoneForMultiView = false, string expectedReportUrl = "");
    }
}