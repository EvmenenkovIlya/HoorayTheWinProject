﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

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
        public override InlineKeyboardMarkup GetInlineKM()
        {
            return null;
        }

        public override bool SetAnswer(Update update, TestManager test)
        {
            List<string> answers;
            test.AnswerBase.TryGetValue(update.Message!.Chat.Id, out answers!);
            answers.Add(update.Message.Text!);
            return true;
        }
    }
}
