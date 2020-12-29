using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace tastyProject
{
    /// <summary>
    /// Interaction logic for Search.xaml
    /// </summary>
    public partial class Search : Window
    {
        double m_MouseX;
        double m_MouseY;
        public Search()
        {
            InitializeComponent();
            button1.PreviewMouseUp += new MouseButtonEventHandler(button1_MouseUp);
            button1.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(button1_MouseLeftButtonUp);
            button1.PreviewMouseMove += new MouseEventHandler(button1_MouseMove);

            button2.PreviewMouseUp += new MouseButtonEventHandler(button1_MouseUp);
            button2.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(button1_MouseLeftButtonUp);
            button2.PreviewMouseMove += new MouseEventHandler(button1_MouseMove);

            button3.PreviewMouseUp += new MouseButtonEventHandler(button1_MouseUp);
            button3.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(button1_MouseLeftButtonUp);
            button3.PreviewMouseMove += new MouseEventHandler(button1_MouseMove);

            button4.PreviewMouseUp += new MouseButtonEventHandler(button1_MouseUp);
            button4.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(button1_MouseLeftButtonUp);
            button4.PreviewMouseMove += new MouseEventHandler(button1_MouseMove);
        }
        private void button1_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Get the Position of Window so that it will set margin from this window
            m_MouseX = e.GetPosition(this).X;
            m_MouseY = e.GetPosition(this).Y;
        }

        private void button1_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            Button b = (Button)sender;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // Capture the mouse for border
                e.MouseDevice.Capture(b);
                System.Windows.Thickness _margin = new System.Windows.Thickness();
                int _tempX = Convert.ToInt32(e.GetPosition(this).X);
                int _tempY = Convert.ToInt32(e.GetPosition(this).Y);
                _margin = b.Margin;
                // when While moving _tempX get greater than m_MouseX relative to usercontrol 
                if (m_MouseX > _tempX)
                {
                    // add the difference of both to Left
                    _margin.Left += (_tempX - m_MouseX);
                    // subtract the difference of both to Left
                    _margin.Right -= (_tempX - m_MouseX);
                }
                else
                {
                    _margin.Left -= (m_MouseX - _tempX);
                    _margin.Right -= (_tempX - m_MouseX);
                }
                if (m_MouseY > _tempY)
                {
                    _margin.Top += (_tempY - m_MouseY);
                    _margin.Bottom -= (_tempY - m_MouseY);
                }
                else
                {
                    _margin.Top -= (m_MouseY - _tempY);
                    _margin.Bottom -= (_tempY - m_MouseY);
                }
                b.Margin = _margin;
                m_MouseX = _tempX;
                m_MouseY = _tempY;
            }
        }
        private void button1_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Button b = (Button)sender;
            e.MouseDevice.Capture(null);
            double x = e.GetPosition(this).X;
            double y = e.GetPosition(this).Y;
            Point relativePoint = img.TranslatePoint(new Point(0, 0), this);
           // if (x >= relativePoint.X && x<=relativePoint.X+img.Width)
               // if (y >= relativePoint.Y && y<=relativePoint.Y+img.Height)
               // {
                    //adding to basket list
                    b.Visibility = Visibility.Collapsed;
                //}
        }

        private void button1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (System.Windows.Input.Mouse.LeftButton == MouseButtonState.Pressed)
                return;

        }


    }
}
