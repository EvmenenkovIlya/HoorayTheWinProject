using HoorayTheWinProject_.TestLogicInTG;
using HoorayTheWinProjectLogic;
using HoorayTheWinProjectLogic.Questions;
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

        public async void Send(AbstractQuestion abstractQuestion)
        {
            InSeries qs = (InSeries)abstractQuestion;
            foreach (var id in _ids)
            {
                InlineKeyboardMarkup inlineKeyboard = InSeriesTG.InlineKM(qs);               
                await _client.SendTextMessageAsync(new ChatId(id), abstractQuestion.TextOfQuestion, replyMarkup: inlineKeyboard);
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

                if (!DataMock.DataBase.ContainsKey(update.Message.Chat.Id))
                {
                    DataMock.DataBase.Add(update.Message.Chat.Id, new HoorayTheWinProjectLogic.User((string)update.Message.Chat.Username!));
                }
            }
            else if (update.CallbackQuery != null)
            {                
                await botClient.EditMessageTextAsync(
                    update.CallbackQuery.Message!.Chat.Id,
                    update.CallbackQuery.Message!.MessageId,
                    update.CallbackQuery.Message!.Text!,
                    replyMarkup: null
                    );


                string s = update.CallbackQuery.From.FirstName + " "
                    + update.CallbackQuery.From.LastName + "на вопрос" + DataMock.testMock.AbstractQuestions + "ответил"
                    + update.CallbackQuery.Data;
                _onMessage(s);
            }
        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}