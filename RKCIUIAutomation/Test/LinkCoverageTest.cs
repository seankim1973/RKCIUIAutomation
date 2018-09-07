using NUnit.Framework;
using RKCIUIAutomation.Page.Workflows;

namespace RKCIUIAutomation.Test.LinkCoverage
{
    [TestFixture]
    public class VerifyProjectConfigurationMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Project Configuration Menu only")]
        public void NavigateToVerifyProjectConfigurationMenu() => LinkCoverageWF._NavigateToVerifyProjectConfigurationMenu();
    }

    [TestFixture]
    public class VerifyQALabMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(Component2, Component.Other)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QA Lab Menu only")]
        public void NavigateToVerifyQALabMenu() => LinkCoverageWF._NavigateToVerifyQALabMenu();
    }

    [TestFixture]
    public class VerifyQARecordControlMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QA Record Control Menu only")]
        public void NavigateToVerifyQARecordControlMenu() => LinkCoverageWF._NavigateToVerifyQARecordControlMenu();
    }

    [TestFixture]
    public class VerifyOVMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(Component2, Component.OV_Test)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for OV Menu only")]
        public void NavigateToOVMenu() => LinkCoverageWF._NavigateToOVMenu();
    }

    [TestFixture]
    public class VerifyQAEngineerMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QA Engineer Menu only")]
        public void NavigateToVerifyQAEngineerMenu() => LinkCoverageWF._NavigateToVerifyQAEngineerMenu();
    }

    [TestFixture]
    public class VerifyReportsAndNoticesMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Reports & Notices Menu only")]
        public void NavigateToReportsAndNoticesMenu() => LinkCoverageWF._NavigateToReportsAndNoticesMenu();
    }

    [TestFixture]
    public class VerifyQASearchMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToQASearchMenu() => LinkCoverageWF._NavigateToQASearchMenu();
    }

    [TestFixture]
    public class VerifyQAFieldMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage), Property(Component2,Component.QAField)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify QA Field Menu only")]
        public void NavigateToQAFieldMenu() => LinkCoverageWF._NavigateToQAFieldMenu();
    }

    [TestFixture]
    public class VerifyControlPointMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToControlPointMenu() => LinkCoverageWF._NavigateToControlPointMenu();
    }

    [TestFixture]
    public class VerifyOwnerMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToOwnerMenu() => LinkCoverageWF._NavigateToOwnerMenu();
    }

    [TestFixture]
    public class VerifyMaterialMixCodeMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToMaterialMixCodeMenu() => LinkCoverageWF._NavigateToMaterialMixCodeMenu();
    }

    [TestFixture]
    public class VerifyRMCenterMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToRMCenterMenu() => LinkCoverageWF._NavigateToRMCenterMenu();
    }

    [TestFixture]
    public class VerifyRFIMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToRFIMenu() => LinkCoverageWF._NavigateToRFIMenu();
    }

    [TestFixture]
    public class VerifyQCLabMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(Component2, Component.Other)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QC Lab Menu only")]
        public void NavigateToVerifyQCLabMenu() => LinkCoverageWF._NavigateToVerifyQCLabMenu();
    }

    [TestFixture]
    public class VerifyQCRecordControlMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QC Record Control Menu only")]
        public void NavigateToVerifyQCRecordControlMenu() => LinkCoverageWF._NavigateToVerifyQCRecordControlMenu();
    }

    [TestFixture]
    public class VerifyQCEngineerMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title for QC Engineer Menu only")]
        public void NavigateToVerifyQCEngineerMenu() => LinkCoverageWF._NavigateToVerifyQCEngineerMenu();
    }

    
    [TestFixture]
    public class VerifyQCSearchMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property(TestCaseNumber, "ELVS2222")]
        [Property(Priority, "Priority 1")]
        [Description("Verify Page Title forQC Search Menu only")]
        public void NavigateToQCSearchMenu() => LinkCoverageWF._NavigateToQCSearchMenu();
    }
}
