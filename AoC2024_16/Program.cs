namespace AoC2024_16;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_16_Test.txt";
    private const char wallChar = '#';
    private const char floorChar = '.';
    private const char startChar = 'S';
    private const char endChar = 'E';

    private static int height = 0;
    private static int width = 0;

    static void Main()
    {
        Puzzle1();
        Console.WriteLine();
        Puzzle2();
        Console.WriteLine();
        Console.Write("Press enter to continue...");
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        height = lines.Length;
        width = lines[0].Length;
        Point startPos;
        Point endPos;
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                if (lines[y][x] == startChar)
                {
                    startPos = new Point(y, x);
                }
                else if (lines[y][x] == endChar)
                {
                    endPos = new Point(y, x);
                }
            }
        }

        // ...
        Console.WriteLine($"Day 16 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        // ...
        Console.WriteLine($"Day 16 Puzzle 2 Answer = {answer}");
    }
}
