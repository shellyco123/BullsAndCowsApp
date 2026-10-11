namespace BullsAndCowsApp.Models
{
    public class GuessRecord
    {
        private int _attemptNumber;
        private string _guess = "";
        private int _bulls;
        private int _cows;

        public int AttemptNumber
        {
            get { return _attemptNumber; }
            set { _attemptNumber = value; }
        }

        public string Guess
        {
            get { return _guess; }
            set { _guess = value; }
        }

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

        public string Feedback
        {
            get { return _bulls + " Bulls, " + _cows + " Cows"; }
        }
    }
}
