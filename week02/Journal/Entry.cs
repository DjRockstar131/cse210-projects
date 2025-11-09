using System;

public class Entry
{
    public string Date { get; set; } = "";
    public string Prompt { get; set; } = "";
    public string Response { get; set; } = "";

    // Small creative add: optional Mood (1–5). Leave empty if unused.
    public string Mood { get; set; } = "";

    public override string ToString()
    {
        var moodPart = string.IsNullOrWhiteSpace(Mood) ? "" : $"  |  Mood: {Mood}/5";
        return $"[{Date}] {Prompt}\n{Response}{moodPart}";
    }
}
