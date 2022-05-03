using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic.Questions;


namespace HoorayTheWinProjectLogic
{
    public class Report
    {
        public string Name { get; set; }
        public string Question { get; set; }
        public List<string> UserAnswer { get; set; }

        public Report(User user, AbstractQuestion question, AbstractQuestion answer)
        {
            Name = user.NameUser;
            Question = question.TextOfQuestion;
            UserAnswer = answer.Answer;
        }

        public Report() 
        {
            Name = "Vasya";
            Question = "ndhdjdhs";
            UserAnswer = new List<string>();
        }
    }
}
