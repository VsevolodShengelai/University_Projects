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

namespace matrix_multiplication
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

        double[,] matrixFirst;
        double[,] matrixSecond;
        double[,] resultMatrix;
        Random random = new();

        private void InitializeGrid(Grid grid, ref double[,] matrix)
        {
            if (grid == null) return;
            // Reset the grid before doing anything
            grid.Children.Clear();
            grid.ColumnDefinitions.Clear();
            grid.RowDefinitions.Clear();
            // Get the dimensions
            int columns = matrix.GetLength(0);
            int rows = matrix.GetLength(1);
            // Add the correct number of coumns to the grid
            for (int x = 0; x < columns; x++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition()
                {
                    Width = new GridLength(1, GridUnitType.Star),
                });
            }
            for (int y = 0; y < rows; y++)
            {
                // GridUnitType.Star - The value is expressed as a weighted proportion of available space
                grid.RowDefinitions.Add(new RowDefinition()
                {
                    Height = new GridLength(1, GridUnitType.Star),
                });
            }
            // Fill each cell of the grid with an editable TextBox containing the value from the matrix
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    double cell = matrix[x, y];
                    ClickSelectTextBox t = new();
                    t.Padding = new Thickness(3, 3, 3, 3);
                    t.Text = cell.ToString();
                    t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                    t.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                    t.SetValue(Grid.RowProperty, y);
                    t.SetValue(Grid.ColumnProperty, x);
                    grid.Children.Add(t);
                }
            }
        }

        private void GetValuesFromGrid(Grid grid, ref double[,] matrix)
        {
            int columns = grid.ColumnDefinitions.Count;
            int rows = grid.RowDefinitions.Count;
            // Iterate over cells in Grid, copying to matrix array
            
            for (int c = 0; c < grid.Children.Count; c++)
            {
                TextBox t = (TextBox)grid.Children[c];
                int row = Grid.GetRow(t);
                int column = Grid.GetColumn(t);
                matrix[column, row] = t.Text == "" ? 0.0 : double.Parse(t.Text);
            }
        }

        private void SetValuesFromMatrix(Grid grid, ref double[,] matrix)
        {
            for (int c = 0; c < grid.Children.Count; c++)
            {
                TextBox t = (TextBox)grid.Children[c];
                int row = Grid.GetRow(t);
                int column = Grid.GetColumn(t);
                t.Text = matrix[column, row].ToString();
            }
        }

        private void InitializeMatrixRandomly(ref double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int x = 0; x < cols; ++x)
            {
                for (int y = 0; y < rows; ++y)
                {
                    matrix[x, y] = Math.Round(random.NextDouble() * 10000) / 100;
                }
            }
        }

        private void buttonCreateFirstMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rows, columns;
            if (!int.TryParse(textboxFirstRow.Text, out rows) || rows <= 0)
            {
                MessageBox.Show("Введдите целое ненулевое число строк",
                    "Неверно введены данные",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            if (!int.TryParse(textboxFirstCol.Text, out columns) || columns <= 0)
            {
                MessageBox.Show("Введдите целое ненулевое число столбцов",
                    "Неверно введены данные",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            matrixFirst = new double[columns, rows];
            InitializeGrid(gridFirst, ref matrixFirst);
        }

        private void buttonCreateSecondMatrix_Click(object sender, RoutedEventArgs e)
        {
            int rows, columns;
            if (!int.TryParse(textboxSecondRow.Text, out rows) && rows <= 0)
            {
                MessageBox.Show("Введдите целое ненулевое число строк",
                    "Неверно введены данные",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            if (!int.TryParse(textboxSecondCol.Text, out columns) && columns <= 0)
            {
                MessageBox.Show("Введдите целое ненулевое число столбцов",
                    "Неверно введены данные",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            matrixSecond = new double[columns, rows];
            InitializeGrid(gridSecond, ref matrixSecond);
        }

        private void buttonMultiply_Click(object sender, RoutedEventArgs e)
        {
            if (matrixFirst == null || matrixSecond == null)
            {
                MessageBox.Show(
                    "Сначала создайте все матрицы",
                    "Невозможно умножить",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            int columnsFirst = matrixFirst.GetLength(0);
            int rowsFirst = matrixFirst.GetLength(1);
            int columnsSecond = matrixSecond.GetLength(0);
            int rowsSecond = matrixSecond.GetLength(1);

            if (columnsFirst != rowsSecond)
            {
                MessageBox.Show(
                    "Столбцы первой матрицы и строки второй матрицы должны быть равны",
                    "Невозможно умножить",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            GetValuesFromGrid(gridFirst, ref matrixFirst);
            GetValuesFromGrid(gridSecond, ref matrixSecond);

            resultMatrix = new double[columnsSecond, rowsFirst];

            for (int i = 0; i < rowsFirst; ++i)
            {
                for (int j = 0; j < columnsSecond; ++j)
                {
                    double sum = 0;
                    for (int k = 0; k < columnsFirst; ++k)
                    {
                        sum += matrixFirst[k, i] * matrixSecond[j, k];
                    }
                    resultMatrix[j, i] = sum;
                }
            }

            InitializeGrid(gridThird, ref resultMatrix);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (matrixFirst == null)
            {
                MessageBox.Show(
                    "Сначала создайте матрицу",
                    "Нечего заполнять",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            InitializeMatrixRandomly(ref matrixFirst);
            SetValuesFromMatrix(gridFirst, ref matrixFirst);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (matrixSecond == null)
            {
                MessageBox.Show(
                    "Сначала создайте матрицу",
                    "Нечего заполнять",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }
            InitializeMatrixRandomly(ref matrixSecond);
            SetValuesFromMatrix(gridSecond, ref matrixSecond);
        }
    }
}