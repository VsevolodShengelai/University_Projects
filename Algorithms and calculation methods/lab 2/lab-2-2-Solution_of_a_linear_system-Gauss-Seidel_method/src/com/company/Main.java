package com.company;

//Метод Гаусса-Зейделя

import java.util.concurrent.SynchronousQueue;

public class Main {

    public static double[] findSolution(double[][] matrix, double eps) {

        final int ITERATIONLIMIT = 100000;

        int size = matrix.length;
        double[] previousVariableValues = new double[size];
        for (int i = 0; i < size; i++) {
            previousVariableValues[i] = 0.0;
        }
        // Будем выполнять итерационный процесс до тех пор,
        // пока не будет достигнута необходимая точность
        int counter = 0; //Введём счётчик для подсчёта количества итераций

        //Перед стартом итерационного алгоритма мы сделаем следующее:
        //Проверим матрицу на сходимость
        //Уберём нули с главной диагонали

        //Проверка матрицы на сходимость
        for (int n = 0; n < matrix.length; n++){
            double sum = 0;
            for (int k = 0; k < matrix.length; k++){
                if (k!=n) {      //Если это не диагональный элемент - добавляем к сумме
                    sum += Math.abs(matrix[n][k]);
                }
            }
            if (Math.abs(matrix[n][n]) <= sum){
                System.out.println("Матрица не удовлетворяет достаточному условию сходимости по строке " + n);
                //return previousVariableValues;
            }
        }


        for (int n = 0; n < matrix.length; n++){

            //Уберём нули с главной диагонали (переставим уравнения)
            if (matrix[n][n] == 0) {
                int num_of_normal_equation= -1; //Инициализируем недопустимым значением
                for(int j = n; j < matrix.length; j++){
                    if (matrix[j][n] != 0){
                        num_of_normal_equation = j;
                        break;
                    }
                }
                double[] c = matrix[num_of_normal_equation];
                matrix[num_of_normal_equation] = matrix[n];
                matrix[n] = c;
            }
        }


        while (true) {
            counter++;
            // Введем вектор значений неизвестных на текущем шаге
            double[] currentVariableValues = new double[size];

            // Посчитаем значения неизвестных на текущей итерации
            // в соответствии с теоретическими формулами
            for (int i = 0; i < size; i++) {
                // Инициализируем i-ую неизвестную значением
                // свободного члена i-ой строки матрицы
                currentVariableValues[i] = matrix[i][size];

                // Вычитаем сумму по всем отличным от i-ой неизвестным
                for (int j = 0; j < size; j++) {
                    // При j < i можем использовать уже посчитанные
                    // на этой итерации значения неизвестных
                    if (j < i) {
                        currentVariableValues[i] -= matrix[i][j] * currentVariableValues[j];
                    }

                    // При j > i используем значения с прошлой итерации
                    if (j > i) {
                        currentVariableValues[i] -= matrix[i][j] * previousVariableValues[j];
                    }
                }
                // Делим на коэффициент при i-ой неизвестной
                currentVariableValues[i] /= matrix[i][i];
            }
            // Посчитаем текущую погрешность относительно предыдущей итерации
            double exit_eps = 0.0;
            for (int i = 0; i < size; i++) {
                exit_eps += Math.abs(currentVariableValues[i] - previousVariableValues[i]);
            }
            // Если необходимая точность достигнута, то завершаем процесс
            if (exit_eps < eps) {
                break;
            }
            if (counter > ITERATIONLIMIT){

                System.out.println("Программма достигла предела итераций: " + ITERATIONLIMIT);
                break;
            }
            // Переходим к следующей итерации, так
            // что текущие значения неизвестных
            // становятся значениями на предыдущей итерации
            previousVariableValues = currentVariableValues;
        }

        System.out.println("Значение счётчика итераций:" + counter);
        System.out.println();

        return previousVariableValues;
    }

    public static void main(String[] args) {
        double[][] matrix = {
                { 3, 0, -1, -4 },
                { 2, -5, 1, 9 },
                { 2, -2, 6, 8 }
        };

        double EPS = 10e-4;

        double[] x = findSolution(matrix, EPS);


        for(int i = 0; i < x.length; i++){
            System.out.print("x"+(i+1)+"="+x[i]+"\n");
        };
    }
}
