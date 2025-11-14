using System;
using System.Collections.Generic;

public class Scripture
{
    private Reference _reference;
    private List<Word> _words;
    private static Random _random = new Random();

    public Scripture(Reference reference, string text)
    {
        _reference = reference;
        _words = new List<Word>();

        string[] parts = text.Split(' ');

        foreach (string part in parts)
        {
            _words.Add(new Word(part));
        }
    }

    public string GetDisplayText()
    {
        List<string> wordTexts = new List<string>();

        foreach (Word word in _words)
        {
            wordTexts.Add(word.GetDisplayText());
        }

        string scriptureText = string.Join(" ", wordTexts);
        return $"{_reference.GetDisplayText()} - {scriptureText}";
    }

    /// <summary>
    /// Hides a given number of random *visible* words.
    /// </summary>
    public void HideRandomWords(int numberToHide)
    {
        // Build a list of indexes of words that are not yet hidden
        List<int> visibleIndexes = new List<int>();
        for (int i = 0; i < _words.Count; i++)
        {
            if (!_words[i].IsHidden())
            {
                visibleIndexes.Add(i);
            }
        }

        if (visibleIndexes.Count == 0)
        {
            return; // nothing left to hide
        }

        for (int i = 0; i < numberToHide && visibleIndexes.Count > 0; i++)
        {
            int randomListIndex = _random.Next(visibleIndexes.Count);
            int wordIndex = visibleIndexes[randomListIndex];

            _words[wordIndex].Hide();

            // remove so we don't pick the same word again this round
            visibleIndexes.RemoveAt(randomListIndex);
        }
    }

    public bool IsCompletelyHidden()
    {
        foreach (Word word in _words)
        {
            if (!word.IsHidden())
            {
                return false;
            }
        }
        return true;
    }
}
