namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    /// <summary>
    /// Implementation of Search page class objects and workflows
    /// </summary>
    public class Search_Impl : Search, ISearch
    {
        ////Page workflow common to all tenants
        //public override void SelectDDL_DocumentType<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.DocumentType, itemIndexOrName);
        //public override void SelectDDL_Status<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Status, itemIndexOrName);
        //public override void EnterText_Title(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Title), text);
        //public override void EnterText_TransmittalNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.TransmittalNumber), text);
        //public override void EnterText_From(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.From), text);
        //public override void EnterText_Attention(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Attention), text);

        ///// <summary>
        ///// Date value in string format (i.e. MM/DD/YYYY)
        ///// </summary>
        ///// <param name="date"></param>
        //public override void EnterDate_From(string fromDate) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Transmittal_FromDate), fromDate);

        ///// <summary>
        ///// Date value in string format (i.e. MM/DD/YYYY)
        ///// </summary>
        ///// <param name="toDate"></param>
        //public override void EnterDate_To(string toDate) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Transmittal_ToDate), toDate);

        //public override void PopulateAllSearchCriteriaFields()
        //{
        //    SelectDDL_DocumentType(1);
        //    EnterText_Number("Common Test Number");
        //    EnterText_TransmittalNumber("Common Test Transmittal Number");
        //    EnterText_Title("Common Test Title");
        //    EnterText_From("From Common Test");
        //    EnterText_MSLNumber("Common Test MSL Number");
        //    EnterText_Attention("Attention Common Test");
        //}

        ////Not used in tenant(s): GLX
        //public override void EnterText_Number(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.Number), text);
        
        ////Not used in tenant(s): GLX, I15Tech
        //public override void EnterText_MSLNumber(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.MSLNumber), text);
        
        ////Used only in GLX
        //public override void SelectDDL_Category<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.Category, itemIndexOrName);
        //public override void SelectDDL_SegmentArea<T>(T itemIndexOrName) => ExpandAndSelectFromDDList(SearchCriteria.SegmentArea, itemIndexOrName);
        //public override void EnterText_OriginatorDocumentRef(string text) => EnterText(GetTextInputFieldByLocator(SearchCriteria.OriginatorDocumentRef), text);


    }//end of general Search page Implementation


    /// <summary>
    /// Workflow methods specific to GLX Tenant
    /// </summary>
    public class Search_GLX : Search_Impl
    {
        public override void PopulateAllSearchCriteriaFields()
        {
            SelectDDL_DocumentType(1);
            EnterText_Title("GLX Test Title");
            EnterText_TransmittalNumber("GLX Test1234");
            EnterText_From("GLX From Test");
            EnterText_Attention("GLX Test Attention");
            SelectDDL_SegmentArea(1);
            EnterDate_From("1/1/2018");
            EnterDate_To("6/6/2018");
            SelectDDL_Category(1);
            EnterText_OriginatorDocumentRef("GLX Test Originator Ref.");
        }

    } //end of Implementation for GLX


    /// <summary>
    /// Workflow methods specific to I15Tech Tenant
    /// </summary>
    public class Search_I15Tech : Search_Impl
    {
        public override void PopulateAllSearchCriteriaFields()
        {
            SelectDDL_DocumentType(1);
            EnterText_Number("I15Tech Test Number");
            EnterText_From("From I15Tech Test");
            EnterText_Title("I15Tech Test Title");
            EnterText_Attention("I15Tech Attention");
            EnterText_TransmittalNumber("I15Tech Transmittal Number");
        }


    } //end of Implementation for I15Tech

} //end of namespace
