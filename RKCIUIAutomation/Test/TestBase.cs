using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.RMCenter.Search;
using System;

namespace RKCIUIAutomation.Test
{
    public class TestBase : PageBase
    {
        public static void LoginAs(UserType user) => LoginPage.LoginUser(user);
        public static IPageNavigation NavigateToPage => PageNavigation_Impl.SetClass<IPageNavigation>();
        public static ISearch RMCenter_SearchPage => Search_Impl.SetClass<ISearch>();

    }
}
