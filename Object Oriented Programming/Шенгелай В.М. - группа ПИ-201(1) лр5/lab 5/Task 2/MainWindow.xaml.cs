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

namespace Task_2
{
    // Enumerations Exercise 1
    /// <summary>
    /// Enumeration of girder material types
    /// </summary>
    public enum Material { StainlessSteel, Aluminium, ReinforcedConcrete, Composite, Titanium }
    /// <summary>47
    /// Enumeration of girder cross-sections
    /// </summary>
    public enum CrossSection { IBeam, Box, ZShaped, CShaped }
    /// <summary>
    /// Enumeration of test results
    /// </summary>
    public enum TestResult { Pass, Fail }
    // Structures Exercise 2
    /// <summary>
    /// Structure containing test results
    /// </summary>
    public struct TestCaseResult
    {
        /// <summary>
        /// Test result (enumeration type)
        /// </summary>
        public TestResult Result;
        /// <summary>
        /// Description of reason for failure
        /// </summary>
        public string ReasonForFailure;
    }



    //Класс генерации значений для объектов TestCaseResult
    //Он симулирует проведение тестов
    public class TestManager
    {
        string[] reasonForFailure_arr = 
        { 
        "Ошибка 1011",
        "Ошибка 1111",
        "Ошибка 0011"
        };

        #region FieldsAndProperties
        private int passCount; //privare можно опустить ,все поля по умолчанию приватные
        private int failCount;

        public int[] arr = new int[10];
        
        // Пользователю нельзя устанавливать поля счётчиков
        public int PassCount
        {
             get
            {
                return passCount;
            }
        }
        public int FailCount
        {
            get
            {
                return failCount;
            }
        }
        #endregion

        public TestCaseResult[] GenerateResult(TestCaseResult[] TestCaseResults_arr)
        {

            Random rand = new Random();
            passCount = 0;
            failCount = 0;

            for (int i = 0; i < TestCaseResults_arr.Length; i++)
            {


                //Присваиваем значение полю Result и полю ReasonForFailure_arr

                int a = rand.Next(0, 21);

                arr[i] = a;

                if (a > 10)
                {
                    TestCaseResults_arr[i].Result = TestResult.Pass;
                    passCount = passCount + 1;
                }
                else if (a < 10)
                {
                    TestCaseResults_arr[i].Result = TestResult.Fail;
                    failCount = failCount + 1;
                    TestCaseResults_arr[i].ReasonForFailure = reasonForFailure_arr[rand.Next(0, 3)];
                }
            }
            
            return TestCaseResults_arr;
        }
    }


    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void calcButton_Click(object sender, RoutedEventArgs e)
        {
            TestCaseResult[] TestCaseResults_arr = new TestCaseResult[10];

            TestManager MyMamager = new TestManager();
            TestCaseResults_arr = MyMamager.GenerateResult(TestCaseResults_arr);
            int passcount = MyMamager.PassCount;
            int failcount = MyMamager.FailCount;

            textBlock.Text = "";

            for (int i = 0; i < TestCaseResults_arr.Length; i++)
            {
                if (TestCaseResults_arr[i].Result == TestResult.Pass)
                {
                    textBlock.Text += "\nТест "+ (i+1) + ". " + "Результат: " + TestCaseResults_arr[i].Result.ToString();
                }
                if (TestCaseResults_arr[i].Result == TestResult.Fail)
                {
                    textBlock.Text += "\nТест " + (i + 1) + ". " + "Результат: " + TestCaseResults_arr[i].Result.ToString()
                        + ", " + TestCaseResults_arr[i].ReasonForFailure;
                }
            }

            textBlock.Text += "\n\n";
            textBlock.Text += "Кол-во успешных попыток: " + passcount;
            textBlock.Text += "\nКол-во неудачных попыток: " + failcount;


        }
    }
}
