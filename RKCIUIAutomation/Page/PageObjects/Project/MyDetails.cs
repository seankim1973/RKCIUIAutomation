﻿using OpenQA.Selenium;

namespace RKCIUIAutomation.Page.PageObjects.Project
{
    public class MyDetails : PageBase
    {
        public MyDetails(){}
        public MyDetails(IWebDriver driver) => Driver = driver;
        public static MyDetails MyDetailsPage { get => new MyDetails(Driver); set { } }


    }
}
