using System;

public class Fraction
{
    // Private attributes (fields)
    private int _top;
    private int _bottom;

    // Constructors

    // 1) No-parameter constructor: 1/1
    public Fraction()
    {
        _top = 1;
        _bottom = 1;
    }

    // 2) One-parameter constructor: top / 1
    public Fraction(int top)
    {
        _top = top;
        _bottom = 1;
    }

    // 3) Two-parameter constructor: top / bottom
    public Fraction(int top, int bottom)
    {
        _top = top;
        _bottom = bottom;
    }

    // Getters and setters

    public int GetTop()
    {
        return _top;
    }

    public void SetTop(int top)
    {
        _top = top;
    }

    public int GetBottom()
    {
        return _bottom;
    }

    public void SetBottom(int bottom)
    {
        _bottom = bottom;
    }

    // Return "top/bottom" as a string
    public string GetFractionString()
    {
        return $"{_top}/{_bottom}";
    }

    // Return decimal value (double)
    public double GetDecimalValue()
    {
        return (double)_top / _bottom;
    }
}
