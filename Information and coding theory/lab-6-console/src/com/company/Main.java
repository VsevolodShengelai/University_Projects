package com.company;

import java.util.Arrays;

public class Main {

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
        double[] bytearray;
        //bytearray = generateArray(k);
        bytearray = generatePrerecordedArray(k);

        int[] inf_array = new int[N + p];
        for (int i = 0; i < N; i++) {
            if (bytearray[i] == 1) {
                inf_array[i + p] = 1;
            }
        }

        System.out.println(Arrays.toString(inf_array));

        //Запишием массив неприводимого полинома
        //ЗАПИСАТЬ МАССИВЫ ДЛЯ ВСЕХ ПОЛИНОМОВ и удалить эту запись
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
            System.out.println("===Проход" + enumerator+1+"===");
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




        //Единичная матрица на данном этапе лишняя - её не строим
        //Строим проверочную матрицу
        double[][] checkProducingMatrix = new double[N][p]; //До переворачивания
        //Счётчик проверочных последовательностей у нас в десятичном формате
        //Идём от обратного Единиц тогда точно должно хватить
        //Количество единиц (должно быть не менее d_min)
        //Последовательности не будут повторяться
        double checkNum = Math.pow(2, p) - 1;
        double checkPast = checkNum;
        for (int i = 0; i < N; i++) {
            while (Integer.bitCount((int) checkNum) < d_min - 1) {
                checkNum--;
            }
            byte octet = (byte) (checkNum);
            StringBuilder bin = new StringBuilder(Integer.toBinaryString(octet));
            //Добавляем нули, чтобы длина строки была равна p
            while (bin.length() < p) {
                bin.insert(0, "0");
            }
            for (int j = 0; j < p; j++) {
                checkProducingMatrix[i][j] = bin.charAt(j) - '0';
            }
            //Если checkNum изменится, то отнимать уже не будем
            if (checkPast == checkNum) {
                checkNum--;
            }
            checkPast = checkNum;
        }

        //Вывод производящей проверочной подматрицы
        withdrawMatrix(checkProducingMatrix);

        double[][] checkMatrix = new double[p][N + p];
        for (int i = 0; i < checkProducingMatrix[0].length; i++) {//Цикл по столбцам исходной матрицы
            for (int j = 0; j < checkProducingMatrix.length; j++) { //Цикл по строкам исходной матрицы
                checkMatrix[i][j] = checkProducingMatrix[j][i];
            }
        }
        for (int i = 0; i < checkMatrix.length; i++) {
            checkMatrix[i][N + i] = 1;
        }

        //Вывод проверочной матрицы (Даллее работаем только с ней)
        withdrawMatrix(checkMatrix);

        //Создаём массив Integer`ов и заполняем его
        //double[] bytearray;
        bytearray = generateArray(k);
        //bytearray = generatePrerecordedArray(k);

        //Составим проверочные "уравнения" и занесём правильные p (проверочные разряды)
        for (int i = 0; i < p; i++) { //Цикл количества уравнений
            int counter = 0;
            for (int j = 0; j < N; j++) {
                if (checkMatrix[i][j] == bytearray[j] &&
                        bytearray[j] == 1) {
                    counter++;
                }
            }
            if (counter % 2 == 1) {
                bytearray[N + i] = 1;
            } else { //else здесь нужен, т.к. генератор забивает ВСЕ элементы
                bytearray[N + i] = 0;
            }

        }

        //Допустим ошибку в каком-нибудь любом бите - что это за бит определим потом
        System.out.println(ANSI_RED + "Последовательность передана с ошибкой" + ANSI_RESET);

        double[] double_clear_array = Arrays.copyOf(bytearray, bytearray.length);
        double[] byte_error_array = getRandomError(bytearray); //Случайная ошибка
        bytearray = Arrays.copyOf(double_clear_array, double_clear_array.length);
        int errorBitNumber = defineError(byte_error_array, checkMatrix); //Определить бит с ошибкой

        //Посмотрим - бит проверочный, или информационный
        String byteType;

        if (errorBitNumber < N) {
            byteType = "Информационный";
        } else {
            byteType = "Проверочный";
        }

        System.out.println("Тип бита с ошибкой:" + ANSI_PURPLE + byteType + ANSI_RESET);


        //Теперь выведем кодовые последовательности - правильную и искажённую
        System.out.print("Код без ошибки: ");
        for (int i = 0; i < bytearray.length; i++) {
            if (i != errorBitNumber) {
                System.out.print((int) bytearray[i]);
            } else {
                System.out.print(ANSI_GREEN + (int) bytearray[i] + ANSI_RESET);
            }
        }
        System.out.println();
        System.out.print("Код с ошибкой:  ");
        for (int i = 0; i < byte_error_array.length; i++) {
            if (i != errorBitNumber) {
                System.out.print((int) byte_error_array[i]);
            } else {
                System.out.print(ANSI_RED + (int) byte_error_array[i] + ANSI_RESET);
            }
        }
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

    public static double[] generatePrerecordedArray(int k){
        return new double[]{1, 1, 1, 0, 0, 0};
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

    public static void withdrawMatrix(double[][] matrix){
        for (int i = 0; i < matrix.length; i++) {
            for (int j = 0; j < matrix[0].length; j++) {
                System.out.print((int)matrix[i][j] + " ");
            }
            System.out.println();
        }
        System.out.println("==========");
    }
}
