using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Drawing;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Final_Project_WPF.DAL;
using Final_Project_WPF.BL;
using Final_Project_WPF.Screens;
using Final_Project_WPF.Util;
using System.ComponentModel;


namespace Final_Project_WPF.Screens
{
    /// <summary>
    /// Interaction logic for UserReport.xaml
    /// </summary>
    public partial class UserReport : Window
    {
        

        public UserReport()
        {
            InitializeComponent();
            loadview();
            Sortby.SelectionChanged += SelectionChanged;
            Sortdir .SelectionChanged += SelectionChanged;
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sort();
        }

        private void loadview()
        {
            ClientArr CAR = new ClientArr();
            CAR.Fill();
            UserListView.Items.Clear();
            UserListView.ItemsSource = CAR;

            Sortby.ItemsSource = new string[] { "Name", "ID", "Group" };
            Sortdir.ItemsSource = Enum.GetNames<ListSortDirection>();

            UserListView.Items.SortDescriptions.Add(new SortDescription("ID", ListSortDirection.Ascending));

        }

        void Sort()
        {
            var sortprop = Sortby.SelectedItem.ToString();
            var sortdir = Sortdir.SelectedItem.ToString() == "Ascending" ? ListSortDirection.Ascending : ListSortDirection.Descending;

            UserListView.Items.SortDescriptions[0] = new SortDescription(sortprop, sortdir); 
        }

        private void GoBack_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog dlg = new PrintDialog();

            Window currentMainWindow = Application.Current.MainWindow;

            Application.Current.MainWindow = this;

            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                Application.Current.MainWindow = currentMainWindow; // do it early enough if the 'if' is entered
                dlg.PrintVisual(this, "Certificate");
            }
        }

    }
}
