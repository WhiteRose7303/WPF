using System;
using System.Windows;
using Final_Project_WPF.Screens;

namespace Final_Project_WPF
{
    /// <summary>
    /// Interaction logic for Hello.xaml
    /// </summary>
    public partial class Hello : Window
    {
        public Hello()
        {
            //test
            InitializeComponent();
            //Registor.IsEnabled = false;
            //loadmange();
            //loaddashboard();
        }

        private void loaddashboard()
        {
            Dashboard d = new Dashboard();
            d.Show();
            this.Close();
        }

        private void Registor_Click(object sender, RoutedEventArgs e)
        {
            Register r = new Register();
            r.Show();
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LogIn lg = new LogIn();
            lg.Show();
            this.Close();
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ClientManagment_click(object sender, RoutedEventArgs e)
        {
            //need to add a client check!

            AdminLogIn ad = new AdminLogIn();
            ad.Show();
            this.Close();
        }
        private void loadmange()
        {
            MainWindow h = new MainWindow();
            h.Show();
            this.Close();
        }

        private void Diagnostics_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Diagnostics d = new Diagnostics();
            d.Show();
            
        }
    }
}