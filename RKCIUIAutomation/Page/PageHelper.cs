using OpenQA.Selenium;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using System;
using System.Reflection;
using System.Text.RegularExpressions;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : BaseClass
    {
        public PageHelper()
        {
        }

        public PageHelper(IWebDriver driver) => this.Driver = driver;

        public static PageBaseHelper PgBaseHelper
            => new PageBaseHelper();

        public static string GetMaxShortDate()
            => DateTime.MaxValue.Date.ToShortDateString();

        public static string GetShortDate(string shortDate = "", bool formatWithZero = false)
        {
            string date = shortDate.HasValue()
                ? shortDate
                : DateTime.Now.ToShortDateString();

            if (formatWithZero)
            {
                string[] splitShortDate = Regex.Split(date, "/");
                string mm = splitShortDate[0];
                string dd = splitShortDate[1];
                string yyyy = splitShortDate[2];

                mm = mm.Length == 1 ? $"0{mm}" : mm;
                dd = dd.Length == 1 ? $"0{dd}" : dd;
                date = $"{mm}/{dd}/{yyyy}";
            }

            return date;
        }

        public static string GetFutureShortDate()
        {
            string currentDate = DateTime.Now.ToShortDateString();
            string[] splitShortDate = Regex.Split(currentDate, "/");
            int mm = int.Parse(splitShortDate[0]);
            int dd = int.Parse(splitShortDate[1]);
            int yyyy = int.Parse(splitShortDate[2]);

            yyyy = dd > 27 && mm == 12
                ? yyyy + 1
                : yyyy;

            mm = dd > 27
                ? mm == 12
                    ? 1
                    : mm + 1
                : mm;

            dd = dd > 27
                ? 1
                : dd + 1;

            return $"{mm}/{dd}/{yyyy}";
        }

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
        {
            if (parentNavEnum == null)
            {
                return $"//ul[@class='dropdown-menu']/li/a[contains(text(),'{navEnum.GetString()}')]";
            }
            else
            {
                return $"//a[contains(text(),'{parentNavEnum.GetString()}')]/following-sibling::ul[@class='dropdown-menu']/li/a[contains(text(),'{navEnum.GetString()}')]";
            }
        }

        private string SetInputFieldXpath(string inputFieldLabel) => $"//label[contains(text(),'{inputFieldLabel}')]/following::input[1]";

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
            BaseUtils baseUtils = new BaseUtils();

            string _ddListID = ddListID.GetType() == typeof(string)
                ? baseUtils.ConvertToType<string>(ddListID)
                : baseUtils.ConvertToType<Enum>(ddListID).GetString();

            string ddListXPath = _ddListID.Contains("Time")
                ? $"//ul[@id='{_ddListID}_timeview']"
                : $"//div[@id='{_ddListID}-list']";

            string itemValueXPath = string.Empty;

            if (itemIndexOrName.GetType().Equals(typeof(string)))
            {
                var argName = baseUtils.ConvertToType<string>(itemIndexOrName);
                itemValueXPath = useContains
                    ? $"contains(text(),'{argName}')"
                    : $"text()='{argName}'";
            }
            else if (itemIndexOrName.GetType().Equals(typeof(int)))
            {
                int itemIndex = baseUtils.ConvertToType<int>(itemIndexOrName);
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

        public By GetInputFieldByLocator(string inputFieldLabel)
            => By.XPath(SetInputFieldXpath(inputFieldLabel));

        public By GetDDListByLocator(Enum ddListID)
            => By.XPath(SetDDListFieldXpath(ddListID));

        public By GetDDListCurrentSelectionByLocator(Enum ddListID)
            => By.XPath(SetDDListCurrentSelectionXpath(ddListID));

        public By GetMultiSelectDDListCurrentSelectionByLocator(Enum multiSelectDDListID)
            => By.XPath(SetMultiSelectDDListCurrentSelectionXpath(multiSelectDDListID));

        public By GetExpandDDListButtonByLocator<T>(T ddListID, bool isMultiSelectDDList = false)
            => isMultiSelectDDList
                ? By.XPath($"//select[@id='{ConvertToType<Enum>(ddListID).GetString()}']/parent::div")
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
                buttonVal = ConvertToType<string>(buttonName);
            }
            else if (argType == typeof(Enum))
            {
                buttonVal = ConvertToType<Enum>(buttonName).GetString();
            }

            return By.XPath(SetInputButtonXpath(buttonVal));
        }
    }

    public static class EnumHelper
    {
        public static string GetString(this Enum value, bool getValue2 = false)
        {
            string output = null;

            try
            {
                Type type = value.GetType();
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                output = (getValue2) ? attrs[0].Value2 : attrs[0].Value;
            }
            catch (Exception)
            {
                throw;
            }
            return output;
        }
    }

    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value, string value2 = "")
        {
            Value = value;
            Value2 = value2;
        }

        public string Value { get; }
        public string Value2 { get; }
    }
}