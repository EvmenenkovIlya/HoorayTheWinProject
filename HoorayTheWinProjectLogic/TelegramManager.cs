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

namespace HoorayTheWinProjectLogic
{
    public class TelegramManager
    {
        private TelegramBotClient _client;
        private Test _test;
        private const string _token = "5309481862:AAHaEMz6L2bozc4jO2DuAAxj1yHDipoSV5s";

        public TelegramManager(Test test)
        {
            _client = new TelegramBotClient(_token);
            _test = test;
        }

        public void Start()
        {
            _client.StartReceiving(HandleRecieve, HandleError);
        }

        public async void Send(AbstractQuestion abstractQuestion, long id)
        {
            InlineKeyboardMarkup inlineKeyboard = abstractQuestion.GetInlineKM();
            await _client.SendTextMessageAsync(new ChatId(id), abstractQuestion.TextOfQuestion, replyMarkup: inlineKeyboard);
        }

        private async Task HandleRecieve(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {

            if (update.Message == null || update.Message.Text == null)
            {
                //await _client.SendTextMessageAsync(update.Message!.Chat.Id, "Enter text or emoji",  replyMarkup: null);
                return;
            }
            if (DataMock.DataBase.Contains(update.Message.Chat.Id) == false)
            {
                DataMock.DataBase.Add(update.Message.Chat.Id);
                DataMock._other.AddUser(new User(update.Message.Chat));
            }
            if (DataMock.IsTesting && DataMock._testToStart.AnswerBase.ContainsKey(update.Id))
            {               
                SendNextQuestion(update.Id);
                //int numberOfQuestion = 0;
                //foreach (long id in DataMock._testToStart.AnswerBase.Keys)
                //{
                //    int numberOfAnswer = (DataMock._testToStart.AnswerBase[id]).Count;
                //    if (numberOfAnswer == numberOfQuestion)
                //    {
                //        Send(DataMock._testToStart.FinalTest.AbstractQuestions[numberOfQuestion], id);
                //        numberOfQuestion++;
                //    }
                //    else
                //    {
                //        // try to set answer
                //        (DataMock._testToStart.AnswerBase[id]).Add(update.Message.Text);
                //    }
                //}
            }
            //else if (update.CallbackQuery != null)
            //{
            //    await botClient.EditMessageTextAsync(
            //        update.CallbackQuery.Message!.Chat.Id,
            //        update.CallbackQuery.Message!.MessageId,
            //        update.CallbackQuery.Message!.Text!,
            //        replyMarkup: null
            //        );
            //}
        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        private void SendNextQuestion(long id)
        {
            int questionIndex = (DataMock._testToStart.AnswerBase[id]).Count;
            Send(DataMock._testToStart.FinalTest.AbstractQuestions[questionIndex], id);
        }
    }
}