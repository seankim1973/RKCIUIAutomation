using NUnit.Framework;
using RKCIUIAutomation.Base;
using System.Threading;

namespace RKCIUIAutomation
{
    //[TestFixture]
    [Category("TestFixture Category")]
    public class UnitTest1 : BaseClass
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
            Thread.Sleep(5000);
            Assert.True(1==1);
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
