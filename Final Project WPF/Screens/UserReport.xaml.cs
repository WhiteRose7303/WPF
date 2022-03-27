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
        }
        
        private void loadview()
        {
            ClientArr CAR = new ClientArr();
            CAR.Fill();
            UserListView.Items.Clear();
            UserListView.ItemsSource = CAR;            
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
        /*
private void PopulateView()
{
ClientArr Clientar = new ClientArr();
Clientar.Fill();

Client p;
ListViewItem LVI;
for (int i = 0; i < Clientar.Count; i++)
{
p = Clientar[i] as Client;

//יצירת פריט-תיבת-תצוגה
//LVI = new ListViewItem(new[] { p.FirstName, p.LastName, p.Email, p.Phone, p.ID });
//הוספת פריט-תיבת-תצוגה לתיבת תצוגה

UserListView.Items.Add(LVI);
}
}
*/


    }
}
