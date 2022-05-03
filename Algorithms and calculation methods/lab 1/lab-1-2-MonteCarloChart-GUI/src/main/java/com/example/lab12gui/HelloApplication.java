package com.example.lab12gui;

import javafx.application.Application;
import javafx.fxml.FXMLLoader;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.scene.Scene;
import javafx.scene.chart.ScatterChart;
import javafx.stage.Stage;
import javafx.scene.Parent;
import javafx.scene.Scene;
import javafx.scene.chart.LineChart;
import javafx.scene.chart.NumberAxis;
import javafx.scene.chart.XYChart;
import javafx.stage.Stage;



import java.io.IOException;

public class HelloApplication extends Application {
    public static double f (double x) {
        return Math.log(x+Math.sqrt(x*x-0.25))/(2*x*x);
    }

    @Override
    public void start(Stage stage) throws IOException {
        FXMLLoader fxmlLoader = new FXMLLoader(HelloApplication.class.getResource("hello-view.fxml"));
        stage.setTitle("Monte-Carlo");

        //Введём данные для нашего примера
        double a = 0.5;
        double b = 1.7;
        //График функции должен быть чётким, поэтому у него будет свой набор координат
        //Тысячи для хорошей картинки будет досстататочно
        int N = 100; //Количество точек для построения графика функции
        double h = (b-a)/N;


        double[] xi = new double[N];
        double[] yi = new double[N];
        double[] fi = new double[N];


        double maxyi = 0; //Для определения максимального y - чтобы найти S квадрата
        for (int i = 0; i < N; i++){
            xi[i] = a+h*i;
            fi[i] = f(xi[i]);
            if (Math.abs(fi[i]) > maxyi) maxyi = Math.abs(fi[i]);
            }


        //Подготовим первый набор данных для фукции
        ObservableList<XYChart.Data> datas = FXCollections.observableArrayList();
        ObservableList<XYChart.Data> datas2 = FXCollections.observableArrayList();
        ObservableList<XYChart.Data> datas3 = FXCollections.observableArrayList();

        //Datas2 - линия функции, без точек
        for(int i=0; i<N; i++){
            datas2.add(new XYChart.Data(xi[i],fi[i]));
        }

        //Теперь разбрасывем точки


        N = 1000; //количество точек
        double c = -maxyi;
        double d = maxyi;
        double S = (d-c)*(b-a);
        //Начинаем разброс
        xi = new double[N];
        yi = new double[N];
        fi = new double[N];
        int pos_point_counter = 0;
        int neg_point_counter = 0;
        int bad_point_counter = 0;

        for(int i = 0; i < N; i++){
            xi[i] = a+Math.random()*(b-a);
            yi[i] = c+Math.random()*(d-c);
            //System.out.println( xi[i] + "    "+ yi[i]);
            fi[i] = f(xi[i]);
            if (yi[i] < 0){//точка ниже y=0
                if (fi[i] < 0 & yi[i]> fi[i]){
                    neg_point_counter++;
                    //Попала
                    datas.add(new XYChart.Data(xi[i],yi[i])); //В datas Забиваем Не попавшие точки!!!

                }
                else{
                    bad_point_counter++;
                    //Не попала
                    datas3.add(new XYChart.Data(xi[i],yi[i]));
                }
            }
            if(yi[i] > 0){ //точка выше y=0
                if (fi[i] > 0 & yi[i] < fi[i]){
                    pos_point_counter++;
                    //Попала
                    datas.add(new XYChart.Data(xi[i],yi[i]));
                }
                else{
                    bad_point_counter++;
                    //Не попала
                    datas3.add(new XYChart.Data(xi[i],yi[i]));
                }
            }
        }


        double integral = S*(pos_point_counter)/(pos_point_counter+neg_point_counter+bad_point_counter);
        //Вычтем отрицательные площади
        integral -= S*(neg_point_counter)/(pos_point_counter+neg_point_counter+bad_point_counter);
        System.out.println(pos_point_counter);
        System.out.println(neg_point_counter);
        System.out.println(bad_point_counter);
        System.out.println(S);

        System.out.println(integral);



        NumberAxis x  = new NumberAxis(a, b, 0.1);
        NumberAxis y  = new NumberAxis(c, d, 0.1);
        LineChart<Number, Number> numberLineChart = new LineChart<Number, Number>(x,y);
        numberLineChart.setTitle("Series");
        XYChart.Series series1 = new XYChart.Series();
        XYChart.Series series2 = new XYChart.Series();
        XYChart.Series series3 = new XYChart.Series();
        series3.setName("Не попали");
        series2.setName(""); //Здесь f(x) на графике не видно
        series1.setName("Попали");

        series1.setData(datas);
        series2.setData(datas2);
        series3.setData(datas3);

        Scene scene = new Scene(numberLineChart, 600, 600);
        numberLineChart.getData().add(series1);
        numberLineChart.getData().add(series2);
        numberLineChart.getData().add(series3);
        numberLineChart.getStylesheets().add(HelloApplication.class.getResource("style.css")
                .toExternalForm());
        stage.setScene(scene);
        stage.show();


    }

    public static void main(String[] args) {
        launch();
    }
}