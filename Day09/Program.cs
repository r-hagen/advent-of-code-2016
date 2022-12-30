using System.Diagnostics;
using System.Text.RegularExpressions;

var input = File.ReadAllText("d09.in");
var re = new Regex(@"^\((\d+)x(\d+)\)", RegexOptions.Compiled);

long DecompressedLength(ReadOnlySpan<char> inputSpan, bool isPart2) {
    var pos = 0;
    var len = 0L;

    while (pos < inputSpan.Length) {
        var compressedSpan = inputSpan[pos..];

        if (re.IsMatch(compressedSpan)) {
            var match = re.Match(compressedSpan.ToString());
            var letterCount = int.Parse(match.Groups[1].Value);
            var repeatCount = int.Parse(match.Groups[2].Value);
            var dataSpan = compressedSpan.Slice(match.Length, letterCount);
            pos += match.Length + dataSpan.Length;

            if (isPart2 && re.IsMatch(dataSpan))
                len += repeatCount * DecompressedLength(dataSpan, isPart2);
            else
                len += dataSpan.Length * repeatCount;
        }
        else {
            len++;
            pos++;
        }
    }

    return len;
}

Debug.Assert(DecompressedLength("X(8x2)(3x3)ABCY", true) == 20);
Debug.Assert(DecompressedLength("(27x12)(20x12)(13x14)(7x10)(1x12)A", true) == 241920);
Debug.Assert(DecompressedLength("(25x3)(3x3)ABC(2x3)XY(5x2)PQRSTX(18x9)(3x2)TWO(5x7)SEVEN", true) == 445);

Console.WriteLine($"Part 1: {DecompressedLength(input, false)}");
Console.WriteLine($"Part 2: {DecompressedLength(input, true)}");