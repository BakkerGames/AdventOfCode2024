namespace AoC2024_16;

public class Node
{
    public Position Pos = new();

    public int Value = 0;

    public Node? Previous = null;
    public List<Node> Next = [];

    public Node()
    {
    }

    public Node(int y, int x, int dir)
    {
        Pos.Y = y;
        Pos.X = x;
        Pos.Direction = dir;
    }

    public Node(int y, int x, int dir, int value)
    {
        Pos.Y = y;
        Pos.X = x;
        Pos.Direction = dir;
        Value = value;
    }

    public Node(int y, int x, int dir, int value, Node prev)
    {
        Pos.Y = y;
        Pos.X = x;
        Pos.Direction = dir;
        Value = value;
        Previous = prev;
    }

    public Node Clone()
    {
        return new Node(Pos.Y, Pos.X, Pos.Direction, Value);
    }
}
