namespace AoC2024_14;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_14.txt";

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
        List<Robot> robots = [];
        var height = 103;
        var width = 101;
        var moves = 100;
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            var lineParts = s.Split(' ', StringSplitOptions.TrimEntries);
            var starting = lineParts[0][2..].Split(',').Select(x => int.Parse(x)).ToArray();
            var vector = lineParts[1][2..].Split(',').Select(x => int.Parse(x)).ToArray();
            var r = new Robot()
            {
                Position = new Point(starting[1], starting[0]),
                Vector = new Point(vector[1], vector[0])
            };
            robots.Add(r);
        }
        int[] quadrants = new int[4];
        foreach (Robot r in robots)
        {
            var EndingY = (((r.Position.Y + (r.Vector.Y * moves)) % height) + height) % height;
            var EndingX = (((r.Position.X + (r.Vector.X * moves)) % width) + width) % width;
            if (EndingY < height / 2)
            {
                if (EndingX < width / 2)
                {
                    quadrants[0]++;
                }
                else if (EndingX > width / 2)
                {
                    quadrants[1]++;
                }
            }
            else if (EndingY > height / 2)
            {
                if (EndingX < width / 2)
                {
                    quadrants[2]++;
                }
                else if (EndingX > width / 2)
                {
                    quadrants[3]++;
                }
            }
        }
        answer = quadrants[0] * quadrants[1] * quadrants[2] * quadrants[3];
        Console.WriteLine($"Day 14 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        List<Robot> robots = [];
        var height = 103;
        var width = 101;
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            var lineParts = s.Split(' ', StringSplitOptions.TrimEntries);
            var starting = lineParts[0][2..].Split(',').Select(x => int.Parse(x)).ToArray();
            var vector = lineParts[1][2..].Split(',').Select(x => int.Parse(x)).ToArray();
            var r = new Robot()
            {
                Position = new Point(starting[1], starting[0]),
                Vector = new Point(vector[1], vector[0])
            };
            robots.Add(r);
        }
        while (true)
        {
            answer++;
            var grid = new bool[height, width];
            foreach (Robot r in robots)
            {
                grid[r.Position.Y, r.Position.X] = true;
                r.Position.Y = (((r.Position.Y + r.Vector.Y) % height) + height ) % height;
                r.Position.X = (((r.Position.X + r.Vector.X) % height) + height ) % height;
            }
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Console.Write(grid[y,x] ? '#' :  ' ');
                }
                Console.WriteLine();
            }
            break;
        }
        Console.WriteLine($"Day 14 Puzzle 2 Answer = {answer}");
    }
}
