using OpenQA.Selenium;
using RKCIUIAutomation.Page.Main;
using RKCIUIAutomation.Page.Project;

namespace RKCIUIAutomation.Page
{
    public class PageBase : PageRefs
    {
        private static By link_Login = By.XPath("//a[text()=' Login']");
        private static By link_Logout = By.XPath("//a[text()=' Log out']");

        public PageBase() : base(Driver)
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
        public PageRefs(IWebDriver driver)
        {
        }

        private MyDetails _MyDetails { get => new MyDetails(Driver); set { } }
        // MyDetails MyDetails() => _MyDetails;

        private LandingPage _landingPage { get => new LandingPage(); set { } }
        public LandingPage LandingPage() => _landingPage;

        private LoginPage _loginPage { get => new LoginPage(Driver); set { } }
        public LoginPage LoginPage() => _loginPage;
    }
}
