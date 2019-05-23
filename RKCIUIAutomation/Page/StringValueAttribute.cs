using System;

namespace RKCIUIAutomation.Page
{
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