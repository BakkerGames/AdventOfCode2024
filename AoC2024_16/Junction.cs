namespace AoC2024_16;

public class Junction : Point
{
    public int MinScoreToHere { get; set; } = int.MaxValue;

    public Junction()
    {
    }

    public Junction(int y, int x)
    {
        Y = y;
        X = x;
    }
}
