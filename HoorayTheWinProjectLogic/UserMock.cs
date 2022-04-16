using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace HoorayTheWinProjectLogic
{
    public class UserMock
    {
        User user1 = new User("Arkadiy Parovozov");
        User user2 = new User("Proskovya Lyagushkina");
        User user3 = new User("Magomed Petrov");
        User user4 = new User("Jessica Pupkina");
        User user5 = new User("Leopold Podlotrusov");
        User user6 = new User("Darya Shadrina");
        User user7 = new User("Valeriya Puzikova");
        User user8 = new User("Ilya Evmenenkov");
        User user9 = new User("Vikentiy Strashko");
        User user10 = new User("Kanye West");

        Group group1 = new Group("Hooray, the win!");
        Group group2 = new Group("Romashki");

        public Group GetFirstGroup()
        {
            group1.AddUser(user1);
            group1.AddUser(user2);
            group1.AddUser(user3);
            group1.AddUser(user4);
            group1.AddUser(user5);
            return group1;
        }

        public Group GetSeconsGroup()
        {
            group1.AddUser(user6);
            group1.AddUser(user7);
            group1.AddUser(user8);
            group1.AddUser(user9);
            group1.AddUser(user10);
            return group2;
        }



    }
}
