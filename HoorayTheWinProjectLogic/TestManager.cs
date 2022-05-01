using HoorayTheWinProjectLogic.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HoorayTheWinProjectLogic
{
    public class TestManager
    {
        GroupStorage groups = GroupStorage.GetInstance();
        public Test Test { get; set; }

        public long ChatId { get; set; }

        public List<Group> Groups { get; set; }

        public Dictionary<long, List<string>> AnswerBase { get; set; } = new Dictionary<long, List<string>>();

        public int QuestionIndex { get; set; }

        public TestManager(Test test)
        {
            List<Group> groupsForTest = (groups.groups.Where(x => x.IsSelected == true)).ToList();
            Groups = groupsForTest;
            foreach (Group group in groupsForTest)
            {
                foreach (User user in group.Users)
                {
                    ChatId = user.ChatId;
                    AnswerBase.Add(user.ChatId, new List<string>());
                }
            }
            Test = test;
            QuestionIndex = 0;
        }

        public void GetReport()
        { 
        
        }
    }
}
