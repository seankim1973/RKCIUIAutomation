using System;

namespace RKCIUIAutomation.Config
{
    public class ConfigUtil
    {
        public String DetermineSiteUrl(TestEnv testEnv, Project project)
        {
            String _project = project.GetString();
            String _testEnv = testEnv.GetString();

            var stringEnum = $"{_project}{_testEnv}";

            Enum.TryParse(stringEnum, out SiteUrl siteUrl);
            String _siteUrl = siteUrl.GetString();

            return _siteUrl;
        }

    }
}
