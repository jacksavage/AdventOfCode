using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day04 : ISolution
    {
        public string Run(int part, IEnumerable<string> input)
        {
            return part switch
            {
                1 => KeysPerPassport(input).Count(PassportIsValid).ToString(),
                _ => null
            };
        }
        
        static IEnumerable<HashSet<string>> KeysPerPassport(IEnumerable<string> input)
        {
            var keys = new HashSet<string>();

            foreach (string line in input)
            {
                if (line == "")
                {
                    yield return keys;
                    keys = new HashSet<string>();
                }
                else
                {
                    foreach (string field in line.Split())
                        keys.Add(field.Split(':')[0]);
                }
            }

            yield return keys;
        }
    
        static bool PassportIsValid(HashSet<string> keys)
        {
            var required = new string[] { "byr", "iyr", "eyr", "hgt" , "hcl", "ecl", "pid"};
            return required.All(key => keys.Contains(key));
        }
    }
}