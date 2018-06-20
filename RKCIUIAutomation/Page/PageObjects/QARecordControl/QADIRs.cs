using OpenQA.Selenium;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RKCIUIAutomation.Page.PageObjects.QARecordControl
{
    public interface IQADIRs
    {
        bool IsLoaded();
    }

    public class QADIRs : QADIRs_Impl
    {

    }

    public abstract class QADIRs_Impl : TestBase, IQADIRs
    {
        /// <summary>
        /// Method to instantiate page class based on NUNit3-Console cmdLine parameter 'Project'
        /// </summary>
        public static T SetClass<T>() => (T)SetPageClassBasedOnTenant();
        public static IQADIRs SetPageClassBasedOnTenant()
        {
            IQADIRs instance = new QADIRs();

            if (projectName == ProjectName.SGWay)
            {
                LogInfo($"###### using QADIRs_SGWay instance ###### ");
                instance = new QADIRs_SGWay();
            }
            else if (projectName == ProjectName.SH249)
            {
                LogInfo($"###### using QADIRs_SH249 instance ###### ");
                instance = new QADIRs_SH249();
            }
            else if (projectName == ProjectName.Garnet)
            {
                LogInfo($"###### using QADIRs_Garnet instance ###### ");
                instance = new QADIRs_Garnet();
            }
            else if (projectName == ProjectName.GLX)
            {
                LogInfo($"###### using QADIRs_GLX instance ###### ");
                instance = new QADIRs_GLX();
            }
            else if (projectName == ProjectName.I15South)
            {
                LogInfo($"###### using QADIRs_I15South instance ###### ");
                instance = new QADIRs_I15South();
            }
            else if (projectName == ProjectName.I15Tech)
            {
                LogInfo($"###### using QADIRs_I15Tech instance ###### ");
                instance = new QADIRs_I15Tech();
            }

            return instance;
        }

        public virtual bool IsLoaded() => Driver.Title.Equals("DIR List - ELVIS PMC");

    }

    public class QADIRs_Garnet : QADIRs
    {
    }

    public class QADIRs_GLX : QADIRs
    {
    }

    public class QADIRs_SH249 : QADIRs
    {
    }

    public class QADIRs_SGWay : QADIRs
    {
    }
    
    public class QADIRs_I15South : QADIRs
    {
    }

    public class QADIRs_I15Tech : QADIRs
    {
    }

}
