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

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is EnteringAResponse))
            {
                return false;
            }
            else
            {
                EnteringAResponse objTest = (EnteringAResponse)obj;
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
    }
}
