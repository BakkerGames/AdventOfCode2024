namespace AoC2024_08;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_08.txt";

    static void Main()
    {
        Puzzle1();
        Console.WriteLine();
        Puzzle2();
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        int answer = 0;
        var lines = File.ReadAllLines(inputFile);
        var height = lines.Length;
        var width = lines[0].Length;
        Dictionary<char, List<Point>> freqs = [];
        for (int y = 0; y < height; y++)
        {
            if (string.IsNullOrWhiteSpace(lines[y])) continue;
            for (int x = 0; x < width; x++)
            {
                char c = lines[y][x];
                if (c == '.') continue;
                freqs.TryAdd(c, []);
                freqs[c].Add(new Point(y, x));
            }
        }
        List<Point> answerList = [];
        foreach (char c in freqs.Keys)
        {
            for (int i = 0; i < freqs[c].Count - 1; i++)
            {
                var p1 = freqs[c][i];
                for (int j = i + 1; j < freqs[c].Count; j++)
                {
                    var p2 = freqs[c][j];
                    var yOfs = p2.Y - p1.Y;
                    var xOfs = p2.X - p1.X;
                    var antinode1 = new Point(p1.Y - yOfs, p1.X - xOfs);
                    if (!answerList.Contains(antinode1))
                    {
                        if (antinode1.Y >= 0 && antinode1.Y < height && antinode1.X >= 0 && antinode1.X < width)
                        {
                            answerList.Add(antinode1);
                        }
                    }
                    var antinode2 = new Point(p2.Y + yOfs, p2.X + xOfs);
                    if (!answerList.Contains(antinode2))
                    {
                        if (antinode2.Y >= 0 && antinode2.Y < height && antinode2.X >= 0 && antinode2.X < width)
                        {
                            answerList.Add(antinode2);
                        }
                    }
                }
            }
        }
        foreach (Point a in answerList)
        {
            answer++;
        }
        Console.WriteLine($"Day 08 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        int answer = 0;
        var lines = File.ReadAllLines(inputFile);
        var height = lines.Length;
        var width = lines[0].Length;
        Dictionary<char, List<Point>> freqs = [];
        for (int y = 0; y < height; y++)
        {
            if (string.IsNullOrWhiteSpace(lines[y])) continue;
            for (int x = 0; x < width; x++)
            {
                char c = lines[y][x];
                if (c == '.') continue;
                freqs.TryAdd(c, []);
                freqs[c].Add(new Point(y, x));
            }
        }
        List<Point> answerList = [];
        foreach (char c in freqs.Keys)
        {
            for (int i = 0; i < freqs[c].Count - 1; i++)
            {
                var p1 = freqs[c][i];
                for (int j = i + 1; j < freqs[c].Count; j++)
                {
                    var p2 = freqs[c][j];
                    var yOfs = p2.Y - p1.Y;
                    var xOfs = p2.X - p1.X;
                    var loop = 0;
                    while (true)
                    {
                        var a = new Point(p1.Y - (loop * yOfs), p1.X - (loop * xOfs));
                        if (a.Y < 0 || a.Y >= height || a.X < 0 || a.X >= width)
                        {
                            break;
                        }
                        if (!answerList.Contains(a))
                        {
                            answerList.Add(a);
                        }
                        loop++;
                    }
                    loop = 0;
                    while (true)
                    {
                        var a = new Point(p2.Y + (loop * yOfs), p2.X + (loop * xOfs));
                        if (a.Y < 0 || a.Y >= height || a.X < 0 || a.X >= width)
                        {
                            break;
                        }
                        if (!answerList.Contains(a))
                        {
                            answerList.Add(a);
                        }
                        loop++;
                    }
                }
            }
        }
        foreach (Point a in answerList)
        {
            answer++;
        }
        Console.WriteLine($"Day 08 Puzzle 2 Answer = {answer}");
    }
}
