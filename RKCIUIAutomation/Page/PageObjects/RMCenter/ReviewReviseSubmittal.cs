using OpenQA.Selenium;
using static RKCIUIAutomation.Base.Factory;

namespace RKCIUIAutomation.Page.PageObjects.RMCenter
{
    public class ReviewReviseSubmittal : ReviewReviseSubmittal_Impl
    {
        public ReviewReviseSubmittal(IWebDriver driver) => this.Driver = driver;

        public override T SetClass<T>(IWebDriver driver)
        {
            throw new System.NotImplementedException();
        }
    }

    public interface IReviewReviseSubmittal
    {
    }

    public abstract class ReviewReviseSubmittal_Impl : PageBase, IReviewReviseSubmittal
    {
    }


    #region Implementation specific to Garnet

    public class ReviewReviseSubmittal_Garnet : ReviewReviseSubmittal
    {
        public ReviewReviseSubmittal_Garnet(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to Garnet


    #region Implementation specific to GLX

    public class ReviewReviseSubmittal_GLX : ReviewReviseSubmittal
    {
        public ReviewReviseSubmittal_GLX(IWebDriver driver) : base(driver)
        {
        }

    }

    #endregion Implementation specific to GLX


    #region Implementation specific to SH249

    public class ReviewReviseSubmittal_SH249 : ReviewReviseSubmittal
    {
        public ReviewReviseSubmittal_SH249(IWebDriver driver) : base(driver)
        {
        }

    }

    #endregion Implementation specific to SH249


    #region Implementation specific to SGWay

    public class ReviewReviseSubmittal_SGWay : ReviewReviseSubmittal
    {
        public ReviewReviseSubmittal_SGWay(IWebDriver driver) : base(driver)
        {

        }

    }

    #endregion Implementation specific to SGWay


    #region Implementation specific to I15South

    public class ReviewReviseSubmittal_I15South : ReviewReviseSubmittal
    {
        public ReviewReviseSubmittal_I15South(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to I15South


    #region Implementation specific to I15Tech

    public class ReviewReviseSubmittal_I15Tech : ReviewReviseSubmittal
    {
        public ReviewReviseSubmittal_I15Tech(IWebDriver driver) : base(driver)
        {
        }

    }

    #endregion Implementation specific to I15Tech


    #region Implementation specific to LAX

    public class ReviewReviseSubmittal_LAX : ReviewReviseSubmittal
    {
        public ReviewReviseSubmittal_LAX(IWebDriver driver) : base(driver)
        {
        }
    }

    #endregion Implementation specific to LAX
}