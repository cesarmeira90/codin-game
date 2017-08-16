using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Solution
{
    class ScrabbleMagic
    {
        public class ScrabbleWord
        {
            public string Word { get; set; }
            public int Points { get; set; }
        }

        public int GetWordPoints(string word)
        {
            int score = 0;
            foreach (var letter in word)
            {
                score += GetLetterPoints(letter);
            }
            return score;
        }

        public int GetLetterPoints(char letter)
        {
            switch (letter)
            {
                case 'a':
                case 'e':
                case 'i':
                case 'o':
                case 'n':
                case 'r':
                case 't':
                case 'l':
                case 's':
                case 'u':
                    return 1;
                case 'd':
                case 'g':
                    return 2;
                case 'b':
                case 'c':
                case 'm':
                case 'p':
                    return 3;
                case 'y':
                case 'w':
                case 'v':
                case 'h':
                case 'f':
                    return 4;
                case 'k':
                    return 5;
                case 'j':
                case 'x':
                    return 8;
                case 'q':
                case 'z':
                    return 10;
                default:
                    return 0;
            }
        }

        public List<ScrabbleWord> ValidWords = new List<ScrabbleWord>();

        public List<ScrabbleWord> GetPossibleWords(string letters)
        {
            List<ScrabbleWord> validWords = new List<ScrabbleWord>();
            List<char> chars;
            foreach (var ws in ValidWords)
            {
                string word = ws.Word;
                chars = new List<char>();
                chars.AddRange(letters);
                bool isValidWord = true;
                foreach (char letter in word)
                {
                    if (chars.Contains(letter))
                    {
                        chars.Remove(letter);
                    }
                    else
                    {
                        isValidWord = false;
                        break;
                    }
                }
                if (isValidWord)
                {
                    validWords.Add(ws);
                }
            }
            return validWords;
        }

        public string GetBestWord(string letters)
        {
            var validWords = GetPossibleWords(letters);
            // Game says that there is always a possible word.
            return validWords.OrderByDescending(sw => sw.Points).First().Word;
        }
    }



    static void Main(string[] args)
    {
        ScrabbleMagic scrableMagic = new ScrabbleMagic();

        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {
            string W = Console.ReadLine();
            scrableMagic.ValidWords.Add(new ScrabbleMagic.ScrabbleWord()
            {
                Word = W,
                Points = scrableMagic.GetWordPoints(W)
            });
        }
        var Letters = Console.ReadLine();
        
        Console.WriteLine(scrableMagic.GetBestWord(Letters));
    }


    static void Log(string message, params object[] args)
    {
        Console.Error.WriteLine(string.Format(":: {0}", string.Format(message, args)));
    }

}