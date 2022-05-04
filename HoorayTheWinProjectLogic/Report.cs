using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic.Data;
using HoorayTheWinProjectLogic.Questions;


namespace HoorayTheWinProjectLogic
{
    public class Report
    {
        public string Name { get; set; }
        public List<string> Questions { get; set; } = new List<string>();
        public List<string> UserAnswer { get; set; } = new List<string>();
        public Report(User user)
        {
            TestToBot testToBot = TestToBot.GetInstance();
            Name = user.NameUser;
            foreach (var question in testToBot.Manager.Test.AbstractQuestions)
            {
                Questions.Add(question.TextOfQuestion);
            }
            UserAnswer = testToBot.Manager.AnswerBase[user.ChatId];
        }

    }
}
