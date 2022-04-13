using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoorayTheWinProjectLogic
{
    public class Group
    {
        public string NameGroup { get; private set; }
        public List<User> Users { get; private set; }

        public ObservableCollection<Node> Groups { get; private set; }

        public Group(string nameGroup)
        {
            NameGroup = nameGroup;
            Users = new List<User>();
            Groups = new ObservableCollection<Node>();
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



        /// </summary>
        
        
            ObservableCollection<Node> groups;
            

                groups = new ObservableCollection<Node>
        {
            new Node
            {
                Name ="Group Фиксики",
                Nodes = new ObservableCollection<Node>
                {
                    new Node {Name="Чинила 1" },
                    new Node {Name="Чинила 2" },
                }
            },
            new Node
            {
                Name ="Group Лунтик's friends",
                Nodes = new ObservableCollection<Node>
                {
                    new Node {Name="Вупсень" },
                    new Node {Name="Пупсень" },
                    new Node {Name="Кузнечик" }
                }
            },

       
                treeView1.ItemsSource = groups;

            }
        }
    }

}


