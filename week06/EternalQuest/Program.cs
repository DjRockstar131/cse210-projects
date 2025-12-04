using System;
using System.Collections.Generic;
using System.IO;

namespace EternalQuest
{
    abstract class Goal
    {
        private string _name;
        private string _description;
        private int _points;

        protected Goal(string name, string description, int points)
        {
            _name = name;
            _description = description;
            _points = points;
        }

        public string Name => _name;
        public string Description => _description;
        public int Points => _points;

        // How many points this event gives (or takes away)
        public abstract int RecordEvent();

        // Whether the goal is considered completed
        public abstract bool IsComplete { get; }

        // For display in the menu: [ ] Name (Description) etc.
        public abstract string GetDetailsString();

        // For saving to a file
        public abstract string GetSaveString();

        // Factory method for loading from a line
        public static Goal FromSaveString(string line)
        {
            // Format: Type|field1|field2|...
            string[] parts = line.Split('|');
            string type = parts[0];

            switch (type)
            {
                case "SimpleGoal":
                    // SimpleGoal|name|description|points|isComplete
                    return new SimpleGoal(
                        parts[1],
                        parts[2],
                        int.Parse(parts[3]),
                        bool.Parse(parts[4])
                    );

                case "EternalGoal":
                    // EternalGoal|name|description|points
                    return new EternalGoal(
                        parts[1],
                        parts[2],
                        int.Parse(parts[3])
                    );

                case "ChecklistGoal":
                    // ChecklistGoal|name|description|points|bonus|target|current
                    return new ChecklistGoal(
                        parts[1],
                        parts[2],
                        int.Parse(parts[3]),
                        int.Parse(parts[4]),
                        int.Parse(parts[5]),
                        int.Parse(parts[6])
                    );

                case "NegativeGoal":
                    // NegativeGoal|name|description|points|timesRecorded
                    return new NegativeGoal(
                        parts[1],
                        parts[2],
                        int.Parse(parts[3]),
                        int.Parse(parts[4])
                    );

                default:
                    throw new Exception("Unknown goal type in save file: " + type);
            }
        }
    }

    class SimpleGoal : Goal
    {
        private bool _isComplete;

        public SimpleGoal(string name, string description, int points, bool isComplete = false)
            : base(name, description, points)
        {
            _isComplete = isComplete;
        }

        public override bool IsComplete => _isComplete;

        public override int RecordEvent()
        {
            if (!_isComplete)
            {
                _isComplete = true;
                return Points;
            }
            else
            {
                Console.WriteLine("This goal is already complete. No additional points awarded.");
                return 0;
            }
        }

        public override string GetDetailsString()
        {
            string checkbox = IsComplete ? "[X]" : "[ ]";
            return $"{checkbox} {Name} ({Description})";
        }

        public override string GetSaveString()
        {
            // SimpleGoal|name|description|points|isComplete
            return $"SimpleGoal|{Name}|{Description}|{Points}|{IsComplete}";
        }
    }

    class EternalGoal : Goal
    {
        public EternalGoal(string name, string description, int points)
            : base(name, description, points)
        {
        }

        public override bool IsComplete => false; // never finishes

        public override int RecordEvent()
        {
            // Always awards points
            return Points;
        }

        public override string GetDetailsString()
        {
            // Eternal goals never get an [X]
            return $"[∞] {Name} ({Description})";
        }

        public override string GetSaveString()
        {
            // EternalGoal|name|description|points
            return $"EternalGoal|{Name}|{Description}|{Points}";
        }
    }

    class ChecklistGoal : Goal
    {
        private int _bonusPoints;
        private int _targetCount;
        private int _currentCount;

        public ChecklistGoal(
            string name,
            string description,
            int pointsPerCompletion,
            int bonusPoints,
            int targetCount,
            int currentCount = 0)
            : base(name, description, pointsPerCompletion)
        {
            _bonusPoints = bonusPoints;
            _targetCount = targetCount;
            _currentCount = currentCount;
        }

