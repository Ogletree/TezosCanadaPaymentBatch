using Newtonsoft.Json;

namespace TezosService.Model.TzScan
{
    public class Baker
    {
        [JsonProperty("tz")]
        public string Tz { get; set; }

        [JsonProperty("alias", NullValueHandling = NullValueHandling.Ignore)]
        public string Alias { get; set; }
    }
}