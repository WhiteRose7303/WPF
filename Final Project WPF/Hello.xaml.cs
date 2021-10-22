using System.Windows;

namespace Final_Project_WPF
{
    /// <summary>
    /// Interaction logic for Hello.xaml
    /// </summary>
    public partial class Hello : Window
    {
        public Hello()
        {
            InitializeComponent();
        }

        private void Registor_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
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
    }
}