        public override bool IsComplete => _currentCount >= _targetCount;

        public override int RecordEvent()
        {
            _currentCount++;

            int total = Points; // base points per event

            if (_currentCount == _targetCount)
            {
                Console.WriteLine("Checklist goal complete! Bonus awarded!");
                total += _bonusPoints;
            }

            return total;
        }

        public override string GetDetailsString()
        {
            string checkbox = IsComplete ? "[X]" : "[ ]";
            return $"{checkbox} {Name} ({Description}) -- Completed {_currentCount}/{_targetCount} times";
        }

        public override string GetSaveString()
        {
            // ChecklistGoal|name|description|points|bonus|target|current
            return $"ChecklistGoal|{Name}|{Description}|{Points}|{_bonusPoints}|{_targetCount}|{_currentCount}";
        }
    }

    // EXTRA CREATIVITY:
    // NegativeGoal subtracts points each time you record it.
    // Use this for bad habits you are trying to avoid.
    class NegativeGoal : Goal
    {
        private int _timesRecorded;

        public NegativeGoal(string name, string description, int penaltyPoints, int timesRecorded = 0)
            : base(name, description, penaltyPoints)
        {
            _timesRecorded = timesRecorded;
        }

        public override bool IsComplete => false; // never "complete"

        public override int RecordEvent()
        {
            _timesRecorded++;
            // Return negative points as penalty
            return -Points;
        }

        public override string GetDetailsString()
        {
            return $"[!] {Name} ({Description}) -- Recorded {_timesRecorded} times (bad habit)";
        }

        public override string GetSaveString()
        {
            // NegativeGoal|name|description|points|timesRecorded
            return $"NegativeGoal|{Name}|{Description}|{Points}|{_timesRecorded}";
        }
    }

    class Program
    {
        private static List<Goal> _goals = new List<Goal>();
        private static int _score = 0;
        private static int _level = 1;
        private const int PointsPerLevel = 1000;

        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                UpdateLevel(); // keep level in sync with score

                Console.Clear();
                Console.WriteLine("=== Eternal Quest ===");
                Console.WriteLine($"Score: {_score}  |  Level: {_level}");
                Console.WriteLine();

                Console.WriteLine("Menu:");
                Console.WriteLine("1. Create New Goal");
                Console.WriteLine("2. List Goals");
                Console.WriteLine("3. Save Goals");
                Console.WriteLine("4. Load Goals");
                Console.WriteLine("5. Record Event");
                Console.WriteLine("6. Quit");
                Console.Write("Select an option: ");

