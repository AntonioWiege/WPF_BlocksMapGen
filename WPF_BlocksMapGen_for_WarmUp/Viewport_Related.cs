using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Reflection;
using System.Windows.Media;

namespace WPFGettingInto
{
    public class Viewport_Related
    {
        #region vars
        public MainWindow Mwindow;
        public Viewport3D VP3D;
        public MeshGeometry3D mesh;

        private bool isMouseInViewportArea = false;

        public CameraController cameraController;
        PerspectiveCamera? myPCamera;
        OrthographicCamera? myOCamera;

        DispatcherTimer keyboardTick;

        private Point lastMousePosition;
        private bool isMouseCaptured = false;

        int mouseWheelDelta = 0;
        #endregion
        #region init
        public Viewport_Related(Viewport3D vp3D, MeshGeometry3D meshPrevMain, MainWindow Mwindow)
        {
            this.Mwindow = Mwindow;
            VP3D = vp3D;
            mesh = meshPrevMain;
            cameraController = new CameraController(Mwindow);
            InitializeKeyboardTick();
            TogglePerspective(null, null);
            Mwindow.OnMouseDown_CallTheseActions.Add(MouseDown);
            Mwindow.OnMouseUp_CallTheseActions.Add(MouseUp);
            Mwindow.OnMouseMove_CallTheseActions.Add(MouseMove);
            GenMesh();
        }
        public void GenMesh()
        {
            MeshGeneration.GenerateMesh(mesh, (int)Mwindow.MapSize.Value, (int)Mwindow.MapSteps.Value);
        }
        void InitializeKeyboardTick()
        {
            keyboardTick = new DispatcherTimer(DispatcherPriority.Input);
            keyboardTick.Tick += new EventHandler(cameraController.KeyListener);
            keyboardTick.Interval = new TimeSpan(1000);
            keyboardTick.Start();
        }

    #endregion
    #region MouseRelated
    public void MouseViewportEnter(object sender, MouseEventArgs e)
    {
        isMouseInViewportArea = true;
    }
    public void MouseViewportLeave(object sender, MouseEventArgs e)
    {
        isMouseInViewportArea = false;
    }

    private void MouseDown(object sender, MouseButtonEventArgs e, Window window)
    {
        if (e.LeftButton == MouseButtonState.Pressed && isMouseInViewportArea)
        {
            lastMousePosition = e.GetPosition(window);
            window.CaptureMouse();
            isMouseCaptured = true;
        }
    }

    private void MouseUp(object sender, MouseButtonEventArgs e, Window window)
    {
        if (e.LeftButton == MouseButtonState.Released)
        {
            window.ReleaseMouseCapture();
            isMouseCaptured = false;
        }
    }

    private void MouseMove(object sender, MouseEventArgs e, Window window)
    {
        if (isMouseCaptured)
        {
            Point currentMousePosition = e.GetPosition(window);
            double deltaX = currentMousePosition.X - lastMousePosition.X;
            double deltaY = currentMousePosition.Y - lastMousePosition.Y;

            RotateCamera(deltaX, deltaY);

            lastMousePosition = currentMousePosition;
        }
    }

    public void MouseWheelDelta(object sender, MouseWheelEventArgs e)
    {
        mouseWheelDelta += e.Delta;
    }
    #endregion
    #region CamRelated
    private void RotateCamera(double deltaX, double deltaY)
    {
        ProjectionCamera camera = (ProjectionCamera)VP3D.Camera;
        Vector3D lookDirection = camera.LookDirection;
        Vector3D upDirection = camera.UpDirection;

        Vector3D yawAxis = new Vector3D(0, 1, 0);

        Vector3D pitchAxis = Vector3D.CrossProduct(lookDirection, upDirection);

        Matrix3D yawRotation = new Matrix3D();
        yawRotation.Rotate(new Quaternion(yawAxis, -deltaX * 0.5));
        lookDirection = yawRotation.Transform(lookDirection);
        upDirection = yawRotation.Transform(upDirection);

        Matrix3D pitchRotation = new Matrix3D();
        pitchRotation.Rotate(new Quaternion(pitchAxis, -deltaY * 0.5));
        lookDirection = pitchRotation.Transform(lookDirection);
        upDirection = pitchRotation.Transform(upDirection);

        camera.LookDirection = lookDirection;
        camera.UpDirection = upDirection;
    }

    public void TogglePerspective(object sender, RoutedEventArgs e)
    {
        myPCamera = new PerspectiveCamera((myPCamera==null)?new Point3D(0.5,3,-5): myPCamera.Position, (myPCamera != null) ? myPCamera.LookDirection : new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), 50 + mouseWheelDelta);
        VP3D.Camera = myPCamera;
    }
	
    public void ToggleOrthographic(object sender, RoutedEventArgs e)
    {
        myOCamera = new OrthographicCamera((myOCamera==null)?new Point3D(0.5,3,-5): myOCamera.Position, (myOCamera != null) ? myOCamera.LookDirection : new Vector3D(0, 0, 1), new Vector3D(0, 1, 0), 3 + mouseWheelDelta * .1);
        VP3D.Camera = myOCamera;
    }
    #endregion
}
}
