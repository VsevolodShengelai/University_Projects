package com.company;

import java.util.Arrays;

public class Main {

    private static final String ANSI_PURPLE = "\u001B[35m";
    private static final String ANSI_RESET = "\u001B[0m";

    public static void main(String[] args) {

        //Коды для подсвечивания строк
        String ANSI_RED = "\u001B[31m";
        String ANSI_GREEN = "\u001B[32m";

        int N = 4; // Вводимый параметр N
        System.out.println("n= " + N);
        int k = 1;
        while (true){
            if (Math.pow(2,N) <= Math.pow(2,k)/(k+1)){
                System.out.println("k= " + k);
                break;
            }
            k++;
        }

        int p = k - N;
        double sigma = 1;
        double d_min = 2*sigma + 1;
        System.out.println("p= " + p);
        System.out.println("d_min= " + d_min);

        //Единичная матрица на данном этапе лишняя - её не строим
        //Строим проверочную матрицу
        double[][] checkProducingMatrix = new double[N][p]; //До переворачивания
        //Счётчик проверочных последовательностей у нас в десятичном формате
        //Идём от обратного Единиц тогда точно должно хватить
        //Количество единиц (должно быть не менее d_min)
        //Последовательности не будут повторяться
        double checkNum = Math.pow(2,p)-1;
        double checkPast = checkNum;
        for (int i = 0; i < N; i++) {
            while (Integer.bitCount((int)checkNum) < d_min-1){
                checkNum--;
            }
            byte octet = (byte) (checkNum);
            StringBuilder bin = new StringBuilder(Integer.toBinaryString(octet));
            //Добавляем нули, чтобы длина строки была равна p
            while (bin.length() < p){
                bin.insert(0, "0");
            }
            for (int j = 0; j < p; j++) {
                checkProducingMatrix[i][j] = bin.charAt(j) - '0';
            }
            //Если checkNum изменится, то отнимать уже не будем
            if (checkPast == checkNum){
                checkNum --;
            }
            checkPast = checkNum;
        }

        //Вывод производящей проверочной подматрицы
        withdrawMatrix(checkProducingMatrix);

        double[][] checkMatrix = new double[p][N+p];
        for (int i = 0; i < checkProducingMatrix[0].length; i++) {//Цикл по столбцам исходной матрицы
            for (int j = 0; j < checkProducingMatrix.length; j++) { //Цикл по строкам исходной матрицы
                checkMatrix[i][j] = checkProducingMatrix[j][i];
            }
        }
        for (int i = 0; i < checkMatrix.length; i++) {
            checkMatrix[i][N+i] = 1;
        }

        //Вывод проверочной матрицы (Даллее работаем только с ней)
        withdrawMatrix(checkMatrix);

        //Создаём массив Integer`ов и заполняем его
        double[] bytearray;
        bytearray = generateArray(k);
        //bytearray = generatePrerecordedArray(k);

        //Составим проверочные "уравнения" и занесём правильные p (проверочные разряды)
        for (int i = 0; i < p; i++) { //Цикл количества уравнений
            int counter = 0;
            for (int j = 0; j < N; j++) {
                if (checkMatrix[i][j] == bytearray[j] &&
                bytearray[j] == 1){
                    counter++;
                }
            }
            if (counter%2 == 1){
                bytearray[N+i] = 1;
            }
            else{ //else здесь нужен, т.к. генератор забивает ВСЕ элементы
                bytearray[N+i] = 0;
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

        if (errorBitNumber < N){
            byteType = "Информационный";
        }
        else {
            byteType = "Проверочный";
        }

        System.out.println("Тип бита с ошибкой:" + ANSI_PURPLE + byteType + ANSI_RESET);
        

        //Теперь выведем кодовые последовательности - правильную и искажённую
        System.out.print("Код без ошибки: ");
        for (int i = 0; i < bytearray.length; i++) {
            if(i!=errorBitNumber){
                System.out.print((int)bytearray[i]);
            }
            else{
                System.out.print(ANSI_GREEN + (int)bytearray[i] + ANSI_RESET);
            }
        }
        System.out.println();
        System.out.print("Код с ошибкой:  ");
        for (int i = 0; i < byte_error_array.length; i++) {
            if(i!=errorBitNumber){
                System.out.print((int)byte_error_array[i]);
            }
            else{
                System.out.print(ANSI_RED + (int)byte_error_array[i] + ANSI_RESET);
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
        return new double[]{1, 1, 0, 1, 0, 0, 0};
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
