using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.GeneralNCR;

namespace RKCIUIAutomation.Page.Workflows
{
    public class QaRcrdCtrl_GeneralNCR_WF : QaRcrdCtrl_GeneralNCR_WF_Impl
    {
        public QaRcrdCtrl_GeneralNCR_WF()
        {
        }

        public QaRcrdCtrl_GeneralNCR_WF(IWebDriver driver) => this.Driver = driver;
    }

    public interface IQaRcrdCtrl_GeneralNCR_WF
    {
        /// <summary>
        /// Verifies Required field error labels in a new document then populates required fields and clicks Save & Forward button
        /// <para>Returns unique NCR document description string value</para>
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        string CreateAndSaveForwardNCRDocument(UserType user);

        /// <summary>
        /// Verifies a document is shown in 'Revise' tab, after clicking Revise button for a document in the 'Review' tab.
        /// <para>Verifies a document is shown in 'To Be Closed' tab, after clicking Save & Fwd button for a document in the 'Review' tab.</para>
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ncrDescription"></param>
        void ReviewAndApproveNCRDocument(UserType user, string ncrDescription);

        void DisapproveCloseDocument(UserType user, string ncrDescription);
    }

    public abstract class QaRcrdCtrl_GeneralNCR_WF_Impl : TestBase, IQaRcrdCtrl_GeneralNCR_WF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IQaRcrdCtrl_GeneralNCR_WF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IQaRcrdCtrl_GeneralNCR_WF instance = new QaRcrdCtrl_GeneralNCR_WF(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_SGWay instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_SH249 instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_Garnet instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_GLX instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_I15South instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_I15Tech instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                LogInfo($"###### using QaRcrdCtrl_GeneralNCR_WF_LAX instance ###### ");
                instance = new QaRcrdCtrl_GeneralNCR_WF_LAX(driver);
            }
            return instance;
        }

        public virtual string CreateAndSaveForwardNCRDocument(UserType user)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_NCR();
            Assert.True(VerifyPageTitle("List of NCR Reports"));
            QaRcrdCtrl_GeneralNCR.ClickBtn_New();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyReqFieldErrorLabelsForNewDoc());
            QaRcrdCtrl_GeneralNCR.PopulateRequiredFieldsAndSaveForward();
            return QaRcrdCtrl_GeneralNCR.GetNCRDocDescription();
        }

        public virtual void ReviewAndApproveNCRDocument(UserType user, string ncrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_NCR();
            Assert.True(VerifyPageTitle("List of NCR Reports"));
            QaRcrdCtrl_GeneralNCR.ClickTab_CQM_Review();
            QaRcrdCtrl_GeneralNCR.FilterDescription(ncrDescription);
            TableHelper.ClickEditBtnForRow();

            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyReqFieldErrorLabelForTypeOfNCR());
            QaRcrdCtrl_GeneralNCR.SelectRdoBtn_TypeOfNCR_Level1();
            QaRcrdCtrl_GeneralNCR.ClickBtn_Approve();
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Resolution_Disposition, ncrDescription));
            QaRcrdCtrl_GeneralNCR.FilterDescription(ncrDescription);
            TableHelper.ClickEditBtnForRow();

            //>>>WORKFLOW for (Concession Request DDList) Return To Conformance
            //click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.SelectDDL_ConcessionRequest_ReturnToConformance();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Verification_and_Closure, ncrDescription));
            //click Save&Fwd button and verify required field error label is shown
            //select return to conformance --> workflow for Kick back button <-- verify in Resolution/Disposition tab
            //select return to conformance --> workflow for Revise button <-- verify in Resolution/Disposition tab
            //select return to conformance --> workflow for Close button <-- verify in AllNCR tab

            //TODO - move to a new test case for ConcessionRequest_ConcessionDeviation
            //>>>WORKFLOW for (Concession Request DDList) Concession Deviation
            //click Save&Fwd button and verify required field error label is shown for Concession Request DDList
            QaRcrdCtrl_GeneralNCR.SelectDDL_ConcessionRequest_ConcessionDeviation();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.Developer_Concurrence, ncrDescription));
            TableHelper.ClickEditBtnForRow();
            //click Save&Fwd button and verify required field error label is shown (RecordEngineer_SignBtn, EngOfRecord, EngApprovalDate, ApprovalRadioBtn)
            QaRcrdCtrl_GeneralNCR.ClickBtn_Sign_RecordEngineer();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SignaturePanel_OK();
            QaRcrdCtrl_GeneralNCR.EnterEngineerOfRecord();//Enter Engineer_of_Record field
            QaRcrdCtrl_GeneralNCR.EnterRecordEngineerApprovedDate();//Enter RecordEngineerApprovedDate field
            QaRcrdCtrl_GeneralNCR.SelectRdoBtn_EngOfRecordApproval_Yes();//Select Approval RdoBtn (Yes/No)
            QaRcrdCtrl_GeneralNCR.ClickBtn_SaveForward();
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.DOT_Approval, ncrDescription));
            TableHelper.ClickEditBtnForRow();
            //click Save&Fwd button and verify required field error label is shown(Owner_SignBtn, DOTReview, OwnerApprovalDate, OwnerApprovalRdoBtn)
            QaRcrdCtrl_GeneralNCR.ClickBtn_Sign_Owner();
            QaRcrdCtrl_GeneralNCR.ClickBtn_SignaturePanel_OK();
            QaRcrdCtrl_GeneralNCR.EnterOwnerReview();//Enter Owner field
            QaRcrdCtrl_GeneralNCR.EnterOwnerApprovedDate();//Enter OwnerApprovedDate field
            QaRcrdCtrl_GeneralNCR.SelectRdoBtn_OwnerApproval_Yes();//Select Approval RdoBtn (Yes/No)
            Assert.True(QaRcrdCtrl_GeneralNCR.VerifyNCRDocIsDisplayed(TableTab.All_NCRs, ncrDescription));
        }

        public virtual void DisapproveCloseDocument(UserType user, string ncrDescription)
        {
            LoginAs(user);
            NavigateToPage.QARecordControl_General_NCR();
            Assert.True(VerifyPageTitle("List of NCR Reports"));
            QaRcrdCtrl_GeneralNCR.ClickTab_CQM_Review();
            QaRcrdCtrl_GeneralNCR.FilterDescription(ncrDescription);
            TableHelper.ClickEditBtnForRow();
            QaRcrdCtrl_GeneralNCR.ClickBtn_DisapproveClose();
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_GLX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_GLX(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_Garnet : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_SH249 : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_SH249(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_SGWay : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_SGWay(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_I15Tech : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_I15South : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class QaRcrdCtrl_GeneralNCR_WF_LAX : QaRcrdCtrl_GeneralNCR_WF
    {
        public QaRcrdCtrl_GeneralNCR_WF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}