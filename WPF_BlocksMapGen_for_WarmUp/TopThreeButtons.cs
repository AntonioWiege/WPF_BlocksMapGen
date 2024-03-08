using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WPFGettingInto
{
    class TopThreeButtons
    {
        Window window { get; set; }

        public TopThreeButtons(Window window) { 
        this.window = window;
        }    

        public void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Close Button Clicked");
            window.Close();
        }

        public void btnMin_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Minimize Button Clicked");
            window.WindowState = WindowState.Minimized;
        }

        public void btnMax_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Maximize Button Clicked");
            if (window.WindowState == WindowState.Maximized)
            {
                window.WindowState = WindowState.Normal;
            }
            else
            {
                window.WindowState = WindowState.Maximized;
            }
        }
    }
}
