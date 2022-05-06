using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Telegram.Bot.Types;
using HoorayTheWinProjectLogic;
using HoorayTheWinProjectLogic.Questions;
using HorrayTheWinProjectLogicTests.QuestionsTestSource;

namespace HorrayTheWinProjectLogicTests
{
    [TestFixture]
    public class QuestiosTests
    {
        [TestCaseSource(typeof(EnteringAResponseSetAnswerTestSource))]
        public void EnteringAResponseSetAnswerTest(Update update, Enums.BehaviorOptions expected)
        {
            Enums.BehaviorOptions behaviorOptions = new EnteringAResponse().SetAnswer(update.Message!.Text);
            Enums.BehaviorOptions actual = behaviorOptions;
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(ChooseNumberSetAnswerTestSource))]
        public void ChooseNumberSetAnswerTest(Update update, Enums.BehaviorOptions expected)
        {
            Enums.BehaviorOptions behaviorOptions = new ChooseNumber().SetAnswer(update.CallbackQuery!.Data);
            Enums.BehaviorOptions actual = behaviorOptions;
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(ChooseOneSetAnswerTestSource))]
        public void ChooseOneSetAnswerTest(Update update, Enums.BehaviorOptions expected)
        {
            Enums.BehaviorOptions behaviorOptions = new ChooseOne().SetAnswer(update.CallbackQuery!.Data);
            Enums.BehaviorOptions actual = behaviorOptions;
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(InSeriesSetAnswerTestSource))]
        public void InSeriesSetAnswerTest(Update update, Enums.BehaviorOptions expected)
        {
            Enums.BehaviorOptions behaviorOptions = new EnteringAResponse().SetAnswer(update.CallbackQuery!.Data);
            Enums.BehaviorOptions actual = behaviorOptions;
            Assert.AreEqual(expected, actual);
        }

        [TestCaseSource(typeof(YesNoSetAnswerTestSource))]
        public void YesNoSetAnswerTest(Update update, Enums.BehaviorOptions expected)
        {
            Enums.BehaviorOptions behaviorOptions = new EnteringAResponse().SetAnswer(update.CallbackQuery!.Data);
            Enums.BehaviorOptions actual = behaviorOptions;
            Assert.AreEqual(expected, actual);
        }
    }
}
