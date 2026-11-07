namespace BullsAndCowsApp.Models
{
    public class BullsAndCowsGame
    {
        private const int CodeLength = 4;

        private Random _random = new Random();

        private string _secretNumber = "";

        private int _attemptCount;

        private bool _isGameOver;

        private int _randomNumber;

        public int AttemptCount
        {
            get { return _attemptCount; }
            private set { _attemptCount = value; }
        }

        public bool IsGameOver
        {
            get { return _isGameOver; }
            private set { _isGameOver = value; }
        }

        public int RandomNumber
        {
            get { return _randomNumber; }
            private set { _randomNumber = value; }
        }

        public BullsAndCowsGame()
        {
            StartNewGame();
        }

        public void StartNewGame()
        {
            _secretNumber = GenerateSecretNumber();
            AttemptCount = 0;
            IsGameOver = false;
        }

        public void GenerateRandomNumber()
        {
            RandomNumber = _random.Next(1000, 10000);
        }

        private string GenerateSecretNumber()
        {
            char[] digits = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            for (int i = 0; i < 10; i++)
            {
                int j = _random.Next(10);
                char temp = digits[i];
                digits[i] = digits[j];
                digits[j] = temp;
            }

            if (digits[0] == '0')
            {
                digits[0] = digits[4];
            }

            return "" + digits[0] + digits[1] + digits[2] + digits[3];
        }

        public static bool IsValidGuess(string guess, out string errorMessage)
        {
            errorMessage = "";

            if (guess == null || guess.Length != CodeLength)
            {
                errorMessage = "Please enter exactly 4 digits.";
                return false;
            }

            for (int i = 0; i < guess.Length; i++)
            {
                if (guess[i] < '0' || guess[i] > '9')
                {
                    errorMessage = "Please enter exactly 4 digits.";
                    return false;
                }
            }

            for (int i = 0; i < guess.Length; i++)
            {
                for (int j = i + 1; j < guess.Length; j++)
                {
                    if (guess[i] == guess[j])
                    {
                        errorMessage = "All digits must be different from each other.";
                        return false;
                    }
                }
            }

            return true;
        }

        public GuessResult SubmitGuess(string guess)
        {
            if (IsGameOver)
            {
                throw new Exception("The game is over. Start a new game first.");
            }

            int bulls = 0;
            int cows = 0;

            for (int i = 0; i < CodeLength; i++)
            {
                if (guess[i] == _secretNumber[i])
                {
                    bulls++;
                }
                else if (_secretNumber.Contains(guess[i]))
                {
                    cows++;
                }
            }

            AttemptCount++;

            GuessResult result = new GuessResult();
            result.Bulls = bulls;
            result.Cows = cows;

            if (result.IsWinningGuess)
            {
                IsGameOver = true;
            }

            return result;
        }
    }
}
