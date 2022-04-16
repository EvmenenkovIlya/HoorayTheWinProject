using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HoorayTheWinProjectLogic.Questions;

namespace HoorayTheWinProjectLogic
{
    public class QuestionsMock
    {
        /*string question1 = "Who is the author of War and Peace?";
        string answer1 = "Lermontov";
        string answer2 = "Leskov";
        string answer3 = "Dostoevskiy";
        string answer4 = "Tolstoy";*/
        ChooseOne firstQuestion = new ChooseOne("Who is the author of War and Peace?", "Lermontov", "Dostoevskiy", "Tolstoy", "Leskov");
        ChooseNumber secondQuestion = new ChooseNumber("What kind of animal have a four legs?", "Fish", "Kangaroo", "Tiger", "Panda");
        InSeries thirdQuestion = new InSeries("Sort in right order the authors by their year of birth", "Mayakovskiy", "Pushkin", "Tyutchev", "Pelevin");
        YesNo forthQuestion = new YesNo("Is an year contains 365 days?");
        EnteringAResponse fifthQuestion = new EnteringAResponse("What is the color of banan?", "Yellow");




    }
}
