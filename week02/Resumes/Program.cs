using System;

public class Program
{
    public static void Main()
    {
        // Create first job and set fields using dot notation
        Job job1 = new Job();
        job1._jobTitle = "Dishwasher / Busser";
        job1._company  = "Orlandos Mexican Restaurant";
        job1._startYear = 2020;
        job1._endYear   = 2023;

        // Create second job
        Job job2 = new Job();
        job2._jobTitle = "Cashier";
        job2._company  = "Orlandos Mexican Restaurant";
        job2._startYear = 2023;
        job2._endYear   = 2025;

        // (Early test you can do if you want)
        // Console.WriteLine(job1._company);
        // Console.WriteLine(job2._company);

        // Create a resume and add jobs
        Resume myResume = new Resume();
        myResume._name = "Davin Quist";
        myResume._jobs.Add(job1);
        myResume._jobs.Add(job2);

        // Verify access (optional test)
        // Console.WriteLine(myResume._jobs[0]._jobTitle);

        // Final output using Display methods
        myResume.Display();
    }
}
