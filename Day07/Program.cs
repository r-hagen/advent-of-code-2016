var addresses = File.ReadLines("d07.in").ToArray();

static bool SupportsTls(string address) {
    var insideBracket = false;
    var abbaFound = false;

    bool IsAbba(string s) => s.Distinct().Count() == 2 && s[0] == s[3] && s[1] == s[2];

    for (var i = 0; i <= address.Length - 4; i++) {
        insideBracket = address[i] switch {
            '[' => true,
            ']' => false,
            _ => insideBracket,
        };

        var letters = address.Substring(i, 4);

        if (!IsAbba(letters)) continue;
        if (insideBracket) return false;

        abbaFound = true;
    }

    return abbaFound;
}

var p1 = addresses.Count(SupportsTls);
Console.WriteLine($"Part 1: {p1}");

static bool SupportsSsl(string address) {
    var insideBracket = false;
    var abaList = new List<string>();
    var babList = new List<string>();

    bool IsAba(string s) => s.Distinct().Count() == 2 && s[0] == s[2];

    for (var i = 0; i < address.Length - 2; i++) {
        insideBracket = address[i] switch {
            '[' => true,
            ']' => false,
            _ => insideBracket
        };

        var letters = address.Substring(i, 3);

        if (!IsAba(letters)) continue;

        if (insideBracket) babList.Add(letters);
        else abaList.Add(letters);
    }

    return abaList.Any(aba => babList.Contains($"{aba[1]}{aba[0]}{aba[1]}"));
}

var p2 = addresses.Count(SupportsSsl);
Console.WriteLine($"Part 2: {p2}");