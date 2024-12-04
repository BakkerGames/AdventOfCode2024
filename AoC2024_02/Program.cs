using System.Text;

namespace AoC2024_02;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_02.txt";

    static void Main(string[] args)
    {
        //Puzzle1();
        Puzzle2();
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        var lines = File.ReadAllLines(inputFile);
        var safeReports = 0;
        foreach (string report in lines)
        {
            if (string.IsNullOrWhiteSpace(report)) continue;
            Console.Write(report);
            if (Safe(report, true))
            {
                safeReports++;
                Console.WriteLine(" - Safe");
            }
            else
            {
                Console.WriteLine(" - Unsafe");
            }
        }
        Console.WriteLine($"Day 02 Puzzle 1 Answer = {safeReports}");
    }

    static bool Safe(string report, bool showReason)
    {
        var levels = report.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int lastValue = 0;
        bool increase = false;
        for (int i = 0; i < levels.Length; i++)
        {
            int value = int.Parse(levels[i]);
            if (i > 0)
            {
                var diff = Math.Abs(value - lastValue);
                if (diff < 1 || diff > 3)
                {
                    if (showReason) Console.Write($" - Diff = {diff}");
                    return false;
                }
            }
            if (i == 1)
            {
                increase = lastValue > value;
            }
            else if (i > 1 && increase != lastValue > value)
            {
                if (showReason) Console.Write(" - Flip");
                return false;
            }
            lastValue = value;
        }
        return true;
    }

    static void Puzzle2()
    {
        var lines = File.ReadAllLines(inputFile);
        var safeReports = 0;
        foreach (string report in lines)
        {
            if (string.IsNullOrWhiteSpace(report)) continue;
            Console.Write(report);
            if (Safe(report, true) || RemoveOneLevelSafe(report))
            {
                safeReports++;
                Console.WriteLine(" - Safe");
            }
            else
            {
                Console.WriteLine(" - Unsafe");
            }
        }
        Console.WriteLine($"Day 02 Puzzle 2 Answer = {safeReports}");
    }

    private static bool RemoveOneLevelSafe(string report)
    {
        var levels = report.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        for (int i = 0; i < levels.Length; i++)
        {
            StringBuilder newReport = new();
            for (int j = 0; j < levels.Length; j++)
            {
                if (j != i)
                {
                    if (newReport.Length > 0)
                    {
                        newReport.Append(' ');
                    }
                    newReport.Append(levels[j]);
                }
            }
            if (Safe(newReport.ToString(), false))
            {
                Console.Write($" - Now Safe removing {i}:{levels[i]}");
                return true;
            }
        }
        return false;
    }
}
