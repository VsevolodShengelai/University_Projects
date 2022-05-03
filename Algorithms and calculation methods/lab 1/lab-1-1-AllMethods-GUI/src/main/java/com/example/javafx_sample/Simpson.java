package com.example.javafx_sample;

import java.util.Scanner;

public class Simpson {

    //Интегрируемое выражений
    public static double f (double x) {
        return Math.log(x+Math.sqrt(x*x-0.25))/(2*x*x);
    }

    public static double integral_calc (double a, double b, double N) {
        double H = (b-a)/N;
        double s1 = 0, s2 = 0; //Суммы на чётных и нечётных i

        for (int i = 0; i < N; i++){
            if (i%2 == 0){ //Если число чётное
                s1 = s1+f(a+i*H);
            }
            else {
                s2 = s2+f(a+i*H);
            }
        }
        double Integral = H/3*(f(a)+f(b)+2*s1+4*s2);
        //System.out.println(Integral);
        return Integral;
    }

    public static double[] take_a_result(double a, double b, double eps) {
        long m = System.currentTimeMillis();
        //System.out.println(m);

        int N = 2;
        double previous_I = integral_calc(a,b,N);
        N*=2;
        double current_I = integral_calc(a,b,N);

        while (true){
            if (Math.abs(current_I-previous_I)/15 >= eps){
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



