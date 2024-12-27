namespace AoC2024_16;

public class Position : Point, IEquatable<Position>, IComparable<Position>
{
    private int _direction = 0;

    public Position()
    {
    }

    public Position(int y, int x, int d)
    {
        Y = y;
        X = x;
        Direction = d;
    }

    public int Direction
    {
        get
        {
            return _direction;
        }
        set
        {
            _direction = value % 4;
            if (_direction < 0)
            {
                _direction += 4;
            }
        }
    }

    public int CompareTo(Position? other)
    {
        if (other == null) return -1;
        if (this.Y < other.Y) return -1;
        if (this.Y == other.Y && this.X < other.X) return -1;
        if (this.Y == other.Y && this.X == other.X)
        {
            if (this.Direction < other.Direction) return -1;
            if (this.Direction == other.Direction) return 0;
        }
        return 1;
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
        return (Y * 100000) + (X * 10) + Direction;
    }

    public override string ToString()
    {
        return $"({Y},{X},{Direction})";
    }
}
