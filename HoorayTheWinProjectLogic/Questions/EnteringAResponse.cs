using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.Questions
{
    public class EnteringAResponse : AbstractQuestion
    {
        public EnteringAResponse(string question, string answerOfUser)
        {
            List<string> AnswerUser = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = "1";
            AnswerUser.Add(answerOfUser);

        }
    }
}
