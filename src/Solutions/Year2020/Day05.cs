using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day05 : ISolution
    {
        public string Run(int part, IEnumerable<string> input)
        {
            int c1 = 7, r1 = 127;
            return part switch
            {
                1 => input.Select(line => SeatId(line, c1, r1)).Max().ToString(),
                2 =>
                    FindSeat(
                        seatIDs:input.Select(line => SeatId(line, c1, r1)),
                        c1, r1
                    ).ToString(),
                _ => null
            };
        }

        static int SeatId(string bsp, int c1, int r1)
        {
            int c0 = 0, r0 = 0;

            foreach (char c in bsp)
            {
                if (c == 'F' || c == 'B') (r0, r1) = Partition(r0, r1, c == 'F');
                else if (c == 'L' || c == 'R') (c0, c1) = Partition(c0, c1, c == 'L');
            }

            return r0 * 8 + c0;
        }

        static (int y0, int y1) Partition(int x0, int x1, bool lowerHalf) =>
            lowerHalf ? (x0, x0 + (x1 - x0) / 2) : (x0 + (x1 - x0) / 2 + 1, x1);
    
        static int FindSeat(IEnumerable<int> seatIDs, int c1, int r1)
        {
            int[] buffer = seatIDs.OrderBy(s => s).ToArray();
            for (int i = 0; i < buffer.Length - 1; i++)
            {
                if (buffer[i + 1] == buffer[i] + 2) return buffer[i] + 1;
            }

            return -1;
        }
    }
}