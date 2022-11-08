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

namespace Game_of_Fifteen
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Game model;

        public MainWindow()
        {
            InitializeComponent();
            model = new Game();
            model.RePaint += Model_RePaint;
            model.Init();
        }

        private void Model_RePaint (object sender, int [,] e)
        {
            //массив массивов
            int[][] map = new int [4][];
            for (int i = 0; i < 4; i++)
            {
                map[i] = new int[4];
                for (int j = 0; j < 4; j++)
                {
                    map[i][j] = model.map[i,j];
                }
                ic.ItemsSource = map;
            }
        }

        private void New_Game_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Scores_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var brd = sender as Border;
            var key = (int)brd.DataContext;
        }
    }


}
