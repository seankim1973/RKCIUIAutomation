using MiniGuids;
using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using System;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class DesignDocument : PageBase
    {
        private string designDocTitle;
        private string designDocNumber;
        private string docTitleKey;
        private string docNumberKey;
        private MiniGuid guid;

        public enum TableTab
        {
            [StringValue("Creating")] Creating,
            [StringValue("Comment")] Comment,
            [StringValue("Resolution")] Resolution,
            [StringValue("Response")] Response,
            [StringValue("Closing")] Closing,
            [StringValue("Closed")] Closed
        }

        private By UploadNewDesignDoc_ByLocator => By.XPath("//a[text()='Upload New Design Document']");
        private By CancelBtn_ByLocator => By.Id("btnCancel");
        private By SaveOnlyBtn_ByLocator => By.Id("btnSave");
        private By SaveForwardBtn_ByLocator => By.Id("btnSaveForward");


        public void ClickBtn_UploadNewDesignDoc() => ClickElement(UploadNewDesignDoc_ByLocator);
        public void ClickTab_Creating() => ClickTableTab(TableTab.Creating);
        public void ClickTab_Comment() => ClickTableTab(TableTab.Comment);
        public void ClickTab_Resolution() => ClickTableTab(TableTab.Resolution);
        public void ClickTab_Response() => ClickTableTab(TableTab.Response);
        public void ClickTab_Closing() => ClickTableTab(TableTab.Closing);
        public void ClickTab_Closed() => ClickTableTab(TableTab.Closed);

        

        public void CreateDocument()
        {
            ClickElement(UploadNewDesignDoc_ByLocator);
            EnterDesignDocTitleAndNumber();
            UploadFile("test.xlsx");
            ClickElement(SaveForwardBtn_ByLocator);
        }




        enum DesignDocDetails_InputFields
        {
            [StringValue("Submittal_Title")] Title,
            [StringValue("Submittal_Document_Number")] DocumentNumber,
            [StringValue("Submittal_Document_DocumentDate")] DocumentDate,
            [StringValue("Submittal_TransmittalDate")] TransmittalDate,
            [StringValue("Submittal_TransmittalNumber")] TransmittalNumber
        }


        private void SetDesignDocTitleAndNumber()
        {
            guid = MiniGuid.NewGuid();

            string docKey = $"{tenantName}{GetTestName()}";
            docTitleKey = $"{docKey}_DsgnDocTtl";
            docNumberKey = $"{docKey}_DsgnDocNumb";
            CreateVar(docTitleKey, docTitleKey);
            CreateVar(docNumberKey, guid);
        }

        public void EnterDesignDocTitleAndNumber()
        {
            SetDesignDocTitleAndNumber();
            designDocTitle = GetVar(docTitleKey).ToString();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.Title), designDocTitle);
            designDocNumber = GetVar(docNumberKey).ToString();
            EnterText(PageHelper.GetTextInputFieldByLocator(DesignDocDetails_InputFields.DocumentNumber), designDocNumber);
        }



    }
}
