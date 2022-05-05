using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Telegram.Bot.Types;
using HoorayTheWinProjectLogic;
using HoorayTheWinProjectLogic.Questions;



namespace HorrayTheWinProjectLogicTests
{
    [TestFixture]
    public class EnteringAResponseTests
    {
        [TestCase(-1.5, -2, 7)]
        public void SetAnswerTest(Update update, Update expected)
        {
            double actual = HoorayTheWinProjectLogic.Questions.AbstractQuestion. (update);
            Assert.AreEqual(expected, actual);
        }
    }
}
