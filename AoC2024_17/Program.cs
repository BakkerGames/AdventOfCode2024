namespace AoC2024_17;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_17.txt";

    private static long A = 0;
    private static long B = 0;
    private static long C = 0;
    private static List<int> prog = [];
    private static int ip;

    static void Main()
    {
        //Puzzle1();
        //Console.WriteLine();
        Puzzle2();
        Console.WriteLine();
        Console.Write("Press enter to continue...");
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        List<long> output = [];
        var lines = File.ReadAllLines(inputFile);
        var Expected = "";
        foreach (string s in lines)
        {
            if (s.StartsWith("Register A:"))
            {
                A = long.Parse(s[11..].Trim());
                continue;
            }
            if (s.StartsWith("Register B:"))
            {
                B = long.Parse(s[11..].Trim());
                continue;
            }
            if (s.StartsWith("Register C:"))
            {
                C = long.Parse(s[11..].Trim());
                continue;
            }
            if (s.StartsWith("Expected:"))
            {
                Expected = s;
                continue;
            }
            if (s.StartsWith("Program:"))
            {
                prog = s[8..].Trim().Split(',').Select(x => int.Parse(x)).ToList();
                Console.WriteLine($"Initial A={A}");
                Console.WriteLine($"Initial B={B}");
                Console.WriteLine($"Initial C={C}");
                Console.WriteLine(s);
                output.Clear();
                ip = 0;
                while (ip < prog.Count)
                {
                    int instruction = prog[ip];
                    int literal = prog[ip + 1];
                    ip += 2;
                    PerformInstruction(instruction, literal, ref ip, output);
                }
                Console.WriteLine($"Result A={A}");
                Console.WriteLine($"Result B={B}");
                Console.WriteLine($"Result C={C}");
                Console.WriteLine(Expected);
                Console.WriteLine($"Day 17 Puzzle 1 Answer = {string.Join(',', output)}");
                Console.WriteLine();
            }
        }
    }

    static void Puzzle2()
    {
        List<long> output = [];
        var lines = File.ReadAllLines(inputFile);
        var Expected = "";
        foreach (string s in lines)
        {
            if (s.StartsWith("Register A:"))
            {
                A = long.Parse(s[11..].Trim());
                continue;
            }
            if (s.StartsWith("Register B:"))
            {
                B = long.Parse(s[11..].Trim());
                continue;
            }
            if (s.StartsWith("Register C:"))
            {
                C = long.Parse(s[11..].Trim());
                continue;
            }
            if (s.StartsWith("Expected:"))
            {
                Expected = s;
                continue;
            }
            if (s.StartsWith("Program:"))
            {
                Console.WriteLine($"Initial A={Convert.ToString(A, 8)}");
                Console.WriteLine($"Initial B={Convert.ToString(B, 8)}");
                Console.WriteLine($"Initial C={Convert.ToString(C, 8)}");
                Console.WriteLine();
                Console.WriteLine(s);
                Console.WriteLine();
                prog = s[8..].Trim().Split(',').Select(x => int.Parse(x)).ToList();
                Decompile(prog);
                Console.WriteLine();
                while (ip < prog.Count)
                {
                    int instruction = prog[ip];
                    int literal = prog[ip + 1];
                    ip += 2;
                    PerformInstruction(instruction, literal, ref ip, output, true);
                }
                Console.WriteLine();
                Console.WriteLine($"Result A={Convert.ToString(A, 8)}");
                Console.WriteLine($"Result B={Convert.ToString(B, 8)}");
                Console.WriteLine($"Result C={Convert.ToString(C, 8)}");
                Console.WriteLine();
                Console.WriteLine(s);
                Console.WriteLine($"Day 17 Puzzle 2 Output = {string.Join(',', output)}");
            }
        }
    }

    private static long GetCombo(long value)
    {
        return value switch
        {
            0 or 1 or 2 or 3 => value,
            4 => A,
            5 => B,
            6 => C,
            _ => throw new SystemException($"Invalid operand {value}"),
        };
    }

    private static void Decompile(List<int> prog)
    {
        int ip = 0;
        while (ip < prog.Count)
        {
            int instruction = prog[ip];
            int literal = prog[ip + 1];
            string literalValue = literal switch
            {
                0 or 1 or 2 or 3 => literal.ToString(),
                4 => "A",
                5 => "B",
                6 => "C",
                _ => "ERR",
            };
            switch (instruction)
            {
                case 0: // adv
                    Console.WriteLine($"adv {literal} - A <- A / 2^{literalValue}");
                    break;
                case 1: // bxl
                    Console.WriteLine($"bxl {literal} - B <- B xor {literal}");
                    B ^= literal;
                    break;
                case 2: // bst
                    Console.WriteLine($"bst {literal} - B <- {literalValue} % 8");
                    break;
                case 3: // jnz
                    Console.WriteLine($"jnz {literal}");
                    break;
                case 4: // bxc
                    Console.WriteLine($"bxc {literal} - B <- B xor C");
                    break;
                case 5: // out
                    Console.WriteLine($"out {literal} - {literalValue} % 8");
                    break;
                case 6: // bdv
                    Console.WriteLine($"bdv {literal} - B <- A / 2^{literalValue}");
                    break;
                case 7: // cdv
                    Console.WriteLine($"cdv {literal} - C <- A / 2^{literalValue}");
                    break;
            }
            ip += 2;
        }
    }

    private static void PerformInstruction(int instruction, int literal, ref int ip, List<long> output, bool debug = false)
    {
        long combo;
        string literalValue = literal switch
        {
            0 or 1 or 2 or 3 => literal.ToString(),
            4 => "A",
            5 => "B",
            6 => "C",
            _ => "ERR",
        };
        switch (instruction)
        {
            case 0: // adv
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"adv {literal} - A <= A({Convert.ToString(A, 8)}) / 2 ^ {literalValue}({combo}) = {Convert.ToString((long)(A / Math.Pow(2, combo)), 8)}");
                }
                A = (long)(A / Math.Pow(2, combo));
                break;
            case 1: // bxl
                if (debug)
                {
                    Console.WriteLine($"bxl {literal} - B <= B({Convert.ToString(B, 8)}) xor {Convert.ToString(literal, 8)} = {Convert.ToString(B ^ literal, 8)}");
                }
                B ^= literal;
                break;
            case 2: // bst
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"bst {literal} - B <= {literalValue}({Convert.ToString(combo, 8)}) % 8 = {combo % 8}");
                }
                B = combo % 8;
                break;
            case 3: // jnz
                if (debug)
                {
                    Console.WriteLine($"jnz {literal} - A: {Convert.ToString(A, 8)} jump: {A != 0}");
                }
                if (A != 0)
                {
                    ip = literal;
                }
                break;
            case 4: // bxc
                if (debug)
                {
                    Console.WriteLine($"bxc {literal} - B <= B({Convert.ToString(B, 8)}) xor C({Convert.ToString(C, 8)}) = {Convert.ToString(B ^ C, 8)}");
                }
                B ^= C;
                break;
            case 5: // out
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"out {literal} - {literalValue}({Convert.ToString(combo, 8)}) % 8 = {combo % 8}");
                    Console.WriteLine($"{combo % 8}");
                }
                output.Add(combo % 8);
                break;
            case 6: // bdv
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"bdv {literal} - B <= A({Convert.ToString(A, 8)}) / 2 ^ {literalValue}({combo}) = {Convert.ToString((long)(A / Math.Pow(2, combo)), 8)}");
                }
                B = (long)(A / Math.Pow(2, combo));
                break;
            case 7: // cdv
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"cdv {literal} - C <= A({Convert.ToString(A, 8)}) / 2 ^ {literalValue}({combo}) = {Convert.ToString((long)(A / Math.Pow(2, combo)), 8)}");
                }
                C = (long)(A / Math.Pow(2, combo));
                break;
        }
    }
}
