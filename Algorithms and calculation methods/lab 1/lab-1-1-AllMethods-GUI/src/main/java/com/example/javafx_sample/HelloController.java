package com.example.javafx_sample;

import javafx.fxml.FXML;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;

public class HelloController {
    @FXML
    private TextField lower_limit;
    @FXML
    private TextField upper_limit;
    @FXML
    private TextField epsilon;

    @FXML
    private Label rezult_3_simpson;
    @FXML
    private Label ncount_3_simpson;
    @FXML
    private Label time_3_simpson;

    @FXML
    private Label rezult_1_rectangle;
    @FXML
    private Label ncount_1_rectangle;
    @FXML
    private Label time_1_rectangle;

    @FXML
    private Label rezult_2_trapeze;
    @FXML
    private Label ncount_2_trapeze;
    @FXML
    private Label time_2_trapeze;


    @FXML
    protected void onCalculateButtonClick() {
        //welcomeText.setText("Welcome to JavaFX Application!");
        double a = Double.parseDouble(lower_limit.getText());
        double b = Double.parseDouble(upper_limit.getText());
        double eps = Double.parseDouble(epsilon.getText());

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

    }
    @FXML
    protected void onResetButtonClick() {

        rezult_3_simpson.setText("");
        ncount_3_simpson.setText("");
        time_3_simpson.setText("");

        rezult_1_rectangle.setText("");
        ncount_1_rectangle.setText("");
        time_1_rectangle.setText("");

        rezult_2_trapeze.setText("");
        ncount_2_trapeze.setText("");
        time_2_trapeze.setText("");

    }
}