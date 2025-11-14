using System;

class Program
{
    static void Main(string[] args)
    {
        // You can change this to any scripture you like
        Reference reference = new Reference("Proverbs", 3, 5, 6);
        string text = "Trust in the Lord with all thine heart; and lean not unto thine own understanding.";

        Scripture scripture = new Scripture(reference, text);

        string input = "";

        while (input.ToLower() != "quit" && !scripture.IsCompletelyHidden())
        {
            Console.Clear();
            Console.WriteLine(scripture.GetDisplayText());
            Console.WriteLine();
            Console.Write("Press Enter to hide words or type 'quit' to finish: ");

            input = Console.ReadLine() ?? "";

            if (input.ToLower() != "quit")
            {
                // hide a few words each round
                scripture.HideRandomWords(3);
            }
        }

        // Final display (either user quit or all words are hidden)
        Console.Clear();
        Console.WriteLine(scripture.GetDisplayText());
        Console.WriteLine();
        Console.WriteLine("Program finished. Press Enter to close.");
        Console.ReadLine();
    }
}
