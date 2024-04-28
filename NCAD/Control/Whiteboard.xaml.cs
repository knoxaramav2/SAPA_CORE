using HelixToolkit.Wpf;
using NCAD._3D;
using NCAD.Model;
using NCAD.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using XNAControls;

namespace NCAD.Control
{
    /// <summary>
    /// Interaction logic for Whiteboard.xaml
    /// </summary>
    public partial class Whiteboard : UserControl
    {
        private Dictionary<string, Model3DGroup> __models = [];
        private List<Control3D> __selected = [];
        private List<Control3D> __circuitItems = [];
        private Model3D? __grabbed = null;
        private double __grabbedDist = 1.0f;
        private Point __lastMouse = new Point(0, 0);

        public Whiteboard()
        {
            InitializeComponent();
            Setup();
        }

        #region setup controls

        private void Setup()
        {
            PreloadModels();
            Load3DScene();
        }

        private void SetModelColor(Model3DGroup model, Color color)
        {
            var geom = model.Children[0] as GeometryModel3D ?? throw new Exception("Material not set");
            var diffMat = new DiffuseMaterial(new SolidColorBrush(color));
            geom.Material = diffMat;
        }

        private void PreloadModels()
        {
            var modelFiles = FileUtil.ListFiles(FileUtil.ModelUri, "*.obj");
            __models = [];

            var importer = new ModelImporter();

            foreach (var file in modelFiles)
            {
                var model = importer.Load(file);
                var key = System.IO.Path.GetFileNameWithoutExtension(file).ToLower();
                __models.Add(key, model);
            }
        }

        private Control3D LoadModel<T>(T model, string modelName)where T: IControlModel
        {
            Control3D ret;
            switch (model)
            {
                case Neuron neuron:
                    ret = new Neuron3D(neuron);
                    break;
                default:
                    throw new Exception("Invalid model");
            }

            //TODO Make static, perform in constructor
            ret.Content = __models[modelName.ToLower()];
            return ret;
        }

        public void Load3DScene()
        {
            __selected.Add(SpawnObject(new Neuron(), Colors.Blue));
        }

        Control3D SpawnObject(IControlModel model, Color color)
        {
            string modelName = model switch
            {
                Neuron _ => "sphere",
                _ => throw new Exception("Invalid type"),
            };
            var model3d = LoadModel(model, modelName);
            SetModelColor((Model3DGroup)model3d.Content, color);
            Viewport.Children.Add(model3d);
            __circuitItems.Add(model3d);
            return model3d;
        }

        #endregion

        #region events

        private void Viewport_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var msLoc = e.GetPosition(Viewport);
            var hit = new PointHitTestParameters(msLoc);
            var res = VisualTreeHelper.HitTest(Viewport, msLoc);

            Debug.WriteLine(res);
            var meshRes = res as RayMeshGeometry3DHitTestResult;
            if (meshRes != null)
            {
                __grabbed = meshRes.ModelHit;
                if (__grabbedDist == 1.0f) { __grabbedDist = meshRes.DistanceToRayOrigin; }
                Debug.WriteLine("Hit!");
            }
            else
            {
                __grabbed = null;
            }
        }

        static Point LastMouse = new Point();
        static Point LastP2S = new Point();
        static Point LastW = new Point();

        private Point ModelPosToScreen(Model3D model)
        {
            
            if (model == null) { return new Point(); }
            var m = model.Transform.Value;
            var point = new Point3D(m.OffsetX, m.OffsetY, m.OffsetZ);
            var matrix = Viewport.Viewport.GetTotalTransform();
            matrix.Invert();
            var spos = matrix.Transform(point);

            return new Point(spos.X, spos.Y);
        }

        private void Viewport_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = Viewport.PointFromScreen(e.GetPosition(Viewport));

