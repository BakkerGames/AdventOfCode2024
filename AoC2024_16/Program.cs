namespace AoC2024_16;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_16_Test_1.txt";
    private const char wallChar = '#';
    private const char floorChar = '.';
    private const char startChar = 'S';
    private const char endChar = 'E';

    private const bool showPath = true;

    private static int height = 0;
    private static int width = 0;
    private static char[,] grid = new char[0, 0];
    private static int[,] score = new int[0, 0];
    private static Point startPos = new();
    private static Point endPos = new();

    private static long bestScore;
    private static List<Point> bestSeats = [];

    // 0 = north, 1 = east, 2 = south, 3 = west
    private static readonly int[] NextMoveY = [-1, 0, 1, 0];
    private static readonly int[] NextMoveX = [0, 1, 0, -1];

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
        long answer;
        var lines = File.ReadAllLines(inputFile);
        height = lines.Length;
        width = lines[0].Length;
        grid = new char[height, width];
        score = new int[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                score[y, x] = int.MaxValue;
                if (lines[y][x] == startChar)
                {
                    startPos = new Point(y, x);
                    grid[y, x] = floorChar;
                }
                else if (lines[y][x] == endChar)
                {
                    endPos = new Point(y, x);
                    grid[y, x] = floorChar;
                }
                else
                {
                    grid[y, x] = lines[y][x];
                }
            }
        }
        // facing east at start
        score[startPos.Y, startPos.X] = 0;
        CheckPath(startPos.Y - 1, startPos.X, 0, 1001); // left 90-degree turn north
        CheckPath(startPos.Y, startPos.X + 1, 1, 1); // one step forward
        CheckPath(startPos.Y + 1, startPos.X, 2, 1001); // right 90-degree turn south
        if (showPath)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[y, x] == wallChar)
                    {
                        Console.Write("      #");
                    }
                    else if (score[y, x] == int.MaxValue)
                    {
                        Console.Write("      .");
                    }
                    else
                    {
                        Console.Write($"{score[y, x],7}");
                    }
                }
                Console.WriteLine();
            }
        }
        // get answer
        answer = score[endPos.Y, endPos.X];
        Console.WriteLine($"Day 16 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        height = lines.Length;
        width = lines[0].Length;
        grid = new char[height, width];
        score = new int[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                score[y, x] = int.MaxValue;
                if (lines[y][x] == startChar)
                {
                    startPos = new Point(y, x);
                    grid[y, x] = floorChar;
                }
                else if (lines[y][x] == endChar)
                {
                    endPos = new Point(y, x);
                    grid[y, x] = floorChar;
                }
                else
                {
                    grid[y, x] = lines[y][x];
                }
            }
        }
        // facing east at start
        score[startPos.Y, startPos.X] = 0;

        CheckPath(startPos.Y - 1, startPos.X, 0, 1001); // left 90-degree turn north
        CheckPath(startPos.Y, startPos.X + 1, 1, 1); // one step forward
        CheckPath(startPos.Y + 1, startPos.X, 2, 1001); // right 90-degree turn south

        bestScore = score[endPos.Y, endPos.X];

        CheckPath2(startPos.Y - 1, startPos.X, 0, 1001); // left 90-degree turn north
        CheckPath2(startPos.Y, startPos.X + 1, 1, 1); // one step forward
        CheckPath2(startPos.Y + 1, startPos.X, 2, 1001); // right 90-degree turn south



        Console.WriteLine($"Day 16 Puzzle 2 Answer = {answer}");
    }

    private static void CheckPath(int y, int x, int facing, int scoreToHere)
    {
        if (grid[y, x] == wallChar)
        {
            return;
        }
        if (score[y, x] <= scoreToHere)
        {
            return;
        }
        score[y, x] = scoreToHere;
        if (endPos.Y == y && endPos.X == x)
        {
            return; // at end, don't search any more
        }
        CheckPath(y + NextMoveY[(facing + 3) % 4],
                  x + NextMoveX[(facing + 3) % 4],
                  (facing + 3) % 4,
                  scoreToHere + 1001);
        CheckPath(y + NextMoveY[facing],
                  x + NextMoveX[facing],
                  facing,
                  scoreToHere + 1);
        CheckPath(y + NextMoveY[(facing + 1) % 4],
                  x + NextMoveX[(facing + 1) % 4],
                  (facing + 1) % 4,
                  scoreToHere + 1001);
    }

    private static bool CheckPath2(int y, int x, int facing, int scoreToHere)
    {
        if (grid[y, x] == wallChar)
        {
            return false;
        }
        var herePoint = new Point(y, x);
        if (endPos.Y == y && endPos.X == x)
        {
            if (scoreToHere == bestScore)
            {
                if (!bestSeats.Contains(herePoint))
                {
                    bestSeats.Add(herePoint);
                }
                return true;
            }
            return false;
        }
        var result = false;
        if (CheckPath2(
                y + NextMoveY[(facing + 3) % 4],
                x + NextMoveX[(facing + 3) % 4],
                (facing + 3) % 4,
                scoreToHere + 1001))
        {
            if (!bestSeats.Contains(herePoint))
            {
                bestSeats.Add(herePoint);
            }
            result = true;
        }
        if (CheckPath2(
                y + NextMoveY[facing],
                x + NextMoveX[facing],
                facing,
                scoreToHere + 1))
        {
            if (!bestSeats.Contains(herePoint))
            {
                bestSeats.Add(herePoint);
            }
            result = true;
        }
        if (CheckPath2(
                y + NextMoveY[(facing + 1) % 4],
                x + NextMoveX[(facing + 1) % 4],
                (facing + 1) % 4,
                scoreToHere + 1001))
        {
            if (!bestSeats.Contains(herePoint))
            {
                bestSeats.Add(herePoint);
            }
            result = true;
        }
        return result;
    }
}
