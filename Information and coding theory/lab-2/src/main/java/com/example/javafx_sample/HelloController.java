package com.example.javafx_sample;

import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;

import java.io.Console;

public class HelloController {
    @FXML
    private TextField N;
    @FXML
    private TextField P;
    @FXML
    private Label Sum_label;
    @FXML
    private Label H_max_label;
    @FXML
    private Label I_x_label;
    @FXML
    private Label I_x_comma_y_label;
    @FXML
    private Label I_x_slash_y_label;
    @FXML
    private TextArea Text_area_Px;



    @FXML
    protected void onCalculateButtonClick() {
        int Num = Integer.parseInt(N.getText());
        double P_x = Double.parseDouble(P.getText());

        //Получаем случайные значения вероятностей ВХОДНЫХ сообщений
        double[] p_array = Calculator.getRandDistArray(Num, 1);

        String p_array_text = "";
        //Выведем их в текстовое поле!
        for (int i = 0; i < p_array.length; i++) {
            p_array_text =p_array_text + "x"+i+": " + p_array[i] + "\n";
        }
        Text_area_Px.setText(p_array_text);

        //Получаем матрицу вероятностей сообщения x на входе и y на выходе
        //Для этого нужно знать только N
        double[][] matrix = Calculator.getMatrix(Num, P_x);

        //Находим условную энтропию
        Double I_x_slash_y = Calculator.getEntropy(matrix, p_array);
        I_x_slash_y_label.setText(Double.toString(I_x_slash_y));

        double Sum = 0;
        for (int i = 0; i < p_array.length; i++) {
            Sum += p_array[i];
        }
        Sum_label.setText(Double.toString(Sum));

        //Выведем значение количества информации для всей совокупности дискретных случайных сообщений
        double I_x = Calculator.getI_x(p_array);
        I_x_label.setText(Double.toString(I_x));

        I_x_comma_y_label.setText(Double.toString(I_x - I_x_slash_y));

        //Выведем значение максимальной энтропии
        H_max_label.setText(Double.toString(Calculator.getH_max(Num)));

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