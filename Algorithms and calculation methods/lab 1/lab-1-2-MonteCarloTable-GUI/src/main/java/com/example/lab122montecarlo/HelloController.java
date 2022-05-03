package com.example.lab122montecarlo;

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
    private TextField num;
    @FXML
    private TextField ser;

    @FXML
    private Label rezult_1;
    @FXML
    private Label ncount_1;
    @FXML
    private Label time_1;
    @FXML
    private Label accuracy_1;


    @FXML
    protected void onCalculateButtonClick() {
        //welcomeText.setText("Welcome to JavaFX Application!");
        double a = Double.parseDouble(lower_limit.getText());
        double b = Double.parseDouble(upper_limit.getText());
        double eps = Double.parseDouble(epsilon.getText());
        int N =  Integer.parseInt(num.getText());
        int series =  Integer.parseInt(ser.getText());


        //Метод Монте-Карло
        MonteCarlo monte_carlo = new MonteCarlo();
        double[] answer4 = monte_carlo.take_a_result(a,b,eps,N,series);
        rezult_1.setText(Double.toString(answer4[0]));
        ncount_1.setText(Double.toString(answer4[1]));
        time_1.setText(Double.toString(answer4[2]));
        accuracy_1.setText(Double.toString(answer4[3]));
    }

    @FXML
    protected void onCalculateButtonClickAccurately() {

        double a = Double.parseDouble(lower_limit.getText());
        double b = Double.parseDouble(upper_limit.getText());
        double eps = Double.parseDouble(epsilon.getText());
        int N = Integer.parseInt(num.getText());
        int series = Integer.parseInt(ser.getText());


        //Метод Монте-Карло
        MonteCarlo monte_carlo = new MonteCarlo();
        double[] answer4 = monte_carlo.take_a_result_accurately(a, b, eps, N, series);
        rezult_1.setText(Double.toString(answer4[0]));
        ncount_1.setText(Double.toString(answer4[1]));
        time_1.setText(Double.toString(answer4[2]));
        accuracy_1.setText(Double.toString(answer4[3]));
    }

    @FXML
    protected void onResetButtonClick() {
        rezult_1.setText("");
        ncount_1.setText("");
        time_1.setText("");
        accuracy_1.setText("");
    }
}