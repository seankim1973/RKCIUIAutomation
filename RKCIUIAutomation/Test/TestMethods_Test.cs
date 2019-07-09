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

            IList<WorkflowType> collection = new List<WorkflowType>()
            {
                WorkflowType.E1,
                WorkflowType.E2,
                WorkflowType.E3,
                WorkflowType.F1,
                WorkflowType.F2,
                WorkflowType.F3,
                WorkflowType.A1
            };

            foreach (var item in collection)
            {
                QATestMethod.CreateNewTestRecord(item);

                //Populate Required Fields
                //Click Add/Remove Test Methods

                IList<IWebElement> availTestInputs = new List<IWebElement>();
                availTestInputs = driver.FindElements(By.XPath("//input[@class='k-checkbox TestMethodSelection']"));

                foreach (var elem in availTestInputs)
                {
                    string label = elem.FindElement(By.XPath("./parent::span/following-sibling::span")).Text;
                    string identifier = elem.GetAttribute("identifier");
                    Console.WriteLine($"{label}\nIdentifier Attribute : {identifier}");
                }

                QATestMethod.ClickBtn_Cancel(); 
            }

            //Populate Required Fields
            //Click Add/Remove Test Methods
            //Get List of Available Tests by attribute 'identifier'
            //XPath to Input elem - //div[@id='AvailableTestsWindow']//input[@class='k-checkbox TestMethodSelection']

            AssertAll();
        }
    }
}
