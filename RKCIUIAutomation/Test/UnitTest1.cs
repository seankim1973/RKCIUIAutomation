using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace RKCIUIAutomation
{
    //[TestFixture]
    [Category("TestFixture Category")]
    public class UnitTest1 : TestBase
    {
        //[Test]
        [Category("Test Category")]
        public void TestMethod1()
        {
            //driver.Navigate().GoToUrl("http://www.google.com");
            Thread.Sleep(5000);
            Assert.True(true);
        }



        //[Test(Description = "test description - inline")]
        public void Testmethod2()
        {

        }

        //[Test, Description("test description - comma seperated")]
        //public void testmethod3()
        //{
        //}

        //[Testcase(Testname = "test0004")]
        //public void testmethod4()
        //{
        //}
    }    
}
