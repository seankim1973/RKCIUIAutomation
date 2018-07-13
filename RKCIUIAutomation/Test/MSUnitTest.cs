using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Tools;
using static RKCIUIAutomation.Config.ProjectProperties;
using static RKCIUIAutomation.Page.Navigation.NavMenu;
using static RKCIUIAutomation.Base.BaseUtils;
using RKCIUIAutomation.Page;
using MiniGuids;
using System.Text.RegularExpressions;

namespace RKCIUIAutomation.Test
{
    /// <summary>
    /// Summary description for JUnitTest
    /// </summary>
    [TestClass]
    public class MSUnitTest : TestBase
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

        //[TestMethod]
        public void MSUnitTest1()
        {
            //Directory.SetCurrentDirectory(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString());
            //log.Info("#######" + Directory.GetParent(Directory.GetCurrentDirectory().ToString()));

            //var proj = GetSiteUrl(TestEnv.Prod, Project.SMF202);
            //var user = GetUser(UserType.SysAdmin);
            //log.Info(proj);
            //log.Info(user);
            //log.Info();

            //var collection = ConfigurationManager.GetSection($"TestConfigs/UserType") as NameValueCollection;
            //var userPw = collection["SysAdminUsername"].Split(',');

            //string[] userPw = GetUser(UserType.ProjAdmin);
            //string username = userPw[0];
            //string password = userPw[1];
            //log.Info($"Username : {username} and Password : {password}");


            //By locator = PageHelper.GetExpandDDListByLocator(DDListID.Action);
            //string locatorName = locator.GetType().Namespace;

            //IWebDriver driver = new ChromeDriver();
            //try
            //{
            //    driver.Navigate().GoToUrl("http://stage.garnet.elvispmc.com/");
            //    IWebElement elem = driver.FindElement(By.XPath("//img[@title='Garnet Interchange']"));

            //    log.Info(elem.GetType().Name);
            //}
            //catch (Exception e)
            //{
            //    log.Info(e.Message);
            //}

            //driver.Quit();

            //AutoItX.Run("notepad.exe", "C:\\Windows");
            //AutoItX.WinWaitActive("Untitled");
            //AutoItX.Send("Testing 1 2 3 4 5");
            //IntPtr winHandle = AutoItX.WinGetHandle("Untitled");
            //AutoItX.WinKill(winHandle);

            log.Error($"{GetCodeBasePath()}\\UploadFiles\\test.xlsx");
        }

        //[TestMethod]
        public void MSUnitTest2()
        {
            List<string> components = GetComponentsForProject(TenantName.Garnet);
            int componentCount = components.Count;

            log.Error($"Component count is {componentCount}");

            foreach (var component in components)
            {
                log.Error(component.ToString());
            }

            Assert.IsTrue(componentCount.Equals(4));
            Assert.IsFalse(components.Contains(Component.DIR));
        }

        //[TestMethod]
        public void MSUnitTest3()
        {
            var project = Project.Administration.UserManagement.Menu.Roles;
            Type projectType = project.GetType();
            var reflectedType = projectType.ReflectedType;
            var reflectedTypeName = reflectedType.Name;

            log.Error($"ProjectType: {projectType.ToString()}");
            log.Error($"ReflectedType: {reflectedType}");
            log.Error($"ReflectedType Name: {reflectedTypeName}");
            log.Error(typeof(Project).ToString());

            //Assert.IsTrue(reflectedType.IsSubclassOf(typeof(Project)));
            Assert.IsTrue(reflectedType.Equals(typeof(Project.Administration.UserManagement)));
            //try
            //{

            //    //Assert.IsTrue(reflectedTypeName == typeof(Project).ToString());
            //}
            //catch (Exception e)
            //{
            //    log.Error(e.Message);
            //}
        }

        //[TestMethod]
        public void MSUnitTest4()
        {
            var stringOne = "one";
            var numberOne = 1;
            Type stringOneType = null;
            stringOneType = stringOne.GetType();
            Type numberOneType = null;
            numberOneType = numberOne.GetType();

            log.Error(stringOne.GetType().ToString());
            Assert.IsTrue(stringOneType.Equals(typeof(string)));
            log.Error(numberOne.GetType().ToString());
            Assert.IsTrue(numberOneType.Equals(typeof(int)));

        }


