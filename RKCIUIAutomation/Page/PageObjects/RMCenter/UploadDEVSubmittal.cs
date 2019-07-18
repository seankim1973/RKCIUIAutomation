using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.Search;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class UploadDEVSubmittal : PageBase, IUploadDEVSubmittal
    {
        public UploadDEVSubmittal()
        {

        }

        public UploadDEVSubmittal(IWebDriver driver)
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
            IUploadDEVSubmittal instance = new UploadDEVSubmittal(driver);

            switch (tenantName)
            {
                case TenantName.SGWay:
                    log.Info($"###### using UploadDEVSubmittal_SGWay instance ###### ");
                    instance = new UploadDEVSubmittal_SGWay(driver);
                    break;
                case TenantName.SH249:
                    log.Info($"###### using  UploadDEVSubmittal_SH249 instance ###### ");
                    instance = new UploadDEVSubmittal_SH249(driver);
                    break;
                case TenantName.I15South:
                    log.Info($"###### using  UploadDEVSubmittal_I15South instance ###### ");
                    instance = new UploadDEVSubmittal_I15South(driver);
                    break;
                case TenantName.I15Tech:
                    log.Info($"###### using UploadDEVSubmittal_I15Tech instance ###### ");
                    instance = new UploadDEVSubmittal_I15Tech(driver);
                    break;
                case TenantName.LAX:
                    log.Info($"###### using UploadDEVSubmittal_LAX instance ###### ");
                    instance = new UploadDEVSubmittal_LAX(driver);
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
            [StringValue("QuantityUnitId", UPLOAD)] QuantityUnitId,
            [StringValue("Quantity", UPLOAD)] Quantity,
            [StringValue("UploadFiles", UPLOAD)] Attachments
        }

        public enum ColumnName
        {
            [StringValue("submittalNo")] SubmittalNumber,
            [StringValue("Number")] Number,
            [StringValue("StatusFlowItem.Name")] Status
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
                EntryFieldType.Attachments
            };
        }

        public bool VerifySubmittalNumberIsDisplayed(string submittalNumber, bool isSearchPage = false, bool isStatusNew = false)
        {
            bool status = true;

            if (!isSearchPage)
                //Verify In Progress Status is displayed
                status = GridHelper.VerifyRecordIsDisplayed(
                    ColumnName.Status,
                    isStatusNew ? "New" : "In Progress",
                    TableHelper.TableType.Single);

            return status &&
                GridHelper.VerifyRecordIsDisplayed(
                    isSearchPage ? ColumnName.Number : ColumnName.SubmittalNumber,
                    submittalNumber,
                    TableHelper.TableType.Single);
        }

        #region #endregion Common Workflow Implementation class

        public virtual void LogintoSubmittal(UserType userType)
        {
            LoginAs(userType);
            PageAction.WaitForPageReady();
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
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

        public virtual KeyValuePair<string, string> PopulateFields(bool isSaveFlow = false)
        {
            KeyValuePair<string, string> valuePair;

            if (isSaveFlow)
                ClickSave();
            else
            {
                ClickElement(By.Id("CancelSubmittal"));
                NavigateToPage.RMCenter_Upload_DEV_Submittal();
                ClickSubmitForward();
            }

            //Verify Required field validation is coming up for required fields
            VerifyRequiredFields(tenantRoundOneRequiredFields, isSaveFlow);

            PopulateFields(tenantRoundOneRequiredFields);

            if (isSaveFlow)
                ClickSave();
            else
                ClickSubmitForward();

            valuePair = new KeyValuePair<string, string>(PageAction.GetText(By.Id("SubmittalTitle")), PageAction.GetText(By.Id("SubmittalNo")));

            //Verify Required field validation is coming up for required fields
            VerifyRequiredFields(tenantRoundTwoRequiredFields, isSaveFlow);

            PopulateFields(tenantRoundTwoRequiredFields);

            if (isSaveFlow)
                ClickSave();
            else
                ClickSubmitForward();

            return valuePair;
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
                "VerifyUploadQASubmittalRequiredFields"), "VerifyUploadQASubmittalRequiredFields");
        }

        private void PopulateFields(IList<EntryFieldType> fields)
        {
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

    public interface IUploadDEVSubmittal
    {
        IList<UploadDEVSubmittal.EntryFieldType> GetTenantRoundOneRequiredFields();
        IList<UploadDEVSubmittal.EntryFieldType> GetTenantRoundTwoRequiredFields();
        void LogintoSubmittal(UserType userType);
        KeyValuePair<string, string> PopulateFields(bool isSaveFlow = false);
        bool VerifySubmittalNumberIsDisplayed(string submittalNumber, bool isSearchPage = false, bool isStatusNew = false);
        IList<string> GetTenantRequiredFieldLocators();
    }

    /// <summary>
    /// Tenant specific implementation of UploadDEVSubmittal
    /// </summary>

    #region Implementation specific to SH249

    public class UploadDEVSubmittal_SH249 : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SH249


    #region Implementation specific to SGWay

    public class UploadDEVSubmittal_SGWay : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to SGWay


    #region Implementation specific to I15South

    public class UploadDEVSubmittal_I15South : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South


    #region Implementation specific to I15Tech

    public class UploadDEVSubmittal_I15Tech : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15Tech


    #region Implementation specific to LAX

    public class UploadDEVSubmittal_LAX : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_LAX(IWebDriver driver) : base(driver)
        {
        }

        public override void LogintoSubmittal(UserType userType)
        {
            LoginAs(userType);
            PageAction.WaitForPageReady();
            NavigateToPage.RMCenter_Upload_DEV_Submittal();
            TestUtility.AddAssertionToList_VerifyPageHeader("Submittal Details", "LogintoQASubmittal()");
        }
    }

    #endregion Implementation specific to LAX
}