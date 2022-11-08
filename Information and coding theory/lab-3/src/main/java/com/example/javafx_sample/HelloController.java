package com.example.javafx_sample;

import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;

public class HelloController {
    @FXML
    private TextField N;
    @FXML
    private TextField Q_calc_label;
    @FXML
    private Label H_max_label;
    @FXML
    private Label H_label;
    @FXML
    private Label I_x_label;
    @FXML
    private Label I_x_comma_y_label;
    @FXML
    private Label C_interference_label;
    @FXML
    private TextArea Text_area_Px;
    @FXML
    private Label C_label;



    @FXML
    protected void onCalculateButtonClick() {
        int Num = Integer.parseInt(N.getText());

        //Получаем случайные значения вероятностей ВХОДНЫХ сообщений
        double[] p_array = Calculator.getRandDistArray(Num, 1);

        String p_array_text = "";
        //Выведем их в текстовое поле!
        for (int i = 0; i < p_array.length; i++) {
            p_array_text =p_array_text + "x"+i+": " + p_array[i] + "\n";
        }
        Text_area_Px.setText(p_array_text);

        //Выведем значение максимальной энтропии
        double H_max = Calculator.getH_max(Num);
        H_max_label.setText(Double.toString(H_max));
        //Время импульса
        double U_y = 1/(Num * 0.001);
        //Находим пропускную способность  вканале без помех
        double C = U_y * H_max;
        C_label.setText(Double.toString(C));
        double H_y = Calculator.getI_x(p_array);
        H_label.setText(Double.toString(H_y));
        //Скорость передачи информации
        double I_y = U_y * H_y;
        I_x_label.setText(Double.toString(I_y));

        //Расчёты для канала с потерями
        //Вероятность ошибки (вероятность перехода в соседние каналы)
        // !!! С Q нужно быть аккуратным - сумма вероятностей не может быть больше 1 !!!
        // Иначе бросаем исключение
        double Q = 1/(2.0*Num);
        Q_calc_label.setText(Double.toString(Q));
        //Получаем матрицу вероятностей сообщения y на входе и z на выходе
        //Для этого нужно знать только N - для размера матрицы и Q - вероятность ошибки
        double[][] matrix = Calculator.getMatrix(Num, Q);
        //Находим условную энтропию
        double I_x_slash_y = Calculator.getEntropy(matrix, p_array);
        double C_inter = U_y * (H_max - I_x_slash_y);
        C_interference_label.setText(Double.toString(C_inter));
        double I_x_comma_y = U_y * (H_y - I_x_slash_y);
        I_x_comma_y_label.setText(Double.toString(I_x_comma_y));

        Text_area_Px.setText(Text_area_Px.getText() + Calculator.roundNum(C) + "\n"
                + Calculator.roundNum(I_y) + "\n"
                + Calculator.roundNum(C_inter) + "\n"
                + Calculator.roundNum(I_x_comma_y));

        /*System.out.println(Calculator.getI_x(p_array));
        System.out.println(Calculator.getH_max(Num));

        Text_area.setText(Double.toString(Calculator.getI_x(p_array)));*/

        /*

        //Метод Симпсона
        Simpson simpson = new Simpson();
        double[] answer3 = simpson.take_a_result(a,b,eps);
        rezult_3_simpson.setText(Double.toString(answer3[0]));
        ncount_3_simpson.setText(Double.toString(answer3[1]));
        time_3_simpson.setText(Double.toString(answer3[2]));

        //Метод средних прямоугольников
        MiddleRectangle rectangle = new MiddleRectangle();
        double[] answer1 = rectangle.take_a_result(a,b,eps);
        rezult_1_rectangle.setText(Double.toString(answer1[0]));
        ncount_1_rectangle.setText(Double.toString(answer1[1]));
        time_1_rectangle.setText(Double.toString(answer1[2]));

        //Метод средних трапеций
        Trapeze trapeze = new Trapeze();
        double[] answer2 = trapeze.take_a_result(a,b,eps);
        rezult_2_trapeze.setText(Double.toString(answer2[0]));
        ncount_2_trapeze.setText(Double.toString(answer2[1]));
        time_2_trapeze.setText(Double.toString(answer2[2]));

         */

    }
    @FXML
    protected void onResetButtonClick() {
/*
        rezult_3_simpson.setText("");
        ncount_3_simpson.setText("");
        time_3_simpson.setText("");

        rezult_1_rectangle.setText("");
        ncount_1_rectangle.setText("");
        time_1_rectangle.setText("");

        rezult_2_trapeze.setText("");
        ncount_2_trapeze.setText("");
        time_2_trapeze.setText("");
*/
    }
}