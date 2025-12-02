using System;
using System.Collections.Generic;
using System.Linq;
using AdventOfCode;

Console.WriteLine();

// parse args and run
SolutionID id = ParseArgs(args);
if (id is not null)
{
    string date = $"{id.Year:d4}-12-{id.Day:d2}";
    Console.WriteLine($"Solving puzzle for {date}, part {id.Part}");
    string result = Solve(id.Year, id.Day, id.Part);

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

static string Solve(int year, int day, int part)
{
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
    return solution.Run(part, input: ReadLines());
}

static IEnumerable<string> ReadLines()
{
    string line;
    while ((line = Console.In.ReadLine()) is not null)
        yield return line;
}

record SolutionID(int Year, int Day, int Part);
