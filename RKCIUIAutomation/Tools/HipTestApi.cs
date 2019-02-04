using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using RestSharp;
using RKCIUIAutomation.Base;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using static RKCIUIAutomation.Tools.HipTest;

namespace RKCIUIAutomation.Tools
{
    public class HipTestApi : TestBase
    {
        private static Lazy<List<KeyValuePair<string, List<int>>>> _suiteTestCaseDataset;
        private List<KeyValuePair<string, List<int>>> SuiteTestCaseDataset { get { return _suiteTestCaseDataset.Value; } set { } }

        private static ConfigUtils config = new ConfigUtils();
        private static readonly string _accessToken;
        private static readonly string _clientId;
        private static readonly string _userId;
        private static readonly string _userPw;

        private readonly static string proj_rkci = "95332";

        private readonly string ApiBase = $"https://app.hiptest.com/api/projects/{proj_rkci}";

        private static readonly Lazy<HipTestApi> _lazy;
        public static HipTestApi HipTestInstance { get { return _lazy.Value; } }

        static HipTestApi()
        {
            _lazy = new Lazy<HipTestApi>(() => new HipTestApi());

            _accessToken = config.GetHipTestCreds(HipTestKey.HipTest_Token);
            _clientId = config.GetHipTestCreds(HipTestKey.HipTest_ClientID);
            _userId = config.GetHipTestCreds(HipTestKey.HipTest_UID);
            _userPw = config.GetHipTestCreds(HipTestKey.HipTest_PWD);
        }

        public enum HipTestKey
        {
            HipTest_UID,
            HipTest_PWD,
            HipTest_Token,
            HipTest_ClientID
        }

        private enum ResourceType
        {
            BuildIDs,
            TestRuns,
            TestResults,
            TestSnapshots
        }

        private enum TestRunType
        {
            Sync,
            GetTest
        }

        private IRestRequest CreateRequest(Method requestMethod, string resource)
        {
            try
            {
                
                var request = new RestRequest(requestMethod)
                {
                    Resource = resource
                };

                request
                    .AddHeader("Accept", "application/vnd.api+json; version=1")
                    .AddHeader("access-token", config.GetDecryptedPW(_accessToken))
                    .AddHeader("client", config.GetDecryptedPW(_clientId))
                    .AddHeader("uid", config.GetDecryptedPW(_userId));
                return request;
            }
            catch (Exception e)
            {
                log.Debug($"Error occured in SetRequest Method.\n{e.Message}");
                throw;
            }
        }

        private Task<IRestResponse> CallExecuteRequest(Method requestMethod, ResourceType resource, RootObject json = null, params object[] requestParams)
        {
            var executeTask = ExecuteRequest(requestMethod, resource, json, requestParams);
            Task.WaitAll(executeTask);
            return executeTask;
        }

