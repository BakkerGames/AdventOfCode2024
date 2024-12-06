namespace AoC2024_06;

public class Position : IEquatable<Position>
{
    public int Y { get; set; }
    public int X { get; set; }
    public int Direction { get; set; }

    public Position Clone()
    {
        return new Position() { Y = this.Y, X = this.X, Direction = this.Direction };
    }

    public bool Equals(Position? other)
    {
        if (other == null) return false;
        return other.Y == Y && other.X == X && other.Direction == Direction;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as Position);
    }

    public override int GetHashCode()
    {
        return (Y * 10000) + (X * 10) + Direction;
    }
}
