using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Page.PageObjects.RMCenter;

namespace RKCIUIAutomation.Test
{
    public class TestBase : TestUtils
    {
        //private LoginPage _loginPage;
        public void LoginAs(UserType user) => new LoginPage(driver).LoginUser(user);
        

        //private PageNavigation_Impl PageNavigationImpl = new PageNavigation();
        public IPageNavigation NavigateToPage => new PageNavigation(driver).SetClass<IPageNavigation>();

        //private Search_Impl SearchPageImpl = new Search();
        public ISearch RMCenter_SearchPage => new Search(driver).SetClass<ISearch>();

        public TestDetails TestDetails => new TestDetails(driver);

        public ILinkCoverage LinkCoverage => new LinkCoverage(driver).SetClass<ILinkCoverage>();
    }
}
