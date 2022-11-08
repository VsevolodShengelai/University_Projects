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

namespace task2
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
        public double[,] MatrixMultiply(double[,] one, double[,] two)
        {
            int colOne = one.GetLength(0);
            int rowOne = one.GetLength(1);
            int colTwo = two.GetLength(0);
            int rowTwo = two.GetLength(1);

            if (colOne != rowTwo)
            {
                throw new ArgumentException("First matrix columns should match second matrix rows");
            }

            var result = new double[colTwo, rowOne];

            for (int i = 0; i < rowOne; ++i)
            {
                for (int j = 0; j < colTwo; ++j)
                {
                    double sum = 0;
                    for (int k = 0; k < colOne; ++k)
                    {
                        if (one[k, i] < 0)
                        {
                            throw new ArgumentException($"Matrix1 contains an invalid entry in cell[{k}, {i}].");
                        }
                        if (two[j, k] < 0)
                        {
                            throw new ArgumentException($"Matrix2 contains an invalid entry in cell[{j}, {k}].");
                        }
                        sum += one[k, i] * two[j, k];
                    }
                    result[j, i] = sum;
                }
            }

            return result;
        }
    }
}
