using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Configuration;
using System.Text.RegularExpressions;

namespace MiniPaint
{


    //App allows User to draw basic Shapes ad use diffrent strokes for them with fill.It is using System.Windows.Shapes 

    public partial class MainWindow : Window
    {
      public static int StrokeVal = 2;
        public static string scb = "#0000FF";
 

        // Define enum for all shapes
        private enum MyShape
        {
            Line, Ellipse, Rectangle
        }

        // Define the current used shape
        private MyShape currShape;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LineButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Line;
        }

        private void EllipseButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Ellipse;
        }

        private void RectangleButton_Click(object sender, RoutedEventArgs e)
        {
            currShape = MyShape.Rectangle;
        }

        Point start;
        Point end;
         private void MyCanvas_MouseDown(object sender, MouseButtonEventArgs e) // Get X & Y possition from first clik of the mouse 
        {
            start = e.GetPosition(this);
        }

        private void MyCanvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Draw the chosen shape
            switch (currShape)
            {
                case MyShape.Line:
                    DrawLine();
                    break;

                case MyShape.Ellipse:
                    DrawEllipse();
                    break;

                case MyShape.Rectangle:
                    DrawRectangle();
                    break;

                default:
                    return;
            }
        }

        private void MyCanvas_MouseMove(object sender, MouseEventArgs e)
        {
            // Update the X & Y as the mouse moves
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                end = e.GetPosition(this);
            }
        }

        // Sets and draws line after mouse is released or clicked again
        private void DrawLine()
        {
            Line newLine = new Line()//string scb
            {
                Stroke = Brushes.Blue,
                StrokeThickness = StrokeVal,
                X1 = start.X,
                Y1 = start.Y - 50,
                X2 = end.X,
                Y2 = end.Y - 50
            };
            MyCanvas.Children.Add(newLine);
        }

        // Sets and draws ellipse after mouse is released
        private void DrawEllipse()
        {
            Ellipse newEllipse = new Ellipse()
            {
                Stroke = Brushes.Blue,
                Fill = Brushes.Red,
                StrokeThickness = StrokeVal,
                Height = 10,
                Width = 10
            };
            //Allows user to draw from any directions 
            //Don't allow user to draw on the Toolbar
            if (end.X >= start.X)
            {
                newEllipse.SetValue(Canvas.LeftProperty, start.X);
                newEllipse.Width = end.X - start.X;
            }
            else
            {
                newEllipse.SetValue(Canvas.LeftProperty, end.X);
                newEllipse.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                newEllipse.SetValue(Canvas.TopProperty, start.Y - 50);
                newEllipse.Height = end.Y - start.Y;
            }
            else
            {
                newEllipse.SetValue(Canvas.TopProperty, end.Y - 50);
                newEllipse.Height = start.Y - end.Y;
            }

            MyCanvas.Children.Add(newEllipse);
        }

        // Sets and draws rectangle after mouse is released
        private void DrawRectangle()
        {
            Rectangle newRectangle = new Rectangle()
            {
                Stroke = Brushes.Orange,
                Fill = Brushes.Blue,
                StrokeThickness = StrokeVal
            };

            if (end.X >= start.X)
            {
                // Defines the left part of the rectangle
                newRectangle.SetValue(Canvas.LeftProperty, start.X);
                newRectangle.Width = end.X - start.X;
            }
            else
            {
                newRectangle.SetValue(Canvas.LeftProperty, end.X);
                newRectangle.Width = start.X - end.X;
            }

            if (end.Y >= start.Y)
            {
                // Defines the top part of the rectangle
                newRectangle.SetValue(Canvas.TopProperty, start.Y - 50);
                newRectangle.Height = end.Y - start.Y;
            }
            else
            {
                newRectangle.SetValue(Canvas.TopProperty, end.Y - 50);
                newRectangle.Height = start.Y - end.Y;
            }

            MyCanvas.Children.Add(newRectangle);
        }

        private void LineButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void EllipseButton_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void RectangleButton_Click_1(object sender, RoutedEventArgs e)
        {

        }
   
        //
        private void Stroke_Colour_Click(object sender, RoutedEventArgs e)
        {
            ColourRGB Red = new ColourRGB
            {
                RGB = int.Parse(textBoxR.Text)
            };
            ColourRGB Green = new ColourRGB
            {
                RGB = int.Parse(textBoxG.Text)
            };
            ColourRGB Blue = new ColourRGB
            {
                RGB = int.Parse(textBoxB.Text)
            };
            SolidColorBrush scb = new SolidColorBrush(Color.FromRgb(Convert.ToByte(Red.RGB), Convert.ToByte(Green.RGB), Convert.ToByte(Blue.RGB)));
            btnStroke.Background = scb;

            textBoxR.Text = "0"; //clearing textBox
            textBoxG.Text = "0";
            textBoxB.Text = "0";

        }


        private class ColourRGB
        {
            private int _RGB = 0;
            //Method testing if Value parsed in the Texbox is in Range of RGB if it above 255 then it signed to 255 and if it is below 0 then it is equal to 0
            public int RGB    {
                get
                {
                    return _RGB;
                }
                set
                {
                    if (value <= -1)
                    {
                        _RGB = 255;  
                    }
                    else if (value >= 256)
                    {
                        _RGB = 255;
                    }
                    else { _RGB = value; }

                }
            }


        }

        private void textBoxR_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void textBoxG_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void textBoxB_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

     
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e) //Don't Allow letter and sign to Texbox
        {
        Regex regex = new Regex("[^0-9]+");
        e.Handled = regex.IsMatch(e.Text);
        }

        private void btnStrokeSize1(object sender, RoutedEventArgs e) //buttons to choose Stroke thockness of Line , Eclipse and Rectangle
        {
            StrokeVal = 2;

        }
        private void btnStrokeSize2(object sender, RoutedEventArgs e)
        {
            StrokeVal = 5;
        }
        private void btnStrokeSize3(object sender, RoutedEventArgs e)
        {
            StrokeVal = 8;
        }
        private void btnStrokeSize4(object sender, RoutedEventArgs e)
        {
            StrokeVal = 15;
        }


    }
}