using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Background;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Shapes
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        //global variables
        int _rows;
        int _height = 80;
        int _width = 80;

        public MainPage()
        {
            this.InitializeComponent();
        }

        //event handler
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // local variable for the sender
            RadioButton current = (RadioButton)sender;
            _rows = Convert.ToInt32(current.Tag);
            createGrid();
            setupTheCircle();
            createRectangle();
        }
        #region Circle/Ellipse
        private void setupTheCircle()
        {
            // add event handler to el1
            el1.Tapped += El1_Tapped;
        }
        //event handler
        private void El1_Tapped(object sender, TappedRoutedEventArgs e)
        {

            el1.Height = 40;
            el1.Width = 40;
            Color[] circleColors =
            {Colors.Red, Colors.Yellow, Colors.Pink,
             Colors.Green, Colors.Orange, Colors.Purple, Colors.Blue};

            //randomises the circle so that whenever it is tapped one of the above colors are chosen
            Random random = new Random();
            int i = random.Next(0, 6);
            el1.Fill = new SolidColorBrush(circleColors[i]);
            //try catch so function can be carried out continously
            try
            {
                rootGrid.Children.Remove(FindName("el1") as Ellipse);
            }
            catch
            {
            }

        }
        #endregion

        private void createGrid()
        {
            //try catch so function can be carried out continously
            try
            {
                rootGrid.Children.Remove(FindName("grd") as Grid);
            }
            catch
            {
            }
            #region create a grid object
            Grid grdBoard = new Grid();
            grdBoard.Name = "grd";
            grdBoard.HorizontalAlignment = HorizontalAlignment.Center;
            grdBoard.VerticalAlignment = VerticalAlignment.Top;
            grdBoard.Height = _height * _rows;
            grdBoard.Width = _width * _rows;
            grdBoard.Background = new SolidColorBrush(Colors.Green);
            grdBoard.Margin = new Thickness(3);
            grdBoard.SetValue(Grid.RowProperty, 1);
            #endregion

            // add _rows number of row definitions and column definitions
            for (int i = 0; i < _rows; i++)
            {
                grdBoard.RowDefinitions.Add(new RowDefinition());
                grdBoard.ColumnDefinitions.Add(new ColumnDefinition());
            }

            // add the grid to the root grid children collection
            rootGrid.Children.Add(grdBoard);
            // create the border object
            Border brdr; // don't put this in the loop

            int iR, iC;

            for (iR = 0; iR < _rows; iR++) // on each row
            {
                for (iC = 0; iC < _rows; iC++)  // for each col on that row
                {
                    #region Create one border element and add to the grid
                    brdr = new Border();
                    // give it height, width, horizontal & vertical align in centre
                    brdr.Height = _height * 1.2;
                    brdr.Width = _width * 1.2;
                    brdr.HorizontalAlignment = HorizontalAlignment.Center;
                    brdr.VerticalAlignment = VerticalAlignment.Center;
                    // set the Grid.col, grid.row property
                    brdr.SetValue(Grid.RowProperty, iR);
                    brdr.SetValue(Grid.ColumnProperty, iC);

                    //show the individual squares
                    if (1 == (iR + iC) % 2)
                    {
                        brdr.Background = new SolidColorBrush(Colors.White);
                    }
                    //add it to the grid
                    grdBoard.Children.Add(brdr);
                    #endregion
                } // end iC
            } // end of iR

        }//end of create grid

        #region Rectangle 
        private void createRectangle()
        {
            rect1.Tapped += Rect1_Tapped;
        }
        //event handler
        private void Rect1_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int i;
            Random random = new Random();
            i = random.Next(1, 4);
            //if statements to determine what the rectangle looks like
            if (i == 1)
            {
                rect1.Fill = new SolidColorBrush(Colors.Black);
                rect1.Stroke = new SolidColorBrush(Colors.Yellow);
                rect1.StrokeThickness = 3;
                rect1.RadiusX = 50;
                rect1.RadiusY = 10;
            }
            else if (i == 2)
            {
                rect1.Fill = new SolidColorBrush(Colors.Gold);
                rect1.Stroke = new SolidColorBrush(Colors.Black);
                rect1.StrokeThickness = 2;
            }
            else if (i == 3)
            {
                rect1.Fill = new SolidColorBrush(Colors.Cyan);
                rect1.Stroke = new SolidColorBrush(Colors.Coral);
                rect1.StrokeThickness = 4;
            }
            else
            {

            }
        }
        #endregion
        private void OnCanceled(IBackgroundTaskInstance sender, BackgroundTaskCancellationReason reason)
        {


            //
            // Indicate that the background task is canceled.
            //

            Debug.WriteLine("Background " + sender.Task.Name + " Cancel Requested...");
        }
    }

}

