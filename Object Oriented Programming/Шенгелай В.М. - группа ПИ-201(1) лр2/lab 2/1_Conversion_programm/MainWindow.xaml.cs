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

namespace _1_Conversion_programm
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static bool MyExplicit <inType> (dynamic from)
        {
            try
            {
                inType to = (inType)from;
                return true;
            }
            catch
            {
                return false;
            }
        }

        static bool MyImplict<inType>(dynamic from)
        {
            try
            {
                inType to = from;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            
            //Type sourceType = typeof(Int16);

            

            
        }

        //Тестовый класс
        class Student
        {

        }

        private void calcButton_Click(object sender, RoutedEventArgs e)
        {
            textBlock_1.Text = "Явное преобразование: ";
            textBlock_2.Text = "Неявное преобразование: ";

            Dictionary<string, dynamic> typematching = new Dictionary<string, dynamic>();
            typematching.Add("char", (char)'a');
            typematching.Add("string", (string)"s");
            typematching.Add("byte", (byte)0);
            typematching.Add("int", (int)123);
            typematching.Add("float", (float)4.5);
            typematching.Add("double", (double)5.4);
            typematching.Add("decimal", (decimal)10);
            typematching.Add("bool", (bool)true);
            Student a = new Student();
            typematching.Add("object", (object)a);


            string to = comboBox2.Text;

            if (comboBox2.Text == "char")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<char>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<char>(typematching[comboBox1.Text]);
            }
            else if (comboBox2.Text == "string")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<string>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<string>(typematching[comboBox1.Text]);
            }
            else if (comboBox2.Text == "byte")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<byte>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<byte>(typematching[comboBox1.Text]);
            }
            else if (comboBox2.Text == "int")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<int>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<int>(typematching[comboBox1.Text]);
            }
            else if (comboBox2.Text == "float")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<float>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<float>(typematching[comboBox1.Text]);
            }
            else if (comboBox2.Text == "double")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<double>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<double>(typematching[comboBox1.Text]);
            }
            else if (comboBox2.Text == "decimal")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<decimal>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<decimal>(typematching[comboBox1.Text]);
            }
            else if (comboBox2.Text == "bool")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<bool>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<bool>(typematching[comboBox1.Text]);
            }
            else if (comboBox2.Text == "object")
            {
                textBlock_1.Text = "Явное преобразование: " + MyExplicit<object>(typematching[comboBox1.Text]);
                textBlock_2.Text = "Неявное преобразование: " + MyImplict<object>(typematching[comboBox1.Text]);
            }


        }
    }
}
