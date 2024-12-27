

namespace AoC2024_16;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_16_Test_1.txt";
    private const char wallChar = '#';
    private const char floorChar = '.';
    private const char startChar = 'S';
    private const char endChar = 'E';
    private const int stepCost = 1;
    private const int turnCost = 1000;

    private const bool showData = true;

    private static int height = 0;
    private static int width = 0;
    private static char[,] grid = new char[0, 0];
    private static int[,] minScore = new int[0, 0];
    private static Point startPos = new();
    private static Point endPos = new();
    private static List<Node>[,] nodes = new List<Node>[0, 0];
    private static int BestAnswer = int.MaxValue;

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
        LoadData();

        var rootNode = new Node(startPos.Y, startPos.X, 1); // east=1
        AddNodes(rootNode);

        if (showData)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (grid[y, x] == wallChar)
                    {
                        Console.Write("      #");
                    }
                    else
                    {
                        Console.Write($"{minScore[y, x],7}");
                    }
                }
                Console.WriteLine();
            }
        }

        // get answer
        answer = BestAnswer;

        Console.WriteLine($"Day 16 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer;
        LoadData();

        var rootNode = new Node(startPos.Y, startPos.X, 1); // east=1
        AddNodes(rootNode);

        bool[,] bestSeat = new bool[height, width];
        foreach (Node n in nodes[endPos.Y, endPos.X])
        {
            if (n.Value == BestAnswer)
            {
                var temp = n;
                while (temp != null)
                {
                    bestSeat[temp.Pos.Y, temp.Pos.X] = true;
                    temp = temp.Previous;
                }
            }
        }
        if (showData)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (bestSeat[y, x])
                    {
                        Console.Write('O');
                    }
                    else
                    {
                        Console.Write(grid[y, x]);
                    }
                }
                Console.WriteLine();
            }
        }
        answer = 0;
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (bestSeat[y, x]) answer++;
            }
        }
        Console.WriteLine($"Day 16 Puzzle 2 Answer = {answer}");
    }

    private static void AddNodes(Node n)
    {
        if (n.Value > BestAnswer)
        {
            return;
        }

        if (n.Value > minScore[n.Pos.Y, n.Pos.X])
        {
            return;
        }

        nodes[n.Pos.Y, n.Pos.X].Add(n);
        if (minScore[n.Pos.Y, n.Pos.X] > n.Value)
        {
            minScore[n.Pos.Y, n.Pos.X] = n.Value;
        }

        if (n.Pos.Y == endPos.Y && n.Pos.X == endPos.X)
        {
            if (BestAnswer > n.Value)
            {
                BestAnswer = n.Value;
            }
            return;
        }

        int yForward = n.Pos.Y + NextMoveY[n.Pos.Direction];
        int xForward = n.Pos.X + NextMoveX[n.Pos.Direction];
        if (grid[yForward, xForward] != wallChar && !InPath(yForward, xForward, n))
        {
            Node nodeForward = new(yForward, xForward, n.Pos.Direction)
            {
                Value = n.Value + stepCost,
                Previous = n
            };
            //n.Next.Add(nodeForward);
            AddNodes(nodeForward);
        }

        int dirLeft = (n.Pos.Direction + 3) % 4;
        int yLeft = n.Pos.Y + NextMoveY[dirLeft];
        int xLeft = n.Pos.X + NextMoveX[dirLeft];
        if (grid[yLeft, xLeft] != wallChar && !InPath(yLeft, xLeft, n))
        {
            Node nodeTurnLeft = new(n.Pos.Y, n.Pos.X, dirLeft, n.Value + turnCost, n);
            nodes[n.Pos.Y, n.Pos.X].Add(nodeTurnLeft);
            Node nodeLeft = new(yLeft, xLeft, dirLeft, nodeTurnLeft.Value + stepCost, nodeTurnLeft);
            //n.Next.Add(nodeTurnLeft);
            //nodeTurnLeft.Next.Add(nodeLeft);
            AddNodes(nodeLeft);
        }

        int dirRight = (n.Pos.Direction + 1) % 4;
        int yRight = n.Pos.Y + NextMoveY[dirRight];
        int xRight = n.Pos.X + NextMoveX[dirRight];
        if (grid[yRight, xRight] != wallChar && !InPath(yRight, xRight, n))
        {
            Node nodeTurnRight = new(n.Pos.Y, n.Pos.X, dirRight, n.Value + turnCost, n);
            nodes[n.Pos.Y, n.Pos.X].Add(nodeTurnRight);
            Node nodeRight = new(yRight, xRight, dirRight, nodeTurnRight.Value + stepCost, nodeTurnRight);
            //n.Next.Add(nodeTurnRight);
            //nodeTurnRight.Next.Add(nodeRight);
            AddNodes(nodeRight);
        }
    }

    private static bool InPath(int y, int x, Node n)
    {
        var temp = n;
        while (temp.Previous != null)
        {
            if (temp.Pos.Y == y && temp.Pos.X == x)
            {
                return true;
            }
            temp = temp.Previous;
        }
        return false;
    }

    private static void LoadData()
    {
        var lines = File.ReadAllLines(inputFile);
        height = lines.Length;
        width = lines[0].Length;
        grid = new char[height, width];
        minScore = new int[height, width];
        nodes = new List<Node>[height, width];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                minScore[y, x] = int.MaxValue;
                nodes[y, x] = [];
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
    }

    //private static void CheckPath(int y, int x, int facing, int scoreToHere)
    //{
    //    if (grid[y, x] == wallChar)
    //    {
    //        return;
    //    }
    //    if (score[y, x] <= scoreToHere)
    //    {
    //        return;
    //    }
    //    score[y, x] = scoreToHere;
    //    if (endPos.Y == y && endPos.X == x)
    //    {
    //        return; // at end, don't search any more
    //    }
    //    CheckPath(y + NextMoveY[(facing + 3) % 4],
    //              x + NextMoveX[(facing + 3) % 4],
    //              (facing + 3) % 4,
    //              scoreToHere + 1001);
    //    CheckPath(y + NextMoveY[facing],
    //              x + NextMoveX[facing],
    //              facing,
    //              scoreToHere + 1);
    //    CheckPath(y + NextMoveY[(facing + 1) % 4],
    //              x + NextMoveX[(facing + 1) % 4],
    //              (facing + 1) % 4,
    //              scoreToHere + 1001);
    //}

    //private static bool CheckPath2(int y, int x, int facing, int scoreToHere)
    //{
    //    if (grid[y, x] == wallChar)
    //    {
    //        return false;
    //    }
    //    var herePoint = new Point(y, x);
    //    if (endPos.Y == y && endPos.X == x)
    //    {
    //        if (scoreToHere == bestScore)
    //        {
    //            if (!bestSeats.Contains(herePoint))
    //            {
    //                bestSeats.Add(herePoint);
    //            }
    //            return true;
    //        }
    //        return false;
    //    }
    //    var result = false;
    //    if (CheckPath2(
    //            y + NextMoveY[(facing + 3) % 4],
    //            x + NextMoveX[(facing + 3) % 4],
    //            (facing + 3) % 4,
    //            scoreToHere + 1001))
    //    {
    //        if (!bestSeats.Contains(herePoint))
    //        {
    //            bestSeats.Add(herePoint);
    //        }
    //        result = true;
    //    }
    //    if (CheckPath2(
    //            y + NextMoveY[facing],
    //            x + NextMoveX[facing],
    //            facing,
    //            scoreToHere + 1))
    //    {
    //        if (!bestSeats.Contains(herePoint))
    //        {
    //            bestSeats.Add(herePoint);
    //        }
    //        result = true;
    //    }
    //    if (CheckPath2(
    //            y + NextMoveY[(facing + 1) % 4],
    //            x + NextMoveX[(facing + 1) % 4],
    //            (facing + 1) % 4,
    //            scoreToHere + 1001))
    //    {
    //        if (!bestSeats.Contains(herePoint))
    //        {
    //            bestSeats.Add(herePoint);
    //        }
    //        result = true;
    //    }
    //    return result;
    //}
}
