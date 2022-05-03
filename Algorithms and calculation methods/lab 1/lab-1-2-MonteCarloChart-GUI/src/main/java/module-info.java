module com.example.lab12gui {
    requires javafx.controls;
    requires javafx.fxml;

    requires org.controlsfx.controls;
    requires com.dlsc.formsfx;
    requires org.kordamp.bootstrapfx.core;

    opens com.example.lab12gui to javafx.fxml;
    exports com.example.lab12gui;
}