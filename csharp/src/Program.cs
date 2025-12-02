using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using AdventOfCode;

Console.WriteLine();

// parse args and run
SolutionID id = ParseArgs(args);
if (id is not null)
{
    string date = $"{id.Year:d4}-12-{id.Day:d2}";
    Console.WriteLine($"Solving puzzle for {date}, part {id.Part}");
    string result = await Solve(id.Year, id.Day, id.Part);

    if (result is null)
        Console.WriteLine("Solution not found");
    else
        Console.WriteLine($"Solution: \"{result}\"");
}

Console.WriteLine();

static SolutionID ParseArgs(string[] args)
{
    if (
        args.Length == 3
        && int.TryParse(args[0], out int year)
        && int.TryParse(args[1], out int day)
        && int.TryParse(args[2], out int part)
    )
        return new SolutionID(year, day, part);

    Console.WriteLine(
        "Failed to parse command line arguments\n"
            + "Please provide the puzzle year, day, and part\n"
            + "Usage: aoc <year> <day> <part>"
    );

    return null;
}

static async Task<string> Solve(int year, int day, int part)
{
    // get a reader for the input
    IEnumerable<string> input = await ReadInput(year, day);
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

static async Task<IEnumerable<string>> ReadInput(int year, int day)
{
    var dataDir = Path.Combine("data", year.ToString("d4"));
    if (!Directory.Exists(dataDir))
        Directory.CreateDirectory(dataDir);
    var path = Path.Combine(dataDir, day.ToString("d2"));

    if (!File.Exists(path))
    {
        var url = $"https://adventofcode.com/{year}/day/{day}/input";

        var cookie = ReadCookie();
        if (cookie is null)
            return null;

        Console.WriteLine("Downloading input file");

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("Cookie", $"session={cookie}");

        try
        {
            var response = await client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            File.WriteAllText(path, content);
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
    var cookieFileName = "session-cookie";
    if (File.Exists(cookieFileName))
        return File.ReadAllText(cookieFileName);
    Console.WriteLine($"Missing file {cookieFileName}");
    return null;
}

record SolutionID(int Year, int Day, int Part);
