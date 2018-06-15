using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.Navigation
{
    /// <summary>
    /// Common pageObjects and workflows are inherited from abstract _Impl class
    /// </summary>
    public class PageNavigation : PageNavigation_Impl//, IPageNavigation
    { }

    public class PageNavigation_SGWay : PageNavigation
    {
        public override void Qms_Document() => Navigate.Menu(NavMenu.QARecordControl.Menu.Qms_Document);
    }


    public class PageNavigation_SH249 : PageNavigation
    {
        public override void Qms_Document() => Navigate.Menu(NavMenu.Project.Menu.QMS_Documents);
    }

}
