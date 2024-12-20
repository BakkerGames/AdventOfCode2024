namespace AoC2024_15;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_15.txt";
    private const char robotChar = '@';
    private const char boxChar = 'O';
    private const char boxChar1 = '[';
    private const char boxChar2 = ']';
    private const char wallChar = '#';
    private const char floorChar = '.';

    private static int height = 0;
    private static int width = 0;
    private static char[,] grid = new char[0, 0];

    private static readonly bool showSteps = false;

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
        height = 0;
        width = lines[0].Length;
        while (lines[height] != "")
        {
            height++;
        }
        grid = new char[height, width];
        Point robot = new();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                switch (lines[y][x])
                {
                    case wallChar:
                        grid[y, x] = wallChar;
                        break;
                    case robotChar:
                        robot.Y = y;
                        robot.X = x;
                        grid[y, x] = robotChar;
                        break;
                    case boxChar:
                        grid[y, x] = boxChar;
                        break;
                    default:
                        grid[y, x] = floorChar;
                        break;
                }
            }
        }
        if (showSteps)
        {
            ShowGrid();
        }
        // move robot and boxes
        foreach (string s in lines[height..])
        {
            foreach (char c in s)
            {
                int moveY = 0;
                int moveX = 0;
                switch (c)
                {
                    case '^':
                        moveY = -1;
                        break;
                    case '>':
                        moveX = 1;
                        break;
                    case 'v':
                        moveY = 1;
                        break;
                    case '<':
                        moveX = -1;
                        break;
                }
                if (moveY == 0 && moveX == 0) continue;
                if (showSteps)
                {
                    Console.WriteLine($"Move {c}:");
                }
                if (grid[robot.Y + moveY, robot.X + moveX] == wallChar)
                {
                    if (showSteps)
                    {
                        Console.WriteLine("Robot hit wall");
                        Console.WriteLine();
                    }
                    continue; // hit wall, don't move
                }
                if (grid[robot.Y + moveY, robot.X + moveX] == floorChar)
                {
                    grid[robot.Y, robot.X] = floorChar;
                    robot.Y += moveY;
                    robot.X += moveX;
                    grid[robot.Y, robot.X] = robotChar;
                    if (showSteps)
                    {
                        ShowGrid();
                    }
                    continue;
                }
                // hit a box
                int testY = robot.Y + moveY;
                int testX = robot.X + moveX;
                while (grid[testY, testX] == boxChar)
                {
                    testY += moveY;
                    testX += moveX;
                }
                if (grid[testY, testX] == wallChar)
                {
                    // hit a wall, can't move boxes
                    if (showSteps)
                    {
                        Console.WriteLine("Boxes hit wall");
                        Console.WriteLine();
                    }
                    continue;
                }
                // move box to end (don't have to move whole line!)
                grid[testY, testX] = boxChar;
                grid[robot.Y, robot.X] = floorChar;
                robot.Y += moveY;
                robot.X += moveX;
                grid[robot.Y, robot.X] = robotChar;
                if (showSteps)
                {
                    ShowGrid();
                }
            }
        }
        // get answer
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[y, x] == boxChar)
                {
                    answer += (y * 100) + x;
                }
            }
        }
        Console.WriteLine($"Day 15 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        height = 0;
        width = lines[0].Length;
        while (lines[height] != "")
        {
            height++;
        }
        width *= 2;
        grid = new char[height, width];
        Point robot = new();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width / 2; x++)
            {
                switch (lines[y][x])
                {
                    case wallChar:
                        grid[y, x * 2] = wallChar;
                        grid[y, (x * 2) + 1] = wallChar;
                        break;
                    case robotChar:
                        robot.Y = y;
                        robot.X = x * 2;
                        grid[y, x * 2] = robotChar;
                        grid[y, (x * 2) + 1] = floorChar;
                        break;
                    case boxChar:
                        grid[y, x * 2] = boxChar1;
                        grid[y, (x * 2) + 1] = boxChar2;
                        break;
                    default:
                        grid[y, x * 2] = floorChar;
                        grid[y, (x * 2) + 1] = floorChar;
                        break;
                }
            }
        }
        //Console.WriteLine("Initial state:");
        //ShowGrid();
        // move robot and boxes
        foreach (string s in lines[height..])
        {
            foreach (char c in s)
            {
                int moveY = 0;
                int moveX = 0;
                switch (c)
                {
                    case '^':
                        moveY = -1;
                        break;
                    case '>':
                        moveX = 1;
                        break;
                    case 'v':
                        moveY = 1;
                        break;
                    case '<':
                        moveX = -1;
                        break;
                }
                if (moveY == 0 && moveX == 0) continue;
                if (showSteps)
                {
                    Console.WriteLine($"Move {c}:");
                }
                if (grid[robot.Y + moveY, robot.X + moveX] == wallChar)
                {
                    if (showSteps)
                    {
                        Console.WriteLine("Robot hit wall");
                        Console.WriteLine();
                    }
                    continue; // hit wall, don't move
                }
                if (grid[robot.Y + moveY, robot.X + moveX] == floorChar)
                {
                    grid[robot.Y, robot.X] = floorChar;
                    robot.Y += moveY;
                    robot.X += moveX;
                    grid[robot.Y, robot.X] = robotChar;
                    if (showSteps)
                    {
                        ShowGrid();
                    }
                    continue;
                }
                // hit a box
                int testY = robot.Y + moveY;
                int testX = robot.X + moveX;
                if (moveY == 0) // horizontal
                {
                    while (grid[testY, testX] == boxChar1 || grid[testY, testX] == boxChar2)
                    {
                        testX += moveX;
                    }
                    if (grid[testY, testX] == wallChar)
                    {
                        if (showSteps)
                        {
                            Console.WriteLine("Boxes hit wall");
                            Console.WriteLine();
                        }
                        continue; // hit a wall, can't move boxes
                    }
                    while (testX != robot.X)
                    {
                        grid[testY, testX] = grid[testY, testX - moveX];
                        testX -= moveX;
                    }
                    grid[robot.Y, robot.X] = floorChar;
                    robot.X += moveX;
                    grid[robot.Y, robot.X] = robotChar;
                    if (showSteps)
                    {
                        ShowGrid();
                    }
                }
                else // vertical, have to move boxes as a unit
                {
                    List<Point> boxes = [];
                    List<Point> currBoxes = [];
                    List<Point> newBoxes = [];
                    currBoxes.Add(new Point(testY, testX));
                    if (grid[testY, testX] == boxChar1)
                    {
                        currBoxes.Add(new Point(testY, testX + 1));
                    }
                    else
                    {
                        currBoxes.Add(new Point(testY, testX - 1));
                    }
                    var blocked = false;
                    while (true)
                    {
                        testY += moveY;
                        newBoxes.Clear();
                        blocked = false;
                        foreach (Point p in currBoxes)
                        {
                            if (grid[p.Y + moveY, p.X] == wallChar)
                            {
                                blocked = true;
                                if (showSteps)
                                {
                                    Console.WriteLine("Boxes hit wall");
                                    Console.WriteLine();
                                }
                                break;
                            }
                            if (grid[p.Y + moveY, p.X] == boxChar1)
                            {
                                var p1 = new Point(p.Y + moveY, p.X);
                                var p2 = new Point(p.Y + moveY, p.X + 1);
                                if (!newBoxes.Contains(p1)) newBoxes.Add(p1);
                                if (!newBoxes.Contains(p2)) newBoxes.Add(p2);
                            }
                            else if (grid[p.Y + moveY, p.X] == boxChar2)
                            {
                                var p1 = new Point(p.Y + moveY, p.X - 1);
                                var p2 = new Point(p.Y + moveY, p.X);
                                if (!newBoxes.Contains(p1)) newBoxes.Add(p1);
                                if (!newBoxes.Contains(p2)) newBoxes.Add(p2);
                            }
                        }
                        if (blocked)
                        {
                            break;
                        }
                        boxes.AddRange(currBoxes);
                        currBoxes.Clear();
                        currBoxes.AddRange(newBoxes);
                        if (newBoxes.Count > 0)
                        {
                            // found boxes, keep looking
                            continue;
                        }
                        // only found spaces, can move boxes
                        for (int i = boxes.Count - 1; i >= 0; i--)
                        {
                            Point p = boxes[i];
                            grid[p.Y + moveY, p.X] = grid[p.Y, p.X];
                            grid[p.Y, p.X] = floorChar;
                        }
                        grid[robot.Y, robot.X] = floorChar;
                        robot.Y += moveY;
                        grid[robot.Y, robot.X] = robotChar;
                        if (showSteps)
                        {
                            ShowGrid();
                        }
                        break;
                    }
                }
            }
        }
        // get answer
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[y, x] == boxChar1)
                {
                    answer += (y * 100) + x;
                }
            }
        }
        Console.WriteLine($"Day 15 Puzzle 2 Answer = {answer}");
    }

    private static void ShowGrid()
    {
        // show result
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Console.Write(grid[y, x]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }
}
