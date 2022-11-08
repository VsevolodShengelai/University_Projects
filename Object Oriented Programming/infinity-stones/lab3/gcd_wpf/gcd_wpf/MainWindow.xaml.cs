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


namespace gcd_wpf
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
        private static bool TryGetNumbers(ref TextBox[] textboxes, out int[] numbers, out int errorIndex)
        {
            numbers = new int[textboxes.Length];
            for (int i = 0; i < textboxes.Length; ++i)
            {
                if (!int.TryParse(textboxes[i].Text, out numbers[i]))
                {
                    errorIndex = i;
                    return false;
                }
            }
            errorIndex = -1;
            return true;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            int a, b;
            if (!int.TryParse(textbox1.Text, out a))
            {
                labelEuclid.Content = "В 1 поле находится не целое число";
                return;
            }
            if (!int.TryParse(textbox2.Text, out b))
            {
                labelEuclid.Content = "Во 2 поле находится не целое число";
                return;
            }
            Stopwatch sw = new();
            sw.Start();
            int gcd = GCDAlgorithms.EuclidGCD(a, b);
            sw.Stop();
            labelEuclid.Content = $"Euclid: {gcd}, Time (ticks): {sw.ElapsedTicks}";
            sw.Restart();
            gcd = GCDAlgorithms.SteinGCD(a, b);
            sw.Stop();
            labelStein.Content = $"Stein: {gcd}, Time (ticks): {sw.ElapsedTicks}";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            int[] ns;
            int errorIndex;
            TextBox[] textboxes = { textbox1, textbox2, textbox3 };
            if (!TryGetNumbers(ref textboxes, out ns, out errorIndex))
            {
                labelEuclid.Content = $"В поле ввода №{errorIndex + 1} содержится не целое число";
                return;
            }
            Stopwatch sw = new();
            sw.Start();
            int gcd = GCDAlgorithms.EuclidGCD(ns[0], ns[1], ns[2]);
            sw.Stop();
            labelEuclid.Content = $"Euclid: {gcd}, Time (ticks): {sw.ElapsedTicks}";
            sw.Restart();
            gcd = GCDAlgorithms.SteinGCD(ns[0], ns[1], ns[2]);
            sw.Stop();
            labelStein.Content = $"Stein: {gcd}, Time (ticks): {sw.ElapsedTicks}";
        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            int[] ns;
            int errorIndex;
            TextBox[] textboxes = { textbox1, textbox2, textbox3, textbox4 };
            if (!TryGetNumbers(ref textboxes, out ns, out errorIndex))
            {
                labelEuclid.Content = $"В поле ввода №{errorIndex + 1} содержится не целое число";
                return;
            }
            Stopwatch sw = new();
            sw.Start();
            int gcd = GCDAlgorithms.EuclidGCD(ns[0], ns[1], ns[2], ns[3]);
            sw.Stop();
            labelEuclid.Content = $"Euclid: {gcd}, Time (ticks): {sw.ElapsedTicks}";
            sw.Restart();
            gcd = GCDAlgorithms.SteinGCD(ns[0], ns[1], ns[2], ns[3]);
            sw.Stop();
            labelStein.Content = $"Stein: {gcd}, Time (ticks): {sw.ElapsedTicks}";
        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            int[] ns;
            int errorIndex;
            TextBox[] textboxes = { textbox1, textbox2, textbox3, textbox4, textbox5 };
            if (!TryGetNumbers(ref textboxes, out ns, out errorIndex))
            {
                labelEuclid.Content = $"В поле ввода №{errorIndex + 1} содержится не целое число";
                return;
            }
            Stopwatch sw = new();
            sw.Start();
            int gcd = GCDAlgorithms.EuclidGCD(ns[0], ns[1], ns[2], ns[3], ns[4]);
            sw.Stop();
            labelEuclid.Content = $"Euclid: {gcd}, Time (ticks): {sw.ElapsedTicks}";
            sw.Restart();
            gcd = GCDAlgorithms.SteinGCD(ns[0], ns[1], ns[2], ns[3], ns[4]);
            sw.Stop();
            labelStein.Content = $"Stein: {gcd}, Time (ticks): {sw.ElapsedTicks}";
        }

        private void buttonVar_Click(object sender, RoutedEventArgs e)
        {
            string[] strList = textboxVar.Text.Split(',');
            int[] intList = new int[strList.Length];
            for (int i = 0; i < intList.Length; ++i)
            {
                if (!int.TryParse(strList[i], out intList[i]))
                {
                    labelEuclid.Content = $"{i + 1}-й элемент это не целое число";
                    return;
                }
            }
            Stopwatch sw = new();
            sw.Start();
            int gcd = GCDAlgorithms.EuclidGCDVararg(intList);
            sw.Stop();
            labelEuclid.Content = $"Euclid: {gcd}, Time (ticks): {sw.ElapsedTicks}";
            sw.Restart();
            gcd = GCDAlgorithms.SteinGCDVararg(intList);
            sw.Stop();
            labelStein.Content = $"Stein: {gcd}, Time (ticks): {sw.ElapsedTicks}";
        }
    }
}
