using HoorayTheWinProjectLogic;
using HoorayTheWinProjectLogic.Data;
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
        GroupStorage groups = GroupStorage.GetInstance();
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

        public void SendNextQuestion(long chatId, TestManager testManager, int id)
        {
            _client.SendTextMessageAsync(chatId,
            testManager.Test.AbstractQuestions[id].TextOfQuestion,
            replyMarkup: testManager.Test.AbstractQuestions[id].GetInlineKM());

        }

        private async Task HandleRecieve(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null)
            {
                if (update.Message.Text == null)
                    
                    return;
                long chatId = update.Message.Chat.Id;

                if (!groups.IsInBase(chatId))
                {
                    groups.Add(chatId);
                    groups.groups[0].AddUser(new User(update.Message.Chat));
                }
                if (DataMock.IsTesting)
                {
                    if ((DataMock.testToStart.AnswerBase[chatId]).Count() != DataMock.testToStart.Test.AbstractQuestions.Count())
                    {
                        DataMock.testToStart.AnswerBase[chatId].Add(update.Message.Text);
                        int id = DataMock.testToStart.AnswerBase[chatId].Count();
                        if (IsFinished(chatId))
                        {
                            return;
                        }
                        SendNextQuestion(chatId, DataMock.testToStart, id);
                    }
                    if (IsFinished(chatId))
                    {
                        return;
                    }

                    //SendNextQuestion(update.Id);
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

        public void SendMessageWhenTestNotFinished(long chatId)
        {
            _client.SendTextMessageAsync(chatId,
           "Хаха, не успел",
            replyMarkup: null);

        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        private bool IsFinished(long chatId)
        {
            if ((DataMock.testToStart.AnswerBase[chatId]).Count() == DataMock.testToStart.Test.AbstractQuestions.Count())
            {
                _client.SendTextMessageAsync(chatId, "Уходи, ты все!", replyMarkup: null);
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
