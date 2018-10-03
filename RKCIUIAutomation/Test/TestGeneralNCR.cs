using NUnit.Framework;
using RKCIUIAutomation.Config;

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
            GeneralNCR.ClickBtn_New();
            GeneralNCR.PopulateRequiredFieldsAndSave();
            Assert.True(GeneralNCR.VerifyNCRDocInReviseTab());
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
    public class Verify_NCR_UnitTest_Garnet : TestBase
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
            GeneralNCR.ClickTab_Review_Assign_NCR();
            GeneralNCR.ClickTab_Resolution_Disposition();
            GeneralNCR.ClickTab_Engineer_Concurrence();
            GeneralNCR.ClickTab_Owner_Concurrence();
            GeneralNCR.ClickTab_Originator_Concurrence();
            GeneralNCR.ClickTab_Verification();
            GeneralNCR.ClickTab_Closed_NCR();
            GeneralNCR.ClickTab_Creating_Revise();
            GeneralNCR.ClickBtn_New();
        }
    }

    [TestFixture]
    public class Verify_NCR_UnitTest_GLX : TestBase
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
            GeneralNCR.ClickTab_CQM_Review();
            GeneralNCR.ClickTab_Resolution_Disposition();
            GeneralNCR.ClickTab_Developer_Concurrence();
            GeneralNCR.ClickTab_DOT_Approval();
            GeneralNCR.ClickTab_Verification_and_Closure();
            GeneralNCR.ClickTab_All_NCRs();
            GeneralNCR.ClickTab_Creating_Revise();
            GeneralNCR.ClickBtn_New();
        }
    }

    [TestFixture]
    public class Verify_NCR_UserAccts : TestBase
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

}
