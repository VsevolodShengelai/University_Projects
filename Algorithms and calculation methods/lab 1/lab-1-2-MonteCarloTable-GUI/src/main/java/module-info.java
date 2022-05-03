module com.example.lab122montecarlo {
    requires javafx.controls;
    requires javafx.fxml;

    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;
    requires org.kordamp.bootstrapfx.core;

    opens com.example.lab122montecarlo to javafx.fxml;
    exports com.example.lab122montecarlo;
}