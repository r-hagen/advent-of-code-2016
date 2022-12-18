var k1 = new Dictionary<(int, int), int> {
    {(0, 0), 1}, {(1, 0), 2}, {(2, 0), 3},
    {(0, 1), 4}, {(1, 1), 5}, {(2, 1), 6},
    {(0, 2), 7}, {(1, 2), 8}, {(2, 2), 9},
};

(int X, int Y) p1 = (1, 1);
var code1 = new List<int>();

foreach (var line in File.ReadLines("d02.in")) {
    foreach (var move in line) {
        p1 = move switch {
            'U' when p1.Y >= 1 => p1 with {Y = p1.Y - 1},
            'D' when p1.Y <= 1 => p1 with {Y = p1.Y + 1},
            'L' when p1.X >= 1 => p1 with {X = p1.X - 1},
            'R' when p1.X <= 1 => p1 with {X = p1.X + 1},
            _ => p1
        };
    }

    code1.Add(k1[(p1.X, p1.Y)]);
}

Console.WriteLine("Part 1: " + string.Join("", code1));

var k2 = new Dictionary<(int, int), char> {
    {(2, 0), '1'},
    {(1, 1), '2'}, {(2, 1), '3'}, {(3, 1), '4'},
    {(0, 2), '5'}, {(1, 2), '6'}, {(2, 2), '7'}, {(3, 2), '8'}, {(4, 2), '9'},
    {(1, 3), 'A'}, {(2, 3), 'B'}, {(3, 3), 'C'},
    {(2, 4), 'D'},
};

(int X, int Y) p2 = (0, 2);
var code2 = new List<char>();

foreach (var line in File.ReadLines("d02.in")) {
    foreach (var move in line) {
        p2 = move switch {
            'U' when k2.ContainsKey((p2.X, p2.Y - 1)) => p2 with {Y = p2.Y - 1},
            'D' when k2.ContainsKey((p2.X, p2.Y + 1)) => p2 with {Y = p2.Y + 1},
            'L' when k2.ContainsKey((p2.X - 1, p2.Y)) => p2 with {X = p2.X - 1},
            'R' when k2.ContainsKey((p2.X + 1, p2.Y)) => p2 with {X = p2.X + 1},
            _ => p2
        };
    }

    code2.Add(k2[(p2.X, p2.Y)]);
}

Console.WriteLine("Part 2: " + string.Join("", code2));