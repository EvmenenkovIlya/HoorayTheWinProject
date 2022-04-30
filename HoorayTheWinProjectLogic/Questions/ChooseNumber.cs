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
            base.Answer = new List<string>() { answerOne, answerTwo, answerThree, answerFour };
            TextOfQuestion = question;
            TypeQuestion = 0;
        }
        public ChooseNumber() { }
        
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

        public override bool SetAnswer(Update update, TestManager test)
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
