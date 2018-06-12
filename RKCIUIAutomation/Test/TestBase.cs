using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.PageObjects.Project;
using RKCIUIAutomation.Page.PageObjects.RMCenter.Search;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;

namespace RKCIUIAutomation.Test
{
    public class TestBase : PageBase
    {
        public TestBase(){}
        public TestBase(IWebDriver driver) => Driver = driver;

        public static IPageNavigation NavigateToPage => PageNavigation.SetClass<IPageNavigation>();
        public static ISearch RMCenter_SearchPage => Search.SetClass<ISearch>();


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



        public static T SetClass<T>() => (T)SetPageClassBasedOnTenant();
        private static object SetPageClassBasedOnTenant()
        {
            var instance = new Search_Impl();

            if (projectName == ProjectName.GLX)
            {
                LogInfo("###### using Search_GLX instance");
                instance = new Search_GLX();
            }
            else if (projectName == ProjectName.I15Tech)
            {
                LogInfo("###### using Search_I15Tech instance");
                instance = new Search_I15Tech();
            }
            else
                LogInfo("###### using Search_Impl (Common) instance");

            return instance;
        }


    }
}
