namespace AoC2024_12;

public class Region
{
    public char Plant { get; set; } = ' ';
    public List<Plot> Plots { get; set; } = [];

    public long Area()
    {
        return Plots.Count;
    }

    public long Parimeter()
    {
        return Plots.Sum(x => x.Perimeter());
    }

    public long Sides()
    {
        
    }
}
