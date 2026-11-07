using System.ComponentModel;
using System.Windows.Input;
using BullsAndCowsApp.Models;

namespace BullsAndCowsApp.ViewModels
{
    // The middle layer: the screen talks to this class, and this class talks to the game rules
    public class GameViewModel : INotifyPropertyChanged
    {
        // The screen listens to this event to know when to update itself
        public event PropertyChangedEventHandler? PropertyChanged;

        private BullsAndCowsGame _game;
        private ICommand _submitGuessCommand;
        private ICommand _newGameCommand;
        private ICommand _showRandomNumberCommand;

        private string _guessText = "";
        private int _attemptCount;
        private string _errorMessage = "";
        private string _statusMessage = "";
        private string _randomNumberText = "";

        public GameViewModel()
        {
            _game = new BullsAndCowsGame();
            _submitGuessCommand = new GuessButtonCommand(this);
            _newGameCommand = new NewGameButtonCommand(this);
            _showRandomNumberCommand = new RandomNumberButtonCommand(this);
            _statusMessage = "I picked a 4-digit number with no repeated digits.";
        }

        // The text in the guess box
        public string GuessText
        {
            get { return _guessText; }
            set
            {
                _guessText = value;
                OnPropertyChanged("GuessText");
            }
        }

        // The attempt number shown on the screen
        public int AttemptCount
        {
            get { return _attemptCount; }
            set
            {
                _attemptCount = value;
                OnPropertyChanged("AttemptCount");
            }
        }

        // The error text. Empty means there is no error
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
                OnPropertyChanged("HasError");
            }
        }

        // True when the error message should be visible
        public bool HasError
        {
            get { return _errorMessage != ""; }
        }

        // The result of the last guess
        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged("StatusMessage");
            }
        }

        // The random number shown on the screen
        public string RandomNumberText
        {
            get { return _randomNumberText; }
            set
            {
                _randomNumberText = value;
                OnPropertyChanged("RandomNumberText");
            }
        }

        // Runs when GUESS is tapped
        public ICommand SubmitGuessCommand
        {
            get { return _submitGuessCommand; }
        }

        // Runs when New game is tapped
        public ICommand NewGameCommand
        {
            get { return _newGameCommand; }
        }

        // Runs when Random number is tapped
        public ICommand ShowRandomNumberCommand
        {
            get { return _showRandomNumberCommand; }
        }

        // Checks the guess, sends it to the game and updates the screen
        public void SubmitGuess()
        {
            if (_game.IsGameOver)
            {
                ErrorMessage = "This game is over. Tap New game to play again.";
                return;
            }

            string input = GuessText.Trim();

            string errorMessage;
            if (BullsAndCowsGame.IsValidGuess(input, out errorMessage) == false)
            {
                ErrorMessage = errorMessage;
                return;
            }

            GuessResult result = _game.SubmitGuess(input);

            ErrorMessage = "";
            GuessText = "";
            AttemptCount = _game.AttemptCount;

            if (result.IsWinningGuess)
            {
                StatusMessage = "Correct! You guessed it in " + _game.AttemptCount + " attempts.";
            }
            else
            {
                StatusMessage = "Result: " + result.Bulls + " Bulls, " + result.Cows + " Cows";
            }
        }

        // Starts a new game and clears the screen
        public void StartNewGame()
        {
            _game.StartNewGame();

            ErrorMessage = "";
            GuessText = "";
            AttemptCount = _game.AttemptCount;
            StatusMessage = "New game started! I picked a new number.";
        }

        // Asks the Model for a new random number and shows it
        public void ShowRandomNumber()
        {
            _game.GenerateRandomNumber();
            RandomNumberText = _game.RandomNumber.ToString();
        }

        // Tells the screen that a property has a new value
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    // The command of the GUESS button
    public class GuessButtonCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private GameViewModel _viewModel;

        public GuessButtonCommand(GameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        // The button is always enabled
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _viewModel.SubmitGuess();
        }
    }

    // The command of the New game button
    public class NewGameButtonCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private GameViewModel _viewModel;

        public NewGameButtonCommand(GameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        // The button is always enabled
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _viewModel.StartNewGame();
        }
    }

    // The command of the Random number button
    public class RandomNumberButtonCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        private GameViewModel _viewModel;

        public RandomNumberButtonCommand(GameViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        // The button is always enabled
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            _viewModel.ShowRandomNumber();
        }
    }
}
