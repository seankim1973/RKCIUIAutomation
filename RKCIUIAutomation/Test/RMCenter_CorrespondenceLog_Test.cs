using NUnit.Framework;
using RKCIUIAutomation.Config;
using static RKCIUIAutomation.Base.Factory;


namespace RKCIUIAutomation.Test.CorrespondenceLog
{
    [Parallelizable]
    [TestFixture]
    public class RMCenter_CorrespondenceLog_Test : TestBase
    {
        [Test]
        [Category(Component.Correspondence_Log)]
        [Property(TestCaseNumber, 2187523)]
        [Property(Priority, "High")]
        [Description("To keep track of Correspondence info (incoming/outgoing) shared by RK technician to client.")]
        public void CorresondenceLog_End_To_End()
        {
            ProjCorrespondenceLog.LogintoCorrespondenceLogPage(UserType.TransmissionsGeneral);
            string transmittalNumber = ProjCorrespondenceLog.CreateNewAndPopulateFields();

            AddAssertionToList(ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayed(transmittalNumber), "VerifyTransmittalLogIsDisplayed");
            AddAssertionToList(ProjCorrespondenceLog.VerifyTableColumnValues(), "VerifyTableColumnValues");
            ProjCorrespondenceLog.ClickViewBtnForTransmissionsRow();
            AddAssertionToList(ProjCorrespondenceLog.VerifyTransmissionDetailsPageValues(), "VerifyTransmissionDetailsPageValues");
            ProjCorrespondenceLog.VerifyTransmissionDetailsPageValuesInRemainingTableTabs(transmittalNumber);
            AssertAll();
        }

        [Test]
        [Category(Component.Correspondence_Log)]
        [Property(TestCaseNumber, 2187525)]
        [Property(Priority, "High")]
        [Description("Verify filtering of 'Correspondence Log / Transmittal Log' documents table by column names.")]
        public void CorresondenceLog_Filters()
        {
            ProjCorrespondenceLog.LogintoCorrespondenceLogPage(UserType.TransmissionsGeneral);
            string transmittalNumber = ProjCorrespondenceLog.CreateNewAndPopulateFields();
            ProjCorrespondenceLog.VerifyTransmittalLogIsDisplayedByGridColumnFilter();
            ProjCorrespondenceLog.VerifyTransmissionDetailsGridFilterInRemainingTableTabs(transmittalNumber);
            AssertAll();
        }

        [Test]
        [Category(Component.Correspondence_Log)]
        [Property(TestCaseNumber, 2187525)]
        [Property(Priority, "High")]
        [Description("Search behavior validation")]
        public void CorrespondenceLog_Search()
        {
            ProjCorrespondenceLog.LogintoCorrespondenceLogPage(UserType.TransmissionsGeneral);
            //CreateNewAndPopulateFields() method will create a new Transmission entry and build data to be used in RM Center Search - refer to method summary
            string transmittalNumber = ProjCorrespondenceLog.CreateNewAndPopulateFields();
            var tenantKVPairsList = ProjCorrespondenceLog.GetTenantEntryFieldKVPairsList();
            NavigateToPage.RMCenter_Search();
            RMCenterSearch.VerifySearchResultByCriteria(transmittalNumber, tenantKVPairsList);
            AssertAll();
        }
    }
}
