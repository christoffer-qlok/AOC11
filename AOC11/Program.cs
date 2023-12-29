using System.Collections.Generic;

namespace AOC11
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int expPart2 = 1000000;
            const int expPart1 = 2;
            var lines = File.ReadAllLines("input.txt");
            var map = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                map[i] = lines[i].ToCharArray();
            }

            var emptyCols = new HashSet<int>();
            for (int col = 0; col < map[0].Length; col++)
            {
                bool empty = true;
                for (int row = 0; row < map.Length; row++)
                {
                    if (map[row][col] != '.')
                    {
                        empty = false;
                        break;
                    }
                }
                if (empty)
                {
                    emptyCols.Add(col);
                }
            }

            var emptyRows = new HashSet<int>();
            for (int row = 0; row < map.Length; row++)
            {
                if (map[row].All(c => c == '.'))
                {
                    emptyRows.Add(row);
                }
            }

            var galaxies = new List<Coord>();

            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == '#')
                    {
                        galaxies.Add(new Coord() { X = x, Y = y });
                    }
                }
            }

            var combinations = (from item1 in galaxies
                               from item2 in galaxies
                               where item1.CompareTo(item2) < 0
                               select Tuple.Create(item1, item2)).ToArray();

            long sumPart1 = GetTotalDistances(emptyCols, emptyRows, combinations, expPart1);
            long sumPart2 = GetTotalDistances(emptyCols, emptyRows, combinations, expPart2);

            Console.WriteLine($"Part 1: {sumPart1}");
            Console.WriteLine($"Part 2: {sumPart2}");
        }

        private static long GetTotalDistances(HashSet<int> emptyCols, HashSet<int> emptyRows, Tuple<Coord, Coord>[] combinations, int exp)
        {
            long sum = 0;
            foreach (var item in combinations)
            {
                sum += Math.Abs(item.Item1.X - item.Item2.X);
                sum += Math.Abs(item.Item1.Y - item.Item2.Y);
                foreach (var col in emptyCols)
                {
                    if ((item.Item1.X > col && item.Item2.X < col) || (item.Item2.X > col && item.Item1.X < col))
                    {
                        sum += exp - 1;
                    }
                }

                foreach (var row in emptyRows)
                {
                    if ((item.Item1.Y > row && item.Item2.Y < row) || (item.Item2.Y > row && item.Item1.Y < row))
                    {
                        sum += exp - 1;
                    }
                }
            }

            return sum;
        }
    }
}