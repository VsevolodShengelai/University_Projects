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

namespace Task_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void convertButton_Click(object sender, RoutedEventArgs e)
        {
            string number_s = inputTextBox.Text;
            int number_i;

            bool result = int.TryParse(number_s, out number_i);

            if (result)
            {
                if (number_i >= 0)
                {
                    int i = number_i;
                    int remainder = 0;

                    StringBuilder binary = new StringBuilder("");

                    do
                    {
                        remainder = i % 2;
                        i = i / 2;
                        binary.Insert(0, remainder);
                    }
                    while (i > 0);

                    string output = binary.ToString();
                    binaryLabel.Content = output;

                    return;
                }
                else
                {
                    MessageBox.Show("Введите целое неотрицательное число", "Сообщение", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    return;
                }
            }
            else
            {
                MessageBox.Show("Введите целое неотрицательное число", "Сообщение", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
        }
    }
}
