package com.example.lab122montecarlo;

import java.math.BigDecimal;
import java.math.RoundingMode;

public class MonteCarlo {
    public static double f (double x) {
        return Math.log(x+Math.sqrt(x*x-0.25))/(2*x*x);
    }

    public static double integral_calc (double a, double b, int M) {
        //График функции должен быть чётким, поэтому у него будет свой набор координат
        //Тысячи для хорошей картинки будет досстататочно
        int N = 100; //Количество точек для построения графика функции
        double h = (b-a)/N;

        double[] xi = new double[N];
        double[] yi = new double[N];
        double[] fi = new double[N];

        double maxyi = 0; //Для определения максимального y - чтобы найти S квадрата
        for (int i = 0; i < N; i++){
            xi[i] = a+h*i;
            fi[i] = f(xi[i]);
            if (Math.abs(fi[i]) > maxyi) maxyi = Math.abs(fi[i]);
        }


        //Теперь разбрасывем точки

        N = M; //количество точек
        double c = -maxyi;
        double d = maxyi;
        double S = (d-c)*(b-a);
        //Начинаем разброс
        xi = new double[N];
        yi = new double[N];
        fi = new double[N];
        int pos_point_counter = 0;
        int neg_point_counter = 0;
        int bad_point_counter = 0;

        for(int i = 0; i < N; i++){
            xi[i] = a+Math.random()*(b-a);
            yi[i] = c+Math.random()*(d-c);
            //System.out.println( xi[i] + "    "+ yi[i]);
            fi[i] = f(xi[i]);
            if (yi[i] < 0){//точка ниже y=0
                if (fi[i] < 0 & yi[i]> fi[i]){
                    //Попала
                    neg_point_counter++;
                }
                else{
                    //Не попала
                    bad_point_counter++;
                }
            }
            if(yi[i] > 0){ //точка выше y=0
                if (fi[i] > 0 & yi[i] < fi[i]){
                    pos_point_counter++;
                    //Попала
                }
                else{
                    bad_point_counter++;
                    //Не попала
                }
            }
        }

        double integral = S*(pos_point_counter)/(pos_point_counter+neg_point_counter+bad_point_counter);
        //Вычтем отрицательные площади
        integral -= S*(neg_point_counter)/(pos_point_counter+neg_point_counter+bad_point_counter);
        //System.out.println(pos_point_counter);
        //System.out.println(neg_point_counter);
        //System.out.println(bad_point_counter);
        //System.out.println(S);
        //System.out.println(integral);

        return integral;
    }

    public static double[] take_a_result(double a, double b, double eps, int N, int series) {
        long m = System.currentTimeMillis();
        //System.out.println(m);

        double[] integrals = new double[series];

        for(int i = 0; i < series; i++){
            integrals[i] = integral_calc(a, b, N);
        }

        //Погрешность метода вычисляется в виже стандартного отклонения средних
        double accuracy = 0;

        double SumS = 0;
        double SumSSquare = 0;

        for(int i = 0; i < series; i++){
            SumS+=integrals[i];
            SumSSquare+=integrals[i]*integrals[i];
        }
        SumS/=series;
        SumSSquare/=series;

        accuracy = Math.abs(SumSSquare - SumS*SumS);

        double integral_answer =0;

        for(int i = 0; i < series; i++){
            integral_answer += integrals[i];
        }
        integral_answer/=series;

        //System.out.println(System.currentTimeMillis());
        double time = System.currentTimeMillis() - m;

        double accuracy_round = round(accuracy,8);

        return new double[]{integral_answer, N, time, accuracy_round};
    }

    //Метод для округления числа
    private static double round(double value, int places) {
        if (places < 0) throw new IllegalArgumentException();

        BigDecimal bd = new BigDecimal(Double.toString(value));
        bd = bd.setScale(places, RoundingMode.HALF_UP);
        return bd.doubleValue();
    }

    public static double[] take_a_result_accurately(double a, double b, double eps, int N, int series) {
        long m = System.currentTimeMillis();
        //System.out.println(m);

        double[] integrals = new double[series];

        for(int i = 0; i < series; i++){
            integrals[i] = integral_calc(a, b, N);
        }

        //Погрешность метода вычисляется в виже стандартного отклонения средних
        double accuracy = 0;

        while (true) {
            double SumS = 0;
            double SumSSquare = 0;

            for(int i = 0; i < series; i++){
                SumS+=integrals[i];
                SumSSquare+=integrals[i]*integrals[i];
            }
            SumS/=series;
            SumSSquare/=series;

            accuracy = Math.abs(SumSSquare - SumS*SumS);
            System.out.println(accuracy);

            if (accuracy >= eps) {
                N *= 2;
                integrals = new double[series];
                for(int i = 0; i < series; i++){
                    integrals[i] = integral_calc(a, b, N);
                }
            }
            else {
                break;
            }
        }
        double integral_answer =0;

        for(int i = 0; i < series; i++){
            integral_answer += integrals[i];
        }
        integral_answer/=series;

        //System.out.println(System.currentTimeMillis());
        double time = System.currentTimeMillis() - m;

        double accuracy_round = round(accuracy,8);

        return new double[]{integral_answer, N, time, accuracy_round};
    }
}
