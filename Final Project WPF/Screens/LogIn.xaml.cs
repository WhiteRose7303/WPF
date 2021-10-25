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
using System.Windows.Shapes;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Threading;

namespace Final_Project_WPF
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        public LogIn()
        {
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            InitializeComponent();
        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            Hello h = new Hello();
            h.Show();
            this.Close();
        }

        private void LogIn1_Click(object sender, RoutedEventArgs e)
        {
            SqlConnection connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename = C: \Users\Hadar CS\source\repos\WPF\Final Project WPF\Database1.mdf; Integrated Security=True");
            connection.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='C:\Users\Hadar CS\source\repos\WPF\Final Project WPF\Database1.mdf';Integrated Security=True";
            try
            {
                if (connection.State == ConnectionState.Closed)
                    connection.Open();
                string query = "SELECT COUNT(1) FROM Clientstab WHERE identitynumber=@ID AND password=@pass";
                SqlCommand sqlc = new SqlCommand(query, connection);
                sqlc.CommandType = CommandType.Text;
                sqlc.Parameters.AddWithValue("@ID", ID.Text);
                sqlc.Parameters.AddWithValue("@Pass", password.Text);
                int count = Convert.ToInt32(sqlc.ExecuteScalar());
                if (count == 1)
                {
                    string query2 = "SELECT aproved FROM Clientstab WHERE identitynumber=@ID AND password=@pass";
                    SqlCommand sqlcd = new SqlCommand(query2, connection);
                    sqlc.CommandType = CommandType.Text;
                    sqlcd.Parameters.AddWithValue("@ID", ID.Text);
                    sqlcd.Parameters.AddWithValue("@Pass", password.Text);
                    int isad = Convert.ToInt32(sqlcd.ExecuteScalar());
                    if (isad == 1)
                    {
                        Dashboard m = new Dashboard();
                        m.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You were not aproved by an admin, Please contact your system administrator", "Warning");
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
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            ID.BorderBrush = Brushes.Silver;
            password.BorderBrush = Brushes.Silver;
            dispatcherTimer.Stop();
        }
    }
}
