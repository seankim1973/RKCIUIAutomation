using Newtonsoft.Json;
using RKCIUIAutomation.Test;
using System;
using System.Collections.Generic;

#pragma warning disable IDE1006 // Naming Styles
namespace RKCIUIAutomation.Tools
{
    public class HipTestData : TestBase
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
