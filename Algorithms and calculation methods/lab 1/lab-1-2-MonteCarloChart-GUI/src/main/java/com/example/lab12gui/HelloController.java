package com.example.lab12gui;

import javafx.fxml.FXML;
import javafx.scene.chart.LineChart;
import javafx.scene.control.Label;

public class HelloController {
    @FXML
    private Label welcomeText;

    @FXML
    protected void onHelloButtonClick() {

        welcomeText.setText("Welcome to JavaFX Application!");
    }
}