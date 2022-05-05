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
            new[]
            {
                InlineKeyboardButton.WithCallbackData(Answer[0], Answer[0]),
                InlineKeyboardButton.WithCallbackData(Answer[1], Answer[1]),
            },
            new[]
            {
                InlineKeyboardButton.WithCallbackData(Answer[2], Answer[2]),
                InlineKeyboardButton.WithCallbackData(Answer[3], Answer[3]),
            },
            new[]
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

        private void PressingDone(Update update)
        {
            List<string> list = GetAnswerList(update);
            if (list.Count == 0)
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
            List<string> answers = GetAnswerList(update);
            if (answers.Count == 0)
            {
                answers.Add(message);
            }
            else
            {
                if (IsInAnswer(update))                   
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
            TestToBot testToBot = TestToBot.GetInstance();
            List<string> values = GetPastIndex(update).Split(' ').ToList();
            List<string> answer = testToBot.Manager.AnswerBase[update.CallbackQuery.Message!.Chat.Id];
            if (values.Count > 1)
            {
                values.Remove(message);
                string result = String.Join(" ", values);
                answer[answer.Count - 1] = result;
            }
            else if (values.Count == 0 && GetPastIndex(update) == "Empty")
            {
                answer[answer.Count - 1] = message;
            }
            else
            {
                answer[answer.Count - 1] = "Empty";
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
            List<string> answers = testToBot.Manager.AnswerBase[chatId];
            return answers;
        }
        public List<string> ModifyAnswers(List<string> userAnswers)
        {
            string[] arr = new string[4];
            Answer.CopyTo(arr);
            List<string>  modifyAnswer = arr.ToList<string>();
            foreach (var answer in userAnswers)
            {
                if (modifyAnswer.Contains(answer))
                {
                    int index = modifyAnswer.IndexOf(answer);
                    modifyAnswer[index] = $"{answer} ✅";
                }
            }
            return modifyAnswer;
        }
        private bool IsInAnswer(Update update)
        {
            return (
                    !GetPastIndex(update).Contains(Answer[0])
                    && !GetPastIndex(update).Contains(Answer[1])
                    && !GetPastIndex(update).Contains(Answer[2])
                    && !GetPastIndex(update).Contains(Answer[3])
                    && !GetPastIndex(update).Contains("Empty")
                    );
        }
    }
}
