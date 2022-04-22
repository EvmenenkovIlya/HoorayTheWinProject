using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using HoorayTheWinProjectLogic;
using System.Collections.ObjectModel;
using HoorayTheWinProjectLogic.Questions;
using System.Windows.Threading;

namespace HoorayTheWinProject_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    

    public partial class MainWindow : Window
    {
        private TelegramManager _telegramManager;
        private const string _token = "5309481862:AAHaEMz6L2bozc4jO2DuAAxj1yHDipoSV5s";
        private List<string> _labels;
        private DispatcherTimer _timer;

        Group group1 = UserMock.GetFirstGroup();
        Group group2 = UserMock.GetSecondGroup();
        Group _other = new Group("Other");
        Test _bankOfQuestions = new Test("Bank Of Questions");
        Test test1 = QuestionsMock.ReturnTest();
        List<Group> groups = new List<Group>();
        List<Test> tests = new List<Test>();
        

        public MainWindow()
        {
            groups.Add(_other);
            tests.Add(_bankOfQuestions);
            tests.Add(test1);
            groups.Add(group1);
            groups.Add(group2);
            _telegramManager = new TelegramManager(_token, OnMessage);
            _labels = new List<string>();
            InitializeComponent();

            ComboBoxChooseTestForStart.ItemsSource = tests;
            ComboBoxListOfTests.ItemsSource = tests;
            ComboBoxChooseGroup.ItemsSource = groups;
            ListBoxGroups.ItemsSource = groups;            
            ListBoxListOfTests.ItemsSource = tests;
            ListBoxCheckBoxOfGroupForTest.ItemsSource = groups;
            TextBoxChageUserName.IsEnabled = false;
            TextBoxChangeGroupName.IsEnabled = false;
            TextBoxChangeNameOfTest.IsEnabled = false;
            TextBoxTextOfQuestion.IsEnabled = false;
            ButtonChangeUserName.IsEnabled = false;            
            ButtonDeleteGroup.IsEnabled = false;
            ButtonAddTest.IsEnabled = false;
            ButtonDeleteTest.IsEnabled = false;
            ButtonChangeNameOfTest.IsEnabled = false;            
            ButtonDeleteQuestionFromTest.IsEnabled = false;
            ButtonAddQuestionToTest.IsEnabled = false;
            ButtonCreateNewGroup.IsEnabled = false;
            ButtonChangeGroupName.IsEnabled = false;           
            ButtonSaveTheChanges.IsEnabled= false;
            ButtonContentOfQuestion.IsEnabled = false;
            ButtonAddToGroup.IsEnabled = false;
            ButtonDeleteFromGroup.IsEnabled = false;
            ButtonCreateAQuestion.IsEnabled = false;
            ComboBoxChooseGroup.IsEnabled = false;
            ComboBoxListOfTests.IsEnabled = false;
            ComboBoxTypeOfQuestion.IsEnabled = false;            
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();
        }       

        private void TextBoxTextOfQuestion_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBoxGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group groupOfUser = (Group)ListBoxGroups.SelectedItem;
            if (groupOfUser == null || groupOfUser.Users.Count == 0)
            {
                ListBoxListOfUsers.ItemsSource = null;
            }
            else
            {
                ListBoxListOfUsers.ItemsSource = groupOfUser.Users;
            }
            if (ListBoxGroups.SelectedItem == _other)
            {
                ButtonDeleteGroup.IsEnabled = false;                
                TextBoxChangeGroupName.IsEnabled = false;
            }
            else
            {
                ButtonDeleteGroup.IsEnabled = true;                
                TextBoxChangeGroupName.IsEnabled = true;
            }                        
            TextBoxChangeGroupName.Clear();
            TextBoxChageUserName.IsEnabled = false;
            ButtonDeleteFromGroup.IsEnabled = false;
            ButtonAddToGroup.IsEnabled = false;
            ComboBoxChooseGroup.IsEnabled = false;
        }

        private void TextBoxChangeGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = groups.FindIndex(x => x.NameGroup == TextBoxChangeGroupName.Text);
            if (TextBoxChangeGroupName.Text == "" || index >= 0)
            {
                ButtonChangeGroupName.IsEnabled = false;
            }
            else
            {
                ButtonChangeGroupName.IsEnabled = true;
            }           
        }

        private void ButtonChangeGroupName_Click(object sender, RoutedEventArgs e)
        {
            Group group = (Group)ListBoxGroups.SelectedItem;
            group.NameGroup = TextBoxChangeGroupName.Text;
            ListBoxGroups.Items.Refresh();
            ComboBoxChooseGroup.Items.Refresh();
            ListBoxCheckBoxOfGroupForTest.Items.Refresh();
            TextBoxChangeGroupName.Clear();
            ButtonChangeGroupName.IsEnabled = false;
        }


        private void ButtonCreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            Group groupNew = new Group(TextBoxNewGroupName.Text);
            groups.Add(groupNew);
            ListBoxGroups.Items.Refresh();
            ComboBoxChooseGroup.Items.Refresh();            
            ListBoxCheckBoxOfGroupForTest.Items.Refresh();
            TextBoxNewGroupName.Clear();           
            ButtonCreateNewGroup.IsEnabled = false;
        }

        private void ButtonDeleteGroup_Click(object sender, RoutedEventArgs e)
        {
            Group groupOfUser = (Group)ListBoxGroups.SelectedItem;
            foreach (User user in groupOfUser.Users)
            {
                _other.AddUser(user);
            }
             groups.Remove(groupOfUser);
            ListBoxGroups.Items.Refresh();
            ComboBoxChooseGroup.Items.Refresh();
            ListBoxCheckBoxOfGroupForTest.Items.Refresh();
            ListBoxListOfUsers.ItemsSource = null;
            ButtonDeleteGroup.IsEnabled = false;
            TextBoxChangeGroupName.IsEnabled = false;
        }

        private void ListBoxListOfTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Test selectedTest = (Test)ListBoxListOfTests.SelectedItem;            
            if (ListBoxListOfTests.SelectedItem == _bankOfQuestions)
            {
                ButtonDeleteTest.IsEnabled = false;
                TextBoxChangeNameOfTest.IsEnabled = false;
            }
            else
            {
                ButtonDeleteTest.IsEnabled = true;
                TextBoxChangeNameOfTest.IsEnabled = true;
            }
            if (selectedTest == null || selectedTest.AbstractQuestions.Count == 0)
            {
                ListBoxListOfQuestions.ItemsSource = null;
            }
            else
            { 
                ListBoxListOfQuestions.ItemsSource = selectedTest.AbstractQuestions;
            }            
            ButtonDeleteQuestionFromTest.IsEnabled = false;
            ComboBoxListOfTests.IsEnabled = false;
            ButtonContentOfQuestion.IsEnabled = false;
            ListBoxListOfQuestions.SelectedItem = null;           
        }

        private void OnTick(object sender, EventArgs e)
        {
            LB.Items.Refresh();
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {
            _telegramManager.Send(TBQuestion.Text);
        }

        public void OnMessage(string s)
        {
            _labels.Add(s);
        }
        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {
            _telegramManager.Start();
        }

        private void TextBoxTextOfQuestion_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBoxListOfQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {            
            ComboBoxListOfTests.IsEnabled = true;            
            ComboBoxListOfTests.SelectedIndex = -1;
            ButtonContentOfQuestion.IsEnabled = true;
            ButtonDeleteQuestionFromTest.IsEnabled = true;
        }

        private void ListBoxCheckBoxOfGroupForTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group groupOfUser = (Group)ListBoxCheckBoxOfGroupForTest.SelectedItem;

        }
        private void HideAllVariants()
        {
            RadioButtonOne.Visibility = Visibility.Hidden;
            RadioButtonTwo.Visibility = Visibility.Hidden;
            RadioButtonThree.Visibility = Visibility.Hidden;
            RadioButtonFour.Visibility = Visibility.Hidden;

            CheckBoxOne.Visibility = Visibility.Hidden;
            CheckBoxTwo.Visibility = Visibility.Hidden;
            CheckBoxThree.Visibility = Visibility.Hidden;
            CheckBoxFour.Visibility = Visibility.Hidden;

            TextBoxOne.Visibility = Visibility.Hidden;
            TextBoxTwo.Visibility = Visibility.Hidden;
            TextBoxThree.Visibility = Visibility.Hidden;
            TextBoxFour.Visibility = Visibility.Hidden;
        }
        private void ComboBoxTypeOfQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            HideAllVariants();

            if (ComboBoxTypeOfQuestion.SelectedIndex == 0) //chooseNumber
            {
                CheckBoxOne.Visibility = Visibility.Visible;
                CheckBoxTwo.Visibility = Visibility.Visible;
                CheckBoxThree.Visibility = Visibility.Visible;
                CheckBoxFour.Visibility = Visibility.Visible;

                TextBoxOne.Visibility = Visibility.Visible;
                TextBoxTwo.Visibility = Visibility.Visible;
                TextBoxThree.Visibility = Visibility.Visible;
                TextBoxFour.Visibility = Visibility.Visible;
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 1) //ChooseOne
            {
                RadioButtonOne.Visibility = Visibility.Visible;
                RadioButtonTwo.Visibility = Visibility.Visible;
                RadioButtonThree.Visibility = Visibility.Visible;
                RadioButtonFour.Visibility = Visibility.Visible;

                TextBoxOne.Visibility = Visibility.Visible;
                TextBoxTwo.Visibility = Visibility.Visible;
                TextBoxThree.Visibility = Visibility.Visible;
                TextBoxFour.Visibility = Visibility.Visible;
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 2) //WriteReponse
            {
                TextBoxOne.Visibility = Visibility.Visible;
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 3) //InSeries
            {
                TextBoxOne.Visibility = Visibility.Visible;
                TextBoxTwo.Visibility = Visibility.Visible;
                TextBoxThree.Visibility = Visibility.Visible;
                TextBoxFour.Visibility = Visibility.Visible;
                return;

            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 4)//YesNo
            {
                RadioButtonOne.Visibility = Visibility.Visible;
                RadioButtonTwo.Visibility = Visibility.Visible;

                TextBoxOne.Visibility = Visibility.Visible;
                TextBoxTwo.Visibility = Visibility.Visible;
                return;
            }
        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
           

        }

        private void ButtonChangeUserName_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)ListBoxListOfUsers.SelectedItem;
            user.NameUser = TextBoxChageUserName.Text;
            ListBoxListOfUsers.Items.Refresh();
            TextBoxChageUserName.Clear();            
            ButtonChangeUserName.IsEnabled = false;           
            ListBoxListOfUsers.SelectedIndex = -1;
            TextBoxChageUserName.IsEnabled = false;
            ButtonDeleteFromGroup.IsEnabled = false;
            ComboBoxChooseGroup.IsEnabled = false;
        }

        private void TextBoxChageUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TextBoxChageUserName.Cursor == null)
            {
                ButtonDeleteFromGroup.IsEnabled = false;
                ComboBoxChooseGroup.IsEnabled = false;
            }
            User user = (User)ListBoxListOfUsers.SelectedItem;
            string tmp = TextBoxChageUserName.Text;
            if (tmp == "" || tmp == user.NameUser)
            {
                ButtonChangeUserName.IsEnabled = false;
            }
            else
            {
                ButtonChangeUserName.IsEnabled = true;
            }
        }

        private void ListBoxListOfUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TextBoxChageUserName.Clear();
            ComboBoxChooseGroup.SelectedIndex = -1;
            ButtonAddToGroup.IsEnabled = false;
            TextBoxChageUserName.IsEnabled = true;
            ButtonDeleteFromGroup.IsEnabled = true;
            ComboBoxChooseGroup.IsEnabled = true;
            if (ListBoxGroups.SelectedItem == _other)
            {
                ButtonDeleteFromGroup.IsEnabled = false;
            }
            else
            {
                ButtonDeleteFromGroup.IsEnabled = true;
            }
        }

        private void ButtonDeleteFromGroup_Click(object sender, RoutedEventArgs e)
        {
            Group groupOfUser = (Group)ListBoxGroups.SelectedItem;
            User user = (User)ListBoxListOfUsers.SelectedItem;
            _other.AddUser(user);
            groupOfUser.RemoveUser(user);            
            ListBoxListOfUsers.Items.Refresh();
            TextBoxChageUserName.IsEnabled = false;
            ComboBoxChooseGroup.IsEnabled = false;
            ButtonDeleteFromGroup.IsEnabled = false;
        }

        private void ComboBoxChooseGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxGroups.Items.Refresh();
            TextBoxChageUserName.IsEnabled = false;            
            ButtonDeleteFromGroup.IsEnabled = false;
            if(ComboBoxChooseGroup.SelectedIndex != -1)
            {
                ButtonAddToGroup.IsEnabled = true;
            }
        }

        private void ButtonAddToGroup_Click(object sender, RoutedEventArgs e)
        {
            Group groupOfUser = (Group)ComboBoxChooseGroup.SelectedItem;
            Group group = (Group)ListBoxGroups.SelectedItem;
            User user = (User)ListBoxListOfUsers.SelectedItem;
            groupOfUser.AddUser(user);
            group.RemoveUser(user);
            ListBoxListOfUsers.Items.Refresh();
            ComboBoxChooseGroup.SelectedIndex = -1;
            TextBoxChageUserName.IsEnabled = false;
            ButtonDeleteFromGroup.IsEnabled = false;
            ComboBoxChooseGroup.IsEnabled = false;
            ButtonAddToGroup.IsEnabled = false;
        }

        private void TextBoxNewGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = groups.FindIndex(x=>x.NameGroup==TextBoxNewGroupName.Text);
            if(TextBoxNewGroupName.Text=="" || index >= 0)
            {
                ButtonCreateNewGroup.IsEnabled = false;
            }
            else
            {
                ButtonCreateNewGroup.IsEnabled = true;
            }
        }

        private void ButtonDeleteTest_Click(object sender, RoutedEventArgs e)
        {
            Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;           
            tests.Remove(chosenTest);
            ListBoxListOfTests.Items.Refresh();
            ComboBoxListOfTests.Items.Refresh();
            ComboBoxChooseTestForStart.Items.Refresh();       
            ListBoxListOfQuestions.ItemsSource = null;
            ButtonDeleteTest.IsEnabled = false;
        }

        private void ButtonDeleteQuestionFromTest_Click(object sender, RoutedEventArgs e)
        {           
            Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;
            AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
            chosenTest.DeleteQuestion(question);            
            ListBoxListOfQuestions.Items.Refresh();
            ComboBoxListOfTests.IsEnabled = false;
            ButtonContentOfQuestion.IsEnabled = false;
            ButtonDeleteQuestionFromTest.IsEnabled = false;
        }
       
        private void ButtonAddQuestionToTest_Click(object sender, RoutedEventArgs e)
        {
            Test chosenTest = (Test)ComboBoxListOfTests.SelectedItem;
            AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
            int index = chosenTest.AbstractQuestions.FindIndex(x => x.TextOfQuestion == question.TextOfQuestion);
            if (index >= 0)
            {
                MessageBox.Show("There is such a question in this test");
            }
            else
            {
                chosenTest.AddQuestion(question);
                ButtonAddQuestionToTest.IsEnabled = false;
                ComboBoxListOfTests.SelectedIndex = -1;
                ListBoxListOfQuestions.SelectedItem = null;
                ButtonContentOfQuestion.IsEnabled = false;
                ButtonDeleteQuestionFromTest.IsEnabled = false;
                ComboBoxListOfTests.IsEnabled = false;
            }            
        }

        private void ButtonStartNewTest_Click(object sender, RoutedEventArgs e)
        {
            var listForTest = groups.Where(x => x.IsSelected == true);
            ListBoxGroups.ItemsSource = listForTest;
        }

        private void ButtonCreateAQuestion_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxTypeOfQuestion.IsEnabled = false;           
            TextBoxTextOfQuestion.IsEnabled = false;            
            ButtonCreateAQuestion.IsEnabled = false;            
            if (ComboBoxTypeOfQuestion.SelectedIndex == 0) //chooseNumber
            {
                ChooseNumber chooseNumber = new ChooseNumber (TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text) ;
                //chooseNumber.Answer = new List<string> { TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text };
                _bankOfQuestions.AddQuestion(chooseNumber);
                ListBoxListOfQuestions.Items.Refresh();
                TextBoxTextOfQuestion.Clear();
                ComboBoxTypeOfQuestion.SelectedIndex = -1;
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 1) //chooseOne
            {
                ChooseOne chooseOne = new ChooseOne(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text);
                _bankOfQuestions.AddQuestion(chooseOne);
                ListBoxListOfQuestions.Items.Refresh();
                TextBoxTextOfQuestion.Clear();
                ComboBoxTypeOfQuestion.SelectedIndex = -1;
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 2)//enteringAReponse
            {
                EnteringAResponse enteringAResponse = new EnteringAResponse(TextBoxTextOfQuestion.Text, TextBoxOne.Text);
                _bankOfQuestions.AddQuestion(enteringAResponse);
                ListBoxListOfQuestions.Items.Refresh();
                TextBoxTextOfQuestion.Clear();
                ComboBoxTypeOfQuestion.SelectedIndex = -1;
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 3) //InSeries
            {
                InSeries inSeries = new InSeries(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text);
                _bankOfQuestions.AddQuestion(inSeries);
                ListBoxListOfQuestions.Items.Refresh();
                TextBoxTextOfQuestion.Clear();
                ComboBoxTypeOfQuestion.SelectedIndex = -1;
                return;
            }
            if(ComboBoxTypeOfQuestion.SelectedIndex == 4) //Yes/No
            {
                YesNo yesNo = new YesNo(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text);
                _bankOfQuestions.AddQuestion(yesNo);
                ListBoxListOfQuestions.Items.Refresh();
                TextBoxTextOfQuestion.Clear();
                ComboBoxTypeOfQuestion.SelectedIndex = -1;
                return;
            }            
        }

        private void ButtonContentOfQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (ButtonCreateAQuestion.IsEnabled == true)
            {
                MessageBox.Show("Finish creating the question");
            }
            else
            {
                AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
                TextBoxTextOfQuestion.Text = question.TextOfQuestion;
                ComboBoxListOfTests.IsEnabled = false;
                ButtonDeleteQuestionFromTest.IsEnabled = false;
                ListBoxListOfQuestions.IsEnabled = false;
                ListBoxListOfTests.IsEnabled = false;
                TextBoxChangeNameOfTest.IsEnabled = false;
                ButtonDeleteTest.IsEnabled = false;
                ButtonContentOfQuestion.IsEnabled = false;
                ButtonNewQuestion.IsEnabled = false;
                ButtonCreateAQuestion.IsEnabled = false;
                ButtonSaveTheChanges.IsEnabled = true;
                TextBoxTextOfQuestion.IsEnabled = true;
            }
        }

        private void ButtonSaveTheChanges_Click(object sender, RoutedEventArgs e)
        {
            AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
            question.TextOfQuestion = TextBoxTextOfQuestion.Text;
            ListBoxListOfQuestions.Items.Refresh();            
            TextBoxTextOfQuestion.Clear();
            TextBoxTextOfQuestion.IsEnabled=false;            
            ButtonSaveTheChanges.IsEnabled = false;
            ListBoxListOfQuestions.IsEnabled = true;
            ListBoxListOfTests.IsEnabled = true;
            TextBoxChangeNameOfTest.IsEnabled = true;
            ButtonDeleteTest.IsEnabled = true;
            ButtonDeleteQuestionFromTest.IsEnabled = true;
            ComboBoxListOfTests.IsEnabled = true;            
            ButtonContentOfQuestion.IsEnabled = true;
            ButtonNewQuestion.IsEnabled = true;
        }

        private void TextBoxChangeNameOfTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = tests.FindIndex(x => x.NameTest == TextBoxChangeNameOfTest.Text);
            if (TextBoxChangeNameOfTest.Text == "" || index >=0)
            {
                ButtonChangeNameOfTest.IsEnabled = false;
            }
            else
            {
                ButtonChangeNameOfTest.IsEnabled = true;
            }
        }

        private void ButtonChangeNameOfTest_Click(object sender, RoutedEventArgs e)
        {
            Test test = (Test)ListBoxListOfTests.SelectedItem;
            test.NameTest = TextBoxChangeNameOfTest.Text;
            ListBoxListOfTests.Items.Refresh();
            ComboBoxListOfTests.Items.Refresh();
            ComboBoxChooseTestForStart.Items.Refresh();
            TextBoxChangeNameOfTest.Clear();
            ButtonChangeNameOfTest.IsEnabled = false;
            ListBoxListOfTests.SelectedItem = -1;
        }

        private void TextBoxAddTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = tests.FindIndex(x => x.NameTest == TextBoxAddTest.Text);
            if (TextBoxAddTest.Text == "" || index >= 0)
            {
                ButtonAddTest.IsEnabled = false;
            }
            else
            {
                ButtonAddTest.IsEnabled = true;
            }
        }

        private void ButtonAddTest_Click(object sender, RoutedEventArgs e)
        {
            Test chosenTest = new Test(TextBoxAddTest.Text);            
            tests.Add(chosenTest);
            ListBoxListOfTests.Items.Refresh();
            ComboBoxListOfTests.Items.Refresh();
            ComboBoxChooseTestForStart.Items.Refresh();
            TextBoxAddTest.Clear();
            ButtonAddTest.IsEnabled = false;
        }

        private void ComboBoxListOfTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(ComboBoxListOfTests.SelectedIndex != -1)
            {
                ButtonAddQuestionToTest.IsEnabled = true;
            }
            else
            {
                ButtonAddQuestionToTest.IsEnabled = false;
            }
            ButtonContentOfQuestion.IsEnabled = false;
            ButtonDeleteQuestionFromTest.IsEnabled = false;
        }

        private void ButtonNewQuestion_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxTypeOfQuestion.IsEnabled = true;
            TextBoxTextOfQuestion.IsEnabled = true;
            ButtonCreateAQuestion.IsEnabled = true;
        }
    }
}
