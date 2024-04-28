package jcad.jncad;

import javafx.application.Application;
import javafx.geometry.Point2D;
import javafx.geometry.Point3D;
import javafx.scene.*;
import javafx.scene.input.*;
import javafx.scene.paint.Color;
import javafx.scene.shape.Shape;
import javafx.scene.shape.Shape3D;
import javafx.scene.transform.Rotate;
import javafx.scene.transform.Translate;
import javafx.stage.Stage;
import javafx.scene.shape.Sphere;

import java.io.IOException;

public class JnCad extends Application {
    public static final int WIN_WIDTH = 1600;
    public static final int WIN_HEIGHT = 900;

    PerspectiveCamera __camera;
    private Point3D __focusPoint = new Point3D(0, 0, 0);
    private Point2D __mpos = new Point2D(0, 0);
    private boolean __dragging = false;
    private double __dist = 0.0;
    private Shape3D __selected = null;
    private Point3D __vect0, __vectPos;
    private final Rotate rotX = new Rotate(-20, Rotate.X_AXIS);
    private final Rotate rotY = new Rotate(-20, Rotate.Y_AXIS);

    @Override
    public void start(Stage stage) throws IOException {

        __camera = new PerspectiveCamera(true);
        __camera.setFarClip(100000.0);
        __camera.setNearClip(0.1);
        __camera.setVerticalFieldOfView(false);
        __camera.translateXProperty().set((double) WIN_WIDTH /2);
        __camera.translateYProperty().set((double) WIN_HEIGHT /2);
        __camera.translateZProperty().set(-1000);
        __focusPoint = new Point3D(
                __camera.getBoundsInParent().getCenterX(),
                __camera.getBoundsInParent().getCenterY(),
                __camera.getBoundsInParent().getCenterZ()+500);
        
        Sphere sphere = new Sphere(50);

        Group group = new Group();
        group.getChildren().add(sphere);

        sphere.translateXProperty().set((double) WIN_WIDTH /2);
        sphere.translateYProperty().set((double) WIN_HEIGHT /2);

        stage.addEventHandler(KeyEvent.KEY_PRESSED, e ->{
            switch (e.getCode()){
                case ADD:
                    __camera.translateZProperty().set(__camera.getTranslateZ() + 10); break;
                case SUBTRACT:
                    __camera.translateZProperty().set(__camera.getTranslateZ() - 10); break;
            }

            System.out.printf("CAM: %f, %f, %f | SEL: %f, %f, %f\n",
                    __camera.getTranslateX(), __camera.getTranslateY(), __camera.getTranslateZ(),
                    sphere.getTranslateX(), sphere.getTranslateY(), sphere.getTranslateZ());
        });

        stage.addEventHandler(ScrollEvent.SCROLL, e-> {
            __camera.translateZProperty().set(__camera.getTranslateZ() + e.getDeltaY());
            System.out.printf("CAM: %f, %f, %f | SEL: %f, %f, %f\n",
                    __camera.getTranslateX(), __camera.getTranslateY(), __camera.getTranslateZ(),
                    sphere.getTranslateX(), sphere.getTranslateY(), sphere.getTranslateZ());
        });

        Scene scene = getScene(group, sphere);

        stage.setTitle("NCAD");
        stage.setScene(scene);
        stage.show();

//        FXMLLoader fxmlLoader = new FXMLLoader(JnCad.class.getResource("hello-view.fxml"));
//        Scene scene = new Scene(fxmlLoader.load(), 320, 240);
//        stage.setTitle("Hello!");
//        stage.setScene(scene);
//        stage.show();

        System.out.printf("CAM: %f, %f\n", __camera.getTranslateX(), __camera.getTranslateY());
        System.out.printf("SEL: %f, %f\n", sphere.getTranslateX(), sphere.getTranslateY());
    }

