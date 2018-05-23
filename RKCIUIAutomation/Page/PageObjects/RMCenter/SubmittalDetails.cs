using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class SubmittalDetails : RMCenter
    {
        public By txt_PageTitle = By.XPath("//h3[1]");
        public By input_Name = By.Id("SubmittalNo");
        public By txt_SubmittalType = By.XPath("//span[@data-valmsg-for='SubmittalTypeId']");
        public By txt_Originator = By.XPath("//label[contains(text(),'Originator')]/parent::div[@class='form-group']");
        public By txt_Status = By.XPath("//label[contains(text(),'Status')]/parent::div[@class='form-group']");
        public By ul_Action = By.Id("SubmittalActionId_listbox");
        public By ul_Segment_Area = By.Id("SegmentId_listbox");
        public By ul_Location = By.Id("LocationId_listbox");
        public By ul_Feature = By.Id("FeatureId_listbox");
        public By ul_Grade = By.Id("GradeId_listbox");
        public By ul_Supplier = By.Id("SupplierId_listbox");
        public By ul_Specification = By.Id("SpecificationId_listbox");
        public By chkbx_IsMaterialCertified = By.Id("IsMaterialCert");
        public By input_Quantity = By.Id("Quantity");
        public By ul_QuantityUnit = By.Id("QuantityUnitId_listbox");

        public By input_ActivityLogNotes = By.Id("SubmittalNote");
        public By btn_ActionBy_Filter = By.XPath("//th[@data-title='Action By']/a[1]");
        public By btn_ActionBy_Column = By.XPath("//th[@data-title='Action By']/a[2]");
        public By form_ColumnFilterBox = By.XPath("//form[@data-role='popup'][contains(@style,'display: block;')]");

        public By btn_GoToFirstPg = By.XPath("//a[@aria-label='Go to the first page']");
        public By btn_GoToPrevPg = By.XPath("//a[@aria-label='Go to the previous page']");
        public By btn_GoToNextPg = By.XPath("//a[@aria-label='Go to the next page']");
        public By btn_GoToLastPg = By.XPath("//a[@aria-label='Go to the last page']");

        public By btn_Cancel = By.Id("CancelSubmittal");
        public By btn_Save = By.Id("SaveSubmittal");
        public By btn_SubmitForward = By.Id("SaveForwardSubmittal");
        

    }
}
