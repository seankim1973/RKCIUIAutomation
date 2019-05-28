using NUnit.Framework;
using RKCIUIAutomation.Page.Workflows;
using static RKCIUIAutomation.Base.Factory;


namespace RKCIUIAutomation.Test.LinkCoverage
{
    [TestFixture]
    public class VerifyProjectConfigurationMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Project Configuration Menu only")]
        public void NavigateToVerifyProjectConfigurationMenu() => WF_LinkCoverage._NavigateToVerifyProjectConfigurationMenu();
    }

    [TestFixture]
    public class VerifyQALabMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(Component2, Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QA Lab Menu only")]
        public void NavigateToVerifyQALabMenu() => WF_LinkCoverage._NavigateToVerifyQALabMenu();
    }

    [TestFixture]
    public class VerifyQARecordControlMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QA Record Control Menu only")]
        public void NavigateToVerifyQARecordControlMenu() => WF_LinkCoverage._NavigateToVerifyQARecordControlMenu();
    }

    [TestFixture]
    public class VerifyOVMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(Component2, Component.OV_Test)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for OV Menu only")]
        public void NavigateToOVMenu() => WF_LinkCoverage._NavigateToOVMenu();
    }

    [TestFixture]
    public class VerifyQAEngineerMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QA Engineer Menu only")]
        public void NavigateToVerifyQAEngineerMenu() => WF_LinkCoverage._NavigateToVerifyQAEngineerMenu();
    }

    [TestFixture]
    public class VerifyReportsAndNoticesMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Reports & Notices Menu only")]
        public void NavigateToReportsAndNoticesMenu() => WF_LinkCoverage._NavigateToReportsAndNoticesMenu();
    }

    [TestFixture]
    public class VerifyQASearchMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToQASearchMenu() => WF_LinkCoverage._NavigateToQASearchMenu();
    }

    [TestFixture]
    public class VerifyQAFieldMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage), Property(Component2, Component.QAField)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify QA Field Menu only")]
        public void NavigateToQAFieldMenu() => WF_LinkCoverage._NavigateToQAFieldMenu();
    }

    [TestFixture]
    public class VerifyControlPointMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(Component2, Component.Control_Point)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToControlPointMenu() => WF_LinkCoverage._NavigateToControlPointMenu();
    }

    [TestFixture]
    public class VerifyOwnerMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToOwnerMenu() => WF_LinkCoverage._NavigateToOwnerMenu();
    }

    [TestFixture]
    public class VerifyMaterialMixCodeMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToMaterialMixCodeMenu() => WF_LinkCoverage._NavigateToMaterialMixCodeMenu();
    }

    [TestFixture]
    public class VerifyRMCenterMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToRMCenterMenu() => WF_LinkCoverage._NavigateToRMCenterMenu();
    }

    [TestFixture]
    public class VerifyRFIMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToRFIMenu() => WF_LinkCoverage._NavigateToRFIMenu();
    }

    [TestFixture]
    public class VerifyQCLabMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(Component2, Component.Other)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QC Lab Menu only")]
        public void NavigateToVerifyQCLabMenu() => WF_LinkCoverage._NavigateToVerifyQCLabMenu();
    }

    [TestFixture]
    public class VerifyQCRecordControlMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QC Record Control Menu only")]
        public void NavigateToVerifyQCRecordControlMenu() => WF_LinkCoverage._NavigateToVerifyQCRecordControlMenu();
    }

    [TestFixture]
    public class VerifyQCEngineerMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QC Engineer Menu only")]
        public void NavigateToVerifyQCEngineerMenu() => WF_LinkCoverage._NavigateToVerifyQCEngineerMenu();
    }

    [TestFixture]
    public class VerifyQCSearchMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, 2222)]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title forQC Search Menu only")]
        public void NavigateToQCSearchMenu() => WF_LinkCoverage._NavigateToQCSearchMenu();
    }
}