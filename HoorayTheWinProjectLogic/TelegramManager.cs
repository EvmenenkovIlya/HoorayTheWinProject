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

        public TelegramManager()
        {
            _client = new TelegramBotClient(_token);
        }

        public void Start()
        {
            _client.StartReceiving(HandleRecieve, HandleError);
        }

        public  void SendNextQuestion(long chatId)
        {
            int numberOfQuestion = (DataMock._testToStart.AnswerBase[chatId]).Count();
            _client.SendTextMessageAsync(chatId,
            DataMock._testToStart.Test.AbstractQuestions[numberOfQuestion].TextOfQuestion,
            replyMarkup: DataMock._testToStart.Test.AbstractQuestions[numberOfQuestion].GetInlineKM());
        }

        private async Task HandleRecieve(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Message != null)
            {
                if (update.Message!.Text == null)
                {
                    return;
                }
                long chatId = update.Message.Chat.Id;
                if (DataMock.DataBase.Contains(chatId) == false)
                {
                    DataMock.DataBase.Add(chatId);
                    DataMock._other.AddUser(new User(update.Message.Chat));
                }
                if (DataMock.IsTesting == true)
                {
                    if ((DataMock._testToStart.AnswerBase[chatId]).Count() < DataMock._testToStart.Test.AbstractQuestions.Count())
                    {
                        Enums.BehaviorOptions behaviorOption = DataMock._testToStart.Test.AbstractQuestions[(DataMock._testToStart.AnswerBase[chatId]).Count()].SetAnswer(update);
                        if (behaviorOption == Enums.BehaviorOptions.invalidAnswer)
                        {
                            SendNextQuestion(chatId);
                        }
                        else if (behaviorOption == Enums.BehaviorOptions.nextQuestoin)
                        {
                           await botClient.EditMessageTextAsync(
                           update.Message!.Chat.Id,
                           update.Message!.MessageId,
                           update.Message!.Text!,
                           replyMarkup: null
                           );
                            SendNextQuestion(chatId);
                        }
                        else if (behaviorOption == Enums.BehaviorOptions.refreshKeybord)
                        {                            
                            return;
                        }
                    }
                    else if ((DataMock._testToStart.AnswerBase[chatId]).Count() == DataMock._testToStart.Test.AbstractQuestions.Count())
                    {
                        await _client.SendTextMessageAsync(chatId, "The test is over!!!");
                    }
                }
            }
            else if (update.CallbackQuery != null)
            {
                long chatId = update.CallbackQuery.Message!.Chat.Id;
                if ((DataMock._testToStart.AnswerBase[chatId]).Count() < DataMock._testToStart.Test.AbstractQuestions.Count())
                {
                    Enums.BehaviorOptions behaviorOption = DataMock._testToStart.Test.AbstractQuestions[(DataMock._testToStart.AnswerBase[chatId]).Count()].SetAnswer(update);
                    if (behaviorOption == Enums.BehaviorOptions.invalidAnswer)
                    {
                        SendNextQuestion(chatId);
                    }
                    else if (behaviorOption == Enums.BehaviorOptions.nextQuestoin)
                    {
                        await botClient.EditMessageTextAsync(
                        update.CallbackQuery.Message!.Chat.Id,
                        update.CallbackQuery.Message!.MessageId,
                        update.CallbackQuery.Message!.Text!,
                        replyMarkup: null
                        );
                        SendNextQuestion(chatId);
                    }
                    else if (behaviorOption == Enums.BehaviorOptions.refreshKeybord)
                    {                        
                        return;
                    }
                }
                else if ((DataMock._testToStart.AnswerBase[chatId]).Count() == DataMock._testToStart.Test.AbstractQuestions.Count())
                {
                    await _client.SendTextMessageAsync(chatId, "The test is over!!!");
                }
            }

        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }
    }
}