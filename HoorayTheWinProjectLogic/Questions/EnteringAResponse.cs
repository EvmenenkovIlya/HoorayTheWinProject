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
            List<string> Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = "2";
            Answer.Add(answerOfUser);
            base.Answer = Answer;
        }
    }
}
