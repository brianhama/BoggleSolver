using System;

namespace BoggleSolver
{
    public class WeightedRandomLetterGenerator
    {
        private const string CONSONANTS = "bcdfghjklmnpqrstvwxyz";
        private const string VOWELS = "aeiou";
        private static Random _random = new Random();

        public static char GetRandomChar()
        {
            if (_random.Next(0, 4) == 1)
                return VOWELS[_random.Next(0, VOWELS.Length)];
            else
                return CONSONANTS[_random.Next(0, CONSONANTS.Length)];
        }
    }
}