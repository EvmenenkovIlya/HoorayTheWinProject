﻿using System;
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
            List<string> AnswerUser = new List<string>();
            TextOfQuestion = question;
            TypeQuestion = "4";
            AnswerUser.Add(answerOne);
            AnswerUser.Add(answerTwo);
            AnswerUser.Add(answerThree);
            AnswerUser.Add(answerFour);
            
        }
    }
}
