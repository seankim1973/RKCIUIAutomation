﻿using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class UploadDEVSubmittal : PageBase, IUploadDEVSubmittal
    {
        public UploadDEVSubmittal()
        {}

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

        //GLX and LAX - StringValue[0] = table tab name, StringValue[1] = Table content reference id
        public enum TableTab
        {
            [StringValue("Unsent Transmissions", "TransmissionGridNew")] UnsentTransmissions,
            [StringValue("Pending Transmissions", "TransmissionGridPending")] PendingTransmissions,
            [StringValue("Transmitted Records", "TransmissionGridForwarded")] TransmittedRecords
        }

        public enum EntryField
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
            [StringValue("Number")] Number
        }

        [ThreadStatic]
        internal static IList<EntryField> tenantRoundOneRequiredFields;

        [ThreadStatic]
        internal static IList<EntryField> tenantRoundTwoRequiredFields;

        public virtual IList<EntryField> GetTenantRoundOneRequiredFields()
        {
            return tenantRoundOneRequiredFields = new List<EntryField>()
            {
                EntryField.SubmittalNo,
                EntryField.SubmittalTitle
            };
        }

        public virtual IList<EntryField> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryField>()
            {
                EntryField.SubmittalActionId,
                EntryField.SegmentId,
                EntryField.FeatureId,
                EntryField.GradeId,
                EntryField.SpecificationId,
                EntryField.Attachments
            };
        }

        public bool VerifySubmittalNumberIsDisplayed(string submittalNumber, bool isSearch = false)
        {
            return GridHelper.VerifyRecordIsDisplayed(
                isSearch ? ColumnName.Number : ColumnName.SubmittalNumber,
                submittalNumber,
                TableHelper.TableType.Single
            );

            //if (isSearch)
            //    GridHelper.FilterTableColumnByValue(ColumnName.Number, submittalNumber, TableHelper.TableType.Single, FilterOperator.EqualTo);
            //else
            //    GridHelper.FilterTableColumnByValue(ColumnName.SubmittalNumber, submittalNumber, TableHelper.TableType.Single, FilterOperator.EqualTo);

            //return true;
        }

        #region #endregion Common Workflow Implementation class

        public virtual void LogintoQASubmittal(UserType userType)
        {
            LoginAs(userType);
            PageAction.WaitForPageReady();
            NavigateToPage.RMCenter_Upload_QA_Submittal();
            TestUtility.AddAssertionToList_VerifyPageHeader("Transmissions", "LogintoQASubmittal()");
        }

        private KeyValuePair<EntryField, string> PopulateFieldValue<T>(EntryField entryField, T indexOrText, bool useContains = false)
        {
            string fieldType = entryField.GetString(true);
            Type argType = indexOrText.GetType();
            object argValue = null;
            bool isValidArg = false;

            KeyValuePair<EntryField, string> fieldValuePair;
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
                                    int elemMaxLength = int.Parse(PageAction.GetAttribute(inputLocator, "maxlength"));

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

            return fieldValuePair = new KeyValuePair<EntryField, string>(entryField, fieldValue);
        }

        public virtual string PopulateFields(int round)
        {
            var requiredFields = round.Equals(1) ? tenantRoundOneRequiredFields : tenantRoundTwoRequiredFields;
            string submittalNumber = string.Empty;

            foreach (EntryField field in requiredFields)
            {
                var kvpFromEntry = new KeyValuePair<EntryField, string>();
                kvpFromEntry = PopulateFieldValue(field, string.Empty);

                if (field.Equals(EntryField.SubmittalNo))
                    submittalNumber = kvpFromEntry.Value;

                log.Debug($"Added KeyValPair to expected table column values./nEntry Field: {kvpFromEntry.Key.ToString()} || Value: {kvpFromEntry.Value}");
            }

            return submittalNumber;
        }

        #endregion

    }

    public interface IUploadDEVSubmittal
    {
        IList<UploadDEVSubmittal.EntryField> GetTenantRoundOneRequiredFields();
        IList<UploadDEVSubmittal.EntryField> GetTenantRoundTwoRequiredFields();
        void LogintoQASubmittal(UserType userType);
        string PopulateFields(int round);
        bool VerifySubmittalNumberIsDisplayed(string submittalNumber, bool isSearch = false);
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

        public override IList<EntryField> GetTenantRoundOneRequiredFields()
        {
            return tenantRoundOneRequiredFields = new List<EntryField>()
                    {
                        EntryField.SubmittalTitle
                    };
        }

        public override IList<EntryField> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryField>()
                    {
                        EntryField.SubmittalActionId,
                        EntryField.Attachments
                    };
        }
    }

    #endregion Implementation specific to SH249


    #region Implementation specific to SGWay

    public class UploadDEVSubmittal_SGWay : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_SGWay(IWebDriver driver) : base(driver)
        { }

        public override IList<EntryField> GetTenantRoundOneRequiredFields()
        {
            return tenantRoundOneRequiredFields = new List<EntryField>()
            {
                EntryField.SubmittalTitle
            };
        }

        public override IList<EntryField> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryField>()
            {
                EntryField.SubmittalActionId,
                EntryField.Attachments
            };
        }
    }

    #endregion Implementation specific to SGWay


    #region Implementation specific to I15South

    public class UploadDEVSubmittal_I15South : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_I15South(IWebDriver driver) : base(driver)
        { }

        public override IList<EntryField> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryField>()
            {
                EntryField.SubmittalActionId,
                EntryField.SegmentId,
                EntryField.FeatureId,
                EntryField.SpecificationId,
                EntryField.Quantity,
                EntryField.QuantityUnitId,
                EntryField.Attachments
            };
        }
    }

    #endregion Implementation specific to I15South


    #region Implementation specific to I15Tech

    public class UploadDEVSubmittal_I15Tech : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_I15Tech(IWebDriver driver) : base(driver)
        { }

        public override IList<EntryField> GetTenantRoundTwoRequiredFields()
        {
            return tenantRoundTwoRequiredFields = new List<EntryField>()
            {
                EntryField.SubmittalActionId,
                EntryField.SegmentId,
                EntryField.FeatureId,
                EntryField.SpecificationId,
                EntryField.Attachments
            };
        }
    }

    #endregion Implementation specific to I15Tech


    #region Implementation specific to LAX

    public class UploadDEVSubmittal_LAX : UploadDEVSubmittal
    {
        public UploadDEVSubmittal_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX
}