        private Task<IRestResponse> ExecuteRequest(Method requestMethod, ResourceType resource, RootObject json = null, params object[] requestParams)
        {
            int buildId;
            int testRunId;
            int testResultId;
            int testSnapshotId;

            string endPoint = string.Empty;
            string content = string.Empty;
            string statusCode = string.Empty;

            BaseUtils baseUtils = new BaseUtils();

            try
            {
                switch (resource)
                {
                    case ResourceType.TestRuns:

                        if (requestMethod == Method.GET)
                        {
                            TestRunType testRunType = (TestRunType)requestParams[0];
                            testRunId = baseUtils.ConvertToType<int>(requestParams[1]);

                            if (testRunType == TestRunType.GetTest)
                            {
                                testSnapshotId = baseUtils.ConvertToType<int>(requestParams[3]);
                                bool allTests = baseUtils.ConvertToType<bool>(requestParams[2]);
                                endPoint = allTests ? $"/test_runs/{testRunId}/test_snapshots" : $"/test_runs/{testRunId}";
                            }
                            else if (testRunType == TestRunType.Sync)
                            {
                                endPoint = $"/test_runs/{testRunId}?show_synchronization_information=true";
                            }
                        }
                        else
                        {
                            endPoint = "/test_runs";
                        }
                        break;

                    case ResourceType.TestResults:
                        if (requestMethod == Method.POST)
                        {
                            testRunId = baseUtils.ConvertToType<int>(requestParams[0]);
                            buildId = baseUtils.ConvertToType<int>(requestParams[1]);
                            endPoint = $"/test_runs/{testRunId}/builds/{buildId}/test_results";
                        }
                        else if (requestMethod == Method.PATCH)
                        {
                            testRunId = baseUtils.ConvertToType<int>(requestParams[0]);
                            testSnapshotId = baseUtils.ConvertToType<int>(requestParams[1]);
                            testResultId = baseUtils.ConvertToType<int>(requestParams[2]);
                            endPoint = $"/test_runs/{testRunId}/test_snapshots/{testSnapshotId}/test_results/{testResultId}";
                        }
                        else
                        {
                            testRunId = baseUtils.ConvertToType<int>(requestParams[0]);
                            testSnapshotId = baseUtils.ConvertToType<int>(requestParams[1]);
                            endPoint = $"/test_runs/{testRunId}/test_snapshots/{testSnapshotId}?include=last-result";
                        }
                        break;

                    case ResourceType.TestSnapshots:
                        testRunId = baseUtils.ConvertToType<int>(requestParams[0]);
                        endPoint = $"/test_runs/{testRunId}/test_snapshots?include=scenario";
                        break;
                }

                IRestRequest request = CreateRequest(requestMethod, endPoint);

                if (json != null)
                {
                    request.AddJsonBody(json);
                }

                log.Info($"API Endpoint: {endPoint}");
                IRestClient client = new RestClient(ApiBase);

                var responseTask = Task.Factory.StartNew(() => client.Execute(request));
                Task.WaitAny(responseTask);
                var response = responseTask.Result;
                statusCode = response.StatusCode.ToString();
                content = response.Content;

                return responseTask;
            }
            catch (Exception e)
            {
                log.Debug($"ExecuteRequest StatusCode: {statusCode}\n{e.Message}");
                log.Debug(content);
                throw;
            }
        }

        /// <summary>
        /// Provide test case numbers(scenarioIDs), as string[] array, to be included in the Hiptest Test Run.
        /// Provide testRunParams as following index: [0]TestSuite, [1]TestEnv, [2]TenantName.
        /// </summary>
        /// <param name="scenarioIDs"></param>
        /// <param name="testRunDetails"></param>
        /// <returns></returns>
        public int CreateTestRun(List<int> scenarioIDs, string[] testRunDetails)
        {
            Task<IRestResponse> responseTask = null;
            string statusCode = string.Empty;
            string content = string.Empty;

            try
            {
                string testSuite = testRunDetails[0];
                string testEnv = testRunDetails[5];
                string tenantName = testRunDetails[6];
                string dateTime = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}";
                string testRunName = $"({testEnv}){tenantName}_{testSuite} {dateTime}";
                string testDescription = $"UI Automation Run for: ({testEnv}){tenantName}\nStartTime: {dateTime}\nTestSuite: {testSuite}";

                var json = new RootObject
                {
                    data = new Datum
                    {
                        attributes = new Attributes
                        {
                            name = testRunName,
                            description = testDescription,
                            scenario_ids = scenarioIDs.ToArray()
                        },
                        relationships = new Relationships
                        {
                            tags = new Tags
                            {
                                data = new List<TagData>
                                {
                                    new TagData
                                    {
                                        type = "tags",
                                        key = "Tenant",
                                        value = tenantName
                                    },
                                    new TagData
                                    {
                                        type = "tags",
                                        key = "TestEnv",
                                        value = testEnv
                                    },
                                    new TagData
                                    {
                                        type = "tags",
                                        key = "TestSuite",
                                        value = testSuite
                                    }
                                }
                            }
                        }
                    }
                };

                responseTask = CallExecuteRequest(Method.POST, ResourceType.TestRuns, json);

                do
                {
                    Thread.Sleep(1000);
                }
                while (responseTask == null);

                Task.WaitAll(responseTask);

                var response = responseTask.Result;
                statusCode = response.StatusCode.ToString();
                content = response.Content;

                //Get Test Run ID
                RootObject root = JsonConvert.DeserializeObject<RootObject>(content);
                var testRunId = root.data.id;
                log.Info($"Created TestRun:\nName: {testRunName}\n ID: {testRunId}");

                Console.WriteLine($"TESTRUN ID: {testRunId}");
                Console.WriteLine(content);

                return testRunId;
            }
            catch (Exception e)
            {
                log.Debug($"CreateTestRun StatusCode: {statusCode}\n{e.Message}");
                log.Debug(content);
                throw;
            }
        }

