namespace AoC2024_12;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_12.txt";

    private static int height = 0;
    private static int width = 0;

    private static bool[,] visited = new bool[0, 0];

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
        visited = new bool[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                visited[y, x] = false;
            }
        }
        var regions = GetRegions(lines);
        foreach (Region r in regions)
        {
            answer += r.Area() * r.Parimeter();
        }
        Console.WriteLine($"Day 12 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        // ...
        Console.WriteLine($"Day 12 Puzzle 2 Answer = {answer}");
    }

    static List<Region> GetRegions(string[] lines)
    {
        List<Region> results = [];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (!visited[y, x])
                {
                    var region = new Region()
                    {
                        Plant = lines[y][x]
                    };
                    AddPlotToRegion(region, y, x, lines);
                    results.Add(region);
                }
            }
        }
        return results;
    }

    static void AddPlotToRegion(Region region, int y, int x, string[] lines)
    {
        char plant = lines[y][x];
        Plot plot = new()
        {
            Plant = plant,
            Y = y,
            X = x,
            Perimeter = 0
        };
        if (y - 1 < 0 || lines[y - 1][x] != plant) plot.Perimeter++;
        if (x + 1 >= width || lines[y][x + 1] != plant) plot.Perimeter++;
        if (y + 1 >= height || lines[y + 1][x] != plant) plot.Perimeter++;
        if (x - 1 < 0 || lines[y][x - 1] != plant) plot.Perimeter++;
        visited[y, x] = true;
        region.Plots.Add(plot);
        // check adjacent plots
        if (y - 1 >= 0 && !visited[y - 1, x] && lines[y - 1][x] == plant)
        {
            AddPlotToRegion(region, y - 1, x, lines);
        }
        if (x + 1 < width && !visited[y, x + 1] && lines[y][x + 1] == plant)
        {
            AddPlotToRegion(region, y, x + 1, lines);
        }
        if (y + 1 < height && !visited[y + 1, x] && lines[y + 1][x] == plant)
        {
            AddPlotToRegion(region, y + 1, x, lines);
        }
        if (x - 1 >= 0 && !visited[y, x - 1] && lines[y][x - 1] == plant)
        {
            AddPlotToRegion(region, y, x - 1, lines);
        }
    }
}
