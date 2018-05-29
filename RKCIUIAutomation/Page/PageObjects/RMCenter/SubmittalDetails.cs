using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class SubmittalDetails : RMCenter
    {
        public SubmittalDetails()
        { }
        public SubmittalDetails(IWebDriver driver) => Driver = driver;

        public enum DDListID
        {
            [StringValue("SubmittalActionId")] Action,
            [StringValue("SegmentId")] Segment_Area,
            [StringValue("LocationId")] Location,
            [StringValue("FeatureId")] Feature,
            [StringValue("GradeId")] Grade,
            [StringValue("SupplierId")] Supplier,
            [StringValue("SpecificationId")] Specification,
            [StringValue("QuantityUnitId")] QuantityUnit
        }

        public static By Err_Name { get; } = By.Id("SubmittalNo-error");
        public static By Err_SubmittalTitle { get; } = By.Id("SubmittalTitle-error");
        public static By Err_DDListAction { get; } = By.XPath("//span[@data-valmsg-for='SubmittalActionId']");
        public static By Err_DDListSegmentArea { get; } = By.XPath("//span[@data-valmsg-for='SegmentId']");
        public static By Err_DDListFeatureId { get; } = By.XPath("//span[@data-valmsg-for='FeatureId']");
        public static By Err_DDListGradeId { get; } = By.XPath("//span[@data-valmsg-for='GradeId']");
        public static By Err_DDListSpecificationId { get; } = By.XPath("//span[@data-valmsg-for='SpecificationId']");
        public static By Err_DDListQuantity { get; } = By.XPath("//span[@data-valmsg-for='Quantity']");
        public static By Err_DDListQuantityUnitId { get; } = By.XPath("//span[@data-valmsg-for='QuantityUnitId']");
        public static By Err_Attachments { get; } = By.XPath("//span[@data-valmsg-for='UploadFiles[0].Files']");


        public static By Txt_PageTitle { get; } = By.XPath("//h3[1]");
        public static By Input_Name { get; } = By.Id("SubmittalNo");
        public static By Txt_SubmittalType { get; } = By.XPath("//span[@data-valmsg-for='SubmittalTypeId']");
        public static By Txt_Originator { get; } = By.XPath("//label[contains(text(),'Originator')]/parent::div[@class='form-group']");
        public static By Txt_Status { get; } = By.XPath("//label[contains(text(),'Status')]/parent::div[@class='form-group']");
        public static By Input_SubmittalTitle { get; } = By.Id("SubmittalTitle");
        public static By Input_SubmittalDate { get; } = By.Id("SubmittalDate");
        public static By Chkbx_IsMaterialCertified { get; } = By.Id("IsMaterialCert");
        public static By Input_Quantity { get; } = By.XPath("//input[@id='Quantity']/preceding-sibling::input");
        public static By Btn_SelectFiles { get; } = By.XPath("//div[@aria-label='Select files...']");
        public static By Input_ActivityLogNotes { get; } = By.Id("SubmittalNote");
        public static By Btn_ActionBy_Filter { get; } = By.XPath("//th[@data-title='Action By']/a[1]");
        public static By Btn_ActionBy_Column { get; } = By.XPath("//th[@data-title='Action By']/a[2]");
        public static By Form_ColumnFilterBox { get; } = By.XPath("//form[@data-role='popup'][contains(@style,'display: block;')]");




    }
}
