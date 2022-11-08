package com.example.quadratic_equation;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;

import java.net.URL;
import java.util.Arrays;
import java.util.ResourceBundle;


public class HelloController  {
    @FXML private Pane titlePane;
    @FXML private ImageView btnMinimize, btnClose;
    @FXML private Label lblResult;

    @FXML private Button calculateButton;
    @FXML private TextField input_label;
    @FXML private TextField output_label;
    @FXML private TextField n_field;
    @FXML private TextField moves_field;
    @FXML private TextField moves_sequence_label;
    @FXML private TextField moves_syndrom_label;
    @FXML private TextArea text_area;

    int N = -1;
    int k = -1;
    int p = -1;
    int[] bytearray;

    public void onGenerate(ActionEvent actionEvent) {
        N = Integer.parseInt(n_field.getText());
        k = 1;
        while (true) {
            if (Math.pow(2, N) <= Math.pow(2, k) / (k + 1)) {
                break;
            }
            k++;
        }
        p = k - N;
        bytearray = generateArray(k,N);
        input_label.setText(Arrays.toString(bytearray).replaceAll("\\[|]", "").
                replaceAll(", ", ""));
    }

    public void onCalculate(ActionEvent actionEvent) {
        String bytearray_string = input_label.getText();
        bytearray = new int[bytearray_string.length()];
        for (int i = 0; i < bytearray_string.length(); i++) {
            bytearray[i] = bytearray_string.charAt(i) - '0';
        }
        int[] inf_array = new int[N + p];
        for (int i = 0; i < N; i++) {
            if (bytearray[i] == 1) {
                inf_array[i + p] = 1;
            }
        }
        System.out.println(Arrays.toString(inf_array));
        //Находим синдром(проверочный) и записываем проверочные биты
        int syndrom = getSyndrom(inf_array, p);
        StringBuilder bin = new StringBuilder(Integer.toBinaryString((byte)syndrom));
        while (bin.length() < p){
            bin.insert(0, "0");
        }
        for (int j = 0; j < bin.length(); j++) {
            bytearray[N+j] = bin.charAt(j) - '0';
        }
        output_label.setText(Arrays.toString(bytearray).replaceAll("\\[|]", "").
                replaceAll(", ", ""));
        //Допустим ошибки во всех битах
        System.out.println("Допустим ошибки во всех битах");
        StringBuilder text_area_string = new StringBuilder();
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
            text_area_string.append("byte" + i + ": " + Integer.toBinaryString(syndrom) + "\n");
            System.out.println("Синдром бита " + i + ":" + syndrom);
        }
        text_area.setText(String.valueOf(text_area_string));
    }

    public void onReshuffle(ActionEvent actionEvent) {
        onCalculate(actionEvent);
        int[] bytearray_current = new int[bytearray.length];
        for (int i = 0; i < bytearray.length; i++) {
            bytearray_current[i] = bytearray[i];
        }

        moveRight(bytearray_current, Integer.parseInt(moves_field.getText()));
        sort(bytearray_current);
        int syndrom = getSyndrom(bytearray_current, p);
        System.out.println("Проверочный синдром:" + syndrom);
        sort(bytearray_current);
        moves_sequence_label.setText(Arrays.toString(bytearray_current).replaceAll("\\[|]", "").
                replaceAll(", ", ""));
        moves_syndrom_label.setText("0");
    }

    public static int[] generateArray(int k, int N){
        int [] bytearray  = new int[k];
        for (int i = 0; i < N; i++) {
            if (Math.random() < 0.5){
                bytearray[i] = 1;
            }
            else {
                bytearray[i] = 0;
            }

        }
        return bytearray;
    }

    double a, b, c;
    String a_str, b_str, c_str;

    public static int getSyndrom(int[] inf_array, int p){
        //Запишием массив неприводимого полинома
        //ЕСЛИ СО СТРОКАМИ ВАРИАНТ ПОЛУЧИТСЯ - записываем здесь сразу строки
        int [] divider_polinom = new int[0]; //Не проинициализировать не получится
        if (p == 3) {
            divider_polinom = new int[]{1, 0, 1, 1};
        }
        if (p == 4) {
            divider_polinom = new  int[]{1,0,0,1,1};
        }
        if(p == 7){
            divider_polinom = new int[]{1, 0, 0, 0, 0, 0, 1, 1};
        }
        if (p == 5){
            divider_polinom = new int[]{1, 0, 0, 1, 0, 1};
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
        result = Math.abs(result);

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
            result = Math.abs(result);
            System.out.println("result: " + result);
            dividend_decimal = result;
            dividend = new StringBuilder(Integer.toBinaryString((byte) dividend_decimal));
            divider = new StringBuilder();
        }

        return result;
    }

    //Сдвиг элементов массива
    public static void moveRight(int[] array, int positions) {
        positions = positions%array.length;

        StringBuilder shifted = new StringBuilder(array.length);
        String str = Arrays.toString(array).replaceAll("\\[|]", "").
                replaceAll(", ", "");

        for(int i=0; i<array.length; i++)
        {
            int ind = (str.length() + i - positions) % str.length();
            shifted.append(str.charAt(ind));
        }

        String array_moved = shifted.toString();
        for (int i = 0; i < array_moved.length(); i++) {
            array[i] = array_moved.charAt(i) - '0';
        }

    }
    //Реверс массива - кoстыль для моей программы
    public static void sort(int[] massive) {
        for (int i = 0; i < massive.length / 2; i++) {
            int tmp = massive[i];
            massive[i] = massive[massive.length - i - 1];
            massive[massive.length - i - 1] = tmp;
        }
    }



    private double x, y;
    private double num1 = 0;
    private String operator = "+";

    public void init(Stage stage) {
        titlePane.setOnMousePressed(mouseEvent -> {
            x = mouseEvent.getSceneX();
            y = mouseEvent.getSceneY();
        });
        titlePane.setOnMouseDragged(mouseEvent -> {
            stage.setX(mouseEvent.getScreenX()-x);
            stage.setY(mouseEvent.getScreenY()-y);
        });

        btnClose.setOnMouseClicked(mouseEvent -> stage.close());
        btnMinimize.setOnMouseClicked(mouseEvent -> stage.setIconified(true));
    }

    @FXML
    void onNumberClicked(MouseEvent event) {
        int value = Integer.parseInt(((Pane)event.getSource()).getId().replace("btn",""));
        lblResult.setText(Double.parseDouble(lblResult.getText())==0?String.valueOf((double)value):String.valueOf(Double.parseDouble(lblResult.getText())*10+value));
    }

    @FXML
    void onSymbolClicked(MouseEvent event) {
        String symbol = ((Pane)event.getSource()).getId().replace("btn","");
        if(symbol.equals("Equals")) {
            double num2 = Double.parseDouble(lblResult.getText());
            switch (operator) {
                case "+" -> lblResult.setText((num1+num2) + "");
                case "-" -> lblResult.setText((num1-num2) + "");
                case "*" -> lblResult.setText((num1*num2) + "");
                case "/" -> lblResult.setText((num1/num2) + "");
            }
            operator = ".";
        }
        else if(symbol.equals("Clear")) {
            lblResult.setText(String.valueOf(0.0));
            operator = ".";
        }
        else {
            switch (symbol) {
                case "Plus" -> operator = "+";
                case "Minus" -> operator = "-";
                case "Multiply" -> operator = "*";
                case "Divide" -> operator = "/";
            }
            num1 = Double.parseDouble(lblResult.getText());
            lblResult.setText(String.valueOf(0.0));
        }
    }


}