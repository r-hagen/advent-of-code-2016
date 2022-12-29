using System.Text;

var messages = File.ReadLines("d06.in").Select(x => x.ToArray()).ToArray();

var sb = new StringBuilder();
for (var col = 0; col < messages[0].Length; col++) {
    var columnLetters = messages.Select(line => line[col]).ToArray();
    var mostCommon = columnLetters.Distinct()
        .Select(letter => (Letter: letter, Count: columnLetters.Count(x => x == letter)))
        .MaxBy(t => t.Count)
        .Letter;
    sb.Append(mostCommon);
}
Console.WriteLine($"Part 1: {sb}");

sb.Clear();
for (var col = 0; col < messages[0].Length; col++) {
    var columnLetters = messages.Select(line => line[col]).ToArray();
    var leastCommon = columnLetters.Distinct()
        .Select(letter => (Letter: letter, Count: columnLetters.Count(x => x == letter)))
        .MinBy(t => t.Count)
        .Letter;
    sb.Append(leastCommon);
}
Console.WriteLine($"Part 2: {sb}");