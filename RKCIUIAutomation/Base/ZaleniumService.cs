using System;
using System.Threading;
using System.Web.Script.Serialization;

namespace RKCIUIAutomation.Base
{
#pragma warning disable IDE1006 // Naming Styles

    public class ZaleniumService : BaseUtils
    {
        public void Start()
        {
            string cmdLineArgument = $"run --rm -ti --name zalenium -p 4444:4444 -p 5555:5555 " +
                $"-v /var/run/docker.sock:/var/run/docker.sock " +
                $"-v C:\\inetpub\\wwwroot\\dashboard:/home/seluser/videos dosel/zalenium start" +
                $"--desiredContainers=6";
            Service(cmdLineArgument);
        }

        public void Stop()
        {
            string cmdLineArgument = "stop zalenium";
            Service(cmdLineArgument);
        }

        private void Service(string cmdLineArgument)
        {
            if (!ZaleniumIsRunning())
            {
                try
                {
                    RunExternalExecutible("docker", cmdLineArgument);

                    while (!ZaleniumIsRunning())
                    {
                        Thread.Sleep(5000);
                    }
                }
                catch (Exception e)
                {
                    LogError(e.Message);
                    throw;
                }
            }
        }

        public bool ZaleniumIsRunning()
        {
            BaseUtils baseUtils = new BaseUtils();
            string zaleniumBaseUrl = "http://10.1.1.207:4444";
            string zaleniumStatus = "wd/hub/status";

            bool isRunning = false;
            string json = string.Empty;
            try
            {
                RestUtils Rest = new RestUtils();
                json = Rest.GetJsonResponse(zaleniumBaseUrl, zaleniumStatus);
                JavaScriptSerializer js = new JavaScriptSerializer();
                Value value = js.Deserialize<RootObject>(json).value;
                isRunning = value.ready;
            }
            catch (Exception e)
            {
                LogError(e.Message);
            }

            return isRunning;
        }

        public class Build
        {
            public string revision { get; set; }
            public string time { get; set; }
            public string version { get; set; }
        }

        public class Os
        {
            public string arch { get; set; }
            public string name { get; set; }
            public string version { get; set; }
        }

        public class Java
        {
            public string version { get; set; }
        }

        public class Value
        {
            public bool ready { get; set; }
            public string message { get; set; }
            public Build build { get; set; }
            public Os os { get; set; }
            public Java java { get; set; }
        }

        public class RootObject
        {
            public int status { get; set; }
            public Value value { get; set; }
        }
    }
}