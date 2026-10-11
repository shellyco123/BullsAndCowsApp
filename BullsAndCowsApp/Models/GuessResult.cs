namespace BullsAndCowsApp.Models
{
    public class GuessResult
    {
        private int _bulls;
        private int _cows;

        public int Bulls
        {
            get { return _bulls; }
            set { _bulls = value; }
        }

        public int Cows
        {
            get { return _cows; }
            set { _cows = value; }
        }

        public bool IsWinningGuess
        {
            get { return _bulls == 4; }
        }
    }
}
