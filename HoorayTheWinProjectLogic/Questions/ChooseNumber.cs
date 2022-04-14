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
            TextOfQuestion = question;
            TypeQuestion = "4";
            AnswerUser[0] = answerOne;
            AnswerUser[1] = answerTwo;
            AnswerUser[2] = answerThree;
            AnswerUser[3] = answerFour;
        }
    }
}
