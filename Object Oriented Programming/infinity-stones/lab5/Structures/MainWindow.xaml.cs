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

namespace Structures
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private TestCaseResult[] results;
        private static Random rng = new();
        private static readonly string[] reasonsForFailure = {
            "Cross-section is too weak",
            "Material is too weak",
            "Material is too stift",
            "Cross-section is to stift"
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private static TestCaseResult GenerateResult()
        {
            var s = new TestCaseResult();
            if (rng.NextDouble() <= 1.0 / 3.0)
            {
                s.Result = TestResult.Fail;
                s.ReasonForFailure = reasonsForFailure[rng.Next(reasonsForFailure.Length)];
            }
            else
            {
                s.Result = TestResult.Pass;
                s.ReasonForFailure = null;
            }
            return s;
        }

        private void RunTests_Click(object sender, RoutedEventArgs e)
        {
            reasonsList.Items.Clear();

            results = new TestCaseResult[10];

            int passCounter = 0;
            int failCounter = 0;

            for (int i = 0; i < results.Length; ++i)
            {
                TestCaseResult result = GenerateResult();
                results[i] = result;

                if (result.Result == TestResult.Fail)
                {
                    reasonsList.Items.Add(result.ReasonForFailure);
                    failCounter++;
                }
                else if (result.Result == TestResult.Pass)
                {
                    passCounter++;
                }
            }

            passLabel.Content = $"Successes: {passCounter}";
            failLabel.Content = $"Failures: {failCounter}";
        }
    }
}
