using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day03 : ISolution
    {
        public string Run(int part, IEnumerable<string> input)
        {
            string[] lines = input.ToArray();

            return part switch
            {
                1 => CountTrees(lines, dX: 3, dY: 1).ToString(),
                2 => CountTrees(lines, (1, 1), (3, 1), (5, 1), (7, 1), (1, 2)).ToString(),
                _ => null
            };
        }

        static int CountTrees(IEnumerable<string> input, int dX, int dY)
        {
            int treeCount = 0;
            int y = dY;
            int x = 0;

            foreach (string line in input)
            {
                if (y == 0)
                {
                    if (line[x % line.Length] == '#') treeCount++;
                    y = dY;
                }

                y--;
                x += dX;
            }

            return treeCount;
        }

        static long CountTrees(IEnumerable<string> input, params (int dX, int dY)[] slopes) =>
            slopes
            .Select(slope => CountTrees(input, slope.dX, slope.dY))
            .Aggregate(1L, (product, treeCount) => product * treeCount);
    }
}