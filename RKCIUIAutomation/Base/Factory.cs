using log4net;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Test;

namespace RKCIUIAutomation.Base
{
    public static class Factory
    {
        public static readonly ILog log = LogManager.GetLogger("");

        public static IKendoGrid Kendo => new KendoGrid();
        public static IAction PageAction => new Action();
        public static IBaseUtils Utility => new BaseUtils();
        public static IConfigUtils Configs => new ConfigUtils();
        public static ITableHelper TableHelper => new TableHelper();
        public static ITestUtils TestUtility => new TestUtils();
        public static IProjectProperties Props => new ProjectProperties();
    }
}