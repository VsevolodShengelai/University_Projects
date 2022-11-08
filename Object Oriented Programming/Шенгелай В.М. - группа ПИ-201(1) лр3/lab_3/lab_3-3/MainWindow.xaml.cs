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

namespace lab_3
{
    /// <summary>
    /// В этой работе используются алгоритмы поиска НОД только для двух чисел!
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
                shteinLabel.Content = "Штейн: " + int_nums[0];
                return;
            }
            else if (int_nums.Length == 0)
            {
                MessageBox.Show("Вы ввели 0 чисел. Алгоритм так не работает", "Сообщение", MessageBoxButton.OK,
                MessageBoxImage.Information);
                return;
            }
            else if (int_nums.Length == 2)
            {
                // Замеры скорости
                Stopwatch sw = new Stopwatch();
                sw.Start();
                int euclid_rez = NOD(int_nums);
                sw.Stop();
                long euclidTime = sw.ElapsedTicks;

                sw.Start();
                int shtein_rez = FindGCDStein(int_nums[0], int_nums[1]);
                sw.Stop();
                long shteinTime = sw.ElapsedTicks;

                euclidLabel.Content = "Евклид: " + euclid_rez + "  Кол-во тиков: " + euclidTime;
                shteinLabel.Content = "Штейн: " + shtein_rez + "  Кол-во тиков: " + shteinTime;
                return;
            }
            else
            {
                // Замеры скорости
                Stopwatch sw = new Stopwatch();
                sw.Start();
                int euclid_rez = NOD(int_nums);
                sw.Stop();
                long euclidTime = sw.ElapsedTicks;

                Stopwatch sw_new = new Stopwatch();
                sw_new.Start();
                int shtein_rez = FindGCDStein(int_nums);
                sw_new.Stop();
                long shteinTime = sw_new.ElapsedTicks;

                euclidLabel.Content = "Евклид: " + euclid_rez + "  Кол-во тиков: " + euclidTime;
                shteinLabel.Content = "Штейн: " + shtein_rez + "  Кол-во тиков: " + shteinTime;
                return;
            }
        }

        static public int FindGCDStein(int u, int v)
        {
            int k;
            // Step 1.
            // gcd(0, v) = v, because everything divides zero,
            // and v is the largest number that divides v.
            // Similarly, gcd(u, 0) = u. gcd(0, 0) is not typically
            // defined, but it is convenient to set gcd(0, 0) = 0.
            if (u == 0 || v == 0)
                return u | v;
            // Step 2.
            // If u and v are both even, then gcd(u, v) = 2•gcd(u/2, v/2),
            // because 2 is a common divisor.
            for (k = 0; ((u | v) & 1) == 0; ++k)
            {
                u >>= 1;
                v >>= 1;
            }
            // Step 3.
            // If u is even and v is odd, then gcd(u, v) = gcd(u/2, v),
            // because 2 is not a common divisor.
            // Similarly, if u is odd and v is even,
            // then gcd(u, v) = gcd(u, v/2).
            while ((u & 1) == 0)
                u >>= 1;
            // Step 4.
            // If u and v are both odd, and u ≥ v,
            // then gcd(u, v) = gcd((u − v)/2, v).
            // If both are odd and u < v, then gcd(u, v) = gcd((v − u)/2, u).
            // These are combinations of one step of the simple
            // Euclidean algorithm,
            // which uses subtraction at each step, and an application
            // of step 3 above.
            // The division by 2 results in an integer because the
            // difference of two odd numbers is even.
            do
            {
                while ((v & 1) == 0) // Loop x
                    v >>= 1;
                // Now u and v are both odd, so diff(u, v) is even.
                // Let u = min(u, v), v = diff(u, v)/2.
                if (u < v)
                {
                    v -= u;
                }
                else
                {
                    int diff = u - v;
                    u = v;
                    v = diff;
                }
                v >>= 1;
                // Step 5.
                // Repeat steps 3–4 until u = v, or (one more step)
                // until u = 0.
                // In either case, the result is (2^k) * v, where k is
                // the number of common factors of 2 found in step 2.
            } while (v != 0);
            u <<= k;
            return u;
        }

        public static int FindGCDStein(params int[] in_array)
        {
            if (in_array.Length == 1)
            {
                return in_array[0];
            }
            else if (in_array.Length == 0)
            {
                MessageBox.Show("Список для Штейна пуст");
                return 0;
            }

            int cur_gcd = FindGCDStein(in_array[0], in_array[1]);

            for (int i = 2; i < in_array.Length; i++)
            {
                cur_gcd = FindGCDStein(cur_gcd, in_array[i]);

                if (cur_gcd == 1)
                {
                    break;
                }
            }

            return cur_gcd;
        }
    }
}
