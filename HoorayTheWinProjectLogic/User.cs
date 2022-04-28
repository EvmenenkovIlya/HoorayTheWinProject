using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace HoorayTheWinProjectLogic
{
    public class User
    {
        public string NameUser { get; set; }

        public long ChatId { get; private set; }

        public User(Chat chat)
        {
            NameUser = chat.Username!;
            ChatId = chat.Id;
        }
        public override string ToString()
        {
            return NameUser;
        }
        
    }
}
