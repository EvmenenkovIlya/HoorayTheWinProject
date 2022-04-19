using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HoorayTheWinProject_
{
    public class TelegramManager
    {
        private TelegramBotClient _client;
        private Action<string> _onMessage;
        private List<long> _ids;

        public TelegramManager(string token, Action<string> onMessage)
        {
            _client = new TelegramBotClient(token);
            _onMessage = onMessage;
            _ids = new List<long>();
        }

        public async void Send(string s)
        {
            foreach (var id in _ids)
            {
                InlineKeyboardMarkup inlineKeyboard = new(
                       new[]
                       {
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("Q", "QQQQQ"),
                                InlineKeyboardButton.WithCallbackData("W", "WWWWW"),
                                InlineKeyboardButton.WithCallbackData("E", "EEEEE"),
                            },
                            new []
                            {
                                InlineKeyboardButton.WithCallbackData("R", "RRRRR"),
                                InlineKeyboardButton.WithCallbackData("T", "TTTTT"),
                            }
                       });
                await _client.SendTextMessageAsync(new ChatId(id), s, replyMarkup: inlineKeyboard);
            }
        }

        public void Start()
        {
            _client.StartReceiving(HandleRecieve, HandleError);
        }

        private async Task HandleRecieve(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null && update.Message.Text != null)
            {

                if (!_ids.Contains(update.Message.Chat.Id))
                {
                    _ids.Add(update.Message.Chat.Id);
                }

                string s = update.Message.Chat.FirstName + " "
                    + update.Message.Chat.LastName + " "
                    + update.Message.Text;
                _onMessage(s);
            }
        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}