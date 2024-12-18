namespace AoC2024_15;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_15_Test.txt";
    private const char robotChar = '@';
    private const char boxChar = 'O';
    private const char boxChar1 = '[';
    private const char boxChar2 = ']';
    private const char wallChar = '#';
    private const char floorChar = '.';

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
        var height = 0;
        var width = lines[0].Length;
        while (lines[height] != "")
        {
            height++;
        }
        var grid = new char[height, width];
        Robot robot = new();
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
                if (grid[robot.Y + moveY, robot.X + moveX] == wallChar)
                {
                    continue; // hit wall, don't move
                }
                if (grid[robot.Y + moveY, robot.X + moveX] == floorChar)
                {
                    grid[robot.Y, robot.X] = floorChar;
                    robot.Y += moveY;
                    robot.X += moveX;
                    grid[robot.Y, robot.X] = robotChar;
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
                    continue;
                }
                // move box to end (don't have to move whole line!)
                grid[testY, testX] = boxChar;
                grid[robot.Y, robot.X] = floorChar;
                robot.Y += moveY;
                robot.X += moveX;
                grid[robot.Y, robot.X] = robotChar;
            }
        }
        // show result
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (grid[y, x] == boxChar)
                {
                    answer += (y * 100) + x;
                }
                Console.Write(grid[y, x]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine($"Day 15 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        var height = 0;
        var width = lines[0].Length;
        while (lines[height] != "")
        {
            height++;
        }
        var grid = new char[height, width * 2];
        Robot robot = new();
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
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
        //foreach (string s in lines[height..])
        //{
        //    foreach (char c in s)
        //    {
        //        int moveY = 0;
        //        int moveX = 0;
        //        switch (c)
        //        {
        //            case '^':
        //                moveY = -1;
        //                break;
        //            case '>':
        //                moveX = 1;
        //                break;
        //            case 'v':
        //                moveY = 1;
        //                break;
        //            case '<':
        //                moveX = -1;
        //                break;
        //        }
        //        if (moveY == 0 && moveX == 0) continue;
        //        if (grid[robot.Y + moveY, robot.X + moveX] == wallChar)
        //        {
        //            continue; // hit wall, don't move
        //        }
        //        if (grid[robot.Y + moveY, robot.X + moveX] == floorChar)
        //        {
        //            grid[robot.Y, robot.X] = floorChar;
        //            robot.Y += moveY;
        //            robot.X += moveX;
        //            grid[robot.Y, robot.X] = robotChar;
        //            continue;
        //        }
        //        // hit a box
        //        int testY = robot.Y + moveY;
        //        int testX = robot.X + moveX;
        //        while (grid[testY, testX] == boxChar)
        //        {
        //            testY += moveY;
        //            testX += moveX;
        //        }
        //        if (grid[testY, testX] == wallChar)
        //        {
        //            // hit a wall, can't move boxes
        //            continue;
        //        }
        //        // move box to end (don't have to move whole line!)
        //        grid[testY, testX] = boxChar;
        //        grid[robot.Y, robot.X] = floorChar;
        //        robot.Y += moveY;
        //        robot.X += moveX;
        //        grid[robot.Y, robot.X] = robotChar;
        //    }
        //}
        // show result
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width * 2; x++)
            {
                Console.Write(grid[y, x]);
            }
            Console.WriteLine();
        }
        Console.WriteLine();
        Console.WriteLine($"Day 15 Puzzle 2 Answer = {answer}");
    }
}
