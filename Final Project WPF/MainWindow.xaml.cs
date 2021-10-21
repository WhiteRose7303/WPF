using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Final_Project_WPF.BL;
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
            ClientArrToForm();
        }

        private Client FromToClient()
        {
            Client Client = new Client();
            Client.FirstName = m_FirstName.Text;
            Client.LastName = m_LastName.Text;
            Client.Phone = m_Phone.Text;
            Client.ID = int.Parse((string)IDLabel.Content);
            if (m_ZipCode.Text != "")
                Client.ZipCode = m_ZipCode.Text;
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

                    m_FirstName.BorderBrush = Brushes.Green;
                    m_LastName.BorderBrush = Brushes.Green;
                    m_ZipCode.BorderBrush = Brushes.Green;
                    m_Phone.BorderBrush = Brushes.Green;
                    dispatcherTimer.Start();
                    insert = true;
                }
            }
            else
            {
                //עדכון לקוח קיים

                if (client.Update())
                {
                    ClientArrToForm();

                    m_FirstName.BorderBrush = Brushes.Green;
                    m_LastName.BorderBrush = Brushes.Green;
                    m_ZipCode.BorderBrush = Brushes.Green;
                    m_Phone.BorderBrush = Brushes.Green;
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
            if (m_FirstName.Text.Length < 2)
            {
                flag = false;
                m_FirstName.BorderBrush = Brushes.Red;
                m_FirstName.Focus();
            }
            else
                m_FirstName.BorderBrush = Brushes.Silver;

            if (m_ZipCode.Text.Length != 7)
            {
                flag = false;
                m_ZipCode.BorderBrush = Brushes.Red;
                m_ZipCode.Focus();
            }
            else
                m_ZipCode.BorderBrush = Brushes.Silver;

            if (m_Phone.Text.Length != 10)
            {
                flag = false;
                m_Phone.BorderBrush = Brushes.Red;
                m_Phone.Focus();
            }
            else
                m_Phone.BorderBrush = Brushes.Silver;

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
            m_FirstName.BorderBrush = Brushes.Silver;
            m_LastName.BorderBrush = Brushes.Silver;
            m_ZipCode.BorderBrush = Brushes.Silver;
            m_Phone.BorderBrush = Brushes.Silver;
            if (insert == true)
            {
                clear();
            }

            dispatcherTimer.Stop();
        }

        private void clear()
        {
            m_FirstName.Clear();
            m_LastName.Clear();
            m_ZipCode.Clear();
            m_Phone.Clear();
            IDLabel.Content = "0";
        }

        private void ClientToForm(Client client)
        {
            //ממירה את המידע בטנ"מ לקוח לטופס
            if (client != null)
            {
                IDLabel.Content = client.ID.ToString();
                m_FirstName.Text = client.FirstName;
                m_LastName.Text = client.LastName;
                m_ZipCode.Text = client.ZipCode.ToString();
                m_Phone.Text = client.Phone;
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

        private void m_FirstName_KeyUp(object sender, KeyEventArgs e)
        {
            ClientArr clientArr = new ClientArr();
            clientArr.Fill();

            //מסננים את אוסף הלקוחות לפי שדות הסינון שרשם המשתמש

            clientArr = clientArr.Filter(m_FirstName.Text,
            m_Phone.Text);
            //מציבים בתיבת הרשימה את אוסף הלקוחות

            ListBox_Client.DataContext = clientArr;
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            clear();
        }

    }
}