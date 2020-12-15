using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode.Solutions.Year2020
{
    internal class Day04 : ISolution
    {
        static HashSet<string> ValidEyeColors = new() { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
        public string Run(int part, IEnumerable<string> input)
        {
            return part switch
            {
                1 => KeysPerPassport(input).Count(PassportHasRequiredKeys).ToString(),
                2 => KeysPerPassport(input).Where(PassportHasRequiredKeys).Count(PassportHasValidValues).ToString(),
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
    
        static bool PassportHasRequiredKeys(Dictionary<string, string> kvps)
        {
            var required = new string[] { "byr", "iyr", "eyr", "hgt" , "hcl", "ecl", "pid"};
            return required.All(key => kvps.ContainsKey(key));
        }

        static bool PassportHasValidValues(Dictionary<string, string> kvps)
        {
            // birth year
            string byr = kvps["byr"];
            if (byr.Length != 4) return false;
            if (!IsBetween(byr, 1920, 2002)) return false;

            // issue year
            string iyr = kvps["iyr"];
            if (iyr.Length != 4) return false;
            if (!IsBetween(iyr, 2010, 2020)) return false;

            // expiration year
            string eyr = kvps["eyr"];
            if (eyr.Length != 4) return false;
            if (!IsBetween(eyr, 2020, 2030)) return false;

            // height
            string hgt = kvps["hgt"];
            if (hgt.Length < 4) return false;
            string hgtVal = hgt.Substring(0, hgt.Length - 2);
            string units = hgt.Substring(hgtVal.Length, 2);
            bool hgtValid = units switch { "cm" => IsBetween(hgtVal, 150, 193), "in" => IsBetween(hgtVal, 59, 76), _ => false};
            if (!hgtValid) return false;

            // hair color
            string hcl = kvps["hcl"];
            if (hcl.Length != 7) return false;
            static bool charBetween(char c, char min, char max) => min <= c && c <= max;
            static bool isHex(char c) => charBetween(c, '0', '9') || charBetween(c, 'a', 'f') || charBetween(c, 'A', 'F');
            if (hcl[0] != '#' || !hcl.Skip(1).All(isHex)) return false;

            // eye color
            string ecl = kvps["ecl"];
            if (!ValidEyeColors.Contains(ecl)) return false;

            // passport id
            string pid = kvps["pid"];
            if (pid.Length != 9) return false;
            if (!pid.All(c => '0' <= c && c <= '9')) return false;

            // everything checks out
            return true;
        }

        static bool IsBetween(string numText, int min, int max)
        {
            if (!int.TryParse(numText, out int num)) return false;
            return min <= num && num <= max;
        }
    }
}