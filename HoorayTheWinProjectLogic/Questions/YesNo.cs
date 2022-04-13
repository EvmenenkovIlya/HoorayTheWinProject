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
            TextOfQuestion = question;
            TypeQuestion = "2";
            AnswerUser[0] = "Yes";
            AnswerUser[1] = "No";
        }
        public override void ChangeText(string question)
        {
            TextOfQuestion = question;
        }
    }
}