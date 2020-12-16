using System.Linq;
using System.Collections.Generic;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day06 : ISolution
    {
        public string Run(int part, IEnumerable<string> input)
        {
            return part switch
            {
                1 => YesQuestionsPerGroup(input).Sum(yesQs => yesQs.Count).ToString(),
                _ => null
            };
        }

        static IEnumerable<HashSet<char>> YesQuestionsPerGroup(IEnumerable<string> input)
        {
            var yesQuestions = new HashSet<char>();

            foreach (string line in input)
            {
                if (line == "")
                {
                    yield return yesQuestions;
                    yesQuestions = new HashSet<char>();
                }
                else
                {
                    foreach (char yesQuestion in line)
                        yesQuestions.Add(yesQuestion);
                }
            }

            yield return yesQuestions;
        }
    }
}