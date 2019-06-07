using System;
using OpenQA.Selenium;

namespace RKCIUIAutomation.Page
{
    public interface IPageHelper
    {
        /// <summary>
        /// Returns By locator - By.XPath("//a[text()='%parameter%']")
        /// </summary>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        By GetButtonByLocator(string buttonName);
        By GetDDListByLocator(Enum ddListID);
        By GetDDListCurrentSelectionByLocator(Enum ddListID);
        By GetDDListItemsByLocator<T, I>(T ddListID, I itemIndexOrName, bool useContains = false);
        By GetExpandDDListButtonByLocator<T>(T ddListID, bool isMultiSelectDDList = false);
        
        /// <summary>
        /// Returns By locator - By.XPath("//input[@value='%parameter%']")
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="buttonName"></param>
        /// <returns></returns>
        By GetInputButtonByLocator<T>(T buttonName);
        By GetInputFieldByLocator<T>(T inputFieldLabelOrID);
        By GetMainNavMenuByLocator(Enum navEnum);
        By GetMultiSelectDDListCurrentSelectionByLocator(Enum multiSelectDDListID);
        By GetNavMenuByLocator(Enum navEnum, Enum parentNavEnum = null);
        By GetSubmitButtonByLocator(Enum buttonValue, bool submitType = true);
        By GetTextAreaFieldByLocator(Enum textAreaEnum);
        By GetTextInputFieldByLocator(Enum inputEnum);
    }
}