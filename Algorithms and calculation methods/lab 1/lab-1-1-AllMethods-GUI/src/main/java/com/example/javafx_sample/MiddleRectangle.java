package com.example.javafx_sample;

import java.util.Scanner;

public class MiddleRectangle {

    //Интегрируемое выражений
    public static double f (double x) {
        return Math.log(x+Math.sqrt(x*x-0.25))/(2*x*x);
    }

    public static double integral_calc (double a, double b, double N) {

        double result = 0;
        double h = (b - a) / N; //шаг разбиения отрезка [a;b].

        for(int i = 0; i < N; i++)
        {
            result += f(a + h*(i + 0.5)); //Определяем значение yi подынтегральной функции f(x) в каждой части деления.
        }
        result *= h;
        return result;
    }

    public static double[] take_a_result(double a, double b, double eps) {
        long m = System.currentTimeMillis();
        //System.out.println(m);

        int N = 2;
        double previous_I = integral_calc(a,b,N);
        N*=2;
        double current_I = integral_calc(a,b,N);

        //Формула Рунге для метода центральных прямоугольников
        while (true){
            if (Math.abs(current_I-previous_I)/3 >= eps){
                previous_I = current_I;
                N*=2;
                current_I = integral_calc(a,b,N);
            }
            else{
                break;
            }
        }
        //System.out.println(System.currentTimeMillis());
        double time = System.currentTimeMillis() - m;

        return new double[]{current_I, N, time};
    }
}



