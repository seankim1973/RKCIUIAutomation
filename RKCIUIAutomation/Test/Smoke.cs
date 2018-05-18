using System;
using System.Reflection;
using NUnit.Framework;
using RKCIUIAutomation.Page.Main;

namespace RKCIUIAutomation.Test
{
    [TestFixture]
    public class Smoke : TestBase
    {
        [Test, Property("TC#", "ELVS2345"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify user can login successfully using project - admin account")]
        public void VerifyUserCanLogin_ProjAdmin()
        {
            LoginPage().LoginUser(Config.UserType.ProjAdmin);
        }


        [Test, Property("TC#","ELVS1234"), Property("Priority", "Priority 2")]
        [Category("QA Test")]
        [Description("Verify user can login successfully using project - user account")]
        public void VerifyUserCanLogin_ProjUser()
        {
            LoginPage().LoginUser(Config.UserType.ProjAdmin);
        }


        [Test, Property("TC#", "ELVS3456"), Property("Priority", "Priority 1")]
        [Category("RM Center")]
        [Description("Verify user can login successfully using project - user account")]
        public void GenericTest()
        {
            LoginPage().LoginUser(Config.UserType.ProjAdmin);
        }

    }
}
