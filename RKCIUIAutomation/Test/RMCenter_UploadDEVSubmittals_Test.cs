using NUnit.Framework;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using static RKCIUIAutomation.Config.ProjectProperties;

namespace RKCIUIAutomation.Test.UploadSubmittals
{
    [Parallelizable]
    [TestFixture]
    public class RMCenter_UploadDEVSubmittals_Test : TestBase
    {
        [Test]
        [Category(Component.Correspondence_Log)]
        [Property(TestBase.TestCaseNumber, 2187523)]
        [Property(TestBase.Priority, "High")]
        [Description("To keep track of Correspondence info (incoming/outgoing) shared by RK technician to client.")]
        public void QASubmittals_End_To_End()
        {
            //UploadQASubmittal.LogintoQASubmittal(UserType.Bhoomi);
            ClickSubmitForward();
        }
    }
}
