using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic.Questions;

namespace HoorayTheWinProjectLogic
{
    public class Test
    {
        public string NameTest { get; set; }
        public List<Group> Groups { get; set; }
        public List<AbstractQuestion> AbstractQuestions { get; set; }
        public bool IsSelected { get; set; }
        public Test(string nameTest)
        {
            NameTest = nameTest;
            Groups = new List<Group>();
            AbstractQuestions = new List<AbstractQuestion>();
        }

        public void AddQuestion(AbstractQuestion abstractQuestion)
        {
            if (abstractQuestion == null)
            {
                throw new NullReferenceException();
            }
            AbstractQuestions.Add(abstractQuestion);
        }

        public void DeleteQuestion(int index)
        {
            if (AbstractQuestions.Count < 1)
            {
                throw new Exception("The group is empty");
            }

            AbstractQuestions.RemoveAt(index);
        }

        public void AddGroup(Group group)
        {
            if (group == null)
            {
                throw new NullReferenceException();
            }
            Groups.Add(group);
        }

        public void DeleteGroup(int index)
        {
            if (Groups.Count < 1)
            {
                throw new Exception("The group is empty");
            }

            Groups.RemoveAt(index);
        }

        public void StartTest()
        {

        }

        public void FinishTest()
        {

        }
        public override string ToString()
        {
            return NameTest;
        }
    }
}
