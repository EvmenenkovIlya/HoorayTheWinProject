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
        public string NameTest { get; private set; }
        //public List<Group> Groups { get; private set; }
        public List <AbstractQuestion> AbstractQuestions { get; private set; }
        public void ChangeName(string nameTest)
        {
            NameTest = nameTest;    
        }
        public void AddQuestion(AbstractQuestion abstractQuestion)
        {
            if (abstractQuestion == null)
            {
                throw new NullReferenceException();
            }
            AbstractQuestions.Add(abstractQuestion); 
        }

        public void RemoveQuestion(int index)
        {
            if (AbstractQuestions.Count < 1)
            {
                throw new Exception("The list of questions is empty");
            }
            AbstractQuestions.RemoveAt(index);
        }
    }
}
