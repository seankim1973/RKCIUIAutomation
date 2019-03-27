using MiniGuids;
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

        public ProjectCorrespondenceLog(IWebDriver driver) => this.Driver = driver;

        //GLX and LAX - StringValue[0] = table tab name, StringValue[1] = Table content reference id
        public enum TableTab
        {
            [StringValue("Unsent Transmissions", "TransmissionGridNew")] UnsentTransmissions,
            [StringValue("Pending Transmissions", "TransmissionGridPending")] PendingTransmissions,
            [StringValue("Transmitted Records", "TransmissionGridForwarded")] TransmittedRecords
        }

        public enum EntryField
        {
            [StringValue("DocumentDate", "DATE")] Date,
            [StringValue("TransmittalNo", "TXT")] TransmittalNumber,
            [StringValue("SecurityClassificationId", "DDL")] SecurityClassification,
            [StringValue("Title", "TXT")] Title,
            [StringValue("From", "TXT")] From,
            [StringValue("AgencyFromId", "DDL")] AgencyFrom,
            [StringValue("Attention", "TXT")] Attention,
            [StringValue("AgencyToId", "DDL")] AgencyAttention,
            [StringValue("DocumentTypeCatogoryId", "DDL")] DocumentCategory,
            [StringValue("DocumentTypeId", "DDL")] DocumentType,
            [StringValue("OriginatorDocumentRef", "TXT")] OriginatorDocumentRef,
            [StringValue("Revision", "TXT")] Revision,
            [StringValue("SelectedTransmittedIds", "MULTIDDL")] Transmitted,
            [StringValue("SegmentId", "DDL")] Segment_Area,
            [StringValue("DesignPackagesIdsNcr", "MULTIDDL")] DesignPackages,
            [StringValue("CdrlNumber", "TXT")] CDRL,
            [StringValue("ResponseRequiredRadioButton_True", "RDOBTN")] ResponseRequired_Yes,
            [StringValue("ResponseRequiredRadioButton_False", "RDOBTN")] ResponseRequired_No,
            [StringValue("ResponseRequiredDate", "DATE")] ResponseRequiredBy_Date,
            [StringValue("OwnerReponseId", "DDL")] OwnerResponse,
            [StringValue("OwnerResponseBy", "TXT")] OwnerResponseBy,
            [StringValue("OwnerResponseDate", "DATE")] OwnerResponseDate,
            [StringValue("SectionId", "DDL")] SpecSection,
            [StringValue("MSLNo", "TXT")] MSLNumber,
            [StringValue("AvailableAccessItems", "DDL")] Access,
            [StringValue("ViaId", "DDL")] Via,
            [StringValue("AllowReshare", "CHKBOX")] AllowResharing,
            [StringValue("TransmissionFiles", "UPLOAD")] Attachments
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
            [StringValue("TransmittedTypeNames")] TransmittedType,
            [StringValue("ViaId")] Via,
        }

        [ThreadStatic]
        internal static IList<EntryField> expectedRequiredFields;

        [ThreadStatic]
        internal static IList<EntryField> allEntryFields;

        [ThreadStatic]
        internal static IList<EntryField> expectedEntryFieldsForTablColumns;

        internal void CreateAndStoreRandomValueForField(Enum fieldEnum)
        {
            string fieldName = fieldEnum.GetString();
            MiniGuid guid = GenerateRandomGuid();
            string key = $"{tenantName}{GetTestName()}_{fieldName}";
            CreateVar(key, guid);
            log.Debug($"##### Created random variable for field {fieldName}\nKEY: {key} || VALUE: {GetVar(key)}");
        }

        internal string GetVarForEntryField(Enum fieldEnum)
            => GetVar($"{tenantName}{GetTestName()}_{fieldEnum.GetString()}");

        /// <summary>
        /// For &lt;T&gt;indexOrText argument, provide 1 indexed value or text value of a DDList selection OR text value to enter in a text field
        /// <para>
        /// Use (bool)useContains arg when selecting a DDList item with partial value for [T](string)indexOrText
        /// </para>
        /// <para>
        /// (bool)useContains arg defaults to false and is ignored if arg indexOrText is an Integer
        /// </para>
        /// <para>
        /// When a field is a DATE field, the current short date will be entered by default.  Set futureDate boolean argument to true to set the Date field for the next day
        /// </para>
        ///</summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entryField"></param>
        /// <param name="indexOrText"></param>
        internal void PopulateFieldValue<T>(EntryField entryField, T indexOrText = default(T), bool useContains = false, bool futureDate = false)
        {
            const string TEXT = "TXT";
            const string DLL = "DDL";
            const string DATE = "DATE";
            const string MULTIDDL = "MULTIDDL";
            const string RDOBTN = "RDOBTN";
            const string CHKBOX = "CHKBOX";
            const string UPLOAD = "UPLOAD";

            string fieldType = entryField.GetString(true);
            Type argType = indexOrText.GetType();
            object argValue = null;
            bool isValidArg = false;

            if (argType == typeof(string))
            {
                isValidArg = true;
                argValue = ConvertToType<string>(indexOrText);
                argValue = ((string)argValue).HasValue()
                        ? argValue
                        : fieldType.Equals(DATE)
                            ? GetShortDate()
                            : GetVarForEntryField(entryField);
            }
            else if (argType == typeof(int))
            {
                isValidArg = true;
                argValue = ConvertToType<int>(indexOrText);
                argValue = argValue != null || ((int)argValue == 0)
                    ? argValue
                    : 1;
            }
            else
            {
                LogError($"Argument type ({argType}) is not supported : {indexOrText.ToString()}");
            }


            if (isValidArg)
            {
                if (fieldType.Equals(TEXT) || fieldType.Equals(DATE))
                {
                    EnterText(By.Id(entryField.GetString()), (string)argValue);
                }
                else if (fieldType.Equals(DLL) || fieldType.Equals(MULTIDDL))
                {
                    bool isMultiSelectDDL = fieldType.Equals(MULTIDDL)
                        ? true
                        : false;

                    ExpandAndSelectFromDDList(entryField, argValue, useContains, isMultiSelectDDL);
                }
                else if (fieldType.Equals(RDOBTN) || fieldType.Equals(CHKBOX))
                {
                    SelectRadioBtnOrChkbox(entryField);
                }
                else if (fieldType.Equals(UPLOAD))
                {
                    UploadFile();
                }
            }
        }

        internal bool VerifyRequiredFields()
        {
            By reqFieldLocator = By.XPath("//span[contains(text(),'Required')]");
            IList<string> actualReqFields = new List<string>();
            actualReqFields = GetAttributes(reqFieldLocator, "data-valmsg-for");

            IList<string> expectedReqFields = new List<string>();

            foreach (EntryField field in GetRequiredFieldsList())
            {
                expectedReqFields.Add(field.GetString());
            }

            return expectedReqFields.SequenceEqual(actualReqFields);
        }

        public override void EnterDate(string shortDate = "")
            => PopulateFieldValue(EntryField.Date, shortDate);

        public override void EnterTransmittalNumber(string value = "")
            => PopulateFieldValue(EntryField.TransmittalNumber, value);

        public override void EnterTitle(string value = "")
            => PopulateFieldValue(EntryField.Title, value);

        public override void EnterFrom(string value = "")
            => PopulateFieldValue(EntryField.From, value);

        public override void EnterAttention(string value = "")
            => PopulateFieldValue(EntryField.Attention, value);

        public override void EnterOriginatorDocumentRef(string value = "")
            => PopulateFieldValue(EntryField.OriginatorDocumentRef, value);

        public override void EnterRevision(string value = "")
            => PopulateFieldValue(EntryField.Revision, value);

        public override void EnterCDRL(string value = "")
            => PopulateFieldValue(EntryField.CDRL, value);

        public override void EnterResponseRequiredByDate(string value = "")
            => PopulateFieldValue(EntryField.ResponseRequiredBy_Date, value);

        public override void EnterOwnerResponseBy(string value = "")
            => PopulateFieldValue(EntryField.OwnerResponseBy, value);

        public override void EnterOwnerResponseDate(string value = "")
            => PopulateFieldValue(EntryField.OwnerResponseDate, value);

        public override void EnterMSLNumber(string value = "")
            => PopulateFieldValue(EntryField.MSLNumber, value);

        public override void SelectDDL_Access<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.Access, indexOrName);

        public override void SelectDDL_SpecSection<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.SpecSection, indexOrName);

        public override void SelectDDL_OwnerResponse<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.OwnerResponse, indexOrName);

        public override void SelectDDL_DesignPackages<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.DesignPackages, indexOrName);

        public override void SelectDDL_SegmentArea<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.Segment_Area, indexOrName);

        public override void SelectDDL_Transmitted<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.Transmitted, indexOrName);

        public override void SelectDDL_DocumentCategory<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.DocumentCategory, indexOrName);

        public override void SelectDDL_DocumentType<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.DocumentType, indexOrName);

        public override void SelectDDL_AgencyAttention<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.AgencyAttention, indexOrName);

        public override void SelectDDL_AgencyFrom<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.AgencyFrom, indexOrName);

        public override void SelectDDL_SecurityClassification<T>(T indexOrName = default(T))
            => PopulateFieldValue(EntryField.SecurityClassification, indexOrName);

        public override void SelectRdoBtn_ResponseRequired_Yes()
            => PopulateFieldValue(EntryField.ResponseRequired_Yes, "");

        public override void SelectRdoBtn_ResponseRequired_No()
            => PopulateFieldValue(EntryField.ResponseRequired_No, "");

        public override void SelectChkbox_AllowResharing()
            => PopulateFieldValue(EntryField.AllowResharing, "");
    }

    public interface IProjectCorrespondenceLog
    {
        void CreateNewAndPopulateFields();

        IList<EntryField> GetRequiredFieldsList();

        IList<EntryField> GetAllEntryFields();

        IList<EntryField> GetExpectedEntryFieldsForTableColumns();

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


        public virtual IList<EntryField> GetRequiredFieldsList()
            => expectedRequiredFields;

        public virtual IList<EntryField> GetAllEntryFields()
            => allEntryFields;

        public virtual IList<EntryField> GetExpectedEntryFieldsForTableColumns()
            => expectedEntryFieldsForTablColumns;

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

        public override IList<EntryField> GetRequiredFieldsList()
        {
            expectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.SecurityClassification,
                EntryField.Title,
                EntryField.DocumentType,
                EntryField.Transmitted,
                EntryField.Attachments
            };

            return expectedRequiredFields;
        }

        public override IList<EntryField> GetAllEntryFields()
        {
            allEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.SecurityClassification,
                EntryField.Title,
                EntryField.From,
                EntryField.AgencyFrom,
                EntryField.Attention,
                EntryField.AgencyAttention,
                EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.OriginatorDocumentRef,
                EntryField.Revision,
                EntryField.Transmitted,
                EntryField.Segment_Area,
                EntryField.DesignPackages,
                EntryField.CDRL,
                EntryField.ResponseRequired_Yes,
                EntryField.ResponseRequiredBy_Date,
                EntryField.SpecSection,
                EntryField.MSLNumber,
                EntryField.Access
            };

            return allEntryFields;
        }

        public override IList<EntryField> GetExpectedEntryFieldsForTableColumns()
        {
            expectedEntryFieldsForTablColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.OriginatorDocumentRef,
                EntryField.Revision,
                EntryField.Transmitted,
                EntryField.MSLNumber
            };

            return expectedEntryFieldsForTablColumns;
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


        public override IList<EntryField> GetRequiredFieldsList()
        {
            expectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.Title,
                EntryField.From,
                EntryField.DocumentType,
                EntryField.Via,
                EntryField.Attachments
            };

            return expectedRequiredFields;
        }

        public override IList<EntryField> GetAllEntryFields()
        {
            allEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.Via
            };

            return allEntryFields;
        }

        public override IList<EntryField> GetExpectedEntryFieldsForTableColumns()
        {
            expectedEntryFieldsForTablColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.Via
            };

            return expectedEntryFieldsForTablColumns;
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


        public override IList<EntryField> GetRequiredFieldsList()
        {
            expectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.Attachments
            };

            return expectedRequiredFields;
        }

        public override IList<EntryField> GetAllEntryFields()
        {
            allEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.MSLNumber,
                EntryField.Via
            };

            return allEntryFields;
        }

        public override IList<EntryField> GetExpectedEntryFieldsForTableColumns()
        {
            expectedEntryFieldsForTablColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.MSLNumber,
                EntryField.Via
            };

            return expectedEntryFieldsForTablColumns;
        }



    }

    #endregion Implementation specific to SGWay

    #region Implementation specific to I15South

    public class ProjectCorrespondenceLog_I15South : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_I15South(IWebDriver driver) : base(driver)
        {
        }


        public override IList<EntryField> GetRequiredFieldsList()
        {
            expectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.Attachments
            };

            return expectedRequiredFields;
        }

        public override IList<EntryField> GetAllEntryFields()
        {
            allEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentCategory,
                EntryField.DocumentType
            };

            return allEntryFields;
        }

        public override IList<EntryField> GetExpectedEntryFieldsForTableColumns()
        {
            expectedEntryFieldsForTablColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType
            };

            return expectedEntryFieldsForTablColumns;
        }



    }

    #endregion Implementation specific to I15South

    #region Implementation specific to I15Tech

    public class ProjectCorrespondenceLog_I15Tech : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_I15Tech(IWebDriver driver) : base(driver)
        {
        }


        public override IList<EntryField> GetRequiredFieldsList()
        {
            expectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.Attachments,
                EntryField.Transmitted
            };

            return expectedRequiredFields;
        }

        public override IList<EntryField> GetAllEntryFields()
        {
            allEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.Transmitted
            };

            return allEntryFields;
        }

        public override IList<EntryField> GetExpectedEntryFieldsForTableColumns()
        {
            expectedEntryFieldsForTablColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.Transmitted
            };

            return expectedEntryFieldsForTablColumns;
        }


    }

    #endregion Implementation specific to I15Tech

    #region Implementation specific to LAX

    public class ProjectCorrespondenceLog_LAX : ProjectCorrespondenceLog
    {
        public ProjectCorrespondenceLog_LAX(IWebDriver driver) : base(driver)
        {
        }


        public override IList<EntryField> GetRequiredFieldsList()
        {
            expectedRequiredFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.SecurityClassification,
                EntryField.Title,
                EntryField.DocumentType,
                EntryField.Transmitted,
                EntryField.Attachments
            };

            return expectedRequiredFields;
        }

        public override IList<EntryField> GetAllEntryFields()
        {
            allEntryFields = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.SecurityClassification,
                EntryField.Title,
                EntryField.From,
                EntryField.AgencyFrom,
                EntryField.Attention,
                EntryField.AgencyAttention,
                EntryField.DocumentCategory,
                EntryField.DocumentType,
                EntryField.OriginatorDocumentRef,
                EntryField.Revision,
                EntryField.Transmitted,
                EntryField.Segment_Area,
                EntryField.Access
            };

            return allEntryFields;
        }

        public override IList<EntryField> GetExpectedEntryFieldsForTableColumns()
        {
            expectedEntryFieldsForTablColumns = new List<EntryField>()
            {
                EntryField.Date,
                EntryField.TransmittalNumber,
                EntryField.Title,
                EntryField.From,
                EntryField.Attention,
                EntryField.DocumentType,
                EntryField.OriginatorDocumentRef,
                EntryField.Revision,
                EntryField.Transmitted,
            };

            return expectedEntryFieldsForTablColumns;
        }



    }

    #endregion Implementation specific to LAX

}