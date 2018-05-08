using NUnit.Framework;
using RKCIUIAutomation.Base;
using System.Threading;

namespace RKCIUIAutomation
{
    [TestFixture]
    [Category("TestFixture Category")]
    public class UnitTest1 : BaseClass
    {
        [Parallelizable]
        [Test]
        [Category("Test Category")]
        public void TestMethod1()
        {
            //driver.Navigate().GoToUrl("http://www.google.com");
            Thread.Sleep(5000);
            Assert.True(true);
        }

        [Parallelizable]
        [Test(Description = "test description - inline")]
        public void testmethod2()
        {
            Thread.Sleep(5000);
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
