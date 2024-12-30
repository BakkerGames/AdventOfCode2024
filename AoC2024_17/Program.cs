﻿namespace AoC2024_17;

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
        Puzzle1();
        Console.WriteLine();
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
        long answer = 0;
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
                Console.WriteLine(s);
                output.Clear();
                ip = 0;
                A = answer;
                bool match = false;
                do
                {
                    while (ip < prog.Count)
                    {
                        int instruction = prog[ip];
                        int literal = prog[ip + 1];
                        ip += 2;
                        PerformInstruction(instruction, literal, ref ip, output);
                    }
                    match = output.Count == prog.Count;
                    if (match)
                    {
                        for (int i = 0; i < output.Count; i++)
                        {
                            if (output[i] != prog[i])
                            {
                                match = false;
                            }
                        }
                    }
                    if (!match)
                    {
                        answer++;
                        A = answer;
                        B = 0;
                        C = 0;
                        ip = 0;
                        output.Clear();
                        if (answer % 1000000 == 0)
                        {
                            Console.Write('\r');
                            Console.Write(answer);
                        }
                    }
                } while (!match);
                Console.WriteLine();
                Console.WriteLine($"Result A={A}");
                Console.WriteLine($"Result B={B}");
                Console.WriteLine($"Result C={C}");
                Console.WriteLine($"Day 17 Puzzle 1 Output = {string.Join(',', output)}");
                Console.WriteLine($"Day 17 Puzzle 2 Answer = {answer}");
                Console.WriteLine();
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

    private static void PerformInstruction(int instruction, int literal, ref int ip, List<long> output)
    {
        long combo;
        switch (instruction)
        {
            case 0: // adv
                combo = GetCombo(literal);
                A = (long)(A / Math.Pow(2, combo));
                break;
            case 1: // bxl
                B ^= literal;
                break;
            case 2: // bst
                combo = GetCombo(literal);
                B = combo % 8;
                break;
            case 3: // jnz
                if (A != 0)
                {
                    ip = literal;
                }
                break;
            case 4: // bxc
                B ^= C;
                break;
            case 5: // out
                combo = GetCombo(literal);
                output.Add(combo % 8);
                break;
            case 6: // bdv
                combo = GetCombo(literal);
                B = (long)(A / Math.Pow(2, combo));
                break;
            case 7: // cdv
                combo = GetCombo(literal);
                C = (long)(A / Math.Pow(2, combo));
                break;
        }
    }
}
