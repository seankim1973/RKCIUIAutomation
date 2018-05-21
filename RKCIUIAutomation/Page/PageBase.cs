using OpenQA.Selenium;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.Project;

namespace RKCIUIAutomation.Page
{
    public class PageBase : PageRefs
    {
        private static By link_Login = By.XPath("//a[text()=' Login']");
        private static By link_Logout = By.XPath("//a[text()=' Log out']");

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
        public PageRefs()
        {
        }

        private MyDetails _MyDetails { get => new MyDetails(Driver); set { } }
        public MyDetails MyDetailsPage() => _MyDetails;

        private LandingPage _landingPage { get => new LandingPage(); set { } }
        public LandingPage LandingPage() => _landingPage;

        private LoginPage _loginPage { get => new LoginPage(Driver); set { } }
        public LoginPage loginPage => _loginPage;

        private NavMenu _navMenu { get => new NavMenu(Driver); set{ } }
        public NavMenu navigateMenu => _navMenu;

        private NavMenu.ProjectMenu _projectMenu { get => new NavMenu.ProjectMenu(Driver); set { } }
        public NavMenu.ProjectMenu projectMenu => _projectMenu;

    }
}
