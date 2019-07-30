using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.Factory;
using System.ComponentModel;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class UploadQASubmittal : PageBase, IUploadQASubmittal
    {
        public UploadQASubmittal()
        {
        }

        public UploadQASubmittal(IWebDriver driver)
        {
            this.Driver = driver;
            tenantRoundOneRequiredFields = GetTenantRoundOneRequiredFields();
            tenantRoundTwoRequiredFields = GetTenantRoundTwoRequiredFields();
        }

        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Tenant'
        /// </summary>
        public override T SetClass<T>(IWebDriver driver)
        {
            IUploadQASubmittal instance = new UploadQASubmittal(driver);

            switch (tenantName)
            {
                case TenantNameType.SGWay:
                    log.Info($"###### using UploadQASubmittal_SGWay instance ###### ");
                    instance = new UploadQASubmittal_SGWay(driver);
                    break;
                case TenantNameType.SH249:
                    log.Info($"###### using  UploadQASubmittal_SH249 instance ###### ");
                    instance = new UploadQASubmittal_SH249(driver);
                    break;
                case TenantNameType.I15South:
                    log.Info($"###### using  UploadQASubmittal_I15South instance ###### ");
                    instance = new UploadQASubmittal_I15South(driver);
                    break;
                case TenantNameType.I15Tech:
                    log.Info($"###### using UploadQASubmittal_I15Tech instance ###### ");
                    instance = new UploadQASubmittal_I15Tech(driver);
                    break;
                case TenantNameType.LAX:
                    log.Info($"###### using UploadQASubmittal_LAX instance ###### ");
                    instance = new UploadQASubmittal_LAX(driver);
                    break;
                default:
                    break;
            }

            return (T)instance;
        }

        public enum EntryFieldType
        {
            [StringValue("SubmittalNo", TEXT)] SubmittalNo,
            [StringValue("SubmittalTitle", TEXT)] SubmittalTitle,
            [StringValue("SubmittalActionId", DDL)] SubmittalActionId,
            [StringValue("SegmentId", DDL)] SegmentId,
            [StringValue("FeatureId", DDL)] FeatureId,
            [StringValue("GradeId", DDL)] GradeId,
            [StringValue("SpecificationId", DDL)] SpecificationId,
            [StringValue("Quantity", NUMBER)] Quantity,
            [StringValue("QuantityUnitId", DDL)] QuantityUnitId,
            [StringValue("UploadFiles[0].Files", UPLOAD)] Attachments
        }

        public enum ColumnName
        {
            [StringValue("SubmittalNo")]
            [Description("SubmittalNo")]
            SubmittalNumber,

            [StringValue("SubmittalTitle")]
            [Description("SubmittalTitle")]
            SubmittalTitle,

            [StringValue("Originator.Name")]
            [Description("div/OriginatorId")]
            OriginatorName,

            [StringValue("SubmittalType.Name")]
            [Description("div/SubmittalTypeId")]
            SubmittalTypeName,

            [StringValue("StatusFlowItem.Name")]
            [Description("div/StatusId")]
            Status, 

            [StringValue("IsLocked")] IsLocked,

            [Description("")]
            [StringValue("LockedDateGrid")] LockedDateGrid,

            [Description("")]
            [StringValue("RecordLock.CreatedBy")] RecordLockCreatedBy,

            //Search Column Name
            [StringValue("Number")] Number
        }

        [ThreadStatic]
        internal static IList<EntryFieldType> tenantRoundOneRequiredFields;

        [ThreadStatic]
        internal static IList<EntryFieldType> tenantRoundTwoRequiredFields;

        [ThreadStatic]
        internal static IList<string> reqFieldLocators;

        public virtual IList<EntryFieldType> GetTenantRoundOneRequiredFields()
        {
            return tenantRoundOneRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.SubmittalNo,
                EntryFieldType.SubmittalTitle
            };
        }

        public virtual IList<EntryFieldType> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.SubmittalActionId,
                EntryFieldType.SegmentId,
                EntryFieldType.FeatureId,
                EntryFieldType.GradeId,
                EntryFieldType.SpecificationId,
                EntryFieldType.Attachments,
                EntryFieldType.Quantity
            };
        }

        public bool VerifySubmittalNumberIsDisplayed(string submittalNumber, bool isSearchPage = false, bool isStatusNew = false)
        {
            bool status = true;

            if(!isSearchPage)
                //Verify In Progress Status is displayed
                status = GridHelper.VerifyRecordIsDisplayed(
                    ColumnName.Status, 
                    isStatusNew ? "New": "In Progress",
                    TableHelper.TableType.Single);
            
            return status && 
                GridHelper.VerifyRecordIsDisplayed(
                    isSearchPage ? ColumnName.Number : ColumnName.SubmittalNumber, 
                    submittalNumber, 
                    TableHelper.TableType.Single);
        }

        public void VerifyIfRecordsAreDisplayedByFilter(Dictionary<ColumnName, string> values)
        {
            bool status = true;

            //Filter the record
            GridHelper.VerifyRecordIsDisplayed(ColumnName.SubmittalNumber, values[ColumnName.SubmittalNumber], TableHelper.TableType.Single);

            //Open the record
            GridHelper.ClickButtonForRow(Page.TableHelper.TableButton.View, string.Empty, false);

            //Go back to Revise Review Submittal page - so the record gets locked
            NavigateToPage.RMCenter_Review_Revise_Submittal();

            foreach (var valuePair in values)
            {
                status = status && 
                    GridHelper.VerifyRecordIsDisplayed(
                        valuePair.Key, 
                        valuePair.Value, 
                        TableHelper.TableType.Single, 
                        false, 
                        valuePair.Key.Equals(ColumnName.LockedDateGrid) ? FilterOperator.GreaterThanOrEqualTo : FilterOperator.Contains
                    );

                TestUtility.AddAssertionToList(status, string.Format("Filter Validation, Key: {0}, Value: {1}", valuePair.Key, valuePair.Value));
            }
        }

        #region #endregion Common Workflow Implementation class

        public virtual void LogintoSubmittal(UserType userType)
        {
            LoginAs(userType);
            PageAction.WaitForPageReady();
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            TestUtility.AddAssertionToList_VerifyPageHeader("Submittal Details", "LogintoQASubmittal()");
        }

        private KeyValuePair<EntryFieldType, string> PopulateFieldValue<T>(EntryFieldType entryField, T indexOrText, bool useContains = false)
        {
            string fieldType = entryField.GetString(true);
            Type argType = indexOrText.GetType();
            object argValue = null;
            bool isValidArg = false;

            KeyValuePair<EntryFieldType, string> fieldValuePair;
            string fieldValue = string.Empty;

            try
            {
                if (argType == typeof(string))
                {
                    isValidArg = true;
                    argValue = ConvertToType<string>(indexOrText);
                }
                else if (argType == typeof(int))
                {
                    isValidArg = true;
                    argValue = ConvertToType<int>(indexOrText);
                }

                if (isValidArg)
                {
                    switch (fieldType)
                    {
                        case TEXT:
                        case DATE:
                        case FUTUREDATE:
                            if (!((string)argValue).HasValue())
                            {
                                if (fieldType.Equals(DATE) || fieldType.Equals(FUTUREDATE))
                                {
                                    argValue = fieldType.Equals(DATE)
                                        ? GetShortDate()
                                        : GetFutureShortDate();
                                }
                                else
                                {
                                    //argValue = GetVarForEntryField(entryField);
                                    argValue = GetVar(entryField);
                                    int argValueLength = ((string)argValue).Length;

                                    By inputLocator = GetInputFieldByLocator(entryField);
                                    int elemMaxLength = int.Parse(PageAction.GetAttributeForElement(inputLocator, "maxlength"));

                                    argValue = argValueLength > elemMaxLength
                                        ? ((string)argValue).Substring(0, elemMaxLength)
                                        : argValue;
                                }

                                fieldValue = (string)argValue;
                            }

                            PageAction.EnterText(By.Id(entryField.GetString()), fieldValue);

                            break;

                        case DDL:
                        case MULTIDDL:
                            if ((argType == typeof(string) && !((string)argValue).HasValue()) || (int)argValue < 1)
                            {
                                argValue = 1;
                            }

                            bool isMultiselectDDList = false;

                            if (fieldType.Equals(MULTIDDL))
                            {
                                isMultiselectDDList = true;
                            }

                            PageAction.ExpandAndSelectFromDDList(entryField, argValue, useContains, isMultiselectDDList);

                            break;

                        case RDOBTN:
                        case CHKBOX:
                            PageAction.SelectRadioBtnOrChkbox(entryField);

                            break;

                        case UPLOAD:
                            PageAction.UploadFile();

                            break;
                        case NUMBER:
                            string xpath = string.Format("//input[contains(@id, '{0}')]/preceding-sibling::input", entryField);
                            Driver.FindElement(By.XPath(xpath)).SendKeys("1");

                            break;

                        default:
                            break;
                    }
                }
                else
                {
                    log.Error($"Argument type ({argType}) is not supported : {indexOrText.ToString()}");
                }
            }
            catch (Exception e)
            {
                log.Error(e.StackTrace);
            }

            return fieldValuePair = new KeyValuePair<EntryFieldType, string>(entryField, fieldValue);
        }

        public virtual Dictionary<UploadQASubmittal.ColumnName, string> PopulateFields(bool isSaveFlow = false)
        {
            var values = new Dictionary<ColumnName, string>();

            if (isSaveFlow)
                ClickSave();
            else
            {
                ClickElement(By.Id("CancelSubmittal"));
                NavigateToPage.RMCenter_Upload_QA_Submittal();
                ClickSubmitForward();
            }

            //Verify Required field validation is coming up for required fields
            VerifyRequiredFields(tenantRoundOneRequiredFields, isSaveFlow);

            PopulateFields(tenantRoundOneRequiredFields);

            if (isSaveFlow)
                ClickSave();
            else
                ClickSubmitForward();

            //Verify Required field validation is coming up for required fields
            VerifyRequiredFields(tenantRoundTwoRequiredFields, isSaveFlow);

            PopulateFields(tenantRoundTwoRequiredFields);

            foreach (ColumnName column in Enum.GetValues(typeof(ColumnName)))
            {
                var desc = StaticHelpers.GetDescription(column);
                var value = string.Empty;
                if (desc != null)
                {
                    if (desc.StartsWith("div"))
                    {
                        var valueArray = Driver.FindElement(By.XPath("//input[@id='" + desc.Split('/')[1] + "']/parent::div")).Text.Split('\n');
                        value = (valueArray.Length > 1) ? valueArray[1] : "In Progress";
                    }
                    else if (!string.IsNullOrEmpty(desc))
                        value = PageAction.GetText(By.Id(desc));
                    else
                        value = string.Empty;

                    //Add value to collection
                    values.Add(column, value);
                }
            }

            //Add 'Date' to list to be filtering list
            values[ColumnName.LockedDateGrid] = DateTime.Now.ToShortDateString();
            //Add 'Locked by' to list to be filtering list
            values[ColumnName.RecordLockCreatedBy] = ConfigUtil.GetCurrentUserEmail();

            if (isSaveFlow)
                ClickSave();
            else
                ClickSubmitForward();

            
            return values;
        }

        private void VerifyRequiredFields(IList<EntryFieldType> actualFields, bool isSaveFlow = false)
        {
            IList<string> actualReqFields = new List<string>();
            foreach (EntryFieldType field in actualFields)
            {
                if (isSaveFlow && !field.GetString().Equals("UploadFiles"))
                        actualReqFields.Add(field.GetString());
                else
                    actualReqFields.Add(field.GetString());
            }

            var expectedReqFields = GetTenantRequiredFieldLocators()
                                    .Where(i => !string.IsNullOrEmpty(i))
                                    .ToList();

            TestUtility.AddAssertionToList(PageAction.VerifyExpectedList(actualReqFields, expectedReqFields, 
                "VerifyUploadQASubmittalRequiredFields"), "VerifyUploadQASubmittalRequiredFields") ;
        }

        private void PopulateFields(IList<EntryFieldType> fields)
        {
            string submittalNumber = string.Empty;
            string title = string.Empty;

            foreach (EntryFieldType field in fields)
            {
                var kvpFromEntry = new KeyValuePair<EntryFieldType, string>();
                kvpFromEntry = PopulateFieldValue(field, string.Empty);

                log.Debug($"Added KeyValPair to expected table column values./nEntry Field: {kvpFromEntry.Key.ToString()} || Value: {kvpFromEntry.Value}");
            }
        }

        public virtual IList<string> GetTenantRequiredFieldLocators()
        {
            return reqFieldLocators = PageAction.GetAttributeForElements(new List<By>()
            {
                By.XPath("//span[contains(text(),'Required')]"),
                By.XPath("//span[contains(text(),'Required')]/parent::span"),
                By.XPath("//span[contains(text(),'You need at least 1 submittal file')]")
                
            }, "data-valmsg-for");
        }

        #endregion

    }

    public interface IUploadQASubmittal
    {
        IList<UploadQASubmittal.EntryFieldType> GetTenantRoundOneRequiredFields();
        IList<UploadQASubmittal.EntryFieldType> GetTenantRoundTwoRequiredFields();
        void LogintoSubmittal(UserType userType);
        Dictionary<UploadQASubmittal.ColumnName, string> PopulateFields(bool isSaveFlow = false);
        bool VerifySubmittalNumberIsDisplayed(string submittalNumber, bool isSearchPage = false, bool isStatusNew = false);
        void VerifyIfRecordsAreDisplayedByFilter(Dictionary<UploadQASubmittal.ColumnName, string> values);
        IList<string> GetTenantRequiredFieldLocators();
    }

    /// <summary>
    /// Tenant specific implementation of UploadQASubmittal
    /// </summary>

    #region Implementation specific to SH249

    public class UploadQASubmittal_SH249 : UploadQASubmittal
    {
        public UploadQASubmittal_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override IList<EntryFieldType> GetTenantRoundOneRequiredFields()
        {
            return tenantRoundOneRequiredFields = new List<EntryFieldType>()
                    {
                        EntryFieldType.SubmittalTitle
                    };
        }

        public override IList<EntryFieldType> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryFieldType>()
                    {
                        EntryFieldType.SubmittalActionId,
                        EntryFieldType.Attachments
                    };
        }
    }

    #endregion Implementation specific to SH249


    #region Implementation specific to SGWay

    public class UploadQASubmittal_SGWay : UploadQASubmittal
    {
        public UploadQASubmittal_SGWay(IWebDriver driver) : base(driver)
        {}

        public override IList<EntryFieldType> GetTenantRoundOneRequiredFields()
        {
            return tenantRoundOneRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.SubmittalTitle
            };
        }

        public override IList<EntryFieldType> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.SubmittalActionId,
                EntryFieldType.Attachments
            };
        }
    }

    #endregion Implementation specific to SGWay


    #region Implementation specific to I15South

    public class UploadQASubmittal_I15South : UploadQASubmittal
    {
        public UploadQASubmittal_I15South(IWebDriver driver) : base(driver)
        {}

        public override IList<EntryFieldType> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.SubmittalActionId,
                EntryFieldType.SegmentId,
                EntryFieldType.FeatureId,
                EntryFieldType.SpecificationId,
                EntryFieldType.Quantity,
                EntryFieldType.QuantityUnitId,
                EntryFieldType.Attachments,
            };
        }
    }

    #endregion Implementation specific to I15South


    #region Implementation specific to I15Tech

    public class UploadQASubmittal_I15Tech : UploadQASubmittal
    {
        public UploadQASubmittal_I15Tech(IWebDriver driver) : base(driver)
        {}

        public override IList<EntryFieldType> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryFieldType>()
            {
                EntryFieldType.SubmittalActionId,
                EntryFieldType.SegmentId,
                EntryFieldType.FeatureId,
                EntryFieldType.SpecificationId,
                EntryFieldType.Attachments
            };
        }
    }

    #endregion Implementation specific to I15Tech


    #region Implementation specific to LAX

    public class UploadQASubmittal_LAX : UploadQASubmittal
    {
        public UploadQASubmittal_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX
}