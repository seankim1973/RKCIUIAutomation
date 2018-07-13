using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using System;
using System.Reflection;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : BaseClass
    {
        public Enum ConvertToEnumType<T>(T navEnum) => (Enum)Convert.ChangeType(navEnum, typeof(Enum));

        private string SetDDListFieldXpath(Enum ddListID) => $"//span[@aria-owns='{ddListID.GetString()}_listbox']";
        private string SetDDListFieldExpandArrowXpath(Enum ddListID) => $"{SetDDListFieldXpath(ddListID)}//span[@class='k-select']/span";
        private string SetMainNavMenuXpath(Enum navEnum) => $"//li[@class='dropdown']/a[text()='{navEnum.GetString()}']";
        private string SetNavMenuXpath(Enum navEnum, Enum parentNavEnum = null)
        {
            if (parentNavEnum == null)
            {
                return $"//ul[@class='dropdown-menu']/li/a[text()='{navEnum.GetString()}']";
            }
            else
            {
                return $"//a[text()='{parentNavEnum.GetString()}']/following-sibling::ul[@class='dropdown-menu']/li/a[text()='{navEnum.GetString()}']";
            }          
        }
        private string SetInputFieldXpath(string inputFieldLabel) => $"//label[contains(text(),'{inputFieldLabel}')]/following::input[1]";
        private string SetDDListItemsXpath<T>(Enum ddListID, T itemIndexOrName)
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
        private string SetDDListItemsXpath(Enum ddListID, int itemIndex) => $"//div[@id='{ddListID.GetString()}-list']//li[{itemIndex}]";
        private string SetTextInputFieldByLocator(Enum inputEnum) => $"//input[@id='{inputEnum.GetString()}']";
        private string SetButtonXpath(string buttonName) => $"//a[text()='{buttonName}']";
        private string SetInputButtonXpath(string buttonName) => $"//input[@value='{buttonName}']";


        public By GetMainNavMenuByLocator(Enum navEnum) => By.XPath(SetMainNavMenuXpath(navEnum));
        public By GetNavMenuByLocator(Enum navEnum, Enum parentNavEnum = null) => By.XPath(SetNavMenuXpath(navEnum, parentNavEnum));
        public By GetInputFieldByLocator(string inputFieldLabel) => By.XPath(SetInputFieldXpath(inputFieldLabel));
        public By GetDDListByLocator(Enum ddListID) => By.XPath(SetDDListFieldXpath(ddListID));
        public By GetExpandDDListButtonByLocator(Enum ddListID) => By.XPath(SetDDListFieldExpandArrowXpath(ddListID));
        public By GetDDListItemsByLocator<T>(Enum ddListID, T itemIndexOrName) => By.XPath(SetDDListItemsXpath(ddListID, itemIndexOrName));
        public By GetTextInputFieldByLocator(Enum inputEnum) => By.XPath(SetTextInputFieldByLocator(inputEnum));
        public By GetButtonByLocator(string buttonName) => By.XPath(SetButtonXpath(buttonName));
        public By GetInputButtonByLocator(string buttonName) => By.XPath(SetInputButtonXpath(buttonName));



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