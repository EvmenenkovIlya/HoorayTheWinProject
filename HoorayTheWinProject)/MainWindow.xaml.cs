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
using HoorayTheWinProjectLogic.Data;

namespace HoorayTheWinProject_
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private TelegramManager _telegramManager;
        private DispatcherTimer _timer;
        TestsStorage tests = TestsStorage.GetInstance();
        GroupStorage groups = GroupStorage.GetInstance();
        ReportStorage reports = ReportStorage.GetInstance();

        public MainWindow()
        {
            _telegramManager = new TelegramManager();
            _telegramManager.Start();

            InitializeComponent();

            ListBoxListOfTests.ItemsSource = DataMock.tests;
            ComboBoxListOfTests.ItemsSource = DataMock.tests;
            ComboBoxChooseTestForStart.ItemsSource = DataMock.tests;
            ListBoxGroups.ItemsSource = groups.groups;
            ComboBoxChooseGroup.ItemsSource = groups.groups;
            ListBoxCheckBoxOfGroupForTest.ItemsSource = groups.groups;
            ComboBoxTypeOfQuestion.ItemsSource = DataMock.forComboBox;

            TextBoxChageUserName.IsEnabled = false;
            TextBoxTextOfQuestion.IsEnabled = false;
            TextBoxChangeGroupName.IsEnabled = false;
            TextBoxChangeNameOfTest.IsEnabled = false;
            ButtonAddTest.IsEnabled = false;
            ButtonDeleteTest.IsEnabled = false;
            ButtonAddToGroup.IsEnabled = false;
            ButtonDeleteGroup.IsEnabled = false;
            ButtonStartNewTest.IsEnabled = false;
            ButtonFinishNewTest.IsEnabled = false;
            ButtonResetQuestion.IsEnabled = false;
            ButtonChangeUserName.IsEnabled = false;
            ButtonCreateNewGroup.IsEnabled = false;
            ButtonChangeUserName.IsEnabled = false;            
            ButtonSaveTheChanges.IsEnabled = false;
            ButtonChangeGroupName.IsEnabled = false;
            ButtonDeleteFromGroup.IsEnabled = false;
            ButtonCreateAQuestion.IsEnabled = false;
            ButtonChangeNameOfTest.IsEnabled = false;
            ButtonAddQuestionToTest.IsEnabled = false;
            ButtonContentOfQuestion.IsEnabled = false;
            ButtonDeleteQuestionFromTest.IsEnabled = false;
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
            if (ListBoxGroups.SelectedItem == groups.groups[0])
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
            int index = groups.groups.FindIndex(x => x.NameGroup == TextBoxChangeGroupName.Text);
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
            groups.groups.Add(groupNew);
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
                groups.groups[0].AddUser(user);
            }
            groups.groups.Remove(groupOfUser);
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
            if (ListBoxListOfTests.SelectedItem == DataMock._bankOfQuestions)
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
            if (ListBoxGroups.SelectedItem == groups.groups[0])
            {
                ListBoxListOfUsers.Items.Refresh();
                ListBoxListOfUsers.ItemsSource = groups.groups[0].Users;
            }
        }

        private void ButtonSend_Click(object sender, RoutedEventArgs e)
        {          
            //_telegramManager.Send(DataMock.qs, 296570604);
        }

        private void ButtonStart_Click(object sender, RoutedEventArgs e)
        {          
            //_telegramManager.Start();
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
            if (ButtonCreateAQuestion.IsEnabled ==  true || ButtonSaveTheChanges.IsEnabled == true)
            {
                TextBoxOne.Clear();
                TextBoxTwo.Clear();
                TextBoxThree.Clear();
                TextBoxFour.Clear();
                TextBoxTextOfQuestion.Clear();
                RadioButtonOne.IsChecked = false;
                RadioButtonTwo.IsChecked = false;
                RadioButtonThree.IsChecked = false;
                RadioButtonFour.IsChecked = false;
                CheckBoxOne.IsChecked = false;
                CheckBoxTwo.IsChecked = false;
                CheckBoxThree.IsChecked = false;
                CheckBoxFour.IsChecked = false;
            }
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
            if (TextBoxChageUserName.Text == "" || TextBoxChageUserName.Text == user.NameUser)
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
            if (ListBoxGroups.SelectedItem == groups.groups[0])
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
            groups.groups[0].AddUser(user);
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
            if (ComboBoxChooseGroup.SelectedIndex == -1
            || (ListBoxGroups.SelectedItem == ComboBoxChooseGroup.SelectedItem))
            {
                ButtonAddToGroup.IsEnabled = false;
            }
            else
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
            int index = groups.groups.FindIndex(x=>x.NameGroup==TextBoxNewGroupName.Text);
            if (TextBoxNewGroupName.Text == "" || index >= 0)
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
            DataMock.tests.Remove(chosenTest);
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
            int index = chosenTest.AbstractQuestions.FindIndex(x => x.TextOfQuestion == question.TextOfQuestion
            && x.TypeQuestion == question.TypeQuestion);
            if (index >= 0)
            {
                MessageBox.Show("A question with this type is already in the test");
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
            DataMock.IsTesting = true;
            DataMock._testToStart = new TestManager((Test)ComboBoxChooseTestForStart.SelectedItem);
            foreach (long chatId in DataMock._testToStart.AnswerBase.Keys)
            { 
                _telegramManager.SendNextQuestion(chatId, DataMock._testToStart);
            }
            ButtonFinishNewTest.IsEnabled = true;
            ButtonStartNewTest.IsEnabled = false;
            ComboBoxChooseTestForStart.IsEnabled = false;
            ListBoxCheckBoxOfGroupForTest.IsEnabled = false;
        }

        private void ButtonFinishNewTest_Click(object sender, RoutedEventArgs e)
        {
            DataMock.IsTesting = false;
            ComboBoxChooseTestForStart.IsEnabled = true;
            ListBoxCheckBoxOfGroupForTest.IsEnabled = true;
            ComboBoxChooseTestForStart.SelectedIndex = -1;
            ButtonFinishNewTest.IsEnabled = false;
        }
        private void ComboBoxChooseTestForStart_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboBoxChooseTestForStart.SelectedIndex == -1)
            {
                ButtonStartNewTest.IsEnabled = false;
            }
            else
            {
                ButtonStartNewTest.IsEnabled = true;
            }
        }
        private void Clear()
        {           
            TextBoxTextOfQuestion.Clear();            
            ComboBoxTypeOfQuestion.SelectedIndex = -1;
            ComboBoxTypeOfQuestion.IsEnabled = false;
            TextBoxTextOfQuestion.IsEnabled = false;
            ButtonCreateAQuestion.IsEnabled = false;
            ButtonResetQuestion.IsEnabled = false;
            RadioButtonOne.IsChecked = false;
            RadioButtonTwo.IsChecked = false;
            RadioButtonThree.IsChecked = false;
            RadioButtonFour.IsChecked = false;
            CheckBoxOne.IsChecked = false;
            CheckBoxTwo.IsChecked = false;
            CheckBoxThree.IsChecked = false;
            CheckBoxFour.IsChecked = false;
        }

        private void ButtonCreateAQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxTypeOfQuestion.SelectedIndex == -1)
            {
                MessageBox.Show("Finish creating the question");            
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 0) //chooseNumber
            {
                if (TextBoxTextOfQuestion.Text == "" || TextBoxOne.Text == "" || TextBoxTwo.Text == "" || TextBoxThree.Text == "" ||
                    TextBoxFour.Text == "")
                {
                    MessageBox.Show("Finish creating the question");
                }
                else
                {                   
                    
                    ChooseNumber chooseNumber = new ChooseNumber(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text);
                    int index = DataMock._bankOfQuestions.AbstractQuestions.FindIndex(x => x.TextOfQuestion == chooseNumber.TextOfQuestion
                    && x.TypeQuestion == chooseNumber.TypeQuestion);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type is already in the question bank");
                    }
                    else
                    {
                        DataMock._bankOfQuestions.AddQuestion(chooseNumber);
                        if (ListBoxListOfTests.SelectedIndex == 0)
                        {
                            ListBoxListOfQuestions.Items.Refresh();
                            ListBoxListOfQuestions.ItemsSource = DataMock._bankOfQuestions.AbstractQuestions;
                        }
                        chooseNumber.Answer = new List<string> { TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text };
                        Clear();
                        return;
                    }
                }
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 1) //chooseOne
            {
                if (TextBoxTextOfQuestion.Text == "" || TextBoxOne.Text == "" || TextBoxTwo.Text == "" || TextBoxThree.Text == "" ||
                    TextBoxFour.Text == "")
                {
                    MessageBox.Show("Finish creating the question");
                }
                else
                {
                    ChooseOne chooseOne = new ChooseOne(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text);
                    int index = DataMock._bankOfQuestions.AbstractQuestions.FindIndex(x => x.TextOfQuestion == chooseOne.TextOfQuestion
                    && x.TypeQuestion == chooseOne.TypeQuestion);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type is already in the question bank");
                    }
                    else
                    {
                        DataMock._bankOfQuestions.AddQuestion(chooseOne);
                        if (ListBoxListOfTests.SelectedIndex == 0)
                        {
                            ListBoxListOfQuestions.Items.Refresh();
                            ListBoxListOfQuestions.ItemsSource = DataMock._bankOfQuestions.AbstractQuestions;
                        }
                        chooseOne.Answer = new List<string> { TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text };
                        Clear();
                        return;
                    }
                }
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 2)//enteringAReponse
            {
                if (TextBoxTextOfQuestion.Text == "")
                {
                    MessageBox.Show("Finish creating the question");
                }
                else
                {
                    EnteringAResponse enteringAResponse = new EnteringAResponse(TextBoxTextOfQuestion.Text);
                    int index = DataMock._bankOfQuestions.AbstractQuestions.FindIndex(x => x.TextOfQuestion == enteringAResponse.TextOfQuestion
                    && x.TypeQuestion == enteringAResponse.TypeQuestion);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type is already in the question bank");
                    }
                    else
                    {
                        DataMock._bankOfQuestions.AddQuestion(enteringAResponse);
                        if (ListBoxListOfTests.SelectedIndex == 0)
                        {
                            ListBoxListOfQuestions.Items.Refresh();
                            ListBoxListOfQuestions.ItemsSource = DataMock._bankOfQuestions.AbstractQuestions;
                        }
                        enteringAResponse.Answer = new List<string> { };
                        Clear();
                        return;
                    }
                }
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 3) //InSeries
            {
                if (TextBoxTextOfQuestion.Text == "" || TextBoxOne.Text == "" || TextBoxTwo.Text == "" || TextBoxThree.Text == ""
                    || TextBoxFour.Text == "")
                {
                    MessageBox.Show("Finish creating the question");
                }
                else
                {
                    InSeries inSeries = new InSeries(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text);
                    int index = DataMock._bankOfQuestions.AbstractQuestions.FindIndex(x => x.TextOfQuestion == inSeries.TextOfQuestion
                    && x.TypeQuestion == inSeries.TypeQuestion);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type is already in the question bank");
                    }
                    else
                    {
                        DataMock._bankOfQuestions.AddQuestion(inSeries);
                        if (ListBoxListOfTests.SelectedIndex == 0)
                        {
                            ListBoxListOfQuestions.Items.Refresh();
                            ListBoxListOfQuestions.ItemsSource = DataMock._bankOfQuestions.AbstractQuestions;
                        }
                        inSeries.Answer = new List<string> { TextBoxOne.Text, TextBoxTwo.Text, TextBoxThree.Text, TextBoxFour.Text };
                        Clear();
                        return;
                    }
                }
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 4) //Yes/No
            {
                if (TextBoxTextOfQuestion.Text == "" || TextBoxOne.Text == "" || TextBoxTwo.Text == "")
                {
                    MessageBox.Show("Finish creating the question");
                }
                else
                {
                    YesNo yesNo = new YesNo(TextBoxTextOfQuestion.Text, TextBoxOne.Text, TextBoxTwo.Text);
                    int index = DataMock._bankOfQuestions.AbstractQuestions.FindIndex(x => x.TextOfQuestion == yesNo.TextOfQuestion
                    && x.TypeQuestion == yesNo.TypeQuestion);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type is already in the question bank");
                    }
                    else
                    {
                        DataMock._bankOfQuestions.AddQuestion(yesNo);
                        if (ListBoxListOfTests.SelectedIndex == 0)
                        {
                            ListBoxListOfQuestions.Items.Refresh();
                            ListBoxListOfQuestions.ItemsSource = DataMock._bankOfQuestions.AbstractQuestions;
                        }
                        yesNo.Answer = new List<string> { TextBoxOne.Text, TextBoxTwo.Text };
                        Clear();                        
                        return;
                    }
                }
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
                ComboBoxTypeOfQuestion.SelectedIndex = question.TypeQuestion;                
                if (question.TypeQuestion == 0)
                {
                    TextBoxOne.Text = question.Answer[0];
                    TextBoxTwo.Text = question.Answer[1];
                    TextBoxThree.Text = question.Answer[2];
                    TextBoxFour.Text = question.Answer[3];
                }
                if (question.TypeQuestion == 1)
                {
                    TextBoxOne.Text = question.Answer[0];
                    TextBoxTwo.Text = question.Answer[1];
                    TextBoxThree.Text = question.Answer[2];
                    TextBoxFour.Text = question.Answer[3];
                }
                if (question.TypeQuestion == 2)
                {
                    
                }
                if (question.TypeQuestion == 3)
                {
                    TextBoxOne.Text = question.Answer[0];
                    TextBoxTwo.Text = question.Answer[1];
                    TextBoxThree.Text = question.Answer[2];
                    TextBoxFour.Text = question.Answer[3];
                }
                if (question.TypeQuestion == 4)
                {
                    TextBoxOne.Text = question.Answer[0];
                    TextBoxTwo.Text = question.Answer[1];                    
                }
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
                ComboBoxTypeOfQuestion.IsEnabled = true;
            }
        }
        private void Open()
        {            
            ListBoxListOfQuestions.Items.Refresh();
            TextBoxTextOfQuestion.Clear();
            TextBoxTextOfQuestion.IsEnabled = false;
            ButtonSaveTheChanges.IsEnabled = false;
            ComboBoxTypeOfQuestion.IsEnabled = false;
            ComboBoxTypeOfQuestion.SelectedIndex = -1;
            ListBoxListOfQuestions.IsEnabled = true;
            ListBoxListOfTests.IsEnabled = true;
            TextBoxChangeNameOfTest.IsEnabled = true;
            ButtonDeleteTest.IsEnabled = true;
            ButtonDeleteQuestionFromTest.IsEnabled = true;
            ComboBoxListOfTests.IsEnabled = true;
            ButtonContentOfQuestion.IsEnabled = true;
            ButtonNewQuestion.IsEnabled = true;
        }
        private void ButtonSaveTheChanges_Click(object sender, RoutedEventArgs e)
        {
            if (ComboBoxTypeOfQuestion.SelectedIndex == 0) //chooseNumber
            {
                if (TextBoxTextOfQuestion.Text == "" || TextBoxOne.Text == "" || TextBoxTwo.Text == "" || TextBoxThree.Text == "" ||
                    TextBoxFour.Text == "")
                {
                    MessageBox.Show("Finish changing the question");
                }
                else
                {
                    Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;
                    AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;                    
                    int index = chosenTest.AbstractQuestions.FindIndex(x => x.TextOfQuestion == TextBoxTextOfQuestion.Text
                    && x.TypeQuestion == ComboBoxTypeOfQuestion.SelectedIndex && x.Answer[0] == TextBoxOne.Text
                    && x.Answer[1] == TextBoxTwo.Text && x.Answer[2] == TextBoxThree.Text && x.Answer[3] == TextBoxFour.Text);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type and such answer options is already in the test");
                    }
                    else
                    {
                        question.TextOfQuestion = TextBoxTextOfQuestion.Text;
                        question.TypeQuestion = ComboBoxTypeOfQuestion.SelectedIndex;
                        question.Answer.Clear();
                        question.Answer.Add(TextBoxOne.Text);
                        question.Answer.Add(TextBoxTwo.Text);
                        question.Answer.Add(TextBoxThree.Text);
                        question.Answer.Add(TextBoxFour.Text);
                        Open();
                        //HideAllVariants();
                        return;
                    }
                }
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 1) //chooseOne
            {
                if (TextBoxTextOfQuestion.Text == "" || TextBoxOne.Text == "" || TextBoxTwo.Text == "" || TextBoxThree.Text == "" ||
                    TextBoxFour.Text == "")
                {
                    MessageBox.Show("Finish changing the question");
                }
                else
                {
                    Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;
                    AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
                    int index = chosenTest.AbstractQuestions.FindIndex(x => x.TextOfQuestion == TextBoxTextOfQuestion.Text
                    && x.TypeQuestion == ComboBoxTypeOfQuestion.SelectedIndex && x.Answer[0] == TextBoxOne.Text
                    && x.Answer[1] == TextBoxTwo.Text && x.Answer[2] == TextBoxThree.Text && x.Answer[3] == TextBoxFour.Text);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type and such answer options is already in the test");
                    }
                    else
                    {
                        question.TextOfQuestion = TextBoxTextOfQuestion.Text;
                        question.TypeQuestion = ComboBoxTypeOfQuestion.SelectedIndex;
                        question.Answer.Clear();
                        question.Answer.Add(TextBoxOne.Text);
                        question.Answer.Add(TextBoxTwo.Text);
                        question.Answer.Add(TextBoxThree.Text);
                        question.Answer.Add(TextBoxFour.Text);
                        Open();
                        //HideAllVariants();
                        return;
                    }
                }
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 2)//enteringAReponse
            {
                if (TextBoxTextOfQuestion.Text == "")
                {
                    MessageBox.Show("Finish changing the question");
                }
                else
                {
                    Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;
                    AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
                    int index = chosenTest.AbstractQuestions.FindIndex(x => x.TextOfQuestion == TextBoxTextOfQuestion.Text
                    && x.TypeQuestion == ComboBoxTypeOfQuestion.SelectedIndex);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type is already in the test");
                    }
                    else
                    {
                        question.TextOfQuestion = TextBoxTextOfQuestion.Text;
                        question.TypeQuestion = ComboBoxTypeOfQuestion.SelectedIndex;
                        question.Answer.Clear();
                        Open();
                        //HideAllVariants();
                        return;
                    }
                }
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 3) //InSeries
            {
                if (TextBoxTextOfQuestion.Text == "" || TextBoxOne.Text == "" || TextBoxTwo.Text == "" || TextBoxThree.Text == ""
                    || TextBoxFour.Text == "")
                {
                    MessageBox.Show("Finish changing the question");
                }
                else
                {
                    Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;
                    AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
                    int index = chosenTest.AbstractQuestions.FindIndex(x => x.TextOfQuestion == TextBoxTextOfQuestion.Text
                    && x.TypeQuestion == ComboBoxTypeOfQuestion.SelectedIndex && x.Answer[0] == TextBoxOne.Text
                    && x.Answer[1] == TextBoxTwo.Text && x.Answer[2] == TextBoxThree.Text && x.Answer[3] == TextBoxFour.Text);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type and such answer options is already in the test");
                    }
                    else
                    {
                        question.TextOfQuestion = TextBoxTextOfQuestion.Text;
                        question.TypeQuestion = ComboBoxTypeOfQuestion.SelectedIndex;
                        question.Answer.Clear();
                        question.Answer.Add(TextBoxOne.Text);
                        question.Answer.Add(TextBoxTwo.Text);
                        question.Answer.Add(TextBoxThree.Text);
                        question.Answer.Add(TextBoxFour.Text);
                        Open();
                        //HideAllVariants();
                        return;
                    }
                }
            }
            if (ComboBoxTypeOfQuestion.SelectedIndex == 4) //Yes/No
            {
                if (TextBoxTextOfQuestion.Text == "" || TextBoxOne.Text == "" || TextBoxTwo.Text == "")
                {
                    MessageBox.Show("Finish changing the question");
                }
                else
                {
                    Test chosenTest = (Test)ListBoxListOfTests.SelectedItem;
                    AbstractQuestion question = (AbstractQuestion)ListBoxListOfQuestions.SelectedItem;
                    int index = chosenTest.AbstractQuestions.FindIndex(x => x.TextOfQuestion == TextBoxTextOfQuestion.Text
                    && x.TypeQuestion == ComboBoxTypeOfQuestion.SelectedIndex && x.Answer[0] == TextBoxOne.Text
                    && x.Answer[1] == TextBoxTwo.Text);
                    if (index >= 0)
                    {
                        MessageBox.Show("A question with this type and such answer options is already in the test");
                    }
                    else
                    {
                        question.TextOfQuestion = TextBoxTextOfQuestion.Text;
                        question.TypeQuestion = ComboBoxTypeOfQuestion.SelectedIndex;
                        question.Answer.Clear();
                        question.Answer.Add(TextBoxOne.Text);
                        question.Answer.Add(TextBoxTwo.Text);                        
                        Open();
                        //HideAllVariants();
                        return;
                    }
                }
            }
        }

        private void TextBoxChangeNameOfTest_TextChanged(object sender, TextChangedEventArgs e)
        {
            int index = DataMock.tests.FindIndex(x => x.NameTest == TextBoxChangeNameOfTest.Text);
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
            int index = DataMock.tests.FindIndex(x => x.NameTest == TextBoxAddTest.Text);
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
            DataMock.tests.Add(chosenTest);
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
            ButtonResetQuestion.IsEnabled = true;
            TextBoxOne.Clear();
            TextBoxTwo.Clear();
            TextBoxThree.Clear();
            TextBoxFour.Clear();
        }

        private void ButtonResetQuestion_Click(object sender, RoutedEventArgs e)
        {
            ComboBoxTypeOfQuestion.IsEnabled = false;
            ComboBoxTypeOfQuestion.SelectedIndex = -1;
            TextBoxTextOfQuestion.IsEnabled = false;
            TextBoxTextOfQuestion.Clear();
            TextBoxOne.Clear();
            TextBoxTwo.Clear();
            TextBoxThree.Clear();
            TextBoxFour.Clear();
            RadioButtonOne.IsChecked = false;
            RadioButtonTwo.IsChecked = false;
            RadioButtonThree.IsChecked = false;
            RadioButtonFour.IsChecked = false;
            CheckBoxOne.IsChecked = false;
            CheckBoxTwo.IsChecked = false;
            CheckBoxThree.IsChecked = false;
            CheckBoxFour.IsChecked = false;
            ButtonCreateAQuestion.IsEnabled = false;
            ButtonResetQuestion.IsEnabled = false;
        }

        private void ButtonReport_Click(object sender, RoutedEventArgs e)
        {
            ReportStorage reports = ReportStorage.GetInstance();
            reports.Save();
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            tests.Save();
            groups.Save();
            reports.Save();
        }
    }
}
