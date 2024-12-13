namespace AoC2024_12;

public class Region
{
    public char Plant { get; set; } = ' ';
    public List<Plot> Plots { get; set; } = [];

    public int MinY { get; set; } = -1;
    public int MinX { get; set; } = -1;
    public int MaxY { get; set; } = -1;
    public int MaxX { get; set; } = -1;

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
        Plot?[,] garden = new Plot?[MaxY - MinY + 1, MaxX - MinX + 1];
        foreach (Plot p in Plots)
        {
            garden[p.Y - MinY, p.X - MinX] = p;
        }
        var north = 0;
        var south = 0;
        var east = 0;
        var west = 0;
        for (int y = 0; y <= MaxY - MinY; y++)
        {
            var onNorthFence = false;
            var onSouthFence = false;
            for (int x = 0; x <= MaxX - MinX; x++)
            {
                if (!onNorthFence && garden[y, x] != null && garden[y, x]!.NorthFence)
                {
                    onNorthFence = true;
                    north++;
                }
                else if (onNorthFence && (garden[y, x] == null || !garden[y, x]!.NorthFence))
                {
                    onNorthFence = false;
                }
                if (!onSouthFence && garden[y, x] != null && garden[y, x]!.SouthFence)
                {
                    onSouthFence = true;
                    south++;
                }
                else if (onSouthFence && (garden[y, x] == null || !garden[y, x]!.SouthFence))
                {
                    onSouthFence = false;
                }
            }
        }
        for (int x = 0; x <= MaxX - MinX; x++)
        {
            var onEastFence = false;
            var onWestFence = false;
            for (int y = 0; y <= MaxY - MinY; y++)
            {
                if (!onEastFence && garden[y, x] != null && garden[y, x]!.EastFence)
                {
                    onEastFence = true;
                    east++;
                }
                else if (onEastFence && (garden[y, x] == null || !garden[y, x]!.EastFence))
                {
                    onEastFence = false;
                }
                if (!onWestFence && garden[y, x] != null && garden[y, x]!.WestFence)
                {
                    onWestFence = true;
                    west++;
                }
                else if (onWestFence && (garden[y, x] == null || !garden[y, x]!.WestFence))
                {
                    onWestFence = false;
                }
            }
        }
        return north + south + east + west; // todo ###
    }
}
