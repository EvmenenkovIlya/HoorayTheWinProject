using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.Questions
{
    public class ChooseNumber : AbstractQuestion
    {
        public ChooseNumber(string question, string answerOne, string answerTwo, string answerThree, string answerFour)
        {
            List<string> Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = 0;
            Answer.Add(answerOne);
            Answer.Add(answerTwo);
            Answer.Add(answerThree);
            Answer.Add(answerFour);
            base.Answer = Answer;

        }
    }
}
