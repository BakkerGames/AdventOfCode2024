using System.Text;

namespace AoC2024_11;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_11.txt";
    private const string tempPrefix = "E:\\Temp\\";

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
        int blinkCount = 25;
        long answer = 0;
        var data = File.ReadAllText(inputFile);
        var initialStones = data.Split(' ', StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList();
        Dictionary<long, long> values = [];
        foreach (long key in initialStones)
        {
            values.TryAdd(key, 0);
            values[key]++;
        }
        for (int blink = 1; blink <= blinkCount; blink++)
        {
            Dictionary<long, long> newValues = [];
            foreach (long key in values.Keys)
            {
                if (key == 0)
                {
                    newValues.TryAdd(1, 0);
                    newValues[1] += values[key];
                }
                else
                {
                    var s = key.ToString();
                    if (s.Length % 2 == 0)
                    {
                        var newKey1 = long.Parse(s[..(s.Length / 2)]);
                        var newKey2 = long.Parse(s[(s.Length / 2)..]);
                        newValues.TryAdd(newKey1, 0);
                        newValues.TryAdd(newKey2, 0);
                        newValues[newKey1] += values[key];
                        newValues[newKey2] += values[key];
                    }
                    else
                    {
                        var newKey = key * 2024;
                        newValues.TryAdd(newKey, 0);
                        newValues[newKey] += values[key];
                    }
                }
            }
            values = newValues;
        }
        foreach (long key in values.Keys)
        {
            answer += values[key];
        }
        Console.WriteLine($"Day 11 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        int blinkCount = 75;
        long answer = 0;
        var data = File.ReadAllText(inputFile);
        var initialStones = data.Split(' ', StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList();
        Dictionary<long, long> values = [];
        foreach (long key in initialStones)
        {
            values.TryAdd(key, 0);
            values[key]++;
        }
        for (int blink = 1; blink <= blinkCount; blink++)
        {
            Dictionary<long, long> newValues = [];
            foreach (long key in values.Keys)
            {
                if (key == 0)
                {
                    newValues.TryAdd(1, 0);
                    newValues[1] += values[key];
                }
                else
                {
                    var s = key.ToString();
                    if (s.Length % 2 == 0)
                    {
                        var newKey1 = long.Parse(s[..(s.Length / 2)]);
                        var newKey2 = long.Parse(s[(s.Length / 2)..]);
                        newValues.TryAdd(newKey1, 0);
                        newValues.TryAdd(newKey2, 0);
                        newValues[newKey1] += values[key];
                        newValues[newKey2] += values[key];
                    }
                    else
                    {
                        var newKey = key * 2024;
                        newValues.TryAdd(newKey, 0);
                        newValues[newKey] += values[key];
                    }
                }
            }
            values = newValues;
        }
        foreach (long key in values.Keys)
        {
            answer += values[key];
        }
        Console.WriteLine($"Day 11 Puzzle 2 Answer = {answer}");
    }

    static void Puzzle2_BruteForce() // never finished
    {
        int blinkCount = 75;
        long answer = 0;
        var data = File.ReadAllText(inputFile);
        var initialStones = data.Split(' ', StringSplitOptions.TrimEntries).Select(x => long.Parse(x)).ToList();
        using (var streamOut = File.Open(tempPrefix + "0.dat", FileMode.Create, FileAccess.Write))
        {
            using var writer = new BinaryWriter(streamOut);
            foreach (long s in initialStones)
            {
                writer.Write(s);
            }
        }
        long value;
        long part1;
        long part2;
        StringBuilder stringVal = new();
        for (int blink = 1; blink <= blinkCount; blink++)
        {
            Console.WriteLine(blink);
            answer = 0; // capture the longest output
            if (blink > 1)
            {
                File.Delete(tempPrefix + (blink - 2).ToString() + ".dat");
            }
            using var streamIn = File.Open(tempPrefix + (blink - 1).ToString() + ".dat", FileMode.Open, FileAccess.Read);
            using var streamOut = File.Open(tempPrefix + blink.ToString() + ".dat", FileMode.Create, FileAccess.Write);
            using var reader = new BinaryReader(streamIn);
            using var writer = new BinaryWriter(streamOut);
            try
            {
                while (true)
                {
                    value = reader.ReadInt64();
                    if (value == 0)
                    {
                        value = 1;
                        writer.Write(value);
                    }
                    else
                    {
                        stringVal.Clear();
                        stringVal.Append(value);
                        if (stringVal.Length % 2 == 0)
                        {
                            part1 = 0;
                            part2 = 0;
                            for (int i = 0; i < stringVal.Length / 2; i++)
                            {
                                part1 = (part1 * 10) + (int)stringVal[i] - (int)'0';
                            }
                            for (int i = stringVal.Length / 2; i < stringVal.Length; i++)
                            {
                                part2 = (part2 * 10) + (int)stringVal[i] - (int)'0';
                            }
                            writer.Write(part1);
                            writer.Write(part2);
                            answer++;
                        }
                        else
                        {
                            value *= 2024;
                            writer.Write(value);
                        }
                    }
                    answer++;
                }
            }
            catch (Exception)
            {
            }
        }
        File.WriteAllText("C:\\Temp\\answer.txt", answer.ToString());
        Console.WriteLine($"Day 11 Puzzle 2 Answer = {answer}");
    }
}
