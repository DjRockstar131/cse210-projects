using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter a list of numbers, type 0 when finished.");

        List<int> numbers = new List<int>();
        int userNumber;

        // Core: collect numbers until 0 is entered
        do
        {
            Console.Write("Enter number: ");
            userNumber = int.Parse(Console.ReadLine());

            if (userNumber != 0)
            {
                numbers.Add(userNumber);
            }

        } while (userNumber != 0);

        // Defensive check if the list is empty
        if (numbers.Count == 0)
        {
            Console.WriteLine("No numbers were entered.");
            return;
        }

        // Core: calculations
        int sum = numbers.Sum();
        double average = numbers.Average();
        int max = numbers.Max();

        Console.WriteLine($"The sum is: {sum}");
        Console.WriteLine($"The average is: {average}");
        Console.WriteLine($"The largest number is: {max}");

        // Stretch: smallest positive number
        List<int> positiveNumbers = numbers.Where(n => n > 0).ToList();
        if (positiveNumbers.Count > 0)
        {
            int smallestPositive = positiveNumbers.Min();
            Console.WriteLine($"The smallest positive number is: {smallestPositive}");
        }

        // Stretch: sorted list
        numbers.Sort();
        Console.WriteLine("The sorted list is:");
        foreach (int num in numbers)
        {
            Console.WriteLine(num);
        }
    }
}
