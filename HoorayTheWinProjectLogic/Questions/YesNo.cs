using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.Questions
{
    public class YesNo : AbstractQuestion
    {
        public YesNo(string question, string answerOne, string answerTwo)
        {
            List<string> Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = 4;
            Answer.Add(answerOne);
            Answer.Add(answerTwo);
            base.Answer = Answer;
        }
    }
}