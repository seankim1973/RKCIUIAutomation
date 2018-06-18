using NUnit.Framework;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Config.ProjectProperties;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class TenantNavMenu : TestBase
    {
        [Test]
        [Category(Component.Other)]
        [Property("Component2", Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Generate Page Navigation Menu Names and URLs")]
        public void GetSiteNavigation()
        {
            LoginAs(UserType.Bhoomi);
            TestUtils.LoopThroughNavMenu();
        }


        [Test]
        [Category(Component.Other)]
        [Property("Component2", Component.Link_Coverage)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Generate Page Title for Navigation Menu Pages")]
        public void GetPageTitles()
        {
            LoginAs(UserType.Bhoomi);
            TestUtils.GetPageTitleForNavPages();
        }
    }
}
