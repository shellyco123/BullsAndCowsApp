using System.Collections.ObjectModel;
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
        private ObservableCollection<GuessRecord> _history;
        private ICommand _submitGuessCommand;
        private ICommand _newGameCommand;

        private string _guessText = "";
        private int _attemptCount;
        private string _errorMessage = "";
        private string _statusMessage = "";

        public GameViewModel()
        {
            _game = new BullsAndCowsGame();
            _history = new ObservableCollection<GuessRecord>();
            _submitGuessCommand = new GuessButtonCommand(this);
            _newGameCommand = new NewGameButtonCommand(this);
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

        // The rows of the history list. The list updates the screen by itself
        public ObservableCollection<GuessRecord> History
        {
            get { return _history; }
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

            GuessRecord record = new GuessRecord();
            record.AttemptNumber = _game.AttemptCount;
            record.Guess = input;
            record.Bulls = result.Bulls;
            record.Cows = result.Cows;
            History.Add(record);

            ErrorMessage = "";
            GuessText = "";
            AttemptCount = _game.AttemptCount;

            if (result.IsWinningGuess)
            {
                StatusMessage = "Correct! You guessed it in " + _game.AttemptCount + " attempts.";
            }
            else
            {
                StatusMessage = "Result: " + record.Feedback;
            }
        }

        // Starts a new game and clears the screen
        public void StartNewGame()
        {
            _game.StartNewGame();
            History.Clear();

            ErrorMessage = "";
            GuessText = "";
            AttemptCount = _game.AttemptCount;
            StatusMessage = "New game started! I picked a new number.";
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
}
