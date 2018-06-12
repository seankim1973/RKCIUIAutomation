namespace RKCIUIAutomation.Page.PageObjects.RMCenter.Search
{
    /// <summary>
    /// Implementation of Search page class objects and workflows
    /// </summary>
    public class Search_Impl : Search, ISearch
    {
        //Common page objects and workflows are inherited from abstract class
    }


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

}
