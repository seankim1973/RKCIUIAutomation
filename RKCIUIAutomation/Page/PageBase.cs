using OpenQA.Selenium;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.Project;
using RKCIUIAutomation.Page.PageObjects.RMCenter;

namespace RKCIUIAutomation.Page
{
    public class PageBase : PageRefs
    {
        private static readonly By link_Login = By.XPath("//a[text()=' Login']");
        private static readonly By link_Logout = By.XPath("//a[text()=' Log out']");

        public PageBase() : base()
        {
        }

        public LoginPage ClickLoginLink()
        {
            ClickElement(link_Login);
            return new LoginPage(Driver);
        }

        public LandingPage ClickLogoutLink()
        {
            ClickElement(link_Logout);
            return new LandingPage();
        }



    }

    public class PageRefs : PageHelper
    {
        public PageRefs(){ }

        private MyDetails _MyDetails { get => new MyDetails(Driver); set { } }
        public MyDetails MyDetailsPage() => _MyDetails;

        private LandingPage _landingPage { get => new LandingPage(); set { } }
        public LandingPage LandingPage() => _landingPage;

        private LoginPage _loginPage { get => new LoginPage(Driver); set { } }
        public LoginPage LoginPg => _loginPage;

        private NavMenu _navMenu { get => new NavMenu(Driver); set{ } }
        public NavMenu Navigate => _navMenu;

        private NavEnums _navEnums { get => new NavEnums(Driver); set { } }
        public NavEnums MenuEnums => _navEnums;

        private RMCenter _rmCenter { get => new RMCenter(Driver); set { } }
        public RMCenter RMCenter => _rmCenter;

    }
}
