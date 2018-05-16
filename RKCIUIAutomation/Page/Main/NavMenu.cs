using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.Project;
using static RKCIUIAutomation.Page.Project.MyDetails;
using static RKCIUIAutomation.Page.Main.NavMenu.MouseOverMenu;
using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.Main
{
    public class NavMenu : PageBase, INavMenu
    {
        public NavMenu()
        {
                
        }
        public NavMenu(IWebDriver driver)
        {

        }
        internal class MouseOverMenu : NavMenu
        {
            public MouseOverMenu()
            {

            }
            public MouseOverMenu(IWebDriver driver) : base(driver)
            {

            }
            public static MouseOverMenu MouseOver { get => new MouseOverMenu(Driver); set { } }

            internal void Project() => HoverOverElement(MainNav_By.Project);
            internal void QALab() => HoverOverElement(MainNav_By.QA_Lab);
            internal void QARecordControl() => HoverOverElement(MainNav_By.QA_RecordControl);
            internal void QAEngineer() => HoverOverElement(MainNav_By.QA_Engineer);
            internal void ReportsNotices() => HoverOverElement(MainNav_By.Reports_Notices);
            internal void QASearch() => HoverOverElement(MainNav_By.QA_Search);
            internal void QAField() => HoverOverElement(MainNav_By.QA_Field);
            internal void Owner() => HoverOverElement(MainNav_By.Owner);
            internal void MaterialMixCodes() => HoverOverElement(MainNav_By.Material_Mix_Codes);
            internal void RMCenter() => HoverOverElement(MainNav_By.RM_Center);
            internal void QAInbox() => HoverOverElement(MainNav_By.QA_Inbox);
            internal void DOTInbox() => HoverOverElement(MainNav_By.DOT_Inbox);
            internal void OwnerInbox() => HoverOverElement(MainNav_By.Owner_Inbox);
            internal void DevInbox() => HoverOverElement(MainNav_By.Dev_Inbox);
            internal void RFI() => HoverOverElement(MainNav_By.RFI);
            internal void ELVIS() => HoverOverElement(MainNav_By.ELVIS);
            internal void Proj_Administration() => HoverOverElement(Project_By.Administration);
            internal void Proj_Admin_UserManagement() => HoverOverElement(Project_By.Administration_By.User_Management);
            internal void Proj_Admin_SystemConfiguration() => HoverOverElement(Project_By.Administration_By.System_Configuration);
            internal void Proj_Admin_SysConfig_Equipment() => HoverOverElement(Project_By.Administration_By.SystemConfiguration_By.Equipment);
            internal void Proj_Admin_SysConfig_GradeManagement() => HoverOverElement(Project_By.Administration_By.SystemConfiguration_By.Grade_Management);
        }

        public MyDetails NavigateToMyDetails()
        {
            MouseOver.Project();
            ClickElement(Project_By.My_Details);
            return MyDetailsPage;
        }

        public QMSDocuments NavigateToQMSDocuments()
        {
            MouseOver.Project();
            ClickElement(Project_By.Qms_Document);
            return new QMSDocuments();
        }
    }
}
