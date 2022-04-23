using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic;
using HoorayTheWinProject_;
using HoorayTheWinProjectLogic.Questions;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace HoorayTheWinProject_.TestLogicInTG
{
    public class InSeriesTG
    {
        public static InlineKeyboardMarkup InlineKM(InSeries question) 
        {
            InlineKeyboardMarkup inlineKeyboard = new(
             new[]
             {
             new []
             {
                 InlineKeyboardButton.WithCallbackData((string)question.Answer[0], "First Answer"),
                 InlineKeyboardButton.WithCallbackData((string)question.Answer[1], "Second Answer"),
             },
             new []
             {
                 InlineKeyboardButton.WithCallbackData((string)question.Answer[2], "Third Answer"),
                 InlineKeyboardButton.WithCallbackData((string)question.Answer[3], "Fourth Answer"),
             },
             new []
             {   
                 InlineKeyboardButton.WithCallbackData("Done", "Finished")}
             });
            return inlineKeyboard;
        }
        
    }
}
