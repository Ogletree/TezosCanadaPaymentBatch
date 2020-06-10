using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Tz.Net;

namespace TezosService.Model.TzScan
{
    public class RewardSplit
    {
        [JsonProperty("delegate_staking_balance")]
        public BigFloat DelegateStakingBalance { get; set; }

        [JsonProperty("delegators_nb")]
        public long DelegatorsNb { get; set; }

        [JsonProperty("delegators_balance")]
        private List<DelegatorsBalance> DelegatorsBalance { get; set; }

        [JsonProperty("blocks_rewards")]
        public BigFloat BlocksRewards { get; set; }

        [JsonProperty("endorsements_rewards")]
        public BigFloat EndorsementsRewards { get; set; }

        [JsonProperty("fees")]
        public BigFloat Fees { get; set; }

        [JsonProperty("future_blocks_rewards")]
        public long FutureBlocksRewards { get; set; }

        [JsonProperty("future_endorsements_rewards")]
        public long FutureEndorsementsRewards { get; set; }

        [JsonProperty("gain_from_denounciation_baking")]
        public BigFloat GainFromDenounciationBaking { get; set; }

        [JsonProperty("lost_deposit_from_denounciation_baking")]
        public BigFloat LostDepositFromDenounciationBaking { get; set; }

        [JsonProperty("lost_rewards_denounciation_baking")]
        public BigFloat LostRewardsDenounciationBaking { get; set; }

        [JsonProperty("lost_fees_denounciation_baking")]
        public BigFloat LostFeesDenounciationBaking { get; set; }

        [JsonProperty("gain_from_denounciation_endorsement")]
        public BigFloat GainFromDenounciationEndorsement { get; set; }

        [JsonProperty("lost_deposit_from_denounciation_endorsement")]
        public BigFloat LostDepositFromDenounciationEndorsement { get; set; }

        [JsonProperty("lost_rewards_denounciation_endorsement")]
        public BigFloat LostRewardsDenounciationEndorsement { get; set; }

        [JsonProperty("lost_fees_denounciation_endorsement")]
        public BigFloat LostFeesDenounciationEndorsement { get; set; }

        [JsonProperty("revelation_rewards")]
        public BigFloat RevelationRewards { get; set; }

        [JsonProperty("lost_revelation_rewards")]
        public BigFloat LostRevelationRewards { get; set; }

        [JsonProperty("lost_revelation_fees")]
        public BigFloat LostRevelationFees { get; set; }
        public static RewardSplit FromJson(string json) => JsonConvert.DeserializeObject<RewardSplit>(json, Converter.Converter.Settings);
        public static string ToJson(RewardSplit self) => JsonConvert.SerializeObject((object)self, (JsonSerializerSettings)Converter.Converter.Settings);

        public List<DelegatorsBalance> GetDelegatorsBalance()
        {
            var delegatorsBalances = DelegatorsBalance.Where(x => x.Balance > 0 &&
                !Singleton.Instance.GetBurners().Contains(x.Account.Tz)).ToList();
            return delegatorsBalances;
        }

        public decimal GetDelegatePercentage(DelegatorsBalance delegatorsBalance)
        {
            var percent = delegatorsBalance.Balance / (decimal)DelegateStakingBalance;
            return percent;
        }

        public BigFloat GetTotalReward()
        {
            var totalReward = BlocksRewards 
                              + EndorsementsRewards 
                              + Fees 
                              + RevelationRewards 
                              + GainFromDenounciationBaking
                              + GainFromDenounciationEndorsement
                              - LostDepositFromDenounciationBaking
                              - LostFeesDenounciationBaking
                              - LostRewardsDenounciationBaking
                              - LostDepositFromDenounciationEndorsement
                              - LostFeesDenounciationEndorsement
                              - LostRewardsDenounciationEndorsement
                              - LostRevelationFees 
                              - LostRevelationRewards;
            return totalReward;
        }
    }
}
