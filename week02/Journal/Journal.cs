using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

public class Journal
{
    // Using a rare delimiter per simplification guidance.
    private const string Delim = "~|~";

    public List<Entry> Entries { get; } = new();

    public void Add(Entry e) => Entries.Add(e);

    public void Display()
    {
        if (Entries.Count == 0)
        {
            Console.WriteLine("No entries yet.\n");
            return;
        }

        Console.WriteLine("----- Journal -----");
        foreach (var e in Entries)
        {
            Console.WriteLine(e.ToString());
            Console.WriteLine();
        }
    }

    // ======== SIMPLE SAVE/LOAD (Custom delimiter) ========
    public void SaveDelimited(string path)
    {
        using var sw = new StreamWriter(path, false, Encoding.UTF8);
        foreach (var e in Entries)
        {
            // Order: Date ~|~ Prompt ~|~ Response ~|~ Mood
            sw.WriteLine(string.Join(Delim, new[]
            {
                e.Date ?? "", e.Prompt ?? "", e.Response ?? "", e.Mood ?? ""
            }));
        }
    }

    public void LoadDelimited(string path)
    {
        Entries.Clear();
        foreach (var line in File.ReadLines(path, Encoding.UTF8))
        {
            var parts = line.Split(Delim);
            var entry = new Entry
            {
                Date = parts.Length > 0 ? parts[0] : "",
                Prompt = parts.Length > 1 ? parts[1] : "",
                Response = parts.Length > 2 ? parts[2] : "",
                Mood = parts.Length > 3 ? parts[3] : ""
            };
            Entries.Add(entry);
        }
    }

    // ======== CSV SAVE/LOAD (Exceeds Requirements) ========
    // RFC4180-style minimal handling: wrap each field in quotes, double any embedded quotes.
    private static string CsvEscape(string s) => $"\"{(s ?? "").Replace("\"", "\"\"")}\"";

    public void SaveCsv(string path)
    {
        using var sw = new StreamWriter(path, false, Encoding.UTF8);
        // Header
        sw.WriteLine("Date,Prompt,Response,Mood");
        foreach (var e in Entries)
        {
            sw.WriteLine(string.Join(",", new[]
            {
                CsvEscape(e.Date), CsvEscape(e.Prompt), CsvEscape(e.Response), CsvEscape(e.Mood)
            }));
        }
    }

    // Basic CSV parser that respects quotes and commas.
    public void LoadCsv(string path)
    {
        Entries.Clear();
        bool skipHeader = true;
        foreach (var line in File.ReadLines(path, Encoding.UTF8))
        {
            if (skipHeader) { skipHeader = false; continue; }
            var fields = ParseCsvLine(line);
            var entry = new Entry
            {
                Date = fields.Count > 0 ? fields[0] : "",
                Prompt = fields.Count > 1 ? fields[1] : "",
                Response = fields.Count > 2 ? fields[2] : "",
                Mood = fields.Count > 3 ? fields[3] : ""
            };
            Entries.Add(entry);
        }
    }

    private static List<string> ParseCsvLine(string line)
    {
        var result = new List<string>();
        if (line is null) return result;

        var sb = new StringBuilder();
        bool inQuotes = false;

        for (int i = 0; i < line.Length; i++)
        {
            char c = line[i];

            if (inQuotes)
            {
                if (c == '\"')
                {
                    // Double quote inside quotes -> literal quote
                    if (i + 1 < line.Length && line[i + 1] == '\"')
                    {
                        sb.Append('\"'); i++;
                    }
                    else
                    {
                        inQuotes = false;
                    }
                }
                else
                {
                    sb.Append(c);
                }
            }
            else
            {
                if (c == ',')
                {
                    result.Add(sb.ToString());
                    sb.Clear();
                }
                else if (c == '\"')
                {
                    inQuotes = true;
                }
                else
                {
                    sb.Append(c);
                }
            }
        }
        result.Add(sb.ToString());
        return result;
    }
}
