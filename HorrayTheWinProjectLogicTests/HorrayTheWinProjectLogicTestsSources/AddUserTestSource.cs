using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic;

namespace HorrayTheWinProjectLogicTests.HorrayTheWinProjectLogicTestsSources
{
    public class AddUserTestSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            User user = new User();
            user.NameUser = "BlaBla";
            user.ChatId = 15698;
            Group group = new Group();
            group.Users = new List<User> ();
            Group expectedGroup = new Group();
            expectedGroup.Users = new List<User> { new User ("BlaBla", 15698) };
            yield return new object []{user, group, expectedGroup };
            
        }
    }
}
