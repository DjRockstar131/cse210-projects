using System;
using System.Threading;

namespace MindfulnessProgram
{
    public abstract class Activity
    {
        private string _name;
        private string _description;
        private int _duration; // in seconds

        protected Activity(string name, string description)
        {
            _name = name;
            _description = description;
        }

        public void Start()
        {
            Console.WriteLine($"Welcome to the {_name}.");
            Console.WriteLine();
            Console.WriteLine(_description);
            Console.WriteLine();

            Console.Write("How long, in seconds, would you like for your session? ");
            while (!int.TryParse(Console.ReadLine(), out _duration) || _duration <= 0)
            {
                Console.Write("Please enter a positive whole number of seconds: ");
            }

            Console.WriteLine();
            Console.WriteLine("Get ready to begin...");
            ShowSpinner(3);
            Console.Clear();
        }

        public void End()
        {
            Console.WriteLine();
            Console.WriteLine("Well done!");
            ShowSpinner(2);
            Console.WriteLine();
            Console.WriteLine($"You have completed {_duration} seconds of the {_name}.");
            ShowSpinner(3);
            Console.WriteLine();
            Console.WriteLine("Press Enter to return to the main menu...");
            Console.ReadLine();
        }

        public int GetDuration()
        {
            return _duration;
        }

        protected void ShowSpinner(int seconds)
        {
            // Simple spinner animation
            char[] sequence = { '|', '/', '-', '\\' };
            DateTime endTime = DateTime.Now.AddSeconds(seconds);
            int index = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write(sequence[index]);
                Thread.Sleep(200);
                Console.Write("\b \b"); // backspace, space, backspace
                index = (index + 1) % sequence.Length;
            }
        }

        protected void ShowCountDown(int seconds)
        {
            for (int i = seconds; i > 0; i--)
            {
                Console.Write(i);
                Thread.Sleep(1000);
                Console.Write("\b \b");
            }
        }

        public abstract void RunActivity();
    }
}
