using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TezosService.Connectors;
using TezosService.Model.Mine;

namespace TezosService
{
    public class ViewAddAddress
    {
        private readonly Message _message;

        public ViewAddAddress(Message message)
        {
            _message = message;
        }

        public void Execute()
        {
            List<DelegateConfig> configs;
            using (var context = new MyContext())
            {
                configs = context.DelegateConfig.Where(x => x.TelegramId == _message.From.Id).ToList();
            }

            if (!configs.Any())
            {
                Singleton.Instance.SendTextMessageAsync(_message.From.Id,
                    @"Enter a KT1 address on one line. For example:
*KT1H6CLk8oDPzjWwobhoQLwujQHzZeNsNSd2* ");
                return;
            }

            var message = @"To add entries, enter a KT1 address on one line.
Currently you have on file;
";
            foreach (var config in configs)
            {
                message += $"▫️  [{config.Account.Substring(0, 4)}…{config.Account.Substring(config.Account.Length - 4)}](https://tzscan.io/{config.Account}) pays {config.GetRewardOffsetText()}";
                if (!string.IsNullOrEmpty(config.PayoutAccountOverride))
                    message += $" Payments for this account are sent to {config.PayoutAccountOverride}";
                message += "\n";
            }

            Singleton.Instance.Bot.SendTextMessageAsync(_message.From.Id, message, ParseMode.Markdown, true);
        }
    }
}