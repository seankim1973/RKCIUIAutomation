using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : PageHelper_Impl
    {
        public PageHelper()
        {
        }

        public PageHelper(IWebDriver driver) => this.Driver = driver;

        public const string DDL = "DDL";
        public const string TEXT = "TXT";
        public const string NUMBER = "NUMBER";
        public const string DATE = "DATE";
        public const string RDOBTN = "RDOBTN";
        public const string CHKBOX = "CHKBOX";
        public const string UPLOAD = "UPLOAD";
        public const string MULTIDDL = "MULTIDDL";
        public const string FUTUREDATE = "FUTUREDATE";
        public const string XPATH_TEXT = "XPATH";
        public const string AUTOPOPULATED = "AUTOPOPULATED";
        public const string AUTOPOPULATED_DDL = "AUTOPOPULATED_DDL";
        public const string AUTOPOPULATED_TEXT = "AUTOPOPULATED_TXT";
        public const string AUTOPOPULATED_DATE = "AUTOPOPULATED_DATE";

        /// <summary>
        /// Returns XPath in string format - //div[@class='k-content k-state-active']
        /// </summary>
        public static string ActiveContentXPath => "//div[@class='k-content k-state-active']";

        public static string GetMaxShortDate()
            => DateTime.MaxValue.Date.ToShortDateString();

        public static string GetShortDate(string shortDate = "", bool formatWithZero = false)
        {
            string mm = string.Empty;
            string dd = string.Empty;
            string yyyy = string.Empty;
            string date = DateTime.Now.ToShortDateString();

            if (shortDate.HasValue())
            {
                string[] splitShortDate = Regex.Split(shortDate, "/");
                mm = splitShortDate[0];
                dd = splitShortDate[1];
                yyyy = splitShortDate[2];

                if (formatWithZero)
                {
                    mm = mm.Length == 1
                        ? $"0{mm}"
                        : mm;
                    dd = dd.Length == 1
                        ? $"0{dd}"
                        : dd;
                }
                else
                {
                    mm = mm.StartsWith("0")
                        ? Regex.Replace(mm, "0", "")
                        : mm;
                    dd = dd.StartsWith("0")
                        ? Regex.Replace(dd, "0", "")
                        : dd;
                }

                date = $"{mm}/{dd}/{yyyy}";
            }

            return date;
        }

        private static string ModifyShortDate(string shortDate, double daysToModifyDateBy)
        {
            DateTime dateValue = DateTime.TryParse(shortDate, out dateValue)
                ? dateValue.AddDays(daysToModifyDateBy)
                : DateTime.Now.AddDays(daysToModifyDateBy);

            return dateValue.ToShortDateString();
        }

        public static string GetFutureShortDate(string shortDate = "", double daysToAdd = 1)
            => ModifyShortDate(shortDate, daysToAdd);

        public static string GetPastShortDate(string shortDate = "", double daysToSubtract = 1)
            => ModifyShortDate(shortDate, -daysToSubtract);

        public static string GetShortTime()
            => DateTime.Now.ToShortTimeString();

        private static string FormatTimeBlock(TimeBlock timeBlock)
        {
            string[] block = Regex.Split(timeBlock.ToString(), "_");
            string meridiem = block[0];
            string time = $"{block[1]}:{block[2]}";
            return $"{time} {meridiem}";
        }

        public static string GetShortDateTime(string shortDate = "", TimeBlock shortTime = TimeBlock.AM_12_00)
        {
            string date = shortDate.HasValue()
                ? shortDate
                : DateTime.Now.ToShortDateString();
            string time = shortTime.Equals(TimeBlock.AM_12_00)
                ? GetShortTime()
                : FormatTimeBlock(shortTime);
            return $"{date} {time}";
        }

        private string SetDDListFieldXpath<T>(T ddListID, bool useContainsOperator = false)
        {
            string _ddListID = string.Empty;
            string _ddFieldXpath = string.Empty;

            if (ddListID.GetType() == typeof(string))
            {
                _ddListID = BaseUtil.ConvertToType<string>(ddListID);
            }
            else if(ddListID is Enum)
            {
                _ddListID = BaseUtil.ConvertToType<Enum>(ddListID).GetString();
            }

            if (useContainsOperator)
            {
                if (_ddListID.Contains("Time"))
                {
                    _ddFieldXpath = $"//span[contains(@aria-controls,'{_ddListID}')]";
                }
                else
                {
                    _ddFieldXpath = $"//span[contains(@aria-owns,'{_ddListID}')]";
                }
            }
            else
            {
                if (_ddListID.Contains("Time"))
                {
                    _ddFieldXpath = $"//span[@aria-controls='{_ddListID}_timeview']";
                }
                else
                {
                    _ddFieldXpath = $"//span[@aria-owns='{_ddListID}_listbox']";
                }
            }

            return _ddFieldXpath;
        }

        private string SetDDListFieldExpandArrowXpath<T>(T ddListID, bool isMultiSelectDDList = false)
        {
            string _ddListID = string.Empty;
            string _ddArrowXpath = string.Empty;

            if (ddListID is Enum)
            {
                _ddListID = ConvertToType<Enum>(ddListID).GetString();
            }
            else if (ddListID.GetType().Equals(typeof(string)))
            {
                _ddListID = ConvertToType<string>(ddListID);
            }

            if (isMultiSelectDDList)
            {
                _ddArrowXpath = $"//select[@id='{_ddListID}']/parent::div";
            }
            else
            {
                if (_ddListID.Contains("Time"))
                {
                    _ddArrowXpath = $"{SetDDListFieldXpath(_ddListID)}/parent::span/span";
                }
                else
                {
                    _ddArrowXpath = $"{SetDDListFieldXpath(_ddListID)}//span[@class='k-select']/span";
                }
            }

            return _ddArrowXpath;
        }

        private string SetDDListCurrentSelectionXpath<T>(T ddListID, bool useContainsOperator = false)
            => $"{SetDDListFieldXpath(ddListID, useContainsOperator)}//span[@class='k-input']";

        private string SetMultiSelectDDListCurrentSelectionXpath<T>(T multiSelectDDListID)
        {
            string multiSelectDDListValues = string.Empty;
            string multiSelectID = string.Empty;

            if (multiSelectDDListID is Enum)
            {
                multiSelectID = ConvertToType<Enum>(multiSelectDDListID).GetString();
            }
            else if (multiSelectDDListID.GetType().Equals(typeof(string)))
            {
                multiSelectID = ConvertToType<string>(multiSelectDDListID);
            }

            multiSelectDDListValues = $"//ul[@id='{multiSelectID}_taglist']/li/span[1]";
            return multiSelectDDListValues;
        }

        private string SetMainNavMenuXpath(Enum navEnum)
            => $"//li[@class='dropdown']/a[contains(text(),'{navEnum.GetString()}')]";

        private string SetNavMenuXpath(Enum navEnum, Enum parentNavEnum = null)
            => parentNavEnum == null
                ? $"//ul[@class='dropdown-menu']/li/a[text()='{navEnum.GetString()}']"
                : $"//a[contains(text(),'{parentNavEnum.GetString()}')]/following-sibling::ul[@class='dropdown-menu']/li/a[text()='{navEnum.GetString()}']";

        private string SetInputFieldXpath<T>(T inputFieldLabelOrID)
        {
            Type argType = inputFieldLabelOrID.GetType();
            object argValue = null;

            if (inputFieldLabelOrID is string)
            {
                argValue = ConvertToType<string>(inputFieldLabelOrID);
                argValue = $"//label[contains(text(),'{(string)argValue}')]/following::input[1]";
            }
            else if(inputFieldLabelOrID is Enum)
            {
                argValue = ConvertToType<Enum>(inputFieldLabelOrID);               
                argValue = $"//input[@id='{((Enum)argValue).GetString()}']";
            }

            return (string)argValue;
        }

        /// <summary>
        /// [bool] useContains arg defaults to false and is ignored if arg [I]itemIndexOrName is int type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="ddListID"></param>
        /// <param name="itemIndexOrName"></param>
        /// <param name="useContains"></param>
        /// <returns></returns>
        private string SetDDListItemsXpath<T, I>(T ddListID, I itemIndexOrName, bool useContains = false)
        {
            string invalidArgMsg = string.Empty;
            string _ddListID = string.Empty;
            string ddListXPath = string.Empty;
            string itemValueXPath = string.Empty;

            try
            {
                object ddListIdType = ddListID.GetType();
                object argValueType = itemIndexOrName.GetType();

                if (ddListID.GetType().Equals(typeof(string)))
                {
                    _ddListID = BaseUtil.ConvertToType<string>(ddListID);
                }
                else if (ddListID is Enum)
                {
                    _ddListID = BaseUtil.ConvertToType<Enum>(ddListID).GetString();
                }
                else
                {
                    invalidArgMsg = $"parameter {ddListID} [ddListID] is type of {ddListID.GetType()}, but should be string or Enum type";
                    throw new ArgumentException(invalidArgMsg);
                }

                if (_ddListID.Contains("Time"))
                {
                    ddListXPath = $"//ul[@id='{_ddListID}_timeview']";
                }
                else
                {
                    ddListXPath = $"//div[@id='{_ddListID}-list']";
                }

                if (argValueType.Equals(typeof(string)))
                {
                    string argValue = BaseUtil.ConvertToType<string>(itemIndexOrName);

                    if (useContains)
                    {
                        itemValueXPath = $"contains(text(),'{argValue}')";
                    }
                    else
                    {
                        itemValueXPath = $"text()='{argValue}'";
                    }
                }
                else if (argValueType.Equals(typeof(int)))
                {
                    int itemIndex = BaseUtil.ConvertToType<int>(itemIndexOrName);

                    if (_ddListID.Contains("Time"))
                    {
                        itemIndex = itemIndex + 1;
                    }

                    itemValueXPath = itemIndex.ToString();
                }
                else
                {
                    invalidArgMsg = $"parameter {itemIndexOrName} [itemIndexOrName] is type of {itemIndexOrName.GetType()}, but should be string or int type";
                    throw new ArgumentException(invalidArgMsg);
                }
            }
            catch (ArgumentException ae)
            {
                log.Error($"Invalid input type: {ae.Message}\n{ae.StackTrace}");
                throw;
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }
            
            return $"{ddListXPath}//li[{itemValueXPath}]";
        }

        private string SetDDListItemsXpath(Enum ddListID, int itemIndex)
            => $"//div[@id='{ddListID.GetString()}-list']//li[{itemIndex}]";

        private string SetTextInputFieldByLocator(Enum inputEnum)
            => $"//input[@id='{inputEnum.GetString()}']";

        private string SetTextAreaFieldByLocator(Enum textAreaEnum)
            => $"//textarea[@id='{textAreaEnum.GetString()}']";

        private string SetButtonXpath(string buttonName)
            => $"//a[text()='{buttonName}']";

        private string SetInputButtonXpath(string buttonName)
            => $"//input[@value='{buttonName}']";


        public override By GetSubmitButtonByLocator(Enum buttonValue, bool submitType = true)
        {
            string submitTypeXPath = string.Empty;

            if (submitType)
            {
                submitTypeXPath = "[@type='submit']";
            }

            By locator = By.XPath($"//input{submitTypeXPath}[@value='{buttonValue.GetString()}']");
            return locator;
        }

        public override By GetMainNavMenuByLocator(Enum navEnum)
            => By.XPath(SetMainNavMenuXpath(navEnum));

        public override By GetNavMenuByLocator(Enum navEnum, Enum parentNavEnum = null)
            => By.XPath(SetNavMenuXpath(navEnum, parentNavEnum));

        public override By GetInputFieldByLocator<T>(T inputFieldLabelOrID)
            => By.XPath(SetInputFieldXpath(inputFieldLabelOrID));

        public override By GetDDListByLocator(Enum ddListID)
            => By.XPath(SetDDListFieldXpath(ddListID));

        public override By GetDDListCurrentSelectionByLocator<T>(T ddListID)
            => By.XPath(SetDDListCurrentSelectionXpath(ddListID));

        public override By GetDDListCurrentSelectionInActiveTabByLocator(Enum ddListID, bool useContainsOperator = true)
            => By.XPath($"{ActiveContentXPath}{SetDDListCurrentSelectionXpath(ddListID, useContainsOperator)}");

        public override By GetMultiSelectDDListCurrentSelectionByLocator<T>(T multiSelectDDListID)
            => By.XPath(SetMultiSelectDDListCurrentSelectionXpath(multiSelectDDListID));

        public override By GetExpandDDListButtonByLocator<T>(T ddListID, bool isMultiSelectDDList = false)
            => By.XPath(SetDDListFieldExpandArrowXpath(ddListID, isMultiSelectDDList));

        public override By GetDDListItemsByLocator<T, I>(T ddListID, I itemIndexOrName, bool useContainsOperator = false)
            => By.XPath(SetDDListItemsXpath(ddListID, itemIndexOrName, useContainsOperator));

        public override By GetTextInputFieldByLocator(Enum inputEnum)
            => By.XPath(SetTextInputFieldByLocator(inputEnum));

        public override By GetTextAreaFieldByLocator(Enum textAreaEnum)
            => By.XPath(SetTextAreaFieldByLocator(textAreaEnum));

        public override By GetButtonByLocator(string buttonName)
            => By.XPath(SetButtonXpath(buttonName));

        public override By GetInputButtonByLocator<T>(T buttonName)
        {
            Type argType = buttonName.GetType();
            string buttonVal = string.Empty;

            if (argType == typeof(string))
            {
                buttonVal = ConvertToType<string>(buttonName);
            }
            else if (argType == typeof(Enum))
            {
                buttonVal = ConvertToType<Enum>(buttonName).GetString();
            }

            return By.XPath(SetInputButtonXpath(buttonVal));
        }

        internal IList<IWebElement> CheckForRequiredInputFields()
        {
            IList<IWebElement> inputFieldElements = null;
            By inputXPath = null;

            string[] requiredLabelErrorOffset = new string[] { "Required", "required" };
            string[] requiredInputTypeXPathOffset = new string[] { "span/input[@id]", "div/input", "div/select" };

            inputFieldElements = new List<IWebElement>();

            for (int i = 0; i < requiredLabelErrorOffset.Length; i++)
            {
                for (int x = 0; x < requiredInputTypeXPathOffset.Length; x++)
                {
                    inputXPath = By.XPath($"//span[contains(@class, 'ValidationErrorMessage')][contains(text(), '{requiredLabelErrorOffset[i]}')]/following-sibling::{requiredInputTypeXPathOffset[x]}");
                    ((List<IWebElement>)inputFieldElements).AddRange(GetElements(inputXPath));
                } 
            }

            Console.WriteLine($"@@@@@@ FOUND {inputFieldElements.Count} REQUIRED ELEMENTs @@@@@@");
            return inputFieldElements;
        }

        public override IList<string> PopulateEntryFieldsAndGetValuesArray(bool requiredFieldsOnly = false, int integerInputMinValue = 1, int integerInputMaxValue = 99)
        {
            IList<IWebElement> inputFieldElements = null;
            IList<string> fieldValuesList = new List<string>();
            By inputXPath = null;

            if (requiredFieldsOnly)
            {
                inputFieldElements = CheckForRequiredInputFields();
            }
            else
            {
                inputXPath = By.XPath("//div[@id='HeaderDiv']//div[@class='row']/div[contains(@class,'col')]/div[@class='form-group']//input[@id]");
                inputFieldElements = GetElements(inputXPath);
            }

            for (int i = 0; i < inputFieldElements.Count; i++)
            {
                By byIdLocator = null;
                IWebElement currentElem = null;

                string inputId = string.Empty;
                string labelXPath = string.Empty;
                string fieldLabel = string.Empty;
                string inputValue = string.Empty;
                string fieldValue = string.Empty;
                string dataRoleType = string.Empty;
                string currentElemXPath = string.Empty;
                string inputTypeAttribute = string.Empty;
                bool fieldIsWithoutDataRole = false;
                bool fieldIsMultiSelectDDL = false;

                currentElem = inputFieldElements[i];
                inputTypeAttribute = currentElem.GetAttribute("type");

                Console.WriteLine($"@@@@@@@@@ {currentElem} : NO {i} - {inputTypeAttribute} @@@@@@@@@");

                inputId = currentElem.GetAttribute("id");
                byIdLocator = By.Id(inputId);
                currentElemXPath = $"//input[@id='{inputId}']";
                dataRoleType = currentElem.GetAttribute("data-role");

                if (inputTypeAttribute.HasValue())
                {
                    Console.WriteLine($"@@@@@@@@@ {currentElem} : NO {i} TYPE ATTRIB HASVALUE : {inputTypeAttribute} @@@@@@@@@");

                    if (inputTypeAttribute.Equals("hidden"))
                    {
                        fieldValue = GetText(byIdLocator);
                    }
                    else
                    {
                        if (!dataRoleType.HasValue())
                        {
                            fieldIsWithoutDataRole = true;

                            if (inputId.HasValue())
                            {
                                fieldValue = GetText(byIdLocator, logReport: false);

                                if (!fieldValue.HasValue())
                                {
                                    inputValue = GetVar(inputId);
                                    EnterText(byIdLocator, inputValue);
                                    fieldValue = inputValue;
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!dataRoleType.HasValue())
                    {
                        fieldValue = GetText(byIdLocator, logReport: false);
                        fieldIsWithoutDataRole = true;
                    }
                }

                if (dataRoleType.HasValue())
                {
                    if (dataRoleType.Equals("dropdownlist") || dataRoleType.Equals("multiselect"))
                    {
                        if (dataRoleType.Equals("multiselect"))
                        {
                            fieldIsMultiSelectDDL = true;
                        }

                        fieldValue = GetTextFromDDL(inputId, fieldIsMultiSelectDDL);

                        if (!fieldValue.HasValue() || fieldValue.Equals("Please Select"))
                        {
                            try
                            {
                                ExpandAndSelectFromDDList(inputId, 2, isMultiSelectDDList: fieldIsMultiSelectDDL);
                            }
                            catch (NoSuchElementException)
                            {
                                ClickInMainBodyAwayFromField();
                                ExpandAndSelectFromDDList(inputId, 1, isMultiSelectDDList: fieldIsMultiSelectDDL);
                            }

                            fieldValue = GetTextFromDDL(inputId, fieldIsMultiSelectDDL);
                        }
                    }
                    else if (dataRoleType.Equals("maskedtextbox"))
                    {
                        fieldValue = GetText(byIdLocator);

                        if (!fieldValue.HasValue())
                        {
                            inputValue = GetVar(inputId);
                            EnterText(byIdLocator, inputValue);
                            fieldValue = inputValue;
                        }
                    }
                    else if (dataRoleType.Equals("numerictextbox"))
                    {
                        fieldValue = GetText(byIdLocator);

                        if (!fieldValue.HasValue())
                        {
                            inputValue = GetRandomInteger(integerInputMinValue, integerInputMaxValue).ToString();
                            EnterText(byIdLocator, inputValue);
                            fieldValue = inputValue;
                        }
                    }
                }

                try
                {
                    string labelParentXPath = "ancestor::span";

                    if (fieldIsWithoutDataRole)
                    {
                        if (fieldIsMultiSelectDDL)
                        {
                            labelParentXPath = "ancestor::div[contains(@class, 'k-multiselect k-header')]";
                        }
                        else
                        {
                            labelParentXPath = "parent::div";
                        }
                    }

                    labelXPath = $"{currentElemXPath}/{labelParentXPath}/preceding-sibling::label";
                    fieldLabel = GetText(By.XPath(labelXPath), logReport: false);
                }
                catch (NoSuchElementException)
                {
                    fieldLabel = inputId;
                }
                
                var kvPair = $"{fieldLabel}::{fieldValue}";
                fieldValuesList.Add(kvPair);
                Console.WriteLine($"ADDED TO KVPairList : {kvPair}");
            }

            return fieldValuesList;
        }


    }

    public abstract class PageHelper_Impl : PageInteraction, IPageHelper
    {
        public abstract By GetButtonByLocator(string buttonName);
        public abstract By GetDDListByLocator(Enum ddListID);
        public abstract By GetDDListCurrentSelectionByLocator<T>(T ddListID);
        public abstract By GetDDListCurrentSelectionInActiveTabByLocator(Enum ddListID, bool useContainsOperator = true);
        public abstract By GetDDListItemsByLocator<T, I>(T ddListID, I itemIndexOrName, bool useContainsOperator = false);
        public abstract By GetExpandDDListButtonByLocator<T>(T ddListID, bool isMultiSelectDDList = false);
        public abstract By GetInputButtonByLocator<T>(T buttonName);
        public abstract By GetInputFieldByLocator<T>(T inputFieldLabelOrID);
        public abstract By GetMainNavMenuByLocator(Enum navEnum);
        public abstract By GetMultiSelectDDListCurrentSelectionByLocator<T>(T multiSelectDDListID);
        public abstract By GetNavMenuByLocator(Enum navEnum, Enum parentNavEnum = null);
        public abstract By GetSubmitButtonByLocator(Enum buttonValue, bool submitType = true);
        public abstract By GetTextAreaFieldByLocator(Enum textAreaEnum);
        public abstract By GetTextInputFieldByLocator(Enum inputEnum);
        public abstract IList<string> PopulateEntryFieldsAndGetValuesArray(bool requiredFieldsOnly = false, int integerInputMinValue = 1, int integerInputMaxValue = 99);
    }
}