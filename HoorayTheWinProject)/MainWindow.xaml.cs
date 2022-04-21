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
            ListBoxGroups.ItemsSource = groups;
            ListBoxListOfTests.ItemsSource = tests;
            ListBoxCheckBoxOfGroupForTest.ItemsSource = tests;
            ButtonCreateNewGroup.IsEnabled = false;
            ButtonChangeGroupName.IsEnabled = false;
            TextBoxChangeGroupName.IsEnabled = false;
            LB.ItemsSource = _labels;


            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += OnTick;
            _timer.Start();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonAddTest_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBoxTextOfQuestion_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
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
                TextBoxChangeGroupName.IsEnabled = false;
            }
            else
            {
                TextBoxChangeGroupName.IsEnabled = true;
            }

            TextBoxChangeGroupName.Clear();

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
            TextBoxNewGroupName.Clear();
            groups.Add(groupNew);
            ListBoxGroups.Items.Refresh();
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
        }

        private void ListBoxListOfTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            Test selectedTest = (Test)ListBoxListOfTests.SelectedItem;
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

        }

        private void ListBoxCheckBoxOfGroupForTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Group groupOfUser = (Group)ListBoxCheckBoxOfGroupForTest.SelectedItem;

        }

        private void ComboBoxTypeOfQuestion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxTypeOfQuestion.SelectedIndex == 0) //chooseNumber
            {
                RadioButtonOne.Visibility = Visibility.Hidden;
                RadioButtonTwo.Visibility = Visibility.Hidden;
                RadioButtonThree.Visibility = Visibility.Hidden;
                RadioButtonFour.Visibility = Visibility.Hidden;

                TextBoxOne.Visibility = Visibility.Hidden;
                TextBoxTwo.Visibility = Visibility.Hidden;
                TextBoxThree.Visibility = Visibility.Hidden;
                TextBoxFour.Visibility = Visibility.Hidden;

            }

            if (ComboBoxTypeOfQuestion.SelectedIndex == 1) //ChooseOne
            {
                TextBoxOne.Visibility = Visibility.Hidden;
                TextBoxTwo.Visibility = Visibility.Hidden;
                TextBoxThree.Visibility = Visibility.Hidden;
                TextBoxFour.Visibility = Visibility.Hidden;

                CheckBoxOne.Visibility = Visibility.Hidden;
                CheckBoxTwo.Visibility = Visibility.Hidden;
                CheckBoxThree.Visibility = Visibility.Hidden;
                CheckBoxFour.Visibility = Visibility.Hidden;
            }

            if (ComboBoxTypeOfQuestion.SelectedIndex == 3) //InSeries
            {
                CheckBoxOne.Visibility = Visibility.Hidden;
                CheckBoxTwo.Visibility = Visibility.Hidden;
                CheckBoxThree.Visibility = Visibility.Hidden;
                CheckBoxFour.Visibility = Visibility.Hidden;

                RadioButtonOne.Visibility = Visibility.Hidden;
                RadioButtonTwo.Visibility = Visibility.Hidden;
                RadioButtonThree.Visibility = Visibility.Hidden;
                RadioButtonFour.Visibility = Visibility.Hidden;

            }

        }

        private void ButtonStop_Click(object sender, RoutedEventArgs e)
        {
           
        }

    }
}
