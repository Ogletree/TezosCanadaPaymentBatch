using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using log4net;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TezosService.Connectors;
using TezosService.Extensions;
using TezosService.Model;
using TezosService.Model.TzScan;
using Tz.Net.Extensions;

namespace TezosService.Command
{
    public class SeeRewards
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Message _message;
        private TimeSpan _approxFinish;
        public SeeRewards(Message message)
        {
            _message = message;
        }

        public void Execute()
        {
            List<string> addressList;
            List<DelegatePayments> payments;
            using (var context = new MyContext())
            {
                addressList = context.DelegateConfig.Where(x => x.TelegramId == _message.From.Id).Select(x => x.Account).ToList();
                payments = context.DelegatePayments.Where(x => addressList.Contains(x.Account)).ToList();
            }
            var head = TzScanConnector.GetHead();
            var level = TzScanConnector.GetLevel(head.Hash);
            _approxFinish = TimeSpan.FromMinutes(level.CycleEndBlock() - level.BlockLevel + 20);

            var message = "";
            var tzScanConnector = new TzScanConnector(Singleton.Instance.Bakers[0]);
            foreach (var account in addressList)
            {
                message = $"This information is straight from tzscan.io for [{account.Substring(0, 4)}…{account.Substring(account.Length - 4)}](https://tzscan.io/{account}?default=rewards) with your expected fee applied.\n";
                foreach (var reward in tzScanConnector.GetDelegateRewards(account))
                {
                    var payment = payments.FirstOrDefault(x => x.Cycle == reward.Cycle && x.Account == account);
                    if (payment != null)
                    {
                        message += $"▫️ *{payment.Reward}ꜩ* for cycle {reward.Cycle}. [{payment.Paid.Substring(0,4)}…{payment.Paid.Substring(payment.Paid.Length-4)}](https://tzscan.io/{payment.Paid})\n";
                    }
                    else
                    {
                        var pendingReward = (reward.Balance / reward.StakingBalance) * (reward.Rewards + reward.ExtraRewards - reward.Losses);
                        message += $"▫️ *{pendingReward.ToTez()}ꜩ* for cycle {reward.Cycle}. {GetText(reward)}\n";
                    }
                }
                Singleton.Instance.Bot.SendTextMessageAsync(_message.From.Id, message, ParseMode.Markdown, true);
            }
        }

        private string GetText(DelegateRewards reward)
        {
            switch (reward.Status.Status)
            {
                case StatusEnum.RewardsPending:
                    return "❄️ Rewards Pending";
                case StatusEnum.CycleInProgress:
                    return $"🔄 In Progress ({_approxFinish.ToPrettyFormat()})";
                case StatusEnum.CyclePending:
                    return "⏲️ Cycle Pending";
                default:
                    return "";
            }
        }
    }
}