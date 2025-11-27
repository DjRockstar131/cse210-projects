using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    public class ListingActivity : Activity
    {
        private List<string> _prompts;
        private Queue<string> _promptQueue;
        private Random _random = new Random();

        public ListingActivity()
            : base(
                "Listing Activity",
                "This activity will help you reflect on the good things in your life by having you " +
                "list as many things as you can in a certain area.")
        {
            _prompts = new List<string>
            {
                "Who are people that you appreciate?",
                "What are personal strengths of yours?",
                "Who are people that you have helped this week?",
                "When have you felt the Holy Ghost this month?",
                "Who are some of your personal heroes?"
            };

            ResetPromptQueue();
        }

        public override void RunActivity()
        {
            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            string prompt = GetNextPrompt();
            Console.WriteLine("List as many responses as you can to the following prompt:");
            Console.WriteLine();
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine();

            Console.Write("You may begin in: ");
            ShowCountDown(5);
            Console.WriteLine();

            int count = 0;

            while (DateTime.Now < endTime)
            {
                Console.Write("> ");
                string item = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(item))
                {
                    count++;
                }

                if (DateTime.Now >= endTime)
                {
                    break;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"You listed {count} items!");
        }

        private void ResetPromptQueue()
        {
            _promptQueue = new Queue<string>(Shuffle(_prompts));
        }

        private string GetNextPrompt()
        {
            if (_promptQueue.Count == 0)
            {
                ResetPromptQueue();
            }
            return _promptQueue.Dequeue();
        }

        private List<T> Shuffle<T>(List<T> list)
        {
            List<T> copy = new List<T>(list);
            for (int i = copy.Count - 1; i > 0; i--)
            {
                int j = _random.Next(i + 1);
                T temp = copy[i];
                copy[i] = copy[j];
                copy[j] = temp;
            }
            return copy;
        }
    }
}
