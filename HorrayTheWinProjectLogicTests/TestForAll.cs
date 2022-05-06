using NUnit.Framework;
using HoorayTheWinProjectLogic.Questions;
using HoorayTheWinProjectLogic;
using HorrayTheWinProjectLogicTests.HorrayTheWinProjectLogicTestsSources;
namespace HorrayTheWinProjectLogicTests
{
    public class TestsForLinkList
    {

        [TestCaseSource(typeof(AddUserTestSource))]
        public void AddUserTest(User user, Group group, Group expectedGroup)
        {
            Group actualGroup = group;
            group.AddUser(user);
            Assert.AreEqual(expectedGroup, actualGroup);
        }
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
