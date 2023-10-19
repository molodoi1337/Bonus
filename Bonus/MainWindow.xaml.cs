using GuessNumberGame;
using System.Windows.Threading;
using System;
using System.Windows;

namespace GuessNumberGame
{
    public partial class MainWindow : Window
    {
        private int targetNumber;
        private int attemptsCount;

        DispatcherTimer _timer;
        TimeSpan _time;

        public MainWindow()// закинуть таймер в функцию и сделать сброс времени после победы или поражение,добавить поражение ну и по визуалу
        {
            InitializeComponent();

            _time = TimeSpan.FromSeconds(80);

            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, delegate
            {
                Time.Text = _time.ToString("c");
                if (_time == TimeSpan.Zero) Close();
                _time = _time.Add(TimeSpan.FromSeconds(-1));
            }, Application.Current.Dispatcher);

            _timer.Start();
            StartNewGame();
        }
       

        private void StartNewGame()
        {
            Random random = new Random();
            targetNumber = random.Next(0, 100);
            attemptsCount = 0;
        }

        private void HandleGuess()
        {
            int userGuess;

            if (int.TryParse(guessNumberTextBox.Text, out userGuess))
            {
                attemptsCount++;

                if (userGuess == targetNumber)
                {
                    resultTextBlock.Text = $"Вы угадали число за {attemptsCount} попыток!";
                    StartNewGame();
                }
                else if (userGuess < targetNumber)
                {
                    resultTextBlock.Text = "Загаданное число больше.";
                }
                else
                {
                    resultTextBlock.Text = "Загаданное число меньше.";
                }

                guessNumberTextBox.Text = "";
            }
            else
            {
                resultTextBlock.Text = "Введите число!";
            }
        }

        private void guessButton_Click(object sender, RoutedEventArgs e)
        {
            HandleGuess();
        }

        private void guessNumberTextBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.Enter)
            {
                HandleGuess();
            }
        }
    }
}
