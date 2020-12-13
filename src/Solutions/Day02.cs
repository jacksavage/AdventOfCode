using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day02 : ISolution
    {
        record Password(int Pos1, int Pos2, char Letter, string Text);

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
                1 => CountValidPasswords(passwords, IsValid1).ToString(),
                2 => CountValidPasswords(passwords, IsValid2).ToString(),
                _ => null
            };
        }

        static bool IsValid1(Password password)
        {
            var letterCount = password.Text.Count(letter => letter == password.Letter);
            return password.Pos1 <= letterCount && letterCount <= password.Pos2;
        }

        static bool IsValid2(Password password)
        {
            bool isMatch(int pos) => password.Text[pos - 1] == password.Letter;
            return isMatch(password.Pos1) ^ isMatch(password.Pos2);
        }

        static int CountValidPasswords(IEnumerable<Password> passwords, Func<Password, bool> predicate) =>
            passwords.Count(predicate);
    }
}