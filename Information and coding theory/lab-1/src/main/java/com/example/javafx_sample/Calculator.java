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
}



