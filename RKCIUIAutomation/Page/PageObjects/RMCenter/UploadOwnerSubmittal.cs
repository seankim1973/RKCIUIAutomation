using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class UploadOwnerSubmittal : PageBase, IUploadOwnerSubmittal
    {
        public UploadOwnerSubmittal()
        {
            //tenantAllEntryFieldKeyValuePairs = GetTenantEntryFieldKVPairsList();
        }

        public UploadOwnerSubmittal(IWebDriver driver)
        {
            this.Driver = driver;
            //tenantTableTabs = GetTenantTableTabsList();
            //reqFieldLocators = GetTenantRequiredFieldLocators();
            //tenantAllEntryFields = GetTenantAllEntryFieldsList();
            //tenantExpectedRequiredFields = GetTenantRequiredFieldsList();
            //expectedEntryFieldsForTblColumns = GetTenantEntryFieldsForTableColumns();
        }

        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Tenant'
        /// </summary>
        public override T SetClass<T>(IWebDriver driver)
        {
            IUploadOwnerSubmittal instance = new UploadOwnerSubmittal(driver);

            if (tenantName == TenantName.SGWay)
            {
                Factory.log.Info($"###### using UploadOwnerSubmittal_SGWay instance ###### ");
                instance = new UploadOwnerSubmittal_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                Factory.log.Info($"###### using  UploadOwnerSubmittal_SH249 instance ###### ");
                instance = new UploadOwnerSubmittal_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                Factory.log.Info($"###### using  UploadOwnerSubmittal_Garnet instance ###### ");
                instance = new UploadOwnerSubmittal_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                Factory.log.Info($"###### using  UploadOwnerSubmittal_GLX instance ###### ");
                instance = new UploadOwnerSubmittal_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                Factory.log.Info($"###### using  UploadOwnerSubmittal_I15South instance ###### ");
                instance = new UploadOwnerSubmittal_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                Factory.log.Info($"###### using UploadOwnerSubmittal_I15Tech instance ###### ");
                instance = new UploadOwnerSubmittal_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                Factory.log.Info($"###### using UploadOwnerSubmittal_LAX instance ###### ");
                instance = new UploadOwnerSubmittal_LAX(driver);
            }
            return (T)instance;
        }

        //GLX and LAX - StringValue[0] = table tab name, StringValue[1] = Table content reference id
        public enum TableTab
        {
            [StringValue("Unsent Transmissions", "TransmissionGridNew")] UnsentTransmissions,
            [StringValue("Pending Transmissions", "TransmissionGridPending")] PendingTransmissions,
            [StringValue("Transmitted Records", "TransmissionGridForwarded")] TransmittedRecords
        }

        public enum EntryField
        {
            [StringValue("DocumentDate", DATE)] Date,
            [StringValue("TransmittalNo", TEXT)] TransmittalNumber,
            [StringValue("SecurityClassificationId", DDL)] SecurityClassification,
            [StringValue("Title", TEXT)] Title,
            [StringValue("From", TEXT)] From,
            [StringValue("AgencyFromId", DDL)] AgencyFrom,
            [StringValue("Attention", TEXT)] Attention,
            [StringValue("AgencyToId", DDL)] AgencyAttention,
            [StringValue("DocumentTypeCatogoryId", DDL)] DocumentCategory,
            [StringValue("DocumentTypeId", DDL)] DocumentType,
            [StringValue("OriginatorDocumentRef", TEXT)] OriginatorDocumentRef,
            [StringValue("Revision", TEXT)] Revision,
            [StringValue("SelectedTransmittedIds", MULTIDDL)] Transmitted,
            [StringValue("SegmentId", DDL)] Segment_Area,
            [StringValue("DesignPackagesIdsNcr", MULTIDDL)] DesignPackages,
            [StringValue("CdrlNumber", TEXT)] CDRL,
            [StringValue("ResponseRequiredRadioButton_True", RDOBTN)] ResponseRequired_Yes,
            [StringValue("ResponseRequiredRadioButton_False", RDOBTN)] ResponseRequired_No,
            [StringValue("ResponseRequiredDate", FUTUREDATE)] ResponseRequiredBy_Date,
            [StringValue("OwnerReponseId", DDL)] OwnerResponse,
            [StringValue("OwnerResponseBy", TEXT)] OwnerResponseBy,
            [StringValue("OwnerResponseDate", DATE)] OwnerResponseDate,
            [StringValue("SectionId", DDL)] SpecSection,
            [StringValue("MSLNo", TEXT)] MSLNumber,
            [StringValue("AvailableAccessItems", DDL)] Access,
            [StringValue("ViaId", DDL)] Via,
            [StringValue("AllowReshare", CHKBOX)] AllowResharing,
            [StringValue("TransmissionFiles", UPLOAD)] Attachments
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
            [StringValue("TransmittedTypeNames")] TransmittedTypes,
            [StringValue("ViaId")] Via,
        }

    }

    public interface IUploadOwnerSubmittal
    {

    }

    /// <summary>
    /// Tenant specific implementation of UploadOwnerSubmittal
    /// </summary>

    #region Implementation specific to Garnet

    public class UploadOwnerSubmittal_Garnet : UploadOwnerSubmittal
    {
        public UploadOwnerSubmittal_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet


    #region Implementation specific to GLX

    public class UploadOwnerSubmittal_GLX : UploadOwnerSubmittal
    {
        public UploadOwnerSubmittal_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to GLX


    #region Implementation specific to SH249

    public class UploadOwnerSubmittal_SH249 : UploadOwnerSubmittal
    {
        public UploadOwnerSubmittal_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SH249


    #region Implementation specific to SGWay

    public class UploadOwnerSubmittal_SGWay : UploadOwnerSubmittal
    {
        public UploadOwnerSubmittal_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SGWay


    #region Implementation specific to I15South

    public class UploadOwnerSubmittal_I15South : UploadOwnerSubmittal
    {
        public UploadOwnerSubmittal_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South


    #region Implementation specific to I15Tech

    public class UploadOwnerSubmittal_I15Tech : UploadOwnerSubmittal
    {
        public UploadOwnerSubmittal_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15Tech


    #region Implementation specific to LAX

    public class UploadOwnerSubmittal_LAX : UploadOwnerSubmittal
    {
        public UploadOwnerSubmittal_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX
}