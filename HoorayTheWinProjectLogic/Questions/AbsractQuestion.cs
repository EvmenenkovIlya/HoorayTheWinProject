namespace HoorayTheWinProjectLogic.Questions
{
    public abstract class AbstractQuestion
    {
        public string TextOfQuestion { get; set; }

        public string TypeQuestion { get; set; }

        public List <string> AnswerUser { get; set; }

    }
}