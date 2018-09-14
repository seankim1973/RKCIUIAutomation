using Newtonsoft.Json;
using RestSharp;
using RKCIUIAutomation.Config;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;
using static RKCIUIAutomation.Tools.DataClass;

namespace RKCIUIAutomation.Tools
{
    public class HipTestApi : TestBase
    {
        [ThreadStatic]
        readonly IRestClient client;
        readonly string _accessToken;
        readonly string _clientId;
        readonly string _userId;
        readonly string _userPw;
        readonly static string proj_rkci = "95332";
        readonly static string proj_sandbox = "658856";
        readonly string ApiBase = $"https://app.hiptest.com/api/projects/{proj_rkci}";

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
                
        public HipTestApi()
        {
            client = new RestClient(ApiBase);
            ConfigUtils config = new ConfigUtils();
            _accessToken = config.GetHipTestCreds(HipTestKey.HipTest_Token);
            _clientId = config.GetHipTestCreds(HipTestKey.HipTest_ClientID);
            _userId = config.GetHipTestCreds(HipTestKey.HipTest_UID);
            _userPw = config.GetHipTestCreds(HipTestKey.HipTest_PWD);
        }
                
#pragma warning disable IDE0017 // Simplify object initialization
        public IRestRequest SetRequest(Method requestMethod, string resource)
        {
            var request = new RestRequest(requestMethod);
            request.Resource = resource;
            request
                .AddHeader("Accept", "application/vnd.api+json; version=1")
                .AddHeader("access-token", _accessToken)
                .AddHeader("client", _clientId)
                .AddHeader("uid", _userId);
            return request;
        }
      
