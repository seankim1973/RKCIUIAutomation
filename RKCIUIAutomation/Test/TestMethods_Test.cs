using NUnit.Framework;
using RKCIUIAutomation.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RKCIUIAutomation.Base.Factory;
using static RKCIUIAutomation.Page.TableHelper;
using static RKCIUIAutomation.Page.PageObjects.QARecordControl.QATestAll_Common;
using OpenQA.Selenium;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.PageObjects.QARecordControl;
using System.Threading;

namespace RKCIUIAutomation.Test.TestMethods
{
    [Parallelizable]
    [TestFixture]
    public class TestMethods_Test : TestBase
    {
        [Test]
        [Category(Component.TestMethods)]
        //[Property(Component2, Component.DIR_WF_Simple_QA)]
        [Property(TestCaseNumber, 2188159)]
        [Property(Priority, "High")]
        [Description("To validate creating and saving a Laboratory test method header.")]
        public void LabA_Create_Save_Lab_Test_Method_Header()
        {
            LoginAs(UserType.TestTech);

            IList<WorkflowType> workflowTypeList = new List<WorkflowType>()
            {
                //WorkflowType.E1,
                //WorkflowType.E2,
                //WorkflowType.E3,
                //WorkflowType.F1,
                //WorkflowType.F2,
                //WorkflowType.F3,
                WorkflowType.A1
            };

            foreach (var workflowType in workflowTypeList)
            {
                QATestMethod.CreateNewTestRecord(workflowType);
                QATestMethod.ClickBtn_Save();

                try
                {
                    QATestMethod.CheckForLINError();
                }
                catch (NoSuchElementException)
                {
                    //Populate Required Fields
                    PgHelper.PopulateEntryFieldsAndGetValuesArray(true);
                    WaitForPageReady();
                    QATestMethod.ClickBtn_Save();
                    WaitForPageReady();
                }

                //Click Add/Remove Test Methods
                QATestMethod.ClickBtn_AddRemoveTestMethods();

                IList<IWebElement> availTestInputs = new List<IWebElement>();
                availTestInputs = driver.FindElements(By.XPath("//input[@class='k-checkbox TestMethodSelection']"));

                Report.Info($"WorkFlow Type : {workflowType} - Available TestMethod Selections");
                foreach (var elem in availTestInputs)
                {
                    string label = elem.FindElement(By.XPath("./parent::span/following-sibling::span")).Text;
                    string identifier = elem.GetAttribute("identifier");
                    Report.Info($"{label}\n>>>Identifier Attribute : {identifier}");
                }

                IList<Enum> testMethodsToAdd = new List<Enum>()
                {
                    A_TestMethodType.AASHTO_T11,
                    A_TestMethodType.ASTM_C39
                };

                QATestMethod.AddTestMethod(testMethodsToAdd);
                Thread.Sleep(5000);

                By availableTestModal_CloseBtn = By.XPath("//span[@id='AvailableTestsWindow_wnd_title']/parent::div//a[@role='button'][@aria-label='Close']");
                ClickElement(availableTestModal_CloseBtn);
                //QATestMethod.ClickBtn_Cancel();
            }

            //Populate Required Fields
            //Click Add/Remove Test Methods
            //Get List of Available Tests by attribute 'identifier'
            //XPath to Input elem - //div[@id='AvailableTestsWindow']//input[@class='k-checkbox TestMethodSelection']

            AssertAll();
        }
    }
}
