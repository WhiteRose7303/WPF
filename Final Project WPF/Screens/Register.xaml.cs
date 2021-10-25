using Final_Project_WPF.BL;
using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Final_Project_WPF.Util;

namespace Final_Project_WPF
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class Register : Window
    {
        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private bool insert = false;
        public Register()
        {
            InitializeComponent();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
        }

       

        private void gobackButton_Click(object sender, RoutedEventArgs e)
        {
            Hello h = new Hello();
            h.Show();
            this.Close();
        }

        private void registerButton_Click(object sender, RoutedEventArgs e)
        {
            Client client = FromToClient();

            if (client.ID == 0)
            {
                if (!CheckForm())
                {
                    //כאן מופיעה הודעת השגיאה
                    MessageBox.Show("Error");
                }
                else
                {
                    //הוספת לקוח חדש

                    client.Insert();

                    FirstName_TB.BorderBrush = Brushes.Green;
                    LastName_TB.BorderBrush = Brushes.Green;
                    ID_TB.BorderBrush = Brushes.Green;
                    Phone_TB.BorderBrush = Brushes.Green;
                    dispatcherTimer.Start();
                    insert = true;
                    EmailSenders.SelfRegEmail(Email_TB.Text, FirstName_TB.Text, LastName_TB.Text, ID_TB.Text, Phone_TB.Text, Pass_TB.Text);
                }
            }
        }

        private bool CheckForm()
        {
            bool flag = true;
            bool containsInt1 = FirstName_TB.Text.ToString().Any(char.IsDigit);
            if (FirstName_TB.Text.Length < 2 || containsInt1)
            {
                flag = false;
                FirstName_TB.BorderBrush = Brushes.Red;
                FirstName_TB.Focus();
            }
            else
                FirstName_TB.BorderBrush = Brushes.Silver;
            bool containsInt2 = LastName_TB.Text.ToString().Any(char.IsDigit);
            if (LastName_TB.Text.Length < 2 || containsInt2)
            {
                flag = false;
                LastName_TB.BorderBrush = Brushes.Red;
                LastName_TB.Focus();
            }
            else
            {
                LastName_TB.BorderBrush = Brushes.Silver;
            }
            bool containschar = ID_TB.Text.ToString().Any(char.IsDigit);
            if (ID_TB.Text.Length > 9 || !containschar)
            {
                flag = false;
                ID_TB.BorderBrush = Brushes.Red;
                ID_TB.Focus();
            }
            else
                ID_TB.BorderBrush = Brushes.Silver;
            string phone = Phone_TB.Text.ToString();

            if (Phone_TB.Text.Length != 10 || phone[0] != '0' || phone[1] != '5')
            {
                flag = false;
                Phone_TB.BorderBrush = Brushes.Red;
                Phone_TB.Focus();
            }
            else
                Phone_TB.BorderBrush = Brushes.Silver;
            dispatcherTimer.Start();
            return flag;
        }

        private Client FromToClient()
        {
            Client Client = new Client();
            Client.FirstName = FirstName_TB.Text;
            Client.LastName = LastName_TB.Text;
            Client.Phone = Phone_TB.Text;
            Client.Email = Email_TB.Text;
            Client.Pass = Pass_TB.Text;
            if (ID_TB.Text != "")
                Client.IdentityNumber = ID_TB.Text;
            return Client;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            FirstName_TB.BorderBrush = Brushes.Silver;
            LastName_TB.BorderBrush = Brushes.Silver;
            ID_TB.BorderBrush = Brushes.Silver;
            Phone_TB.BorderBrush = Brushes.Silver;
            if (insert == true)
            {
                clear();
            }

            dispatcherTimer.Stop();
        }

        private void clear()
        {
            FirstName_TB.Clear();
            LastName_TB.Clear();
            ID_TB.Clear();
            Phone_TB.Clear();
            Pass_TB.Clear();
            Email_TB.Clear();
            insert = false;
        }

        
    }
}