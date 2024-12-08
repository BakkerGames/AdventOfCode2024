namespace AoC2024_08;

public class Point : IEquatable<Point>
{
    public int Y { get; set; }
    public int X { get; set; }

    public Point()
    {
    }

    public Point(int y, int x)
    {
        Y = y;
        X = x;
    }

    public Point Clone()
    {
        return new Point() { Y = this.Y, X = this.X };
    }

    public bool Equals(Point? other)
    {
        if (other == null) return false;
        return other.Y == Y && other.X == X;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Point);
    }

    public override int GetHashCode()
    {
        return (Y * 10000) + X;
    }

    public override string ToString()
    {
        return $"({Y},{X})";
    }
}
