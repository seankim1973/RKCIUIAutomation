using OpenQA.Selenium;
using RKCIUIAutomation.Page.Navigation;
using System;
using System.Reflection;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : Action
    {
        private static string SetNavMenuXpath(Enum navEnum) => $"//li/a[contains(text(),'{navEnum.GetString()}')]";
        private static string SetInputFieldXpath(string inputFieldLabel) => $"//label[contains(text(),'{inputFieldLabel}')]/following::input[1]";
        private static string SetDDListFieldXpath(Enum ddListID) => $"//span[@aria-owns='{ddListID.GetString()}_listbox']";
        private static string SetDDListFieldExpandArrowXpath(Enum ddListID) => $"{SetDDListFieldXpath(ddListID)}//span[@class='k-select']";
        private static string SetDDListItemsXpath(Enum ddListID, string itemName) => $"//div[@id='{ddListID.GetString()}-list']//li[text()={itemName}]";
        private static string SetDDListItemsXpath(Enum ddListID, int itemIndex) => $"//div[@id='{ddListID.GetString()}-list']//li[{itemIndex}]";
        public static Enum ConvertToEnumType<T>(T navEnum) => (Enum)Convert.ChangeType(navEnum, typeof(Enum));
         
        public static By GetNavMenuByLocator(Enum navEnum) => By.XPath(SetNavMenuXpath(navEnum));
        public static By GetInputFieldByLocator(string inputFieldLabel) => By.XPath(SetInputFieldXpath(inputFieldLabel));
        public static By GetDDListByLocator(Enum ddListID) => By.XPath(SetDDListFieldXpath(ddListID));
        public static By GetExpandDDListButtonByLocator(Enum ddListID) => By.XPath(SetDDListFieldExpandArrowXpath(ddListID));
        public static By GetDDListItemsByLocator(Enum ddListID, int itemIndex) => By.XPath(SetDDListItemsXpath(ddListID, itemIndex));
        public static By GetDDListItemsByLocator(Enum ddListID, string itemName) => By.XPath(SetDDListItemsXpath(ddListID, itemName));
    }

    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value) => Value = value;
        public string Value { get; }
    }

    public static class BaseHelper
    {
        public static string GetString(this Enum value)
        {
            string output = null;

            try
            {
                Type type = value.GetType();
                FieldInfo fi = type.GetField(value.ToString());
                StringValueAttribute[] attrs = fi.GetCustomAttributes(typeof(StringValueAttribute),false) as StringValueAttribute[];
                output = attrs[0].Value;
            }
            catch (Exception)
            {
                throw;
            }
            return output;
        }
    }
}