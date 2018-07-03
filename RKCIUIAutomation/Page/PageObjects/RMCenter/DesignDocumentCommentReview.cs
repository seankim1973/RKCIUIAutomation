using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Config.ProjectProperties;
using NUnit.Framework;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page.PageObjects.LabFieldTests;
using RKCIUIAutomation.Base;


namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    #region DesignDocumentCommentReview Generic class
    public class DesignDocumentCommentReview : DesignDocumentCommentReview_Impl
    {
        public DesignDocumentCommentReview(){}
        public DesignDocumentCommentReview(IWebDriver driver) => this.driver = driver;
    }
    #endregion  <-- end of DesignDocumentCommentReview Generic Class

    #region Workflow Interface class
    public interface IDesignDocumentCommentReview
    {
        void _LoggedInUserUploadsDesignDocument();
        void _LoggedInUserCommentsAndSave();
        void _LoggedInUserForwardsComment();
        void _LoggedInUserResponseCommentAndSave();
        void _LoggedInUserForwardsResponseComment();
        void _LoggedInUserResolutionCommentAndSave();
        void _LoggedInUserForwardsResolutionComment();
        void _LoggedInUserCloseCommentAndSave();
        void _LoggedInUserForwardsCloseComment();

    }
    #endregion <-- end of Workflow Interface class

    #region Common Workflow Implementation class
    public abstract class DesignDocumentCommentReview_Impl : PageBase, IDesignDocumentCommentReview
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public T SetClass<T>(IWebDriver driver) => (T)SetPageClassBasedOnTenant(driver);
        private IDesignDocumentCommentReview SetPageClassBasedOnTenant(IWebDriver driver)
        {
            IDesignDocumentCommentReview instance = new DesignDocumentCommentReview(driver);

            if (tenantName == TenantName.SGWay)
            {
                LogInfo($"###### using DesignDocumentCommentReview_SGWay instance ###### ");
                instance = new DesignDocumentCommentReview_SGWay(driver);
            }
            else if (tenantName == TenantName.SH249)
            {
                LogInfo($"###### using  DesignDocumentCommentReview_SH249 instance ###### ");
                instance = new DesignDocumentCommentReview_SH249(driver);
            }
            else if (tenantName == TenantName.Garnet)
            {
                LogInfo($"###### using  DesignDocumentCommentReview_Garnet instance ###### ");
                instance = new DesignDocumentCommentReview_Garnet(driver);
            }
            else if (tenantName == TenantName.GLX)
            {
                LogInfo($"###### using  DesignDocumentCommentReview_GLX instance ###### ");
                instance = new DesignDocumentCommentReview_GLX(driver);
            }
            else if (tenantName == TenantName.I15South)
            {
                LogInfo($"###### using  DesignDocumentCommentReview_I15South instance ###### ");
                instance = new DesignDocumentCommentReview_I15South(driver);
            }
            else if (tenantName == TenantName.I15Tech)
            {
                LogInfo($"###### using DesigntDocumentCommentReview_I15Tech instance ###### ");
                instance = new DesignDocumentCommentReview_I15Tech(driver);
            }

            return instance;
        }

        /// <summary>
        /// TODO - implement common workflow
        /// </summary>
        public virtual void _LoggedInUserUploadsDesignDocument()
        {
           
        }

        public virtual void _LoggedInUserCommentsAndSave() { }
        public virtual void _LoggedInUserForwardsComment() { }
        public virtual void _LoggedInUserResponseCommentAndSave() { }
        public virtual void _LoggedInUserForwardsResponseComment() { }
        public virtual void _LoggedInUserResolutionCommentAndSave() { }
        public virtual void _LoggedInUserForwardsResolutionComment() { }
        public virtual void _LoggedInUserCloseCommentAndSave() { }
        public virtual void _LoggedInUserForwardsCloseComment() { }

    }
    #endregion <--end of common implementation class

    /// <summary>
    /// Tenant specific implementation of DesignDocument Comment Review
    /// </summary>

    #region Implementation specific to Garnet
    public class DesignDocumentCommentReview_Garnet : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_Garnet(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to Garnet

    #region Implementation specific to GLX
    public class DesignDocumentCommentReview_GLX : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_GLX(IWebDriver driver) : base(driver) { }
    }
    #endregion specific to GLX

    #region Implementation specific to SH249
    public class DesignDocumentCommentReview_SH249 : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_SH249(IWebDriver driver) : base(driver) { }

    }
    #endregion <--specific toSGway

    #region Implementation specific to SGWay
    public class DesignDocumentCommentReview_SGWay : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_SGWay(IWebDriver driver) : base(driver) { }

    }
    #endregion <--specific toSGway

    #region Implementation specific to I15South
    public class DesignDocumentCommentReview_I15South : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_I15South(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to I15south

    #region Implementation specific to I15Tech
    public class DesignDocumentCommentReview_I15Tech : DesignDocumentCommentReview
    {
        public DesignDocumentCommentReview_I15Tech(IWebDriver driver) : base(driver) { }
    }
    #endregion <--specific to I15tech
}
