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
        public Dictionary<long, List<string>> AnswerBase { get; set; } = new Dictionary<long, List<string>>();
        public Test(string nameTest)
        {
            NameTest = nameTest;
            Groups = new List<Group>();
            AbstractQuestions = new List<AbstractQuestion>();
        }

        public void AddDictionary()
        {
            AnswerBase = new Dictionary<long, List<string>>();
        }
        public Test GetClone()
        {
            List<AbstractQuestion> newQuestions = new List<AbstractQuestion>();
            foreach (AbstractQuestion question in AbstractQuestions)
            {
                newQuestions.Add(question);
            }
            List<Group> newGroups = new List<Group>();
            foreach (Group group in Groups)
            {
                newGroups.Add(group);
            }
            return new Test(NameTest)
            {
                NameTest = NameTest,
                Groups = newGroups,
                AbstractQuestions = newQuestions
            };
        }

        public void AddQuestion(AbstractQuestion abstractQuestion)
        {
            if (abstractQuestion == null)
            {
                throw new NullReferenceException();
            }
            AbstractQuestions.Add(abstractQuestion);
        }

        public void DeleteQuestion(AbstractQuestion abstractQuestion)
        {
            if (AbstractQuestions.Count < 1)
            {
                throw new Exception("There are no questions in the test");
            }

            AbstractQuestions.Remove(abstractQuestion);
        }

        public void AddGroup(Group group)
        {
            if (group == null)
            {
                throw new NullReferenceException();
            }
            Groups.Add(group);
        }

        public void DeleteGroup(Group group)
        {
            if (Groups.Count < 1)
            {
                throw new Exception("There are no groups in the test");
            }

            Groups.Remove(group);
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
