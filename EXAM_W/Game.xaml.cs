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
using System.Windows.Threading;
using static System.Formats.Asn1.AsnWriter;

namespace EXAM_W
{
    /// <summary>
    /// Interaction logic for Game.xaml
    /// </summary>
    public partial class Game : Window
    {
        private int scorePlayer1 = 3;
        private int scorePlayer2 = 3;
        private DispatcherTimer timer;
        public Game()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            ContentRendered += MainWindow_ContentRendered;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(8);
            timer.Tick += Timer_Tick;
        }

        private double ballSpeedX = 5;
        private double ballSpeedY = 5;
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(ball, canvas.ActualWidth / 2 - ball.Width / 2);
            Canvas.SetTop(ball, canvas.ActualHeight / 2 - ball.Height / 2);

            Canvas.SetLeft(paddle1, 50);
            Canvas.SetTop(paddle1, canvas.ActualHeight / 2 - paddle1.Height / 2);

            Canvas.SetLeft(paddle2, canvas.ActualWidth - 50 - paddle2.Width);
            Canvas.SetTop(paddle2, canvas.ActualHeight / 2 - paddle2.Height / 2);

            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
            timer.Start();

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            Canvas.SetLeft(ball, Canvas.GetLeft(ball) + ballSpeedX);
            Canvas.SetTop(ball, Canvas.GetTop(ball) + ballSpeedY);

            Player1.Text = scorePlayer1.ToString();
            Player2.Text = scorePlayer2.ToString();

            if (Canvas.GetLeft(ball) < 0)
            {
                ballSpeedX = -ballSpeedX;
                Canvas.SetTop(ball, Math.Max(0, Math.Min(canvas.ActualHeight - ball.Height, Canvas.GetTop(ball))));

                scorePlayer1--;
                UpdateScores();
                if (scorePlayer1 == 0) 
                {                   
                    ShowGameOverDialog();
                }                    
                
            }
            else if (Canvas.GetLeft(ball) > canvas.ActualWidth - ball.Width)
            {
                ballSpeedX = -ballSpeedX;
                Canvas.SetTop(ball, Math.Max(0, Math.Min(canvas.ActualHeight - ball.Height, Canvas.GetTop(ball))));

                scorePlayer2--;
                UpdateScores();
                if (scorePlayer2 == 0)
                {                   
                    ShowGameOverDialog();
                }
            }

            if (Canvas.GetTop(ball) < 0 || Canvas.GetTop(ball) > canvas.ActualHeight - ball.Height)
            {
                ballSpeedY = -ballSpeedY;
                Canvas.SetTop(ball, Math.Max(0, Math.Min(canvas.ActualHeight - ball.Height, Canvas.GetTop(ball))));             
            }         

            ProcessPaddleMovement();

            if (Canvas.GetLeft(ball) < Canvas.GetLeft(paddle1) + paddle1.Width &&
                Canvas.GetTop(ball) + ball.Height > Canvas.GetTop(paddle1) &&
                Canvas.GetTop(ball) < Canvas.GetTop(paddle1) + paddle1.Height)
                ballSpeedX = -ballSpeedX;

            if (Canvas.GetLeft(ball) + ball.Width > Canvas.GetLeft(paddle2) &&
                Canvas.GetTop(ball) + ball.Height > Canvas.GetTop(paddle2) &&
                Canvas.GetTop(ball) < Canvas.GetTop(paddle2) + paddle2.Height)
                ballSpeedX = -ballSpeedX;
        }
        private void ShowGameOverDialog()
        {
            MessageBoxResult result = MessageBox.Show("Гру завершено!", "Бажаєте почати нову гру?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.NewGame_Click(null, null);
            }
            else
            {
                Close();
            }
            Close();
        }
        private void ProcessPaddleMovement()
        {
            if (Keyboard.IsKeyDown(Key.W) && Canvas.GetTop(paddle1) > 0)
                Canvas.SetTop(paddle1, Canvas.GetTop(paddle1) - 5);
            if (Keyboard.IsKeyDown(Key.S) && Canvas.GetTop(paddle1) < canvas.ActualHeight - paddle1.Height)
                Canvas.SetTop(paddle1, Canvas.GetTop(paddle1) + 5);

            if (Keyboard.IsKeyDown(Key.Up) && Canvas.GetTop(paddle2) > 0)
                Canvas.SetTop(paddle2, Canvas.GetTop(paddle2) - 5);
            if (Keyboard.IsKeyDown(Key.Down) && Canvas.GetTop(paddle2) < canvas.ActualHeight - paddle2.Height)
                Canvas.SetTop(paddle2, Canvas.GetTop(paddle2) + 5);
        }
        private void MainWindow_ContentRendered(object sender, EventArgs e)
        {
            Canvas.SetLeft(ball, canvas.ActualWidth / 2 - ball.Width / 2);
            Canvas.SetTop(ball, canvas.ActualHeight / 2 - ball.Height / 2);

            Canvas.SetLeft(paddle1, 50);
            Canvas.SetTop(paddle1, canvas.ActualHeight / 2 - paddle1.Height / 2);

            Canvas.SetLeft(paddle2, canvas.ActualWidth - 50 - paddle2.Width);
            Canvas.SetTop(paddle2, canvas.ActualHeight / 2 - paddle2.Height / 2);

            if (timer != null && timer.IsEnabled)
            {
                timer.Stop();
            }
            timer.Start();
        }
        private void UpdateScores()
        {
            Player1.Text = scorePlayer1.ToString();
            Player2.Text = scorePlayer2.ToString();
        }

    }
}
