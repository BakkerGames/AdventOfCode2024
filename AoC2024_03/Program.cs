namespace AoC2024_03;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_03.txt";

    static void Main(string[] args)
    {
        //Puzzle1();
        Puzzle2();
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        var data = File.ReadAllText(inputFile);
        int state = 0;
        int number1 = 0;
        int number2 = 0;
        int len1 = 0;
        int len2 = 0;
        int result = 0;
        foreach (char c in data)
        {
            switch (state)
            {
                case 0:
                    number1 = 0;
                    number2 = 0;
                    len1 = 0;
                    len2 = 0;
                    if (c == 'm') state++; else state = 0;
                    break;
                case 1:
                    if (c == 'u') state++; else state = 0;
                    break;
                case 2:
                    if (c == 'l') state++; else state = 0;
                    break;
                case 3:
                    if (c == '(') state++; else state = 0;
                    number1 = 0;
                    number2 = 0;
                    len1 = 0;
                    len2 = 0;
                    break;
                case 4:
                    if (c == ',' && len1 >= 1 && len1 <= 3)
                    {
                        state++;
                    }
                    else if (c >= '0' && c <= '9' && len1 < 3)
                    {
                        number1 = (number1 * 10) + c - '0';
                        len1++;
                    }
                    else
                    {
                        state = 0;
                    }
                    break;
                case 5:
                    if (c == ')' && len2 >= 1 && len2 <= 3)
                    {
                        result += number1 * number2;
                        state = 0;
                    }
                    else if (c >= '0' && c <= '9' && len2 < 3)
                    {
                        number2 = (number2 * 10) + c - '0';
                        len2++;
                    }
                    else
                    {
                        state = 0;
                    }
                    break;
                default:
                    state = 0;
                    break;
            }
        }
        Console.WriteLine($"Day 03 Puzzle 1 Answer = {result}");
    }

    static void Puzzle2()
    {
        var data = File.ReadAllText(inputFile);
        int state = 0;
        int number1 = 0;
        int number2 = 0;
        int len1 = 0;
        int len2 = 0;
        int result = 0;
        bool enabled = true;
        foreach (char c in data)
        {
            switch (state)
            {
                case 0:
                    number1 = 0;
                    number2 = 0;
                    len1 = 0;
                    len2 = 0;
                    if (c == 'm')
                    {
                        state = 1; // start of mul(#,#)
                    }
                    else if (c == 'd')
                    {
                        state = 10; // start of do() or don't()
                    }
                    else
                    {
                        state = 0;
                    }
                    break;
                case 1:
                    if (c == 'u') state++; else state = 0;
                    break;
                case 2:
                    if (c == 'l') state++; else state = 0;
                    break;
                case 3:
                    if (c == '(') state++; else state = 0;
                    number1 = 0;
                    number2 = 0;
                    len1 = 0;
                    len2 = 0;
                    break;
                case 4:
                    if (c == ',' && len1 >= 1 && len1 <= 3)
                    {
                        state++;
                    }
                    else if (c >= '0' && c <= '9' && len1 < 3)
                    {
                        number1 = (number1 * 10) + c - '0';
                        len1++;
                    }
                    else
                    {
                        state = 0;
                    }
                    break;
                case 5:
                    if (c == ')' && len2 >= 1 && len2 <= 3)
                    {
                        if (enabled)
                        {
                            result += number1 * number2;
                        }
                        state = 0;
                    }
                    else if (c >= '0' && c <= '9' && len2 < 3)
                    {
                        number2 = (number2 * 10) + c - '0';
                        len2++;
                    }
                    else
                    {
                        state = 0;
                    }
                    break;
                case 10:
                    if (c == 'o') state++; else state = 0;
                    break;
                case 11:
                    if (c == '(')
                    {
                        state++;
                    }
                    else if (c == 'n')
                    {
                        state = 20; // middle of don't()
                    }
                    else
                    {
                        state = 0;
                    }
                    break;
                case 12:
                    if (c == ')')
                    {
                        enabled = true;
                    }
                    state = 0;
                    break;
                case 20:
                    if (c == '\'') state++; else state = 0;
                    break;
                case 21:
                    if (c == 't') state++; else state = 0;
                    break;
                case 22:
                    if (c == '(') state++; else state = 0;
                    break;
                case 23:
                    if (c == ')')
                    {
                        enabled = false;
                    }
                    state = 0;
                    break;
                default:
                    state = 0;
                    break;
            }
        }
        Console.WriteLine($"Day 03 Puzzle 2 Answer = {result}");
    }
}
