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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace task3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool _checked = false;
        public bool Checked
        {
            get => _checked;
            set => _checked = value;
        }
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int maxInt = int.MaxValue;

            try
            {
                if (Checked)
                {
                    checked
                    {
                        maxInt *= 2;
                    }
                }
                else
                {
                    unchecked
                    {
                        maxInt *= 2;
                    }
                }
            }
            catch (OverflowException ex)
            {
                MessageBox.Show(ex.Message, "Exception occured", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            TextBox1.Text = maxInt.ToString();
        }
    }
}
