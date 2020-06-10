using Telegram.Bot.Types;

namespace TezosService.Command
{
    public class Start
    {
        private readonly Message _message;

        public Start(Message message)
        {
            _message = message;
        }

        public void Execute()
        {
            var message = $@"Welcome {_message.From.FirstName}!
With this bot you can set your baking preferences and review past/future baking rewards.
To start, click the {Commands.ViewAddAddress} button or simply enter a tezos address on one line. For example:
*KT1H6CLk8oDPzjWwobhoQLwujQHzZeNsNSd2*";
            Singleton.Instance.SendTextMessageAsync(_message.From.Id, message);
        }
    }
}