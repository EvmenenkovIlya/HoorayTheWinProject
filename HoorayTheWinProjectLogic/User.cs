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
        public long ChatId { get; set; }
        public User(Chat chat)
        {
            NameUser = $"{chat.FirstName} {chat.LastName}";
            ChatId = chat.Id;
        }
        public User()
        { }

        public User(string name, long chatId)
        {
            NameUser = name;
            ChatId = chatId;
        }
        public override string ToString()
        {
            return NameUser;
        }       
    }
}
