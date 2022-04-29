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
                 InlineKeyboardButton.WithCallbackData(Answer[0], Answer[0]),
                 InlineKeyboardButton.WithCallbackData(Answer[1], Answer[1]),
             },
             new []
             {
                 InlineKeyboardButton.WithCallbackData("Done", "Done")}
             });

            return inlineKeyboard;
        }

        public override bool SetAnswer(Update update, TestManager test)
        {
            foreach (var item in Answer)
            {
                if (update.Message!.Text == item || update.Message.Text == "Done")
                {
                    List<string> answers;
                    test.AnswerBase.TryGetValue(update.Message.Chat.Id, out answers!);
                    answers.Add(update.Message.Text!);
                    return true;
                }
            }
            return false;
        }
    }
}