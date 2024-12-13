namespace AoC2024_12;

public class Plot
{
    public char Plant { get; set; } = ' ';
    public int Y { get; set; } = 0;
    public int X { get; set; } = 0;

    public bool NorthFence { get; set; } = false;
    public bool EastFence { get; set; } = false;
    public bool SouthFence { get; set; } = false;
    public bool WestFence { get; set; } = false;

    public int Perimeter()
    {
        int result = 0;
        if (NorthFence) result++;
        if (EastFence) result++;
        if (SouthFence) result++;
        if (WestFence) result++;
        return result;
    }
}