            if (__grabbed != null)
            {
                var dpos = pos - LastMouse;
                var np = Calc3DFromScreenPoint(new Point(pos.X, -pos.Y), __grabbedDist*2.50);
                Debug.WriteLine($"{np} -> {__grabbedDist}");
                if (true)
                {
                    np.X = Math.Clamp(np.X, -10, 10);
                    np.Y = Math.Clamp(np.Y, -10, 10);
                    np.Z = Math.Clamp(np.Z, -10, 10);
                    np.Z = 0;
                }
                
                MoveTo(__grabbed, np.X, np.Y, np.Z);
                //MoveTo(__grabbed, 5, 5, 5);
            }

            LastMouse = pos;
        }

        private Point Point3DToScreen(Point3D point)
        {
            var cam = (PerspectiveCamera)Viewport.Camera;
            var iX = point.X - cam.Position.X;
            var iY = point.Y - cam.Position.Y;
            var iZ = point.Z - cam.Position.Z;
            var r = ActualWidth / ActualHeight;
            var f = cam.FieldOfView / 2;

            var sX = (iX / (-iZ*Math.Tan(f))) * Viewport.ActualWidth;
            var sY = (1-((iY*r) / (-iZ*Math.Tan(f)))) * Viewport.ActualHeight;

            return new Point(sX, sY);
        }

        private Point3D Calc3DFromScreenPoint(Point point, double dist)
        {
            var cam = (PerspectiveCamera)Viewport.Camera;

            //Camera plane
            var look = cam.LookDirection;

            return new Point3D();
        }

        private Vector3D MousePosToViewport(Point mouse)
        {
            //var mouse3d = new Point3D(mouse.X, mouse.Y, 0);
            //var mouseDir = (Vector3D)Viewport.PointToScreen(mouse3d);

            Point3D mousePoint3D = new Point3D(mouse.X, mouse.Y, 0);

            //Vector3D mouseDirection = (Vector3D)Viewport.PointToScreen(mouse) - (Vector3D)Viewport.PointToScreen(new Point(0, 0));
            var x = new Vector3D();
            
            
            var plane = new Plane3D(new Point3D(0, 0, 0), new Vector3D(0, 0, 1));
            //var ray = new Ray3D((Point3D)Viewport.Camera.Position, );

            return x;
        }

        private void Viewport_MouseUp(object sender, MouseButtonEventArgs e)
        {
            __grabbed = null;
        }

        private void Viewport_MouseEnter(object sender, MouseEventArgs e)
        {
            __lastMouse = e.GetPosition(Viewport);
        }

        #endregion

        #region helpers

        private void MoveTo(Model3D model, double x, double y, double z)
        {
            //var orig = model.Transform;
            //orig.Transform(new Point3D(x, y, z));
            //var group = new Transform3DGroup();
            //group.Children.Add(orig);
            //model.Transform = group;
            var transGroup = new Transform3DGroup();

            //var rot = new RotateTransform3D();
            //var axis = new AxisAngleRotation3D();
            //axis.Axis = new Vector3D(x, y, z);
            //axis.Angle = 40;
            //rot.Rotation = axis;

            var trans = new TranslateTransform3D(x, y, z);

            //transGroup.Children.Add(rot);
            transGroup.Children.Add(trans);
            Debug.WriteLine($"X:{x}, Y:{y} Z:{z}");

            model.Transform = trans;
        }

        private void Translate(Model3D model, double x, double y, double z)
        {
            var transGroup = new Transform3DGroup();

            var rot = new RotateTransform3D();
            var axis = new AxisAngleRotation3D();
            axis.Axis = new Vector3D(x, y, z);
            axis.Angle = 40;
            rot.Rotation = axis;

            var trans = new TranslateTransform3D(
                model.Transform.Value.OffsetX + x,
                model.Transform.Value.OffsetY + y,
                model.Transform.Value.OffsetZ + z);

            //transGroup.Children.Add(rot);
            transGroup.Children.Add(trans);
            Debug.WriteLine($"X:{x}, Y:{y} Z:{z}");

            model.Transform = trans;
        }


        #endregion

        private void Viewport_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Add)
            {
                foreach(var s in __selected) { Translate(s.Content, 1, 0, 0); }
            } else if (e.Key == Key.Subtract)
            {
                foreach (var s in __selected) { Translate(s.Content, -1, 0, 0); }
            }
        }
    }
}