    private Scene getScene(Group group, Sphere sphere) {
        Scene scene = new Scene(group, WIN_WIDTH, WIN_HEIGHT, true, SceneAntialiasing.BALANCED);
        scene.setCamera(__camera);
        scene.setFill(Color.DARKGRAY);

        scene.setOnMousePressed((MouseEvent e) ->{
            __mpos = new Point2D(e.getSceneX(), e.getSceneY());

            if(e.getButton() == MouseButton.PRIMARY){
                PickResult pr = e.getPickResult();
                if(pr!=null && pr.getIntersectedNode() != null && pr.getIntersectedNode() instanceof Sphere){
                    __dist = pr.getIntersectedDistance();
                    __selected = (Shape3D)pr.getIntersectedNode();
                    __dragging = true;
                    __vect0 = UnProjectDir(__mpos.getX(), __mpos.getY(), scene.getWidth(), scene.getHeight());
                }
            } else if (e.isMiddleButtonDown()){
                if (e.isShiftDown()){
                    __dist = __focusPoint.distance(__camera.getTranslateX(), __camera.getTranslateY(), __camera.getTranslateY());
                    __dragging = true;
                    __vect0 = UnProjectDir(__mpos.getX(), __mpos.getY(), scene.getWidth(), scene.getHeight());
                } else {
                    //Rotate
                }
            }


        });

        scene.setOnMouseDragged((MouseEvent e) -> {
            Point2D mpoint = new Point2D(e.getSceneX(), e.getSceneY());
            if (__dragging){

                if (__selected != null){
                    __vectPos = UnProjectDir(mpoint.getX(), mpoint.getY(), scene.getWidth(), scene.getHeight());
                    Point3D p = __vectPos.subtract(__vect0).multiply(__dist);
                    __selected.getTransforms().add(new Translate(p.getX(), p.getY(), p.getZ()));
                    __vect0 = __vectPos;
                    PickResult pr = e.getPickResult();
                    if(pr!=null && pr.getIntersectedNode() != null && pr.getIntersectedNode() == __selected){
                        __dist = pr.getIntersectedDistance();
                    } else {
                        __dragging = false;
                    }
                } else {
                    __vectPos = UnProjectDir(mpoint.getX(), mpoint.getY(), scene.getWidth(), scene.getHeight());
                    Point3D p = __vectPos.subtract(__vect0).multiply(__dist);
                    __focusPoint.add(new Point3D(-p.getX(), -p.getY(), p.getZ()));
                    __camera.getTransforms().add(new Translate(-p.getX(), -p.getY(), p.getZ()));
                }

                System.out.printf("CAM: %f, %f, %f | SEL: %f, %f, %f\n",
                        __camera.getBoundsInParent().getCenterX(), __camera.getBoundsInParent().getCenterY(), __camera.getBoundsInParent().getCenterZ(),
                        sphere.getBoundsInParent().getCenterX(), sphere.getBoundsInParent().getCenterY(), sphere.getBoundsInParent().getCenterZ());
            } else {
                rotX.setAngle(rotX.getAngle()-(mpoint.getX() - __mpos.getX()));
                rotY.setAngle(rotY.getAngle()+(mpoint.getY() - __mpos.getY()));
                __mpos = mpoint;
            }
        });

        scene.setOnMouseReleased((MouseEvent e) -> {
            if(__dragging){ __dragging = false; }
            __selected = null;
        });
        return scene;
    }

    private Point3D UnProjectDir(double sceneX, double sceneY, double sWidth, double sHeight){
        double tHFov = Math.tan(Math.toRadians(__camera.getFieldOfView()) / 2);
        Point3D vMs = new Point3D(tHFov*(2*sceneX/sWidth-1), tHFov*(2*sceneY/sWidth-sHeight/sWidth), 1);
        Point3D res = localToSceneDir(vMs);
        return res.normalize();
    }

    private Point3D localToScene(Point3D pt){
        Point3D res = __camera.localToParentTransformProperty().get().transform(pt);
        if(__camera.getParent() != null){
            res = __camera.getParent().localToSceneTransformProperty().get().transform(res);
        }

        return res;
    }

    private Point3D localToSceneDir(Point3D dir){
        Point3D res = localToScene(dir);
        return res.subtract(localToScene((new Point3D(0,0, 0))));
    }

    public static void main(String[] args) {
        launch();
    }

    //ref- remove

}