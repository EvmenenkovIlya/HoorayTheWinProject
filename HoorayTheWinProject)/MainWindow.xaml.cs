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
        ObservableCollection<Node> groups;
        public MainWindow()
        {
            InitializeComponent();

            groups = new ObservableCollection<Node>
        {
            new Node
            {
                Name ="Group Фиксики",
                Nodes = new ObservableCollection<Node>
                {
                    new Node {Name="Чинила 1" },
                    new Node {Name="Чинила 2" },
                }
            },
            new Node
            {
                Name ="Group Лунтик's friends",
                Nodes = new ObservableCollection<Node>
                {
                    new Node {Name="Вупсень" },
                    new Node {Name="Пупсень" },
                    new Node {Name="Кузнечик" }
                }
            },

        };
            treeView1.ItemsSource = groups;

        }

        private void ButtonCreateAQuestion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCreateAReport_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
