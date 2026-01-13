namespace Question2;

internal class Program
{
    static void Main(string[] args)
    {
        int t = int.Parse(Console.ReadLine());
        string i = Console.ReadLine();
        int m = int.Parse(Console.ReadLine());

        Explorer explorer = new Explorer(t);

        for (int a = 0; a < m; a++)
        {
            if (a == i.Length)
            {
                a = 0;
            }
            explorer.Move(i[a]);
        }
        Console.WriteLine($"Current Coordinate: {explorer.CurrentPosition}");
    }
}

class Explorer
{
    public Point CurrentPosition = new Point(0, 0);
    public CircularQueue Trail;
    private char _facing = 'N';

    public Explorer(int trailSize)
    {
        Trail = new CircularQueue(trailSize);
    }

    public void Move(char direction)
    {
        if (TryMove(direction))
        {
            Trail.Add(CurrentPosition);
            CurrentPosition = CurrentPosition.Move(direction);
        }

    }

    public bool TryMove(char direction, int turnCounter = 0)
    {
        if (turnCounter == 4)
        {
            return false;
        }
        Point nextPosition = CurrentPosition.Move(direction);
        if (Trail.Contains(nextPosition))
        {
            Turn('R');
            return TryMove(direction, ++turnCounter);
        }

        return true;
    }

    public void Turn(char direction)
    {
        if (direction == 'L')
        {
            switch (_facing)
            {
                case 'N':
                    _facing = 'W';
                    break;
                case 'E':
                    _facing = 'N';
                    break;
                case 'S':
                    _facing = 'E';
                    break;
                case 'W':
                    _facing = 'S';
                    break;
            }
        }
        else if (direction == 'R')
        {
            switch (_facing)
            {
                case 'N':
                    _facing = 'E';
                    break;
                case 'E':
                    _facing = 'S';
                    break;
                case 'S':
                    _facing = 'W';
                    break;
                case 'W':
                    _facing = 'N';
                    break;
            }
        }
    }
}

record Point(int X, int Y)
{
    public Point Move(char facing)
    {
        switch (facing)
        {
            case 'N':
                return new Point(X, Y + 1);
            case 'E':
                return new Point(X + 1, Y);
            case 'S':
                return new Point(X, Y - 1);
            default:
                return new Point(X - 1, Y);
        }
    }
}

class CircularQueue
{
    private int _length;
    private Queue<Point> _items;

    public CircularQueue(int queueLength)
    {
        _length = queueLength;
        _items = new Queue<Point>(_length);
    }

    public void Add(Point item)
    {
        if (_items.Count < _length)
        {
            _items.Enqueue(item);
        }
        else
        {
            _items.Dequeue();
            _items.Enqueue(item);
        }
    }

    public bool Contains(Point nextPosition)
    {
        return _items.Contains(nextPosition);
    }
}

