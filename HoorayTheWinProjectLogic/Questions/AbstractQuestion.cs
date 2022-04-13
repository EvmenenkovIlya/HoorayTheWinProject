namespace HoorayTheWinProjectLogic.Questions
{
    public abstract class AbstractQuestion
    {
        public string TextOfQuestion { get; protected set; }

        public string TypeQuestion { get; protected set; }

        public List <string> AnswerUser { get; protected set; }

        public virtual void ChangeText(string question)
        {
            
        }

    }
}