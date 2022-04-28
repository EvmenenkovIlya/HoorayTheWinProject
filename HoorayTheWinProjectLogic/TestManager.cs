using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic
{
    public class TestManager
    {
        public Test TestForStart { get; set; }
        public List<Group> Groups { get; set; }
        public Dictionary<long, List<string>> AnswerBase { get; set; } = new Dictionary<long, List<string>>();

        public TestManager()
        {
            List<Group> groupsForTest = (DataMock.groups.Where(x => x.IsSelected == true)).ToList();
            Groups = groupsForTest;
            foreach (Group group in groupsForTest)
            {
                foreach (User user in group.Users)
                {
                    AnswerBase.Add(user.ChatId, new List<string>());
                }
            }
        }

        public void GetReport()
        { 
        
        }
    }
}
