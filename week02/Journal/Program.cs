using System;

class Program
{
    /*
     * Exceeding Requirements (for 100%):
     * - Adds optional "Mood" (1–5) to each entry.
     * - Supports CSV save/load with proper quoting so files open cleanly in Excel.
     *   (If you prefer the simplified route, use the custom delimiter format.)
     */

    static void Main()
    {
        var journal = new Journal();
        var prompts = new PromptGenerator();

        while (true)
        {
            Console.WriteLine("Journal Menu");
            Console.WriteLine("1. Write a new entry");
            Console.WriteLine("2. Display the journal");
            Console.WriteLine("3. Save the journal to a file");
            Console.WriteLine("4. Load the journal from a file");
            Console.WriteLine("5. Quit");
            Console.Write("Choose an option (1-5): ");
            var choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    WriteNewEntry(journal, prompts);
                    break;

                case "2":
                    journal.Display();
                    break;

                case "3":
                    SaveFlow(journal);
                    break;

                case "4":
                    LoadFlow(journal);
                    break;

                case "5":
                    Console.WriteLine("Goodbye!");
                    return;

                default:
                    Console.WriteLine("Please enter a valid option (1-5).\n");
                    break;
            }
        }
    }

    static void WriteNewEntry(Journal journal, PromptGenerator prompts)
    {
        string prompt = prompts.GetRandomPrompt();
        Console.WriteLine($"Prompt: {prompt}");
        Console.Write("Your response:\n> ");
        string response = Console.ReadLine() ?? "";

        Console.Write("Optional mood (1–5, Enter to skip): ");
        string mood = Console.ReadLine() ?? "";
        if (!string.IsNullOrWhiteSpace(mood))
        {
            // simple guard
            if (!(mood == "1" || mood == "2" || mood == "3" || mood == "4" || mood == "5"))
                mood = ""; // ignore invalid
        }

        var entry = new Entry
        {
            Date = DateTime.Now.ToString("yyyy-MM-dd"),
            Prompt = prompt,
            Response = response,
            Mood = mood
        };

        journal.Add(entry);
        Console.WriteLine("\nEntry saved.\n");
    }

    static void SaveFlow(Journal journal)
    {
        Console.Write("Enter a filename to save (e.g., journal.txt or journal.csv): ");
        string? path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Save cancelled.\n");
            return;
        }

        try
        {
            if (path.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                journal.SaveCsv(path);
                Console.WriteLine($"Journal saved to CSV: {path}\n");
            }
            else
            {
                journal.SaveDelimited(path);
                Console.WriteLine($"Journal saved (custom-delimited): {path}\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving file: {ex.Message}\n");
        }
    }

    static void LoadFlow(Journal journal)
    {
        Console.Write("Enter a filename to load (e.g., journal.txt or journal.csv): ");
        string? path = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(path))
        {
            Console.WriteLine("Load cancelled.\n");
            return;
        }

        try
        {
            if (path.EndsWith(".csv", StringComparison.OrdinalIgnoreCase))
            {
                journal.LoadCsv(path);
                Console.WriteLine($"Journal loaded from CSV: {path}\n");
            }
            else
            {
                journal.LoadDelimited(path);
                Console.WriteLine($"Journal loaded (custom-delimited): {path}\n");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading file: {ex.Message}\n");
        }
    }
}
