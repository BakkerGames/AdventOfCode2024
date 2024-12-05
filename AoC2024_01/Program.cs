namespace AoC2024_01;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_01.txt";

    static void Main()
    {
        Puzzle1();
        Puzzle2();
        Console.ReadLine();
    }

    static void Puzzle1()
    {
        var lines = File.ReadAllLines(inputFile);
        List<int> list1 = [];
        List<int> list2 = [];
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            var items = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1.Add(int.Parse(items[0]));
            list2.Add(int.Parse(items[1]));
        }
        list1.Sort();
        list2.Sort();
        int distance = 0;
        for (int i = 0; i < list1.Count; i++)
        {
            var d = Math.Abs(list1[i] - list2[i]);
            //Console.WriteLine($"{i,4}: {list1[i]} {list2[i]} {d}");
            distance += d;
        }
        Console.WriteLine($"Day 01 Puzzle 1 Answer = {distance}");
    }

    static void Puzzle2()
    {
        var lines = File.ReadAllLines(inputFile);
        List<int> list1 = [];
        List<int> list2 = [];
        foreach (string s in lines)
        {
            if (string.IsNullOrWhiteSpace(s)) continue;
            var items = s.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            list1.Add(int.Parse(items[0]));
            list2.Add(int.Parse(items[1]));
        }
        list2.Sort();
        Dictionary<int, int> Counts = [];
        foreach (int i in list2)
        {
            if (!Counts.TryAdd(i, 1))
            {
                Counts[i]++;
            }
        }
        var similarity = 0;
        foreach (int i in list1)
        {
            if (Counts.TryGetValue(i, out int value))
            {
                similarity += i * value;
            }
        }
        Console.WriteLine($"Day 01 Puzzle 2 Answer = {similarity}");
    }
}
