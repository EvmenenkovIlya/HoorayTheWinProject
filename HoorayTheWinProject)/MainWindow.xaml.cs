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


namespace HoorayTheWinProject_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Group group1 = UserMock.GetFirstGroup();
        Group group2 = UserMock.GetSecondGroup();
        Group _other = new Group("Other");
        Test _bankOfQuestions = new Test("Bank Of Questions");
        Test test1 = QuestionsMock.ReturnTest();
        List<Group> groups = new List<Group>();
        List<Test> tests = new List<Test>();

        public MainWindow()
        {
            InitializeComponent();
            groups.Add(_other);
            tests.Add(_bankOfQuestions);
            tests.Add(test1);
            groups.Add(group1);
            groups.Add(group2);

            ListBoxGroups.ItemsSource = groups;
            ListBoxListOfTests.ItemsSource = tests;
            TextBoxChageUserName.IsEnabled = false;
            ButtonChangeUserName.IsEnabled = false;
            ButtonDeleteGroup.IsEnabled = false;
            ButtonCreateNewGroup.IsEnabled = false;            
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
            Group groupOfUser = (Group) ListBoxGroups.SelectedItem;
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
            }
            else
            {
                ButtonDeleteGroup.IsEnabled = true;
            }
        }

        private void TextBoxNewGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Group groupOfUser = (Group)ListBoxGroups.SelectedItem;
            string tmp = TextBoxNewGroupName.Text;
            if (tmp == "" || tmp == groupOfUser.NameGroup)
            {
                ButtonCreateNewGroup.IsEnabled = false;
            }
            else
            {
                ButtonCreateNewGroup.IsEnabled = true;    
            }
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
            if (selectedTest == null || selectedTest.AbstractQuestions.Count == 0)
            {
                ListBoxListOfQuestions.ItemsSource = null;
            }
            else
            {
                ListBoxListOfQuestions.ItemsSource = selectedTest.AbstractQuestions;
            }
        }

        private void TextBoxTextOfQuestion_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void ListBoxListOfQuestions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ListBoxCheckBoxOfGroupForTest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ButtonChangeUserName_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)ListBoxListOfUsers.SelectedItem;
            user.NameUser = TextBoxChageUserName.Text;
            ListBoxListOfUsers.Items.Refresh();
            TextBoxChageUserName.Clear();
            TextBoxChageUserName.IsEnabled = false;
            ButtonChangeUserName.IsEnabled = false;
        }

        private void TextBoxChageUserName_TextChanged(object sender, TextChangedEventArgs e)
        {
            
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
            if (ListBoxListOfUsers.SelectedItem != null)
            {
                TextBoxChageUserName.IsEnabled = true;
            }
        }

        private void ButtonDeleteFromGroup_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
