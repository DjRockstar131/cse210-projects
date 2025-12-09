public class Swimming : Activity
{
    private int _laps; // 50m per lap

    public Swimming(DateTime date, int minutes, int laps)
        : base(date, minutes, "Swimming")
    {
        _laps = laps;
    }

    public override double GetDistance()
    {
        double km = _laps * 50 / 1000.0;
        return km * 0.62;  // convert km → miles
    }

    public override double GetSpeed()
    {
        return (GetDistance() / Minutes) * 60.0;
    }

    public override double GetPace()
    {
        return Minutes / GetDistance();
    }
}
