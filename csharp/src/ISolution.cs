using System.Collections.Generic;

namespace AdventOfCode
{
    internal interface ISolution
    {
        string Run(int part, IEnumerable<string> input);
    }
}
