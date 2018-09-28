using NUnit.Framework;
using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using RKCIUIAutomation.Test;
using System.Threading;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.DesignDocument;

namespace RKCIUIAutomation.Page.Workflows
{
    public class DesignDocumentWF : DesignDocumentWF_Impl
    {
        public DesignDocumentWF()
        {
        }

        public DesignDocumentWF(IWebDriver driver) => this.Driver = driver;
    }

    public interface IDesignDocumentWF
    {
        void CreateDesignDocCommentReviewDocument(UserType user);

        void EnterRegularComment(UserType user);

        void EnterNoComment(UserType user);

        void ForwardResolutionCommentAndCodeForDisagreeResponse();

        void EnterResolutionCommentAndResolutionCodeforDisagreeResponse();
    }

    public abstract class DesignDocumentWF_Impl : TestBase, IDesignDocumentWF
    {
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);

        private IDesignDocumentWF SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IDesignDocumentWF instance = new DesignDocumentWF(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using DesignDocumentWF_SGWay instance ###### ");
                instance = new DesignDocumentWF_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using DesignDocumentWF_SH249 instance ###### ");
                instance = new DesignDocumentWF_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using DesignDocumentWF_Garnet instance ###### ");
                instance = new DesignDocumentWF_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using DesignDocumentWF_GLX instance ###### ");
                instance = new DesignDocumentWF_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using DesignDocumentWF_I15South instance ###### ");
                instance = new DesignDocumentWF_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using DesignDocumentWF_I15Tech instance ###### ");
                instance = new DesignDocumentWF_I15Tech(driver);
            }
            else if (tenantName == TenantName.LAX)
            {
                LogInfo($"###### using DesignDocumentWF_LAX instance ###### ");
                instance = new DesignDocumentWF_LAX(driver);
            }
            return instance;
        }

        //All Tenants
        public virtual void CreateDesignDocCommentReviewDocument(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            Assert.True(VerifyPageTitle("Design Document"));
            DesignDocCommentReview.CreateDocument();
            ClickLogoutLink();
            ClickLoginLink();
        }

        //Garnet and GLX
        public virtual void EnterRegularComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();
        }

        //Garnet and GLX
        public virtual void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.SortTable_Descending();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
            ClickLogoutLink();
            ClickLoginLink();
        }

        public virtual void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            DesignDocCommentReview.SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        //Garnet ?
        public virtual void EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Requires_Resolution();
            DesignDocCommentReview.SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();
            Thread.Sleep(2000);
            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode(); //
            DesignDocCommentReview.ClickBtn_SaveOnly();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
            WaitForPageReady();
        }
    }

    internal class DesignDocumentWF_I15Tech : DesignDocumentWF
    {
        public DesignDocumentWF_I15Tech(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class DesignDocumentWF_I15South : DesignDocumentWF
    {
        public DesignDocumentWF_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class DesignDocumentWF_GLX : DesignDocumentWF
    {
        public DesignDocumentWF_GLX(IWebDriver driver) : base(driver)
        {
        }

        public override void ForwardResolutionCommentAndCodeForDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Pending_Resolution();
            DesignDocCommentReview.SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();
            DesignDocCommentReview.ClickBtn_SaveForward();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            DesignDocCommentReview.ClickTab_Pending_Resolution();
            DesignDocCommentReview.SortTable_Descending();
            ClickEnterBtnForRow();
            WaitForPageReady();
            Thread.Sleep(2000);
            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            Thread.Sleep(2000);
            DesignDocCommentReview.ClickBtn_BackToList();
            WaitForPageReady();
        }
    }

    internal class DesignDocumentWF_Garnet : DesignDocumentWF
    {
        public DesignDocumentWF_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    internal class DesignDocumentWF_SH249 : DesignDocumentWF
    {
        public DesignDocumentWF_SH249(IWebDriver driver) : base(driver)
        {
        }

        public override void EnterRegularComment(UserType user)
        {
            LoginAs(UserType.IQFUser);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();
        }

        public override void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            TableHelper.ClickTab(DesignDocument.TableTab.Requires_Comment);
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
            ClickLogoutLink();
            ClickLoginLink();
        }
    }

    internal class DesignDocumentWF_SGWay : DesignDocumentWF
    {
        public DesignDocumentWF_SGWay(IWebDriver driver) : base(driver)
        {
        }

        public override void EnterRegularComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterRegularCommentAndDrawingPageNo();
            ClickLogoutLink();
            ClickLoginLink();
        }

        public override void EnterNoComment(UserType user)
        {
            LoginAs(user);
            NavigateToPage.RMCenter_Design_Documents();
            DesignDocCommentReview.FilterTableByValue();
            TableHelper.ClickEnterBtnForRow();
            DesignDocCommentReview.EnterNoComment();
            ClickLogoutLink();
            ClickLoginLink();
        }

        public override void EnterResolutionCommentAndResolutionCodeforDisagreeResponse()
        {
            EnterComment(CommentType.CommentResolutionInput);
            DesignDocCommentReview.SelectDisagreeResolutionCode();
            DesignDocCommentReview.ClickBtn_SaveOnly();
            int commentTabNumber = 2;
            ClickCommentTabNumber(commentTabNumber);
            EnterComment(CommentType.CommentResolutionInput, commentTabNumber);
            DesignDocCommentReview.SelectDisagreeResolutionCode(commentTabNumber);
            DesignDocCommentReview.ClickBtn_SaveOnly();
        }
    }

    internal class DesignDocumentWF_LAX : DesignDocumentWF
    {
        public DesignDocumentWF_LAX(IWebDriver driver) : base(driver)
        {
        }
    }
}