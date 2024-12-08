namespace AoC2024_07;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_07.txt";

    static void Main()
    {
        Puzzle1();
        Console.WriteLine();
        Puzzle2();
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        List<string> operators = ["+", "*"];
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            var tokens = s.Split(' ', StringSplitOptions.TrimEntries);
            if (long.TryParse(tokens[0][..^1], out long testValue))
            {
                List<long> values = tokens[1..].Select(x => long.Parse(x)).ToList();
                var answers = GetAnswers(values, values.Count - 1, operators);
                if (answers.Contains(testValue))
                {
                    answer += testValue;
                }
            }
        }
        Console.WriteLine($"Day 07 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        List<string> operators = ["+", "*", "||"];
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            var tokens = s.Split(' ', StringSplitOptions.TrimEntries);
            if (long.TryParse(tokens[0][..^1], out long testValue))
            {
                List<long> values = tokens[1..].Select(x => long.Parse(x)).ToList();
                var answers = GetAnswers(values, values.Count - 1, operators);
                if (answers.Contains(testValue))
                {
                    answer += testValue;
                }
            }
        }
        Console.WriteLine($"Day 07 Puzzle 2 Answer = {answer}");
    }

    static List<long> GetAnswers(List<long> values, int pos, List<string> operators)
    {
        if (pos == 0)
        {
            return [values[pos]];
        }
        var answers = GetAnswers(values, pos - 1, operators);
        List<long> result = [];
        foreach (long a in answers)
        {
            foreach (string op in operators)
            {
                switch (op)
                {
                    case "+":
                        result.Add(a + values[pos]);
                        break;
                    case "*":
                        result.Add(a * values[pos]);
                        break;
                    case "||":
                        result.Add(long.Parse(a.ToString() + values[pos].ToString()));
                        break;
                }
            }
        }
        return result;
    }
}
