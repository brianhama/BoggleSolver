using System;

namespace BoggleSolver
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            WordDictionary englishDictionary = WordDictionary.LoadWordDictionaryFromFile("english.txt");

            Console.WriteLine("Game board:");

            char[,] letters = new char[4, 4];
            for (int r = 0; r < 4; r++)
            {
                for (int c = 0; c < 4; c++)
                {
                    letters[r, c] = WeightedRandomLetterGenerator.GetRandomChar();
                    Console.Write(letters[r, c] + " ");
                }
                Console.WriteLine();
            }

            GameBoard board = new GameBoard(letters);

            Console.WriteLine();
            Console.WriteLine("Solutions:");

            string[] solutions = board.GetSolutionsForDictionary(englishDictionary);

            foreach (string solution in solutions)
                Console.WriteLine(solution);

            Console.WriteLine();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }
    }
}