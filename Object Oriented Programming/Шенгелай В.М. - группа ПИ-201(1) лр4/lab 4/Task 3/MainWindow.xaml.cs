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

namespace Task_3
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

        private void calcButton_Click(object sender, RoutedEventArgs e)
        {
            string expression = text_Block.Text;
            string num_1_string;
            string num_2_string;

            if (expression.Contains("*"))
            {
                num_1_string = expression.Substring(0, expression.IndexOf("*"));
                num_2_string = expression.Substring(expression.IndexOf("*")+1);
                int num1;
                int num2;
                
                bool result_1 = int.TryParse(num_1_string, out num1);
                bool result_2 = int.TryParse(num_2_string, out num2);

                if (result_1 & result_2)
                {
                    try
                    {
                        //Включаем проверку переполнения
                        int answer = checked (num1 * num2);
                        label_1_Answer.Content = "Ответ: " + answer;
                    }
                    catch (OverflowException)
                    {
                        MessageBox.Show("Обработано исключение переполнения. Ответ не помещается в тип int.", "Сообщение",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Невозможно прочитать выражение. Проверьте его на соотвествие формату", "Сообщение",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
            else
            {
                //Если нет операторов - попросим ввести
                MessageBox.Show("Выражение записано в неверном формате: нет арифметического оператора", "Сообщение", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
