using System;
using System.Reflection;
using log4net;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;
using TezosService.Command;

namespace TezosService
{
    public static class MessageRouter
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        public static void OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                if (e.Message.From.Id != redacted) return;
                if (e.Message.Type != MessageType.Text) return;
                if (e.Message.Text == Commands.ChangeWithdraw)
                {
                    new ChangeWithdraw(e.Message).Execute();
                    return;
                }
                if (e.Message.Text == Commands.SeeRewards)
                {
                    new SeeRewards(e.Message).Execute();
                    return;
                }
                if (e.Message.Text == Commands.ViewAddAddress)
                {
                    new ViewAddAddress(e.Message).Execute();
                    return;
                }
                if (e.Message.Text == Commands.ChangeOffset)
                {
                    new ChangeOffset(e.Message).Execute();
                    return;
                }
                if (e.Message.Text.ToLower().StartsWith("kt1") || e.Message.Text.ToLower().StartsWith("tz1"))
                {
                    new NewAddress(e.Message).Execute();
                    return;
                }
                var command = e.Message.Text.Split(' ')[0].ToLower();
                switch (command)
                {
                    case "/start":
                        new Start(e.Message).Execute();
                        break;
                    default:
                        Singleton.Instance.SendTextMessageAsync(e.Message.From.Id,
                            "🙈 Command not recognized.\n");
                        break;
                }
            }
            catch (Exception exception)
            {
                Log.Error(exception.Message);
                Log.Error(exception.StackTrace);
            }
        }
    }
}