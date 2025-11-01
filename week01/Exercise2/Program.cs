using System;

class Program
{
    static void Main()
    {
        Console.Write("What is your grade percentage? ");
        string input = Console.ReadLine();

        // Parse as integer percentage (typical for this assignment).
        // If the user enters decimals, we'll truncate toward zero.
        int percent = 0;
        if (!int.TryParse(input, out percent))
        {
            Console.WriteLine("Please enter a whole-number percentage (e.g., 87).");
            return;
        }

        // -------- Core requirement: determine the letter --------
        string letter;

        if (percent >= 90)
        {
            letter = "A";
        }
        else if (percent >= 80)
        {
            letter = "B";
        }
        else if (percent >= 70)
        {
            letter = "C";
        }
        else if (percent >= 60)
        {
            letter = "D";
        }
        else
        {
            letter = "F";
        }

        // -------- Stretch: determine + or - sign --------
        string sign = "";
        int lastDigit = Math.Abs(percent) % 10; // last digit of the percentage

        if (letter == "A")
        {
            // No A+. A- is allowed (for lastDigit < 3).
            if (lastDigit < 3 && percent >= 90) sign = "-";
        }
        else if (letter == "F")
        {
            // No F+ or F- — just F.
            sign = "";
        }
        else
        {
            // For B, C, D:
            if (lastDigit >= 7) sign = "+";
            else if (lastDigit < 3) sign = "-";
            // otherwise, no sign
        }

        // Single print statement for the letter grade (per spec change)
        Console.WriteLine($"Your grade is: {letter}{sign}");

        // -------- Pass/fail message (70+ passes) --------
        if (percent >= 70)
        {
            Console.WriteLine("Congratulations! You passed the course.");
        }
        else
        {
            Console.WriteLine("You did not pass this time. Keep working—you'll get it next time!");
        }
    }
}
