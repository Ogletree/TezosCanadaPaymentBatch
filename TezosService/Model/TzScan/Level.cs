using Newtonsoft.Json;

namespace TezosService.Model.TzScan
{
    public class Level
    {
        [JsonProperty("level")]
        public long BlockLevel { get; set; }

        [JsonProperty("level_position")]
        public long LevelPosition { get; set; }

        [JsonProperty("cycle")]
        public long Cycle { get; set; }

        [JsonProperty("cycle_position")]
        public long CyclePosition { get; set; }

        [JsonProperty("voting_period")]
        public long VotingPeriod { get; set; }

        [JsonProperty("voting_period_position")]
        public long VotingPeriodPosition { get; set; }

        [JsonProperty("expected_commitment")]
        public bool ExpectedCommitment { get; set; }
        public static Level FromJson(string json) => JsonConvert.DeserializeObject<Level>(json, Converter.Converter.Settings);
        public static string ToJson(Level self) => JsonConvert.SerializeObject(self, Converter.Converter.Settings);

        public long CycleEndBlock()
        {
            return LevelPosition + (4096 - CyclePosition);
        }
    }
}
