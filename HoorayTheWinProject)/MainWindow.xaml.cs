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
            ListBoxGroups.ItemsSource = groups;
            ComboBoxChooseGroup.ItemsSource = groups;
            ListBoxListOfTests.ItemsSource = tests;
            ListBoxCheckBoxOfGroupForTest.ItemsSource = groups;
            TextBoxChageUserName.IsEnabled = false;
            ButtonChangeUserName.IsEnabled = false;
            ButtonCreateNewGroup.IsEnabled = false;
            ButtonDeleteGroup.IsEnabled = false;
            ButtonDeleteTest.IsEnabled = false;
            ButtonDeleteQuestionFromTest.IsEnabled = false;
            ComboBoxListOfTests.IsEnabled = false;
            ButtonCreateNewGroup.IsEnabled = false;
            ButtonChangeGroupName.IsEnabled = false;
            TextBoxChangeGroupName.IsEnabled = false;
            ButtonSaveTheChanges.IsEnabled= false;
            ButtonContentOfQuestion.IsEnabled = false;
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
            Group group = (Group)ListBoxGroups.SelectedItem;
            string tmp = TextBoxChangeGroupName.Text;

            int counter = 0;
            for (int i = 0; i < groups.Count; i++)
            {
                if (tmp == groups[i].NameGroup)
                {
                    counter++;
                    break;
                }
            }

            if(tmp == "" || counter > 0)
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
            TextBoxChangeGroupName.Clear();
            ButtonChangeGroupName.IsEnabled = false;
            ListBoxGroups.SelectedItem = -1;
            
        }


        private void ButtonCreateNewGroup_Click(object sender, RoutedEventArgs e)
        {
            Group groupNew = new Group(TextBoxNewGroupName.Text);
            groups.Add(groupNew);
            ListBoxGroups.Items.Refresh();
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
             ListBoxListOfUsers.ItemsSource = null;
             ButtonDeleteGroup.IsEnabled = false;
        }

        private void ListBoxListOfTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Test selectedTest = (Test)ListBoxListOfTests.SelectedItem;            
            if (ListBoxListOfTests.SelectedIndex == 0)
            {
                ButtonDeleteTest.IsEnabled = false;               
            }
            else
            {
                ButtonDeleteTest.IsEnabled = true;
            }
            if (selectedTest == null || selectedTest.AbstractQuestions.Count == 0)
            {
                ListBoxListOfQuestions.ItemsSource = null;
            }
            else
            { 
                ListBoxListOfQuestions.ItemsSource = selectedTest.AbstractQuestions;
            }
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
            ButtonDeleteQuestionFromTest.IsEnabled = true;
            ComboBoxListOfTests.IsEnabled = true;
            ButtonContentOfQuestion.IsEnabled = true;
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
            ButtonCreateNewGroup.IsEnabled = true;
        }

        private void ButtonDeleteTest_Click(object sender, RoutedEventArgs e)
        {
            Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;           
            tests.Remove(chosenTest);
            ListBoxListOfTests.Items.Refresh();
            ComboBoxChooseTestForStart.Items.Refresh();           
            ListBoxListOfQuestions.ItemsSource = null;
            ButtonDeleteTest.IsEnabled = false;
        }

        private void ButtonDeleteQuestionFromTest_Click(object sender, RoutedEventArgs e)
        {           
            Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;
            chosenTest.DeleteQuestion(ListBoxListOfQuestions.SelectedIndex);
            ListBoxListOfQuestions.Items.Refresh();
            ButtonDeleteQuestionFromTest.IsEnabled = false;
        }
        private void IsSelected(object sender, RoutedEventArgs e)
        {

        }
        private void ButtonAddQuestionToTest_Click(object sender, RoutedEventArgs e)
        {            
            Test chosenTest = (Test)ComboBoxListOfTests.SelectedItem;
            chosenTest.AddQuestion((AbstractQuestion)ListBoxListOfQuestions.SelectedItem);
        }

        private void ButtonCreateAQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxTypeOfQuestion.SelectedIndex == 0) //chooseNumber
            {
                ChooseNumber chooseNumber = new ChooseNumber (TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text) ;
                //chooseNumber.Answer = new List<string> { TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text };
                _bankOfQuestions.AddQuestion(chooseNumber);
                ListBoxListOfQuestions.Items.Refresh();
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 1) //chooseOne
            {
                ChooseOne chooseOne = new ChooseOne(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text);
                _bankOfQuestions.AddQuestion(chooseOne);
                ListBoxListOfQuestions.Items.Refresh();
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 2)//enteringAReponse
            {
                EnteringAResponse enteringAResponse = new EnteringAResponse(TextBoxTextOfQuestion.Text, TextBoxOne.Text);
                _bankOfQuestions.AddQuestion(enteringAResponse);
                ListBoxListOfQuestions.Items.Refresh();
                return;
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 3) //InSeries
            {
                InSeries inSeries = new InSeries(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text);
                _bankOfQuestions.AddQuestion(inSeries);
                ListBoxListOfQuestions.Items.Refresh();
                return;
            }
            if(ComboBoxTypeOfQuestion.SelectedIndex == 4) //Yes/No
            {
                YesNo yesNo = new YesNo(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text);
                _bankOfQuestions.AddQuestion(yesNo);
                ListBoxListOfQuestions.Items.Refresh();
                return;
            }

        }

        private void ButtonContentOfQuestion_Click(object sender, RoutedEventArgs e)
        {
            ButtonCreateAQuestion.IsEnabled = false;
            
            AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
            
            TextBoxTextOfQuestion.Text = question.TextOfQuestion;
            
            ButtonSaveTheChanges.IsEnabled = true;
        }

        private void ButtonSaveTheChanges_Click(object sender, RoutedEventArgs e)
        {
            AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
            question.TextOfQuestion = TextBoxTextOfQuestion.Text;
            ListBoxListOfQuestions.Items.Refresh();
            ButtonSaveTheChanges.IsEnabled = false;
            TextBoxTextOfQuestion.Clear();
        }
    }
}
