using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects
{
    public class Project : PageBase
    {
        public class MyDetails : Project
        {
            public MyDetails()
            { }
            public MyDetails(IWebDriver driver) => Driver = driver;
            public static MyDetails MyDetailsPage { get => new MyDetails(Driver); set { } }

        }

        public class QmsDocuments
        {
            public QmsDocuments()
            { }
            public QmsDocuments(IWebDriver driver) => Driver = driver;
            public static QmsDocuments QmsDocumentsPage { get => new QmsDocuments(Driver); set { } }
        }

        public class Administration
        {
            public class ProjectDetails
            {
                public ProjectDetails()
                { }
                public ProjectDetails(IWebDriver driver) => Driver = driver;
                public static ProjectDetails ProjectDetailsPage { get => new ProjectDetails(Driver); set { } }
            }
        }
        

    }
}
