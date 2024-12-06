namespace AoC2024_06;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_06.txt";

    static void Main()
    {
        Puzzle1();
        Console.WriteLine();
        Puzzle2();
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        var lines = File.ReadAllLines(inputFile);

        int answer = 0;
        Position guard = new();
        bool[,] visited = new bool[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                visited[y, x] = false;
                char c = lines[y][x];
                if (c == '.' || c == '#') continue;
                guard.Y = y;
                guard.X = x;
                switch (c)
                {
                    case '^':
                        guard.Direction = 0;
                        break;
                    case '>':
                        guard.Direction = 1;
                        break;
                    case 'v':
                        guard.Direction = 2;
                        break;
                    case '<':
                        guard.Direction = 3;
                        break;
                    default:
                        Console.WriteLine($"Unknown character: {c}");
                        break;
                }
            }
        }

        while (guard.Y >= 0 && guard.Y < lines.Length && guard.X >= 0 && guard.X < lines[guard.Y].Length)
        {
            visited[guard.Y, guard.X] = true;
            TakeOneStep(guard, lines);
        }

        for (int y = 0; y <= visited.GetUpperBound(0); y++)
        {
            for (int x = 0; x <= visited.GetUpperBound(1); x++)
            {
                if (visited[y, x])
                {
                    answer++;
                    Console.Write('X');
                }
                else
                {
                    Console.Write(lines[y][x]);
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine($"Day 06 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        var lines = File.ReadAllLines(inputFile);

        int answer = 0;
        Position guard = new();
        bool[,] visited = new bool[lines.Length, lines[0].Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                visited[y, x] = false;
                char c = lines[y][x];
                if (c == '.' || c == '#') continue;
                guard.Y = y;
                guard.X = x;
                switch (c)
                {
                    case '^':
                        guard.Direction = 0;
                        break;
                    case '>':
                        guard.Direction = 1;
                        break;
                    case 'v':
                        guard.Direction = 2;
                        break;
                    case '<':
                        guard.Direction = 3;
                        break;
                    default:
                        Console.WriteLine($"Unknown character: {c}");
                        break;
                }
            }
        }

        // save starting position
        Position guardStart = guard.Clone();

        while (guard.Y >= 0 && guard.Y < lines.Length && guard.X >= 0 && guard.X < lines[guard.Y].Length)
        {
            visited[guard.Y, guard.X] = true;
            TakeOneStep(guard, lines);
        }

        // calculate obstructions

        bool[,] obstruction = new bool[lines.Length, lines[0].Length];

        for (int y = 0; y <= visited.GetUpperBound(0); y++)
        {
            for (int x = 0; x <= visited.GetUpperBound(1); x++)
            {
                obstruction[y, x] = false;
                // only have to check visited locations
                if (!visited[y, x]) continue;

                Console.WriteLine($"{y} - {x}");

                List<Position> positions = [];

                var origLine = lines[y];
                lines[y] = lines[y][..x] + '#' + lines[y][(x + 1)..];
                guard = guardStart.Clone();

                while (guard.Y >= 0 && guard.Y < lines.Length && guard.X >= 0 && guard.X < lines[guard.Y].Length)
                {
                    if (positions.Contains(guard))
                    {
                        obstruction[y, x] = true;
                        break;
                    }
                    positions.Add(new Position() { Y = guard.Y, X = guard.X, Direction = guard.Direction });
                    TakeOneStep(guard, lines);
                }
                lines[y] = origLine;
            }
        }
        
        // show obstructions
        for (int y = 0; y <= obstruction.GetUpperBound(0); y++)
        {
            for (int x = 0; x <= obstruction.GetUpperBound(1); x++)
            {
                if (obstruction[y, x])
                {
                    answer++;
                    Console.Write('O');
                }
                else
                {
                    Console.Write(lines[y][x]);
                }
            }
            Console.WriteLine();
        }
        Console.WriteLine($"Day 06 Puzzle 2 Answer = {answer}");
    }

    static void TakeOneStep(Position guard, string[] lines)
    {
        int newY = guard.Y;
        int newX = guard.X;
        switch (guard.Direction)
        {
            case 0:
                newY--;
                break;
            case 1:
                newX++;
                break;
            case 2:
                newY++;
                break;
            case 3:
                newX--;
                break;
        }
        if (newY >= 0 && newY < lines.Length && newX >= 0 && newX < lines[newY].Length)
        {
            if (lines[newY][newX] == '#')
            {
                guard.Direction = (guard.Direction + 1) % 4;
            }
            else
            {
                guard.Y = newY;
                guard.X = newX;
            }
        }
        else
        {
            guard.Y = newY;
            guard.X = newX;
        }
    }
}
