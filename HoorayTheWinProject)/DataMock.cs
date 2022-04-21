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
        public static List<Group> groups = new List<Group>();
        public static List<Test> tests = new List<Test>();

        public static User user1 = new User("Arkadiy Parovozov");
        public static User user2 = new User("Proskovya Lyagushkina");
        public static User user3 = new User("Magomed Petrov");
        public static User user4 = new User("Jessica Pupkina");
        public static User user5 = new User("Leopold Podlotrusov");        
        public static User user6 = new User("Darya Shadrina");
        public static User user7 = new User("Valeriya Puzikova");
        public static User user8 = new User("Ilya Evmenenkov");
        public static User user9 = new User("Vikentiy Strashko");
        public static User user10 = new User("Kanye West");

        public static Group _other = new Group("Other");
        public static Group group1 = new Group("Hooray, the win!");
        public static Group group2 = new Group("Romashki");
        
        
        public static void CreateMockGroups()
        {
            group1.AddUser(user1);
            group1.AddUser(user2);
            group1.AddUser(user3);
            group1.AddUser(user4);
            group1.AddUser(user5);
            group2.AddUser(user6);
            group2.AddUser(user7);
            group2.AddUser(user8);
            group2.AddUser(user9);
            group2.AddUser(user10);
            groups.Add(_other);            
            groups.Add(group1);
            groups.Add(group2);
        }

        public static ChooseOne firstQuestion = new ChooseOne("Who is the author of War and Peace?", "Lermontov", "Dostoevskiy", "Tolstoy", "Leskov");
        public static ChooseNumber secondQuestion = new ChooseNumber("What kind of animal have a four legs?", "Fish", "Kangaroo", "Tiger", "Panda");
        public static InSeries thirdQuestion = new InSeries("Sort in right order the authors by their year of birth", "Mayakovskiy", "Pushkin", "Tyutchev", "Pelevin");
        public static YesNo forthQuestion = new YesNo("Is an year contains 365 days?", "yes", "no");
        public static EnteringAResponse fifthQuestion = new EnteringAResponse("What is the color of banan?", "Yellow");

        public static Test _bankOfQuestions = new Test("Bank of Questions");
        public static Test testMock = new Test("TestForTest");

        public static void CreateMockTests()
        {
            testMock.AddQuestion(firstQuestion);
            testMock.AddQuestion(secondQuestion);
            testMock.AddQuestion(thirdQuestion);
            testMock.AddQuestion(forthQuestion);
            testMock.AddQuestion(fifthQuestion);
            tests.Add(_bankOfQuestions);
            tests.Add(testMock);
        }
    }
}
