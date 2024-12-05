namespace AoC2024_05;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_05.txt";

    static void Main()
    {
        Puzzle1();
        Puzzle2();
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        var lines = File.ReadAllLines(inputFile);
        int answer = 0;
        bool inRules = true;
        List<string> rules = [];
        List<string> updates = [];
        int middle = 0;
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                inRules = false;
                continue;
            }
            if (inRules)
            {
                rules.Add(s);
            }
            else
            {
                updates.Add(s);
            }
        }
        foreach (string pages in updates)
        {
            var invalid = false;
            var pageList = pages
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.TryParse(x, out int n) ? n : 0).ToList();
            foreach (string rule in rules)
            {
                var ruleList = rule
                    .Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.TryParse(x, out int n) ? n : 0).ToList();
                if (!pageList.Contains(ruleList[0]) || !pageList.Contains(ruleList[1])) continue;
                if (pageList.FindIndex(x => x == ruleList[0]) > pageList.FindIndex(x => x == ruleList[1]))
                {
                    invalid = true;
                    break;
                }
            }
            if (invalid) continue;
            middle = pageList[pageList.Count / 2];
            answer +=  middle;
        }
        Console.WriteLine($"Day 05 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        var lines = File.ReadAllLines(inputFile);
        int answer = 0;
        bool inRules = true;
        List<string> rules = [];
        List<string> updates = [];
        int middle = 0;
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                inRules = false;
                continue;
            }
            if (inRules)
            {
                rules.Add(s);
            }
            else
            {
                updates.Add(s);
            }
        }
        foreach (string pages in updates)
        {
            var invalid = false;
            var pageList = pages
                .Split(',', StringSplitOptions.RemoveEmptyEntries)
                .Select(x => int.TryParse(x, out int n) ? n : 0).ToList();
            foreach (string rule in rules)
            {
                var ruleList = rule
                    .Split('|', StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => int.TryParse(x, out int n) ? n : 0).ToList();
                if (!pageList.Contains(ruleList[0]) || !pageList.Contains(ruleList[1])) continue;
                if (pageList.FindIndex(x => x == ruleList[0]) > pageList.FindIndex(x => x == ruleList[1]))
                {
                    invalid = true;
                    break;
                }
            }
            if (!invalid) continue;
            bool changed = true;
            while (changed)
            {
                changed = false;
                foreach (string rule in rules)
                {
                    var ruleList = rule
                        .Split('|', StringSplitOptions.RemoveEmptyEntries)
                        .Select(x => int.TryParse(x, out int n) ? n : 0).ToList();
                    if (!pageList.Contains(ruleList[0]) || !pageList.Contains(ruleList[1])) continue;
                    int pos0 = pageList.FindIndex(x => x == ruleList[0]);
                    int pos1 = pageList.FindIndex(x => x == ruleList[1]);
                    if (pos0 > pos1)
                    {
                        (pageList[pos1], pageList[pos0]) = (pageList[pos0], pageList[pos1]);
                        changed = true;
                        continue;
                    }
                }
            }
            middle = pageList[pageList.Count / 2];
            answer += middle;
        }
        Console.WriteLine($"Day 05 Puzzle 2 Answer = {answer}");
    }
}
