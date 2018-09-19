using System;
using RestSharp;
using Newtonsoft.Json;
using RKCIUIAutomation.Config;
using System.Collections.Generic;
using RKCIUIAutomation.Page;
using NUnit.Framework.Interfaces;
using System.Net;

namespace RKCIUIAutomation.Tools
{
    public class HipTestApi : HipTestData
    {
        [ThreadStatic]
        private readonly IRestClient client;
        private readonly string _accessToken;
        private readonly string _clientId;
        private readonly string _userId;
        private readonly string _userPw;
        //private readonly static string proj_rkci = "95332";
        private readonly static string newRKCI = "100288";
        private readonly string ApiBase = $"https://app.hiptest.com/api/projects/{newRKCI}";

        [ThreadStatic]
        public List<KeyValuePair<int, List<object>>> hipTestRunData;


        public enum HipTestKey
        {
            HipTest_UID,
            HipTest_PWD,
            HipTest_Token,
            HipTest_ClientID
        }

        enum ResourceType
        {
            BuildIDs,
            TestRuns,
            TestResults,
            TestSnapshots            
        }

        enum TestRunType
        {
            Sync,
            GetTest
        }
                
        public HipTestApi()
        {
            client = new RestClient(ApiBase);
            ConfigUtils config = new ConfigUtils();
            _accessToken = config.GetHipTestCreds(HipTestKey.HipTest_Token);
            _clientId = config.GetHipTestCreds(HipTestKey.HipTest_ClientID);
            _userId = config.GetHipTestCreds(HipTestKey.HipTest_UID);
            _userPw = config.GetHipTestCreds(HipTestKey.HipTest_PWD);
        }
                
        public IRestRequest SetRequest(Method requestMethod, string resource)
        {
            RestRequest request = null;
            try
            {
                request = new RestRequest(requestMethod)
                {
                    Resource = resource
                };

                request
                    .AddHeader("Accept", "application/vnd.api+json; version=1")
                    .AddHeader("access-token", _accessToken)
                    .AddHeader("client", _clientId)
                    .AddHeader("uid", _userId);
                return request;
            }
            catch (Exception e)
            {
                log.Error($"Error occured in SetRequest Method.\n{e.Message}");
                throw;
            }
        }
      
