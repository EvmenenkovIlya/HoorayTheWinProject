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

        public static TestManager _testToStart;

        public static bool IsTesting = false;

        public static Test _bankOfQuestions = new Test("Bank of Questions");

        public static Test testMock = new Test("TestForTest")
        {
            AbstractQuestions = { new ChooseOne("Who is the author of War and Peace?", "Lermontov", "Dostoevskiy", "Tolstoy", "Leskov"),
                                  new ChooseNumber("What kind of animal have a four legs?", "Fish", "Kangaroo", "Tiger", "Panda"),
                                  new InSeries("Sort in right order the authors by their year of birth", "Mayakovskiy", "Pushkin", "Tyutchev", "Pelevin"),
                                  new YesNo("Is an year contains 365 days?", "yes", "no"),
                                  new EnteringAResponse("What is the color of banan?")
            }
        };

        public static List<Test> tests = new List<Test>() { _bankOfQuestions, testMock };
    }
}
