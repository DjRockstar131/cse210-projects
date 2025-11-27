using System;
using System.Collections.Generic;

namespace MindfulnessProgram
{
    public class ReflectionActivity : Activity
    {
        private List<string> _prompts;
        private List<string> _questions;

        private Queue<string> _promptQueue;
        private Queue<string> _questionQueue;
        private Random _random = new Random();

        public ReflectionActivity()
            : base(
                "Reflection Activity",
                "This activity will help you reflect on times in your life when you have shown " +
                "strength and resilience. This will help you recognize the power you have and how " +
                "you can use it in other aspects of your life.")
        {
            _prompts = new List<string>
            {
                "Think of a time when you stood up for someone else.",
                "Think of a time when you did something really difficult.",
                "Think of a time when you helped someone in need.",
                "Think of a time when you did something truly selfless."
            };

            _questions = new List<string>
            {
                "Why was this experience meaningful to you?",
                "Have you ever done anything like this before?",
                "How did you get started?",
                "How did you feel when it was complete?",
                "What made this time different than other times when you were not as successful?",
                "What is your favorite thing about this experience?",
                "What could you learn from this experience that applies to other situations?",
                "What did you learn about yourself through this experience?",
                "How can you keep this experience in mind in the future?"
            };

            ResetPromptQueue();
            ResetQuestionQueue();
        }

        public override void RunActivity()
        {
            int duration = GetDuration();
            DateTime endTime = DateTime.Now.AddSeconds(duration);

            string prompt = GetNextPrompt();
            Console.WriteLine("Consider the following prompt:");
            Console.WriteLine();
            Console.WriteLine($"--- {prompt} ---");
            Console.WriteLine();
            Console.WriteLine("When you have something in mind, press Enter to continue.");
            Console.ReadLine();

            Console.WriteLine("Now ponder on each of the following questions as they relate to this experience.");
            Console.Write("You may begin in: ");
            ShowCountDown(5);
            Console.Clear();

            while (DateTime.Now < endTime)
            {
                string question = GetNextQuestion();
                Console.WriteLine($"> {question}");
                ShowSpinner(8); // give time to think
                Console.WriteLine();
            }
        }

        private void ResetPromptQueue()
        {
            _promptQueue = new Queue<string>(Shuffle(_prompts));
        }

        private void ResetQuestionQueue()
        {
            _questionQueue = new Queue<string>(Shuffle(_questions));
        }

        private string GetNextPrompt()
        {
            if (_promptQueue.Count == 0)
            {
                ResetPromptQueue();
            }
            return _promptQueue.Dequeue();
        }

        private string GetNextQuestion()
        {
            if (_questionQueue.Count == 0)
            {
                ResetQuestionQueue();
            }
            return _questionQueue.Dequeue();
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
