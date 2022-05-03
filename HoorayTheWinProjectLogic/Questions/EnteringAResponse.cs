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
            TestToBot testToBot = TestToBot.GetInstance();
            long chatId = update.Message!.Chat.Id;
            string message = update.Message.Text!;
            List<string> answers;
            testToBot.Manager.AnswerBase.TryGetValue(chatId, out answers!);
            answers.Add(message);
            return Enums.BehaviorOptions.nextQuestoin;
        }
    }
}
