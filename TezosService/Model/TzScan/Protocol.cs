using Newtonsoft.Json;

namespace TezosService.Model.TzScan
{
    public class Protocol
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }
    }
}