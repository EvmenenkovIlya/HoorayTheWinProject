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
            long chatId = update.Message!.Chat.Id;
            string message = update.Message.Text!;
            List<string> answers;
            DataMock._testToStart.AnswerBase.TryGetValue(chatId, out answers!);
            answers.Add(message);
            return Enums.BehaviorOptions.nextQuestoin;
        }
    }
}
