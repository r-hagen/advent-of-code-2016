using System.Text.RegularExpressions;

var rooms = File.ReadLines("d04.in").Select(Parse).ToList();

var sum = rooms.Where(IsReal).Sum(x => x.Id);
Console.WriteLine($"Part 1: {sum}");

var id = rooms.Where(IsReal)
    .Select(x => (x.Id, Decrypted: Decrypt(x)))
    .First(x => x.Decrypted == "northpole object storage")
    .Id;
Console.WriteLine($"Part 2: {id}");

static Room Parse(string s) {
    var match = Regex.Match(s, @"(?<name>.*)-(?<id>\d+)\[(?<checksum>.*)\]");
    return new Room(match.Groups["name"].Value, int.Parse(match.Groups["id"].Value), match.Groups["checksum"].Value);
}

static bool IsReal(Room room) {
    return room.Name.Distinct().Where(x => x != '-')
        .Select(letter => (Letter: letter, Count: room.Name.Count(x => x == letter)))
        .OrderByDescending(x => x.Count)
        .ThenBy(x => x.Letter)
        .Aggregate(string.Empty, (current, next) => current + next.Letter)
        .StartsWith(room.Checksum);
}

static string Decrypt(Room room) {
    char Rotate(char letter, int rotations) {
        return letter == '-'
            ? ' '
            : Enumerable.Range(0, rotations)
                .Aggregate(letter, (shifted, _) => shifted == 'z' ? 'a' : (char) (shifted + 1));
    }

    return room.Name
        .Select(letter => Rotate(letter, room.Id))
        .Aggregate(string.Empty, (current, next) => current + next);
}

public record Room(string Name, int Id, string Checksum);