using System;

public class Reference
{
    private string _book;
    private int _chapter;
    private int _verse;
    private int _endVerse;
    private bool _hasEndVerse;

    // Single-verse constructor: e.g. "Proverbs 3:5"
    public Reference(string book, int chapter, int verse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _hasEndVerse = false;
    }

    // Verse-range constructor: e.g. "Proverbs 3:5-6"
    public Reference(string book, int chapter, int verse, int endVerse)
    {
        _book = book;
        _chapter = chapter;
        _verse = verse;
        _endVerse = endVerse;
        _hasEndVerse = true;
    }

    public string GetDisplayText()
    {
        if (_hasEndVerse)
        {
            return $"{_book} {_chapter}:{_verse}-{_endVerse}";
        }
        else
        {
            return $"{_book} {_chapter}:{_verse}";
        }
    }
}
