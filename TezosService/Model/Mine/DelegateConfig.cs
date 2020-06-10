using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TezosService.Model.Mine
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class DelegateConfig
    {
        [Key, Column(Order = 0)]
        public string Baker { get; set; }
        [Key, Column(Order = 1)]
        public string Account { get; set; }
        public long TelegramId { get; set; }
        public string PayoutAccountOverride { get; set; }
        public string Name { get; set; }
        public int RewardOffset { get; set; }
        public bool WaitForBake { get; set; }

        public string GetRewardOffsetText()
        {
            switch (RewardOffset)
            {
                case 0:
                    return "Immediately when rewards delivered";
                case 1:
                    return "*1* Cycle before rewards delivered";
                case 2:
                    return "*2* Cycles before rewards delivered";
                case 3:
                    return "*3* Cycles before rewards delivered";
                case 4:
                    return "*4* Cycles before rewards delivered";
                case 5:
                    return "*5* Cycles before rewards delivered";
                case 6:
                    return "*6* Cycles before rewards delivered (In progress)";
                case 7:
                    return "*7* Cycles before rewards delivered (Pending)";
                case 8:
                    return "*8* Cycles before rewards delivered (Pending +1)";
                case 9:
                    return "*9* Cycles before rewards delivered (Pending +2)";
                case 10:
                    return "*10* Cycles before rewards delivered (Pending +3)";
                case 11:
                    return "*11* Cycles before rewards delivered (Pending +4)";
                default:
                    return $"{RewardOffset} cycles after reward delivered";
            }
        }
        public static string GetRewOffsetText(int rewardOffset)
        {
            switch (rewardOffset)
            {
                case 0:
                    return "Immediately when rewards delivered";
                case 1:
                    return "*1* Cycle before rewards delivered";
                case 2:
                    return "*2* Cycles before rewards delivered";
                case 3:
                    return "*3* Cycles before rewards delivered";
                case 4:
                    return "*4* Cycles before rewards delivered";
                case 5:
                    return "*5* Cycles before rewards delivered";
                case 6:
                    return "*6* Cycles before rewards delivered (In progress)";
                case 7:
                    return "*7* Cycles before rewards delivered (Pending)";
                case 8:
                    return "*8* Cycles before rewards delivered (Pending +1)";
                case 9:
                    return "*9* Cycles before rewards delivered (Pending +2)";
                case 10:
                    return "*10* Cycles before rewards delivered (Pending +3)";
                case 11:
                    return "*11* Cycles before rewards delivered (Pending +4)";
                default:
                    return $"{rewardOffset} cycles after reward delivered";
            }
        }
    }
}