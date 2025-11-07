using System;
using System.Collections.Generic;

public class Resume
{
    // Member variables (fields)
    public string _name = "";
    public List<Job> _jobs = new List<Job>();

    // Behavior
    public void Display()
    {
        Console.WriteLine(_name);
        foreach (var job in _jobs)
        {
            job.Display(); // reuse Job's display behavior
        }
    }
}
