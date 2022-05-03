module com.example.lab41lagrangepolinomgui {
    requires javafx.controls;
    requires javafx.fxml;

    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;
    requires org.kordamp.bootstrapfx.core;

    opens com.example.lab41lagrangepolinomgui to javafx.fxml;
    exports com.example.lab41lagrangepolinomgui;
}