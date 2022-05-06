using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace HoorayTheWinProjectLogic.Questions
{
    public abstract class AbstractQuestion
    {
        public string TextOfQuestion { get; set; }

        public int TypeQuestion { get; set; }

        public List <string> Answer { get; set; }       
        
        public abstract InlineKeyboardMarkup GetInlineKM();
        public abstract Enums.BehaviorOptions SetAnswer(Update update);
        public abstract InlineKeyboardMarkup GetRefreshInlineKM(List<string> answers);
        public override string ToString()
        {
            return TextOfQuestion;
        }
    }
}