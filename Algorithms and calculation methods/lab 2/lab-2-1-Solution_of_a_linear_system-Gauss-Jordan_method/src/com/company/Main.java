package com.company;
import java.util.Scanner;

public class Main {
    //Метод вывода матрицы на экран
    public static void show_array(double[][] arr) {
        for (int i = 0; i < arr.length; i++) {
            for (int j = 0; j < arr[i].length; j++) {
                System.out.print(arr[i][j] + "\t");
            }
            System.out.print("\n");
        }
    }

    //Метод ввода матрицы (расширенной)
    public static double[][] enter_array() {

        final int MAXVALUE = 35;
        int rows, cols;

        Scanner in = new Scanner(System.in);
        System.out.print("Введите количество строк массива: ");
        rows = in.nextInt();

        System.out.print("Введите количество столбцов массива: ");
        cols = in.nextInt();
        System.out.println();

        double[][] arr = new double[rows][cols];

        in.nextLine(); //Фокусы с символом новой строки
        for (int i = 0; i < arr.length; i++) {
            System.out.print("Строка" + (i+1) + ": ");
            String str = in.nextLine(); //Строка матрицы
            String[] strMas= str.split("[ ]"); //Получили массив без пробелов

            if (strMas.length < cols){
                System.out.println("Строка" + (i+1) + " не соответстует длине сток матрицы ");
                i--;
                continue;
            }

            for (int j = 0; j < arr[i].length; j++){
                arr[i][j] = Double.parseDouble(strMas[j]);
            }
        }
        return arr;
    }

    public static void main(String[] args) {

        double[][] arr = {
                {3,0,-1,-4},
                {2,-5,1,9},
                {2,-2,6 ,8}
        };

        /*double[][] arr = {
                {0, 0, 0, 1, -4, -5},
                {-1, 2, 0, -7, 39, 55},
                {0, 0,-2, 0, -1, 1},
                {1, -2, 0, 3, -17, -23},
                {-2, 4, 0, -1, 16, 25}
        };*/
        //double[][] arr = enter_array(); //Введём матрицу

        //double[][] arr = enter_array(); //Введём матрицу


        // количество проходов = n
        //int n;
        for (int n = 0; n < arr.length; n++){

            //Исправление отравления матрицы(если на главной диагонали стоит 0)

            if (arr[n][n] == 0) {
                int num_of_normal_equation= -1; //Инициализируем недопустимым значением
                for(int j = n; j < arr.length; j++){
                    if (arr[j][n] != 0){
                        num_of_normal_equation = j;
                        break;
                    }
                }
                double[] c = arr[num_of_normal_equation];
                arr[num_of_normal_equation] = arr[n];
                arr[n] = c;
            }

            /*
            Шаг 1. Выбираем главный элемент среди элементов n-го столбца(начинаеим с первого)
            */
            double max_value = arr[n][n];
            int max_position = n; //Номер строки главного элемента

            for(int i = n; i < arr.length; i++){ //Сколько строк, столько и элементов
                if (arr[i][n] > max_value & arr[i][n] != 0){
                    max_value = arr[i][n];
                    max_position = i;
                }
            }
            //Выполним перестановку строк матрицы
            double[] c = arr[max_position];
            arr[max_position] = arr[n];
            arr[n] = c;

            //System.out.println(max_value);

            System.out.println();
            show_array(arr);

            //Шаг 2. Нормировка первого уравнения
            double first_coeff = arr[n][n];
            for(int i = n; i < arr[0].length; i++)
            {
                arr[n][i] = arr[n][i]/first_coeff;
            }
            System.out.println();
            show_array(arr);

            //Шаг 3. Исключение элементов первого столбца
            /*
            Будем обнулять элементы ниже текущего уравнения с 1-цей
            Какое уравнение текущее? Такое же, как и номер столбца
            */

            //int i = n, где n номер прохода по алгоритму
            //Начинаем "обнулять" уравнения с последнего
            //Можно начинать и сверху, без разницы
            for (int i = arr.length-1; i != n; i--){
                double multipler = arr[i][n];

                for (int h = 0; h < arr[0].length; h++)
                {
                    //Умножаем элементы уравнения на число и производим сложение
                    arr[i][h] += -arr[n][h] * multipler;
                }
            }
            System.out.println();
            show_array(arr);
        }

        //Получили треугольную матрицу. Выполняем обратный ход
        double[] answers = new double[arr.length];
        answers[arr.length-1] = arr[arr.length-1][arr[0].length-1]; //Поместим последний икс в массив корней
        int counter = 1;  //Счётчик подстановок

        for(int i = arr.length-2; i > -1; i--){ //Проходимся по уравнениям c низу доверху
            for(int j= 0; j < counter; j++) { //Проходимся по количеству подстановок от 0 до n-1, n - число уравнений
                arr[i][arr[i].length - 1] -=
                        arr[i][arr[i].length-1-(j+1)]*answers[answers.length-1-j];

            }
            counter++;
            answers[i] = arr[i][arr[i].length - 1];
        }

        for(int i = 0; i < arr.length; i++){
            if (i == arr.length-1){
                System.out.print("x"+(i+1)+"="+answers[i]);}
            else{
                System.out.print("x"+(i+1)+"="+answers[i]+",\n");
                }
        }
    }
}
