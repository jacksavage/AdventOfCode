using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day03 : ISolution
    {
        public string Run(int part, IEnumerable<string> input)
        {
            return part switch
            {
                1 => CountTrees(dX: 3, dY: 1, input:input).ToString(),
                _ => null
            };
        }

        static int CountTrees(int dX, int dY, IEnumerable<string> input)
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
    }
}