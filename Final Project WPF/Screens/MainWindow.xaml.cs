using Final_Project_WPF.BL;
using Final_Project_WPF.DAL;
using Final_Project_WPF.Util;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Final_Project_WPF.Screens;

namespace Final_Project_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
        private bool insert = false;

        public MainWindow()
        {
            InitializeComponent();

            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
            //debug
            DebugFill();

            ClientArrToForm();
        }

        public void DebugFill()
        {
            if ((MessageBox.Show("Debug purposes Fill DB?", "Fill DB", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                //clear table
                for (int i = 0; i < 1000; i++)
                {
                    Client_Dal.Delete(i);
                }
                //reseed
                //Client_Dal.reseed();
                //fill debug dataS
                Client_Dal.Insert("Hadar", "Ovadia", "0", "0501234567", "1", "1234", "1", "me@hadarov.com","1");
                Client_Dal.Insert("Israel", "Israeli", "1", "0501234567", "0", "0", "1", "test", "1");
                Client_Dal.Insert("Israela", "Israeli", "2", "0501234567", "0", "0", "1", "test@gmail.com", "1");
                Client_Dal.Insert("Dani", "Avdia", "3", "0501234567", "0", "0", "1", "test", "1");
                Client_Dal.Insert("rony", "old", "4", "0500000000", "0", "1234", "0", "rony@test.com", "1");
            }
        }

        private Client FromToClient()
        {
            Client Client = new Client();
            Client.FirstName = TB_FirstName.Text;
            Client.LastName = TB_LastName.Text;
            Client.Phone = TB_Phone.Text;
            Client.Email = Email.Text;
            Client.ID = int.Parse((string)IDLabel.Content);
            Client.Pass = Password.Text;
            if (Is_admin.IsChecked == true)
            {
                Client.Isadmin = "1";
            }
            else
            {
                Client.Isadmin = "0";
            }

            if (aproved.IsChecked == true)
            {
                Client.Aproved = "1";
            }
            else
            {
                Client.Aproved = "0";
            }

            if (StudentID_textbox.Text != "")
                Client.IdentityNumber = StudentID_textbox.Text;
            return Client;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Client client = FromToClient();

            if (client.ID == 0)
            {
                if (!CheckForm())
                {
                    //כאן מופיעה הודעת השגיאה
                    
                }
                else
                {
                    //הוספת לקוח חדש

                    client.Insert();

                    TB_FirstName.BorderBrush = Brushes.Green;
                    TB_LastName.BorderBrush = Brushes.Green;
                    StudentID_textbox.BorderBrush = Brushes.Green;
                    TB_Phone.BorderBrush = Brushes.Green;
                    dispatcherTimer.Start();
                    insert = true;
                    string admin;
                    string adminapr;
                    string teacher;
                    if ((bool)Is_admin.IsChecked)
                    {
                        admin = "Yes";
                    }
                    else
                        admin = "No";
                    if ((bool)aproved.IsChecked)
                    {
                        adminapr = "Yes";
                    }
                    else
                        adminapr = "No";
                    if ((bool)IsTeacher.IsChecked)
                    {
                        teacher = "Yes";
                    }
                    else
                        teacher = "No";
                    EmailSenders.SendEmail(Email.Text, TB_FirstName.Text, TB_LastName.Text, StudentID_textbox.Text, TB_Phone.Text, Password.Text);
                }
            }
            else
            {
                //עדכון לקוח קיים

                if (client.Update() && CheckForm())
                {
                    if (aprovecheck)
                    {
                        ClientArrToForm();

                        TB_FirstName.BorderBrush = Brushes.Green;
                        TB_LastName.BorderBrush = Brushes.Green;
                        StudentID_textbox.BorderBrush = Brushes.Green;
                        TB_Phone.BorderBrush = Brushes.Green;
                        dispatcherTimer.Start();
                        insert = true;
                        EmailSenders.updateemail(Email.Text, TB_FirstName.Text, TB_LastName.Text, StudentID_textbox.Text, TB_Phone.Text, Password.Text);
                    }
                    else
                    {
                        ClientArrToForm();

                        TB_FirstName.BorderBrush = Brushes.Green;
                        TB_LastName.BorderBrush = Brushes.Green;
                        StudentID_textbox.BorderBrush = Brushes.Green;
                        TB_Phone.BorderBrush = Brushes.Green;
                        dispatcherTimer.Start();
                        insert = true;
                        EmailSenders.updateemail(Email.Text, TB_FirstName.Text, TB_LastName.Text, StudentID_textbox.Text, TB_Phone.Text, Password.Text);
                    }
                }
                else
                {
                    MessageBox.Show("Error updating");
                    dispatcherTimer.Start();
                }
            }
            ClientArrToForm();
            Delete.IsEnabled = false;
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CheckForm()
        {
            bool flag = true;
            bool containsInt1 = TB_FirstName.Text.ToString().Any(char.IsDigit);
            if (TB_FirstName.Text.Length < 2 || containsInt1)
            {
                flag = false;
                TB_FirstName.BorderBrush = Brushes.Red;
                TB_FirstName.Focus();
            }
            else
                TB_FirstName.BorderBrush = Brushes.Silver;
            bool containsInt2 = TB_LastName.Text.ToString().Any(char.IsDigit);
            if (TB_LastName.Text.Length < 2 || containsInt2)
            {
                flag = false;
                TB_LastName.BorderBrush = Brushes.Red;
                TB_LastName.Focus();
            }
            else
            {
                TB_LastName.BorderBrush = Brushes.Silver;
            }
            bool containschar = StudentID_textbox.Text.ToString().Any(char.IsDigit);
            if (StudentID_textbox.Text.Length > 9 || !containschar)
            {
                flag = false;
                StudentID_textbox.BorderBrush = Brushes.Red;
                StudentID_textbox.Focus();
            }
            else
                StudentID_textbox.BorderBrush = Brushes.Silver;
            string phone = TB_Phone.Text.ToString();

            if (TB_Phone.Text.Length != 10 || phone[0] != '0' || phone[1] != '5')
            {
                flag = false;
                TB_Phone.BorderBrush = Brushes.Red;
                TB_Phone.Focus();
            }
            else
                TB_Phone.BorderBrush = Brushes.Silver;
            dispatcherTimer.Start();
            return flag;
        }

        private void ClientArrToForm()
        {
            //ממירה את הטנ "מ אוסף לקוחות לטופס

            ClientArr clientArr = new ClientArr();
            clientArr.Fill();
            ListBox_Client.ItemsSource = clientArr;
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            TB_FirstName.BorderBrush = Brushes.Silver;
            TB_LastName.BorderBrush = Brushes.Silver;
            StudentID_textbox.BorderBrush = Brushes.Silver;
            TB_Phone.BorderBrush = Brushes.Silver;
            if (insert == true)
            {
                clear();
            }

            dispatcherTimer.Stop();
        }

        private void clear()
        {
            TB_FirstName.Clear();
            TB_LastName.Clear();
            StudentID_textbox.Clear();
            TB_Phone.Clear();
            Password.Clear();
            Email.Clear();
            Is_admin.IsChecked = false;
            aproved.IsChecked = false;
            IDLabel.Content = "0";
            Delete.IsEnabled = false;
            insert = false;
        }

        private void ClientToForm(Client client)
        {
            //ממירה את המידע בטנ"מ לקוח לטופס
            if (client != null)
            {
                IDLabel.Content = client.ID.ToString();
                TB_FirstName.Text = client.FirstName;
                TB_LastName.Text = client.LastName;
                Email.Text = client.Email;
                StudentID_textbox.Text = client.IdentityNumber.ToString();
                TB_Phone.Text = client.Phone;
                Password.Text = client.Pass.ToString();
                if (client.Isadmin.ToString() == "1")
                {
                    Is_admin.IsChecked = true;
                }
                else
                {
                    Is_admin.IsChecked = false;
                }
                if (client.Aproved.ToString() == "1")
                {
                    aproved.IsChecked = true;
                }
                else
                {
                    aproved.IsChecked = false;
                }
                if (client.Teacher.ToString() == "1")
                {
                    IsTeacher.IsChecked = true;
                }
                else
                {
                    IsTeacher.IsChecked = false;
                }
            }
            else
            {
                clear();
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Client client = FromToClient();
            if (client.ID == 0)
                MessageBox.Show("You need to choose a client");
            else
            {
                //בהמשך תהיה כאן בדיקה שאין מידע נוסף על לקוח זה

                if ((MessageBox.Show("Are you sure?", "warning", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes))
                {
                    if (client.Delete())
                    {
                        EmailSenders.informdelete(Email.Text, TB_FirstName.Text, TB_LastName.Text, StudentID_textbox.Text, TB_Phone.Text, Password.Text);
                        Delete.IsEnabled = false;
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }

                    ClientToForm(null);
                    ClientArrToForm();
                }
            }
            client.ID = 0;
            ClientArrToForm();
            Delete.IsEnabled = false;
        }

        private void ListBox_Client_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ClientToForm(ListBox_Client.SelectedItem as Client);
            Delete.IsEnabled = true;
        }

        private void TB_FirstName_KeyUp(object sender, KeyEventArgs e)
        {
            ClientArr clientArr = new ClientArr();
            clientArr.Fill();

            //מסננים את אוסף הלקוחות לפי שדות הסינון שרשם המשתמש

            clientArr = clientArr.Filter(TB_FirstName.Text,
            TB_Phone.Text);
            //מציבים בתיבת הרשימה את אוסף הלקוחות

            ListBox_Client.ItemsSource = clientArr;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
            ClientArrToForm();
        }

        private void Go_Back_Click(object sender, RoutedEventArgs e)
        {
            Hello he = new Hello();
            he.Show();
            this.Close();
        }
        bool aprovecheck = false;
        private void aproved_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)aproved.IsChecked)
                aprovecheck = true;
            else
                aprovecheck = false;
           
        }

        public static void GetCurrentUser()
        {
            //need to find the open client
        } 

        private void GradeManeg_Click(object sender, RoutedEventArgs e)
        {
            GradeBuild g = new GradeBuild();
            g.Show();
            g.ClientArrToForm();
            this.Close();
        }
    }
}