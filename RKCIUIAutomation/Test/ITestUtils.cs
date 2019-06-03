using RKCIUIAutomation.Page;
using System.Collections.Generic;

namespace RKCIUIAutomation.Test
{
    public interface ITestUtils
    {
        //IPageInteraction _pgAction { get; set; }
        void AddAssertionToList(bool assertion, string details = "");
        void AddAssertionToList_VerifyPageHeader(string expectedPageHeader, string additionalDetails = "");
        void AssertAll();
        List<string> GetNavMenuUrlList();
        void HttpResponse();
        void LoopThroughNavMenu();
    }
}