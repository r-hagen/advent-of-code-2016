static bool NotEmpty(string s) => !string.IsNullOrWhiteSpace(s);
var triangles = File.ReadLines("d03.in").Select(x => x.Split().Where(NotEmpty).Select(int.Parse).ToArray()).ToArray();

static int IsValidTriangle(int a, int b, int c) => a + b > c && a + c > b && b + c > a ? 1 : 0;

var possible1 = triangles.Sum(x => IsValidTriangle(x[0], x[1], x[2]));
Console.WriteLine($"Part 1: {possible1}");

var possible2 = triangles.Chunk(3).Sum(x =>
    IsValidTriangle(x[0][0], x[1][0], x[2][0]) +
    IsValidTriangle(x[0][1], x[1][1], x[2][1]) +
    IsValidTriangle(x[0][2], x[1][2], x[2][2]));
Console.WriteLine($"Part 2: {possible2}");