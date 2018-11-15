﻿using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using System;
using System.Reflection;
using static RKCIUIAutomation.Page.TableHelper;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : BaseClass
    {
        public PageHelper()
        {
        }

        public PageHelper(IWebDriver driver) => this.Driver = driver;

        public static PageBaseHelper PgBaseHelper => new PageBaseHelper();

        public static string GetMaxShortDate() => DateTime.MaxValue.Date.ToShortDateString();

        public static string GetShortDate() => DateTime.Now.ToShortDateString();

        public static string GetShortTime() => DateTime.Now.ToShortTimeString();

        public static string GetShortDateTime() => $"{GetShortDate()} {GetShortTime()}";

        public OutType ConvertToType<OutType>(object objToConvert)
        {
            try
            {
                Type inputType = objToConvert.GetType();
                return (OutType)Convert.ChangeType(objToConvert, typeof(OutType));
            }
            catch (Exception e)
            {
                log.Error($"Error occured in ConvertToType method:\n{e.Message}");
                throw;
            }
        }

        private string SetDDListFieldXpath<T>(T ddListID)
        {
            string _ddListID = (ddListID.GetType() == typeof(string))
                ? ConvertToType<string>(ddListID)
                : ConvertToType<Enum>(ddListID).GetString();

            string _ddFieldXpath = _ddListID.Contains("Time")
                ? $"//span[@aria-controls='{_ddListID}_timeview']"
                : $"//span[@aria-owns='{_ddListID}_listbox']";

            return _ddFieldXpath;
        }

        //private string SetDDListFieldXpath(Enum ddListID) => $"//span[@aria-owns='{ddListID.GetString()}_listbox']";

        //private string SetDDListFieldXpath(string ddListID) => $"//span[@aria-owns='{ddListID}_listbox']";

        private string SetDDListFieldExpandArrowXpath<T>(T ddListID)
        {
            string _ddListID = (ddListID.GetType() == typeof(string))
                ? ConvertToType<string>(ddListID)
                : ConvertToType<Enum>(ddListID).GetString();

            string _ddArrowXpath = _ddListID.Contains("Time")
                ? $"{SetDDListFieldXpath(_ddListID)}/parent::span/span/span"
                : $"{SetDDListFieldXpath(_ddListID)}//span[@class='k-select']/span";

            return _ddArrowXpath;
        }

        //private string SetDDListFieldExpandArrowXpath(Enum ddListID) => $"{SetDDListFieldXpath(ddListID)}//span[@class='k-select']/span";

        //private string SetDDListFieldExpandArrowXpath(string ddListID) => $"{SetDDListFieldXpath(ddListID)}//span[@class='k-select']/span";

        private string SetDDListCurrentSelectionXpath(Enum ddListID) => $"{SetDDListFieldXpath(ddListID)}//span[@class='k-input']";

        private string SetMainNavMenuXpath(Enum navEnum) => $"//li[@class='dropdown']/a[contains(text(),'{navEnum.GetString()}')]";

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

        private string SetDDListItemsXpath<T, I>(T ddListID, I itemIndexOrName)
        {
            string _ddListID = ddListID.GetType() == typeof(string) 
                ? ConvertToType<string>(ddListID) 
                : ConvertToType<Enum>(ddListID).GetString();

            string ddListXPath = _ddListID.Contains("Time")
                ? $"//ul[@id='{_ddListID}_timeview']"
                : $"//div[@id='{_ddListID}-list']";

            string itemValueXPath = string.Empty;

            if (itemIndexOrName.GetType().Equals(typeof(string)))
            {
                itemValueXPath = $"text()='{ConvertToType<string>(itemIndexOrName)}'";
            }
            else if (itemIndexOrName.GetType().Equals(typeof(int)))
            {
                int itemIndex = ConvertToType<int>(itemIndexOrName);
                itemIndex = _ddListID.Contains("Time") ? itemIndex + 1 : itemIndex;
                itemValueXPath = itemIndex.ToString();
            }

            return $"{ddListXPath}//li[{itemValueXPath}]";
        }

        private string SetDDListItemsXpath(Enum ddListID, int itemIndex) => $"//div[@id='{ddListID.GetString()}-list']//li[{itemIndex}]";

        private string SetTextInputFieldByLocator(Enum inputEnum) => $"//input[@id='{inputEnum.GetString()}']";

        private string SetTextAreaFieldByLocator(Enum textAreaEnum) => $"//textarea[@id='{textAreaEnum.GetString()}']";

        private string SetButtonXpath(string buttonName) => $"//a[text()='{buttonName}']";

        private string SetInputButtonXpath(string buttonName) => $"//input[@value='{buttonName}']";

        public By GetSubmitButtonByLocator(Enum buttonValue) => By.XPath($"//input[@value='{buttonValue.GetString()}']");

        public By GetMainNavMenuByLocator(Enum navEnum) => By.XPath(SetMainNavMenuXpath(navEnum));

        public By GetNavMenuByLocator(Enum navEnum, Enum parentNavEnum = null) => By.XPath(SetNavMenuXpath(navEnum, parentNavEnum));

        public By GetInputFieldByLocator(string inputFieldLabel) => By.XPath(SetInputFieldXpath(inputFieldLabel));

        public By GetDDListByLocator(Enum ddListID) => By.XPath(SetDDListFieldXpath(ddListID));

        public By GetDDListCurrentSelectionByLocator(Enum ddListID) => By.XPath(SetDDListCurrentSelectionXpath(ddListID));

        public By GetExpandDDListButtonByLocator<T>(T ddListID) => By.XPath(SetDDListFieldExpandArrowXpath(ddListID));

        //public By GetExpandDDListButtonByLocator(Enum ddListID) => By.XPath(SetDDListFieldExpandArrowXpath(ddListID));

        //public By GetExpandDDListButtonByLocator(string ddListID) => By.XPath(SetDDListFieldExpandArrowXpath(ddListID));

        public By GetDDListItemsByLocator<T, I>(T ddListID, I itemIndexOrName) => By.XPath(SetDDListItemsXpath(ddListID, itemIndexOrName));

        //public By GetDDListItemsByLocator<T>(Enum ddListID, T itemIndexOrName) => By.XPath(SetDDListItemsXpath(ddListID, itemIndexOrName));

        //public By GetDDListItemsByLocator<T>(string ddListID, T itemIndexOrName) => By.XPath(SetDDListItemsXpath(ddListID, itemIndexOrName));

        public By GetTextInputFieldByLocator(Enum inputEnum) => By.XPath(SetTextInputFieldByLocator(inputEnum));

        public By GetTextAreaFieldByLocator(Enum textAreaEnum) => By.XPath(SetTextAreaFieldByLocator(textAreaEnum));

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