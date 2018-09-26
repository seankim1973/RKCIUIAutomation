using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace RKCIUIAutomation.Tools
{
    public class TestResultUtil
    {
        private string xmlFile = $"TestResult.xml";

        public void GetValue()
        {
            XmlDocument document = new XmlDocument();
            document.Load(xmlFile);

            XmlNode node = document.SelectSingleNode("//setting/name");
            Console.WriteLine(node.Value);
        }

        [XmlRoot(ElementName = "filter")]
        public class Filter
        {
            [XmlElement(ElementName = "test")]
            public string Test { get; set; }
        }

        [XmlRoot(ElementName = "environment")]
        public class Environment
        {
            [XmlAttribute(AttributeName = "framework-version")]
            public string Frameworkversion { get; set; }

            [XmlAttribute(AttributeName = "clr-version")]
            public string Clrversion { get; set; }

            [XmlAttribute(AttributeName = "os-version")]
            public string Osversion { get; set; }

            [XmlAttribute(AttributeName = "platform")]
            public string Platform { get; set; }

            [XmlAttribute(AttributeName = "cwd")]
            public string Cwd { get; set; }

            [XmlAttribute(AttributeName = "machine-name")]
            public string Machinename { get; set; }

            [XmlAttribute(AttributeName = "user")]
            public string User { get; set; }

            [XmlAttribute(AttributeName = "user-domain")]
            public string Userdomain { get; set; }

            [XmlAttribute(AttributeName = "culture")]
            public string Culture { get; set; }

            [XmlAttribute(AttributeName = "uiculture")]
            public string Uiculture { get; set; }

            [XmlAttribute(AttributeName = "os-architecture")]
            public string Osarchitecture { get; set; }
        }

        [XmlRoot(ElementName = "setting")]
        public class Setting
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }

            [XmlElement(ElementName = "item")]
            public List<Item> Item { get; set; }
        }

        [XmlRoot(ElementName = "item")]
        public class Item
        {
            [XmlAttribute(AttributeName = "key")]
            public string Key { get; set; }

            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
        }

        [XmlRoot(ElementName = "settings")]
        public class Settings
        {
            [XmlElement(ElementName = "setting")]
            public List<Setting> Setting { get; set; }
        }

        [XmlRoot(ElementName = "property")]
        public class Property
        {
            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "value")]
            public string Value { get; set; }
        }

        [XmlRoot(ElementName = "properties")]
        public class Properties
        {
            [XmlElement(ElementName = "property")]
            public List<Property> Property { get; set; }
        }

        [XmlRoot(ElementName = "test-case")]
        public class Testcase
        {
            [XmlElement(ElementName = "properties")]
            public Properties Properties { get; set; }

            [XmlElement(ElementName = "output")]
            public string Output { get; set; }

            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "fullname")]
            public string Fullname { get; set; }

            [XmlAttribute(AttributeName = "methodname")]
            public string Methodname { get; set; }

            [XmlAttribute(AttributeName = "classname")]
            public string Classname { get; set; }

            [XmlAttribute(AttributeName = "runstate")]
            public string Runstate { get; set; }

            [XmlAttribute(AttributeName = "seed")]
            public string Seed { get; set; }

            [XmlAttribute(AttributeName = "result")]
            public string Result { get; set; }

            [XmlAttribute(AttributeName = "start-time")]
            public string Starttime { get; set; }

            [XmlAttribute(AttributeName = "end-time")]
            public string Endtime { get; set; }

            [XmlAttribute(AttributeName = "duration")]
            public string Duration { get; set; }

            [XmlAttribute(AttributeName = "asserts")]
            public string Asserts { get; set; }
        }

        [XmlRoot(ElementName = "test-suite")]
        public class Testsuite
        {
            [XmlElement(ElementName = "output")]
            public string Output { get; set; }

            [XmlElement(ElementName = "test-case")]
            public Testcase Testcase { get; set; }

            [XmlAttribute(AttributeName = "type")]
            public string Type { get; set; }

            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }

            [XmlAttribute(AttributeName = "name")]
            public string Name { get; set; }

            [XmlAttribute(AttributeName = "fullname")]
            public string Fullname { get; set; }

            [XmlAttribute(AttributeName = "classname")]
            public string Classname { get; set; }

            [XmlAttribute(AttributeName = "runstate")]
            public string Runstate { get; set; }

            [XmlAttribute(AttributeName = "testcasecount")]
            public string Testcasecount { get; set; }

            [XmlAttribute(AttributeName = "result")]
            public string Result { get; set; }

            [XmlAttribute(AttributeName = "start-time")]
            public string Starttime { get; set; }

            [XmlAttribute(AttributeName = "end-time")]
            public string Endtime { get; set; }

            [XmlAttribute(AttributeName = "duration")]
            public string Duration { get; set; }

            [XmlAttribute(AttributeName = "total")]
            public string Total { get; set; }

            [XmlAttribute(AttributeName = "passed")]
            public string Passed { get; set; }

            [XmlAttribute(AttributeName = "failed")]
            public string Failed { get; set; }

            [XmlAttribute(AttributeName = "warnings")]
            public string Warnings { get; set; }

            [XmlAttribute(AttributeName = "inconclusive")]
            public string Inconclusive { get; set; }

            [XmlAttribute(AttributeName = "skipped")]
            public string Skipped { get; set; }

            [XmlAttribute(AttributeName = "asserts")]
            public string Asserts { get; set; }
        }

        [XmlRoot(ElementName = "test-run")]
        public class Testrun
        {
            [XmlElement(ElementName = "command-line")]
            public string Commandline { get; set; }

            [XmlElement(ElementName = "filter")]
            public Filter Filter { get; set; }

            [XmlElement(ElementName = "test-suite")]
            public Testsuite Testsuite { get; set; }

            [XmlAttribute(AttributeName = "id")]
            public string Id { get; set; }

            [XmlAttribute(AttributeName = "testcasecount")]
            public string Testcasecount { get; set; }

            [XmlAttribute(AttributeName = "result")]
            public string Result { get; set; }

            [XmlAttribute(AttributeName = "total")]
            public string Total { get; set; }

            [XmlAttribute(AttributeName = "passed")]
            public string Passed { get; set; }

            [XmlAttribute(AttributeName = "failed")]
            public string Failed { get; set; }

            [XmlAttribute(AttributeName = "inconclusive")]
            public string Inconclusive { get; set; }

            [XmlAttribute(AttributeName = "skipped")]
            public string Skipped { get; set; }

            [XmlAttribute(AttributeName = "asserts")]
            public string Asserts { get; set; }

            [XmlAttribute(AttributeName = "engine-version")]
            public string Engineversion { get; set; }

            [XmlAttribute(AttributeName = "clr-version")]
            public string Clrversion { get; set; }

            [XmlAttribute(AttributeName = "start-time")]
            public string Starttime { get; set; }

            [XmlAttribute(AttributeName = "end-time")]
            public string Endtime { get; set; }

            [XmlAttribute(AttributeName = "duration")]
            public string Duration { get; set; }
        }
    }
}