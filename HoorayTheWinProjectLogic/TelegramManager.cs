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
        private int tmp = 0;
        public static bool IsTesting = false;
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
            int numberOfQuestion = (DataMock.testToStart.AnswerBase[chatId]).Count();
            _client.SendTextMessageAsync(chatId,
            DataMock.testToStart.Test.AbstractQuestions[numberOfQuestion].TextOfQuestion,
            replyMarkup: DataMock.testToStart.Test.AbstractQuestions[numberOfQuestion].GetInlineKM());
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

                if (!groups.IsInBase(chatId))
                {
                    groups.Add(chatId);
                    groups.groups[0].AddUser(new User(update.Message.Chat));
                }
                if (IsTesting)
                {
                    if ((DataMock.testToStart.AnswerBase[chatId]).Count() < DataMock.testToStart.Test.AbstractQuestions.Count()-1)
                    {
                        Enums.BehaviorOptions behaviorOption = DataMock.testToStart.Test.AbstractQuestions[(DataMock.testToStart.AnswerBase[chatId]).Count() + tmp].SetAnswer(update);
                        if (behaviorOption == Enums.BehaviorOptions.invalidAnswer)
                        {
                            tmp = 0;
                            SendNextQuestion(chatId);
                        }
                        else if (behaviorOption == Enums.BehaviorOptions.nextQuestoin)
                        {
                            tmp = 0;
                            SendNextQuestion(chatId);
                        }
                        else if (behaviorOption == Enums.BehaviorOptions.refreshKeybord)
                        {
                            tmp = -1;
                            return;
                        }
                    }
                    else
                    {
                        await _client.SendTextMessageAsync(chatId, "The test is over!!!");
                    }
                }
            }
            else if (update.CallbackQuery != null)
            {
                long chatId = update.CallbackQuery.Message!.Chat.Id;
                if ((DataMock.testToStart.AnswerBase[chatId]).Count() < DataMock.testToStart.Test.AbstractQuestions.Count()-1)
                {
                    Enums.BehaviorOptions behaviorOption = DataMock.testToStart.Test.AbstractQuestions[(DataMock.testToStart.AnswerBase[chatId]).Count() + tmp].SetAnswer(update);
                    if (behaviorOption == Enums.BehaviorOptions.invalidAnswer)
                    {
                        tmp = 0;
                        SendNextQuestion(chatId);
                    }
                    else if (behaviorOption == Enums.BehaviorOptions.nextQuestoin)
                    {
                        tmp = 0;
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
                        tmp = -1;
                        return;
                    }
                }
                else
                {
                    await _client.SendTextMessageAsync(chatId, "The test is over!!!");
                }
            }
        }

        public void SendMessageWhenTestNotFinished(long chatId)
        {
            _client.SendTextMessageAsync(chatId,
           "Haha, I didn't have time",
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
