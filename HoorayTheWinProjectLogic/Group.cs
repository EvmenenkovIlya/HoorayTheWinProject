using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic
{
    public class Group
    {
        public string NameGroup { get; set; }
        public List<User> Users { get; set; }
        public bool IsSelected { get; set; }
        public Group(string nameGroup)
        {
            NameGroup = nameGroup;
            Users = new List<User>();
        }
        public Group()
        {

        }
        public void AddUser(User user)
        {
            if (user == null)
            {
                throw new NullReferenceException();
            }
            Users.Add(user);
        }
        public void RemoveUser(User user)
        {
            if (Users.Count < 1)
            {
                throw new Exception("The group is empty");
            }

            Users.Remove(user);
        }
        public override string ToString()
        {
            return NameGroup;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null || !(obj is Group))
            {
                return false;
            }
            else
            {
                Group objGroup = (Group)obj;
                if ((objGroup.Users.Count != this.Users.Count) && (objGroup.NameGroup != this.NameGroup))
                {

                    return false;
                }
                else
                {
                    foreach (User user in objGroup.Users)
                    {
                        List<User> users = this.Users.Where(x => x.NameUser == user.NameUser && x.ChatId == user.ChatId).ToList();
                        if (users.Count != 1)
                        {
                            return false;
                        }

                    }
                    return true;

                }
            }
        }

    }
}
