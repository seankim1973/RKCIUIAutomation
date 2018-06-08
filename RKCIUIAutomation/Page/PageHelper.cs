using OpenQA.Selenium;
using System;
using System.Reflection;

namespace RKCIUIAutomation.Page
{
    public static class PageHelper
    {
        public static Enum ConvertToEnumType<T>(T navEnum) => (Enum)Convert.ChangeType(navEnum, typeof(Enum));

        private static string SetDDListFieldXpath(Enum ddListID) => $"//span[@aria-owns='{ddListID.GetString()}_listbox']";
        private static string SetDDListFieldExpandArrowXpath(Enum ddListID) => $"{SetDDListFieldXpath(ddListID)}//span[@class='k-select']/span";
        private static string SetMainNavMenuXpath(Enum navEnum) => $"//li[@class='dropdown']/a[text()='{navEnum.GetString()}']";
        private static string SetNavMenuXpath(Enum navEnum) => $"//ul[@class='dropdown-menu']/li/a[text()='{navEnum.GetString()}']";
        private static string SetInputFieldXpath(string inputFieldLabel) => $"//label[contains(text(),'{inputFieldLabel}')]/following::input[1]";
        private static string SetDDListItemsXpath<T>(Enum ddListID, T itemIndexOrName)
        {
            Type itemType = itemIndexOrName.GetType();

            string locatorXpath = string.Empty;
            string inputValue = (string)Convert.ChangeType(itemIndexOrName, typeof(string));

            if (itemType.Equals(typeof(string)))
            {
                locatorXpath = $"text()={inputValue}";
            }
            else if (itemType.Equals(typeof(int)))
            {
                locatorXpath = inputValue;
            }
            return $"//div[@id='{ddListID.GetString()}-list']//li[{locatorXpath}]";
        }

        private static string SetDDListItemsXpath(Enum ddListID, int itemIndex) => $"//div[@id='{ddListID.GetString()}-list']//li[{itemIndex}]";
        private static string SetTableTabXpath(Enum tableTab) => $"//ul[@class='k-reset k-tabstrip-items']//span[text()='{tableTab.GetString()}']";
        private static string SetTableNavPageXpath(int pageNumber) => $"//div[@id='TestGrid_New']//div[@data-role='pager']/ul/li/a[text()='{pageNumber.ToString()}']";
        private static string SetTextInputFieldByLocator(Enum inputEnum) => $"//input[@id='{inputEnum.GetString()}']";
        public static By GetMainNavMenuByLocator(Enum navEnum) => By.XPath(SetMainNavMenuXpath(navEnum));
        public static By GetNavMenuByLocator(Enum navEnum) => By.XPath(SetNavMenuXpath(navEnum));
        public static By GetInputFieldByLocator(string inputFieldLabel) => By.XPath(SetInputFieldXpath(inputFieldLabel));
        public static By GetDDListByLocator(Enum ddListID) => By.XPath(SetDDListFieldXpath(ddListID));
        public static By GetExpandDDListButtonByLocator(Enum ddListID) => By.XPath(SetDDListFieldExpandArrowXpath(ddListID));
        public static By GetDDListItemsByLocator<T>(Enum ddListID, T itemIndexOrName) => By.XPath(SetDDListItemsXpath(ddListID, itemIndexOrName));
        public static By GetTableNavByLocator(int pageNumber) => By.XPath(SetTableNavPageXpath(pageNumber));
        public static By GetTableTabByLocator(Enum tableTab) => By.XPath(SetTableTabXpath(tableTab));
        public static By GetTextInputFieldByLocator(Enum inputEnum) => By.XPath(SetTextInputFieldByLocator(inputEnum));

        public static string GetString(this Enum value)
        {
            string output = null;

            try
            {
                Type type = value.GetType();
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute), false) as StringValueAttribute[];
                output = attrs[0].Value;
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
        public StringValueAttribute(string value) => Value = value;
        public string Value { get; }
    }

}