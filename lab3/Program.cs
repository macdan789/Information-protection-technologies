using System.Text;

var alphabet = GetAlphabet(@"E:\lpnu\ТЗІ\lab3\alphabet.txt");

var encodedText = File.ReadAllText(@"E:\lpnu\ТЗІ\lab3\SZ_156");
var decodedText = new StringBuilder();

foreach (var symbol in encodedText)
{
    if (alphabet.ContainsKey(symbol))
    {
        decodedText.Append(alphabet[symbol]);
    }
    else
    {
        decodedText.Append(symbol);
    }
}

Console.WriteLine(decodedText.ToString());

Dictionary<char, char> GetAlphabet(string path)
{
    var content = File.ReadAllText(path);
    var keyValuePairs = content
        .Split('\n')
        .Select(x => x.Split("--"))
        .Select(x => new[] { x[0].Trim(), x[1].Trim() })
        .ToList();

    Dictionary<char, char> alphabet = new();
    foreach (var pair in keyValuePairs)
    {
        alphabet.Add(char.Parse(pair[0]), char.Parse(pair[1]) == '_' ? ' ' : char.Parse(pair[1]));
    }

    return alphabet;
}