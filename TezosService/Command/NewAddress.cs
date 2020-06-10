using System;
using System.Data.Entity.Migrations;
using System.Reflection;
using log4net;
using Telegram.Bot.Types;
using TezosService.Connectors;
using TezosService.Model.Mine;

namespace TezosService.Command
{
    public class NewAddress
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly Message _message;

        public NewAddress(Message message)
        {
            _message = message;
        }

        public void Execute()
        {
            try
            {
                var context = new MyContext();
                var config = context.DelegateConfig.Find(Singleton.Instance.Bakers[0].Account, _message.Text);
                if (config != null)
                {
                    config.TelegramId = _message.From.Id;
                    config.Name = _message.From.Username;
                    context.DelegateConfig.AddOrUpdate(config);
                    context.SaveChangesAsync();
                    Singleton.Instance.SendTextMessageAsync(_message.From.Id,
                        $"👍 We already have you on record.\nCurrently set to receive your ꜩ {config.GetRewardOffsetText()}");
                    return;
                }
                context.DelegateConfig.Add(new DelegateConfig
                {
                    Account = _message.Text,
                    Baker = Singleton.Instance.Bakers[0].Account,
                    TelegramId = _message.From.Id,
                    Name = _message.From.Username, RewardOffset = 0
                });
                context.SaveChangesAsync();
                Singleton.Instance.SendTextMessageAsync(_message.From.Id,
                    "✅ Thanks! We've got you on file now. The current default is to send your ꜩ as soon as the reward is delivered. If you prefer earlier/later payments. Click the \"⚖️ Change Payment Offset\" button.");
            }
            catch (Exception e)
            {
                Log.Error(e.Message);
                Log.Error(e.StackTrace);
            }
        }
    }
}