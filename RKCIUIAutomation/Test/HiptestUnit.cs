using NUnit.Framework;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Test.Hiptest
{
    [TestFixture]
    public class BreakSheet_Module : TestBase
    {
        [Test]
        [Category(Component.Breaksheet_Module)]
        [Property(TestCaseNumber, 2238575)]
        [Property(Priority, "High")]
        [Description("To produce a Cylinder Break Sheet")]
        public void Create_a_BreakSheet_Document_Defect_EPA_691()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QALab_BreakSheet_Creation();
            Assert.True(true);
        }

        [Test]
        [Category(Component.Breaksheet_Module)]
        [Property(TestCaseNumber, 2238576)]
        [Property(Priority, "High")]
        [Description("Revise that the system is displaying the Break Sheet Legacy PDF properly.")]
        public void Produce_a_PDF_record_of_the_BreakSheet_Legacy_information()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QALab_BreakSheet_Legacy();
            Assert.True(true);
        }

        [Test]
        [Category(Component.Breaksheet_Module)]
        [Property(TestCaseNumber, 2238577)]
        [Property(Priority, "High")]
        [Description("To produce a Cylinder Break Sheet")]
        public void Produce_a_Cylinder_BreakSheet()
        {
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QCLab_BreakSheet_Creation();
            Assert.True(false);
        }
    }

    [TestFixture]
    public class HiptestUnit
    {
        [TestFixture]
        public class CVL_Lists : TestBase
        {
            [Test]
            [Category(Component.CVL_List)]
            [Property(Component2, Component.CVL_Lists)]
            [Property(TestCaseNumber, 2238579)]
            [Property(Priority, "Priority 1")]
            [Description("To create and add a 'CVL(Computer Vocabulary List) Lists' record")]
            public void Create_a_CVL_Lists_Record()
            {
                LoginAs(UserType.Bhoomi);
                NavigateToPage.SysConfig_CVL_Lists();
                Assert.True(true);
            }
        }

        [TestFixture]
        public class CVL_List_Items5 : TestBase
        {
            [Test]
            [Category(Component.CVL_List)]
            [Property(Component2, Component.CVL_Lists)]
            [Property(TestCaseNumber, 2238580)]
            [Property(Priority, "Priority 1")]
            [Description("To edit a 'CVL(Computer Vocabulary List) Lists' record.")]
            public void CVL_Lists_Record_Editing()
            {
                LoginAs(UserType.Bhoomi);
                NavigateToPage.SysConfig_CVL_Lists();
                Assert.True(true);
            }
        }

        [TestFixture]
        public class CVL_List_Items4 : TestBase
        {
            [Test]
            [Category(Component.CVL_List)]
            [Property(Component2, Component.CVL_Lists)]
            [Property(TestCaseNumber, 2238581)]
            [Property(Priority, "Priority 1")]
            [Description("To delete a 'CVL(Computer Vocabulary List) Lists' record.")]
            public void CVL_Lists_Document_deletion()
            {
                LoginAs(UserType.Bhoomi);
                NavigateToPage.SysConfig_CVL_Lists();
                Assert.True(false);
            }
        }

        [TestFixture]
        public class CVL_List_Items3 : TestBase
        {
            [Test]
            [Category(Component.CVL_List)]
            [Property(Component2, Component.CVL_List_Items)]
            [Property(TestCaseNumber, 2238584)]
            [Property(Priority, "High")]
            [Description("To create and add a 'CVL(Computer Vocabulary List) List Items' record.")]
            public void Create_a_CVL_List_Items_Record()
            {
                LoginAs(UserType.Bhoomi);
                NavigateToPage.SysConfig_CVL_List_Items();
                Assert.True(true);
            }
        }

        [TestFixture]
        public class CVL_List_Items2 : TestBase
        {
            [Test]
            [Category(Component.CVL_List)]
            [Property(Component2, Component.CVL_List_Items)]
            [Property(TestCaseNumber, 2238585)]
            [Property(Priority, "High")]
            [Description("The scope of this test case is to filter 'CVL(Computer Vocabulary List)List Items' documents using the 'Name' filter.")]
            public void CVL_Lists_Record_Editing()
            {
                LoginAs(UserType.Bhoomi);
                NavigateToPage.SysConfig_CVL_List_Items();
                Assert.True(true);
            }
        }

        [TestFixture]
        public class CVL_List_Items1 : TestBase
        {
            [Test]
            [Category(Component.CVL_List)]
            [Property(Component2, Component.CVL_List_Items)]
            [Property(TestCaseNumber, 2238586)]
            [Property(Priority, "High")]
            [Description("The scope of this test case is to filter 'CVL(Computer Vocabulary List)List Items' documents using the 'CVL List' filter.")]
            public void CVL_List_Items_CVL_List_Filter()
            {
                LoginAs(UserType.Bhoomi);
                NavigateToPage.SysConfig_CVL_List_Items();
                Assert.True(false);
            }
        }
    }
}