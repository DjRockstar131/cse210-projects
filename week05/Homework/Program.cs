using System;

class Program
{
    static void Main(string[] args)
    {
        // Test base class
        Assignment a1 = new Assignment("Spencer Norton", "Multiplication");
        Console.WriteLine(a1.GetSummary());
        Console.WriteLine();

        // Test MathAssignment
        MathAssignment m1 = new MathAssignment("KJ Bartholomew", "Fractions", "7.3", "8-19");
        Console.WriteLine(m1.GetSummary());
        Console.WriteLine(m1.GetHomeworkList());
        Console.WriteLine();

        // Test WritingAssignment
        WritingAssignment w1 = new WritingAssignment("Owen Mahan", "European History", "The Causes of World War II");
        Console.WriteLine(w1.GetSummary());
        Console.WriteLine(w1.GetWritingInformation());
    }
}
