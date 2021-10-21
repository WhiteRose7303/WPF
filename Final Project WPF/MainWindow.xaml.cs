using Final_Project_WPF.BL;
using Final_Project_WPF.DAL;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

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
            if ((MessageBox.Show("Debug purposes Fill DB?", "Fill DB", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes))
            {
                for (int i = 0; i < 1000; i++)
                {
                    Client_Dal.Delete(i);
                }
                Client_Dal.Insert("Hadar", "Ovadia", "1234567", "0501234567");
                Client_Dal.Insert("Israel", "Israeli", "1234567", "0501234567");
                Client_Dal.Insert("Israela", "Israeli", "1234567", "0501234567");
                Client_Dal.Insert("Dani", "Avdia", "1234567", "0501234567");
            }
            ClientArrToForm();
        }

        private Client FromToClient()
        {
            Client Client = new Client();
            Client.FirstName = TB_FirstName.Text;
            Client.LastName = TB_LastName.Text;
            Client.Phone = TB_Phone.Text;
            Client.ID = int.Parse((string)IDLabel.Content);
            if (TB_ZipCode.Text != "")
                Client.ZipCode = TB_ZipCode.Text;
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
                    MessageBox.Show("Error");
                }
                else
                {
                    //הוספת לקוח חדש

                    client.Insert();

                    TB_FirstName.BorderBrush = Brushes.Green;
                    TB_LastName.BorderBrush = Brushes.Green;
                    TB_ZipCode.BorderBrush = Brushes.Green;
                    TB_Phone.BorderBrush = Brushes.Green;
                    dispatcherTimer.Start();
                    insert = true;
                }
            }
            else
            {
                //עדכון לקוח קיים

                if (client.Update() && CheckForm())
                {
                    ClientArrToForm();

                    TB_FirstName.BorderBrush = Brushes.Green;
                    TB_LastName.BorderBrush = Brushes.Green;
                    TB_ZipCode.BorderBrush = Brushes.Green;
                    TB_Phone.BorderBrush = Brushes.Green;
                    dispatcherTimer.Start();
                    insert = true;
                }
                else
                {
                    MessageBox.Show("Error updating");
                    dispatcherTimer.Start();
                }
            }
            ClientArrToForm();
        }

        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private bool CheckForm()
        {
            bool flag = true;
            if (TB_FirstName.Text.Length < 2)
            {
                flag = false;
                TB_FirstName.BorderBrush = Brushes.Red;
                TB_FirstName.Focus();
            }
            else
                TB_FirstName.BorderBrush = Brushes.Silver;

            if (TB_ZipCode.Text.Length != 7)
            {
                flag = false;
                TB_ZipCode.BorderBrush = Brushes.Red;
                TB_ZipCode.Focus();
            }
            else
                TB_ZipCode.BorderBrush = Brushes.Silver;

            if (TB_Phone.Text.Length != 10)
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
            TB_ZipCode.BorderBrush = Brushes.Silver;
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
            TB_ZipCode.Clear();
            TB_Phone.Clear();
            IDLabel.Content = "0";
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
                TB_ZipCode.Text = client.ZipCode.ToString();
                TB_Phone.Text = client.Phone;
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

            ListBox_Client.DataContext = clientArr;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }
    }
}