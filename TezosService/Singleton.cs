using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TezosService.Model.Mine;

namespace TezosService
{
    public sealed class Singleton
    {
        private static readonly Lazy<Singleton> Lazy = new Lazy<Singleton>(() => new Singleton());

        public static Singleton Instance => Lazy.Value;
        private readonly List<MyKey> _myKeys = new List<MyKey>();
        public readonly List<BakeryConfig> Bakers = new List<BakeryConfig>();
        public readonly TelegramBotClient Bot = new TelegramBotClient("redacted:redact");
        private readonly ChatId _chatId = new ChatId(redacted);

        private readonly ReplyKeyboardMarkup _replyKeyboard;
        private Singleton()
        {
            _replyKeyboard = new[]
            {
                new [] { Commands.ViewAddAddress, Commands.ChangeWithdraw },
                new [] { Commands.ManualWithdraw, Commands.ChangeOffset },
                new [] { Commands.SeeRewards}
            };
            _replyKeyboard.ResizeKeyboard = true;


            Bakers.Add(new BakeryConfig
            {
                Account = "tz1...",
                Node = "https://mainnet.tezrpc.me",
                BurnerKt1 = "KT1...",
                GasLimit = 10300,
                Storage = 0,
                NetworkFee = 1270
            });
            _myKeys.Add(new MyKey
            {
                PublicKey = "KT1...",
                Mnemonic = "stuff things blah blah",
                Passphrase = "super secret stuff"
            });
        }

        public string GetPassphrase(string publicKey)
        {
            return _myKeys.FirstOrDefault(x => x.PublicKey == publicKey)?.Passphrase;
        }

        public string GetMnemonic(string publicKey)
        {
            return _myKeys.FirstOrDefault(x => x.PublicKey == publicKey)?.Mnemonic;
        }

        public List<string> GetBurners()
        {
            var burners = new List<string>();
            foreach (var baker in Bakers)
            {
                burners.Add(baker.BurnerKt1);
            }
            return burners;
        }
        public void SendMessage(string message)
        {
            Bot.SendTextMessageAsync(_chatId, message, ParseMode.Markdown, replyMarkup: _replyKeyboard);
        }
        public void SendTextMessageAsync(int chatId, string text)
        {
            Bot.SendTextMessageAsync(chatId, text, ParseMode.Markdown, replyMarkup: _replyKeyboard);
        }
    }
}