        private IRestResponse ExecuteRequest<T>(Method requestMethod, ResourceType resource, params T[] postParams)
        {
            IRestResponse response = null;
            IRestRequest request = null;
            object json = null;

            int testId;
            int buildId;
            int[] scenarioIDs;
            string testStatus;
            string testRunId;
            int testResultId;
            string testRunName;
            string testDescription;
            string endPoint = string.Empty;
            string content = string.Empty;

            try
            {
                switch (resource)
                {
                    case ResourceType.TestRuns:
                        testRunName = ConvertToType<string>(postParams[0]);
                        scenarioIDs = ConvertToType<int[]>(postParams[1]);
                        endPoint = "/test_runs";
                        json = JSON_CreateTestRun(testRunName, scenarioIDs);
                        break;
                    case ResourceType.TestResults:
                        if (requestMethod == Method.POST)
                        {
                            testRunId = ConvertToType<string>(postParams[0]);
                            buildId = ConvertToType<int>(postParams[1]);
                            testId = ConvertToType<int>(postParams[2]);
                            testStatus = ConvertToType<string>(postParams[3]);
                            testDescription = ConvertToType<string>(postParams[4]);
                            endPoint = $"/test_runs/{testRunId}/builds/{buildId}/test_results";
                            json = JSON_AssignResultsToTestRunBuild(testRunId, buildId, testId, testStatus, testDescription); 
                        }
                        else if (requestMethod == Method.PATCH)
                        {
                            testRunId = ConvertToType<string>(postParams[0]);
                            testId = ConvertToType<int>(postParams[1]);
                            testResultId = ConvertToType<int>(postParams[2]);
                            testStatus = ConvertToType<string>(postParams[3].ToString());
                            testDescription = ConvertToType<string>(postParams[4]);
                            endPoint = $"/test_runs/{testRunId}/test_snapshots/{testId}/test_results/{testResultId}";
                            json = JSON_UpdateTestResult(testResultId, testStatus, testDescription);
                        }
                        else
                        {
                            testRunId = ConvertToType<string>(postParams[0]);
                            testId = ConvertToType<int>(postParams[1]);
                            endPoint = $"/test_runs/{testRunId}/test_snapshots/{testId}?include=last-result";
                        }
                        break;
                    case ResourceType.TestSnapshots:
                        testRunId = ConvertToType<string>(postParams[0]);
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
                return response;
            }
            catch (Exception e)
            {
                log.Error($"{response.Content}\n{e}");
                throw;
            }
        }


        public IRestResponse CreateTestRun(string testRunName, int[] scenarioIDs)
        {            
            string content = string.Empty;
            try
            {
                var postParams = new object[]
                {
                    testRunName,
                    scenarioIDs
                };

                IRestResponse response = ExecuteRequest(Method.POST, ResourceType.TestRuns, postParams);
                content = response.Content;
                return response;
            }
            catch (Exception e)
            {
                log.Error($"{content}\n{e}");
                throw;
            }       
        }

        public string GetTestRunId(IRestResponse response)
        {
            string content = string.Empty;
            string testRunId = string.Empty;

            try
            {
                content = response.Content;
                if (!string.IsNullOrEmpty(content))
                {
                    RootObject root = JsonConvert.DeserializeObject<RootObject>(content);
                    testRunId = (root.data.id).ToString();
                }
                
                Console.WriteLine($"TEST RUN ID: {testRunId}");
                return testRunId;
            }
            catch (Exception e)
            {
                log.Error($"{content}\n{e}");
                throw;
            }
        }

        public List<KeyValuePair<string, int>> GetTestRunSnapshotDetails(string testRunId)
        {
            string content = string.Empty;
            IRestResponse response = null;

            var postParams = new object[]
            {
                testRunId
            };

            try
            {
                response = ExecuteRequest(Method.GET, ResourceType.TestSnapshots, postParams);
                content = response.Content;

                DataList root = new DataList();
                root = (DataList)JsonConvert.DeserializeObject(content, typeof(DataList));

                int dataCount = root.data.Count;
                List<KeyValuePair<string, int>> snapshotKVs = new List<KeyValuePair<string, int>>();

                for (int i = 0; i < dataCount; i++)
                {
                    int testSnapshotID = root.data[i].id;
                    string testName = root.data[i].attributes.name;
                    snapshotKVs.Add(new KeyValuePair<string, int>(testName, testSnapshotID));
                }

                return snapshotKVs;
            }
            catch (Exception e)
            {
                content = response == null ? "NULL Response" : content;
                log.Error($"{content}\n{e.Message}");
                throw;
            }
        }

        public IRestResponse GetTestSnapshotResultsID(string testRunId)
        {
            IRestResponse response = null;
            string content = string.Empty;

            try
            {
                List<KeyValuePair<string, int>> kvPairs = GetTestRunSnapshotDetails(testRunId);
                for (int i = 0; i < kvPairs.Count; i++)
                {
                    string testName = kvPairs[i].Key;
                    int testSnapshotId = kvPairs[i].Value;
                    Console.WriteLine($"TestName: {testName}\nTestSnapshotId{testSnapshotId}");
                    var postParams = new object[]
                        {
                            testRunId,
                            testSnapshotId
                        };

                    response = ExecuteRequest(Method.GET, ResourceType.TestResults, postParams);
                    content = response.Content;

                    RootObject root = new RootObject();
                    root = (RootObject)JsonConvert.DeserializeObject(content, typeof(RootObject));

                    string lastResultId = root.included[0].id;
                    Console.WriteLine($"LastResult ID: {lastResultId}\n");
                }

                return response;
            }
            catch (Exception e)
            {
                content = response == null ? "NULL Response" : content;
                log.Error($"{content}\n{e.Message}");
                throw;
            }
            
        }

        public IRestResponse UpdateTestResult(string testRunId, int testId, string testResultId, string testStatus, string description)
        {
            IRestResponse response = null;

            try
            {
                var postParams = new object[]
                    {
                        testRunId,
                        testId,
                        testResultId,
                        testStatus,
                        description
                    };

                response = ExecuteRequest(Method.PATCH, ResourceType.TestResults, postParams);
                Console.WriteLine($"RESPONSE: {response.Content}");
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
                var postParams = new object[]
                    {
                        testRunId,
                        buildId,
                        testId,
                        testStatus,
                        description
                    };

                response = ExecuteRequest(Method.POST, ResourceType.TestResults, postParams);
                return response;
            }
            catch (Exception e)
            {
                log.Error($"{response.Content}\n{e.Message}");
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


        private RootObject JSON_AssignResultsToTestRunBuild(string testRunId, int buildId, int testId, string testStatus, string description)
        {
            var root = new RootObject
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
                        
            return root;
        }

        private RootObject JSON_UpdateTestResult(int testResultId, string testStatus, string description)
        {
            var root = new RootObject
            {
                data = new Datum
                {
                    type = "test-results",
                    id = testResultId,
                    attributes = new Attributes
                    {
                        status = testStatus,
                        status_author = "RKCIUIAutomation",
                        description = description
                    }
                }
            };
            return root;
        }

        private RootObject JSON_CreateTestRun(string testRunName, int[] scenarioIDs)
        {
            var root = new RootObject
            {
                data = new Datum
                {
                    attributes = new Attributes
                    {
                        name = testRunName,
                        scenario_ids = scenarioIDs
                    }
                }
            };
            return root;
        }



    }

#pragma warning disable IDE1006 // Naming Styles
    public class DataClass
    {
        public class Statuses
        {
            public int passed { get; set; }
            public int failed { get; set; }
            public int wip { get; set; }
            public int retest { get; set; }
            public int blocked { get; set; }
            public int skipped { get; set; }
            public int undefined { get; set; }
        }
        
        public class Attributes
        {
            [JsonProperty("scenario-snapshot-id")]
            public int scenario_snapshot_id { get; set; }
            [JsonProperty("created-at")]
            public DateTime created_at { get; set; }
            [JsonProperty("updated-at")]
            public DateTime updated_at { get; set; }
            [JsonProperty("last-author")]
            public string last_author { get; set; }
            public string name { get; set; }
            public string description { get; set; }
            public Statuses statuses { get; set; }
            public string status { get; set; }
            [JsonProperty("status-author")]
            public string status_author { get; set; }
            public bool archived { get; set; }
            public int[] scenario_ids { get; set; }
        }

        public class Links
        {
            public string self { get; set; }
            public string related { get; set; }
        }

        public class Tags
        {
        }

        public class BuildData
        {
            public string type { get; set; }
            public int id { get; set; }
        }

        public class Build
        {
            public BuildData data { get; set; }
        }

        public class TestData
        {
            public string type { get; set; }
            public int id { get; set; }
        }

        public class TestSnapshot
        {
            public TestData data { get; set; }
        }

        public class LastResultData
        {
            public string type { get; set; }
            public string id { get; set; }
        }

        public class TagSnapshots
        {
        }

        public class LastResult
        {
            public Links links { get; set; }
            public LastResultData data { get; set; }

            public Tags tags { get; set; }
            [JsonProperty("tag-snapshots")]
            public TagSnapshots tagSnapshots { get; set; }           
        }

        public class Relationships
        {
            [JsonProperty("test-snapshot")]
            public TestSnapshot testSnapshot { get; set; }
            [JsonProperty("last-result")]
            public LastResult lastResult { get; set; }
            public Tags tags { get; set; }
            public Build build { get; set; }
        }
                        
        public class Datum
        {
            public string type { get; set; }
            public int id { get; set; }
            public Attributes attributes { get; set; }  
            public Links links { get; set; }
            public Relationships relationships { get; set; }
        }

        public class DataList
        {
            public List<Datum> data { get; set; }
        }

        public class Included
        {
            public string type { get; set; }
            public string id { get; set; }
        }

        public class RootObject
        {
            public Datum data { get; set; }
            public List<Included> included { get; set; }
        }

    }




}
