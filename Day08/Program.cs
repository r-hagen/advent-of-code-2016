using System.Text;

const int rows = 6;
const int cols = 50;

var screen = new char[rows, cols];
for (var r = 0; r < rows; r++)
for (var c = 0; c < cols; c++)
    screen[r, c] = '.';

foreach (var instruction in File.ReadLines("d08.in")) {
    var parts = instruction.Split();

    if (parts is ["rect", _]) {
        var rect = parts[1].Split("x").Select(int.Parse).ToArray();
        var (width, height) = (rect[0], rect[1]);
        for (var r = 0; r < height; r++)
        for (var c = 0; c < width; c++)
            screen[r, c] = '#';
    }

    if (parts is ["rotate", ..]) {
        var rowOrCol = int.Parse(parts[2][2..]);
        var rotation = int.Parse(parts[4]);

        if (parts[1] == "column") {
            var column = Enumerable.Range(0, rows)
                .Select(r => (Row: (r + rotation) % rows, Value: screen[r, rowOrCol]))
                .ToArray();
            foreach (var el in column)
                screen[el.Row, rowOrCol] = el.Value;
        }

        if (parts[1] == "row") {
            var row = Enumerable.Range(0, cols)
                .Select(c => (Column: (c + rotation) % cols, Value: screen[rowOrCol, c]))
                .ToArray();
            foreach (var el in row)
                screen[rowOrCol, el.Column] = el.Value;
        }
    }
}

var p1 = 0;
for (var r = 0; r < rows; r++)
for (var c = 0; c < cols; c++)
    if (screen[r, c] == '#')
        p1 += 1;
Console.WriteLine($"Part 1: {p1}");

Console.WriteLine("Part 2");
Show(screen);

static void Show(char[,] s) {
    var sb = new StringBuilder();
    for (var r = 0; r < rows; r++) {
        for (var c = 0; c < cols; c++) {
            sb.Append(s[r, c]);
        }
        Console.WriteLine(sb);
        sb.Clear();
    }
}