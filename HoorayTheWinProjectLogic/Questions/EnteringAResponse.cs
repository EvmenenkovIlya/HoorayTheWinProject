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
    public class EnteringAResponse : AbstractQuestion
    {
        public EnteringAResponse(string question)
        {
            base.Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = 2;
        }
        public EnteringAResponse() { }
        public override InlineKeyboardMarkup GetInlineKM()
        {
            return null;
        }

        public override Enums.BehaviorOptions SetAnswer(Update update)
        {
            if (update.Message == null)
            {
                return Enums.BehaviorOptions.invalidAnswer;
            }
            TestToBot testToBot = TestToBot.GetInstance();
            long chatId = update.Message!.Chat.Id;
            List<string> answers = testToBot.Manager.AnswerBase[chatId];
            answers.Add(update.Message.Text!);
            if (answers.Count == testToBot.Manager.Test.AbstractQuestions.Count())
            {
                return Enums.BehaviorOptions.lastQuestion;
            }
            return Enums.BehaviorOptions.nextQuestion;
        }
    }
}
