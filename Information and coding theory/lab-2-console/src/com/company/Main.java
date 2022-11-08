package com.company;

public class Main {

    public static void main(String[] args) {
	    double[][] matrix = getMatrix(5);

        //Вывод матрицы переходов: отладка
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix.length; j++) {
                System.out.print(matrix[i][j] + " ");
            }
            System.out.println("");
        }

        //Подсчёт суммы по столбцам - проверка: отладка
        for (int j = 0; j < matrix.length; j++) {
            double sum = 0;
            for (int i = 0; i < matrix.length; i++) {
                sum = sum + matrix[i][j];
            }
            System.out.print(sum + "  ");
        }

        double[] p_x_array = getRandDistArray(5, 1);

        double I_x_slash_y = getEntropy(matrix, p_x_array);
        double I_x = getI_x(p_x_array);
        double I_x_comma_y = I_x - I_x_slash_y;

        System.out.println("\n");
        System.out.println("I_x= " + String.format("%.8f",I_x));
        System.out.println("I_x_slash_y= " + String.format("%.8f",I_x_slash_y));
        System.out.println("I_x_comma_y= " + String.format("%.8f",I_x_comma_y));

        double summ = 0;
        double[] thousand_array = new double[1000];
        for (int i = 0; i < thousand_array.length; i++) {
            summ += iteration();
        }
        summ = summ/1000;
        System.out.println("Summ= " + String.format("%.8f",summ));
    }

    public static double iteration() {
        double[][] matrix = getMatrix(36);

        double[] p_x_array = getRandDistArray(36, 1);

        double I_x_slash_y = getEntropy(matrix, p_x_array);
        double I_x = getI_x(p_x_array);
        double I_x_comma_y = I_x - I_x_slash_y;

        System.out.println("\n");
        System.out.println("I_x= " + String.format("%.8f",I_x));
        System.out.println("I_x_slash_y= " + String.format("%.8f",I_x_slash_y));
        System.out.println("I_x_comma_y= " + String.format("%.8f",I_x_comma_y));

        return I_x_comma_y;
    }

    public static double[][] getMatrix(int N){
        double[][] matrix = new double[N][N];
        //Матрицу мы заполняем не по строкам, а по столбцам - так удобно генерировать вероятности (сумма в столбце должна быть 1)
        for (int j = 0; j < matrix.length; j++) {

            matrix[j][j] = (Math.random() * (1-0.9)) + 0.9;//Число из диапазона [0,7 ; 1)
            matrix[j][j] = roundNum(matrix[j][j]); //Округлим данное число

            //System.out.println(matrix[j][j]);//Отладочный вывод вероятностей перехода xi в yi
            //System.out.println("");

            //Найдём остаток и сгенерируем остальные элементы
            double rest = 1- matrix[j][j];
            double[] rest_array = getRandDistArray(N-1, rest);

            int counter = 0;
            for (int i = 0; i < matrix.length; i++) {
                if (i!=j){
                    matrix[i][j] = rest_array[counter];
                    counter++;
                }
            }
        }

        return matrix;
    }

    public static double getEntropy(double[][] matrix, double[] p_x_array){



        //Считаем I(x/y)
        double main_sum = 0;
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix.length; j++) {

                double P_y = 0;
                for (int k = 0; k < matrix.length; k++) {
                    P_y += (p_x_array[k]*matrix[k][j]);
                }

                double Px_y = P_y*matrix[i][j];

                main_sum += Px_y * Math.log(matrix[i][j]) / Math.log(2);
            }
        }
        main_sum = - main_sum;

        return main_sum;
    }

    public static double[] getRandDistArray(int n, double m)
    {
        double randArray[] = new double[n];
        double sum = 0;

        // Generate n random numbers
        for (int i = 0; i < randArray.length; i++)
        {
            randArray[i] = Math.random();
            //randArray[i] = roundNum(randArray[i]);//Округляем числа до 6-го знака после запятой
            sum += randArray[i];
        }

        // Normalize sum to m
        for (int i = 0; i < randArray.length; i++)
        {
            randArray[i] /= sum;
            randArray[i] *= m;
            //System.out.println( randArray[i]);
        }
        return randArray;
    }

    public static double roundNum(double n){
        double value = n;
        String formatResult = String.format("%.6f",value);
        String newString =  formatResult.replace(",", ".");
        double roundednum = Double.parseDouble(newString);
        return roundednum;
    }

    //Находим количество информации для всей совокупности дискретных случайных сообщений
    public static double getI_x(double[] p_array){
        double I_x = 0;
        for (int i = 0; i < p_array.length; i++) {
            I_x = I_x + p_array[i]*Math.log( p_array[i]) / Math.log(2);
        }
        return -I_x;
    }
}
