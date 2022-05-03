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
        public List<AbstractQuestion> AbstractQuestions { get; set; }       
        public Test(string nameTest)
        {
            NameTest = nameTest;
            AbstractQuestions = new List<AbstractQuestion>();
        }
        public Test()
        { 
        
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
        public override string ToString()
        {
            return NameTest;
        }
    }
}
