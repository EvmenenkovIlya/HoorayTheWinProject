using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic;
using HoorayTheWinProjectLogic.Questions;


namespace HorrayTheWinProjectLogicTests
{
    public class AddQuestionTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            ChooseNumber chooseNumber = new ChooseNumber("What is afsdf&", "a", "b", "c", "d");
            Test expectedTest = new Test("testik");
            Test actualTest = new Test("testik");
            expectedTest.AbstractQuestions = new List<AbstractQuestion> { chooseNumber };
            yield return new object[] { chooseNumber, actualTest, expectedTest };

            ChooseOne chooseOne = new ChooseOne("Who are you?", "a", "b", "c", "d");
            expectedTest = new Test("testik");
            actualTest = new Test("testik");
            expectedTest.AbstractQuestions = new List<AbstractQuestion> { chooseOne };
            yield return new object[] { chooseOne, actualTest, expectedTest };

            EnteringAResponse enteringAResponse = new EnteringAResponse("Where am I?");
            expectedTest = new Test("testik");
            actualTest = new Test("testik");
            expectedTest.AbstractQuestions = new List<AbstractQuestion> { enteringAResponse };
            yield return new object[] { enteringAResponse, actualTest, expectedTest };

            InSeries inSeries = new InSeries("How are you feel?", "nice", "bad", "very bad", "amazing");
            expectedTest = new Test("testik");
            actualTest = new Test("testik");
            expectedTest.AbstractQuestions = new List<AbstractQuestion> { inSeries };
            yield return new object[] { inSeries, actualTest, expectedTest };

            YesNo yesNo = new YesNo("Are you a developer?", "yes", "no");
            expectedTest = new Test("testik");
            actualTest = new Test("testik");
            expectedTest.AbstractQuestions = new List<AbstractQuestion> { yesNo };
            yield return new object[] { yesNo, actualTest, expectedTest };
        }
    }
}
