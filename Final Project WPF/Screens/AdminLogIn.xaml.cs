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
using Final_Project_WPF.DAL;
using Final_Project_WPF.BL;
using System.Data.SqlClient;
using System.Data;

namespace Final_Project_WPF
{
    /// <summary>
    /// Interaction logic for AdminLogIn.xaml
    /// </summary>
    public partial class AdminLogIn : Window
    {
        public AdminLogIn()
        {
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
                string query = "SELECT COUNT(1) FROM Clientstab WHERE FirstName=@Name AND password=@pass";
                SqlCommand sqlc = new SqlCommand(query, connection);
                sqlc.CommandType = CommandType.Text;
                sqlc.Parameters.AddWithValue("@Name", name.Text);
                sqlc.Parameters.AddWithValue("@Pass", password.Text);
                int count = Convert.ToInt32(sqlc.ExecuteScalar());
                if (count == 1)
                {
                    string query2 = "SELECT isadmin FROM Clientstab WHERE FirstName=@Name AND password=@pass";
                    SqlCommand sqlcd = new SqlCommand(query2, connection);
                    sqlc.CommandType = CommandType.Text;
                    sqlcd.Parameters.AddWithValue("@Name", name.Text);
                    sqlcd.Parameters.AddWithValue("@Pass", password.Text);
                    int isad = Convert.ToInt32(sqlcd.ExecuteScalar());
                    if(isad == 1)
                    {
                        MainWindow m = new MainWindow();
                        m.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You are not an administrator. please log in through the student login screen.");
                    }


                    
                }
                else
                {
                    MessageBox.Show("credentioals are incorrect!");
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
    }
}
