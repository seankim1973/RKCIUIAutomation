using OpenQA.Selenium;
using System;
using System.Reflection;

namespace RKCIUIAutomation.Page
{
    public static class PageHelper
    {
        public static Enum ConvertToEnumType<T>(T navEnum) => (Enum)Convert.ChangeType(navEnum, typeof(Enum));
        private static string SetDDListFieldXpath(Enum ddListID) => $"//span[@aria-owns='{ddListID.GetString()}_listbox']";
        
        //private static string SetDDListFieldExpandArrowXpath(Enum ddListID) => $"{SetDDListFieldXpath(ddListID)}//span[@class='k-select']";     
        //private static string SetNavMenuXpath(Enum navEnum) => $"//li/a[contains(text(),'{navEnum.GetString()}')]";
        //private static string SetInputFieldXpath(string inputFieldLabel) => $"//label[contains(text(),'{inputFieldLabel}')]/following::input[1]";
        //private static string SetDDListItemsXpath(Enum ddListID, string itemName) => $"//div[@id='{ddListID.GetString()}-list']//li[text()={itemName}]";
        //private static string SetDDListItemsXpath(Enum ddListID, int itemIndex) => $"//div[@id='{ddListID.GetString()}-list']//li[{itemIndex}]";
        //private static string SetTableTabXpath(Enum tableTab) => $"//ul[@class='k-reset k-tabstrip-items']//span[text()='{tableTab.GetString()}']";
        //private static string SetTableNavPageXpath(int pageNumber) => $"//div[@id='TestGrid_New']//div[@data-role='pager']/ul/li/a[text()='{pageNumber.ToString()}']";

        public static By GetNavMenuByLocator(Enum navEnum) => By.XPath($"//li/a[contains(text(),'{navEnum.GetString()}')]");
        public static By GetInputFieldByLocator(string inputFieldLabel) => By.XPath($"//label[contains(text(),'{inputFieldLabel}')]/following::input[1]");
        public static By GetDDListByLocator(Enum ddListID) => By.XPath(SetDDListFieldXpath(ddListID));
        public static By GetExpandDDListButtonByLocator(Enum ddListID) => By.XPath($"{SetDDListFieldXpath(ddListID)}//span[@class='k-select']");
        public static By GetDDListItemsByLocator(Enum ddListID, int itemIndex) => By.XPath($"//div[@id='{ddListID.GetString()}-list']//li[{itemIndex}]");
        public static By GetDDListItemsByLocator(Enum ddListID, string itemName) => By.XPath($"//div[@id='{ddListID.GetString()}-list']//li[text()={itemName}]");
        public static By GetTableNavByLocator(int pageNumber) => By.XPath($"//div[@id='TestGrid_New']//div[@data-role='pager']/ul/li/a[text()='{pageNumber.ToString()}']");
        public static By GetTableTabByLocator(Enum tableTab) => By.XPath($"//ul[@class='k-reset k-tabstrip-items']//span[text()='{tableTab.GetString()}']");

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