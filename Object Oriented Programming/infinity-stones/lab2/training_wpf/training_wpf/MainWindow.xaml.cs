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

namespace training_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    ///

    public partial class MainWindow : Window
    {
        string[] types = {
            "char", "string", "byte", "int", "float", "double", "decimal", "bool", "object"
        };
        dynamic dynVar;

        FlowDocument flowDoc;
        Paragraph paragraph;

        public MainWindow()
        {
            InitializeComponent();

            comboFromType.ItemsSource = types;
            comboToType.ItemsSource = types;

            paragraph = new Paragraph();
            flowDoc = new FlowDocument(paragraph);
            outputLabel.Document = flowDoc;
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            paragraph.Inlines.Clear();
            
            // получаем выбранный тип из ComboBox
            string fromType = comboFromType.SelectedItem as string;
            if (fromType == null)
            {
                paragraph.Inlines.Add(new Run("Выберите тип, из которого нужно преобразовать")
                {
                    Foreground = Brushes.Blue
                });
                return;
            }
            string toType = comboToType.SelectedItem as string;
            if (toType == null)
            {
                paragraph.Inlines.Add(new Run("Выберите тип, в который нужно преобразовать")
                {
                    Foreground = Brushes.Blue
                });
                return;
            }

            bool canImplicitCovert = true;
            bool canExplicitCovert = true;

            switch (fromType)
            {
                case "char":
                    dynVar = new char();
                    break;
                case "byte":
                    dynVar = new byte();
                    break;
                case "int":
                    dynVar = new int();
                    break;
                case "float":
                    dynVar = new float();
                    break;
                case "double":
                    dynVar = new double();
                    break;
                case "decimal":
                    dynVar = new decimal();
                    break;
                case "bool":
                    dynVar = new bool();
                    break;
                case "string":
                    dynVar = "";
                    break;
                case "object":
                    dynVar = new object();
                    break;
            }

            try
            {
                switch (toType)
                {
                    case "char":
                        char foobar1 = (char)dynVar;
                        break;
                    case "byte":
                        byte foobar2 = (byte)dynVar;
                        break;
                    case "int":
                        int foobar3 = (int)dynVar;
                        break;
                    case "float":
                        float foobar4 = (float)dynVar;
                        break;
                    case "double":
                        double foobar5 = (double)dynVar;
                        break;
                    case "decimal":
                        decimal foobar6 = (decimal)dynVar;
                        break;
                    case "bool":
                        bool foobar7 = (bool)dynVar;
                        break;
                    case "string":
                        string foobar8 = (string)dynVar;
                        break;
                    case "object":
                        object foobar9 = (object)dynVar;
                        break;
                }
            }
            catch (Exception)
            {
                canExplicitCovert = false;
            }

            try
            {
                switch (toType)
                {
                    case "char":
                        char foobar1 = dynVar;
                        break;
                    case "byte":
                        byte foobar2 = dynVar;
                        break;
                    case "int":
                        int foobar3 = dynVar;
                        break;
                    case "float":
                        float foobar4 = dynVar;
                        break;
                    case "double":
                        double foobar5 = dynVar;
                        break;
                    case "decimal":
                        decimal foobar6 = dynVar;
                        break;
                    case "bool":
                        bool foobar7 = dynVar;
                        break;
                    case "string":
                        string foobar8 = dynVar;
                        break;
                    case "object":
                        object foobar9 = dynVar;
                        break;
                }
            }
            catch (Exception)
            {
                canImplicitCovert = false;
            }
            paragraph.Inlines.Add(
                new Run("Неявное преобразование: "));
            paragraph.Inlines.Add(
                new Run(canImplicitCovert ? "возможно" : "невозможно")
                {
                    Foreground = canImplicitCovert ? Brushes.Green : Brushes.Red
                });
            paragraph.Inlines.Add(new LineBreak());
            paragraph.Inlines.Add(
                new Run("Явное преобразование: "));
            paragraph.Inlines.Add(
                new Run(canExplicitCovert ? "возможно" : "невозможно")
                {
                    Foreground = canExplicitCovert ? Brushes.Green : Brushes.Red
                });
        }
    }
}
