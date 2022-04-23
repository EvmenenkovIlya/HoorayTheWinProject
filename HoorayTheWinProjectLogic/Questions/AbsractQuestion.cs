namespace HoorayTheWinProjectLogic.Questions
{
    public abstract class AbstractQuestion
    {
        public string TextOfQuestion { get; set; }

        public int TypeQuestion { get; set; }

        public List <string> Answer { get; set; }

        public override string ToString()
        {
            return TextOfQuestion;
        }
    }
}