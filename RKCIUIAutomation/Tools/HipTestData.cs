using Newtonsoft.Json;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using static RKCIUIAutomation.Base.BaseUtils;

#pragma warning disable IDE1006 // Naming Styles

namespace RKCIUIAutomation.Tools
{
    extern alias newtJson;

    public static class HipTest
    {
        [ThreadStatic]
        private static int _testRunId;

        [ThreadStatic]
        private static string[] _testRunDetails;

        [ThreadStatic]
        private static List<int> _hiptestRunTestCaseIDs;

        [ThreadStatic]
        private static List<KeyValuePair<int, List<int>>> _hiptestRunData;

        [ThreadStatic]
        private static List<KeyValuePair<int, KeyValuePair<TestStatus, string>>> _hipTestResults;

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static int CreateTestRunInstance(this HipTestApi hipTestInstance, List<int> scenarioIDs, string[] testRunDetails)
        {
            try
            {
                _testRunId = hipTestInstance.CreateTestRun(scenarioIDs, testRunDetails);
                return _testRunId;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }
            finally
            {
                Thread.Sleep(5000);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static string[] SetTestRunDetails(this HipTestApi hipTestInstance, string[] testRunDetails)
        {
            try
            {
                if (_testRunDetails == null || !_testRunDetails.Any())
                {
                    _testRunDetails = new string[] { };
                    _testRunDetails = testRunDetails;
                }
                else
                {
                    _testRunDetails = testRunDetails;
                }

                return _testRunDetails;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<int> SetTestCaseID(this HipTestApi hipTestInstance, int testCaseNumber)
        {
            try
            {
                if (_hiptestRunTestCaseIDs == null || !_hiptestRunTestCaseIDs.Any())
                {
                    return _hiptestRunTestCaseIDs = new List<int>()
                    {
                        testCaseNumber
                    };
                }
                else
                {
                    _hiptestRunTestCaseIDs.Add(testCaseNumber);
                }

                return _hiptestRunTestCaseIDs;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<KeyValuePair<int, KeyValuePair<TestStatus, string>>> SetTestResultInstance(this HipTestApi hipTestInstance, KeyValuePair<int, KeyValuePair<TestStatus, string>> testResult)
        {
            try
            {
                if (_hipTestResults == null || !_hipTestResults.Any())
                {
                    _hipTestResults = new List<KeyValuePair<int, KeyValuePair<TestStatus, string>>>()
                    {
                        testResult
                    };
                }
                else
                {
                    _hipTestResults.Add(testResult);
                }

                return _hipTestResults;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static List<KeyValuePair<int, List<int>>> BuildTestRunSnapshotDataInstance(this HipTestApi hipTestInstance, int testRunId)
        {
            try
            {
                _hiptestRunData = new List<KeyValuePair<int, List<int>>>();
                _hiptestRunData = hipTestInstance.BuildTestRunSnapshotData(testRunId);
                return _hiptestRunData;
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void UpdateHipTestRunDataInstance(this HipTestApi hipTestInstance, List<KeyValuePair<int, List<int>>> hiptestRunData, List<KeyValuePair<int, KeyValuePair<TestStatus, string>>> hipTestResults)
        {
            try
            {
                hipTestInstance.UpdateHipTestRunData(hiptestRunData, hipTestResults);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }
            finally
            {
                Thread.Sleep(5000);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void SyncTestRunInstance(this HipTestApi hipTestInstance, int testRunId)
        {
            try
            {
                hipTestInstance.SyncTestRun(testRunId);
            }
            catch (Exception e)
            {
                log.Debug(e.Message);
                throw;
            }
        }

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
            [newtJson.Newtonsoft.Json.JsonProperty("scenario-snapshot-id")]
            public int scenario_snapshot_id { get; set; }

            [newtJson.Newtonsoft.Json.JsonProperty("created-at")]
            public DateTime created_at { get; set; }

            [newtJson.Newtonsoft.Json.JsonProperty("updated-at")]
            public DateTime updated_at { get; set; }

            [newtJson.Newtonsoft.Json.JsonProperty("last-author")]
            public string last_author { get; set; }

            public string name { get; set; }
            public string description { get; set; }
            public Statuses statuses { get; set; }
            public string status { get; set; }

            [newtJson.Newtonsoft.Json.JsonProperty("status-author")]
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
            public List<TagData> data { get; set; }
        }

        public class TagData
        {
            public string type { get; set; }
            public string key { get; set; }
            public string value { get; set; }
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

            [newtJson.Newtonsoft.Json.JsonProperty("tag-snapshots")]
            public TagSnapshots tagSnapshots { get; set; }
        }

        public class ScenarioData
        {
            public string type { get; set; }
            public int id { get; set; }
        }

        public class Scenario
        {
            public ScenarioData data { get; set; }
        }

        public class Relationships
        {
            [newtJson.Newtonsoft.Json.JsonProperty("test-snapshot")]
            public TestSnapshot testSnapshot { get; set; }

            [newtJson.Newtonsoft.Json.JsonProperty("last-result")]
            public LastResult lastResult { get; set; }

            public Tags tags { get; set; }
            public Build build { get; set; }
            public Scenario scenario { get; set; }
        }

        public class Datum
        {
            public string type { get; set; }
            public int id { get; set; }
            public Attributes attributes { get; set; }
            public Links links { get; set; }
            public Relationships relationships { get; set; }
        }

        public class DatumList
        {
            public List<Datum> data { get; set; }
        }

        public class Included
        {
            public string type { get; set; }
            public int id { get; set; }
        }

        public class RootObject
        {
            public Datum data { get; set; }
            public List<Included> included { get; set; }
        }
    }
}

#pragma warning restore IDE1006 // Naming Styles