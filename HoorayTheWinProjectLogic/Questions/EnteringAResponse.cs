using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.Questions
{
    public class EnteringAResponse : AbstractQuestion
    {
        public EnteringAResponse(string question)
        {
            List<string> Answer = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = 2;           
            base.Answer = Answer;

        }
    }
}
