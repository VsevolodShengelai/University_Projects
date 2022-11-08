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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Поставим на комбобоксы начальные значения
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
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

        }

        private void fillButton_Click(object sender, RoutedEventArgs e)
        {
            // Берём строки и столбцы по индексу в комбобоксе
            // Изменить в будующем
            int cols = comboBox2.SelectedIndex + 1;
            int rows = comboBox1.SelectedIndex + 1;

            double[,] matrix_1_arr = new double[cols, rows];
            Random rand_instance = new Random();

            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    //double - как вариант
                    //NextDouble метод вернёт рандомное число от 0.0 до 1.0
                    //matrix_1_arr[i, j] = rand_instance.NextDouble() * 100;

                    matrix_1_arr[i, j] = rand_instance.Next(1,100);
                }
            }

            initializeGrid(MatrixGrid_4, matrix_1_arr);

            ScrollViewer sv = new ScrollViewer();
            sv.VerticalScrollBarVisibility = ScrollBarVisibility.Visible;
            MatrixGrid_4.Children.Add(sv);
        }
    }
}
