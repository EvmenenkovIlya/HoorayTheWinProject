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
            TestToBot testToBot = TestToBot.GetInstance();
            int numberOfQuestion = (testToBot.Manager.AnswerBase[chatId]).Count();
            _client.SendTextMessageAsync(chatId,
            testToBot.Manager.Test.AbstractQuestions[numberOfQuestion].TextOfQuestion,
            replyMarkup: testToBot.Manager.Test.AbstractQuestions[numberOfQuestion].GetInlineKM());
        }

        private async Task HandleRecieve(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            TestToBot testToBot = TestToBot.GetInstance();
            AddToBase(update);
            if (IsTesting)
            {
                HandlingAnswer(update);
            }
        }

        public void SendMessageWhenTestNotFinished(long chatId)
        {
            _client.SendTextMessageAsync(chatId,
           "Haha, You didn't have time",
            replyMarkup: null);

        }

        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        private bool IsFinished(long chatId)
        {
            TestToBot testToBot = TestToBot.GetInstance();
            if ((testToBot.Manager.AnswerBase[chatId]).Count() == testToBot.Manager.Test.AbstractQuestions.Count())
            {
                _client.SendTextMessageAsync(chatId, "Уходи, ты все!", replyMarkup: null);
                return true;
            }
            else
            {
                return false;
            }
        }

        private long GetChatId(Update update)
        {
            if (update.Message != null)
            {
                return update.Message.Chat.Id;
            }
            else if (update.CallbackQuery != null)
            {
                return update.CallbackQuery.Message.Chat.Id;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        private void AddToBase(Update update)
        {
            long chatId = GetChatId(update);
            if (!groups.IsInBase(chatId))
            {
                groups.Add(chatId);
                groups.groups[0].AddUser(new User(update.Message.Chat));
            }
            else
            {
                return;
            }
        }

        private void HandlingAnswer(Update update)
        {
            TestToBot testToBot = TestToBot.GetInstance();
            long chatId = GetChatId(update);
            Enums.BehaviorOptions behaviorOption = testToBot.Manager.Test.AbstractQuestions[(testToBot.Manager.AnswerBase[chatId]).Count() + tmp].SetAnswer(update);
            if (!IsFinished(chatId))
            {
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
        }
    }
}
