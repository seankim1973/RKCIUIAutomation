using NUnit.Framework;
using RKCIUIAutomation.Base;
using System;
using System.Reflection;

namespace RKCIUIAutomation
{
    [TestFixture]
    [Category("TestFixture Category")]
    public class UnitTest1 : BaseClass
    {
        [Test]
        [Category("Test Category")]
        public void TestMethod1()
        {
            //driver.Navigate().GoToUrl("http://www.google.com");
            Assert.True(true);
        }

        [Test(Description = "test description - inline")]
        public void testmethod2()
        {
            Assert.True(false);
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
