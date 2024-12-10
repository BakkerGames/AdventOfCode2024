
namespace AoC2024_10;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_10.txt";

    static void Main()
    {
        Puzzle1();
        Console.WriteLine();
        Puzzle2();
        Console.ReadLine();
    }

    static int height;
    static int width;
    static int[,] topo = new int[0, 0];

    static void Puzzle1()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        height = lines.Length;
        width = lines[0].Length;
        topo = new int[height, width];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (!int.TryParse(lines[y][x].ToString(), out topo[y, x]))
                {
                    topo[y, x] = -1;
                }
            }
        }
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (topo[y, x] == 0) // trailhead
                {
                    //Console.WriteLine($"({y},{x}) - {topo[y, x]} - trailhead");
                    var summits = FindSummits(y, x);
                    summits.Sort();
                    var score = 0;
                    for (int i = 0; i < summits.Count; i++)
                    {
                        if (i == 0 || !summits[i].Equals(summits[i - 1]))
                        {
                            score++;
                        }
                    }
                    answer += score;
                }
            }
        }
        //Console.WriteLine();
        Console.WriteLine($"Day 10 Puzzle 1 Answer = {answer}");
    }

    private static List<Point> FindSummits(int y, int x)
    {
        List<Point> result = [];
        if (topo[y, x] == 9) // summit
        {
            //Console.WriteLine($"({y},{x}) - {topo[y,x]} - summit");
            result.Add(new Point(y, x));
            return result;
        }
        var y1 = y - 1;
        var x1 = x;
        if (y1 >= 0 && y1 < height && x1 >= 0 && x1 < width && topo[y1, x1] == topo[y, x] + 1)
        {
            //Console.WriteLine($"({y1},{x1}) - {topo[y1,x1]}");
            result.AddRange(FindSummits(y1, x1));
        }
        y1 = y;
        x1 = x + 1;
        if (y1 >= 0 && y1 < height && x1 >= 0 && x1 < width && topo[y1, x1] == topo[y, x] + 1)
        {
            //Console.WriteLine($"({y1},{x1}) - {topo[y1, x1]}");
            result.AddRange(FindSummits(y1, x1));
        }
        y1 = y + 1;
        x1 = x;
        if (y1 >= 0 && y1 < height && x1 >= 0 && x1 < width && topo[y1, x1] == topo[y, x] + 1)
        {
            //Console.WriteLine($"({y1},{x1}) - {topo[y1, x1]}");
            result.AddRange(FindSummits(y1, x1));
        }
        y1 = y;
        x1 = x - 1;
        if (y1 >= 0 && y1 < height && x1 >= 0 && x1 < width && topo[y1, x1] == topo[y, x] + 1)
        {
            //Console.WriteLine($"({y1},{x1}) - {topo[y1, x1]}");
            result.AddRange(FindSummits(y1, x1));
        }
        return result;
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        height = lines.Length;
        width = lines[0].Length;
        topo = new int[height, width];
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (!int.TryParse(lines[y][x].ToString(), out topo[y, x]))
                {
                    topo[y, x] = -1;
                }
            }
        }
        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                if (topo[y, x] == 0) // trailhead
                {
                    var summits = FindSummits(y, x);
                    answer += summits.Count;
                }
            }
        }
        Console.WriteLine($"Day 10 Puzzle 2 Answer = {answer}");
    }
}
