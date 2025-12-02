using System;
using System.IO;
using System.Net;
using System.Collections.Generic;

namespace AdventOfCode
{
    static class Program
    {
        record SolutionID(int Year, int Day, int Part);

        public static void Main(string[] args)
        {
            Console.WriteLine();

            // parse args and run
            SolutionID id = ParseArgs(args);
            if (id is not null)
            {
                string date = $"{id.Year:d4}-12-{id.Day:d2}";
                Console.WriteLine($"Solving puzzle for {date}, part {id.Part}");
                string result = Solve(id.Year, id.Day, id.Part);
                
                if (result is null) Console.WriteLine("Solution not found");
                else Console.WriteLine($"Solution: \"{result}\"");
            }

            Console.WriteLine();
        }

        static SolutionID ParseArgs(string[] args)
        {
            if (
                args.Length > 2 &&
                int.TryParse(args[0], out int year) &&
                int.TryParse(args[1], out int day) &&
                int.TryParse(args[2], out int part)
            ) return new SolutionID(year, day, part);

            Console.WriteLine(
                "Failed to parse command line arguments\n" +
                "Please provide the puzzle year, day, and part\n" +
                "Usage: aoc <year> <day> <part>"
            );

            return null;
        }

        static string Solve(int year, int day, int part)
        {
            // get a reader for the input
            IEnumerable<string> input = ReadInput(year, day);
            if (input is null)
            {
                Console.WriteLine("Failed to read input");
                return null;
            }

            // create an instance of the solution
            string typeName = $"AdventOfCode.Solutions.Year{year}.Day{day:d2}";
            Type type = Type.GetType(typeName);
            if (type is null)
            {
                Console.WriteLine($"No solution found with the name {typeName}");
                return null;
            }
            var solution = (ISolution)Activator.CreateInstance(type);

            // run it and return the result
            return solution.Run(part, input);
        }

        static IEnumerable<string> ReadInput(int year, int day)
        {
            var dataDir = Path.Combine("data", year.ToString("d4"));
            if (!Directory.Exists(dataDir)) Directory.CreateDirectory(dataDir);
            var path = Path.Combine(dataDir, day.ToString("d2"));

            if (!File.Exists(path))
            {
                var url = $"https://adventofcode.com/{year}/day/{day}/input";
                var client = new WebClient();

                var cookie = ReadCookie();
                if (cookie is null) return null;

                Console.WriteLine("Downloading input file");
                client.Headers.Add(HttpRequestHeader.Cookie, $"session={cookie}");

                try
                {
                    client.DownloadFile(url, path);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Failed to download input file");
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            return File.ReadLines(path);
        }

        static string ReadCookie()
        {
            var cookieFileName = "session_cookie";
            if (File.Exists(cookieFileName))
                return File.ReadAllText(cookieFileName);
            Console.WriteLine($"Missing file {cookieFileName}");
            return null;
        }
    }
}
