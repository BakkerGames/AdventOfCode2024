namespace AoC2024_13;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_13.txt";

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
        long aX = 0;
        long aY = 0;
        long bX = 0;
        long bY = 0;
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            var tokens = s.Replace(",", "").Split(' ', StringSplitOptions.TrimEntries);
            if (tokens[0] == "Button" && tokens[1] == "A:")
            {
                aX = long.Parse(tokens[2][2..]);
                aY = long.Parse(tokens[3][2..]);
            }
            else if (tokens[0] == "Button" && tokens[1] == "B:")
            {
                bX = long.Parse(tokens[2][2..]);
                bY = long.Parse(tokens[3][2..]);
            }
            else if (tokens[0] == "Prize:")
            {
                long prizeX = long.Parse(tokens[1][2..]);
                long prizeY = long.Parse(tokens[2][2..]);
                Console.Write($"{aX}, {aY}, {bX}, {bY} -> ({prizeX},{prizeY}) = ");
                var cost = CalculateTokens1(aX, aY, bX, bY, prizeX, prizeY);
                Console.WriteLine(cost);
                answer += cost;
                Console.WriteLine();
            }
        }
        Console.WriteLine($"Day 13 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        long aX = 0;
        long aY = 0;
        long bX = 0;
        long bY = 0;
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            var tokens = s.Replace(",", "").Split(' ', StringSplitOptions.TrimEntries);
            if (tokens[0] == "Button" && tokens[1] == "A:")
            {
                aX = long.Parse(tokens[2][2..]);
                aY = long.Parse(tokens[3][2..]);
            }
            else if (tokens[0] == "Button" && tokens[1] == "B:")
            {
                bX = long.Parse(tokens[2][2..]);
                bY = long.Parse(tokens[3][2..]);
            }
            else if (tokens[0] == "Prize:")
            {
                long prizeX = long.Parse(tokens[1][2..]) + 10000000000000;
                long prizeY = long.Parse(tokens[2][2..]) + 10000000000000;
                Console.Write($"{aX}, {aY}, {bX}, {bY} -> ({prizeX},{prizeY}) = ");
                var cost = CalculateTokens2(aX, aY, bX, bY, prizeX, prizeY);
                Console.WriteLine(cost);
                answer += cost;
                Console.WriteLine();
            }
        }
        Console.WriteLine($"Day 13 Puzzle 2 Answer = {answer}");
    }

    private static long CalculateTokens1(long aX, long aY, long bX, long bY, long prizeX, long prizeY)
    {
        long maxB = Math.Min(prizeX / bX, prizeY / bY);
        // costs less for b, do first, start at maxB
        for (long bPress = maxB; bPress >= 0; bPress--)
        {
            long aPress = Math.Min((prizeX - (bPress * bX)) / aX, (prizeY - (bPress * bY)) / aY);
            if ((bPress * bX) + (aPress * aX) == prizeX &&
                (bPress * bY) + (aPress * aY) == prizeY)
            {
                return (aPress * 3) + bPress;
            }
        }
        return 0;
    }

    private static long CalculateTokens2(long aX, long aY, long bX, long bY, long prizeX, long prizeY)
    {
        // (aX * aPress) + (bX * bPress) = prizeX
        // (aY * aPress) + (bY * bPress) = prizeY

        long det = (aX * bY) - (aY * bX);
        long aPress = ((prizeX * bY) - (prizeY * bX)) / det;
        long bPress = ((prizeY * aX) - (prizeX * aY)) / det;
        if (((aX * aPress) + (bX * bPress) == prizeX) &&
            ((aY * aPress) + (bY * bPress) == prizeY))
        {
            return aPress * 3 + bPress;
        }
        return 0;
    }
}
