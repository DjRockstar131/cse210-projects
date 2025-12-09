using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        Activity run = new Running(
            new DateTime(2025, 12, 9), 30, 3.0);   // 3 miles

        Activity bike = new Cycling(
            new DateTime(2025, 12, 9), 30, 15.0);  // 15 mph

        Activity swim = new Swimming(
            new DateTime(2025, 12, 9), 30, 40);    // 40 laps

        List<Activity> activities = new List<Activity> { run, bike, swim };

        foreach (Activity activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}
