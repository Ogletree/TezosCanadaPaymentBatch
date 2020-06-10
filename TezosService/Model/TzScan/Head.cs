using System;
using Newtonsoft.Json;

namespace TezosService.Model.TzScan
{
    public class Head
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("predecessor_hash")]
        public string PredecessorHash { get; set; }

        [JsonProperty("fitness")]
        public string Fitness { get; set; }

        [JsonProperty("timestamp")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonProperty("validation_pass")]
        public long ValidationPass { get; set; }

        [JsonProperty("operations")]
        public object[][] Operations { get; set; }

        [JsonProperty("protocol")]
        public Protocol Protocol { get; set; }

        [JsonProperty("test_protocol")]
        public Protocol TestProtocol { get; set; }

        [JsonProperty("network")]
        public string Network { get; set; }

        [JsonProperty("test_network")]
        public string TestNetwork { get; set; }

        [JsonProperty("test_network_expiration")]
        public string TestNetworkExpiration { get; set; }

        [JsonProperty("baker")]
        public Baker Baker { get; set; }

        [JsonProperty("nb_operations")]
        public long NbOperations { get; set; }

        [JsonProperty("priority")]
        public long Priority { get; set; }

        [JsonProperty("level")]
        public long Level { get; set; }

        [JsonProperty("commited_nonce_hash")]
        public string CommitedNonceHash { get; set; }

        [JsonProperty("pow_nonce")]
        public string PowNonce { get; set; }

        [JsonProperty("proto")]
        public long Proto { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("volume")]
        public long Volume { get; set; }

        [JsonProperty("fees")]
        public long Fees { get; set; }

        [JsonProperty("distance_level")]
        public long DistanceLevel { get; set; }
        public static Head FromJson(string json) => JsonConvert.DeserializeObject<Head>(json, Converter.Converter.Settings);
        public static string ToJson(Head self) => JsonConvert.SerializeObject(self, Converter.Converter.Settings);
    }
}
