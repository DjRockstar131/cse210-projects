using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Welcome to Guess My Number!");

        // Keep playing whole games until the user says "no"
        while (true)
        {
            // Core: pick a magic number. (Final step uses random 1..100.)
            int magic = new Random().Next(1, 101);

            // If you want to debug, uncomment the next line to reveal:
            // Console.WriteLine($"[debug] magic = {magic}");

            int guessCount = 0;
            int guess;

            // First prompt for a guess (no loop yet in the very first version,
            // but final version uses a loop until the user guesses the number).
            while (true)
            {
                Console.Write("What is your guess? ");
                string input = Console.ReadLine();
                if (!int.TryParse(input, out guess))
                {
                    Console.WriteLine("Please enter a whole number.");
                    continue;
                }

                guessCount++;

                if (guess < magic)
                {
                    Console.WriteLine("Higher");
                }
                else if (guess > magic)
                {
                    Console.WriteLine("Lower");
                }
                else
                {
                    Console.WriteLine("You guessed it!");
                    break; // exit the guessing loop
                }
            }

            // Stretch: show how many guesses it took
            Console.WriteLine($"You made {guessCount} {(guessCount == 1 ? "guess" : "guesses")}.");

            // Stretch: ask to play again
            Console.Write("\nDo you want to play again? (yes/no) ");
            string again = (Console.ReadLine() ?? "").Trim().ToLowerInvariant();

            if (again != "yes" && again != "y")
            {
                Console.WriteLine("Thanks for playing!");
                break; // exit the outer game loop
            }

            Console.WriteLine(); // blank line before the next round
        }
    }
}
