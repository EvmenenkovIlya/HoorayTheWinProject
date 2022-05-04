using HoorayTheWinProjectLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HoorayTheWinProjectLogic.Questions
{
    public class YesNo : AbstractQuestion
    {
        public YesNo(string question, string answerOne, string answerTwo)
        {
            base.Answer = new List<string>() {"Yes", "No" };
            TextOfQuestion = question;
            TypeQuestion = 4;
        }
        public YesNo() { }
        public override InlineKeyboardMarkup GetInlineKM()
        {
            InlineKeyboardMarkup inlineKeyboard = new(
             new[]
             {
             new []
             {
                 InlineKeyboardButton.WithCallbackData(Answer[0]),
                 InlineKeyboardButton.WithCallbackData(Answer[1]),
             }
             });

            return inlineKeyboard;
        }

        public override Enums.BehaviorOptions SetAnswer(Update update)
        {
            if (update.Message != null)
            {
                return Enums.BehaviorOptions.invalidAnswer;
            }
            TestToBot testToBot = TestToBot.GetInstance();
            long chatId = update.CallbackQuery!.Message!.Chat.Id;
            List<string> answers = testToBot.Manager.AnswerBase[chatId];
            answers.Add(update.CallbackQuery.Data!);
            if (answers.Count == testToBot.Manager.Test.AbstractQuestions.Count())
            {
                return Enums.BehaviorOptions.lastQuestion;
            }
            return Enums.BehaviorOptions.nextQuestion;          
        }
    }
}