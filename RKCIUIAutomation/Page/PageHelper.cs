using OpenQA.Selenium;
using System;
using System.Reflection;

namespace RKCIUIAutomation.Page
{
    public class PageHelper : PageBase
    {
        public static string GetATagXpathString(Enum @enum) => $"//a[text()='{@enum.GetString()}']";
        public static By SetLocatorXpath(Enum @enum) => By.XPath(GetATagXpathString(@enum));
    }


    public class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value)
        {
            Value = value;
        }

        public string Value { get; }
    }

    public static class EnumHelper
    {
        public static string GetString(this Enum value)
        {
            string output = null;
            Type type = value.GetType();

            FieldInfo fi = type.GetField(value.ToString());
            StringValueAttribute[] attrs = fi.GetCustomAttributes(false) as StringValueAttribute[];
            output = attrs[0].Value;
            return output;
        }
    }
}
