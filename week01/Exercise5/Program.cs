using System;

class Program
{
    static void Main()
    {
        DisplayWelcome();

        string userName = PromptUserName();
        int favoriteNumber = PromptUserNumber();

        int squaredNumber = SquareNumber(favoriteNumber);

        DisplayResult(userName, squaredNumber);
    }

    // Displays the welcome message
    static void DisplayWelcome()
    {
        Console.WriteLine("Welcome to the program!");
    }

    // Asks for and returns the user's name
    static string PromptUserName()
    {
        Console.Write("Please enter your name: ");
        string name = Console.ReadLine();
        return name;
    }

    // Asks for and returns the user's favorite number
    static int PromptUserNumber()
    {
        Console.Write("Please enter your favorite number: ");
        int number = int.Parse(Console.ReadLine());
        return number;
    }

    // Accepts an integer and returns it squared
    static int SquareNumber(int number)
    {
        int square = number * number;
        return square;
    }

    // Displays the final result
    static void DisplayResult(string name, int square)
    {
        Console.WriteLine($"{name}, the square of your number is {square}");
    }
}
