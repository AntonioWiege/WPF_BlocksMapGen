using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPFGettingInto
{
    public partial class MainWindow : Window
    {
        public void Init_Window()
        {
            Debug.WriteLine("Starting Initializing Window");

            viewport_Related = new(VP3D, meshPrevMain, this);
            topThreeButtons = new TopThreeButtons(this);

            InitializeMouseEvents();
            InitializeUpdateLoop();
            InitializeModel();

        }
        private void InitializeMouseEvents()
        {
            MouseDown += MainWindow_MouseDown;
            MouseUp += MainWindow_MouseUp;
            MouseMove += MainWindow_MouseMove;
        }

        private void InitializeUpdateLoop()
        {
            CompositionTarget.Rendering += UpdateLoop;
        }

        private void InitializeModel()
        {

        }

    }
}
