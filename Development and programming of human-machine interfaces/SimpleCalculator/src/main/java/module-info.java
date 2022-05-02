module com.example.engineercalculator {
    requires javafx.controls;
    requires javafx.fxml;

    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;
    requires org.kordamp.bootstrapfx.core;

    opens com.example.simplecalculator to javafx.fxml;
    exports com.example.simplecalculator;
}