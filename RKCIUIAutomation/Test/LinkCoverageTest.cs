using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Config.ProjectProperties;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class LinkCoverageTest : TestBase
    {

        #region NUnit Test Case Methods class

        public class VerifyProjectConfigurationMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for Project Configuration Menu only")]
            public void NavigateToVerifyProjectConfigurationMenu() => LinkCoverage._NavigateToVerifyProjectConfigurationMenu();
        }

        public class VerifyQALabMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("Component2", Component.Other)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for QA Lab Menu only")]
            public void NavigateToVerifyQALabMenu() => LinkCoverage._NavigateToVerifyQALabMenu();
        }

        public class VerifyQARecordControlMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for QA Record Control Menu only")]
            public void NavigateToVerifyQARecordControlMenu() => LinkCoverage._NavigateToVerifyQARecordControlMenu();
        }

        public class VerifyOVMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("Component2", Component.OV_Test)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for OV Menu only")]
            public void NavigateToOVMenu() => LinkCoverage._NavigateToOVMenu();
        }

        public class VerifyQAEngineerMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for QA Engineer Menu only")]
            public void NavigateToVerifyQAEngineerMenu() => LinkCoverage._NavigateToVerifyQAEngineerMenu();
        }

        public class VerifyReportsAndNoticesMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for Reports & Notices Menu only")]
            public void NavigateToReportsAndNoticesMenu() => LinkCoverage._NavigateToReportsAndNoticesMenu();
        }

        public class VerifyQASearchMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for RM Center Menu only")]
            public void NavigateToQASearchMenu() => LinkCoverage._NavigateToQASearchMenu();
        }

        public class VerifyQAFieldMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for RM Center Menu only")]
            public void NavigateToQAFieldMenu() => LinkCoverage._NavigateToQAFieldMenu();
        }

        public class VerifyControlPointMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for Control Point Menu only")]
            public void NavigateToControlPointMenu() => LinkCoverage._NavigateToControlPointMenu();
        }

        public class VerifyOwnerMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for Control Point Menu only")]
            public void NavigateToOwnerMenu() => LinkCoverage._NavigateToOwnerMenu();
        }

        public class VerifyMaterialMixCodeMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for Control Point Menu only")]
            public void NavigateToMaterialMixCodeMenu() => LinkCoverage._NavigateToMaterialMixCodeMenu();
        }

        public class VerifyRMCenterMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for RM Center Menu only")]
            public void NavigateToRMCenterMenu() => LinkCoverage._NavigateToRMCenterMenu();
        }

        [TestFixture]
        public class VerifyRFIMenu : TestBase
        {
            [Test]
            [Category(Component.Link_Coverage)]
            [Property("TC#", "ELVS2222")]
            [Property("Priority", "Priority 1")]
            [Description("Verify Page Title for RM Center Menu only")]
            public void NavigateToRFIMenu() => LinkCoverage._NavigateToRFIMenu();
        }
        #endregion <-- end of Test Case Methods class

    }
}
