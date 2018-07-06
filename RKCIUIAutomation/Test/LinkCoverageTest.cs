using NUnit.Framework;
using RKCIUIAutomation.Page.Workflows;

namespace RKCIUIAutomation.Test.LinkCoverage
{
    [TestFixture]
    [Parallelizable]
    public class VerifyProjectConfigurationMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Project Configuration Menu only")]
        public void NavigateToVerifyProjectConfigurationMenu() => LinkCoverageWF._NavigateToVerifyProjectConfigurationMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyQALabMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("Component2", Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for QA Lab Menu only")]
        public void NavigateToVerifyQALabMenu() => LinkCoverageWF._NavigateToVerifyQALabMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyQARecordControlMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for QA Record Control Menu only")]
        public void NavigateToVerifyQARecordControlMenu() => LinkCoverageWF._NavigateToVerifyQARecordControlMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyOVMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("Component2", Component.OV_Test)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for OV Menu only")]
        public void NavigateToOVMenu() => LinkCoverageWF._NavigateToOVMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyQAEngineerMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for QA Engineer Menu only")]
        public void NavigateToVerifyQAEngineerMenu() => LinkCoverageWF._NavigateToVerifyQAEngineerMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyReportsAndNoticesMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Reports & Notices Menu only")]
        public void NavigateToReportsAndNoticesMenu() => LinkCoverageWF._NavigateToReportsAndNoticesMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyQASearchMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToQASearchMenu() => LinkCoverageWF._NavigateToQASearchMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyQAFieldMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToQAFieldMenu() => LinkCoverageWF._NavigateToQAFieldMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyControlPointMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToControlPointMenu() => LinkCoverageWF._NavigateToControlPointMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyOwnerMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToOwnerMenu() => LinkCoverageWF._NavigateToOwnerMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyMaterialMixCodeMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for Control Point Menu only")]
        public void NavigateToMaterialMixCodeMenu() => LinkCoverageWF._NavigateToMaterialMixCodeMenu();
    }

    [TestFixture]
    [Parallelizable]
    public class VerifyRMCenterMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToRMCenterMenu() => LinkCoverageWF._NavigateToRMCenterMenu();
    }

    [TestFixture]
    [Parallelizable]
    [TestFixture]
    public class VerifyRFIMenu : LinkCoverageWF
    {
        [Test]
        [Category(Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Page Title for RM Center Menu only")]
        public void NavigateToRFIMenu() => LinkCoverageWF._NavigateToRFIMenu();
    }

}
