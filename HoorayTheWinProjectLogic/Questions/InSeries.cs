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
                 InlineKeyboardButton.WithCallbackData("Done")}
             });

            return inlineKeyboard;
        }

        public override InlineKeyboardMarkup GetRefreshInlineKM(List<string> answers)
        {
            List<string> modifyAnswers = ModifyAnswers(answers);
            InlineKeyboardMarkup inlineKeyboard = new(
            new[]
            {
            new[]
            {
                InlineKeyboardButton.WithCallbackData(modifyAnswers[0], Answer[0]),
                InlineKeyboardButton.WithCallbackData(modifyAnswers[1], Answer[1]),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(modifyAnswers[2], Answer[2]),
                InlineKeyboardButton.WithCallbackData(modifyAnswers[3], Answer[3]),
            },
            new[]
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
            string message = update.CallbackQuery!.Data!;
            if (message != "Done")
            {
                PressingNotDone(update, message);
                return Enums.BehaviorOptions.refreshKeybord;
            }
            else
            {
                PressingDone(update);
                TestToBot testToBot = TestToBot.GetInstance();
                if (GetAnswerList(update).Count == testToBot.Manager.Test.AbstractQuestions.Count())
                {
                    return Enums.BehaviorOptions.lastQuestion;
                }
                return Enums.BehaviorOptions.nextQuestion;
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is InSeries))
            {
                return false;
            }
            else
            {
                InSeries objTest = (InSeries)obj;
                if ((objTest.TextOfQuestion != this.TextOfQuestion) && (objTest.TypeQuestion != this.TypeQuestion)
                    && (objTest.Answer.Count == this.Answer.Count))
                {
                    return false;
                }
                else
                {
                    foreach (string answer in objTest.Answer)
                    {
                        int indexOfAnswer = objTest.Answer.IndexOf(answer);
                        if (answer != this.Answer[indexOfAnswer])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        private void PressingDone(Update update)
        {
            TestToBot testToBot = TestToBot.GetInstance();
            List<string> list = GetAnswerList(update);
            int index = testToBot.Manager.Test.AbstractQuestions.IndexOf(this);
            if (list.Count == 0 || list.Count == index)
            {
                list.Add("No answer");
            }
            else if (GetPastIndex(update) == "Empty")
            {
                list.Insert(list.Count - 1, "No answer");
                list.RemoveAt(list.Count - 1);
            }
        }

        private void PressingNotDone(Update update, string message)
        {
            if (GetAnswerList(update).Count == 0)
            {
                GetAnswerList(update).Add(message);
            }
            else
            {
                if (
                    !GetPastIndex(update).Contains(Answer[0])
                    && !GetPastIndex(update).Contains(Answer[1])
                    && !GetPastIndex(update).Contains(Answer[2])
                    && !GetPastIndex(update).Contains(Answer[3])
                    && !GetPastIndex(update).Contains("Empty")
                    )
                {
                    AddNewAnswer(update, message);
                }
                else
                {
                    if (!GetPastIndex(update).Contains(message))
                    {
                        AddNewAnswerToIndex(update, message);
                    }
                    else if (GetPastIndex(update).Contains(message) || GetPastIndex(update).Contains("Empty"))
                    {
                        RemoveNewAnswerFromIndex(update, message);
                    }
                }
            }
        }

        private void AddNewAnswer(Update update, string message)
        {
            GetAnswerList(update).Add(message);
        }

        private void AddNewAnswerToIndex(Update update, string message)
        {
            string pastString = GetPastIndex(update);
            if (pastString == "Empty")
            {
                GetAnswerList(update).Remove(pastString);
                GetAnswerList(update).Add(message);
            }
            else
            {
                pastString = pastString + " " + message;
                GetAnswerList(update).Insert(GetAnswerList(update).Count - 1, pastString);
                GetAnswerList(update).RemoveAt(GetAnswerList(update).Count - 1);
            }
        }

        private void RemoveNewAnswerFromIndex(Update update, string message)
        {
            List<string> list = GetAnswerList(update);
            List<string> values = GetPastIndex(update).Split(' ').ToList();
            if (values.Count > 1)
            {
                values.Remove(message);
                string result = String.Join(" ", values);
                list.Insert(list.Count - 1, result);
                list.RemoveAt(list.Count - 1);
            }
            else if (values.Count == 0 && GetPastIndex(update) == "Empty")
            {
                list.Insert(list.Count - 1, message);
                list.RemoveAt(list.Count - 1);
            }
            else
            {
                list.Insert(list.Count - 1, "Empty");
                list.RemoveAt(list.Count - 1);
            }
        }

        private string GetPastIndex(Update update)
        {
            string pastString = GetAnswerList(update)[GetAnswerList(update).Count - 1];
            return pastString;
        }

        private List<string> GetAnswerList(Update update)
        {
            long chatId = update.CallbackQuery!.Message!.Chat.Id;
            TestToBot testToBot = TestToBot.GetInstance();
            List<string> answers;
            testToBot.Manager.AnswerBase.TryGetValue(chatId, out answers!);
            return answers;
        }

        public List<string> ModifyAnswers(List<string> userAnswers)
        {
            string[] arr = new string[4];
            Answer.CopyTo(arr);
            List<string> modifyAnswer = arr.ToList<string>();
            int i = 0;
            string[] abc = new string[4] { "1️⃣", "2️⃣", "3️⃣", "4️⃣" };
            foreach (var answer in userAnswers)
            {              
                if (modifyAnswer.Contains(answer))
                {
                    int index = modifyAnswer.IndexOf(answer);
                    modifyAnswer[index] = $"{abc[i]} {answer}";
                }
                i++;
            }
            return modifyAnswer;
        }

    }
}

