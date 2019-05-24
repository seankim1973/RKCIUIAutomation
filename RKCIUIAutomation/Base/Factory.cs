using log4net;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Page;
using RKCIUIAutomation.Test;

namespace RKCIUIAutomation.Base
{
    public static class Factory
    {
        private static ILog _logger(string loggerName = "") => LogManager.GetLogger(loggerName);
        public static readonly ILog log = _logger();
        
        private static KendoGrid _Kendo() => new KendoGrid();
        public static IKendoGrid Kendo() => _Kendo();

        private static Action _Action() => new Action();
        public static IAction PageAction() => _Action();

        private static BaseUtils _BaseUtils() => new BaseUtils();
        public static IBaseUtils BaseUtility() => _BaseUtils();
        public static IBaseUtils SetReportPath(TenantName tenantName) => new BaseUtils(tenantName);
        public static IBaseUtils SetGridAddress(TestPlatform testPlatform, string gridAddress) => new BaseUtils(testPlatform, gridAddress);

        private static ConfigUtils _ConfigUtils() => new ConfigUtils();
        public static IConfigUtils ConfigUtility() => _ConfigUtils();

        private static TableHelper _TableHelper() => new TableHelper();
        public static ITableHelper GridHelper() => _TableHelper();

        private static TestUtils _TestUtils() => new TestUtils();
        public static ITestUtils TestUtility() => _TestUtils();

        private static PageHelper _PageHelper() => new PageHelper();
        public static IPageHelper PgHelper() => _PageHelper();

        private static ProjectProperties _ProjectProperties() => new ProjectProperties();
        public static IProjectProperties Property() => _ProjectProperties();
        public static IProjectProperties SetTenantComponents(TenantName tenantName) => new ProjectProperties(tenantName);
    }
}