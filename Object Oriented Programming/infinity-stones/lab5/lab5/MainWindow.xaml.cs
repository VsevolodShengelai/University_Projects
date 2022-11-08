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

namespace lab5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            materials.ItemsSource = Enum.GetNames<Material>();
            crosssections.ItemsSource = Enum.GetNames<CrossSection>();
            testresults.ItemsSource = Enum.GetNames<TestResult>();
        }
        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (materials.SelectedItem == null
                || crosssections.SelectedItem == null
                || testresults.SelectedItem == null)
            {
                return;
            }

            StringBuilder selectionStringBuilder = new();

            Material selectedMaterial = Enum.Parse<Material>((string) materials.SelectedItem);
            CrossSection selectedCrossSection = Enum.Parse<CrossSection>((string) crosssections.SelectedItem);
            TestResult selectedTestResult = Enum.Parse<TestResult>((string) testresults.SelectedItem);

            selectionStringBuilder.Append("Material: ");

            switch (selectedMaterial)
            {
                case Material.StainlessSteel:
                    selectionStringBuilder.Append("Stainless Steel");
                    break;
                case Material.Aluminium:
                    selectionStringBuilder.Append("Aluminium");
                    break;
                case Material.ReinforcedConcrete:
                    selectionStringBuilder.Append("Reinforced Concrete");
                    break;
                case Material.Composite:
                    selectionStringBuilder.Append("Composite");
                    break;
                case Material.Titanium:
                    selectionStringBuilder.Append("Titanium");
                    break;
            }

            selectionStringBuilder.Append(", Cross-section: ");

            switch (selectedCrossSection)
            {
                case CrossSection.IBeam:
                    selectionStringBuilder.Append("I-Beam");
                    break;
                case CrossSection.Box:
                    selectionStringBuilder.Append("Box");
                    break;
                case CrossSection.ZShaped:
                    selectionStringBuilder.Append("Z-Shaped");
                    break;
                case CrossSection.CShaped:
                    selectionStringBuilder.Append("C-Shaped");
                    break;
            }

            selectionStringBuilder.Append(", Result: ");

            switch (selectedTestResult)
            {
                case TestResult.Pass:
                    selectionStringBuilder.Append("Pass");
                    break;
                case TestResult.Fail:
                    selectionStringBuilder.Append("Fail");
                    break;
            }

            outputLabel.Content = selectionStringBuilder.ToString();
        }
    }
}
