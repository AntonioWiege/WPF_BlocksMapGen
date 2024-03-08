using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WPFGettingInto
{
    public partial class MainWindow : Window
    {

        public List<Action<object, MouseButtonEventArgs, Window>> OnMouseDown_CallTheseActions = new();
        public List<Action<object, MouseButtonEventArgs, Window>> OnMouseUp_CallTheseActions = new();
        public List<Action<object, MouseEventArgs, Window>> OnMouseMove_CallTheseActions = new();

        private void MainWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in OnMouseDown_CallTheseActions)
            {
                item(sender, e, this);
            }
        }

        private void MainWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in OnMouseUp_CallTheseActions)
            {
                item(sender, e, this);
            }
        }

        private void MainWindow_MouseMove(object sender, MouseEventArgs e)
        {
            foreach (var item in OnMouseMove_CallTheseActions)
            {
                item(sender, e, this);
            }
        }

        private void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (viewport_Related == null)
            {
                Debug.WriteLine("Viewport_Related is null; Returning.");
                return;
            }
            viewport_Related.MouseWheelDelta(sender, e);
        }

        void MouseViewportEnter(object sender, MouseEventArgs e)
        {
            if (viewport_Related == null)
            {
                Debug.WriteLine("Viewport_Related is null; Returning.");
                return;
            }
            viewport_Related.MouseViewportEnter(sender, e);
        }

        public void MouseViewportLeave(object sender, MouseEventArgs e)
        {
            if (viewport_Related == null)
            {
                Debug.WriteLine("Viewport_Related is null; Returning.");
                return;
            }
            viewport_Related.MouseViewportLeave(sender, e);
        }


        private void TogglePerspective(object sender, RoutedEventArgs e)
        {
            if (viewport_Related == null)
            {
                Debug.WriteLine("Viewport_Related is null; Returning.");
                return;
            }
            viewport_Related.TogglePerspective(sender, e);
        }

        private void ToggleOrthographic(object sender, RoutedEventArgs e)
        {
            if (viewport_Related == null)
            {
                Debug.WriteLine("Viewport_Related is null; Returning.");
                return;
            }
            viewport_Related.ToggleOrthographic(sender, e);
        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            if (topThreeButtons == null)
            {
                Debug.WriteLine("TopThreeButtons is null; Returning.");
                return;
            }
            topThreeButtons.btnClose_Click(sender, e);
        }

        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            if (topThreeButtons == null)
            {
                Debug.WriteLine("TopThreeButtons is null; Returning.");
                return;
            }
            topThreeButtons.btnMin_Click(sender, e);
        }

        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if (topThreeButtons == null)
            {
                Debug.WriteLine("TopThreeButtons is null; Returning.");
                return;
            }
            topThreeButtons.btnMax_Click(sender, e);
        }

        private void btnRegenerateMesh(object sender, RoutedEventArgs e)
        {
          if(viewport_Related!=null)  viewport_Related.GenMesh();
            CheckBox cb = (CheckBox)sender;
            cb.IsChecked = false;
        }
    }
}
