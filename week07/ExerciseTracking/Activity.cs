public abstract class Activity
{
    private DateTime _date;
    private int _minutes;
    private string _activityName;

    protected Activity(DateTime date, int minutes, string name)
    {
        _date = date;
        _minutes = minutes;
        _activityName = name;
    }

    public DateTime Date => _date;
    public int Minutes => _minutes;
    public string ActivityName => _activityName;

    public abstract double GetDistance();  // miles
    public abstract double GetSpeed();     // mph
    public abstract double GetPace();      // min per mile

    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} {ActivityName} ({Minutes} min) - " +
               $"Distance {GetDistance():0.0} miles, " +
               $"Speed {GetSpeed():0.0} mph, " +
               $"Pace: {GetPace():0.0} min per mile";
    }
}
