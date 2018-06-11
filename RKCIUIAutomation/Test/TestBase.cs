using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.PageObjects.Project;

namespace RKCIUIAutomation.Test
{
    public class TestBase : PageBase
    {
        public TestBase(){}

        private MyDetails _MyDetails { get => new MyDetails(Driver); set { } }
        public MyDetails MyDetailsPage() => _MyDetails;

        private LandingPage _landingPage { get => new LandingPage(); set { } }
        public LandingPage LandingPage() => _landingPage;

        private LoginPage _loginPage { get => new LoginPage(Driver); set { } }
        public LoginPage LoginPg => _loginPage;

        private NavMenu _navMenu { get => new NavMenu(Driver); set { } }
        public NavMenu Navigate => _navMenu;

        private RMCenter _rmCenter { get => new RMCenter(Driver); set { } }
        public RMCenter RMCenter => _rmCenter;
    }
}
