using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace TezosService.Model.TzScan
{
    public class Account
    {
        [JsonProperty("tz")]
        public string Tz { get; set; }
    }

    public class Gunk
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("block_hash")]
        public string BlockHash { get; set; }

        [JsonProperty("network_hash")]
        public string NetworkHash { get; set; }

        [JsonProperty("type")]
        public TypeClass Type { get; set; }


        public static List<Gunk> FromJson(string json) => JsonConvert.DeserializeObject<List<Gunk>>(json, Converter.Converter.Settings);
       public static string ToJson(List<Gunk> self) => JsonConvert.SerializeObject(self, Converter.Converter.Settings);
    }

    public partial class TypeClass
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("source")]
        public Source Source { get; set; }

        [JsonProperty("operations")]
        public List<Operation> Operations { get; set; }
    }

    public partial class Operation
    {
        [JsonProperty("kind")]
        public string Kind { get; set; }

        [JsonProperty("src")]
        public Source Src { get; set; }

        [JsonProperty("amount")]
        public long Amount { get; set; }

        [JsonProperty("destination")]
        public Source Destination { get; set; }

        [JsonProperty("failed")]
        public bool Failed { get; set; }

        [JsonProperty("internal")]
        public bool Internal { get; set; }

        [JsonProperty("burn")]
        public long Burn { get; set; }

        [JsonProperty("counter")]
        public long Counter { get; set; }

        [JsonProperty("fee")]
        public long Fee { get; set; }

        [JsonProperty("gas_limit")]
        public long GasLimit { get; set; }

        [JsonProperty("storage_limit")]
        public long StorageLimit { get; set; }

        [JsonProperty("op_level")]
        public long OpLevel { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("str_parameters", NullValueHandling = NullValueHandling.Ignore)]
        public string StrParameters { get; set; }
    }

    public partial class Source
    {
        [JsonProperty("tz")]
        public string Tz { get; set; }
    }
}