using NUnit.Framework;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Test.CorrespondenceLog
{
    [Parallelizable]
    [TestFixture]
    public class RMCenter_CorrespondenceLog_Test : TestBase
    {
        [Test]
        [Category(Component.Correspondence_Log)]
        //[Property(Component2, Component.Correspondence_Log)]
        [Property(TestCaseNumber, 2187523)]
        [Property(Priority, "High")]
        [Description("To keep track of Correspondence info (incoming/outgoing) shared by RK technician to client.")]
        public void CorresondenceLog_End_To_End()
        {
            ProjCorrespondenceLog.LogintoCorrespondenceLogPage(UserType.TransmissionsGeneral);
            ProjCorrespondenceLog.CreateNewAndPopulateFields();
        }
    }
}
