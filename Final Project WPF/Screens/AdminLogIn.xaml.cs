using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace Final_Project_WPF
{
    /// <summary>
    /// Interaction logic for AdminLogIn.xaml
    /// </summary>
    public partial class AdminLogIn : Window
    {
        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();

        public AdminLogIn()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            InitializeComponent();
        }

        private void A_LogIn_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename = C: \Users\Hadar CS\source\repos\WPF\Final Project WPF\Database1.mdf; Integrated Security=True");
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Hadar CS\source\repos\WPF\Final Project WPF\Database1.mdf';Integrated Security=True";
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = "SELECT COUNT(1) FROM Clientstab WHERE IdentityNumber=@ID AND password=@pass";
                SqlCommand sqlc = new SqlCommand(query, connection);
                sqlc.CommandType = CommandType.Text;
                sqlc.Parameters.AddWithValue("@ID", ID.Text);
                sqlc.Parameters.AddWithValue("@Pass", password.Text);
                int count = Convert.ToInt32(sqlc.ExecuteScalar());
                if (count == 1)
                {
                    string query2 = "SELECT isadmin FROM Clientstab WHERE IdentityNumber=@ID AND password=@pass";
                    SqlCommand sqlcd = new SqlCommand(query2, connection);
                    sqlc.CommandType = CommandType.Text;
                    sqlcd.Parameters.AddWithValue("@ID", ID.Text);
                    sqlcd.Parameters.AddWithValue("@Pass", password.Text);
                    int isad = Convert.ToInt32(sqlcd.ExecuteScalar());
                    if (isad == 1)
                    {
                        MainWindow m = new MainWindow();
                        m.Show();
                        this.Close();
                        //add call to the client finder finalprojectwpf.getcurrentuser();
                    }
                    else
                    {
                        MessageBox.Show("You are not an administrator. please log in through the student login screen.");
                    }
                }
                else
                {
                    ID.BorderBrush = Brushes.Red;
                    password.BorderBrush = Brushes.Red;
                    dispatcherTimer.Start();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Hello h = new Hello();
            h.Show();
            this.Close();
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ID.BorderBrush = Brushes.Silver;
            password.BorderBrush = Brushes.Silver;
            dispatcherTimer.Stop();
        }
    }
}