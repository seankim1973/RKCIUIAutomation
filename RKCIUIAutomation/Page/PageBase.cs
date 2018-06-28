using OpenQA.Selenium;

namespace RKCIUIAutomation.Page
{
    public class PageBase : Action
    {
        public void ClickLoginLink() => ClickElement(By.XPath("//a[text()=' Login']"));
        public void ClickLogoutLink() => ClickElement(By.XPath("//a[text()=' Log out']"));
    }
}
