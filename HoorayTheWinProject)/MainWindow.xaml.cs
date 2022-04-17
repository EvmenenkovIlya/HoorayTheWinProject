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


namespace HoorayTheWinProject_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Group group1 = UserMock.GetFirstGroup();
        Group group2 = UserMock.GetSecondGroup();
        Group group3 = new Group("Other");
        List<Group> groups = new List<Group>();
        
        public MainWindow()
        {
            InitializeComponent();
            groups.Add(group3);
            groups.Add(group1);
            groups.Add(group2);
            foreach (Group group in groups)
            {
                ListBoxItem groupName = new ListBoxItem() { Content = group.NameGroup};
                ListBoxGroups.Items.Add(groupName);
            }                      
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
            string s = ListBoxGroups.SelectedItem.ToString()[37..];
            Group groupOfUser = groups.Find(x => x.NameGroup.Contains(s));                       
            foreach (User user in groupOfUser.Users)
            {
                ListBoxItem nameUser = new ListBoxItem() { Content = user.NameUser };
                ListBoxListOfUsers.Items.Add(nameUser);
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
            ListBoxItem nameGroup = new ListBoxItem() { Content = groupNew.NameGroup };            
            ListBoxGroups.Items.Add(nameGroup);           
            ButtonCreateNewGroup.IsEnabled = false;
        }
    }
}
