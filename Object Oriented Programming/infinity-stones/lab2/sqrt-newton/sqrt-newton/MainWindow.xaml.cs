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

namespace sqrt_newton
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Globalization.CultureInfo cultureInfo =
            System.Globalization.CultureInfo.CreateSpecificCulture("ru-RU");
        static readonly decimal decimalDelta = Convert.ToDecimal(Math.Pow(10, -28));

        decimal guess, prevGuess;
        bool numberChanged = true;

        double enteredNumberDouble;
        decimal enteredNumberDecimal;
        decimal initialGuess;
        decimal errorRate;
        decimal newtonSqrt;
        double dotnetSqrt;
        int iterationCount;

        public MainWindow()
        {
            InitializeComponent();
        }
        
        decimal NewtonSqrt(decimal n)
        {
            guess = initialGuess;
            iterationCount = 0;
            do
            {
                prevGuess = guess;
                guess = ((n / guess) + guess) / 2;
                ++iterationCount;
            }
            while (Math.Abs(prevGuess - guess) >= decimalDelta);
            return prevGuess;
        }

        private void ClearLabels()
        {
            labelNewton.Content = "";
            labelErrorRate.Content = "";
            labelGuessDifference.Content = "";
            labelIterations.Content = "";
            labelDotnet.Content = "";
        }

        private void UpdateLabels()
        {
            errorRate = Math.Abs(Convert.ToDecimal(dotnetSqrt) - newtonSqrt);

            labelDotnet.Content = dotnetSqrt.ToString("N28", cultureInfo) + " (Используя .NET Framework)";
            labelNewton.Content = newtonSqrt.ToString("N28", cultureInfo) + " (Используя метод Ньютона)";
            labelIterations.Content = "Количество итераций: " + iterationCount;
            labelGuessDifference.Content = "Изменение: " + (guess - prevGuess);
            labelErrorRate.Content = "Погрешность: " + errorRate;
        }

        private bool TryGetNumbersFromTextboxes()
        {
            ClearLabels();

            // ввод данных
            if (!double.TryParse(textboxNumber.Text, System.Globalization.NumberStyles.Float,
                    cultureInfo, out enteredNumberDouble)
                || !decimal.TryParse(textboxNumber.Text, System.Globalization.NumberStyles.Float,
                    cultureInfo, out enteredNumberDecimal))
            {
                labelDotnet.Content = "Ошибка парсинга числа. Попробуйте запятую вместо точки.";
                return false;
            }
            if (!decimal.TryParse(textboxInitialGuess.Text, System.Globalization.NumberStyles.Float, cultureInfo, out initialGuess))
            {
                labelDotnet.Content = "Ошибка парсинга начального приближения. Попробуйте запятую вместо точки.";
                return false;
            }
            if (enteredNumberDouble < 0)
            {
                labelDotnet.Content = "Введите положительное число.";
                return false;
            }
            if (initialGuess < 0)
            {
                labelDotnet.Content = "Введите положительное начальное приближение.";
                return false;
            }
            return true;
        }

        private void PreciseButton_Click(object sender, RoutedEventArgs e)
        {
            if (numberChanged && !TryGetNumbersFromTextboxes())
                return;
            numberChanged = false;
            
            dotnetSqrt = Math.Sqrt(enteredNumberDouble);
            newtonSqrt = NewtonSqrt(enteredNumberDecimal);
            errorRate = Math.Abs(Convert.ToDecimal(dotnetSqrt) - newtonSqrt);
            UpdateLabels();
        }

        private void TextboxNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            numberChanged = true;
        }

        private void NextIterButton_Click(object sender, RoutedEventArgs e)
        {
            if (!numberChanged)
            {
                prevGuess = guess;
                guess = ((enteredNumberDecimal / guess) + guess) / 2;
                newtonSqrt = guess;
                ++iterationCount;

                UpdateLabels();
                return;
            }
            if (!TryGetNumbersFromTextboxes())
                return;
            numberChanged = false;
            dotnetSqrt = Math.Sqrt(enteredNumberDouble);
            labelDotnet.Content = dotnetSqrt.ToString("N28", cultureInfo) + " (Используя .NET Framework)";

            prevGuess = guess = initialGuess;
            guess = ((enteredNumberDecimal / guess) + guess) / 2;
            newtonSqrt = guess;
            iterationCount = 1;

            UpdateLabels();
        }
    }
}
