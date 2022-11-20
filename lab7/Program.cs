using System.Numerics;

const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ .,;-'";
Console.WriteLine(alphabet.Length);

var text = new string(File.ReadAllText(@"E:\lpnu\ТЗІ\lab7\input.txt").Where(t => t is not ('\n' or '\r')).ToArray());

var (e, d, n, fn) = RsaGetKeys(47, 59);
Console.WriteLine($"\ne: {e} d: {d} n: {n} fn: {fn}\n");

var numberText = RsaGetLetterNumber(text);

var encoded = RsaEncode(numberText).ToList();
Console.WriteLine(RsaGetLetterFromNumber(encoded));

Console.WriteLine();

var decoded = RsaDecode(encoded);
Console.WriteLine(RsaGetLetterFromNumber(decoded));

File.WriteAllText(@"E:\lpnu\ТЗІ\lab7\output.txt", RsaGetLetterFromNumber(encoded));

static Keys RsaGetKeys(int p, int q)
{
    var e = 2;
    var fn = (p - 1) * (q - 1);
    while (e < fn)
    {
        if (Math.Abs(Gcd(e, fn) - 1) < 0.00001)
        {
            break;
        }

        e++;
    }

    int d;
    for (var i = 1; ; i++)
    {
        d = (1 + i * fn) / e;
        if (d * e % fn == 1)
        {
            break;
        }
    }

    return new Keys(e, d, p * q, fn);

    static double Gcd(double a, double h)
    {
        while (true)
        {
            var temp = a % h;
            if (temp == 0)
            {
                return h;
            }

            a = h;
            h = temp;
        }
    }
}

static BigInteger[] RsaGetLetterNumber(string inputText)
{
    var result = new BigInteger[inputText.Length];
    for (var i = 0; i < inputText.Length; i++)
    {
        result[i] = alphabet.IndexOf(inputText[i]);
    }

    return result;
}

string RsaGetLetterFromNumber(IEnumerable<BigInteger> arrayLetter)
{
    var result = string.Empty;

    result = arrayLetter.Aggregate(result, (current, letter) => current + alphabet[(int)letter % alphabet.Length]);

    return result;
}

IEnumerable<BigInteger> RsaEncode(IReadOnlyList<BigInteger> text)
{
    var result = new BigInteger[text.Count];
    for (var i = 0; i < text.Count; i++)
    {
        result[i] = BigInteger.ModPow(text[i], e, n);
    }

    return result;
}

IEnumerable<BigInteger> RsaDecode(IReadOnlyList<BigInteger> text)
{
    var result = new BigInteger[text.Count];
    for (var i = 0; i < text.Count; i++)
    {
        result[i] = BigInteger.ModPow(text[i], d, n);
    }

    return result;
}

public record Keys
(
    int E,
    int D,
    int N,
    int Fn
);