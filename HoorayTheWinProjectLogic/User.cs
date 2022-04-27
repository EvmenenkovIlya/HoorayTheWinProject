using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic
{
    public class User
    {
        public string NameUser { get; set; }

        public long ChatId { get; private set; }

        public List<string> Answers { get; set; }

        public User(string nameUser)
        {
            NameUser = nameUser;
        }
        public User(string nameUser, long chatId)
        {
            NameUser = nameUser;
            ChatId = 1;
        }
        public override string ToString()
        {
            return NameUser;
        }
        
    }
}
