const string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ .,;-'";

var text = new string(File.ReadAllText(@"E:\lpnu\ТЗІ\lab6\input.txt").Where(t => t is not ('\n' or '\r')).ToArray());

var encrypted = FeistelNetwork(text, alphabet, 8, 10);
var decrypted = FeistelNetwork(text, alphabet, 8, 10, true);

File.WriteAllText(@"E:\lpnu\ТЗІ\lab6\output.txt", encrypted);

Console.WriteLine(text);
Console.WriteLine();
Console.WriteLine(decrypted);

static string FeistelNetwork(string inputText, string alphabet, int roundCount, int k0, bool decrypt = false)
{
    var result = "";
    if (inputText.Length % 2 != 0)
    {
        inputText += " ";
    }

    var letterArray = new int[inputText.Length];
    for (var i = 0; i < inputText.Length; i++)
    {
        letterArray[i] = alphabet.IndexOf(inputText[i]);
    }

    for (var j = 0; j < inputText.Length; j += 2)
    {
        var k = k0 - (decrypt ? roundCount - 1 : 0);
        var l = letterArray[j];
        var r = letterArray[j + 1];
        for (var i = 1; i <= roundCount; i++)
        {
            if (i == roundCount)
            {
                r ^= FeistelFunction(k, l, alphabet.Length);
                break;
            }

            (l, r) = (r ^ FeistelFunction(k, l, alphabet.Length), l);
            k += decrypt ? 1 : -1;
        }

        letterArray[j] = l;
        letterArray[j + 1] = r;
    }

    result = letterArray.Aggregate(result, (current, letter) => current + alphabet[letter]);

    return result;
}

static int FeistelFunction(int k, int i, int alphabetLength) =>
    (k + i) % alphabetLength;

