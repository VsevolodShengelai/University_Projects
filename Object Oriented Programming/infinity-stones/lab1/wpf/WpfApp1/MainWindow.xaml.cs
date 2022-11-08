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

/// <summary>
/// Приложение WPF которое читает и форматирует данные
/// </summary>
namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Конструктор MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Читает строку, введённую пользователем.
        /// Форматированную строку помещает в текстовое поле
        /// formattedText.
        /// </summary>
        /// <param name="sender">отправитель</param>
        /// <param name="e">событие</param>
        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            string line = testInput.Text;
            line = "x:" + line.Replace(",", " y:");
            formattedText.Text = line;
        }

        /// <summary>
        /// После загрузки приложения, читает данные из
        /// стандартного ввода и форматированный текст помещает
        /// в текстовое поле formattedText
        /// </summary>
        /// <param name="sender">отправитель</param>
        /// <param name="e">событие</param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string line;
            while ((line = Console.ReadLine()) != null)
            {
                formattedText.Text += "x:" + line.Replace(",", " y:") + Environment.NewLine;
            }
        }
    }
}
