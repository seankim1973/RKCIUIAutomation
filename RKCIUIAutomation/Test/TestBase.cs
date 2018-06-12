using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.RMCenter.Search;

namespace RKCIUIAutomation.Test
{
    public class TestBase : PageBase
    {
        public static void LoginAs(UserType user) => LoginPage.LoginUser(user);
        public static IPageNavigation NavigateToPage => PageNavigation.SetClass<IPageNavigation>();
        public static ISearch RMCenter_SearchPage => Search.SetClass<ISearch>();


        private NavMenu _navMenu { get => new NavMenu(Driver); set { } }
        public NavMenu Navigate => _navMenu;


    }
}
