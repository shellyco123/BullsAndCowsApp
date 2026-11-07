using BullsAndCowsApp.Models;

namespace BullsAndCowsApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            BullsAndCowsGame game = new BullsAndCowsGame();
            List<GuessRecord> history = new List<GuessRecord>();

            PrintWelcome();

            while (true)
            {
                Console.WriteLine();
                Console.Write("Your guess (4 digits), 'n' = new game, 'h' = history, 'q' = quit: ");
                string input = Console.ReadLine();

                if (input == null)
                {
                    break;
                }

                input = input.Trim();

                if (input == "")
                {
                    continue;
                }

                if (IsQuit(input))
                {
                    Console.WriteLine();
                    Console.WriteLine("Thanks for playing. Goodbye!");
                    break;
                }
                else if (IsNewGame(input))
                {
                    game.StartNewGame();
                    history.Clear();
                    Console.WriteLine("New game started! I picked a new number.");
                }
                else if (IsHistory(input))
                {
                    PrintHistory(history);
                }
                else if (game.IsGameOver)
                {
                    Console.WriteLine("This game is over. Type 'n' for a new game or 'q' to quit.");
                }
                else
                {
                    string errorMessage;
                    if (BullsAndCowsGame.IsValidGuess(input, out errorMessage) == false)
                    {
                        Console.WriteLine("Error: " + errorMessage);
                    }
                    else
                    {
                        GuessResult result = game.SubmitGuess(input);

                        GuessRecord record = new GuessRecord();
                        record.AttemptNumber = game.AttemptCount;
                        record.Guess = input;
                        record.Bulls = result.Bulls;
                        record.Cows = result.Cows;
                        history.Add(record);

                        Console.WriteLine("-> " + record.Feedback);

                        if (result.IsWinningGuess)
                        {
                            Console.WriteLine();
                            Console.WriteLine("*** Correct! You guessed it in " + game.AttemptCount + " attempts. ***");
                            Console.WriteLine("Type 'n' for a new game or 'q' to quit.");
                        }
                    }
                }
            }
        }

        static bool IsQuit(string input)
        {
            string text = input.ToLower();
            return text == "q" || text == "quit" || text == "exit";
        }

        static bool IsNewGame(string input)
        {
            string text = input.ToLower();
            return text == "n" || text == "new";
        }

        static bool IsHistory(string input)
        {
            string text = input.ToLower();
            return text == "h" || text == "history";
        }

        static void PrintWelcome()
        {
            Console.WriteLine("=======================================");
            Console.WriteLine("          BULLS AND COWS (CLI)");
            Console.WriteLine("=======================================");
            Console.WriteLine("I picked a 4-digit number with no repeated digits.");
            Console.WriteLine("Bulls = right digit, right position.");
            Console.WriteLine("Cows  = right digit, wrong position.");
        }

        static void PrintHistory(List<GuessRecord> history)
        {
            if (history.Count == 0)
            {
                Console.WriteLine("No guesses yet in this game.");
                return;
            }

            Console.WriteLine("Guess history:");
            for (int i = 0; i < history.Count; i++)
            {
                GuessRecord record = history[i];
                Console.WriteLine("  #" + record.AttemptNumber + "  " + record.Guess + "  ->  " + record.Feedback);
            }
        }
    }
}
