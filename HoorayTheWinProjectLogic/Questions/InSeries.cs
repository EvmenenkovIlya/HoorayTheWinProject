﻿using System;
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
            TextOfQuestion = question;
            TypeQuestion = "5";
            AnswerUser[0] = answerOne;
            AnswerUser[1] = answerTwo;
            AnswerUser[2] = answerThree;
            AnswerUser[3] = answerFour;
        }
        public override void ChangeText(string question)
        {
            TextOfQuestion = question;
        }
    }
}

