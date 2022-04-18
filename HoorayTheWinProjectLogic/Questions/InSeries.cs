using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.Questions
{
    public class InSeries : AbstractQuestion
    {
        public InSeries(string question, string answerOne, string answerTwo, string answerThree, string answerFour)
        {
            List<string> Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = "5";
            Answer.Add(answerOne);
            Answer.Add(answerTwo);
            Answer.Add(answerThree);
            Answer.Add(answerFour);
            AnswerUser = Answer;
        }

    }
}

