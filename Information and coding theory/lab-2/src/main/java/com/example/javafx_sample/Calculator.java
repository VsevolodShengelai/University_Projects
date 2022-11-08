package com.example.javafx_sample;

public class Calculator {

    //Генерируем случайные числа, сумма которых равна числу m
    public static double[] getRandDistArray(int n, double m)
    {
        double randArray[] = new double[n];
        double sum = 0;

        // Generate n random numbers
        for (int i = 0; i < randArray.length; i++)
        {
            randArray[i] = Math.random();
            sum += randArray[i];
        }

        // Normalize sum to m
        for (int i = 0; i < randArray.length; i++)
        {
            randArray[i] /= sum;
            randArray[i] *= m;
        }
        return randArray;
    }

    //Находим количество информации для всей совокупности дискретных случайных сообщений
    public static double getI_x(double[] p_array){
        double I_x = 0;
        for (int i = 0; i < p_array.length; i++) {
            I_x = I_x + p_array[i]*Math.log( p_array[i]) / Math.log(2);
        }
        return -I_x;
    }

    //Находим максимальную энтропию
    public static double getH_max(double N){
        double H_max = Math.log(N) / Math.log(2);
        return H_max;
    }

    ///
    //Ниже идёт код, который был написан для ЛР 2. Код выше наследуется от ЛР1
    ///

    public static double[][] getMatrix(int N, double P_x){
        double[][] matrix = new double[N][N];
        //Матрицу мы заполняем не по строкам, а по столбцам - так удобно генерировать вероятности ц
        for (int j = 0; j < matrix.length; j++) {
            matrix[j][j] = (Math.random() * (1-P_x)) + P_x;//Число из диапазона [0,7 ; 1)
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
    public static double roundNum(double n){
        double value = n;
        String formatResult = String.format("%.6f",value);
        String newString =  formatResult.replace(",", ".");
        double roundednum = Double.parseDouble(newString);
        return roundednum;
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

        //System.out.println(main_sum);

        return main_sum;
    }
}



