﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic.Questions
{
    public class EnteringAResponse : AbstractQuestion
    {
        public EnteringAResponse (string question, string answerOfUser)
        { 
            TextOfQuestion = question;
            TypeQuestion = "1";
            AnswerUser[0]=answerOfUser;
        }
        public override void ChangeText(string question)
        {
            TextOfQuestion = question;
        }

    }
}
