﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using AutoIt;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Page.Navigation;
using RKCIUIAutomation.Page.PageObjects.RMCenter;
using static RKCIUIAutomation.Config.ProjectProperties;
using static RKCIUIAutomation.Page.Navigation.NavMenu;
using static RKCIUIAutomation.Page.PageObjects.RMCenter.SubmittalDetails;

namespace RKCIUIAutomation.Test
{
    /// <summary>
    /// Summary description for JUnitTest
    /// </summary>
    [TestClass]
    public class MSUnitTest : BaseClass
    {
        public MSUnitTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        //[TestMethod]
        public void MSUnitTest1()
        {
            //Directory.SetCurrentDirectory(Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).ToString());
            //Console.Out.WriteLine("#######" + Directory.GetParent(Directory.GetCurrentDirectory().ToString()));

            //var proj = GetSiteUrl(TestEnv.Prod, Project.SMF202);
            //var user = GetUser(UserType.SysAdmin);
            //Console.Out.WriteLine(proj);
            //Console.Out.WriteLine(user);
            //Console.Out.WriteLine();

            //var collection = ConfigurationManager.GetSection($"TestConfigs/UserType") as NameValueCollection;
            //var userPw = collection["SysAdminUsername"].Split(',');

            //string[] userPw = GetUser(UserType.ProjAdmin);
            //string username = userPw[0];
            //string password = userPw[1];
            //Console.Out.WriteLine($"Username : {username} and Password : {password}");


            //By locator = PageHelper.GetExpandDDListByLocator(DDListID.Action);
            //string locatorName = locator.GetType().Namespace;

            //IWebDriver driver = new ChromeDriver();
            //try
            //{
            //    driver.Navigate().GoToUrl("http://stage.garnet.elvispmc.com/");
            //    IWebElement elem = driver.FindElement(By.XPath("//img[@title='Garnet Interchange']"));

            //    Console.WriteLine(elem.GetType().Name);
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}

            //driver.Quit();

            //AutoItX.Run("notepad.exe", "C:\\Windows");
            //AutoItX.WinWaitActive("Untitled");
            //AutoItX.Send("Testing 1 2 3 4 5");
            //IntPtr winHandle = AutoItX.WinGetHandle("Untitled");
            //AutoItX.WinKill(winHandle);

            Console.WriteLine($"{GetCodeBasePath()}\\UploadFiles\\test.xlsx");
        }

        //[TestMethod]
        public void MSUnitTest2()
        {
            List<string> components = GetComponentsForProject(ProjectName.Garnet);
            int componentCount = components.Count;
           
            Console.WriteLine($"Component count is {componentCount}");
            
            foreach (var component in components)
            {
                Console.WriteLine(component.ToString());
            }

            Assert.IsTrue(componentCount.Equals(4));
            Assert.IsFalse(components.Contains(Component.DIR));
        }

        //[TestMethod]
        public void MSUnitTest3()
        {
            var project = Project.Administration.UserManagement.Menu.Roles;
            Type projectType = project.GetType();
            var reflectedType = projectType.ReflectedType;
            var reflectedTypeName = reflectedType.Name;

            Console.WriteLine($"ProjectType: {projectType.ToString()}");
            Console.WriteLine($"ReflectedType: {reflectedType}");
            Console.WriteLine($"ReflectedType Name: {reflectedTypeName}");
            Console.WriteLine(typeof(Project).ToString());

            //Assert.IsTrue(reflectedType.IsSubclassOf(typeof(Project)));
            Assert.IsTrue(reflectedType.Equals(typeof(Project.Administration.UserManagement)));
            //try
            //{
                
            //    //Assert.IsTrue(reflectedTypeName == typeof(Project).ToString());
            //}
            //catch (Exception e)
            //{
            //    Console.WriteLine(e.Message);
            //}
        }

        [TestMethod]
        public void MSUnitTest4()
        {
            var stringOne = "one";
            var numberOne = 1;
            Type stringOneType = null;
            stringOneType = stringOne.GetType();
            Type numberOneType = null;
            numberOneType = numberOne.GetType();

            Console.WriteLine(stringOne.GetType().ToString());
            Assert.IsTrue(stringOneType.Equals(typeof(string)));
            Console.WriteLine(numberOne.GetType().ToString());
            Assert.IsTrue(numberOneType.Equals(typeof(int)));

        }
    }

}
