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
                2 => YesQuestionsPerGroup(input, checkAll:true).Sum(yesQs => yesQs.Count).ToString(),
                _ => null
            };
        }

        static IEnumerable<HashSet<char>> YesQuestionsPerGroup(IEnumerable<string> input, bool checkAll = false)
        {
            HashSet<char> yesQuestions = null;

            foreach (string line in input)
            {
                if (line == "")
                {
                    yield return yesQuestions;
                    yesQuestions = null;
                }
                else
                {
                    void addLine(HashSet<char> set)
                    {
                        foreach (char yesQuestion in line)
                            set.Add(yesQuestion);
                    }

                    if (yesQuestions is null)
                    {
                        yesQuestions = new HashSet<char>();
                        addLine(yesQuestions);
                    }
                    else
                    {
                        if (checkAll)
                            yesQuestions.IntersectWith(line);
                        else
                            addLine(yesQuestions);
                    }
                }
            }

            yield return yesQuestions;
        }
    }
}