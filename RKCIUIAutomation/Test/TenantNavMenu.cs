using NUnit.Framework;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Config.ProjectProperties;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class TenantNavMenu : TestBase
    {
        //[Test]
        [Category(Component.Other)]
        [Property("TC#", "ELVS2222")]
        [Property("Priority", "Priority 1")]
        [Description("Verify Component Name")]
        public void GetSiteNavigation()
        {
            LogInfo($"Other component test - This test should run");
            LoginPg.LoginUser(UserType.Bhoomi);
            TestUtils.LoopThroughNavMenu();
        }
    }
}
