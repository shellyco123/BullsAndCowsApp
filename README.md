# Bulls and Cows — Console App (Phase 1)

A command-line Bulls and Cows game. The game logic lives in three Model classes in
`BullsAndCowsApp/Models/` (namespace `BullsAndCowsApp.Models`) so they can be copied into the MAUI app
unchanged. `Program.cs` is the only file that touches the console.

## Run

Open `BullsAndCowsApp.slnx` in Visual Studio 2026 and press F5 (requires .NET 10).

Commands (case-insensitive): a 4-digit guess, `n`/`new`, `h`/`history`, `q`/`quit`/`exit`.

## Structure

| File | Role |
|---|---|
| `Models/GuessResult.cs` | Bulls/Cows of one guess; `IsWinningGuess` is computed from `Bulls`. |
| `Models/GuessRecord.cs` | One history row; `Feedback` is computed ("1 Bulls, 2 Cows"). |
| `Models/BullsAndCowsGame.cs` | Secret number, validation (`IsValidGuess`), scoring (`SubmitGuess`). No console I/O. |
| `Program.cs` | CLI loop and helpers (`IsQuit`, `IsNewGame`, `IsHistory`, `PrintWelcome`, `PrintHistory`). |

## Changes from the spec

- Input is trimmed before it's checked, so `" 1234 "` is accepted as `1234`.
- If the input stream ends (Ctrl+D / Ctrl+Z), the program exits instead of crashing.
- `h` with no guesses yet prints "No guesses yet in this game."
- Leading zero: after shuffling, if the first digit is `0` it is replaced by the 5th shuffled
  digit (not one of the chosen four), so the digits stay unique.

## Known bugs

None known.
