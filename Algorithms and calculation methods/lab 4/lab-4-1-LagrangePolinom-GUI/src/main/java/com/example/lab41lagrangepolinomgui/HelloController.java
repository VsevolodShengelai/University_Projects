package com.example.lab41lagrangepolinomgui;

import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.chart.LineChart;
import javafx.scene.chart.NumberAxis;
import javafx.scene.chart.XYChart;
import javafx.scene.control.*;
import javafx.scene.control.cell.PropertyValueFactory;
import javafx.scene.control.cell.TextFieldTableCell;

import java.net.URL;
import java.util.Observable;
import java.util.ResourceBundle;

import static java.util.List.copyOf;

public class HelloController implements Initializable {
    @FXML
    private TableView<Coordinates> table;
    @FXML
    private TableColumn<Coordinates, String> table_column_x;
    @FXML
    private TableColumn<Coordinates, String> table_column_y;
    @FXML
    private Button addButton;
    @FXML
    private TextField xTextField;
    @FXML
    public LineChart<Number,Number> lineChart;
    @FXML
    protected NumberAxis yAxis;
    @FXML
    protected NumberAxis xAxis;


    //Технический класс, который расширяет массив на 1
    private static double[] extendArraySize(double [] array){
        double [] temp = array.clone();
        array = new double[array.length + 1];
        System.arraycopy(temp, 0, array, 0, temp.length);
        return array;
    }

    double[] X = new double[] {1,2,3,4,5};
    double[] Y = new double[] {1.9,5.5,10,15,21};

    double[] X_old = X;
    double[] Y_old = Y;

    double[] X_new = new double[0];
    double[] Y_new = new double[0];

    boolean first_draw = true;

    @Override
    public void initialize(URL url, ResourceBundle resourceBundle) {
        table_column_x.setCellValueFactory(new PropertyValueFactory<>("X"));
        table_column_y.setCellValueFactory(new PropertyValueFactory<>("Y"));
        table_column_y.setEditable(true);
        table.setItems(getCoordinates());
    }

    ObservableList<Coordinates> getCoordinates(){
        ObservableList<Coordinates> coordinates = FXCollections.observableArrayList();
        for (int i = 0; i < X.length; i++){
            coordinates.add(new Coordinates(X[i], Y[i]));
        }
        return coordinates;
    }

    ObservableList<Coordinates> clearCoordinates(){
        ObservableList<Coordinates> coordinates = FXCollections.observableArrayList();
        for (int i = 0; i < X_old.length; i++){
            coordinates.add(new Coordinates(X_old[i], Y_old[i]));
        }
        return coordinates;
    }

    //Формула для нахождения базового полинома
    private double l(int index, double[] X, double x)
    {
        double l = 1;
        for (int i = 0; i < X.length; i++)
        {
            if (i != index)
            {
                l *= (x - X[i]) / (X[index] - X[i]);
            }
        }
        return l;
    }

    @FXML
    protected void addPoly(){
        int N = 1000 ;
        double h = (Math.abs(X[X.length-1] - X[0]))/N;

        for (int i = 0; i < X.length; i++){ //Проходим по каждому базисному полиному
            ObservableList<XYChart.Data> datasN = FXCollections.observableArrayList();

            for(int k=0; k < N; k++){
                datasN.add(new XYChart.Data(X[0]+h*k,Y[i]*l(i, X, X[0]+h*k)));
            }
            XYChart.Series seriesN = new XYChart.Series();

            seriesN.setData(datasN);



            lineChart.getData().add(seriesN);
            lineChart.setStyle("");
        }
    }

    @FXML
    protected void clearData(){
        lineChart.getData().clear();
        first_draw = true;

        X_new = new double[0];
        Y_new = new double[0];

        X = X_old;
        Y = Y_old;

        table.setItems(clearCoordinates());

    }