        private IRestResponse ExecuteRequest(Method requestMethod, ResourceType resource, RootObject json = null, params object[] requestParams)
        {
            IRestResponse response = null;
            IRestRequest request = null;

            int buildId;
            int testRunId;
            int testResultId;
            int testSnapshotId;

            string endPoint = string.Empty;
            string content = string.Empty;

            try
            {
                switch (resource)
                {
                    case ResourceType.TestRuns:

                        if (requestMethod == Method.GET)
                        {
                            TestRunType testRunType = (TestRunType)requestParams[0];
                            testRunId = ConvertToType<int>(requestParams[1]);

                            if (testRunType == TestRunType.GetTest)
                            {
                                testSnapshotId = ConvertToType<int>(requestParams[3]);
                                bool allTests = ConvertToType<bool>(requestParams[2]);
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
                            testRunId = ConvertToType<int>(requestParams[0]);
                            buildId = ConvertToType<int>(requestParams[1]);
                            endPoint = $"/test_runs/{testRunId}/builds/{buildId}/test_results";
                        }
                        else if (requestMethod == Method.PATCH)
                        {
                            testRunId = ConvertToType<int>(requestParams[0]);
                            testSnapshotId = ConvertToType<int>(requestParams[1]);
                            testResultId = ConvertToType<int>(requestParams[2]);
                            endPoint = $"/test_runs/{testRunId}/test_snapshots/{testSnapshotId}/test_results/{testResultId}";
                        }
                        else
                        {
                            testRunId = ConvertToType<int>(requestParams[0]);
                            testSnapshotId = ConvertToType<int>(requestParams[1]);
                            endPoint = $"/test_runs/{testRunId}/test_snapshots/{testSnapshotId}?include=last-result";
                        }
                        break;
                    case ResourceType.TestSnapshots:
                        testRunId = ConvertToType<int>(requestParams[0]);
                        endPoint = $"/test_runs/{testRunId}/test_snapshots";
                        break;
                }

                request = SetRequest(requestMethod, endPoint);
                if(json != null)
                {
                    request.AddJsonBody(json);
                }
                
                log.Debug($"API Endpoint: {endPoint}");

                response = client.Execute(request);
                Console.WriteLine(response.Content);

                return response;
            }
            catch (Exception e)
            {
                var statusCode = response.StatusCode;
                log.Error($"CreateTestRun StatusCode: {statusCode.ToString()}\n{e.Message}");
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
            IRestResponse response = null;
            string content = string.Empty;

            try
            {
                string testSuite = testRunDetails[0];
                string testEnv = testRunDetails[1];
                string tenantName = testRunDetails[2];

                string testRunName = $"{testEnv}_{tenantName}_{testSuite}";
                string testDescription = $"UI Automation Run for: ({testEnv}){tenantName}\nStartTime: {DateTime.Now.ToShortDateString()}, {DateTime.Now.ToShortTimeString()}\nTestSuite: {testSuite}";

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

                response = ExecuteRequest(Method.POST, ResourceType.TestRuns, json);
                content = response.Content;
                
                try
                {
                    //Get Test Run ID
                    RootObject root = JsonConvert.DeserializeObject<RootObject>(content);
                    hiptestRunId = root.data.id;
                    log.Debug($"Created TestRun:\nName: {testRunName}\n ID: {hiptestRunId}");
                    return hiptestRunId;
                }
                catch (Exception e)
                {
                    var statusCode = response?.StatusCode;
                    log.Error($"GetTestRunID StatusCode: {statusCode.ToString()}\n{e.Message}");
                    throw;
                }
            }
            catch (Exception e)
            {
                var statusCode = response?.StatusCode;
                log.Error($"CreateTestRun StatusCode: {statusCode.ToString()}\n{e.Message}");
                throw;
            }
        }


        /// <summary>
        /// Creates a List of KeyValue Pairs containing testCaseNumber as key and List with index [0](int)testRunId, [1](int)scenarioSnapshotId, [2](int)lastResultId as value
        /// </summary>
        /// <param name="testRunId"></param>
        /// <returns></returns>
        public void BuildTestRunSnapshotData(int testRunId, int testCaseNumber)
        {
            IRestResponse response = null;
            string content = string.Empty;
            string testName = string.Empty;
            int scenarioSnapshotId = -1;
            int lastResultId = -1;

            var requestParams = new object[]
            {
                testRunId
            };

            try
            {
                response = ExecuteRequest(Method.GET, ResourceType.TestSnapshots, null, requestParams);
                content = response.Content;

                DatumList dataList = new DatumList();
                dataList = (DatumList)JsonConvert.DeserializeObject(content, typeof(DatumList));
                
                int dataCount = dataList.data.Count;
                for (int i = 0; i < dataCount; i++)
                {
                    //tcNumber = dataList.data[i].id;
                    testName = dataList.data[i].attributes.name;
                    scenarioSnapshotId = dataList.data[i].attributes.scenario_snapshot_id;
                    log.Debug($"\n### TestSnapshotID: {testCaseNumber}\n### TestName: {testName}\n### ScenarioSnapshotID: {scenarioSnapshotId}");

                    try
                    {
                        requestParams = new object[]
                        {
                            testRunId,
                            testCaseNumber
                        };

                        response = ExecuteRequest(Method.GET, ResourceType.TestResults, null, requestParams);
                        content = response.Content;
                        
                        RootObject root = new RootObject();
                        root = (RootObject)JsonConvert.DeserializeObject(content, typeof(RootObject));

                        lastResultId = root.included[0].id;
                        log.Debug($"\n### LastResultID: {lastResultId}\n");
                    }
                    catch (Exception e)
                    {
                        var statusCode = response?.StatusCode;
                        log.Error($"TestResult StatusCode: {statusCode.ToString()}\n{e.Message}");
                        throw;
                    }
                }

                List<object> scenarioSnapshotData = new List<object>
                {
                    testRunId,
                    scenarioSnapshotId,
                    lastResultId
                };

                hipTestRunData = new List<KeyValuePair<int, List<object>>>
                {
                    new KeyValuePair<int, List<object>>(testCaseNumber, scenarioSnapshotData)
                };                
            }
            catch (Exception e)
            {
                var statusCode = response?.StatusCode;
                log.Error($"ScenarioSnapshot StatusCode: {statusCode.ToString()}\n{e.Message}");
                throw;
            }
        }

        /// <summary>
        /// Updates Hiptest TestRun with TestStatus and Description
        /// </summary>
        /// <param name="testCaseNumber"></param>
        /// <param name="testStatus"></param>
        /// <param name="description"></param>
        public void UpdateHipTestRunData(int testCaseNumber, TestStatus testStatus, string description)
        {
            IRestResponse response = null;

            int testRunId;
            int testSnapshotId;
            int testResultId;

            var kvPairsCount = hipTestRunData.Count;
            try
            {
                PageHelper pageHelper = new PageHelper();

                for (int i = 0; i < kvPairsCount; i++)
                {
                    KeyValuePair<int, List<object>> pair = hipTestRunData[i];

                    if (pair.Key == testCaseNumber)
                    {
                        List<object> snapshotData = pair.Value;

                        var requestParams = new object[]
                        {
                            testRunId = (int)snapshotData[0],
                            testSnapshotId = (int)snapshotData[1],
                            testResultId = (int)snapshotData[2]
                        };

                        var json = new RootObject
                        {
                            data = new Datum
                            {
                                type = "test-results",
                                id = testResultId,
                                attributes = new Attributes
                                {
                                    status = testStatus.ToString().ToLower(),
                                    status_author = "RKCIUIAutomation",
                                    description = description
                                }
                            }
                        };

                        response = ExecuteRequest(Method.PATCH, ResourceType.TestResults, json, requestParams);
                    }
                }
            }
            catch (Exception e)
            {
                var statusCode = response?.StatusCode;
                log.Error($"StatusCode: {statusCode.ToString()}\n{e.Message}");
                throw;
            }
        }


        /// <summary>
        /// Provide taskParams[] with index: [0](int)TestRunID, [1](bool)AllTests, **[2](int)testSnapshotID
        /// **testSnapshotID is not required if performing Sync for AllTests
        /// </summary>
        /// <param name="taskParams"></param>
        /// <returns></returns>
        public IRestResponse GetTestRun(object[] taskParams) => GetTestRunTask(TestRunType.GetTest, taskParams);
        
        /// <summary>
        /// Syncs test results for test cases in a TestRun
        /// </summary>
        /// <param name="taskParams"></param>
        /// <returns></returns>
        public void SyncTestRun(int testRunId) => GetTestRunTask(TestRunType.Sync, testRunId);
        
        private IRestResponse GetTestRunTask<T>(TestRunType testRunType, T taskParams)
        {
            IRestResponse response = null;
            Type paramType = taskParams.GetType();
            int testBuildId = -1;

            try
            {
                if (paramType == typeof(int))
                {
                    testBuildId = ConvertToType<int>(taskParams);
                }
                else if (paramType == typeof(object[]))
                {
                    testBuildId = (int)ConvertToType<object[]>(taskParams)[0];
                }

                var testRunTaskParms = new object[]
                {
                    testRunType,
                    testBuildId,
                };

                response = ExecuteRequest(Method.GET, ResourceType.TestRuns, null, testRunTaskParms);
            }
            catch (Exception e)
            {
                var statusCode = response?.StatusCode;
                log.Error($"ScenarioSnapshot StatusCode: {statusCode.ToString()}\n{e.Message}");
                throw;
            }

            return response;
        }


        public IRestResponse UpdateTestResult(int testRunId, int testSnapshotId, int testResultId, string testStatus, string description)
        {
            IRestResponse response = null;

            try
            {
                var requestParams = new object[]
                {
                    testRunId,
                    testSnapshotId,
                    testResultId
                };

                var json = new RootObject
                {
                    data = new Datum
                    {
                        type = "test-results",
                        id = testResultId,
                        attributes = new Attributes
                        {
                            status = testStatus.ToLower(),
                            status_author = "RKCIUIAutomation",
                            description = description
                        }
                    }
                };

                response = ExecuteRequest(Method.PATCH, ResourceType.TestResults, json, requestParams);
                log.Debug($"RESPONSE: {response.Content}");
                return response;
            }
            catch (Exception e)
            {
                var content = (response == null) ? "Null Response" : response.Content;
                log.Error($"{content}\n{e.Message}");
                throw;
            }
        }

        public IRestResponse AssignResultsToTestRunBuild(string testRunId, int buildId, int testId, string testStatus, string description)
        {
            IRestResponse response = null;

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

                response = ExecuteRequest(Method.POST, ResourceType.TestResults, json, requestParams);
                return response;
            }
            catch (Exception e)
            {
                var content = (response == null) ? "Null Response" : response.Content;
                log.Error($"{content}\n{e.Message}");
                throw;
            }
        }

        public void ReadJSON(string json)
        {
            if (!string.IsNullOrEmpty(json))
            {
                RootObject root = JsonConvert.DeserializeObject<RootObject>(json);
                int testRunId = root.data.id;
                Console.WriteLine($"TEST RUN ID: {testRunId}");
            }
        }

    }
}
