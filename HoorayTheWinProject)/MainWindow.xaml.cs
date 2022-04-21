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
        
        List<Test> tests = new List<Test>();

        public MainWindow()
        {
            DataMock.groups.Add(_other);
            tests.Add(_bankOfQuestions);
            tests.Add(test1);
            DataMock.groups.Add(group1);
            DataMock.groups.Add(group2);
            _telegramManager = new TelegramManager(_token, OnMessage);
            _labels = new List<string>();
            InitializeComponent();

            ComboBoxChooseTestForStart.ItemsSource = tests;
            ComboBoxListOfTests.ItemsSource = tests;
            ListBoxGroups.ItemsSource = DataMock.groups;
            ComboBoxChooseGroup.ItemsSource = DataMock.groups;
            ListBoxListOfTests.ItemsSource = tests;
            ListBoxCheckBoxOfGroupForTest.ItemsSource = DataMock.groups;
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
            string tmp = TextBoxChangeGroupName.Text;
            int index = DataMock.groups.FindIndex(x => x.NameGroup == tmp);
            if (tmp == "" || index >= 0)
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
            DataMock.groups.Add(groupNew);
            ListBoxGroups.Items.Refresh();
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
             DataMock.groups.Remove(groupOfUser);
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

        private void ButtonAddQuestionToTest_Click(object sender, RoutedEventArgs e)
        {            
            Test chosenTest = (Test)ComboBoxListOfTests.SelectedItem;
            chosenTest.AddQuestion((AbstractQuestion)ListBoxListOfQuestions.SelectedItem);
        }

        private void ButtonStartNewTest_Click(object sender, RoutedEventArgs e)
        {
            var listForTest = DataMock.groups.Where(x => x.IsSelected == true);
            ListBoxGroups.ItemsSource = listForTest;
        }

        private void ComboBoxListOfTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
