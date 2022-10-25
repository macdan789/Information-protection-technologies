
var text = File.ReadAllText(@"E:\lpnu\ТЗІ\lab3\decoded.txt");

//Get unique symbols from text
var alphabet = text.ToCharArray().Where(x => x is not ('\r' or '\n')).Distinct().ToList();

var symbolFrequencyByAlphabet = GetSymbolFrequency();
var symbolFrequencyByFrequency = symbolFrequencyByAlphabet
    .OrderByDescending(x => x.Value)
    .ToDictionary(x => x.Key, y => y.Value);

//Print(symbolFrequencyByAlphabet, "alphabet");
Print(symbolFrequencyByFrequency, "frequency");
GetBigrams();
GetThreegrams();
GetFourgrams();


Dictionary<char, int> GetSymbolFrequency()
{
    var result = new Dictionary<char, int>();
    var allSymbols = text.ToCharArray();

    foreach (var symbol in alphabet.OrderBy(x => x))
    {
        var count = allSymbols.Count(x => x == symbol);
        result.Add(symbol, count);
    }

    return result;
}
void Print(Dictionary<char, int> keyValues, string desc)
{
    Console.WriteLine($"Unique symbols by {desc}");
    foreach (var item in keyValues)
    {
        Console.WriteLine($"[{item.Key}]\t{item.Value}");
    }
    Console.WriteLine();
}
void GetBigrams()
{
    var alphabetInString = string.Concat(alphabet.AsReadOnly());

    var result = new Dictionary<string, int>();

    var query = from a in alphabetInString
                from b in alphabetInString
                select (a, b) ;

    foreach (var (firstLetter, secondLetter) in query)
    {
        var count = 0;
        for (var i = 1; i < text.Length; i++)
        {
            if (firstLetter == text[i - 1] && secondLetter == text[i])
            {
                count++;
            }
        }

        result.Add($"{firstLetter}{secondLetter}", count);
    }

    Console.WriteLine("First 15 Bigrams");
    foreach (var letter in result.Where(a => a.Value is not 0).OrderByDescending(b => b.Value).Take(15))
    {
        Console.WriteLine($"[{letter.Key}]\t{letter.Value}");
    }
    Console.WriteLine();
}
void GetThreegrams()
{
    var alphabetInString = string.Concat(alphabet.AsReadOnly());

    var result = new Dictionary<string, int>();

    var query = from a in alphabetInString
                from b in alphabetInString
                from c in alphabetInString
                select (a, b, c);

    foreach (var (firstLetter, secondLetter, thirdLetter) in query)
    {
        var count = 0;
        for (var i = 1; i < text.Length - 1; i++)
        {
            if (firstLetter == text[i - 1] && secondLetter == text[i] && thirdLetter == text[i + 1])
            {
                count++;
            }
        }

        result.Add($"{firstLetter}{secondLetter}{thirdLetter}", count);
    }

    Console.WriteLine("First 15 Threegrams");
    foreach (var letter in result.Where(a => a.Value is not 0).OrderByDescending(b => b.Value).Take(15))
    {
        Console.WriteLine($"[{letter.Key}]\t{letter.Value}");
    }
    Console.WriteLine();
}
void GetFourgrams()
{
    var alphabetInString = string.Concat(alphabet.AsReadOnly());

    var result = new Dictionary<string, int>();

    var query = from a in alphabetInString
                from b in alphabetInString
                from c in alphabetInString
                from d in alphabetInString
                select (a, b, c, d);

    foreach (var (firstLetter, secondLetter, thirdLetter, fourthLetter) in query)
    {
        var count = 0;
        for (var i = 1; i < text.Length - 2; i++)
        {
            if (firstLetter == text[i - 1] && secondLetter == text[i] && thirdLetter == text[i + 1] && fourthLetter == text[i + 2])
            {
                count++;
            }
        }

        result.Add($"{firstLetter}{secondLetter}{thirdLetter}{fourthLetter}", count);
    }

    Console.WriteLine("First 15 Fourgrams");
    foreach (var letter in result.Where(a => a.Value is not 0).OrderByDescending(b => b.Value).Take(15))
    {
        Console.WriteLine($"[{letter.Key}]\t{letter.Value}");
    }
    Console.WriteLine();
}