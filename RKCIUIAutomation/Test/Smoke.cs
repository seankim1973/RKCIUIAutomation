using NUnit.Framework;
using RKCIUIAutomation.Page.Main;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class Smoke : TestBase
    {
        [Test]
        [Category("Smoke")]
        public void VerifyUserCanLogin()
        {
            //LoginPage loginPage = new LoginPage();
            LoginPage().LoginUser(Config.UserType.ProjAdmin);
        }
    }
}
