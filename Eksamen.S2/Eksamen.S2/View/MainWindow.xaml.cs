using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Eksamen.S2.Controller;

namespace Eksamen.S2.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Commander c = new Commander();
        bool isClosed = false;
        Canvas canvas = new Canvas
        {
            Background = Brushes.LightGray,
            Width = 700,
            Height = 450,
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Initialized(object sender, EventArgs e)
        {
            OnStartUp();
        }

        Rectangle playerSquare = new Rectangle();
        /// <summary>
        /// Occurs when booting up the application.
        /// </summary>
        private void OnStartUp()
        {
            Grid.SetZIndex(canvas, -10);
            grid.Children.Add(canvas);

            playerSquare = new Rectangle
            {
                Fill = Brushes.Red,
                Height = 20,
                Width = 20,
                Margin = new Thickness(0, 0, 100, 0),
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Bottom
            };
            Canvas.SetTop(playerSquare, (canvas.Height / 2) - playerSquare.Height);
            Canvas.SetLeft(playerSquare, canvas.Width / 2.5);
            canvas.Children.Add(playerSquare);
        }

        private void PlayerMovement()
        {
            bool wait = false;
            double a = 0;
            double y;
            bool jump = false;
            while (!isClosed)
            {
                this.Dispatcher.Invoke(() =>
                {
                    lblScore.Content = c.GetScore();
                    y = Canvas.GetTop(playerSquare);
                    if (y <= 0)
                    {
                        Canvas.SetTop(playerSquare, 0);
                    }

                    // Variable 'drop' is used to provide an acceleration scalar for falling
                    double drop = (double)1 / 1000000;

                    // Jump timer determines how long it takes for the jump to end, but barely affects height if at all.
                    int jumpTimer = 1000001;
                    // Jump cooldown determines how long until you can jump again.
                    int jumpCooldown = 7001;

                    if (a == jumpTimer)
                    {
                        jump = false;
                    }
                    if (a == jumpCooldown)
                    {
                        wait = false;
                    }
                    // SetTop method used here to accelerate the square downward, even while jumping.
                    Canvas.SetTop(playerSquare, y + (drop * a));

                    // Temporary if-statement that would stop the game.
                    // For now only sends the player back to the middle of the play area.
                    if (y >= canvas.Height - playerSquare.Height)
                    {
                        Canvas.SetTop(playerSquare, (canvas.Height / 2) - playerSquare.Height);
                    }

                    y = Canvas.GetTop(playerSquare);
                    if (Keyboard.IsKeyDown(Key.Up) && y > 0 && !wait)
                    {
                        // Reset variable 'a' for timers.
                        a = 1;
                        // Start jumping and prevent another jump until cooldown is up.
                        jump = true;
                        wait = true;
                        //c.AddPoints(10);
                    }

                    // Will loop alongside the falling acceleration.
                    if (jump)
                    {
                        double minAccel = 50;
                        double baseHeight = 20;
                        // The higher 'minAccel' is, the weaker the initial boost of the jump is.
                        // The value 'baseHeight' determines how much height the square will gain.
                        Canvas.SetTop(playerSquare, y - (baseHeight / (a + minAccel)));
                    }
                    // Increment 'a' for acceleration.
                    a++;
                });
            }
        }

        private void Program_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            isClosed = true;
        }

        private void ContinueToGame_Click(object sender, RoutedEventArgs e)
        {
            c.UpdateCurrentPlayer(tbxUsername.Text, tbxPhoneNumber.Text, tbxEmail.Text);
            grdInputUserValues.Visibility = Visibility.Collapsed;
            lblScore.Visibility = Visibility.Visible;

            Thread movement = new Thread(PlayerMovement);
            movement.Start();
        }

    }
}
