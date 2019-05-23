using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using System;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : BaseClass, IPageHelper
    {
        public PageHelper()
        {
        }

        public PageHelper(IWebDriver driver) => this.Driver = driver;

        public const string TEXT = "TXT";
        public const string DDL = "DDL";
        public const string DATE = "DATE";
        public const string FUTUREDATE = "FUTUREDATE";
        public const string MULTIDDL = "MULTIDDL";
        public const string RDOBTN = "RDOBTN";
        public const string CHKBOX = "CHKBOX";
        public const string UPLOAD = "UPLOAD";

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

        private string SetDDListFieldXpath<T>(T ddListID)
        {
            BaseUtils baseUtils = new BaseUtils();

            string _ddListID = (ddListID.GetType() == typeof(string))
                ? baseUtils.ConvertToType<string>(ddListID)
                : baseUtils.ConvertToType<Enum>(ddListID).GetString();

            string _ddFieldXpath = _ddListID.Contains("Time")
                ? $"//span[@aria-controls='{_ddListID}_timeview']"
                : $"//span[@aria-owns='{_ddListID}_listbox']";

            return _ddFieldXpath;
        }

        private string SetDDListFieldExpandArrowXpath<T>(T ddListID, bool isMultiSelectDDList = false)
        {
            BaseUtils baseUtils = new BaseUtils();

            string _ddListID = (ddListID.GetType() == typeof(string))
                ? baseUtils.ConvertToType<string>(ddListID)
                : baseUtils.ConvertToType<Enum>(ddListID).GetString();

            string _ddArrowXpath = isMultiSelectDDList
                ? $"//select[@id='{_ddListID}']/parent::div"
                :_ddListID.Contains("Time")
                    ? $"{SetDDListFieldXpath(_ddListID)}/parent::span/span"
                    : $"{SetDDListFieldXpath(_ddListID)}//span[@class='k-select']/span";

            return _ddArrowXpath;
        }

        private string SetDDListCurrentSelectionXpath(Enum ddListID)
            => $"{SetDDListFieldXpath(ddListID)}//span[@class='k-input']";

        private string SetMultiSelectDDListCurrentSelectionXpath(Enum multiSelectDDListID)
            => $"//ul[@id='{multiSelectDDListID.GetString()}_taglist']/li/span[1]";

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
                argValue = Utility.ConvertToType<string>(inputFieldLabelOrID);
                argValue = $"//label[contains(text(),'{(string)argValue}')]/following::input[1]";
            }
            else if(inputFieldLabelOrID is Enum)
            {
                argValue = Utility.ConvertToType<Enum>(inputFieldLabelOrID);               
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
            string _ddListID = ddListID.GetType() == typeof(string)
                ? Utility.ConvertToType<string>(ddListID)
                : Utility.ConvertToType<Enum>(ddListID).GetString();

            string ddListXPath = _ddListID.Contains("Time")
                ? $"//ul[@id='{_ddListID}_timeview']"
                : $"//div[@id='{_ddListID}-list']";

            string itemValueXPath = string.Empty;

            if (itemIndexOrName.GetType().Equals(typeof(string)))
            {
                var argName = Utility.ConvertToType<string>(itemIndexOrName);
                itemValueXPath = useContains
                    ? $"contains(text(),'{argName}')"
                    : $"text()='{argName}'";
            }
            else if (itemIndexOrName.GetType().Equals(typeof(int)))
            {
                int itemIndex = Utility.ConvertToType<int>(itemIndexOrName);
                itemIndex = _ddListID.Contains("Time")
                    ? itemIndex + 1
                    : itemIndex;
                itemValueXPath = itemIndex.ToString();
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

        public By GetSubmitButtonByLocator(Enum buttonValue, bool submitType = true)
        {
            string submitTypeXPath = submitType 
                ? "[@type='submit']"
                : "";
            By locator = By.XPath($"//input{submitTypeXPath}[@value='{buttonValue.GetString()}']");
            return locator;
        }

        public By GetMainNavMenuByLocator(Enum navEnum)
            => By.XPath(SetMainNavMenuXpath(navEnum));

        public By GetNavMenuByLocator(Enum navEnum, Enum parentNavEnum = null)
            => By.XPath(SetNavMenuXpath(navEnum, parentNavEnum));

        public By GetInputFieldByLocator<T>(T inputFieldLabelOrID)
            => By.XPath(SetInputFieldXpath(inputFieldLabelOrID));

        public By GetDDListByLocator(Enum ddListID)
            => By.XPath(SetDDListFieldXpath(ddListID));

        public By GetDDListCurrentSelectionByLocator(Enum ddListID)
            => By.XPath(SetDDListCurrentSelectionXpath(ddListID));

        public By GetMultiSelectDDListCurrentSelectionByLocator(Enum multiSelectDDListID)
            => By.XPath(SetMultiSelectDDListCurrentSelectionXpath(multiSelectDDListID));

        public By GetExpandDDListButtonByLocator<T>(T ddListID, bool isMultiSelectDDList = false)
            => isMultiSelectDDList
                ? By.XPath($"//select[@id='{Utility.ConvertToType<Enum>(ddListID).GetString()}']/parent::div")
                : By.XPath(SetDDListFieldExpandArrowXpath(ddListID));

        /// <summary>
        /// [bool] useContains arg defaults to false and is ignored if arg [I]itemIndexOrName is int type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="I"></typeparam>
        /// <param name="ddListID"></param>
        /// <param name="itemIndexOrName"></param>
        /// <param name="useContains"></param>
        /// <returns></returns>
        public By GetDDListItemsByLocator<T, I>(T ddListID, I itemIndexOrName, bool useContains = false)
            => By.XPath(SetDDListItemsXpath(ddListID, itemIndexOrName, useContains));

        public By GetTextInputFieldByLocator(Enum inputEnum)
            => By.XPath(SetTextInputFieldByLocator(inputEnum));

        public By GetTextAreaFieldByLocator(Enum textAreaEnum)
            => By.XPath(SetTextAreaFieldByLocator(textAreaEnum));

        public By GetButtonByLocator(string buttonName)
            => By.XPath(SetButtonXpath(buttonName));

        public By GetInputButtonByLocator<T>(T buttonName)
        {
            Type argType = buttonName.GetType();
            string buttonVal = string.Empty;

            if (argType == typeof(string))
            {
                buttonVal = Utility.ConvertToType<string>(buttonName);
            }
            else if (argType == typeof(Enum))
            {
                buttonVal = Utility.ConvertToType<Enum>(buttonName).GetString();
            }

            return By.XPath(SetInputButtonXpath(buttonVal));
        }
    }
}