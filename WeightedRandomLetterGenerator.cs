using System;
using System.Linq;

namespace BoggleSolver
{
    public class WeightedRandomLetterGenerator
    {
        private static Random _random = new Random();

        private static int _totalWeight = -1;

        private static WeightedLetter[] _weightedLetters = new[] { new WeightedLetter('e', 19), new WeightedLetter('t', 13), new WeightedLetter('a', 12), new WeightedLetter('o', 11), 
            new WeightedLetter('i', 11), new WeightedLetter('n', 11), new WeightedLetter('s', 9), new WeightedLetter('r', 12), new WeightedLetter('h', 5), new WeightedLetter('d', 6), 
            new WeightedLetter('l', 5), new WeightedLetter('u', 4), new WeightedLetter('c', 5), new WeightedLetter('m', 4), new WeightedLetter('f', 4), new WeightedLetter('y', 3), 
            new WeightedLetter('w', 2), new WeightedLetter('g', 3), new WeightedLetter('p', 4), new WeightedLetter('b', 1), new WeightedLetter('v', 1), new WeightedLetter('k', 1), 
            new WeightedLetter('x', 1), new WeightedLetter('q', 1), new WeightedLetter('j', 1), new WeightedLetter('z', 1) };

        public static char GetRandomChar()
        {
            if (_totalWeight == -1)
                _totalWeight = _weightedLetters.Sum(a => a.Weight);

            int choice = _random.Next(_totalWeight);
            int sum = 0;

            foreach (WeightedLetter letter in _weightedLetters)
            {
                sum += letter.Weight;
                if (sum > choice)
                    return letter.Letter;
            }

            return _weightedLetters[0].Letter;
        }

        class WeightedLetter
        {
            public char Letter;
            public int Weight = 0;

            public WeightedLetter(char c, int w)
            {
                this.Letter = c;
                this.Weight = w;
            }
        }
    }
}