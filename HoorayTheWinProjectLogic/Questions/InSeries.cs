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
            TestToBot testToBot = TestToBot.GetInstance();
            testToBot.Manager.AnswerBase.TryGetValue(chatId, out answers!);
            string pastString = answers[answers.Count - 1];
            return Enums.BehaviorOptions.refreshKeybord;
            string past = pastString.Replace(" ", "");
            if (message != "Done")
            {
                if (answers.Count == 0)
                {
                    answers.Add(message);
                }
                else
                {
                    if (
                        !past.Contains(Answer[0])
                        && !past.Contains(Answer[1])
                        && !past.Contains(Answer[2])
                        && !past.Contains(Answer[3])
                        && !pastString.Contains("No answer")
                        )
                    {
                        answers.Add(message);
                        return Enums.BehaviorOptions.refreshKeybord;
                    }
                    else
                    {
                        if (!pastString.Contains(message))
                        {
                            if (pastString == " ")
                            {
                                pastString = (pastString + " " + message).Replace(" ", "");
                            }
                            else
                            {
                                pastString = pastString + " " + message;

                            }
                            answers.Insert(answers.Count - 1, pastString);
                            answers.RemoveAt(answers.Count - 1);
                            return Enums.BehaviorOptions.refreshKeybord;
                        }
                        else if (pastString.Contains(message) || pastString.Contains(" "))
                        {
                            List<string> values = pastString.Split(' ').ToList();
                            if (values.Count > 1)
                            {
                                values.Remove(message);
                                string result = String.Join(" ", values);
                                answers.Insert(answers.Count - 1, result);
                                answers.RemoveAt(answers.Count - 1);
                            }
                            else if (values.Count == 0 && pastString == " ")
                            {
                                answers.Insert(answers.Count - 1, message);
                                answers.RemoveAt(answers.Count - 1);
                            }
                            else
                            {
                                answers.Insert(answers.Count - 1, " ");
                                answers.RemoveAt(answers.Count - 1);
                            }
                        }
                        return Enums.BehaviorOptions.refreshKeybord;
                    }
                }
            }
            else
            {
                if (answers.Count == 0)
                {
                    answers.Add("No answer");
                }
                else if (answers[answers.Count - 1] == " ")
                {
                    answers.Insert(answers.Count - 1, "No answer");
                    answers.RemoveAt(answers.Count - 1);
                }
                return Enums.BehaviorOptions.nextQuestion;
            }
            return Enums.BehaviorOptions.invalidAnswer;
        }
    }
}

