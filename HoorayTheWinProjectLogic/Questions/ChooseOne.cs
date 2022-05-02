using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HoorayTheWinProjectLogic.Questions
{
    public class ChooseOne : AbstractQuestion
    {       
        public ChooseOne (string question, string answerOne, string answerTwo, string answerThree, string answerFour)
        {
            base.Answer = new List<string>() { answerOne, answerTwo, answerThree, answerFour };
            TextOfQuestion = question;
            TypeQuestion = 1;
        }
        public ChooseOne() { }
        public override InlineKeyboardMarkup GetInlineKM()
        {
            InlineKeyboardMarkup inlineKeyboard = new(
             new[]
             {
             new []
             {
                 InlineKeyboardButton.WithCallbackData(Answer[0]),
                 InlineKeyboardButton.WithCallbackData(Answer[1]),
             },
             new []
             {
                 InlineKeyboardButton.WithCallbackData(Answer[2]),
                 InlineKeyboardButton.WithCallbackData(Answer[3]),
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
                    DataMock.testToStart.AnswerBase.TryGetValue(chatId, out answers!);
                    answers.Add(message);
                    return Enums.BehaviorOptions.nextQuestoin;
                }
            }
            return Enums.BehaviorOptions.invalidAnswer;
        }
    }
}
