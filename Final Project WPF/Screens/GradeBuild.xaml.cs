using Final_Project_WPF.BL;
using Final_Project_WPF.BL.Grade;
using System.Windows;

namespace Final_Project_WPF.Screens
{
    /// <summary>
    /// Interaction logic for GradeBuild.xaml
    /// </summary>
    public partial class GradeBuild : Window
    {
        public GradeBuild()
        {
            InitializeComponent();
            ClientArrToForm();
        }


        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
        private Grade FromToClient()
        {
            Grade g = new Grade();
            g.Name = TB_Name.Text;
            g.ID = int.Parse((string)IDLabel.Content);
            return g;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Grade g = FromToClient();

            if (g.ID == 0)
            {
                //הוספת לקוח חדש

                g.Insert();
            }
            else
            {
                //עדכון לקוח קיים

                if (g.Update())
                {
                    ClientArrToForm();
                }
                else
                {
                    MessageBox.Show("Error updating");
                }
            }
            ClientArrToForm();
        }

        public void ClientArrToForm()
        {
            //ממירה את הטנ "מ אוסף לקוחות לטופס

            GradeArr clientArr = new GradeArr();
            clientArr.Fill();
            listbox_Grade.ItemsSource = clientArr;
        }
    }

}