﻿using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Test.NCR
{
    [TestFixture]
    public class Verify_Create_And_Save_NCR_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187687)]
        [Property(Priority, "High")]
        [Description("To validate successful create and save of an NCR (Nonconformance Report) document.")]
        public void Create_And_Save_NCR_Document()
        {
            LoginAs(UserType.NCRTech);
            NavigateToPage.QARecordControl_General_NCR();
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
            QaRcrdCtrl_GeneralNCR.PopulateRequiredFieldsAndSave();
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocInReviseTab());
        }
    }

    [TestFixture]
    public class Verify_QC_Review_of_NCR_document_by_NCR_Manager : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187688)]
        [Property(Priority, "High")]
        [Description("To validate the QC review part of an NCR (Nonconformance Report).")]
        public void QC_Review_of_NCR_document_by_NCR_Manager()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
        }
    }

    [TestFixture]
    public class Verify_Revising_the_NCR_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187689)]
        [Property(Priority, "High")]
        [Description("To successfully revising an NCR (Nonconformance Report) document.")]
        public void Revise_the_NCR_Document()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
        }
    }

    [TestFixture]
    public class Verify_Closing_the_NCR_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187690)]
        [Property(Priority, "High")]
        [Description("To successfully close an NCR (Nonconformance Report) document.")]
        public void Close_the_NCR_Document()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
        }
    }

    [TestFixture]
    public class Verify_Editing_the_NCR_Document : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2187691)]
        [Property(Priority, "High")]
        [Description("To successfully edit an NCR (Nonconformance Report) document.")]
        public void Edit_the_NCR_Document()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
        }
    }

    [TestFixture]
    public class Verify_Vewing_NCR_Document_Report : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 2188063)]
        [Property(Priority, "High")]
        [Description("To successfully view the report of an NCR (Nonconformance Report) document.")]
        public void View_NCR_Document_Report()
        {
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
        }
    }



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
            LogInfo("Unit test for Garnet");
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
            LogInfo("Unit test for GLX");
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
        public void NCR_UserAccts()
        {
            LogInfo($"Testing, UserAccts for {tenantName}");
            LoginAs(UserType.NCRMgr);
            string CurrentUser = GetCurrentUser();
            System.Console.WriteLine($"USER: {CurrentUser}");
            Assert.True(CurrentUser == "NCR Mgr");
            ClickLogoutLink();
            ClickLoginLink();
            LoginAs(UserType.NCRTech);
            CurrentUser = GetCurrentUser();
            System.Console.WriteLine($"USER: {CurrentUser}");
            Assert.True(CurrentUser == "NCR Tech");
        }
    }

    [TestFixture]
    public class UnitTest_NCR_FilterAndSort : TestBase
    {
        [Test]
        [Category(Component.NCR)]
        [Property(TestCaseNumber, 123456)]
        [Property(Priority, "High")]
        [Description("NCR UserAccts")]
        public void NCR_FilterAndSort()
        {
            LogInfo($"Testing, UserAccts for {tenantName}");
            LoginAs(UserType.NCRMgr);
            NavigateToPage.QARecordControl_General_NCR();
            QaRcrdCtrl_GeneralNCR.FilterDescription();
            ClearTableFilters();
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
            LogInfo($"Testing, UserAccts for {tenantName}");
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

}
