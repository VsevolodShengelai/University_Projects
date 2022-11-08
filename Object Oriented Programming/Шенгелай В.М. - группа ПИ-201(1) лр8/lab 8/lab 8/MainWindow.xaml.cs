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
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Globalization;

namespace lab_8
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

        //Определим список здесь, чтобы иметь к нему доступ из любого метода
        List<double> ArrayList = new List<double>();

        public Tuple <string, string> StrRemLet(string[] str_arr)
        {
            StringBuilder from_str = new StringBuilder();
            StringBuilder chars_str = new StringBuilder();

            bool prevIsMinus = false;
            bool prevIsDelimiter = false;

            for (int i = 0; i < str_arr.Length; i++)
            {
                for (int j = 0; j < str_arr[i].Length; j++)
                {
                    if (char.IsLetter(str_arr[i][j]))
                    {
                        from_str.Append(' ');
                        chars_str.Append(str_arr[i][j]);
                        prevIsMinus = false;
                        prevIsDelimiter = false;
                        continue;
                    }
                    else if (str_arr[i][j] == '.')
                    {
                        if (!prevIsDelimiter)
                        {
                            from_str.Append('.');
                            //chars_str.Append(str_arr[i][j]); - Т.к. разделитель идёт в числа
                        }
                        prevIsDelimiter = true;
                        continue;
                    }
                    else if (str_arr[i][j] == '-')
                    {
                        if (!prevIsMinus)
                        {
                            from_str.Append('-');
                        }
                        prevIsMinus = true;
                        continue;
                    }
                    else
                    {
                        from_str.Append(str_arr[i][j]);
                        prevIsMinus = false;
                        prevIsDelimiter = false;
                    }
                }
            }
            string returnStr_chars = chars_str.ToString();
            string returnStr_nums = from_str.ToString();
            return Tuple.Create(returnStr_chars, returnStr_nums);
        }

        private void openNotepadButt_Click(object sender, RoutedEventArgs e)
        {
            //Зпуск нового процесса
            Process newProc = Process.Start("notepad.exe");
            newProc.WaitForExit();
            newProc.Close(); // освободить выделенные ресурсы
        }

        private void downloadFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"C:\Users\Vsevolod\source\repos\lab 8\lab 8";
            //Возвращяет true, false, или null
             if (openFileDialog.ShowDialog() == true)
            {
                //Выгружаем всё в одну строку-переменную, чтобы найти длину массива
                var linescount = File.ReadAllLines(openFileDialog.FileName);

                string[] str_arr = new string[linescount.Length];
                //Вариант с явным указанием кодировки, но и так работает
                //StreamReader f = new StreamReader(openFileDialog.FileName, Encoding.GetEncoding("utf-32"));
                StreamReader f = new StreamReader(openFileDialog.FileName);
                int arr_counter = 0;
                
                while (!f.EndOfStream)
                {
                    // Загружаем строки в массив строк
                    string s = f.ReadLine();
                    str_arr[arr_counter] = s;
                    arr_counter += 1;
                }
                f.Close();

                var tuple_1 = StrRemLet(str_arr);
                string Str_chars = tuple_1.Item1;
                string Str_nums = tuple_1.Item2;

                textBlock.Text = Str_nums;

                //Сразу же записываем символы в файл
                string writePath = @"C:\Users\Vsevolod\source\repos\lab 8\lab 8\charData.txt";

                string text = Str_chars;
                try
                {
                    // Кодировка "windows-1251"
                    using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.GetEncoding("windows-1251")))
                    {
                        sw.WriteLine(text);
                    }
                }
                catch (Exception E)
                {
                    MessageBox.Show(E.Message, "Сообщение", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                }

                //Отображение чисел на экран
                //Нужно проредить строку - убрать повторения пробелов

                while (Str_nums.Contains("  ")) { Str_nums = Str_nums.Replace("  ", " "); }

                var numbers = Str_nums.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

                ArrayList.Clear();

                double result;

                for (int i = 0; i < numbers.Length; i++)
                {
                    if (double.TryParse(numbers[i], out result))
                    {
                        ArrayList.Add(result);
                    }
                }

                updateTextBlock(ArrayList);
                //textBlock.Text = Str_nums;
            }

        }

        public void updateTextBlock(List<double> ArrayList)
        {
            textBlock.Text = "";
            foreach (double v in ArrayList)
                textBlock.Text += "\n" + v;
        }

        private void enterButt_Click(object sender, RoutedEventArgs e)
        {
            string[] plain_str = new string[] { textInput.Text };
            
            var tuple_1 = StrRemLet(plain_str);
            string Str_nums = tuple_1.Item2;

            while (Str_nums.Contains("  ")) { Str_nums = Str_nums.Replace("  ", " "); }

            var numbers = Str_nums.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            double result;

            for (int i = 0; i < numbers.Length; i++)
            {
                if (double.TryParse(numbers[i], out result))
                {
                    ArrayList.Add(result);
                }
            }
            updateTextBlock(ArrayList);
        }

        private void safeButt_Click(object sender, RoutedEventArgs e)
        {
            string writePath = @"C:\Users\Vsevolod\source\repos\lab 8\lab 8\numData.txt";
            string text = "";

            foreach (double v in ArrayList)
                text += v + " ";
            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default))
                {
                    sw.WriteLine(text);
                }
            }
            catch (Exception E)
            {
                MessageBox.Show(E.Message, "Сообщение", MessageBoxButton.OK,
                MessageBoxImage.Information);
            }
        }
    }



    class SmartArray
    {
        public double[] arr;
        private double maxIndex;


        public SmartArray(int maxIndex_arg)
        {
            maxIndex = maxIndex_arg;
            arr = new double[maxIndex_arg];
        }

        public double this[int NumOfElement]// индексатор
        {
            get
            {
                if (NumOfElement >= maxIndex)
                {
                    throw new IndexOutOfRangeException();// исключение
                }
                else
                    return arr[NumOfElement];
            }
            set
            {
                if (NumOfElement >= maxIndex)
                {
                    throw new IndexOutOfRangeException();
                }
                else
                    arr[NumOfElement] = value;
            }
        }
    }
}
