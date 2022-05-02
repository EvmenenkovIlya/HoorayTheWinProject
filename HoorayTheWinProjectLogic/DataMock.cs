using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic.Questions;
using Telegram.Bot.Types;

namespace HoorayTheWinProjectLogic
{
    public static class DataMock
    {
        public static List<string> forComboBox = new List<string>() { "CHOOSE NUMBER", "CHOOSE ONE", "WRITE A REPONSE", "IN SERIES", "YES OR NO" };

        public static TestManager testToStart;

        public static bool IsTesting = false;
    }
}
