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
        /// <summary>
        /// Enumeration of girder material types
        /// </summary>
        public enum Material
        {
            StainlessSteel,
            Aluminium,
            ReinforcedConcrete,
            Composite,
            Titanium
        }
        /// <summary>
        /// Enumeration of girder cross-sections
        /// </summary>
        public enum CrossSection
        {
            IBeam,
            Box,
            ZShaped,
            CShaped
        }
        /// <summary>
        /// Enumeration of test results
        /// </summary>
        public enum TestResult
        {
            Pass,
            Fail
        }

        public MainWindow()
        {
            InitializeComponent();

            //Загружаем наши  enum`ы в листбоксы

            materialListBox.ItemsSource = Enum.GetValues(typeof(Material)).Cast<Material>();
            crossSectionListBox.ItemsSource = Enum.GetValues(typeof(CrossSection)).Cast<CrossSection>();
            testResultListBox.ItemsSource = Enum.GetValues(typeof(TestResult)).Cast<TestResult>();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Можно организовать и через try/catch, но здесь попробуем через if,
            // т.к. ListBox всё же возвращает значение 
            if (materialListBox.SelectedItem == null | crossSectionListBox == null |
                testResultListBox.SelectedItem == null)
            {
                MessageBox.Show("Вы указали не все элементы для проведения теста", "Сообщение",
                MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            Material selectedMaterial = (Material)materialListBox.SelectedItem;
            CrossSection selectedCrossSection = (CrossSection)crossSectionListBox.SelectedItem;
            TestResult selectedTestResult = (TestResult)testResultListBox.SelectedItem;

            StringBuilder selectionStringBuilder = new StringBuilder();

            selectionStringBuilder.Insert(selectionStringBuilder.Length, "Material: ");
            selectionStringBuilder.Append(selectedMaterial.ToString());


            selectionStringBuilder.Insert(selectionStringBuilder.Length, ", CrossSection: ");
            selectionStringBuilder.Append(selectedCrossSection.ToString());

            selectionStringBuilder.Insert(selectionStringBuilder.Length, ", CrossSection: ");
            selectionStringBuilder.Append(selectedTestResult.ToString());


            resultLabel.Content = selectionStringBuilder;

        }
    }
}
