using System;
using System.Collections.Generic;

public class PromptGenerator
{
    private readonly List<string> _prompts = new()
    {
        "Who was the most interesting person I interacted with today?",
        "What was the best part of my day?",
        "How did I see the hand of the Lord in my life today?",
        "What was the strongest emotion I felt today?",
        "If I had one thing I could do over today, what would it be?",
        // Add your own:
        "What is something I learned today that I don't want to forget?",
        "Where did I feel most at peace today?"
    };

    private readonly Random _rand = new();

    public string GetRandomPrompt()
    {
        int i = _rand.Next(_prompts.Count);
        return _prompts[i];
    }
}
