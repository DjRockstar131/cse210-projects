using System;

namespace MindfulnessProgram
{
    class Program
    {
        // Simple log to track how many times each activity was performed (extra credit feature)
        private static int _breathingCount = 0;
        private static int _reflectionCount = 0;
        private static int _listingCount = 0;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                Console.Clear();
                Console.WriteLine("Mindfulness Program");
                Console.WriteLine("-------------------");
                Console.WriteLine("1. Breathing Activity");
                Console.WriteLine("2. Reflection Activity");
                Console.WriteLine("3. Listing Activity");
                Console.WriteLine("4. View Session Summary");
                Console.WriteLine("5. Quit");
                Console.Write("Select a choice from the menu: ");

                string choice = Console.ReadLine();

                Activity activity = null;

                switch (choice)
                {
                    case "1":
                        activity = new BreathingActivity();
                        _breathingCount++;
                        break;

                    case "2":
                        activity = new ReflectionActivity();
                        _reflectionCount++;
                        break;

                    case "3":
                        activity = new ListingActivity();
                        _listingCount++;
                        break;

                    case "4":
                        ShowSummary();
                        continue; // Go back to menu

                    case "5":
                        running = false;
                        continue;

                    default:
                        Console.WriteLine("Invalid choice. Press Enter to continue.");
                        Console.ReadLine();
                        continue;
                }

                Console.Clear();
                activity.Start();        // common start logic
                activity.RunActivity();  // specific activity logic
                activity.End();          // common end logic
            }
        }

        private static void ShowSummary()
        {
            Console.Clear();
            Console.WriteLine("Session Summary");
            Console.WriteLine("---------------");
            Console.WriteLine($"Breathing activities completed:  {_breathingCount}");
            Console.WriteLine($"Reflection activities completed: {_reflectionCount}");
            Console.WriteLine($"Listing activities completed:    {_listingCount}");
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the menu...");
            Console.ReadLine();
        }
    }

    /*
    EXCEEDING REQUIREMENTS (for full credit):

    1. The program keeps a simple log of how many times each activity is performed
       during the session and displays it in the "Session Summary" menu option.

    2. The ReflectionActivity and ListingActivity ensure that prompts are not
       repeated until all prompts have been used at least once in that session.

    3. The program uses a re-usable spinner and countdown animations and a base
       Activity class to avoid duplicated code and to follow encapsulation and
       abstraction principles.
    */
}
