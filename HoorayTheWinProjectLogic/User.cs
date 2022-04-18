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
        public User(string nameUser)
        {
            NameUser = nameUser;
        }
        public override string ToString()
        {
            return NameUser;
        }
    }
}
