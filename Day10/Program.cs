using System.Diagnostics;

var bots = new Dictionary<int, List<int>>();
var outputs = new Dictionary<int, int>();
var instructions = new Dictionary<int, (int, string, int, string)>();

foreach (var line in File.ReadLines("d10.in").ToList()) {
    var parts = line.Split();
    if (parts[0] == "value") {
        var chip = int.Parse(parts[1]);
        var botId = int.Parse(parts[5]);
        if (bots.TryGetValue(botId, out var chips)) chips.Add(chip);
        else bots[botId] = new List<int> {chip};
    }
    else {
        var id = int.Parse(parts[1]);
        var lowType = parts[5];
        var lowId = int.Parse(parts[6]);
        var highType = parts[10];
        var highId = int.Parse(parts[11]);
        Debug.Assert(!instructions.ContainsKey(id));
        instructions[id] = (lowId, lowType, highId, highType);
    }
}

while (bots.Any()) {
    foreach (var (botId, botChips) in bots.Where(kvp => kvp.Value.Count == 2).ToArray()) {
        var (lowId, lowType, highId, highType) = instructions[botId];
        var low = botChips.Min();
        var high = botChips.Max();
        bots.Remove(botId);
        
        if (low == 17 && high == 61) {
            Console.WriteLine($"Part 1: {botId}");
        }

        switch (lowType) {
            case "bot":
                if (bots.TryGetValue(lowId, out var bot)) bot.Add(low);
                else bots[lowId] = new List<int> {low};
                break;
            case "output":
                Debug.Assert(!outputs.ContainsKey(lowId));
                outputs[lowId] = low;
                break;
        }

        switch (highType) {
            case "bot":
                if (bots.TryGetValue(highId, out var bot)) bot.Add(high);
                else bots[highId] = new List<int> {high};
                break;
            case "output":
                Debug.Assert(!outputs.ContainsKey(highId));
                outputs[highId] = high;
                break;
        }
    }
}

Console.WriteLine($"Part 2: {outputs[0] * outputs[1] * outputs[2]}");