package com.company;

import java.util.Arrays;

public class Main {

    //БУДЕМ ИНВЕРТИРОВАТЬ МАССИВ - ПРОСТО ТАК БЫСТРЕЕ

    private static final String ANSI_PURPLE = "\u001B[35m";
    private static final String ANSI_RESET = "\u001B[0m";

    public static void main(String[] args) {

        //Коды для подсвечивания строк
        String ANSI_RED = "\u001B[31m";
        String ANSI_GREEN = "\u001B[32m";

        int N = 3; // Вводимый параметр N
        System.out.println("n= " + N);
        int k = 1;
        while (true) {
            if (Math.pow(2, N) <= Math.pow(2, k) / (k + 1)) {
                System.out.println("k= " + k);
                break;
            }
            k++;
        }

        int p = k - N;
        double sigma = 1;
        double d_min = 2 * sigma + 1;
        System.out.println("p= " + p);
        System.out.println("d_min= " + d_min);

        //Сгенерируем последовательность (её информационные биты)
        int[] bytearray;
        //bytearray = generateArray(k);
        bytearray = generatePrerecordedArray(k);

        System.out.println("Найдём значения проверочных бит");
        int[] inf_array = new int[N + p];
        for (int i = 0; i < N; i++) {
            if (bytearray[i] == 1) {
                inf_array[i + p] = 1;
            }
        }
        System.out.println(Arrays.toString(inf_array));
        int syndrom = getSyndrom(inf_array, p);

        System.out.println("Проверим правильность проверочных бит (синдром = 0)");
        //Запишем проверочные биты
        StringBuilder bin = new StringBuilder(Integer.toBinaryString((byte)syndrom));
        while (bin.length() < p){
            bin.insert(0, "0");
        }
        for (int j = 0; j < bin.length(); j++) {
            bytearray[N+j] = bin.charAt(j) - '0';
        }

        int[] bytearray_current = new int[bytearray.length];
        for (int i = 0; i < bytearray.length; i++) {
            bytearray_current[i] = bytearray[i];
        }
        sort(bytearray_current);
        syndrom = getSyndrom(bytearray_current, p);
        System.out.println("Проверочный синдром:" + syndrom);

        System.out.println("Провернём циклический код на 1 символ");
        for (int i = 0; i < bytearray.length; i++) {
            bytearray_current[i] = bytearray[i];
        }
        moveRight(bytearray_current, 2);
        sort(bytearray_current);
        syndrom = getSyndrom(bytearray_current, p);
        System.out.println("Проверочный синдром:" + syndrom);


        System.out.println("Допустим ошибки во ВСЕХ битах");
        //Допускаем ошибки во ВСЕХ битах по очереди и выписываем синдромы
        for (int i = 0; i < bytearray.length; i++) {
            int[] error_array = new int[bytearray.length];
            for (int j = 0; j < bytearray.length; j++) {
                error_array[j] = bytearray [j];
            }
            if (error_array[i] == 1){
                error_array[i] = 0;
            }
            else{
                error_array[i] = 1;
            }

            sort(error_array);
            syndrom = getSyndrom(error_array, p);
            System.out.println("Синдром бита " + i + ":" + syndrom);
        }


    }

    public static int getSyndrom(int[] inf_array, int p){
        //Запишием массив неприводимого полинома
        //ЕСЛИ СО СТРОКАМИ ВАРИАНТ ПОЛУЧИТСЯ - записываем здесь сразу строки
        int [] divider_polinom = new int[0]; //Не проинициализировать не получится
        if (p == 3) {
            divider_polinom = new int[]{1, 0, 1, 1};
        }

        //НАЧИНАЕМ ПРОЦЕСС ДЕЛЕНИЯ В СТОЛБИК
        //Делимый полином в строку
        StringBuilder dividend = new StringBuilder();
        for (int i = 0; i < inf_array.length ; i++){
            //Записываем непосредственно биты полинома
            dividend.insert(0, inf_array[i]);
        }
        System.out.println("dividend: " + dividend);


        //Неприводимый полином в строку //Записываем в обратном порядке
        StringBuilder divider = new StringBuilder();
        for (int i = 0; i < divider_polinom.length ; i++){
            //Записываем непосредственно биты полинома
            divider.insert(divider.length(), divider_polinom[i]);
        }

        System.out.println(divider);

        //Добирает нулей в неприводимом полиноме
        while (divider.length() < inf_array.length){
            divider.insert(divider.length(), "0");
        }

        System.out.println("divider: " + divider);

        //Выволним вычитание (в десятичной системе счисления)

        ///Уменьшаемое
        int dividend_decimal = Integer.parseInt(String.valueOf(dividend), 2);
        System.out.println(dividend_decimal);
        ///Вычитаемое
        int divider_decimal = Integer.parseInt(String.valueOf(divider), 2);
        System.out.println(divider_decimal);

        int result = (byte) dividend_decimal ^ (byte) divider_decimal;

        System.out.println(result);

        divider = new StringBuilder();

        int enumerator = 0;
        //Пока страршая степень делимого больше или равна степени делителя
        while (Integer.toBinaryString((byte)dividend_decimal).length()
                >= divider_polinom.length){
            System.out.println("===Проход" + (enumerator+1) + "===");
            enumerator++;

            //Добавим нулей делителю

            for (int i = 0; i < divider_polinom.length ; i++){
                //Записываем непосредственно биты полинома
                divider.insert(divider.length(), divider_polinom[i]);
            }
            while (divider.length() < dividend.length()){
                divider.insert(divider.length(), "0");
            }

            ///Уменьшаемое
            dividend_decimal = Integer.parseInt(String.valueOf(dividend), 2);
            System.out.println(dividend_decimal);
            ///Вычитаемое
            divider_decimal = Integer.parseInt(String.valueOf(divider), 2);
            System.out.println(divider_decimal);

            result = (byte) dividend_decimal ^ (byte) divider_decimal;
            System.out.println("result: " + result);


            dividend_decimal = result;
            dividend = new StringBuilder(Integer.toBinaryString((byte) dividend_decimal));
            divider = new StringBuilder();
        }

        return result;
    }

