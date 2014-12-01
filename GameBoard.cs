using System;
using System.Collections.Generic;
using System.Linq;

namespace BoggleSolver
{
    public class GameBoard
    {
        private readonly char[,] _letters;

        public GameBoard(char[,] letters)
        {
            if (letters.GetLength(0) != 4 || letters.GetLength(1) != 4)
            {
                throw new ArgumentException("Game board must be size 4x4.", "letters");
            }

            _letters = letters;
        }

        public char[,] Letters
        {
            get
            {
                return _letters;
            }
        }

        public string[] GetSolutionsForDictionary(WordDictionary dictionary)
        {
            SortedSet<String> solutions = new SortedSet<String>();

            for (int rowIndex = 0; rowIndex < 4; rowIndex++)
                for (int columnIndex = 0; columnIndex < 4; columnIndex++)
                    GetSolutionsForGameLetter(solutions, dictionary, columnIndex, rowIndex);

            return solutions.ToArray();
        }

        private void GetSolutionsForGameLetter(SortedSet<String> solutions, WordDictionary dictionary, Int32 columnIndex, Int32 rowIndex, String currentWord = "")
        {
            char nextChar = Letters[rowIndex, columnIndex];

            currentWord = currentWord + nextChar;
            Letters[rowIndex, columnIndex] = ' ';

            WordDictionary.LookupResult result = dictionary.LookupWord(currentWord);

            switch (result)
            {
                case WordDictionary.LookupResult.Match:
                    if (!solutions.Contains(currentWord))
                        solutions.Add(currentWord);
                    break;

                case WordDictionary.LookupResult.NoMatch:
                    return;
            }

            for (int rowModifier = -1; rowModifier <= 1; rowModifier++)
            {
                Int32 nextRowIndex = rowIndex + rowModifier;
                if (nextRowIndex >= 0 && nextRowIndex <= 3)
                {
                    for (int columnModifier = -1; columnModifier <= 1; columnModifier++)
                    {
                        Int32 nextColumnIndex = columnIndex + columnModifier;
                        if (nextColumnIndex >= 0 && nextColumnIndex <= 3)
                        {
                            char nextLetter = Letters[nextRowIndex, nextColumnIndex];

                            if (!Char.IsWhiteSpace(nextLetter))
                            {
                                GetSolutionsForGameLetter(solutions, dictionary, nextColumnIndex, nextRowIndex, currentWord);
                            }
                        }
                    }
                }
            }

            Letters[rowIndex, columnIndex] = nextChar;
        }
    }
}