                string choice = Console.ReadLine();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        CreateNewGoal();
                        break;
                    case "2":
                        ListGoals();
                        Pause();
                        break;
                    case "3":
                        SaveGoals();
                        Pause();
                        break;
                    case "4":
                        LoadGoals();
                        Pause();
                        break;
                    case "5":
                        RecordEventOnGoal();
                        Pause();
                        break;
                    case "6":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        Pause();
                        break;
                }
            }

            Console.WriteLine("Thanks for playing Eternal Quest!");
        }

        private static void CreateNewGoal()
        {
            Console.WriteLine("Which type of goal would you like to create?");
            Console.WriteLine("1. Simple Goal (one-time)");
            Console.WriteLine("2. Eternal Goal (never-ending)");
            Console.WriteLine("3. Checklist Goal (complete N times)");
            Console.WriteLine("4. Negative Goal (lose points for bad habit)  [EXTRA]");
            Console.Write("Select a type: ");

            string typeChoice = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Enter the name of your goal: ");
            string name = Console.ReadLine();

            Console.Write("Enter a short description: ");
            string description = Console.ReadLine();

            switch (typeChoice)
            {
                case "1":
                    Console.Write("Points for completing this goal: ");
                    int sPoints = int.Parse(Console.ReadLine());
                    _goals.Add(new SimpleGoal(name, description, sPoints));
                    break;

                case "2":
                    Console.Write("Points each time you record this goal: ");
                    int ePoints = int.Parse(Console.ReadLine());
                    _goals.Add(new EternalGoal(name, description, ePoints));
                    break;

                case "3":
                    Console.Write("Points each time you record this goal: ");
                    int cPoints = int.Parse(Console.ReadLine());
                    Console.Write("How many times must this goal be completed? ");
                    int target = int.Parse(Console.ReadLine());
                    Console.Write("Bonus points for completing it that many times: ");
                    int bonus = int.Parse(Console.ReadLine());
                    _goals.Add(new ChecklistGoal(name, description, cPoints, bonus, target));
                    break;

                case "4":
                    Console.Write("Penalty points each time this bad habit happens (positive number): ");
                    int penalty = int.Parse(Console.ReadLine());
                    _goals.Add(new NegativeGoal(name, description, penalty));
                    break;

                default:
                    Console.WriteLine("Invalid goal type. No goal created.");
                    break;
            }

            Console.WriteLine("Goal created!");
        }

        private static void ListGoals()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("No goals created yet.");
                return;
            }

            Console.WriteLine("Your goals:");
            for (int i = 0; i < _goals.Count; i++)
            {
                Goal g = _goals[i];
                Console.WriteLine($"{i + 1}. {g.GetDetailsString()}");
            }
        }

        private static void SaveGoals()
        {
            Console.Write("Enter filename to save to (e.g., goals.txt): ");
            string filename = Console.ReadLine();

            using (StreamWriter writer = new StreamWriter(filename))
            {
                // First line: score
                writer.WriteLine(_score);

                // Then each goal on its own line
                foreach (Goal g in _goals)
                {
                    writer.WriteLine(g.GetSaveString());
                }
            }

            Console.WriteLine("Goals and score saved.");
        }

        private static void LoadGoals()
        {
            Console.Write("Enter filename to load from (e.g., goals.txt): ");
            string filename = Console.ReadLine();

            if (!File.Exists(filename))
            {
                Console.WriteLine("File not found.");
                return;
            }

            string[] lines = File.ReadAllLines(filename);
            if (lines.Length == 0)
            {
                Console.WriteLine("File is empty.");
                return;
            }

            _goals.Clear();

            // First line is score
            _score = int.Parse(lines[0]);

            // Remaining lines are goals
            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i])) continue;
                Goal g = Goal.FromSaveString(lines[i]);
                _goals.Add(g);
            }

            UpdateLevel();
            Console.WriteLine("Goals and score loaded.");
        }

        private static void RecordEventOnGoal()
        {
            if (_goals.Count == 0)
            {
                Console.WriteLine("You have no goals to record. Create one first.");
                return;
            }

            Console.WriteLine("Which goal did you accomplish?");
            ListGoals();
            Console.Write("Enter the goal number: ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int index))
            {
                index--; // convert to 0-based

                if (index >= 0 && index < _goals.Count)
                {
                    Goal selected = _goals[index];
                    int points = selected.RecordEvent();
                    AddPoints(points);
                }
                else
                {
                    Console.WriteLine("Invalid goal number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input.");
            }
        }

        private static void AddPoints(int points)
        {
            if (points == 0)
            {
                Console.WriteLine("No points gained or lost.");
                return;
            }

            _score += points;
            if (points > 0)
            {
                Console.WriteLine($"You earned {points} points!");
            }
            else
            {
                Console.WriteLine($"You lost {-points} points.");
            }

            int oldLevel = _level;
            UpdateLevel();

            if (_level > oldLevel)
            {
                Console.WriteLine($"*** Level Up! You are now Level {_level}! ***");
            }
        }

        private static void UpdateLevel()
        {
            if (_score < 0)
            {
                _score = 0;
            }

            _level = (_score / PointsPerLevel) + 1;
        }

        private static void Pause()
        {
            Console.WriteLine();
            Console.Write("Press Enter to continue...");
            Console.ReadLine();
        }
    }
}
