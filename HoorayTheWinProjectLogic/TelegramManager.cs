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
            TryAddUserToBase(update);

            if (IsTesting && !IsInTest(update))
            {
                SendMessageWhenNotInTest(update);
                return;
            }
            else if (IsTesting)
            {
               await HandlingAnswerAsync(botClient, update);                
            }
        }
        public void SendMessageWhenTestNotFinished(long chatId)
        {
            _client.SendTextMessageAsync(chatId,
           "Haha, You didn't have time",
            replyMarkup: null);
        }
        private void SendMessageWhenNotInTest(Update update)
        {
            long chatId = GetChatId(update);
            _client.SendTextMessageAsync(chatId,
           "Sorry, you are not in test now",
            replyMarkup: null);
        }
        private Task HandleError(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {

            return Task.CompletedTask;
        }

        public bool IsFinished(long chatId)
        {
            TestToBot testToBot = TestToBot.GetInstance();
            return ((testToBot.Manager.AnswerBase[chatId]).Count() == testToBot.Manager.Test.AbstractQuestions.Count());
        }

        private long GetChatId(Update update)
        {
            if (update.Message != null)
            {
                return update.Message.Chat.Id;
            }
            else if (update.CallbackQuery != null)
            {
                return update.CallbackQuery.Message!.Chat.Id;
            }
            else
            {
                throw new InvalidOperationException();
            }
        }
        private bool IsInTest(Update update)
        {
            TestToBot testToBot = TestToBot.GetInstance();
            long chatId = GetChatId(update);
            return testToBot.Manager.AnswerBase.ContainsKey(chatId);
        }
        private void TryAddUserToBase(Update update)
        {
            long chatId = GetChatId(update);
            if (update.Message != null && !groups.IsInBase(chatId))
            {
                groups.AddChatId(chatId);
                groups.groups[0].AddUser(new User(update.Message.Chat));
            }
            else
            {
                return;
            }
        }
        private async Task HandlingAnswerAsync(ITelegramBotClient botClient, Update update)
        {
            TestToBot testToBot = TestToBot.GetInstance();
            long chatId = GetChatId(update);

            if (!IsFinished(chatId))
            {
                Enums.BehaviorOptions behaviorOption = testToBot.Manager.Test.AbstractQuestions[(testToBot.Manager.AnswerBase[chatId]).Count() + tmp].SetAnswer(update);
                if (behaviorOption == Enums.BehaviorOptions.invalidAnswer)
                {
                    tmp = 0;
                    SendNextQuestion(chatId);
                }
                else if (behaviorOption == Enums.BehaviorOptions.nextQuestion)
                {
                    await botClient.EditMessageTextAsync(
                          update.CallbackQuery!.Message!.Chat.Id,
                          update.CallbackQuery.Message!.MessageId,
                          update.CallbackQuery.Message!.Text!,
                          replyMarkup: null
                          );
                    tmp = 0;
                    SendNextQuestion(chatId);
                }
                else if (behaviorOption == Enums.BehaviorOptions.lastQuestion)
                {
                    await _client.SendTextMessageAsync(chatId, "The test is over!", replyMarkup: null);
                    return;
                }
                else if (behaviorOption == Enums.BehaviorOptions.refreshKeybord)
                {
                    tmp = -1;
                    return;
                }
            }
            else 
            {
               await _client.SendTextMessageAsync(chatId, "The test is over!", replyMarkup: null);
            }
        }
    }
}
