using Newtonsoft.Json;

namespace TezosService.Model.TzScan
{
    public class StatusClass
    {
        [JsonProperty("status")]
        public StatusEnum Status { get; set; }
    }
}