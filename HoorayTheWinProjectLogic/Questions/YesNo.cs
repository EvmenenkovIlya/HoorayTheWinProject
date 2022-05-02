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
            long chatId = update.CallbackQuery!.Message!.Chat.Id;
            string message = update.CallbackQuery.Data!;
            if (update.Message != null)
            {
                return Enums.BehaviorOptions.invalidAnswer;
            }
            foreach (var item in Answer)
            {
                if (message == item)
                {
                    List<string> answers;
                    DataMock._testToStart.AnswerBase.TryGetValue(chatId, out answers!);
                    answers.Add(message);
                    return Enums.BehaviorOptions.nextQuestoin;
                }
            }
            return Enums.BehaviorOptions.invalidAnswer;
        }
    }
}