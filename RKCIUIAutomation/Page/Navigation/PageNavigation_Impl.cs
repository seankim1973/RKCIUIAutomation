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
        //public PageNavigation_Impl(){}
        //public PageNavigation_Impl(IWebDriver driver) => Driver = driver;

    }

    public class PageNavigation_SGWay : PageNavigation_Impl
    {
        //public PageNavigation_SGWay() { }
        //public PageNavigation_SGWay(IWebDriver driver) => Driver = driver;

        public override void Qms_Document() => Navigate.Menu(NavMenu.QARecordControl.Menu.Qms_Document);
    }


    public class PageNavigation_SH249 : PageNavigation_Impl
    {
        //public PageNavigation_SH249() { }
        //public PageNavigation_SH249(IWebDriver driver) => Driver = driver;

        public override void Qms_Document() => Navigate.Menu(NavMenu.Project.Menu.QMS_Documents);

    }

}
