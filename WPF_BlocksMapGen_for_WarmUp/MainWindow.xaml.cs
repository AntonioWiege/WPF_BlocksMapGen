using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
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
using System.Windows.Threading;

namespace WPFGettingInto
{
 
    public partial class MainWindow : Window
    {
        TopThreeButtons topThreeButtons;
        public Viewport_Related viewport_Related;

        public double startAnimationValue=1;

        public MainWindow()
        {

            FontFamily = new FontFamily("Lato");
            FontWeight = FontWeights.ExtraLight;

            InitializeComponent();
            Init_Window();

            Debug.WriteLine("Finished Initializing Main Window");
        }

        private void UpdateLoop(object sender, EventArgs e)
        {
        }

    }
}
