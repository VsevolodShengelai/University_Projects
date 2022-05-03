module com.example.javafx_sample {
    requires javafx.controls;
    requires javafx.fxml;

    requires com.dlsc.formsfx;
    requires org.kordamp.bootstrapfx.core;

    opens com.example.javafx_sample to javafx.fxml;
    exports com.example.javafx_sample;
}