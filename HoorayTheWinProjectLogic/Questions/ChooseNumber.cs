using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HoorayTheWinProjectLogic.Questions
{
    public class ChooseNumber : AbstractQuestion
    {
        public ChooseNumber(string question, string answerOne, string answerTwo, string answerThree, string answerFour)
        {
            List<string> Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = 0;
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
            new[]
            {
                InlineKeyboardButton.WithCallbackData(Answer[0]),
                InlineKeyboardButton.WithCallbackData(Answer[1]),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(Answer[2]),
                InlineKeyboardButton.WithCallbackData(Answer[3]),
            },
            new[]
            {
               InlineKeyboardButton.WithCallbackData("Done")}
            });
            return inlineKeyboard;
        }

        public override bool SetAnswer(Update update, TestManager test)
        {
            if (update.Message != null)
                return false;
            if (update.CallbackQuery!.Data != "Done")
            {
                foreach (var item in Answer)
                {
                    if (update.CallbackQuery!.Data == item)
                    {
                        if (DataMock.DataAnswer.Contains(update.CallbackQuery!.Data) == false)
                        {
                            DataMock.DataAnswer.Add(update.CallbackQuery.Data!);
                        }
                        return false;
                    }
                }
            }
            else
            {
                string s = string.Join(", ", DataMock.DataAnswer);
                List<string> answers;
                test.AnswerBase.TryGetValue(update.CallbackQuery.Message!.Chat.Id, out answers!);
                answers.Add(s);
                return true;
            }
            return false;
        }
    }
}
