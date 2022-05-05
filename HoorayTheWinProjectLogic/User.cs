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
        [NonSerialized]
        public int tmp = 0;
        public override string ToString()
        {
            return NameUser;
        }       
    }
}
