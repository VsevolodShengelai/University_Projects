package com.company;

public class Main {
    public static double f(double x){
        return -2*x + 6 +x*x - 4*x*x*x*x;
    }

    public static void use_function(){
        double[] X = new double[] {0,1,2,3,4,5,6};
        double[] Y = new double[X.length];
        double x = -4.5;
        for (int i = 0; i < X.length; i++){
            Y[i] = f(X[i]);
        }

        Lagrange lagrange = new Lagrange();
        double  y = lagrange.GetValue(X,Y,x);
        System.out.println("Интерполяция Лагранжа для X:" + x + " Y:" + y);
        System.out.println("Точное значение X:" + x + " Y:" + f(x));
    }

    public static void use_spreadsheet(){
        double[] X = new double[] {1,2,3,4,5};
        double[] Y = new double[] {1.9, 5.5, 10, 15, 21};
        double x = -0.5;

        Lagrange lagrange = new Lagrange();
        double  y = lagrange.GetValue(X,Y,x);
        System.out.println("Интерполяция Лагранжа. Общий случай. X:" + x + " Y:" + y);
        y = lagrange.GetValue(X,1, Y,x);
        System.out.println("Интерполяция Лагранжа. Равномерное распределение узов. X:" + x + " Y:" + y);
    }


    public static void main(String[] args) {
        use_function();
        //use_spreadsheet();



    }
}
