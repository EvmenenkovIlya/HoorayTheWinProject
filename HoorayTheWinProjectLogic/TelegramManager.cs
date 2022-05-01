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
        private const string _token = "5309481862:AAHaEMz6L2bozc4jO2DuAAxj1yHDipoSV5s";
        Dictionary<long, TestManager> Tests { get; set; } = new Dictionary<long, TestManager>();

        public TelegramManager()
        {
            _client = new TelegramBotClient(_token);
        }

        public void Start()
        {
            _client.StartReceiving(HandleRecieve, HandleError);
        }

        //public async void Send(AbstractQuestion abstractQuestion, long id)
        //{
        //    if (DataMock.IsTesting == false)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        InlineKeyboardMarkup inlineKeyboard = abstractQuestion.GetInlineKM();
        //        await _client.SendTextMessageAsync(new ChatId(id), abstractQuestion.TextOfQuestion, replyMarkup: inlineKeyboard);
        //    }
        //}

        public  void SendNextQuestion(TestManager testManager)
        {
            _client.SendTextMessageAsync(testManager.ChatId,
            testManager.Test.AbstractQuestions[testManager.QuestionIndex].TextOfQuestion,
            replyMarkup: testManager.Test.AbstractQuestions[testManager.QuestionIndex].GetInlineKM());
            testManager.QuestionIndex++;
        }

        private async Task HandleRecieve(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null)
            {
                if(update.Message.Text == null)
                return;
                long chatId = update.Message.Chat.Id;
                if (DataMock.DataBase.Contains(chatId) == false)
                {
                    DataMock.DataBase.Add(chatId);
                    DataMock._other.AddUser(new User(update.Message.Chat));
                }                
                if (DataMock.IsTesting == true)
                {
                    if (Tests.ContainsKey(chatId) == false)
                    {
                        Tests.Add(chatId, DataMock._testToStart);
                    }
                    if (Tests[chatId].QuestionIndex <= Tests[chatId].Test.AbstractQuestions.Count - 1)
                    {
                        var crntTest = Tests[chatId];
                        SendNextQuestion(crntTest);
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(Tests[chatId].ChatId, "Тест окончен!!!");

                    }
                }
            }
            else if (update.CallbackQuery != null && DataMock.IsTesting == true)
            {
                long chatId = update.CallbackQuery!.Message!.Chat.Id;
                if (Tests.ContainsKey(chatId) == false)
                {
                    Tests.Add(chatId, DataMock._testToStart);
                }
                if (Tests[chatId].QuestionIndex <= Tests[chatId].Test.AbstractQuestions.Count - 1)
                {
                    var crntTest = Tests[chatId];
                    SendNextQuestion(crntTest);
                    await botClient.EditMessageReplyMarkupAsync(
                        update.CallbackQuery.Message!.Chat.Id,
                        update.CallbackQuery.Message!.MessageId,
                        null
                        );
                }
                else
                {
                    await _client.SendTextMessageAsync(Tests[chatId].ChatId, "Тест окончен!!!");

                }
            }
        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}