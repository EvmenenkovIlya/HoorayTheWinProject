using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.Questions
{
    public class YesNo : AbstractQuestion
    {
        public YesNo(string question)
        {
            List<string> AnswerUser = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = "2";
            AnswerUser.Add("Yes");
            AnswerUser.Add("No");           
        }
    }
}