    public static double[] generateArray(int k){
        double[] bytearray  = new double[k];

        //счётчик i отсчитывает с 0, реальные номера битов начинаются с 1

        for (int i = 0; i < bytearray.length; i++) {

            if (Math.random() < 0.5){
                bytearray[i] = 1;
            }
            else {
                bytearray[i] = 0;
            }
            //System.out.println("i" + i + ": " + bytearray[i]);

        }
        System.out.println("=========");
        return bytearray;
    }

    public static int[] generatePrerecordedArray(int k){
        return new int[]{1, 1, 1, 0, 0, 0};
    }

    public static double[] getRandomError(double[] bytearray){
        int n = (int)Math.floor(Math.random() * bytearray.length);
        if (bytearray[n] == 1){
            bytearray[n] = 0;
        }
        else{
            bytearray[n] = 1;
        }
        return bytearray;
    }

    public static int defineError(double[] bytearray, double[][] checkMatrix){
        double[] syndrom_array = new double[checkMatrix.length];
        for (int i = 0; i < checkMatrix.length; i++) { //Цикл количества уравнений
            int counter = 0;
            for (int j = 0; j < bytearray.length; j++) {
                if (checkMatrix[i][j] == bytearray[j] &&
                        bytearray[j] == 1){
                    counter++;
                }
            }
            if (counter%2 == 1){
                syndrom_array[i] = 1;
            }
            else{ //else можно не писать, т.к иначе нули
                syndrom_array[i] = 0;
            }
        }

        System.out.print("Синдром:" + ANSI_PURPLE);
        for (int i = 0; i < syndrom_array.length; i++) {
            System.out.print((int)syndrom_array[i]);
        }
        System.out.println(ANSI_RESET);

        int errorBitNumber = -1;
        //Определим номер бита с ошибкой
        for (int i = 0; i < checkMatrix[0].length; i++) {//Цикл по столбцам матрицы
            boolean equal = true;
            for (int j = 0; j < checkMatrix.length; j++) {//Цикл по строкам  матрицы
                if (checkMatrix[j][i] != syndrom_array[j]){
                    equal = false;
                }
            }
            if(equal){
                errorBitNumber = i;
            }
        }
        System.out.println("Номер бита с ошибкой:" + ANSI_PURPLE + (errorBitNumber+1) + ANSI_RESET);

        return errorBitNumber;
    }

    //Реверс массива - кoстыль для моей программы
    public static void sort(int[] massive) {
        for (int i = 0; i < massive.length / 2; i++) {
            int tmp = massive[i];
            massive[i] = massive[massive.length - i - 1];
            massive[massive.length - i - 1] = tmp;
        }
    }

    public static int[] ShiftToRight(int a[],int n){
        int temp = a[n];

        for (int i = n; i > 0; i--) {
            a[i] = a[i-1];
        }

        a[0] = temp;

        System.out.println("Array after shifting to right by one : "+Arrays.toString(a));

        return a;

    }

    public static void moveRight(int[] array, int positions) {
        int size = array.length;
        for (int i = 0; i < positions; i++) {
            int temp = array[size - 1];
            for (int j = size - 1; j > 0; j--) {
                array[j] = array[j-1];
            }
            array[0] = temp;
        }
    }
}
