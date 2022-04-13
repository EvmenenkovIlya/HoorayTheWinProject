using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic
{
    public class Group
    {
        public string NameGroup { get; private set; }
        public List<User> Users { get; private set; }
        public Group(string nameGroup)
        {
            NameGroup = nameGroup;
            Users = new List<User>();
        }

        public void AddUser(User user) 
        {
            if (user == null)
            {
                throw new NullReferenceException();
            }
            Users.Add(user);
        }

        public void RemoveUser(int index)
        {
            if (Users.Count < 1)
            {
                throw new Exception("The group is empty");
            }

            Users.RemoveAt(index);
        }

        public void ChangeName(string newName)
        {
            NameGroup = newName;
        }
    }
}
