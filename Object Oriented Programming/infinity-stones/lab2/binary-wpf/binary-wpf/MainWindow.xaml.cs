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

namespace binary_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            long num;
            if (!long.TryParse(textbox1.Text, out num))
            {
                label1.Content = "В поле ввода не целое число.";
                return;
            }
            if (num < 0)
            {
                label1.Content = "Введите положительное целое число или ноль.";
                return;
            }
            if (num == 0)
            {
                label1.Content = "0";
                return;
            }

            StringBuilder result = new StringBuilder();
            while (num > 0)
            {
                result.Insert(0, (int) num & 1);
                num /= 2;
            }

            label1.Content = result;
        }
    }
}
