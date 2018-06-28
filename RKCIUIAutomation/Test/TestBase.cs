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
        public TestDetails TestDetails => new TestDetails(driver);
        public void LoginAs(UserType user) => new LoginPage(driver).LoginUser(user);      

        public IPageNavigation NavigateToPage => new PageNavigation().SetClass<IPageNavigation>(driver);

        public ISearch RMCenter_SearchPage => new Search().SetClass<ISearch>(driver);

        public ILinkCoverage LinkCoverage => new LinkCoverage().SetClass<ILinkCoverage>(driver);

    }
}
