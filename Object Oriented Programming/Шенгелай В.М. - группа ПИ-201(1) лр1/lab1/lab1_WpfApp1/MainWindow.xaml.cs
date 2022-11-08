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

namespace lab1_WpfApp1
{
    /// <summary>
    /// WPF application to read and format data
    /// </summary>
    public partial class MainWindow : Window
    {
        /// /// <summary>
        /// Constructor for MainWindow
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Read a line of data entered by the user.
        /// Format the data and display the results in the
        /// formattedText TextBlock control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void testButton_Click(object sender, RoutedEventArgs e)
        {
            // Copy the contents of the TextBox into a string
            string line = testInput.Text;
            // Format the data in the string
            line = line.Replace(",", " y:");
            line = "x:" + line;
            // Store the results in the TextBlock
            formattedText.Text = line;
        }

        /// <summary>
        /// After the Window has loaded, read data from the standard input.
        /// Format each line and display the results in the
        /// formattedText TextBlock control.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Buffer to hold a line read from the file on standard input
            string line;
            // Loop until the end of the file
            while ((line = Console.ReadLine()) != null)
            {
                // Format the data in the buffer
                line = line.Replace(",", " y:");
                line = "x:" + line + "\n";
                // Put the results into the TextBlock
                formattedText.Text += line;
            }
        }
    }
}
