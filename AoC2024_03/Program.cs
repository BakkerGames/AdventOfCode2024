namespace AoC2024_03;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_03_Test.txt";

    static void Main(string[] args)
    {
        Puzzle1();
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
    }
}
