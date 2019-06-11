using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using System;
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
        public const string DATE = "DATE";
        public const string RDOBTN = "RDOBTN";
        public const string CHKBOX = "CHKBOX";
        public const string UPLOAD = "UPLOAD";
        public const string MULTIDDL = "MULTIDDL";
        public const string FUTUREDATE = "FUTUREDATE";
        public const string AUTOPOPULATED = "AUTOPOPULATED";

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

        private string SetDDListFieldXpath<T>(T ddListID)
        {
            string _ddListID = (ddListID.GetType() == typeof(string))
                ? BaseUtil.ConvertToType<string>(ddListID)
                : BaseUtil.ConvertToType<Enum>(ddListID).GetString();

            string _ddFieldXpath = _ddListID.Contains("Time")
                ? $"//span[@aria-controls='{_ddListID}_timeview']"
                : $"//span[@aria-owns='{_ddListID}_listbox']";

            return _ddFieldXpath;
        }

        private string SetDDListFieldExpandArrowXpath<T>(T ddListID, bool isMultiSelectDDList = false)
        {
            string _ddListID = (ddListID.GetType() == typeof(string))
                ? BaseUtil.ConvertToType<string>(ddListID)
                : BaseUtil.ConvertToType<Enum>(ddListID).GetString();

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
            string _ddListID = ddListID.GetType() == typeof(string)
                ? BaseUtil.ConvertToType<string>(ddListID)
                : BaseUtil.ConvertToType<Enum>(ddListID).GetString();

            string ddListXPath = _ddListID.Contains("Time")
                ? $"//ul[@id='{_ddListID}_timeview']"
                : $"//div[@id='{_ddListID}-list']";

            string itemValueXPath = string.Empty;

            if (itemIndexOrName.GetType().Equals(typeof(string)))
            {
                var argName = BaseUtil.ConvertToType<string>(itemIndexOrName);
                itemValueXPath = useContains
                    ? $"contains(text(),'{argName}')"
                    : $"text()='{argName}'";
            }
            else if (itemIndexOrName.GetType().Equals(typeof(int)))
            {
                int itemIndex = BaseUtil.ConvertToType<int>(itemIndexOrName);
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


        public override By GetSubmitButtonByLocator(Enum buttonValue, bool submitType = true)
        {
            string submitTypeXPath = submitType 
                ? "[@type='submit']"
                : "";
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

        public override By GetDDListCurrentSelectionByLocator(Enum ddListID)
            => By.XPath(SetDDListCurrentSelectionXpath(ddListID));

        public override By GetDDListCurrentSelectionInActiveTabByLocator(Enum ddListID)
            => By.XPath($"{ActiveContentXPath}{SetDDListCurrentSelectionXpath(ddListID)}");

        public override By GetMultiSelectDDListCurrentSelectionByLocator(Enum multiSelectDDListID)
            => By.XPath(SetMultiSelectDDListCurrentSelectionXpath(multiSelectDDListID));

        public override By GetExpandDDListButtonByLocator<T>(T ddListID, bool isMultiSelectDDList = false)
            => isMultiSelectDDList
                ? By.XPath($"//select[@id='{BaseUtil.ConvertToType<Enum>(ddListID).GetString()}']/parent::div")
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
        public override By GetDDListItemsByLocator<T, I>(T ddListID, I itemIndexOrName, bool useContains = false)
            => By.XPath(SetDDListItemsXpath(ddListID, itemIndexOrName, useContains));

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
    }

    public abstract class PageHelper_Impl : PageInteraction, IPageHelper
    {
        public abstract By GetButtonByLocator(string buttonName);
        public abstract By GetDDListByLocator(Enum ddListID);
        public abstract By GetDDListCurrentSelectionByLocator(Enum ddListID);
        public abstract By GetDDListCurrentSelectionInActiveTabByLocator(Enum ddListID);
        public abstract By GetDDListItemsByLocator<T, I>(T ddListID, I itemIndexOrName, bool useContains = false);
        public abstract By GetExpandDDListButtonByLocator<T>(T ddListID, bool isMultiSelectDDList = false);
        public abstract By GetInputButtonByLocator<T>(T buttonName);
        public abstract By GetInputFieldByLocator<T>(T inputFieldLabelOrID);
        public abstract By GetMainNavMenuByLocator(Enum navEnum);
        public abstract By GetMultiSelectDDListCurrentSelectionByLocator(Enum multiSelectDDListID);
        public abstract By GetNavMenuByLocator(Enum navEnum, Enum parentNavEnum = null);
        public abstract By GetSubmitButtonByLocator(Enum buttonValue, bool submitType = true);
        public abstract By GetTextAreaFieldByLocator(Enum textAreaEnum);
        public abstract By GetTextInputFieldByLocator(Enum inputEnum);
    }
}