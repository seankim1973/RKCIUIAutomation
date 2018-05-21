using OpenQA.Selenium;
using static RKCIUIAutomation.Page.PageObjects.Project;

namespace RKCIUIAutomation.Page.Navigation
{
    public class NavMenu : PageBase
    {
        public NavMenu()
        { }
        public NavMenu(IWebDriver driver) => Driver = driver;



        public class ProjectMenu : NavMenu
        {
            public ProjectMenu()
            { }
            public ProjectMenu(IWebDriver driver) => Driver = driver;

            internal void HoverProject() => Hover(MainNav_By.Project);
            internal void HoverAdministration() => Hover(Project_By.Administration);
            internal void HoverUserManagement() => Hover(Project_By.Administration_By.User_Management);
            internal void HoverSystemConfiguration() => Hover(Project_By.Administration_By.System_Configuration);
            internal void HoverSysConfig_Equipment() => Hover(Project_By.Administration_By.SystemConfiguration_By.Equipment);
            internal void HoverSysConfig_GradeManagement() => Hover(Project_By.Administration_By.SystemConfiguration_By.Grade_Management);

            public MyDetails NavigateToMyDetails()
            {
                HoverAndClick(MainNav_By.Project, Project_By.My_Details);
                return new MyDetails();
            }

            public QmsDocuments NavigateToQmsDocuments()
            {
                HoverAndClick(MainNav_By.Project, Project_By.Qms_Document);
                return new QmsDocuments();
            }

            //public Project.Administration.ProjectDetails NavigateToProjectDetails()
            //{
            //    HoverProject();
            //    HoverAndClick(Project_By.Administration, Project_By.Administration_By.Project_Details);
            //    return new Project.Administration.ProjectDetails();
            //}
        }
        






        internal void QALab() => Hover(MainNav_By.QA_Lab);
        internal void QARecordControl() => Hover(MainNav_By.QA_RecordControl);
        internal void QAEngineer() => Hover(MainNav_By.QA_Engineer);
        internal void ReportsNotices() => Hover(MainNav_By.Reports_Notices);
        internal void QASearch() => Hover(MainNav_By.QA_Search);
        internal void QAField() => Hover(MainNav_By.QA_Field);
        internal void Owner() => Hover(MainNav_By.Owner);
        internal void MaterialMixCodes() => Hover(MainNav_By.Material_Mix_Codes);
        internal void RMCenter() => Hover(MainNav_By.RM_Center);
        internal void QAInbox() => Hover(MainNav_By.QA_Inbox);
        internal void DOTInbox() => Hover(MainNav_By.DOT_Inbox);
        internal void OwnerInbox() => Hover(MainNav_By.Owner_Inbox);
        internal void DevInbox() => Hover(MainNav_By.Dev_Inbox);
        internal void RFI() => Hover(MainNav_By.RFI);
        internal void ELVIS() => Hover(MainNav_By.ELVIS);

    }
}
