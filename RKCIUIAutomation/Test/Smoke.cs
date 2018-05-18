using System;
using NUnit.Framework;
using RKCIUIAutomation.Page.Main;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class Smoke : TestBase
    {
        [Test, Property("TC#", "ELVS2345")]
        [Category("Smoke")][Category("Cat2")]
        [Description("Verify user can login successfully using project - admin account")]
        public void VerifyUserCanLogin_ProjAdmin()
        {
            LoginPage().LoginUser(Config.UserType.ProjAdmin);
        }


        [Test, Property("TC#","ELVS1234")]
        [Category("Smoke")]
        [Description("Verify user can login successfully using project - user account")]
        public void VerifyUserCanLogin_ProjUser()
        {
            LoginPage().LoginUser(Config.UserType.ProjAdmin);
        }


        [Test, Property("TC#", "ELVS3456")]
        [Category("Regression")]
        [Description("Verify user can login successfully using project - user account")]
        public void GenericTest()
        {
            LoginPage().LoginUser(Config.UserType.ProjAdmin);
        }

    }

    internal class SmokeTestSuiteAttribute : Attribute
    {
    }
}
