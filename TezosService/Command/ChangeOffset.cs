using System;
using System.Reflection;
using log4net;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TezosService.Model.Mine;

namespace TezosService.Command
{
    public class ChangeOffset
    {
        private readonly Message _message;
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public ChangeOffset(Message message)
        {
            _message = message;
        }

        public void Execute()
        {
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
{
                new []
                {
                    InlineKeyboardButton.WithCallbackData("0", "ChangeOffset 0"),
                    InlineKeyboardButton.WithCallbackData("1", "ChangeOffset 1"),
                    InlineKeyboardButton.WithCallbackData("2", "ChangeOffset 2"),
                    InlineKeyboardButton.WithCallbackData("3", "ChangeOffset 3"),
                    InlineKeyboardButton.WithCallbackData("4", "ChangeOffset 4"),
                    InlineKeyboardButton.WithCallbackData("5", "ChangeOffset 5"),
                    InlineKeyboardButton.WithCallbackData("6", "ChangeOffset 6"),
                    InlineKeyboardButton.WithCallbackData("7", "ChangeOffset 7"),
                    InlineKeyboardButton.WithCallbackData("8", "ChangeOffset 8"),
                    InlineKeyboardButton.WithCallbackData("9", "ChangeOffset 9"),
                    InlineKeyboardButton.WithCallbackData("10", "ChangeOffset 10"),
                    InlineKeyboardButton.WithCallbackData("11", "ChangeOffset 11")
                }
            });
            Singleton.Instance.Bot.SendTextMessageAsync(_message.From.Id, "Choose which cycle offset that you would like to receive rewards.", ParseMode.Markdown,
                replyMarkup: inlineKeyboard);
        }

        public static void BotOnCallbackQueryReceived(object sender, CallbackQueryEventArgs callbackQueryEventArgs)
        {
            try
            {
                var callbackQuery = callbackQueryEventArgs.CallbackQuery;
                switch (callbackQuery.Data.Split(' ')[0])
                {
                    case "ChangeOffset":
                        break;
                }
                
                /*await Singleton.Instance.Bot.AnswerCallbackQueryAsync(
                    callbackQuery.Id,
                    $"Received! {callbackQuery.Data}");

                await Singleton.Instance.Bot.SendTextMessageAsync(
                    callbackQuery.Message.Chat.Id,
                    $"Received!! {callbackQuery.Data}");*/
            }
            catch (Exception) { /* ignored */ }
        }

        /*
                     var inlineKeyboard = new InlineKeyboardMarkup(new[]
            {
                new [] 
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(0), "ChangeOffset 0")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(1), "ChangeOffset 1")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(2), "ChangeOffset 2")
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(3), "ChangeOffset 3"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(4), "ChangeOffset 4"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(5), "ChangeOffset 5"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(6), "ChangeOffset 6"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(7), "ChangeOffset 7"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(8), "ChangeOffset 8"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(9), "ChangeOffset 9"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(10), "ChangeOffset 10"),
                },
                new []
                {
                    InlineKeyboardButton.WithCallbackData(DelegateConfig.GetRewOffsetText(11), "ChangeOffset 11"),
                }
            });
         */
    }
}