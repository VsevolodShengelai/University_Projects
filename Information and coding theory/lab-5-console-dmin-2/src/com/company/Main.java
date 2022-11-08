package com.company;

import java.util.Arrays;

public class Main {

    public static void main(String[] args) {

        //Коды для подсвечивания строк
        String ANSI_RED = "\u001B[31m";
        String ANSI_RESET = "\u001B[0m";
        String ANSI_PURPLE = "\u001B[35m";
        String ANSI_GREEN = "\u001B[32m";

        double N = 4; // Вводимый параметр N
        System.out.println("n= " + N);
        double k = 1;
        while (true){
            if (Math.pow(2,N) <= Math.pow(2,k)/(k+1)){
                System.out.println("k= " + k);
                break;
            }
            k++;
        }

        double p = k - N;

        System.out.println("p= " + p);
        //Создаём массив Integer`ов и заполняем его
        double[] bytearray;
        //bytearray = generateArray((int) k);
        bytearray = generatePrerecordedArray((int) k);

        //Составим проверочные уравнения и забьём правильные p
        for (int i = 0; i < p; i++) { //Цикл количества уравнений
            boolean itsP = true;
            int counter = 0;
            int pnumber = 0; //Это индекс проверочного бита

            for (int j = 0; j < bytearray.length; j++) { //Цикл одного уравнения
                byte octet = (byte) (j + 1); // Номера битов начинаются с 1, а не с 0
                String bin = Integer.toBinaryString(octet);
                if (bin.length() > i && bin.charAt(bin.length()-1-i) == '1'){
                    if(!itsP){
                        if(bytearray[j] == 1){
                            counter++;
                        }
                    }
                    else{
                        pnumber = j;
                    }
                    itsP = false;
                    System.out.print("bit№"+(j+1)+"=" + (int)bytearray[j] + "   ");
                }
            }

            //Забьём проверочный бит
            if (counter%2 == 1){
                bytearray[pnumber] = 1;
            }
            else{ //Можно не писать, эни всё равно проинициализированы нулями
                bytearray[pnumber] = 0;
            }
            System.out.print("||   " + "Значение проверочного бита " + (pnumber+1) + ":  " + bytearray[pnumber]);

            System.out.println();
            //System.out.print("Счётчик строки " + i + ": " + counter + "  ||  ");
        }

        //Допустим ЕДИНИЧНУЮ ошибку в проверочном бите
        System.out.println("=========");
        System.out.println(ANSI_RED + "Последовательность передана с ошибкой" + ANSI_RESET);

        //Здесь была аномалия, что массив bytearray изменялся как и byte_error_array
        //В итоге - 2 одинаковых массива
        //Мне кажется, это из-за того, что они ссылались на одни и те же данные
        double[] double_clear_array = Arrays.copyOf(bytearray, bytearray.length);
        double[] byte_error_array = getRandomDoubleError(bytearray); //ЕДИНИЧНАЯ или ДВОЙНАЯ
        bytearray = Arrays.copyOf(double_clear_array, double_clear_array.length);
        int errorBitNumber = defineError(byte_error_array, p);
        System.out.println("Номер бита с ошибкой:" + ANSI_PURPLE + errorBitNumber + ANSI_RESET);
        //Посчитаем дополнительный проверочный бит (для нахождения ДВОЙНОЙ ошибки)
        double S4 = getS4(bytearray);
        if (S4 == 0){ //Запишем кратность ошибки
            System.out.println("Кратность ошибки:" + ANSI_PURPLE + "2" + ANSI_RESET);
        }
        else {
            System.out.println("Кратность ошибки:" + ANSI_PURPLE + "1" + ANSI_RESET);
        }

        //Посмотрим - бит проверочный, или информационный
        String byteType = "";

        if ((Integer.bitCount(errorBitNumber) == 1)){ //У проверочного бита в дв записи одна единица
            byteType = "Проверочный";

        }
        else {
            byteType = "Информационный";
        }

        System.out.println("Тип бита с ошибкой:" + ANSI_PURPLE + byteType + ANSI_RESET);
        System.out.println("Синдром:" + ANSI_PURPLE + Integer.toBinaryString((byte) errorBitNumber) + ANSI_RESET);

        //Теперь выведем кодовые последовательности - правильную и искажённую
        //System.out.println(Arrays.toString(bytearray));
        //System.out.println(Arrays.toString(byte_error_array));

        System.out.print("Код без ошибки: ");
        for (int i = 0; i < bytearray.length; i++) {
            if(bytearray[i]==byte_error_array[i]){
                System.out.print((int)bytearray[i]);
            }
            else{
                System.out.print(ANSI_GREEN + (int)bytearray[i] + ANSI_RESET);
            }
        }
        System.out.println();
        System.out.print("Код с ошибкой:  ");
        for (int i = 0; i < byte_error_array.length; i++) {
            if(bytearray[i]==byte_error_array[i]){
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
            //Если число - не свободная двойка, то генерируем его
            //Свободные двойки не трогаем

            if (!(Integer.bitCount(i+1) == 1)){ //Включён только один бит

                if (Math.random() < 0.5){
                    bytearray[i] = 1;
                }
                else {
                    bytearray[i] = 0;
                }
                System.out.println("i" + i + ": " + bytearray[i]);
            }
        }
        System.out.println("=========");
        return bytearray;
    }

    public static double[] generatePrerecordedArray(int k){
        double[] bytearray  = {0, 0, 1, 0, 1, 0, 1};

        for (int i = 0; i < bytearray.length; i++) {
            //Если число - не свободная двойка, то генерируем его
            //Свободные двойки не трогаем

            if (!(Integer.bitCount(i+1) == 1)){ //Включён только один бит
                System.out.println("i" + i + ": " + bytearray[i]);
            }
        }
        System.out.println("=========");
        return bytearray;
    }

    public static double getS4 (double[] bytearray){
        double sum = Arrays.stream(bytearray).sum();
        if (sum%2 == 1){
            return 1;
        }
        else {
            return 0;
        }
    }

    //Генерирует случайную ЕДИНИЧНУЮ ошибку
    public static double[] getRandomError(double[] bytearray){
        int n = (int)Math.floor(Math.random() * bytearray.length); //рандомный элемент массива
        if (bytearray[n] == 1){
            bytearray[n] = 0;
        }
        else{
            bytearray[n] = 1;
        }
        return bytearray;
    }

    //Генерирует случайную ДВОЙНУЮ ошибку
    public static double[] getRandomDoubleError(double[] bytearray){
        int n = (int)Math.floor(Math.random() * bytearray.length); //рандомный элемент массива
        int m = n;
        while (m == n){
            m = (int)Math.floor(Math.random() * bytearray.length);
        }
        if (bytearray[n] == 1){
            bytearray[n] = 0;
        }
        else{
            bytearray[n] = 1;
        }
        if (bytearray[m] == 1){
            bytearray[m] = 0;
        }
        else{
            bytearray[m] = 1;
        }
        return bytearray;
    }

    public static int defineError(double[] bytearray, double p){
        int Sum = 0;
        for (int i = 0; i < p; i++) { //Цикл количества уравнений
            boolean itsP = true;
            int counter = 0;

            for (int j = 0; j < bytearray.length; j++) { //Цикл одного уравнения
                byte octet = (byte) (j + 1); // Номера битов начинаются с 1, а не с 0
                String bin = Integer.toBinaryString(octet);
                if (bin.length() > i && bin.charAt(bin.length()-1-i) == '1'){

                    if(bytearray[j] == 1){
                        counter++;
                    }

                    System.out.print("bit№"+(j+1)+"=" + (int)bytearray[j] + "   ");
                }
            }
            if (counter%2 == 1){
                Sum += Math.pow(2, i);
            }
            System.out.println();
        }
        return Sum;
    }

    public static double[] getStaticError(){
        return new double[]{1, 0, 0, 0, 1, 0, 1};
    }
}