    @FXML
    protected void addButtonClick() {
        double x = Double.parseDouble(xTextField.getText());
        Lagrange lagrange = new Lagrange();
        double y = lagrange.GetValue(X,Y, x);

        //Общий список - для построения полинома Лагранжа
        X = extendArraySize(X);
        Y = extendArraySize(Y);
        X[X.length-1] = x;
        Y[Y.length-1] = y;

        //Добавляем значения в новый список
        X_new = extendArraySize(X_new);
        Y_new = extendArraySize(Y_new);
        X_new[X_new.length-1] = x;
        Y_new[Y_new.length-1] = y;

        //Сортировка массива пузырьком
        /*Массивы новых значений можно также отсортировать,
        * но это не повлияет на внешний вид графика (т.к. у новых массивов
        * отображаются только маркеры)*/
        for(int i = X.length-1 ; i > 0 ; i--){
            for(int j = 0 ; j < i ; j++) {
                if (X[j] > X[j + 1]) {
                    double tmp = X[j];
                    double tmp2 = Y[j];
                    X[j] = X[j + 1];
                    Y[j] = Y[j + 1];
                    X[j + 1] = tmp;
                    Y[j + 1] = tmp2;
                }
            }
        }
        table.setItems(getCoordinates());
    }

    public void drawGraph() {
        if (first_draw == true){
            /*Заполнение графика данными в первый раз
            * Здесь мы применяем к графику таблицу стилей
            */

            //Серия данных для построения многочлена Лагранжа
            ObservableList<XYChart.Data> datas = FXCollections.observableArrayList();
            XYChart.Series series = new XYChart.Series();
            /*
            Разобьём график на множество точек
            10000 будет достаточно для визуально правильного отображения полинома
            */
            double N = 1000;
            double h = (Math.abs(X[X.length-1] - X[0]))/N;
            Lagrange lagrange = new Lagrange();

            for(int i=0; i< N; i++){
                datas.add(new XYChart.Data(X[0]+h*i,lagrange.GetValue(X_old,Y_old,X[0]+h*i)));
            }
            series.setData(datas);
            //Из-за таблицы стилей эта надпись не видна
            //series.setName("Полином Лагранжа");

            //Серия данных для отображения старых точек на многочлене Лагранжа
            ObservableList<XYChart.Data> datas1 = FXCollections.observableArrayList();
            XYChart.Series series1 = new XYChart.Series();
            for(int i=0; i< X_old.length; i++){
                datas1.add(new XYChart.Data(X_old[i],Y_old[i]));
            }
            series1.setData(datas1);
            series1.setName("Точки условия");

            ObservableList<XYChart.Data> datas2 = FXCollections.observableArrayList();
            XYChart.Series series2 = new XYChart.Series();
            for(int i=0; i< X_new.length; i++){
                datas1.add(new XYChart.Data(X_new[i],Y_new[i]));
            }
            series2.setData(datas2);
            series2.setName("Новые точки");

            //Мы добавляем балицу стилей в fxml, здесь это уже не нужно
            //lineChart.setStyle();
            /*lineChart.getStylesheets().add(HelloApplication.class.getResource("style.css")
                .toExternalForm());*/
            lineChart.getData().add(series);
            lineChart.getData().add(series1);
            lineChart.getData().add(series2);


            first_draw = false;
        }
        else{
            XYChart.Series<Number, Number> first_series = lineChart.getData().get(0);
            XYChart.Series<Number, Number> second_series = lineChart.getData().get(1);
            XYChart.Series<Number, Number> third_series = lineChart.getData().get(2);
            first_series.getData().clear();
            second_series.getData().clear();
            third_series.getData().clear();

            double N = 1000;
            double h = (Math.abs(X[X.length-1] - X[0]))/N;
            Lagrange lagrange = new Lagrange();

            for(int i=0; i< N; i++){
                first_series.getData().add(
                        new LineChart.Data<Number,Number>(X[0]+h*i, lagrange.GetValue(X,Y,X[0]+h*i)));
            }

            for(int i=0; i< X_old.length; i++){
                second_series.getData().add(new LineChart.Data<Number,Number>(X_old[i], Y_old[i]));
            }

            for(int i=0; i< X_new.length; i++){
                third_series.getData().add(new LineChart.Data<Number,Number>(X_new[i], Y_new[i]));
            }
        }


        /*График автоматически масштабируется,
        но можно было бы задать изменение прделов осей
        и шаг вручную
         */




        //lineChart.getData().clear();

        //Серия данных для построения многочлена Лагранжа





    }
}