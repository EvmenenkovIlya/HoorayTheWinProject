using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


/*namespace HoorayTheWinProjectLogic
{
    public class Telega
    {
        private TelegramBotClient _client;
        private MainWindow _window;
        public Telega(MainWindow window)
        {
            _client = new TelegramBotClient("Token");
            _client.StartReceiving(HandleUpdateAsync,HandleErrorAsync);
            _window = _window;
        }

        public void Send(string text)
        {
           
        }
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type != UpdateType.Message)
                return;
            if (update.Message!.Type != MessageType.Text)
                return;

            var chatId = update.Message.Chat.Id;
            var messageText = update.Message.Text;
            var firstName = update.Message.Chat.FirstName;
         
            Message sentMessage = await botClient.SendTextMessageAsync(
                chatId: chatId,
                text: "You said:\n" + messageText,
                cancellationToken: cancellationToken);
        }

        public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {         
            return Task.CompletedTask;
        }


    }
}*/
