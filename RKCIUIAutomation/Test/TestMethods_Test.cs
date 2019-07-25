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

            #region Revise Existing Test Record
            //QATestMethod.SelectTab_LabRevise();
            //GridHelper.VerifyRecordIsDisplayed(ColumnNameType.RevisedBy, "ATTestTech@rkci.com");
            //GridHelper.ClickEditBtnForRow();
            #endregion

            #region Create New Test Record

            IList<WorkflowType> workflowTypeList = new List<WorkflowType>()
            {
                WorkflowType.E1,
                WorkflowType.E2,
                WorkflowType.E3,
                WorkflowType.F1,
                WorkflowType.F2,
                WorkflowType.F3,
                WorkflowType.A1
            };
            try
            {
                foreach (var workflowType in workflowTypeList)
                {
                    QATestMethod.CreateNewTestRecord(workflowType);
                    QATestMethod.ClickBtn_Save();

                    Console.WriteLine($"****************************************************");
                    Console.WriteLine($"        WORKFLOWTYPE : {workflowType}");
                    Console.WriteLine($"****************************************************");

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

                    #endregion



                    #region Retrieve and store identifier attribute for all available test method
                    /*
                    IList<IWebElement> availTestInputs = new List<IWebElement>();
                    availTestInputs = driver.FindElements(By.XPath("//input[@class='k-checkbox TestMethodSelection']"));
                    Report.Info($"WorkFlow Type : {workflowType} - Available TestMethod Selections");
                    foreach (var elem in availTestInputs)
                    {
                        string label = elem.FindElement(By.XPath("./parent::span/following-sibling::span")).Text;
                        string identifier = elem.GetAttribute("identifier");
                        Report.Info($"{label}\n>>>Identifier Attribute : {identifier}");
                    }
                    */
                    #endregion

                    #region Select all TestMethod Checkboxes
                    IList<string> testMethodsToAddList = new List<string>();
                    By checkBoxLocator = By.XPath("//span[contains(@style, 'inline-flex')]/preceding-sibling::span/input[contains(@class, 'k-checkbox')]");
                    ((List<string>)testMethodsToAddList).AddRange(GetAttributeForElements(checkBoxLocator, "identifier"));
                    QATestMethod.AddTestMethod(testMethodsToAddList);
                    QATestMethod.ClickModalBtn_Save();
                    #endregion


                    QATestMethod.GatherTestMethodInputFieldAttributeDetails(testMethodsToAddList);

                    QATestMethod.ClickBtn_Cancel();
                }
            }
            catch (Exception e)
            {
                log.Error($"{e.Message}\n{e.StackTrace}");
                throw;
            }

            AssertAll();
        }
    }
}
