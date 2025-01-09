namespace AoC2024_17;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_17_Test_2.txt";

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
                Decompile(prog);
                //return;
                //Console.WriteLine(s);
                //output.Clear();
                //ip = 0;
                //A = answer;
                //bool match = false;
                //do
                //{
                //    while (ip < prog.Count)
                //    {
                //        int instruction = prog[ip];
                //        int literal = prog[ip + 1];
                //        ip += 2;
                //        PerformInstruction(instruction, literal, ref ip, output);
                //    }
                //    match = output.Count == prog.Count;
                //    if (match)
                //    {
                //        for (int i = 0; i < output.Count; i++)
                //        {
                //            if (output[i] != prog[i])
                //            {
                //                match = false;
                //            }
                //        }
                //    }
                //    if (!match)
                //    {
                //        answer++;
                //        A = answer;
                //        B = 0;
                //        C = 0;
                //        ip = 0;
                //        output.Clear();
                //        if (answer % 1000000 == 0)
                //        {
                //            Console.Write('\r');
                //            Console.Write(answer);
                //        }
                //    }
                //} while (!match);
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
                    Console.WriteLine($"adv A<-A/2^{literalValue}");
                    break;
                case 1: // bxl
                    Console.WriteLine($"bxl B^{literal}");
                    B ^= literal;
                    break;
                case 2: // bst
                    Console.WriteLine($"bst B<-{literalValue}");
                    break;
                case 3: // jnz
                    Console.WriteLine($"jnz {literal}");
                    break;
                case 4: // bxc
                    Console.WriteLine("bxc B<-B xor C");
                    break;
                case 5: // out
                    Console.WriteLine($"out {literalValue}");
                    break;
                case 6: // bdv
                    Console.WriteLine($"bdv B<-A/2^{literalValue}");
                    break;
                case 7: // cdv
                    Console.WriteLine($"cdv C<-A/2^{literalValue}");
                    break;
            }
            ip += 2;
        }
    }

    private static void PerformInstruction(int instruction, int literal, ref int ip, List<long> output, bool debug = false)
    {
        long combo;
        switch (instruction)
        {
            case 0: // adv
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"adv {literal} - combo: {combo} A: {A} NewA: {(long)(A / Math.Pow(2, combo))}");
                }
                A = (long)(A / Math.Pow(2, combo));
                break;
            case 1: // bxl
                if (debug)
                {
                    Console.WriteLine($"bxl {literal} - B: {A} NewB: {B ^ literal}");
                }
                B ^= literal;
                break;
            case 2: // bst
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"bst {literal} - combo: {combo} B: {A} NewB: {combo % 8}");
                }
                B = combo % 8;
                break;
            case 3: // jnz
                if (debug)
                {
                    Console.WriteLine($"jnz {literal} - A: {A} jump: {A != 0} ip: {ip} NewIP: {(A != 0 ? literal : ip)}");
                }
                if (A != 0)
                {
                    ip = literal;
                }
                break;
            case 4: // bxc
                if (debug)
                {
                    Console.WriteLine($"bxc {literal} - B: {B} C: {C} NewB: {B ^ C}");
                }
                B ^= C;
                break;
            case 5: // out
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"out {literal} - combo: {combo} output: {combo % 8}");
                }
                output.Add(combo % 8);
                break;
            case 6: // bdv
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"bdv {literal} - combo: {combo} A: {A} B: {B} NewB: {(long)(A / Math.Pow(2, combo))}");
                }
                B = (long)(A / Math.Pow(2, combo));
                break;
            case 7: // cdv
                combo = GetCombo(literal);
                if (debug)
                {
                    Console.WriteLine($"cdv {literal} - combo: {combo} A: {A} C: {C} NewC: {(long)(A / Math.Pow(2, combo))}");
                }
                C = (long)(A / Math.Pow(2, combo));
                break;
        }
    }
}
