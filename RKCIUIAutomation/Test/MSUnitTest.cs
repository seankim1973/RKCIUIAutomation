using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using MiniGuids;
using NUnit.Framework.Interfaces;
using RestSharp.Extensions;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using RKCIUIAutomation.Test;
using RKCIUIAutomation.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using static RKCIUIAutomation.Page.Navigation.NavMenu;

namespace RKCIUIAutomation.Sandbox
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

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

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

        #endregion Additional test attributes

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
            var value = GetVar("GUID");
            Console.WriteLine(value);
        }

        [TestMethod]
        public void TestCreateGetVar()
        {
            CreateVar("Int", "12345");
            CreateVar("String", "BlahBlahTestName1234654789654321");

            Console.WriteLine(GetVar("Int"));
            Console.WriteLine(GetVar("String"));
            Console.WriteLine(GetVar("NotThere"));
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

        private new enum TableButton
        {
            [StringValue("", BtnCategory.Cat1)] QMS_Attachments_View,
            [StringValue("-1")] Report_View,
            [StringValue("-2")] WebForm_View,
            [StringValue("-3")] Attachments_View,
            [StringValue("")] Action_Edit,
            [StringValue("first")] First,
            [StringValue("previous")] Previous,
            [StringValue("next")] Next,
            [StringValue("last")] Last,
            [StringValue("KENDOUItabStripID")] KendoTabStripId,
            NoStringValueEnum
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

        [TestMethod]
        public void EnumParseTest()
        {
            Enum tblTabEnum = TableButton.Report_View;
            BaseUtils baseUtils = new BaseUtils();

            Type enumType = tblTabEnum.GetType();
            object kendoTabStripEnum = Enum.Parse(enumType, "KendoTabStripId");
            Enum tabStripEnum = baseUtils.ConvertToType<Enum>(kendoTabStripEnum);
            string expected = "KENDOUItabStripID";
            string actual = tabStripEnum.GetString();
            Console.WriteLine($"EXPECTED VALUE: {expected}\nACTUAL VALUE: {actual}");
            Assert.AreEqual(expected, actual);
            bool nostringEnumVal =TableButton.NoStringValueEnum.Equals("NoStringValueEnum");
            Console.WriteLine($"nostringEnumVal : {nostringEnumVal}");
        }


        [TestMethod]
        public void ParseUserAcct()
        {
            string[] names = {
            " Welcome Test IQF Records Manager X",
            " Welcome Test IQF Admin X",
            " Welcome Test IQF User X",
            " Welcome Test DOT Admin X",
            " Welcome Test DOT User X",
            " Welcome Test DEV Admin X",
            " Welcome Test DEV User X",
            " Welcome Test User"
            };

            foreach (string name in names)
            {
                string[] acct = null;
                try
                {
                    if (name.Contains("X"))
                    {
                        acct = Regex.Split(name, "Welcome Test ");
                        acct = Regex.Split(acct[1], " X");
                        Console.WriteLine(acct[0]);
                    }
                    else
                    {
                        acct = Regex.Split(name, "Welcome ");
                        Console.WriteLine(acct[1]);
                    }
                }
                catch (Exception e)
                {
                    log.Debug(e.Message);
                }
            }
        }

        [TestMethod]
        public void StaticValues()
        {
            SimpleTestClass testclass = new SimpleTestClass();
            testclass.SetValue1();

            Console.WriteLine(SimpleTestClass.Value1);
            Console.WriteLine(testclass.Value2);
        }
    }

    [TestClass]
    public class SimpleTestClass
    {
        public static string Value1;
        public string Value2;

        public void SetValue1()
        {
            Value1 = "Testing123";
            Value2 = "TestTest";
        }

        [TestMethod]
        public void Ping()
        {
            Ping pingSender = new Ping();
            IPAddress address = IPAddress.Loopback;
            PingReply reply = pingSender.Send(address);

            if (reply.Status == IPStatus.Success)
            {
                Console.WriteLine("Address: {0}", reply.Address.ToString());
                Console.WriteLine("RoundTrip time: {0}", reply.RoundtripTime);
                Console.WriteLine("Time to live: {0}", reply.Options.Ttl);
                Console.WriteLine("Don't fragment: {0}", reply.Options.DontFragment);
                Console.WriteLine("Buffer size: {0}", reply.Buffer.Length);
            }
            else
            {
                Console.WriteLine(reply.Status);
            }

            bool isPingable = (reply.Status == IPStatus.Success) ? true : false;
            Console.WriteLine(isPingable);
        }

        [TestMethod]
        public void HipTest_GetTestRunScenarios()
        {
            //int testCaseNumber = 1234;
            //HipTestApi hipTest = new HipTestApi();
            //hipTest.BuildTestRunSnapshotData(195019);

            string test = "    12345   ";
            test = test.Trim();
            Console.WriteLine(test);
        }

        [TestMethod]
        public void HipTest_UpdateTestResult()
        {
            string[] testRunDetails = new string[]
            {
                "testSuite",
                "testPriority",
                "testCaseNumber",
                "testComponent1",
                "testComponent2",
                "testEnv",
                "tenantName"
            };

            List<int> scenarioIDs = new List<int>
            {
                2238584,
                2238585,
                2238586
            };

            string description = "Test Description - (Env)Tenant";

            var passDescPair = new KeyValuePair<TestStatus, string>(TestStatus.Passed, description);
            var failDescPair = new KeyValuePair<TestStatus, string>(TestStatus.Failed, description);

            var testResults = new List<KeyValuePair<int, KeyValuePair<TestStatus, string>>>
            {
                new KeyValuePair<int, KeyValuePair<TestStatus, string>>(2238585, passDescPair),
                new KeyValuePair<int, KeyValuePair<TestStatus, string>>(2238586, failDescPair),
                new KeyValuePair<int, KeyValuePair<TestStatus, string>>(2238584, passDescPair)
            };

            HipTestApi hipTest = new HipTestApi();
            int testRunID = hipTest.CreateTestRun(scenarioIDs, testRunDetails);
            Console.WriteLine($"TESTRUN ID: {testRunID}");
            Thread.Sleep(5000);
            var runData = hipTest.BuildTestRunSnapshotData(testRunID);
            Thread.Sleep(5000);
            hipTest.UpdateHipTestRunData(runData, testResults);
            //RunCreateTestRunTask(scenarioIDs, testRunDetails, testResults);
        }

        public void RunCreateTestRunTask(List<int> scenarioIDs, string[] testRunDetails, List<KeyValuePair<int, KeyValuePair<TestStatus, string>>> testResults)
        {
            HipTestApi hipTest = new HipTestApi();
            int testRunID = hipTest.CreateTestRun(scenarioIDs, testRunDetails);
            Console.WriteLine($"TESTRUN ID: {testRunID}");
            var runData = hipTest.BuildTestRunSnapshotData(testRunID);
            hipTest.UpdateHipTestRunData(runData, testResults);
        }

        [TestMethod]
        public void NestedIfElse()
        {
            bool boolean = false;

            if (!boolean)
            {
                Console.WriteLine("1");
                if (!boolean)
                {
                    Console.WriteLine("2");
                    if (!boolean)
                    {
                        boolean = true;
                        Console.WriteLine("3");
                        if (!boolean)
                        {
                            Console.WriteLine("4");
                        }
                    }
                }
            }

            if (boolean)
            {
                Console.WriteLine("Eureka!!");
            }
        }

        [TestMethod]
        public void NestForEach()
        {
            IList<string> ABCs = new List<string>()
            {
                "A", "B", "C", "D"
            };

            IList<int> NUMs = new List<int>()
            {
                1, 2, 3, 4, 5
            };

            foreach (string abc in ABCs)
            {
                string letter = abc;

                foreach (int number in NUMs)
                {
                    Console.WriteLine($"{letter}{number.ToString()}");
                }
            }
        }

        [TestMethod]
        public void GetNamespace()
        {
            string[] name = Regex.Split(GetType().Namespace, "\\.");
            Console.WriteLine($"INDEX: {name.Length}");
            Console.WriteLine(name[name.Length - 1]);
        }

        [TestMethod]
        public void ShortDate()
        {
            string shortDate = DateTime.MaxValue.Date.ToShortDateString();
            //DateTime.Now.ToShortDateString();
            string shortTime = DateTime.Now.ToShortTimeString();
            Console.WriteLine($"{shortDate} {shortTime}");
        }

        [TestMethod]
        public void FormatTimeBlock()
        {
            var timeBlock = "AM_12_00";
            string[] block = Regex.Split(timeBlock.ToString(), "_");
            string meridiem = block[0];
            string time = $"{block[1]}:{block[2]} {meridiem}";

            Console.WriteLine(time);
        }

        [TestMethod]
        public void StringContains()
        {
            string userAcct = "Test IQF Comment Review";
            bool containsX = userAcct.Contains("IQF") ? true : false;
            Console.WriteLine($"Contains IQF: {containsX}");
        }

        [TestMethod]
        public void NestedInLineIfElseStatement()
        {
            string expected = string.Empty;
            string actual = string.Empty;

            bool shouldBeSelected = true;
            bool isSelected = true;
            SetValues();
            bool selectionMeetsExpectation = isSelected.Equals(shouldBeSelected) ? true : false;
            string logMsg = selectionMeetsExpectation ? $"field meets expectation: {expected} and {actual}" : $"field does not meet expectation: {expected}, but {actual}";
            //string logMsg = isSelected ? selectionMeetsExpectation ? "is selected as expected(1)" : "is not selected as expected(2)" : $"field does not meet expectation\n{expected}, but {actual}";
            Console.WriteLine($"ShouldBeSelected: {shouldBeSelected.ToString()}\nIsSelected: {isSelected.ToString()}\nMeets Expectation: {selectionMeetsExpectation}\nRadio button {logMsg}\n");

            shouldBeSelected = true;
            isSelected = false;
            SetValues();
            selectionMeetsExpectation = isSelected.Equals(shouldBeSelected) ? true : false;
            logMsg = selectionMeetsExpectation ? $"field meets expectation: {expected} and {actual}" : $"field does not meet expectation: {expected}, but {actual}";
            //logMsg = isSelected ? selectionMeetsExpectation ? "is selected as expected(1)" : "is not selected as expected(2)" : $"field does not meet expectation\n{expected}, but {actual}";
            Console.WriteLine($"ShouldBeSelected: {shouldBeSelected.ToString()}\nIsSelected: {isSelected.ToString()}\nMeets Expectation: {selectionMeetsExpectation}\nRadio button {logMsg}\n");

            shouldBeSelected = false;
            isSelected = true;
            SetValues();
            selectionMeetsExpectation = isSelected.Equals(shouldBeSelected) ? true : false;
            logMsg = selectionMeetsExpectation ? $"field meets expectation: {expected} and {actual}" : $"field does not meet expectation: {expected}, but {actual}";
            //logMsg = isSelected ? selectionMeetsExpectation ? "is selected as expected(1)" : "is not selected as expected(2)" : $"field does not meet expectation\n{expected}, but {actual}";
            Console.WriteLine($"ShouldBeSelected: {shouldBeSelected.ToString()}\nIsSelected: {isSelected.ToString()}\nMeets Expectation: {selectionMeetsExpectation}\nRadio button {logMsg}\n");

            shouldBeSelected = false;
            isSelected = false;
            SetValues();
            selectionMeetsExpectation = isSelected.Equals(shouldBeSelected) ? true : false;
            logMsg = selectionMeetsExpectation ? $"field meets expectation: {expected} and {actual}" : $"field does not meet expectation: {expected}, but {actual}";
            //logMsg = isSelected ? selectionMeetsExpectation ? "is selected as expected(1)" : "is not selected as expected(2)" : $"field does not meet expectation\n{expected}, but {actual}";
            Console.WriteLine($"ShouldBeSelected: {shouldBeSelected.ToString()}\nIsSelected: {isSelected.ToString()}\nMeets Expectation: {selectionMeetsExpectation}\nRadio button {logMsg}\n");

            void SetValues()
            {
                expected = shouldBeSelected ? "should be selected" : "should not be selected";
                actual = isSelected ? "is selected" : "is not selected";
            }
        }

        [TestMethod]
        public void SplitNamesInList()
        {
            QADIRs qaDirs = new QADIRs();

            IList<string> expectedList = qaDirs.GetExpectedRequiredFieldIDsList();
            IList<string> trimmedExpectedIDs = qaDirs.TrimInputFieldIDs(expectedList, "0__");

            Console.WriteLine("Trimmed Expected Field IDs List");
            foreach (string id in trimmedExpectedIDs)
            {
                Console.WriteLine($"Expected Field ID: {id}");
            }

            IList<string> errorIDs = new List<string>()
            {
                "span_0_TimeBegin1",
                "span_0_TimeEnd1",
                "span_0_AverageTemperature",
                "span_0_AreaID",
                "span_0_SectionId",
                "span_0_FeatureID",
                "span_0_ContractorId",
                "span_0_CrewForemanID",
                "span_0_SectionDescription",
                "span_0_InspectionType",
                "span_0_InspectionPassFail"
            };

            IList<string> trimmedIDs = qaDirs.TrimInputFieldIDs(errorIDs, "0_");

            Console.WriteLine("##################### \nTrimmed Error Field IDs List");
            foreach (string id in trimmedIDs)
            {
                Console.WriteLine($"Error Field ID: {id}");
                Console.WriteLine($"Actual Error Field ID: {id} contained in Expected List: {trimmedExpectedIDs.Contains(id)}");
            }


            IList<string> summaryList = new List<string>()
            {
                "DIR: 1st Shift TimeBegin1 Required",
                "DIR: 1st Shift TimeEnd1 Required",
                "Entry Number 1: Area Required",
                "Entry Number 1: Average Temperature Required",
                "Entry Number 1: Section Required",
                "Entry Number 1: Section Description Required",
                "Entry Number 1: Feature Required",
                "Entry Number 1: Crew Foreman Required",
                "Entry Number 1: Contractor Required",
                "Entry Number 1: Inspection Type Required",
                "Entry Number 1: Inspection Result Required"
            };

            IList<string> idList = new List<string>();
            Console.WriteLine("###############################\nSummary List");
            foreach (string error in summaryList)
            {
                string[] splitType = new string[2];

                if (error.Contains("DIR:"))
                {
                    splitType = Regex.Split(error, "Shift ");
                }
                else if (error.Contains("Entry Number"))
                {
                    splitType = Regex.Split(error, "1: ");
                }

                string[] splitReq = Regex.Split(splitType[1], " Required");
                string fieldName = splitReq[0];
                idList.Add(fieldName);
                Console.WriteLine($"List contains field name - {fieldName}: {trimmedIDs.Contains(fieldName)}");
            }
        }

        [TestMethod]
        public void TimeCompare()
        {
            var currentTime = Regex.Replace("19:55", ":", "");
            var refreshTime = Regex.Replace("19:59", ":", "");

            int current = int.Parse(currentTime);
            int refresh = int.Parse(refreshTime);

            bool compare = refresh > current;
            Console.WriteLine($"Current Int: {current}\nRefresh Int: {refresh}\nRefresh is Greater: {compare}");

        }

        [TestMethod]
        public void HttpRequest()
        {
            string fileUrl = "http://stage.glx.elvispmc.com/Dir/ViewDirPDF?DirId=0000";
            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(fileUrl);
            httpRequest.Method = WebRequestMethods.Http.Get;

            HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            Stream httpResponseStream = httpResponse.GetResponseStream();
            string responseCode = httpResponse.StatusCode.ToString();

            Console.WriteLine($"responseCode: {responseCode}");
            Console.WriteLine($"httpResponse: {httpResponse.ResponseUri}");
            Console.WriteLine($"httpResponseStream: {httpResponseStream}");

        }

        [TestMethod]
        public void StringTest()
        {
            //string[] rowIDs = new string[]{ "123", "456", "789" };
            //string url = "http://stage.sh249.elvispmc.com/Dir/ViewDirPDF?DirId=5626";
            //string[] split = Regex.Split(url, "ViewDirPDF\\?DirId=");

            //int rowCount = 3;
            //string expectedUrl = $"{split[0]}ViewMultiDirPDF?";

            //for (int i = 0; i < rowCount; i++)
            //{
            //    string dirID = $"DirIds[{i}]={rowIDs[i]}&";
            //    expectedUrl = $"{expectedUrl}{dirID}";
            //}

            //expectedUrl = expectedUrl.TrimEnd('&');
            //Console.WriteLine(expectedUrl);

            string val = "-1";
            Console.WriteLine((int.Parse(val) - 1).ToString());
        }

        [TestMethod]
        public void CurrentUserDownloadFolder()
        {
            var path = Registry.GetValue(@"HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders", "{374DE290-123F-4565-9164-39C4925E467B}", string.Empty).ToString();
            string fileName = "IQF-DIR-20190127-1.zip";
            string file = $"{path}\\{fileName}";
            bool fileExists = File.Exists(file);
            Assert.IsTrue(fileExists);
            if (fileExists)
            {
                File.Delete(file);
            }
            fileExists = File.Exists(file);
            Assert.IsFalse(fileExists);
            Console.WriteLine(file);
        }

        internal TOut GetDirPackagesDataForRow<TOut>(bool returnArray)
        {
            string tblData = returnArray
                ? "2286180604, 6045180604, 3421180604, 0083180604, 6045180605, 3424180606, 0663180606, 2286180606, 6045180606, 3424180607, 0663180607, 2286180607, 6045180607, 1716180607, 6045180608, 2286180608, 6621180609, 1716180607"
                : "DIRNumber";
            BaseUtils utils = new BaseUtils();

            if (returnArray)
            {
                string[] arrayArg = new string[] { };
                arrayArg = Regex.Split(tblData, ", ");
                return utils.ConvertToType<TOut>(arrayArg);
            }
            else
            {
                return utils.ConvertToType<TOut>(tblData);
            }
        }

        [TestMethod]
        public void GenericOutput()
        {
            //string valueString = GetDirPackagesDataForRow<string>(false);
            string[] valueArray = GetDirPackagesDataForRow<string[]>(true);

            //Console.WriteLine(valueString);

            foreach (string i in valueArray)
            {
                Console.WriteLine(i);
            }
        }

        [TestMethod]
        public void DateTimeParse()
        {
            //string filterValue = "04/22/2018";
            //DateTime.TryParse(filterValue, out DateTime filterDateTime);
            //Console.WriteLine($"MM: {filterDateTime.Month}");
            //Console.WriteLine($"DD: {filterDateTime.Day}");
            //Console.WriteLine($"YYYY: {filterDateTime.Year}");

            #region
            /*
            // Test to return future short date
            string currentDate = "12/28/2019"; //DateTime.Now.ToShortDateString();
            string[] splitShortDate = Regex.Split(currentDate, "/");
            int mm = int.Parse(splitShortDate[0]);
            int dd = int.Parse(splitShortDate[1]);
            int yyyy = int.Parse(splitShortDate[2]);

            yyyy = dd > 27 && mm == 12
                ? yyyy + 1
                :yyyy;

            mm = dd > 27
                ? mm == 12
                    ? 1
                    : mm + 1
                : mm;

            dd = dd > 27
                ? 1
                : dd + 1;

            string futureDate = $"{mm}/{dd}/{yyyy}";
            Console.WriteLine("CURRENT DATE: " + currentDate);
            Console.WriteLine("FUTURE DATE: " + futureDate);
            */
            #endregion

        }

        [TestMethod]
        public void Test()
        {
            //string packageNumber = "IQF-DIR-20181209-2";
            //Console.WriteLine(packageNumber.TrimEnd('1', '2'));

            //string test = "";
            //bool hasValue = test.HasValue();
            //bool isNullOrEmpty = string.IsNullOrEmpty(test);
            //Console.WriteLine($"HAS VALUE: {hasValue}");
            //Console.WriteLine($"ISNULLOREMPTY: {isNullOrEmpty}");

            /*
            //test for comparisons of List Array
            IList<string> stringList = new List<string>
            {
                "test1",
                "test2"
            };

            IList<string> stringList2 = new List<string>
            {
                "test1",
                "test2"
            };
            
            Console.WriteLine($"== eval : {stringList == stringList2}");
            Console.WriteLine($".equal eval : {stringList.Equals(stringList2)}");
            Console.WriteLine($".SequenceEqual eval : {stringList.SequenceEqual(stringList2)}");
            */

            /*
            //test for limiting length of string
            string testString = "abcdefghijklmnopqrstuvwxyz";
            testString = testString.Substring(0, 20);
            Console.WriteLine(testString);
            Console.WriteLine(testString.Length);
            testString = "abcdefghijklmnop";
            Console.WriteLine(testString + " length : " + testString.Length);
            */

            //IList<string> actual = new List<string>()
            //{
            //    "123ABC456",
            //    "78910"
            //};

            //IList<string> expected = new List<string>()
            //{
            //    "123",
            //    "ABC",
            //    "456",
            //    "789",
            //    "11"
            //};

            //for (int i = 0; i < expected.Count; i++)
            //{
            //    string e = expected[i];

            //    IList<bool> compareList = new List<bool>();

            //    foreach (string a in actual)
            //    {
            //        bool aContainsE = a.Contains(e);
            //        Console.WriteLine($"Actual {a} contains Expected {e} : {aContainsE}");
            //        compareList.Add(aContainsE);
            //    }

            //    bool result = compareList.Contains(true)
            //        ? true
            //        : false;

            //    Console.WriteLine($"RESULT : {result}");
            //}

            //string val = "Label::Value";
            //string[] splitVal = new string[] { };
            //splitVal = Regex.Split(val, "::");

            //string label = splitVal[0] + " : ";
            //string value = splitVal[1];

            //Console.WriteLine($"{label}{value}");

            //string str1 = "Value Of String";
            //string str2 = "Value Of";

            //bool str1ContainsStr2 = str1.Contains(str2);
            //bool str2ContainsStr1 = str2.Contains(str1);
            //bool str1Equals2 = str1.Equals(str2);

            // bool valuesMatch = (str1ContainsStr2 || str2ContainsStr1 || str1Equals2)
            //    ? true
            //    : false;

            //Console.WriteLine($"VALUES MATCH : {valuesMatch}");

            string str = "Quality Management Service";
            string value = Regex.Replace(str, @" ", "_");
            Console.WriteLine(value);
        }

        enum TestEnum
        {
            Test1,
            Test2
        }

        [TestMethod]
        public void KeyValueTest()
        {
            //IList<KeyValuePair<TestEnum, string>> kvPairs = new List<KeyValuePair<TestEnum, string>>();

            //KeyValuePair<TestEnum, string> pair1 = new KeyValuePair<TestEnum, string>(TestEnum.Test1, "test1Value");
            //KeyValuePair<TestEnum, string> pair2 = new KeyValuePair<TestEnum, string>(TestEnum.Test2, "test2Value");

            //kvPairs.Add(pair1);
            //kvPairs.Add(pair2);

            //var key = (from kvp in kvPairs where kvp.Key == TestEnum.Test1 select kvp.Value).FirstOrDefault();
            //Console.WriteLine(key);
            object arg = TestEnum.Test1;
            object argString = "";
            Type argType = arg.GetType();
            Type argStringType = argString.GetType();

            Console.WriteLine($"TYPE is Enum: {(arg is Enum).ToString()} - {argType.ToString()}");
            Console.WriteLine($"TYPE is String: {(argString is string).ToString()} - {argStringType.ToString()}");

        }
    }
}