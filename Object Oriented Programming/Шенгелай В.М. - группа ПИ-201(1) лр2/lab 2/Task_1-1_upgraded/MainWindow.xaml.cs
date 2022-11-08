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

namespace Task_1_1
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

        private void calculateButton_approx_Click(object sender, RoutedEventArgs e)
        {
            string number_s = inputTextBox_number.Text;
            double number_d;

            bool result_2 = double.TryParse(number_s, out number_d);
            if (result_2)
            {
                inputTextBox_approx.Text = (number_d / 2).ToString();
                return;
            }
            else
            {
                Output_label.Content = "Не удалось конвертировать данные!";
                return;
            }
        }

        int start_iter = 0;

        private void calculateButton_number_Click(object sender, RoutedEventArgs e)
        {
            string approx_s = inputTextBox_approx.Text;
            double approx_d;

            string number_s = inputTextBox_number.Text;
            double number_d;

            bool result_1 = double.TryParse(approx_s, out approx_d);
            bool result_2 = double.TryParse(number_s, out number_d);
            if (result_1 & result_2)
            {
                if (number_d > 0)
                {
                    // Алгоритм Ньютона
                    double Calculate_double(double xi, double xi_prev, int iterations)
                    {
                        for (int i = 0; i < iterations; ++i)
                        {
                            xi = 0.5 * (number_d / xi_prev + xi_prev);
                            xi_prev = xi;
                        }
                        return xi;
                    }

                    decimal approx_decimal;
                    decimal number_decimal;
                    result_1 = decimal.TryParse(approx_s, out approx_decimal);
                    result_2 = decimal.TryParse(number_s, out number_decimal);

                    decimal Calculate_decimal(decimal xi, decimal xi_prev, int iterations)
                    {
                        for (int i = 0; i < iterations; ++i)
                        {
                            xi = (decimal)0.5 * (number_decimal / xi_prev + xi_prev);
                            xi_prev = xi;
                        }
                        return xi;
                    }

                    int iter1 = start_iter + 1;
                    int iter2 = iter1 + 10;
                    start_iter += 1;


                    double answer_1 = Calculate_double(approx_d, approx_d, iter1);
                    double answer_2 = Calculate_double(approx_d, approx_d, iter2);
                    decimal answer_3 = Calculate_decimal(approx_decimal, approx_decimal, iter2);

                    //Погрешность по методичке
                    double precision12 = Math.Abs((answer_1 - ((answer_2 / answer_1) + answer_1) / 2));

                    decimal answer_2_decimal = (decimal)answer_2;
                    decimal precision23 = Math.Abs((answer_2_decimal - ((answer_3 / answer_2_decimal) + answer_2_decimal) / 2));

                    Output_label.Content = "Операция успешна\n" +
                        "Ответ double: " + answer_1 + "\n" +
                        "Ответ double более точно: " + answer_2 + "\n" +
                        "Ответ decimal: " + answer_3 + "\n" +
                        "Погрешность double-double б.т.: " + precision12 + "\n" +
                        "Погрешность double б.т. - decimal: " + precision23 + "\n" +
                        "Номер итерации: " + start_iter + "\n";


                    return;
                }
                else
                {
                    Output_label.Content = "Пожалуйста, введите число >= 0";
                    return;
                }
            }
            else
            {
                Output_label.Content = "Не удалось конвертировать данные!";
                return;
            }
        }

        private void resetButt_Click(object sender, RoutedEventArgs e)
        {
            start_iter = 0;
            Output_label.Content = "";
        }
    }
}