        [TestMethod]
        public void VerifyMSUnitTest5()
        {
            //var lockedRecords = Project.Administration.AdminTools.Menu.Locked_Records;
            //var reflectedPageType = lockedRecords.GetType().ReflectedType;

            //Assert.IsTrue(reflectedPageType.IsSubclassOf(typeof(Project)));

            //string value = "## Actual : 1234 <br>&nbsp;&nbsp;## Expected : 45678";
            //string[] result = Regex.Split(value, " <br>&nbsp;&nbsp;");
            //log.Info(result[0]);
            //log.Info(result[1]);

            //string exe = "nunit3-console";
            //string arg = $"-p:Platform=Local -p:TestEnv=Stage -p:ProjectName=GLX --test=RKCIUIAutomation.Test.Smoke.LatestTest " +
            //    $"C:\\Users\\schong\\source\\repos\\RKCIUIAutomation\\RKCIUIAutomation\\bin\\Debug\\RKCIUIAutomation.dll";

            //RunExternalExecutible(exe, arg);
            //ZaleniumService zalenium = new ZaleniumService();
            //Console.WriteLine(zalenium.ZaleniumIsRunning());
            //Assert.IsTrue();

            //HashMap.CreateVar("Int", 1200);
            //HashMap.CreateVar("String", "TestName1234");

            //Console.WriteLine(HashMap.GetVar("Int"));
            //Console.WriteLine(HashMap.GetVar("String"));
            //Console.WriteLine(HashMap.GetVar("NotThere"));

            string details = "Test<br>&nbsp;&nbsp;Page Title:BlahBlash<br>&nbsp;&nbsp;More Details Here";

            bool hasPgBreak = false;
            string[] detailsBr = null;

            if (details.Contains("<br>"))
            {
                detailsBr = Regex.Split(details, "<br>&nbsp;&nbsp;");
                hasPgBreak = true;
            }

            if (hasPgBreak)
            {
                for (int i = 0; i < detailsBr.Length; i++)
                {
                    log.Info(detailsBr[i]);
                }
            }

        }

        //[TestMethod]
        public void TestMiniGuid()
        {
            MiniGuid guid;
            guid = MiniGuid.NewGuid();

            string key = "GUID";
            CreateVar(key, guid);

            var value = GetVar(key);
            Console.WriteLine(value);
        }


        //[TestMethod]
        public void TestCreateGetVar()
        {
            CreateVar("Int", 1500000);
            CreateVar("String", "BlahBlahTestName1234654789654321");

            Console.WriteLine(GetVar("Int"));
            Console.WriteLine(GetVar("String"));
            Console.WriteLine(GetVar("NotThere"));

            //
        }

        //[TestMethod]
        public void TestXMLUtil()
        {
            XMLUtil xmlUtil = new XMLUtil();
            xmlUtil.XMLDocumentTool();
        }

        private class BtnCategory
        {
            internal const string Cat1 = "Cat1";
        }
        enum TableButton
        {
            [StringValue("", BtnCategory.Cat1)] QMS_Attachments_View,
            [StringValue("-1")] Report_View,
            [StringValue("-2")] WebForm_View,
            [StringValue("-3")] Attachments_View,
            [StringValue("")] Action_Edit,
            [StringValue("first")] First,
            [StringValue("previous")] Previous,
            [StringValue("next")] Next,
            [StringValue("last")] Last
        }

        [TestMethod]
        public void XPathStringTest()
        {
            string SetXPath_TableRowBaseByTextInRow(string textInRowForAnyColumn) => $"//td[text()='{textInRowForAnyColumn}']/parent::tr/td";
            string xpath(string textInRowForAnyColumn, TableButton tblRowBtn) => $"{SetXPath_TableRowBaseByTextInRow(textInRowForAnyColumn)}[last(){tblRowBtn.GetString()}]/a";
            string xpath2(string textInRowForAnyColumn, TableButton tblRowBtn) => $"{SetXPath_TableRowBaseByTextInRow(textInRowForAnyColumn)}[last(){tblRowBtn.GetString(true)}]/a";

            Console.WriteLine($"XPATH: {xpath("Ron Seal", TableButton.QMS_Attachments_View)}");
            Console.WriteLine($"XPATH2: {xpath2("Ron Seal", TableButton.QMS_Attachments_View)}");
            Console.WriteLine($"PREVIOUS: {xpath("Ron Seal", TableButton.Previous)}");
        }
    }
}
