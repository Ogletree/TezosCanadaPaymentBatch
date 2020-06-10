using System.Collections.Generic;
using Newtonsoft.Json;
using Tz.Net;
using Tz.Net.Extensions;

namespace TezosService.Model.TzScan
{
    public class BakerRewards
    {
        public override string ToString()
        {
            return $"{Cycle} {DelegateStakingBalance.ToTez()} {Status.Status}";
        }

        [JsonProperty("cycle")]
        public long Cycle { get; set; }

        [JsonProperty("delegate_staking_balance")]
        public BigFloat DelegateStakingBalance { get; set; }

        [JsonProperty("delegators_nb")]
        public long DelegatorsNb { get; set; }

        [JsonProperty("delegated_balance")]
        public long DelegatedBalance { get; set; }

        [JsonProperty("blocks_rewards")]
        public long BlocksRewards { get; set; }

        [JsonProperty("endorsements_rewards")]
        public long EndorsementsRewards { get; set; }

        [JsonProperty("fees")]
        public long Fees { get; set; }

        [JsonProperty("future_baking_rewards")]
        public long FutureBakingRewards { get; set; }

        [JsonProperty("future_endorsing_rewards")]
        public long FutureEndorsingRewards { get; set; }

        [JsonProperty("status")]
        public StatusClass Status { get; set; }

        [JsonProperty("gain_from_denounciation")]
        public long GainFromDenounciation { get; set; }

        [JsonProperty("lost_deposit_from_denounciation")]
        public long LostDepositFromDenounciation { get; set; }

        [JsonProperty("lost_rewards_denounciation")]
        public long LostRewardsDenounciation { get; set; }

        [JsonProperty("lost_fees_denounciation")]
        public long LostFeesDenounciation { get; set; }

        [JsonProperty("revelation_rewards")]
        public long RevelationRewards { get; set; }

        [JsonProperty("lost_revelation_rewards")]
        public long LostRevelationRewards { get; set; }

        [JsonProperty("lost_revelation_fees")]
        public long LostRevelationFees { get; set; }
        public static List<BakerRewards> FromJson(string json) => JsonConvert.DeserializeObject<List<BakerRewards>>(json, Converter.Converter.Settings);
        public static string ToJson(List<BakerRewards> self) => JsonConvert.SerializeObject(self, Converter.Converter.Settings);
    }

    public partial class DelegateRewards
    {
        [JsonProperty("cycle")]
        public long Cycle { get; set; }

        [JsonProperty("delegate")]
        public Delegatez Delegate { get; set; }

        [JsonProperty("staking_balance")]
        public decimal StakingBalance { get; set; }

        [JsonProperty("balance")]
        public decimal Balance { get; set; }

        [JsonProperty("rewards")]
        public decimal Rewards { get; set; }

        [JsonProperty("extra_rewards")]
        public decimal ExtraRewards { get; set; }

        [JsonProperty("losses")]
        public decimal Losses { get; set; }

        [JsonProperty("status")]
        public StatusClass Status { get; set; }

        [JsonProperty("block_rewards")]
        public decimal BlockRewards { get; set; }

        [JsonProperty("endorsement_rewards")]
        public decimal EndorsementRewards { get; set; }

        [JsonProperty("fees")]
        public decimal Fees { get; set; }

        [JsonProperty("revelation_rewards")]
        public decimal RevelationRewards { get; set; }

        [JsonProperty("denounciation_gain")]
        public decimal DenounciationGain { get; set; }

        [JsonProperty("revelation_lost_rewards")]
        public decimal RevelationLostRewards { get; set; }

        [JsonProperty("revelation_lost_fees")]
        public decimal RevelationLostFees { get; set; }

        [JsonProperty("denounciation_lost_deposit")]
        public decimal DenounciationLostDeposit { get; set; }

        [JsonProperty("denounciation_lost_rewards")]
        public decimal DenounciationLostRewards { get; set; }

        [JsonProperty("denounciation_lost_fees")]
        public long DenounciationLostFees { get; set; }
        public static List<DelegateRewards> FromJson(string json) => JsonConvert.DeserializeObject<List<DelegateRewards>>(json, Converter.Converter.Settings);
        public static string ToJson(List<DelegateRewards> self) => JsonConvert.SerializeObject(self, Converter.Converter.Settings);
    }
    public partial class Delegatez
    {
        [JsonProperty("tz")]
        public string Tz { get; set; }
    }
}