        /// <summary>
        /// Returns List&lt;KeyValuePair&lt;(int)testCaseNumber, List&lt;int[]&gt;&gt;&gt;
        /// <para>Pair value List&lt;(int[])&gt; has index of:  [0]scenarioId, [1]scenarioSnapshotId, [2]lastResultId</para>
        /// </summary>
        /// <param name="testRunId"></param>
        /// <returns></returns>
        public List<KeyValuePair<int, List<int>>> BuildTestRunSnapshotData(int testRunId)
        {
            string content = string.Empty;
            string testName = string.Empty;
            int scenarioId = -1;
            int scenarioSnapshotId = -1;
            int lastResultId = -1;

            var runData = new List<KeyValuePair<int, List<int>>>();

            var snapshotRequestParams = new object[]
            {
                testRunId
            };

            try
            {
                var snapshotIdTask = Task.Factory.StartNew(() => CallExecuteRequest(Method.GET, ResourceType.TestSnapshots, null, snapshotRequestParams));
                var snapshotResultTask = snapshotIdTask.Result;
                var snapshotTaskList = new Task[] { snapshotIdTask, snapshotResultTask };
                Task.WaitAll(snapshotTaskList);

                var snapshotIdContent = snapshotResultTask.Result.Content;

                DatumList dataList = new DatumList();
                dataList = (DatumList)JsonConvert.DeserializeObject(snapshotIdContent, typeof(DatumList));

                int dataCount = dataList.data.Count;
                for (int i = 0; i < dataCount; i++)
                {
                    scenarioSnapshotId = dataList.data[i].id;
                    testName = dataList.data[i].attributes.name;
                    scenarioId = dataList.data[i].relationships.scenario.data.id;
                    //scenarioSnapshotId = dataList.data[i].attributes.scenario_snapshot_id;
                    Console.WriteLine($"\n### TestCase#: {scenarioId}\n### TestName: {testName}\n### ScenarioSnapshotID: {scenarioSnapshotId}");

                    var testResultRequestParams = new object[]
                    {
                        testRunId,
                        scenarioSnapshotId
                    };

                    var resultIdTask = CallExecuteRequest(Method.GET, ResourceType.TestResults, null, testResultRequestParams);
                    Task.WaitAll(resultIdTask);
                    var resultIdContent = resultIdTask.Result.Content;

                    RootObject root = new RootObject();
                    root = (RootObject)JsonConvert.DeserializeObject(resultIdContent, typeof(RootObject));

                    lastResultId = root.included[0].id;
                    Console.WriteLine($"\n### LastResultID: {lastResultId}\n");

                    var scenarioSnapshotData = new List<int>
                    {
                        testRunId,
                        scenarioSnapshotId,
                        lastResultId
                    };

                    runData.Add(new KeyValuePair<int, List<int>>(scenarioId, scenarioSnapshotData));
                }

                return runData;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }
        }

