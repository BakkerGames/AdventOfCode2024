namespace AoC2024_09;

internal class Program
{
    private const string inputFile = "C:\\Users\\Scott\\source\\repos\\AdventOfCode2024\\Input\\Day_09.txt";

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
        var (data, _, _) = UnpackData(lines[0]);
        int emptyPos = 0;
        int fromPos = data.Count - 1;
        while (emptyPos < fromPos)
        {
            while (emptyPos < data.Count && data[emptyPos] != null) emptyPos++;
            while (fromPos >= 0 && data[fromPos] == null) fromPos--;
            if (emptyPos < fromPos)
            {
                data[emptyPos] = data[fromPos];
                data[fromPos] = null;
                emptyPos++;
                fromPos--;
            }
        }
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i] != null)
            {
                answer += (int)(data[i] ?? 0) * i;
            }
        }
        Console.WriteLine($"Day 09 Puzzle 1 Answer = {answer}");
    }

    static void Puzzle2()
    {
        long answer = 0;
        var lines = File.ReadAllLines(inputFile);
        var (data, free, files) = UnpackData(lines[0]);
        
        //ShowData(data);

        /* This works, but is clunky. Trying lists below.
        int startNullPos;
        int endNullPos;
        int startFilePos;
        int endFilePos = data.Count - 1;
        int fileLen;
        int nullLen;
        int firstNullPos = 0;

        while (endFilePos > firstNullPos)
        {
            while (endFilePos >= 0 && data[endFilePos] == null) endFilePos--;
            startFilePos = endFilePos;
            while (startFilePos > 0 && data[startFilePos - 1] == data[endFilePos]) startFilePos--;
            fileLen = endFilePos - startFilePos + 1;

            endNullPos = firstNullPos;
            do
            {
                startNullPos = endNullPos;
                while (startNullPos < data.Count && data[startNullPos] != null) startNullPos++;
                if (endNullPos == firstNullPos)
                {
                    firstNullPos = startNullPos;
                }
                endNullPos = startNullPos;
                while (endNullPos < data.Count - 1 && data[endNullPos + 1] == null) endNullPos++;
                nullLen = endNullPos - startNullPos + 1;
                endNullPos++;
            } while (endNullPos < startFilePos && nullLen < fileLen);

            if (nullLen >= fileLen)
            {
                int j = startNullPos;
                for (int i = startFilePos; i <= endFilePos; i++)
                {
                    data[j] = data[i];
                    data[i] = null;
                    j++;
                }
            }
            endFilePos = startFilePos - 1;
        }
        */

        for (int fileNum = files.Count - 1; fileNum >= 0; fileNum--)
        {
            for (int freeNum = 0; freeNum < free.Count; freeNum++)
            {
                if (files[fileNum].StartPos < free[freeNum].StartPos)
                {
                    break;
                }
                if (files[fileNum].Length <= free[freeNum].Length)
                {
                    for (int ofs = 0; ofs < files[fileNum].Length; ofs++)
                    {
                        data[free[freeNum].StartPos + ofs] = files[fileNum].ID;
                        data[files[fileNum].StartPos + ofs] = null;
                    }
                    files[fileNum].StartPos = free[freeNum].StartPos;
                    files[fileNum].EndPos = files[fileNum].StartPos + files[fileNum].Length - 1;
                    free[freeNum].StartPos += files[fileNum].Length;
                    free[freeNum].Length -= files[fileNum].Length;
                    //ShowData(data);
                    break;
                }
            }
        }

        for (int i = 0; i < data.Count; i++)
        {
            if (data[i] != null)
            {
                answer += (int)(data[i] ?? 0) * i;
            }
        }

        Console.WriteLine($"Day 09 Puzzle 2 Answer = {answer}");
    }

    static (List<int?>, List<FreeBlock>, List<FileBlock>) UnpackData(string line)
    {
        int ID = 0;
        bool inFile = true;
        List<int?> data = [];
        List<FreeBlock> free = [];
        List<FileBlock> files = [];
        foreach (char c in line)
        {
            var value = (int)(c - '0');
            if (inFile)
            {
                files.Add(new FileBlock()
                {
                    ID = ID,
                    StartPos = data.Count,
                    EndPos = data.Count + value - 1,
                    Length = value
                });
                for (int i = 0; i < value; i++)
                {
                    data.Add(ID);
                }
                ID++;
            }
            else
            {
                free.Add(new FreeBlock()
                {
                    StartPos = data.Count,
                    EndPos = data.Count + value - 1,
                    Length = value
                });
                for (int i = 0; i < value; i++)
                {
                    data.Add(null);
                }
            }
            inFile = !inFile;
        }
        return (data, free, files);
    }

    static void ShowData(List<int?> data)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i] == null)
                Console.Write('.');
            else
                Console.Write(data[i].ToString());
        }
        Console.WriteLine();
    }
}
