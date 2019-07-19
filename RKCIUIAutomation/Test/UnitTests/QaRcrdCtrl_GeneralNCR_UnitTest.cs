using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.Workflows;
using System.Collections.Generic;
using System.Threading;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Test.UnitTests
{
    [TestFixture]
    public class UnitTest_NCR_UnitTest_Garnet : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("NCR UnitTest for Garnet")]
        public void NCR_UnitTest_Garnet()
        {
            Report.Info("Unit test for Garnet");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_NCR();
            QaRcrdCtrl_GeneralNCR.ClickTab_Review_Assign_NCR();
            QaRcrdCtrl_GeneralNCR.ClickTab_Resolution_Disposition();
            QaRcrdCtrl_GeneralNCR.ClickTab_Engineer_Concurrence();
            QaRcrdCtrl_GeneralNCR.ClickTab_Owner_Concurrence();
            QaRcrdCtrl_GeneralNCR.ClickTab_Originator_Concurrence();
            QaRcrdCtrl_GeneralNCR.ClickTab_Verification();
            QaRcrdCtrl_GeneralNCR.ClickTab_Closed_NCR();
            QaRcrdCtrl_GeneralNCR.ClickTab_Creating_Revise();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
        }
    }

    [TestFixture]
    public class UnitTest_NCR_UnitTest_GLX : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("NCR UnitTest for GLX")]
        public void NCR_UnitTest_GLX()
        {
            Report.Info("Unit test for GLX");
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_NCR();
            QaRcrdCtrl_GeneralNCR.ClickTab_CQM_Review();
            QaRcrdCtrl_GeneralNCR.ClickTab_Resolution_Disposition();
            QaRcrdCtrl_GeneralNCR.ClickTab_Developer_Concurrence();
            QaRcrdCtrl_GeneralNCR.ClickTab_DOT_Approval();
            QaRcrdCtrl_GeneralNCR.ClickTab_Verification_and_Closure();
            QaRcrdCtrl_GeneralNCR.ClickTab_All_NCRs();
            QaRcrdCtrl_GeneralNCR.ClickTab_Creating_Revise();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
        }
    }

    [TestFixture]
    public class UnitTest_NCR_UserAccts : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("NCR UserAccts")]
        public void UserName()
        {
            Report.Info($"Testing, UserAccts for {tenantName}");
            //LoginAs(UserType.IQFRecordsMgr);
            DesignDocumentWF ddwf = new DesignDocumentWF();
            ddwf.LogIntoDesignDocumentsPage(DesignDocumentWF.CR_Workflow.CreateComment);

            string CurrentUser = GetCurrentUser();
            Assert.True(CurrentUser == "AT_CR Comment");

            //LogoutToLoginPage();
            //driver.Navigate().GoToUrl("http://stage.sh249.elvispmc.com/Account/Login");
            //LoginAs(UserType.IQFRecordsMgr);
            //CurrentUser = GetCurrentUser();
            //Assert.True(CurrentUser == "IQF Records Manager");
        }
    }

    [TestFixture]
    public class UnitTest_NCR_FilterAndSort : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("NCR FilterAndSort")]
        public void NCR_FilterAndSort()
        {
            Report.Info($"Testing, UserAccts for {tenantName}");
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
            QaRcrdCtrl_GeneralNCR.FilterDescription();
            GridHelper.ClearTableFilters();
            QaRcrdCtrl_GeneralNCR.SortTable_Descending();
            Thread.Sleep(2000);
            QaRcrdCtrl_GeneralNCR.SortTable_Ascending();
            Thread.Sleep(2000);
            QaRcrdCtrl_GeneralNCR.SortTable_ToDefault();
        }
    }

    [TestFixture]
    public class UnitTest_NCR_RequiredFieldIDs : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("NCR UserAccts")]
        public void NCR_RequiredFieldIDs()
        {
            Report.Info($"Testing, UserAccts for {tenantName}");
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();

            string requiredFieldXpath = "//span[contains(text(),'Required')]";
            string sibling = "//following-sibling::";

            string datePickerAddlXpath = "span[contains(@class,'picker')]/span/input";
            string textAreaAddlXpath = "textarea";
            string ddListAddlXpath = "span[contains(@class,'dropdown')]/input";
            string textInputAddlXpath = "input";


            string key;
            string type;
            string name;
            string id;

            List<KeyValuePair<string, string>> reqFieldPairs = new List<KeyValuePair<string, string>>();

            IList<IWebElement> reqFieldElems = Driver.FindElements(By.XPath(requiredFieldXpath));
            System.Console.WriteLine(reqFieldElems.Count);

            var dateElems = Driver.FindElements(By.XPath($"{requiredFieldXpath}{sibling}{datePickerAddlXpath}"));
            if (dateElems != null)
            {
                type = "Date";

                foreach (IWebElement dateElem in dateElems)
                {
                    id = dateElem.GetAttribute("id");
                    name = dateElem.GetAttribute("name");

                    key = $"{type}\n{name}";
                    reqFieldPairs.Add(new KeyValuePair<string, string>(key, id));
                }
            }

            var ddListElems = Driver.FindElements(By.XPath($"{requiredFieldXpath}{sibling}{ddListAddlXpath}"));
            if (ddListElems != null)
            {
                type = "DDList";

                foreach (IWebElement ddlElem in ddListElems)
                {
                    id = ddlElem.GetAttribute("id");
                    name = ddlElem.GetAttribute("name");

                    key = $"{type}\n{name}";
                    reqFieldPairs.Add(new KeyValuePair<string, string>(key, id));
                }
            }

            var txtInputElems = Driver.FindElements(By.XPath($"{requiredFieldXpath}{sibling}{textInputAddlXpath}"));
            if (txtInputElems != null)
            {
                type = "TextInput";

                foreach (IWebElement txtInput in txtInputElems)
                {
                    id = txtInput.GetAttribute("id");
                    name = txtInput.GetAttribute("name");

                    key = $"{type}\n{name}";
                    reqFieldPairs.Add(new KeyValuePair<string, string>(key, id));
                }
            }

            var textAreaElems = Driver.FindElements(By.XPath($"{requiredFieldXpath}{sibling}{textAreaAddlXpath}"));
            if (textAreaElems != null)
            {
                type = "TextArea";

                foreach (IWebElement txtArea in textAreaElems)
                {
                    id = txtArea.GetAttribute("id");
                    name = txtArea.GetAttribute("name");

                    key = $"{type}\n{name}";
                    reqFieldPairs.Add(new KeyValuePair<string, string>(key, id));
                }
            }


            System.Console.WriteLine($"{tenantName}\n");
            for (int x = 0; x < reqFieldPairs.Count; x++)
            {
                var pairKey = reqFieldPairs[x].Key;
                var value = reqFieldPairs[x].Value;

                System.Console.WriteLine($"{pairKey}\n{value}\n");
            }
        }
    }

    [Parallelizable]
    [TestFixture]
    public class UnitTest_Verify_AssertAll : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("AssertAll")]
        public void Verify_AssertAll()
        {
            AddAssertionToList(true);
            AddAssertionToList(true);
            AddAssertionToList(false);
            AddAssertionToList(true);
            AddAssertionToList(true);
            AssertAll();
        }

        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("ExtentReport_NCR")]
        public void Verify_ExtentReport_NCR()
        {
            AddAssertionToList(true);
            AddAssertionToList(true);
            AddAssertionToList(false);
            AddAssertionToList(true);
            AddAssertionToList(true);
            AssertAll();
        }

        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("ExtentReport_CDR")]
        public void Verify_ExtentReport_CDR()
        {
            AddAssertionToList(true);
            AddAssertionToList(true);
            AddAssertionToList(false);
            AddAssertionToList(true);
            AddAssertionToList(true);
            AssertAll();
        }

        [Test]
        [Category(Component.CommentReview_RegularComment)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("ExtentReport_CR")]
        public void Verify_ExtentReport_CR()
        {
            AddAssertionToList(true);
            AddAssertionToList(true);
            AddAssertionToList(false);
            AddAssertionToList(true);
            AddAssertionToList(true);
            AssertAll();
        }
    }

    [TestFixture]
    public class UnitTest_Verify_ExtentReport_NCR : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("ExtentReport_NCR")]
        public void Verify_ExtentReport_NCR()
        {
            AddAssertionToList(true);
            AddAssertionToList(true);
            AddAssertionToList(false);
            AddAssertionToList(true);
            AddAssertionToList(true);
            AssertAll();
        }
    }

    [TestFixture]
    public class UnitTest_Verify_ExtentReport_CDR : TestBase
    {
        [Test]
        [Category(Component.CDR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("ExtentReport_CDR")]
        public void Verify_ExtentReport_CDR()
        {
            AddAssertionToList(true);
            AddAssertionToList(true);
            AddAssertionToList(false);
            AddAssertionToList(true);
            AddAssertionToList(true);
            AssertAll();
        }
    }

    [TestFixture]
    public class UnitTest_Verify_ExtentReport_CR : TestBase
    {
        [Test]
        [Category(Component.CommentReview_RegularComment)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("ExtentReport_CR")]
        public void Verify_ExtentReport_CR()
        {
            AddAssertionToList(true);
            AddAssertionToList(true);
            AddAssertionToList(false);
            AddAssertionToList(true);
            AddAssertionToList(true);
            AssertAll();
        }
    }
}