        /// <summary>
        /// Updates Hiptest TestRun with TestStatus and Description using data in BaseClass.hipTestResults
        /// <para>runData returned by: <see cref="BuildTestRunSnapshotData"/></para>
        /// <para>testResults: List&lt;KeyValuePair&lt;(int)testCaseNumber&gt;, KeyValuePair&lt;(TestStatus)testStatus, (string)testDescription&gt;&gt;&gt;</para>
        /// </summary>
        /// <param name="testResultsDataset"></param>
        public void UpdateHipTestRunData(List<KeyValuePair<int, List<int>>> hipTestRunDataset, List<KeyValuePair<int, KeyValuePair<TestStatus, string>>> testResultsDataset)
        {
            string content = string.Empty;
            string statusCode = string.Empty;
            int hipTestCount = hipTestRunDataset.Count;
            int resultsCount = testResultsDataset.Count;

            try
            {
                for (int i = 0; i < resultsCount; i++)
                {
                    for (int h = 0; h < hipTestCount; h++)
                    {
                        var resultPair = testResultsDataset[i];
                        int tcNumber = resultPair.Key;
                        //Console.WriteLine($"####TC#: {tcNumber.ToString()}");

                        KeyValuePair<int, List<int>> hipTestRunPair = hipTestRunDataset[h];
                        var scenarioId = hipTestRunPair.Key;

                        if (scenarioId == tcNumber)
                        {
                            var statusDescPair = resultPair.Value;
                            var tcStatus = statusDescPair.Key.ToString().ToLower();
                            var description = statusDescPair.Value;

                            var snapshotData = new List<int>(hipTestRunPair.Value);
                            var testRunId = snapshotData[0];
                            var scenarioSnapshotId = snapshotData[1];
                            var testResultId = snapshotData[2];

                            string logMsg =
                                $"\n####HipTest ScenarioID: {scenarioId}\n" +
                                $"####ScenarioSnapshotID: {scenarioSnapshotId}\n" +
                                $"####Status: {tcStatus}\n" +
                                $"####Desc: {description}\n";
                            log.Debug(logMsg);

                            var requestParams = new object[]
                            {
                                testRunId,
                                scenarioSnapshotId,
                                testResultId
                            };

                            //Console.WriteLine($"TestRunID: {testRunId}");
                            //Console.WriteLine($"SnapshotID: {scenarioSnapshotId}");
                            //Console.WriteLine($"TestResultID: {testResultId}");

                            var json = new RootObject
                            {
                                data = new Datum
                                {
                                    type = "test-results",
                                    id = testResultId,
                                    attributes = new Attributes
                                    {
                                        status = tcStatus,
                                        status_author = "RKCIUIAutomation",
                                        description = description
                                    }
                                }
                            };

                            var testResultsTask = CallExecuteRequest(Method.PATCH, ResourceType.TestResults, json, requestParams);

                            Task.WaitAll(testResultsTask);
                            var response = testResultsTask.Result;
                            statusCode = response.StatusCode.ToString();
                            content = response.Content;
                            //Console.WriteLine(response.Content);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                log.Debug($"UpdateHipTestRunData StatusCode: {statusCode}\n{e.Message}");
                log.Debug(content);
                throw;
            }
        }

        /// <summary>
        /// Provide taskParams[] with index: [0](int)TestRunID, [1](bool)AllTests, **[2](int)testSnapshotID
        /// **testSnapshotID is not required if performing Sync for AllTests
        /// </summary>
        /// <param name="taskParams"></param>
        /// <returns></returns>
        private IRestResponse GetTestRun(object[] taskParams) => GetTestRunTask(TestRunType.GetTest, taskParams);

        /// <summary>
        /// Syncs test results for test cases in a TestRun
        /// </summary>
        /// <param name="testRunId"></param>
        public void SyncTestRun(int testRunId) => GetTestRunTask(TestRunType.Sync, testRunId);

        private IRestResponse GetTestRunTask<T>(TestRunType testRunType, T taskParams)
        {
            int testBuildId = -1;
            string content = string.Empty;
            string statusCode = string.Empty;
            Type paramType = taskParams.GetType();
            BaseUtils baseUtils = new BaseUtils();

            try
            {
                if (paramType == typeof(int))
                {
                    testBuildId = baseUtils.ConvertToType<int>(taskParams);
                }
                else if (paramType == typeof(object[]))
                {
                    testBuildId = (int)baseUtils.ConvertToType<object[]>(taskParams)[0];
                }

                var testRunTaskParms = new object[]
                {
                    testRunType,
                    testBuildId,
                };

                var testRunTask = CallExecuteRequest(Method.GET, ResourceType.TestRuns, null, testRunTaskParms);
                Task.WaitAll(testRunTask);
                var response = testRunTask.Result;
                statusCode = response.StatusCode.ToString();
                content = response.Content;
                return response;
            }
            catch (Exception e)
            {
                log.Debug($"Get TestRun Task StatusCode: {statusCode}\n{e.Message}");
                log.Debug(content);
                throw;
            }
        }

        private IRestResponse AssignResultsToTestRunBuild(string testRunId, int buildId, int testId, string testStatus, string description)
        {
            string content = string.Empty;
            string statusCode = string.Empty;

            try
            {
                var requestParams = new object[]
                {
                    testRunId,
                    buildId
                };

                var json = new RootObject
                {
                    data = new Datum
                    {
                        type = "test-results",
                        attributes = new Attributes
                        {
                            status = testStatus,
                            status_author = "RKCIUIAutomation",
                            description = description
                        },
                        relationships = new Relationships
                        {
                            build = new Build
                            {
                                data = new BuildData
                                {
                                    type = "build",
                                    id = testId
                                }
                            }
                        }
                    }
                };

                var testResultsTask = CallExecuteRequest(Method.POST, ResourceType.TestResults, json, requestParams);
                Task.WaitAll(testResultsTask);
                var response = testResultsTask.Result;
                statusCode = response.StatusCode.ToString();
                content = response.Content;
                return response;
            }
            catch (Exception e)
            {
                log.Debug($"AssignResultsToTestRunBuild StatusCode: {statusCode}\n{e.Message}");
                log.Debug(content);
                throw;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public void CreateTestRunDataset(string testSuite, int currentTestCaseNumber)
        {
            _suiteTestCaseDataset = new Lazy<List<KeyValuePair<string, List<int>>>>(() => null);

            KeyValuePair<string, List<int>> suiteTestCasePairs;
            List<int> suiteTestCases = null;
            string currentSuiteName = testSuite;

            if (SuiteTestCaseDataset == null)
            {
                suiteTestCases = new List<int>
                {
                    currentTestCaseNumber
                };
                suiteTestCasePairs = new KeyValuePair<string, List<int>>(currentSuiteName, suiteTestCases);
                SuiteTestCaseDataset = new List<KeyValuePair<string, List<int>>>
                {
                    suiteTestCasePairs
                };

                log.Info($"### Created Dataset and added Suite: {currentSuiteName} - TC#: {currentTestCaseNumber}");
            }
            else
            {
                log.Info($"### Dateset is not Null");
                int dataSetCount = SuiteTestCaseDataset.Count;

                for (int i = 0; i < dataSetCount; i++)
                {
                    KeyValuePair<string, List<int>> suiteTestCasePair = SuiteTestCaseDataset[i];
                    string existingSuiteName = suiteTestCasePair.Key;
                    List<int> existingTCNumbersList = suiteTestCasePair.Value;
                    log.Info($"### Existing Suite Name in Dataset: {existingSuiteName}");

                    if (currentSuiteName == existingSuiteName)
                    {
                        log.Info($"Current Suite Name: {currentSuiteName} matches Existing Suite Name: {existingSuiteName}");
                        if (!existingTCNumbersList.Contains(currentTestCaseNumber))
                        {
                            log.Info($"Existing TC List does not contain the Current TC#...Adding TC#: {currentTestCaseNumber}");
                            for (int n = 0; n < existingTCNumbersList.Count; n++)
                            {
                                log.Info($"Existing TC#: {existingTCNumbersList[n]}");
                            }

                            existingTCNumbersList.Add(currentTestCaseNumber);
                            var newSuiteTCPair = new KeyValuePair<string, List<int>>(existingSuiteName, existingTCNumbersList);
                            newSuiteTCPair = SuiteTestCaseDataset.First(p => p.Key == existingSuiteName);

                            for (int n = 0; n < existingTCNumbersList.Count; n++)
                            {
                                log.Info($"New TC#: {existingTCNumbersList[n]}");
                            }
                        }
                        else
                        {
                            log.Info($"Current test case number ({currentTestCaseNumber}), " +
                                $"has already been added.\nMake sure do you not have duplicate values in the TestCaseNumber Attribute");
                        }
                    }
                    else
                    {
                        log.Info($"Current Dataset does not contain Pairs with Key {currentSuiteName}");
                        suiteTestCases = new List<int>() { currentTestCaseNumber };
                        suiteTestCasePairs = new KeyValuePair<string, List<int>>(currentSuiteName, suiteTestCases);
                        SuiteTestCaseDataset.Add(suiteTestCasePairs);
                    }
                }
            }
        }

        public void GetTestRunDataset()
        {
            List<KeyValuePair<string, List<int>>> dataset = SuiteTestCaseDataset;
            int datasetCount = (int)dataset?.Count;

            for (int i = 0; i < datasetCount; i++)
            {
                KeyValuePair<string, List<int>> dataPair = dataset[i];
                var suiteName = dataPair.Key;
                var testCases = dataPair.Value;
                log.Info($"Suite Name: {suiteName}");

                for (int v = 0; v < testCases.Count; v++)
                {
                    int testCase = testCases[v];
                    log.Info($"TestCase Number: {testCase}");
                }
            }
        }
    }
}