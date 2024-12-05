namespace AoC2024_04;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_04.txt";

    static void Main()
    {
        Puzzle1();
        Puzzle2();
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        var pattern = "XMAS";
        var lines = File.ReadAllLines(inputFile);
        int xmasCount = 0;
        for (int y = 0; y < lines.Length; y++)
        {
            if (string.IsNullOrWhiteSpace(lines[y])) continue;
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (lines[y][x] != pattern[0]) continue;
                for (int y1 = -1; y1 <= 1; y1++)
                {
                    for (int x1 = -1; x1 <= 1; x1++)
                    {
                        int pos = 1;
                        int y2 = y + y1;
                        int x2 = x + x1;
                        while (pos < pattern.Length &&
                            y2 >= 0 && y2 < lines.Length &&
                            x2 >= 0 && x2 < lines[y2].Length &&
                            lines[y2][x2] == pattern[pos])
                        {
                            pos++;
                            y2 += y1;
                            x2 += x1;
                        }
                        if (pos == pattern.Length)
                        {
                            xmasCount++;
                        }
                    }
                }
            }
        }
        Console.WriteLine($"Day 04 Puzzle 1 Answer = {xmasCount}");
    }

    static void Puzzle2()
    {
        var pattern = "MAS";
        var lines = File.ReadAllLines(inputFile);
        int xmasCount = 0;
        for (int y = 1; y < lines.Length - 1; y++)
        {
            if (string.IsNullOrWhiteSpace(lines[y])) continue;
            for (int x = 1; x < lines[y].Length - 1; x++)
            {
                if (lines[y][x] != pattern[1]) continue; // not 'A'
                if ((lines[y - 1][x - 1] == pattern[0] || lines[y - 1][x - 1] == pattern[2]) &&
                    (lines[y + 1][x + 1] == pattern[0] || lines[y + 1][x + 1] == pattern[2]) &&
                    lines[y - 1][x - 1] != lines[y + 1][x + 1] &&
                    (lines[y - 1][x + 1] == pattern[0] || lines[y - 1][x + 1] == pattern[2]) &&
                    (lines[y + 1][x - 1] == pattern[0] || lines[y + 1][x - 1] == pattern[2]) &&
                    lines[y - 1][x + 1] != lines[y + 1][x - 1])
                {
                    xmasCount++;
                }
            }
        }
        Console.WriteLine($"Day 04 Puzzle 2 Answer = {xmasCount}");
    }
}
