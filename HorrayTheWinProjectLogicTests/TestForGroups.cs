using NUnit.Framework;
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

    }
}
