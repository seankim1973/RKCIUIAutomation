using System.Collections.Generic;

namespace RKCIUIAutomation.Page
{
    public interface IKendoGrid
    {
        //KendoGrid _Kendo { get; set; }
        void ChangePageSize(int newSize, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void ClickCommentTab(int commentNumber);
        void ClickTableTab(string tblTabName);
        void FilterTableGrid(string columnName, string filterValue, FilterOperator filterOperator = FilterOperator.EqualTo, FilterLogic filterLogic = FilterLogic.And, string additionalFilterValue = null, FilterOperator additionalFilterOperator = FilterOperator.EqualTo, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        int GetCurrentPageNumber(TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        string GetCurrentTableTabName();
        string GetGridID(TableHelper.TableType tableType);
        List<T> GetItems<T>(TableHelper.TableType tableType = TableHelper.TableType.Unknown) where T : class;
        int GetPageSize(TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void GoToTablePage(int pageNumber, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void Reload(TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void RemoveFilters(TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        void Sort(string columnName, SortType sortType, TableHelper.TableType tableType = TableHelper.TableType.Unknown);
        int TotalNumberRows(TableHelper.TableType tableType = TableHelper.TableType.Unknown);
    }
}