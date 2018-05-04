using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Test
{
    /// <summary>
    /// Summary description for JUnitTest
    /// </summary>
    [TestClass]
    public class MSUnitTest : BaseClass
    {
        public MSUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void MSUnitTest1()
        {
            //Directory.SetCurrentDirectory(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString());
            //Console.Out.WriteLine("#######" + Directory.GetParent(Directory.GetCurrentDirectory().ToString()));

            //var proj = GetSiteUrl(TestEnv.Prod, Project.SMF202);
            //var user = GetUser(UserType.SysAdmin);
            //Console.Out.WriteLine(proj);
            //Console.Out.WriteLine(user);
            //Console.Out.WriteLine();

            //var collection = ConfigurationManager.GetSection($"TestConfigs/UserType") as NameValueCollection;
            //var userPw = collection["SysAdminUsername"].Split(',');

            string[] userPw = GetUser(UserType.ProjAdmin);
            string username = userPw[0];
            string password = userPw[1];
            Console.Out.WriteLine($"Username : {username} and Password : {password}");

        }
    }
}
