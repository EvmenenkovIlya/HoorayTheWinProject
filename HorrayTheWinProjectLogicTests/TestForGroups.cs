using NUnit.Framework;
using HoorayTheWinProjectLogic.Questions;
using HoorayTheWinProjectLogic;
namespace HorrayTheWinProjectLogicTests
{
    public class TestsForLinkList
    {
        //[TestCaseSource(typeof(AddInTheEndTestSource))]
        //public void AddUserToGroup()
        //{
        //    list.AddInTheEnd(value);
        //    LinkList actualList = list;
        //    Assert.AreEqual(expectedList, actualList);
        //}

        [TestCaseSource(typeof(AddQuestionTestSource))]
        public void AddQuestion(AbstractQuestion abstractQuestion,  Test actualTest, Test expectedTest)
        {
            actualTest.AddQuestion(abstractQuestion);
            Assert.AreEqual( expectedTest, actualTest);
        }
        [TestCaseSource(typeof(DeleteQuestionTestSource))]
        public void DeleteQuestion(AbstractQuestion abstractQuestion, Test actualTest, Test expectedTest)
        {
            actualTest.DeleteQuestion(abstractQuestion);
            Assert.AreEqual(expectedTest, actualTest);
        }
    }
}
