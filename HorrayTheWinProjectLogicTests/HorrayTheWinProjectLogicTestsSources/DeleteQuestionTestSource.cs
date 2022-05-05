using HoorayTheWinProjectLogic.Questions;
using HoorayTheWinProjectLogic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HorrayTheWinProjectLogicTests
{
    public class DeleteQuestionTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            ChooseNumber chooseNumber = new ChooseNumber("What is afsdf&", "a", "b", "c", "d");
            Test expectedTest = new Test("testik");
            Test actualTest = new Test("testik");
            actualTest.AbstractQuestions = new List<AbstractQuestion> { chooseNumber };
            expectedTest.AbstractQuestions = new List<AbstractQuestion> { };
            yield return new object[] { chooseNumber, actualTest, expectedTest };

            ChooseOne chooseOne = new ChooseOne("Who are you?", "a", "b", "c", "d");
            expectedTest = new Test("testik");
            actualTest = new Test("testik");
            actualTest.AbstractQuestions = new List<AbstractQuestion> { chooseOne };
            expectedTest.AbstractQuestions = new List<AbstractQuestion> { };
            yield return new object[] { chooseOne, actualTest, expectedTest };
        }
    }
}
