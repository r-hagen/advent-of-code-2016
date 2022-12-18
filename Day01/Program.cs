var moves = File.ReadAllText("d01.in").Split(", ");
var visited = new Dictionary<Location, int>();

var location1 = new Location();
var direction1 = Direction.North;
foreach (var move in moves) {
    (location1, direction1) = Visit(location1, direction1, move);
}
Console.WriteLine("Part 1: " + Distance(location1));

var location2 = new Location();
var direction2 = Direction.North;
visited.Clear();
foreach (var move in moves) {
    (location2, direction2) = Visit(location2, direction2, move);
    var (visitedTwice, _) = visited.FirstOrDefault(v => v.Value >= 2);
    if (visitedTwice is not null) {
        Console.WriteLine($"Part 2: {Distance(visitedTwice)}");
        break;
    }
}

(Location, Direction) Visit(Location position, Direction direction, string line) {
    Enum.TryParse(line[..1], out LeftOrRight leftOrRight);
    var amount = int.Parse(line[1..]);
    
    direction = (direction, leftOrRight) switch {
        (Direction.North, LeftOrRight.R) => Direction.East,
        (Direction.East, LeftOrRight.R) => Direction.South,
        (Direction.South, LeftOrRight.R) => Direction.West,
        (Direction.West, LeftOrRight.R) => Direction.North,
        (Direction.North, LeftOrRight.L) => Direction.West,
        (Direction.East, LeftOrRight.L) => Direction.North,
        (Direction.South, LeftOrRight.L) => Direction.East,
        (Direction.West, LeftOrRight.L) => Direction.South,
        _ => throw new ArgumentOutOfRangeException()
    };

    for (var a = 1; a <= amount; a++) {
        position = direction switch {
            Direction.North => position with {Y = position.Y + 1},
            Direction.East => position with {X = position.X + 1},
            Direction.South => position with {Y = position.Y - 1},
            Direction.West => position with {X = position.X - 1},
            _ => throw new ArgumentOutOfRangeException()
        };

        if (visited.ContainsKey(position))
            visited[position] += 1;
        else 
            visited[position] = 1;
    }

    return (position, direction);
}

int Distance(Location pos) {
    return Math.Abs(pos.X) + Math.Abs(pos.Y);
}

enum LeftOrRight { R, L }
enum Direction { North, East, South, West }

internal record Location {
    public int X { get; init; }
    public int Y { get; init; }
}