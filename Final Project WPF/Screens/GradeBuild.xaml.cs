using Final_Project_WPF.BL;

using System.Windows;
using System.Windows.Input;

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
            GradeArrToForm();
        }


        private void Cancel_Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.Show();
            this.Close();
        }
        private Grade FormToGrade()
        {
            Grade g = new Grade();
            g.Name = TB_Name.Text;
            g.ID = int.Parse(IDLabel.Content.ToString());
            return g;
        }

        private void Save_Button_Click(object sender, RoutedEventArgs e)
        {
            Grade g = FormToGrade();

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
                    GradeArrToForm();
                }
                else
                {
                    MessageBox.Show("Error updating");
                }
            }
            GradeArrToForm();
        }

        public void GradeArrToForm()
        {
            //ממירה את הטנ "מ אוסף לקוחות לטופס

            GradeArr grade = new GradeArr();
            grade.Fill();
            listbox_Grade.ItemsSource = grade;
        }

        private void ListBox_Client_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            GradeToForm(listbox_Grade.SelectedItem as Grade);
        }
        private void GradeToForm(Grade grd)
        {
            if (grd != null)
            {
                TB_Name.Text = grd.Name;
                IDLabel.Content = grd.ID;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Grade g = FormToGrade();
            if (g.ID == 0)
            {
                MessageBox.Show("You need to choose a client");
            }
            else
            {
                //בהמשך תהיה כאן בדיקה שאין מידע נוסף על לקוח זה

                if ((MessageBox.Show("Are you sure?", "warning", MessageBoxButton.YesNo,
                MessageBoxImage.Warning) == MessageBoxResult.Yes))
                {
                    g.Delete();
                    TB_Name.Text = "";
                    GradeArrToForm();
                }
                else
                { }




            }
        }
    }

}