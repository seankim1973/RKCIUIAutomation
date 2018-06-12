using OpenQA.Selenium;
using RKCIUIAutomation.Base;
using static RKCIUIAutomation.Page.Action;

namespace RKCIUIAutomation.Page
{
    public class PageBase : BaseClass
    {
        public static void ClickLoginLink() => ClickElement(By.XPath("//a[text()=' Login']"));
        public static void ClickLogoutLink() => ClickElement(By.XPath("//a[text()=' Log out']"));
    }
}
