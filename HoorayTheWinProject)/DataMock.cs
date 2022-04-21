using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic;
using HoorayTheWinProjectLogic.Questions;



namespace HoorayTheWinProject_
{
    public static class DataMock
    {
        public static User user1 = new User("Arkadiy Parovozov");
        public static User user2 = new User("Proskovya Lyagushkina");
        public static User user3 = new User("Magomed Petrov");
        public static User user4 = new User("Jessica Pupkina");
        public static User user5 = new User("Leopold Podlotrusov");
        public static Group group1 = new Group("Hooray, the win!");


        public static List<Group> groups = new List<Group>();

        

    }
}
