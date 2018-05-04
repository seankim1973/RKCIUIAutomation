#if MSTEST
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Category = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
#else
using NUnit.Framework;
using TestInitialize = NUnit.Framework.SetUpAttribute;
using TestContext = System.Object;
using TestProperty = NUnit.Framework.PropertyAttribute;
using TestClass = NUnit.Framework.TestFixtureAttribute;
using TestMethod = NUnit.Framework.TestAttribute;
using TestCleanup = NUnit.Framework.TearDownAttribute;
#endif

using RKCIUIAutomation.Base;
using System.Threading;

namespace RKCIUIAutomation
{
    [TestFixture]
    [Parallelizable]
    [Category("TestFixture Category")]
    public class UnitTest1 : BaseClass
    {
        [Test]
        [Category("Test Category")]
        public void TestMethod1()
        {
            //driver.Navigate().GoToUrl("http://www.google.com");
            Thread.Sleep(5000);
            Assert.True(true);
        }

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
