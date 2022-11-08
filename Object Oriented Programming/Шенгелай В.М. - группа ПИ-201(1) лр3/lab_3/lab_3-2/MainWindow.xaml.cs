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

namespace lab_3
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

        public static int NOD(int a, int b) //Метод Евклида
        {
            if (a == 0) return b;
            while (b != 0)
            {
                if (a > b)
                {
                    a = a - b;
                }
                else
                {
                    b = b - a;
                }
            }
            return a;
        }

        // Перегружаем метод
        // Он работает!!! Вся логика на бумаге
        // !!! Нельзя скармливать ему меньше, чем 2 числа.
        // С 2-мя числами рекурсия работать не будет, но мы это предусмотрели
        public static int NOD(params int[] list)
        {
            if (list.Length == 2)
            {
                return NOD(list[0], list[1]);
            }
            int temp = list[list.Length - 1];
            int[] new_list = new int[list.Length - 1];

            for (int i = 0; i < new_list.Length; i++)
                new_list[i] = list[i];

            int back = 0;

            if (new_list.Length == 2)
            {
                back = NOD(new_list[0], new_list[1]);
                return back;
            }

            back = NOD(new_list);
            temp = NOD(back, temp);

            return temp;
        }

        private void euclidButton_Click(object sender, RoutedEventArgs e)
        {
            string[] string_nums = main_textBox.Text.Split(' ');
            int[] int_nums = new int[string_nums.Length];

            for (int i = 0; i < int_nums.Length; i++)
            {
                bool logic_result = Int32.TryParse(string_nums[i], out int_nums[i]);
                if (logic_result)
                {
                }
                else
                {
                    MessageBox.Show("Числа введены в неверном формате", "Сообщение", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    return;
                }
            }

            //Реализация проверки ввода нуля чисел или одного числа

            if (int_nums.Length == 1)
            {
                euclidLabel.Content = "Евклид: " + int_nums[0];
                return;
            }
            else if (int_nums.Length == 0)
            {
                MessageBox.Show("Вы ввели 0 чисел. Алгоритм так не работает", "Сообщение", MessageBoxButton.OK,
                MessageBoxImage.Information);
                return;
            }
            else
            {
                int resultOfCalc = NOD(int_nums);
                euclidLabel.Content = "Евклид: " + resultOfCalc;
            }
        }
    }
}
