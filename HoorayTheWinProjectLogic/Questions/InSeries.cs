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
            base.Answer = new List<string>() { answerOne, answerTwo, answerThree, answerFour };
            TextOfQuestion = question;
            TypeQuestion = 3;
        }
        public InSeries() { }
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
             },
             new []
             {
                 InlineKeyboardButton.WithCallbackData("Done")}
             });

            return inlineKeyboard;
        }

        public override Enums.BehaviorOptions SetAnswer(Update update)
        {
            if (update.Message != null)
            {
                return Enums.BehaviorOptions.invalidAnswer;
            }
            long chatId = update.CallbackQuery!.Message!.Chat.Id;
            string message = update.CallbackQuery.Data!;
            List<string> answers;
            DataMock.testToStart.AnswerBase.TryGetValue(chatId, out answers!);
            string pastString = answers[answers.Count - 1];
            if (message != "Done")
            {
                if (answers.Count == 0)
                {
                    answers.Add(message);
                }
                else
                {
                    foreach (var item in Answer)
                    {
                        if (!pastString.Contains(item))
                        {
                            answers.Add(message);
                            return Enums.BehaviorOptions.refreshKeybord;
                        }
                        else
                        {
                            if (pastString.Contains(message))
                            {
                                return Enums.BehaviorOptions.refreshKeybord;
                            }
                            pastString = pastString + ", " + message;
                            answers.Insert(answers.Count - 1, pastString);
                            answers.RemoveAt(answers.Count - 1);
                            return Enums.BehaviorOptions.refreshKeybord;
                        }
                    }
                }
            }
            else
            {
                if (answers.Count == 0)
                {
                    answers.Add("No answer");
                }
                return Enums.BehaviorOptions.nextQuestoin;
            }
            return Enums.BehaviorOptions.invalidAnswer;
        }
    }
}

