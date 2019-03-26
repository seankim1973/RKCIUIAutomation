using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.ProjectCorrespondenceLog;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class ProjectCorrespondenceLog : ProjectCorrespondenceLog_Impl
    {
        public ProjectCorrespondenceLog()
        {
        }

        public ProjectCorrespondenceLog(IWebDriver driver) => driver = Driver;

        //GLX and LAX - StringValue[0] = table tab name, StringValue[1] = Table content reference id
        public enum TableTab
        {
            [StringValue("Unsent Transmissions", "TransmissionGridNew")] UnsentTransmissions,
            [StringValue("Pending Transmissions", "TransmissionGridPending")] PendingTransmissions,
            [StringValue("Transmitted Records", "TransmissionGridForwarded")] TransmittedRecords
        }

        public enum EntryField
        {
            [StringValue("DocumentDate")] Date,
            [StringValue("TransmittalNo")] TransmittalNumber,
            [StringValue("SecurityClassificationId")] SecurityClassification,
            [StringValue("Title")] Title,
            [StringValue("From")] From,
            [StringValue("AgencyFromId")] AgencyFrom,
            [StringValue("Attention")] Attention,
            [StringValue("AgencyToId")] AgencyAttention,
            [StringValue("DocumentTypeCatogoryId")] DocumentCategory,
            [StringValue("DocumentTypeId")] DocumentType,
            [StringValue("OriginatorDocumentRef")] OriginatorDocumentRef,
            [StringValue("Revision")] Revision,
            [StringValue("SelectedTransmittedIds")] Transmitted,
            [StringValue("SegmentId")] Segment_Area,
            [StringValue("DesignPackagesIdsNcr")] DesignPackages,
            [StringValue("CdrlNumber")] CDRL,
            [StringValue("ResponseRequiredRadioButton_True")] ResponseRequired_Yes,
            [StringValue("ResponseRequiredRadioButton_False")] ResponseRequired_No,
            [StringValue("ResponseRequiredDate")] ResponseRequiredBy_Date,
            [StringValue("OwnerReponseId")] OwnerResponse,
            [StringValue("OwnerResponseBy")] OwnerResponseBy,
            [StringValue("OwnerResponseDate")] OwnerResponseDate,
            [StringValue("SectionId")] SpecSection,
            [StringValue("MSLNo")] MSLNumber,
            [StringValue("AvailableAccessItems")] Access,
            [StringValue("AllowReshare")] AllowResharing,
            [StringValue("TransmissionFiles")] Attachments

        }

        public enum ColumnName
        {
            [StringValue("Id")] ID,
            [StringValue("MSLNo")] MSLNumber,
            [StringValue("TransmittalNo")] TransmittalNumber,
            [StringValue("DocumentDateDisplay")] Date,
            [StringValue("Attention")] Attention,
            [StringValue("From")] From,
            [StringValue("Title")] Title,
            [StringValue("DocumentType.Key")] DocumentType,
            [StringValue("OriginatorDocumentRef")] OriginatorRef,
            [StringValue("Revision")] Revision,
            [StringValue("TransmittedTypeNames")] TransmittedType
        }

        [ThreadStatic]
        internal static IList<string> expectedRequiredFields;

        [ThreadStatic]
        internal static IList<string> allEntryFields;

        internal bool VerifyRequiredFields()
        {
            By reqFieldLocator = By.XPath("//span[contains(text(),'Required')]");
            IList<string> actualReqFields = new List<string>();
            actualReqFields = GetAttributes(reqFieldLocator, "data-valmsg-for");

            return GetRequiredFieldsList().SequenceEqual(actualReqFields);
        }

        public override void EnterDate(string shortDate = "")
            => EnterText(By.Id(EntryField.Date.GetString()), shortDate.HasValue()?shortDate:GetShortDate());

        public override void EnterTransmittalNumber(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterTitle(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterFrom(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterAttention(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterOriginatorDocumentRef(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterRevision(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterCDRL(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterResponseRequiredByDate(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterOwnerResponseBy(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterOwnerResponseDate(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void EnterMSLNumber(string value = "")
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_Access<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_SpecSection<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_OwnerResponse<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_DesignPackages<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_SegmentArea<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_Transmitted<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_DocumentCategory<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_DocumentType<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_AgencyAttention<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_AgencyFrom<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectDDL_SecurityClassification<T>(T indexOrName = default(T))
        {
            throw new NotImplementedException();
        }

        public override void SelectRdoBtn_ResponseRequired_Yes()
        {
            throw new NotImplementedException();
        }

        public override void SelectRdoBtn_ResponseRequired_No()
        {
            throw new NotImplementedException();
        }

        public override void SelectChkbox_AllowResharing()
        {
            throw new NotImplementedException();
        }
    }

    public interface IProjectCorrespondenceLog
    {
        void CreateNewAndPopulateFields();

        IList<string> GetRequiredFieldsList();

        void EnterAllFields();

        void LogintoCorrespondenceLogPage(UserType userType);

        bool VerifyTransmittalLogIsDisplayed();

        void EnterDate(string shortDate = "");
        void EnterTransmittalNumber(string value = "");
        void EnterTitle(string value = "");
        void EnterFrom(string value = "");
        void EnterAttention(string value = "");
        void EnterOriginatorDocumentRef(string value = "");
        void EnterRevision(string value = "");
        void EnterCDRL(string value = "");
        void EnterResponseRequiredByDate(string value = "");
        void EnterOwnerResponseBy(string value = "");
        void EnterOwnerResponseDate(string value = "");
        void EnterMSLNumber(string value = "");

        void SelectDDL_Access<T>(T indexOrName = default(T));
        void SelectDDL_SpecSection<T>(T indexOrName = default(T));
        void SelectDDL_OwnerResponse<T>(T indexOrName = default(T));
        void SelectDDL_DesignPackages<T>(T indexOrName = default(T));
        void SelectDDL_SegmentArea<T>(T indexOrName = default(T));
        void SelectDDL_Transmitted<T>(T indexOrName = default(T));
        void SelectDDL_DocumentCategory<T>(T indexOrName = default(T));
        void SelectDDL_DocumentType<T>(T indexOrName = default(T));
        void SelectDDL_AgencyAttention<T>(T indexOrName = default(T));
        void SelectDDL_AgencyFrom<T>(T indexOrName = default(T));
        void SelectDDL_SecurityClassification<T>(T indexOrName = default(T));

        void SelectRdoBtn_ResponseRequired_Yes();
        void SelectRdoBtn_ResponseRequired_No();
        void SelectChkbox_AllowResharing();
    }

    #region Common Workflow Implementation class

    public abstract class ProjectCorrespondenceLog_Impl : TestBase, IProjectCorrespondenceLog
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IProjectCorrespondenceLog SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IProjectCorrespondenceLog instance = new ProjectCorrespondenceLog(driver);

            if (tenantName == TenantName.SGWay)
            {
                log.Info($"###### using ProjectCorrespondenceLog_SGWay instance ###### ");
                instance = new ProjectCorrespondenceLog_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_SH249 instance ###### ");
                instance = new ProjectCorrespondenceLog_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_Garnet instance ###### ");
                instance = new ProjectCorrespondenceLog_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_GLX instance ###### ");
                instance = new ProjectCorrespondenceLog_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                log.Info($"###### using  ProjectCorrespondenceLog_I15South instance ###### ");
                instance = new ProjectCorrespondenceLog_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                log.Info($"###### using ProjectCorrespondenceLog_I15Tech instance ###### ");
                instance = new ProjectCorrespondenceLog_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                log.Info($"###### using ProjectCorrespondenceLog_LAX instance ###### ");
                instance = new ProjectCorrespondenceLog_LAX(driver);
            }
            return instance;
        }

        internal ProjectCorrespondenceLog PCLogBase => new ProjectCorrespondenceLog();

        public bool VerifyTransmittalLogIsDisplayed()
        {
            return true;
        }

        public virtual void LogintoCorrespondenceLogPage(UserType userType)
        {
            LoginAs(userType);
            WaitForPageReady();
            NavigateToPage.RMCenter_Project_Correspondence_Log();
        }


        public virtual IList<string> GetRequiredFieldsList()
            => expectedRequiredFields;

        public virtual void EnterAllFields()
        { }

        public virtual void CreateNewAndPopulateFields()
        {
            ClickNew();
            WaitForPageReady();
            ClickSaveForward();
            AddAssertionToList(PCLogBase.VerifyRequiredFields());
            //requiredFields = new List<string>();
            

        }

        public abstract void EnterDate(string shortDate = "");
        public abstract void EnterTransmittalNumber(string value = "");
        public abstract void EnterTitle(string value = "");
        public abstract void EnterFrom(string value = "");
        public abstract void EnterAttention(string value = "");
        public abstract void EnterOriginatorDocumentRef(string value = "");
        public abstract void EnterRevision(string value = "");
        public abstract void EnterCDRL(string value = "");
        public abstract void EnterResponseRequiredByDate(string value = "");
        public abstract void EnterOwnerResponseBy(string value = "");
        public abstract void EnterOwnerResponseDate(string value = "");
        public abstract void EnterMSLNumber(string value = "");
        public abstract void SelectDDL_Access<T>(T indexOrName = default(T));
        public abstract void SelectDDL_SpecSection<T>(T indexOrName = default(T));
        public abstract void SelectDDL_OwnerResponse<T>(T indexOrName = default(T));
        public abstract void SelectDDL_DesignPackages<T>(T indexOrName = default(T));
        public abstract void SelectDDL_SegmentArea<T>(T indexOrName = default(T));
        public abstract void SelectDDL_Transmitted<T>(T indexOrName = default(T));
        public abstract void SelectDDL_DocumentCategory<T>(T indexOrName = default(T));
        public abstract void SelectDDL_DocumentType<T>(T indexOrName = default(T));
        public abstract void SelectDDL_AgencyAttention<T>(T indexOrName = default(T));
        public abstract void SelectDDL_AgencyFrom<T>(T indexOrName = default(T));
        public abstract void SelectDDL_SecurityClassification<T>(T indexOrName = default(T));
        public abstract void SelectRdoBtn_ResponseRequired_Yes();
        public abstract void SelectRdoBtn_ResponseRequired_No();
        public abstract void SelectChkbox_AllowResharing();
    }

    #endregion Common Workflow Implementation class

    /// <summary>
    /// Tenant specific implementation of ProjectCorrespondenceLog
    /// </summary>

    #region Implementation specific to Garnet

    public class ProjectCorrespondenceLog_Garnet : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet

    #region Implementation specific to GLX

    public class ProjectCorrespondenceLog_GLX : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override IList<string> GetRequiredFieldsList()
        {
            expectedRequiredFields = new List<string>()
            {
                EntryField.Date.GetString(),
                EntryField.SecurityClassification.GetString(),
                EntryField.Title.GetString(),
                EntryField.DocumentType.GetString(),
                EntryField.Transmitted.GetString(),
                EntryField.Attachments.GetString()
            };

            return expectedRequiredFields;
        }



    }

    #endregion Implementation specific to GLX

    #region Implementation specific to SH249

    public class ProjectCorrespondenceLog_SH249 : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void LogintoCorrespondenceLogPage(UserType userType)
        {
            LoginAs(userType);
            WaitForPageReady();
            NavigateToPage.RMCenter_Project_Transmittal_Log();
        }

    }

    #endregion Implementation specific to SH249

    #region Implementation specific to SGWay

    public class ProjectCorrespondenceLog_SGWay : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void LogintoCorrespondenceLogPage(UserType userType)
        {
            LoginAs(userType);
            WaitForPageReady();
            NavigateToPage.RMCenter_Project_Transmittal_Log();
        }
    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to I15South

    public class ProjectCorrespondenceLog_I15South : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class ProjectCorrespondenceLog_I15Tech : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class ProjectCorrespondenceLog_LAX : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX

}