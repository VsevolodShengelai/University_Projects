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

namespace Task_3
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    // класс с методами расширения
    static class MatrixExt
    {
        // метод расширения для получения количества строк матрицы
        public static int RowsCount(this double[,] matrix)
        {
            return matrix.GetLength(1);
        }
        // метод расширения для получения количества столбцов матрицы
        public static int ColumnsCount(this double[,] matrix)
        {
            return matrix.GetLength(0);
        }
    }


    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Поставим на комбобоксы начальные значения
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void initializeGrid(Grid grid, double[,] matrix)
        {
            if (grid != null)
            {
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
                        Width = new
                   GridLength(1, GridUnitType.Star),
                    });
                }
                for (int y = 0; y < rows; y++)
                {
                    // GridUnitType.Star - The value is expressed as a weighted proportion of available space
                    grid.RowDefinitions.Add(new RowDefinition()
                    {
                        Height = new
                   GridLength(1, GridUnitType.Star),
                    });
                }
                // Fill each cell of the grid with an editable TextBox containing the value from the matrix 
                for (int x = 0; x < columns; x++)
                {
                    for (int y = 0; y < rows; y++)
                    {
                        double cell = (double)matrix[x, y];
                        TextBox t = new TextBox();
                        t.Text = cell.ToString();
                        t.VerticalAlignment = System.Windows.VerticalAlignment.Center;
                        t.HorizontalAlignment = System.Windows.HorizontalAlignment.Center;
                        t.SetValue(Grid.RowProperty, y);
                        t.SetValue(Grid.ColumnProperty, x);
                        grid.Children.Add(t);
                    }
                }
            }
        }


        static double[,] MatrixMultiplication(double[,] matrixA, double[,] matrixB)
        {
            
            if (matrixA.ColumnsCount() != matrixB.RowsCount())
            {
                throw new ArgumentException("Умножение не возможно! Количество столбцов первой матрицы не равно количеству строк второй матрицы.");
            }
            
            double[,] matrixC = new double[matrixB.ColumnsCount(), matrixA.RowsCount()];

            for (var i = 0; i < matrixB.ColumnsCount(); i++)
            {
                for (var j = 0; j < matrixA.RowsCount(); j++)
                {
                    matrixC[i, j] = 0;

                    for (int k = 0; k < matrixA.ColumnsCount(); k++)
                    {
                        //Считаем, что [x,y] - [строка, столбец]
                        //Отсчёт начинаем с [1,1]
                        if (matrixA[k, j] <= 0)
                        {
                            throw new ArgumentException("Matrix1 contains an invalid entry in cell"+"["+ (j+1) +"," + (k+1) + "].");
                        }
                        else if (matrixB[i, k] <= 0)
                        {
                            throw new ArgumentException("Matrix2 contains an invalid entry in cell" + "[" + (k+1) + "," + (i+1) + "].");
                        }
                        matrixC[i, j] += matrixA[k, j] * matrixB[i, k];
                    }
                }
            }

            return matrixC;
        }

        private void getValuesFromGrid(Grid grid, double[,] matrix)
        {
            int columns = grid.ColumnDefinitions.Count;
            int rows = grid.RowDefinitions.Count;
            // Iterate over cells in Grid, copying to matrix array
            for (int c = 0; c < grid.Children.Count; c++)
            {
                TextBox t = (TextBox)grid.Children[c];
                int row = Grid.GetRow(t);
                int column = Grid.GetColumn(t);
                matrix[column, row] = double.Parse(t.Text);
            }
        }



        private void calculateButton_Click(object sender, RoutedEventArgs e)
        {
            //Проверяем данные в первой таблице
            var tuple_1 = takeTheMatrixSize(comboBox1.Text, comboBox2.Text);
            int rows_1 = tuple_1.Item1;
            int cols_1 = tuple_1.Item2;
            
            double[,] matrix_1_arr = new double[cols_1, rows_1];
            try
            {
                getValuesFromGrid(MatrixGrid_1, matrix_1_arr);
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно прочитать данные в матрице 1");
                return;
            }

            var tuple_2 = takeTheMatrixSize(comboBox3.Text, comboBox4.Text);
            int rows_2 = tuple_2.Item1;
            int cols_2 = tuple_2.Item2;
            
            double[,] matrix_2_arr = new double[cols_2, rows_2];
            try
            {
                getValuesFromGrid(MatrixGrid_2, matrix_2_arr);
            }
            catch (Exception)
            {
                MessageBox.Show("Невозможно прочитать данные в матрице 2");
                return;
            }

            double[,] matrix_rez = new double[rows_1, cols_2];

            try
            {
                matrix_rez = MatrixMultiplication(matrix_1_arr, matrix_2_arr);
                initializeGrid(MatrixGrid_3, matrix_rez);
            }
            catch (ArgumentException M)
            {
                MessageBox.Show(M.Message, "Сообщение", MessageBoxButton.OK,
                MessageBoxImage.Information);
                return;
            }

        }

        //Берём размеры матриц
        public Tuple<int, int> takeTheMatrixSize(string comboStringRows, string comboStringCols)
        {
            string cols_str = comboStringCols;
            string rows_str = comboStringRows;

            cols_str = cols_str.Substring(0, cols_str.IndexOf(' '));
            rows_str = rows_str.Substring(0, rows_str.IndexOf(' '));

            // Инициализируем пременные нулём.
            // В дальнейшем присвоим им другое значение
            int cols = 0;
            int rows = 0;

            bool result_1 = int.TryParse(cols_str, out cols);
            bool result_2 = int.TryParse(rows_str, out rows);

            if (result_1 & result_2)
            {
                // Проверка матриц на раазмер.
                // Можно организовать и в методе умножения, но она нужна уже здесь
                // Мы обязательно должны вернуть размер матрицы. Поэтому создадим исключение
                if (cols <= 0 | rows <= 0)
                {
                    MessageBox.Show("Указан недопустимый размер матриц", "Сообщение", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                    throw new Exception("Invalid matrix size specified: <= 0");
                }
            }
            else
            {
                MessageBox.Show("Размер матриц нужно указывать числами", "Сообщение", MessageBoxButton.OK,
                MessageBoxImage.Information);
                throw new Exception("Invalid matrix size specified: not int");
            }
            return Tuple.Create(rows, cols);
        }

        private void fillButton_Click(object sender, RoutedEventArgs e)
        {
            //Первая матрица
            var tuple_1 = takeTheMatrixSize(comboBox1.Text, comboBox2.Text);
            int rows = tuple_1.Item1;
            int cols = tuple_1.Item2;

            double[,] matrix_1_arr = new double[cols, rows];
            Random rand_instance = new Random();

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    //double - как вариант
                    //NextDouble метод вернёт рандомное число от 0.0 до 1.0
                    //matrix_1_arr[i, j] = rand_instance.NextDouble() * 100;

                    matrix_1_arr[i, j] = rand_instance.Next(-1, 100);
                }
            }

            initializeGrid(MatrixGrid_1, matrix_1_arr);

            // Вторая матрица
            var tuple_2 = takeTheMatrixSize(comboBox3.Text, comboBox4.Text);
            rows = tuple_2.Item1;
            cols = tuple_2.Item2;

                
            double[,] matrix_2_arr = new double[cols, rows];

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    //double - как вариант
                    //NextDouble метод вернёт рандомное число от 0.0 до 1.0
                    //matrix_1_arr[i, j] = rand_instance.NextDouble() * 100;

                    matrix_2_arr[i, j] = rand_instance.Next(-1, 100);
                }
            }

            initializeGrid(MatrixGrid_2, matrix_2_arr);
        }
    }
}
