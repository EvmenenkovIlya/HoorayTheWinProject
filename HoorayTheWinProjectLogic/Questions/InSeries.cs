using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HoorayTheWinProjectLogic.Questions
{
    public class InSeries : AbstractQuestion
    {
        public InSeries(string question, string answerOne, string answerTwo, string answerThree, string answerFour)
        {
            List<string> Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = 3;
            Answer.Add(answerOne);
            Answer.Add(answerTwo);
            Answer.Add(answerThree);
            Answer.Add(answerFour);
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
                 InlineKeyboardButton.WithCallbackData(Answer[2], Answer[2]),
                 InlineKeyboardButton.WithCallbackData(Answer[3], Answer[3]),
             },
             new []
             {
                 InlineKeyboardButton.WithCallbackData("Done", "Done")}
             });

            return inlineKeyboard;
        }

        public override bool SetAnswer(Update update, Test test)
        {
            foreach (var item in Answer)
            {
                if (update.Message!.Text == item || update.Message.Text == "Done")
                {
                    List<string> answers;
                    test.AnswerBase.TryGetValue(update.Message.Chat.Id, out answers!);
                    answers.Add(update.Message.Text);
                    return true;
                }
            }
            return false;
        }
    }
}

