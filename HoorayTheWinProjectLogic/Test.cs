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

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Test))
            {
                return false;
            }
            else
            {
                Test objTest = (Test)obj;
                if((objTest.AbstractQuestions.Count != this.AbstractQuestions.Count) && (objTest.NameTest != this.NameTest))
                {
                    return false;
                }
                else
                {
                    foreach (AbstractQuestion abstractQuestion in objTest.AbstractQuestions)
                    {
                        int index = objTest.AbstractQuestions.IndexOf(abstractQuestion);
                        if ((abstractQuestion.TextOfQuestion == this.AbstractQuestions[index].TextOfQuestion) 
                            && (abstractQuestion.TypeQuestion == this.AbstractQuestions[index].TypeQuestion)
                            && (abstractQuestion.Answer.Count == this.AbstractQuestions[index].Answer.Count )) 

                        {
                            foreach (string answer in abstractQuestion.Answer)
                            {
                                int indexOfAnswer = abstractQuestion.Answer.IndexOf(answer);
                                if (answer != this.AbstractQuestions[index].Answer[indexOfAnswer])
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    
                    return true;
                }
            }
        }
    }
}
