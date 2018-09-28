using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Page.Workflows;

namespace RKCIUIAutomation.Test
{
    public class TestBase : TestUtils
    {
        internal const string Component2 = "Component2";
        internal const string TestCaseNumber = "TC#";
        internal const string Priority = "Priority";

        public TestDetails TestDetails => new TestDetails(Driver);

        private ILoginPage LoginPage => new LoginPage().SetClass<ILoginPage>(Driver);

        public void LoginAs(UserType user) => LoginPage.LoginUser(user);

        public TableHelper TableHelper => new TableHelper(Driver);

        public IPageNavigation NavigateToPage => new PageNavigation().SetClass<IPageNavigation>(Driver);

        public ISearch RMCenter_SearchPage => new Search().SetClass<ISearch>(Driver);

        public ILinkCoverageWF LinkCoverageWF => new LinkCoverageWF().SetClass<ILinkCoverageWF>(Driver);

        public IDesignDocument DesignDocCommentReview => new DesignDocument().SetClass<IDesignDocument>(Driver);

        public IDesignDocumentWF DesignDocCommentReviewWF => new DesignDocumentWF().SetClass<DesignDocumentWF>(Driver);
    }
}