using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Media3D;

namespace WPFGettingInto
{
    public class CameraController
    {
        MainWindow mainWindow;
        public Vector3D offsetDelta = new Vector3D(0, 0, 0);
        public double speed = 0.001;

        public CameraController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }

        public void KeyListener(object sender, EventArgs e)
        {
            ProjectionCamera camera = (ProjectionCamera)mainWindow.VP3D.Camera;
            offsetDelta = new Vector3D(0,0,0);
            if (Keyboard.IsKeyDown(Key.Q))
            {
                offsetDelta -= camera.UpDirection * speed;// new Point3D(position.X, position.Y- speed, position.Z);
            }
            if (Keyboard.IsKeyDown(Key.W))
            {
                offsetDelta +=  camera.LookDirection * speed;//new Point3D(position.X, position.Y, position.Z+ speed);
            }
            if (Keyboard.IsKeyDown(Key.E))
            {
                offsetDelta +=  camera.UpDirection * speed;//new Point3D(position.X, position.Y+ speed, position.Z);
            }
            if (Keyboard.IsKeyDown(Key.A))
            {
                offsetDelta -=  Vector3D.CrossProduct(camera.LookDirection, camera.UpDirection) * speed;//new Point3D(position.X+speed, position.Y, position.Z);
            }
            if (Keyboard.IsKeyDown(Key.S))
            {
                offsetDelta -= camera.LookDirection * speed;//new Point3D(position.X, position.Y, position.Z-speed);
            }
            if (Keyboard.IsKeyDown(Key.D))
            {
                offsetDelta += Vector3D.CrossProduct(camera.LookDirection, camera.UpDirection) * speed;//new Point3D(position.X-speed, position.Y, position.Z);
            }
            camera.Position += offsetDelta;
        }
    }
}
