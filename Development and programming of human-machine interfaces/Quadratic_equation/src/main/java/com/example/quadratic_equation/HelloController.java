package com.example.quadratic_equation;

import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.scene.control.Button;
import javafx.scene.control.Label;
import javafx.scene.control.TextField;
import javafx.scene.image.ImageView;
import javafx.scene.input.MouseEvent;
import javafx.scene.layout.AnchorPane;
import javafx.scene.layout.Pane;
import javafx.stage.Stage;

public class HelloController {
    @FXML private Pane titlePane;
    @FXML private ImageView btnMinimize, btnClose;
    @FXML private Label lblResult;
    @FXML private Button calculateButton;
    @FXML private TextField aCoeff;
    @FXML private TextField bCoeff;
    @FXML private TextField cCoeff;

    double a, b, c;
    String a_str, b_str, c_str;

    public static double round(double value, int places) {
        if (places < 0) throw new IllegalArgumentException();

        long factor = (long) Math.pow(10, places);
        value = value * factor;
        long tmp = Math.round(value);
        return (double) tmp / factor;
    }

    public void onCalculate(ActionEvent actionEvent) {
        a_str = aCoeff.getText();
        b_str = bCoeff.getText();
        c_str = cCoeff.getText();

        if (a_str == "" | a_str == "" | a_str == ""){
            lblResult.setText("Введите все коэффициенты");
        }
        else{
            a = Double.parseDouble(a_str);
            b = Double.parseDouble(b_str);
            c = Double.parseDouble(c_str);

            if (a == 0)
            {
                if (b != 0)
                    //a не равно 0, уравнение вырождается в линейное//
                    lblResult.setText("x = "+(-c/b));
                else if (c == 0)
                    lblResult.setText("Все коэф-ты равны 0 \nх - любое число");
                else
                    lblResult.setText("Уравнение не имеет решений");
            }
            else
            {
                if (b == 0)
                    if (c == 0)
                        lblResult.setText("x = 0");
                    else if ((-c / a) < 0)
                        lblResult.setText("Действительных корней нет");
                    else
                        lblResult.setText("x = " + Math.sqrt(-c/a));
                else if (c == 0)
                    lblResult.setText("x1 = 0, x2 = " + (-b / a));
                else
                {
                    double D = b * b - 4 * a * c;
                    if (D > 0)
                    {
                        System.out.println(D
                        );
                        //"D>0, 2 действительных корня"
                        lblResult.setText("x1 = " +  round(((-b + Math.sqrt(D)) / (2 * a)),4) +
                                ", x2 = " + round(((-b - Math.sqrt(D)) / (2 * a)),4));
                    }
                    else if (D == 0)
                        //"D = 0; 1 корень
                        lblResult.setText("x = " + -b / (2 * a));
                    else if (D < 0)
                        lblResult.setText("D<0, действительных корней нет");
                }
            }
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