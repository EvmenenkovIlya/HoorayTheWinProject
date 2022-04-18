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
        AbstractQuestion question = QuestionsMock.ReturnQuestion(1);
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

            ListBoxListOfQuestions.Items.Add(question.TextOfQuestion);

            ListBoxGroups.ItemsSource = groups;
            ListBoxListOfTests.ItemsSource = tests;
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
            ListBoxListOfUsers.Items.Clear();
            Group groupOfUser = (Group) ListBoxGroups.SelectedItem;
            if (groupOfUser == null || groupOfUser.Users.Count == 0)
            {
                return;
            }
            else
            {
                //ListBoxListOfUsers.ItemsSource = groupOfUser.Users; Не работает как нужно                
                foreach (User user in groupOfUser.Users)
                {
                    ListBoxItem nameUser = new ListBoxItem() { Content = user.NameUser };
                    ListBoxListOfUsers.Items.Add(nameUser);
                }      
            }
        }

        private void TextBoxNewGroupName_TextChanged(object sender, TextChangedEventArgs e)
        {
            string tmp = TextBoxNewGroupName.Text;
            if (tmp == "" || group1.NameGroup.Contains(tmp))
            {
                return;
            }
            else 
            {
                ButtonCreateNewGroup.IsEnabled = true;                 
            }
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
            ListBoxListOfUsers.Items.Clear();
            Group groupOfUser = (Group)ListBoxGroups.SelectedItem;
            foreach (User user in groupOfUser.Users)
            {
                _other.AddUser(user);
            }               
            groups.Remove(groupOfUser);
            ListBoxGroups.Items.Refresh();           
        }

        private void ListBoxListOfTests_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBoxListOfQuestions.Items.Clear();
            Test selectedTest = (Test)ListBoxListOfTests.SelectedItem;
            if (selectedTest == null || selectedTest.AbstractQuestions.Count == 0)
            {
                return;
            }
            else
            {
                foreach (AbstractQuestion question in selectedTest.AbstractQuestions)
                {
                    ListBoxItem que = new ListBoxItem() { Content = question.TextOfQuestion };
                    ListBoxListOfQuestions.Items.Add(que);
                }
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
    }
}
