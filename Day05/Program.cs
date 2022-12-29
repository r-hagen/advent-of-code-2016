using System.Security.Cryptography;
using System.Text;

const string doorId = "cxdnnyjw";

string ComputeHash(int index) {
    var bytes = Encoding.ASCII.GetBytes($"{doorId}{index}");
    var hash = MD5.HashData(bytes);
    var hex = Convert.ToHexString(hash);
    return hex;
}

var sb = new StringBuilder();
var i1 = 0;
while (sb.Length != 8) {
    var hex = ComputeHash(i1);
    if (hex.StartsWith("00000"))
        sb.Append(hex[5]);
    i1++;
}
Console.WriteLine($"Part 1: {sb}");

var password = Enumerable.Range(0, 8).Select(_ => '_').ToArray();
var found = 0;
var i2 = 0;
while (found != 8) {
    var hex = ComputeHash(i2);
    if (hex.StartsWith("00000") && char.IsDigit(hex[5])) {
        var k = int.Parse(hex[5].ToString());
        if (k < password.Length && password[k] == '_') {
            password[k] = hex[6];
            found++;
        }
    }
    i2++;
}
Console.WriteLine($"Part 2: {string.Join("", password)}");