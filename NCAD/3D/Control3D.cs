
using NCAD.Model;
using System.Data;
using System.Windows;
using System.Windows.Media.Media3D;

namespace NCAD._3D
{
    public abstract class Control3D(IControlModel model) : ModelVisual3D
    {
        protected IControlModel CModel { get; set; } = model;

        //public Point3D Origin { get; set; } = new Point3D();
        //private double __translateX { get; set; } = 0.0;
        //private double __translateY { get; set; } = 0.0;
        //private double __translateZ { get; set; } = 0.0;
    
        
        //public static readonly DependencyProperty TranslateXProperty = 
        //    DependencyProperty.Register("TranslateX", typeof(double), typeof(Control3D), 
        //    new PropertyMetadata(0.0));
        //public static readonly DependencyProperty TranslateYProperty =
        //    DependencyProperty.Register("TranslateY", typeof(double), typeof(Control3D),
        //    new PropertyMetadata(0.0));
        //public static readonly DependencyProperty TranslateZProperty =
        //    DependencyProperty.Register("TranslateZ", typeof(double), typeof(Control3D),
        //    new PropertyMetadata(0.0));
    
        
        //public double TranslateX
        //{
        //    get { return (double)GetValue(TranslateXProperty); }
        //    set { SetValue(TranslateXProperty, value); __translateX = value; }
        //}

        //public double TranslateY
        //{
        //    get { return (double)GetValue(TranslateYProperty); }
        //    set { SetValue(TranslateYProperty, value); __translateY = value; }
        //}

        //public double TranslateZ
        //{
        //    get { return (double)GetValue(TranslateZProperty); }
        //    set { SetValue(TranslateZProperty, value); __translateZ = value; }
        //}

        //public static readonly DependencyProperty Model3DProperty =
        //    DependencyProperty.Register("Model3D", typeof(ModelVisual3D), typeof(Control3D),
        //    new UIPropertyMetadata(null, ModelStateChanged));

        //public ModelVisual3D ModelProperty
        //{
        //    get { return (ModelVisual3D)GetValue(Model3DProperty); }
        //    set { SetValue(Model3DProperty, value); }
        //}

        //public static readonly DependencyProperty RotationAngleProperty =
        //    DependencyProperty.Register("RotationAngle", typeof(double), typeof(Model3D),
        //        new UIPropertyMetadata(45.0));
        //public double RotationAngle
        //{
        //    get { return (double)GetValue(RotationAngleProperty); }
        //    set { SetValue(RotationAngleProperty, value); }
        //}

        //public static AxisAngleRotation3D AxisAngleRotation = new AxisAngleRotation3D(new Vector3D(1, 0, 0), 0);

        //public static readonly DependencyProperty TorusAxisAngleProperty =
        //            DependencyProperty.Register("AxisAngle", typeof(AxisAngleRotation3D), 
        //                typeof(Model3D),
        //            new UIPropertyMetadata(AxisAngleRotation));
        //public AxisAngleRotation3D AxisAngle
        //{
        //    get { return (AxisAngleRotation3D)GetValue(TorusAxisAngleProperty); }
        //    set { SetValue(TorusAxisAngleProperty, value); }
        //}

        //#region Callback

        //protected static void ModelStateChanged(DependencyObject obj, 
        //    DependencyPropertyChangedEventArgs args)
        //{
        //    ((Control3D)obj).Update();
        //}

        //public void Update()
        //{
        //    var axis = new Vector3D(0,1,0);
        //    var matrix = ModelProperty.Content.Transform.Value;
        //    matrix.Rotate(new Quaternion(axis, RotationAngle));
        //    matrix.Transform(new Vector3D(TranslateX, TranslateY, TranslateZ));
        //    ModelProperty.Content.Transform = new MatrixTransform3D(matrix);
        //}

        //#endregion

    }
}
