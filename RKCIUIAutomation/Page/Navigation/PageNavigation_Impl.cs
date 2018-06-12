using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.Navigation
{
    public class PageNavigation_Impl : PageNavigation, IPageNavigation
    {
        //Common page objects and workflows are inherited from abstract class
    }

    public class PageNavigation_SGWay : PageNavigation_Impl
    {
        public override void Qms_Document() => Navigate.Menu(NavMenu.QARecordControl.Menu.Qms_Document);
    }


    public class PageNavigation_SH249 : PageNavigation_Impl
    {
        public override void Qms_Document() => Navigate.Menu(NavMenu.Project.Menu.QMS_Documents);
    }

}
