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

namespace Final_Project_WPF.Screens
{
    /// <summary>
    /// Interaction logic for Diagnostics.xaml
    /// </summary>
    public partial class Diagnostics : Window
    {
        public Diagnostics()
        {
            InitializeComponent();
        }

        private void DATA_R_Click(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            m.DebugFill();
        }

        private void OPENUSERREP_Click(object sender, RoutedEventArgs e)
        {
            UserReport u = new UserReport();
            u.ShowDialog();
        }
    }
}
