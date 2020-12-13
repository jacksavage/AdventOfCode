using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day02 : ISolution
    {
        record Password(int Min, int Max, char Letter, string Text);

        public string Run(int part, IEnumerable<string> input)
        {
            var passwords =
                from line in input
                let fields = line.Split(' ')
                let minMax = fields[0].Split('-')
                let min = int.Parse(minMax[0])
                let max = int.Parse(minMax[1])
                let letter = fields[1][0]
                let password = fields[2]
                select new Password(min, max, letter, password);

            return part switch
            {
                1 => Part1(passwords).ToString(),
                _ => null
            };
        }

        static bool IsValid(Password password)
        {
            var letterCount = password.Text.Count(letter => letter == password.Letter);
            return password.Min <= letterCount && letterCount <= password.Max;
        }

        static int Part1(IEnumerable<Password> passwords) =>
            passwords.Where(IsValid).Count();
    }
}