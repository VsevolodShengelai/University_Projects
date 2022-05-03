package com.example.lab41lagrangepolinomgui;

public class Lagrange {
    private double l(int index, double[] X, double x)
    {
        double l = 1;
        for (int i = 0; i < X.length; i++)
        {
            if (i != index)
            {
                l *= (x - X[i]) / (X[index] - X[i]);
            }
        }
        return l;
    }

    public double GetValue(double[] X, double[] Y, double x)
    {
        double y = 0;
        for (int i = 0; i < X.length; i++)
        {
            y += Y[i] * l(i, X, x);
        }
        return y;
    }

    private double l(int index, double[] X, double h, double x)
    {
        double l = 1;
        for (int i = 0; i < X.length; i++)
        {
            if (i != index)
            {
                l *= (x - X[0] - i * h) / (index - i);
            }
        }
        return l / Math.pow(h,X.length - 1);
    }

    public double GetValue(double[] X, double h, double[] Y, double x)
    {
        double y = 0;
        for (int i = 0; i < X.length; i++)
        {
            y += Y[i] * l(i,X,h,x);
        }

        return y;
    }

}
