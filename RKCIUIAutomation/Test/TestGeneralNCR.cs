using NUnit.Framework;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Test
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
            LoginAs(UserType.Bhoomi);
            NavigateToPage.QARecordControl_General_NCR();
            GeneralNCR.ClickTab_Creating();
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
        }
    }

}
