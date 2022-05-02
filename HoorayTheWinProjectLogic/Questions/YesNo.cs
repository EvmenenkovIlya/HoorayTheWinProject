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
            List<string> Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = 4;
            Answer.Add(answerOne);
            Answer.Add(answerTwo);
            base.Answer = Answer;
        }

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

        public override bool SetAnswer(Update update, TestManager test)
        {
            if (update.Message != null)
                return false;
            foreach (var item in Answer)
            {
                if (update.CallbackQuery!.Data == item)
                {
                    List<string> answers;
                    test.AnswerBase.TryGetValue(update.CallbackQuery.Message!.Chat.Id, out answers!);
                    answers.Add(update.CallbackQuery.Data);
                    return true;
                }
            }
            return false;
        }
    }
}