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
        
        static IEnumerable<Dictionary<string, string>> KeysPerPassport(IEnumerable<string> input)
        {
            var kvps = new Dictionary<string, string>();

            foreach (string line in input)
            {
                if (line == "")
                {
                    yield return kvps;
                    kvps = new Dictionary<string, string>();
                }
                else
                {
                    foreach (string field in line.Split())
                    {
                        var kvp = field.Split(':');
                        kvps.Add(kvp[0], kvp[1]);
                    }
                }
            }

            yield return kvps;
        }
    
        static bool PassportIsValid(Dictionary<string, string> kvps)
        {
            var required = new string[] { "byr", "iyr", "eyr", "hgt" , "hcl", "ecl", "pid"};
            return required.All(key => kvps.ContainsKey(key));
        }
    }
}