var _alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ .,;-'".ToCharArray();
const string input = @"E:\lpnu\ТЗІ\lab2\CC_156";
const string result = @"E:\lpnu\ТЗІ\lab2\result.txt";

if (File.Exists(result))
{
    File.Delete(result);
}

var text = new string(File.ReadAllText(input)
    .Where(x => x != '\n' && x != '\r')
    .ToArray());

Decrypt(text);
CorrespondingSymbols(_alphabet, text, 24);

void Decrypt(string text)
{
    for (int j = 1; j < 32; j++)
    {
        File.AppendAllLines(result, new string[] { $"Decryption with key {j}:\n" });
        List<char> decryptedText = new();

        for (int i = 0; i < text.Length; i++)
        {
            int index = Array.IndexOf(_alphabet, text[i]);
            int num = index - j;
            int newIndex = 0;

            if (num < 0)
            {
                newIndex = num + 32;
            }
            else
            {
                newIndex = num % 32;
            }

            var newLetter = _alphabet[newIndex];
            decryptedText.Add(newLetter);
        }

        File.AppendAllLines(result, new string[] { string.Concat(decryptedText), "" });
    }
}
void CorrespondingSymbols(char[] alphabet, string text, int key)
{
    for (int i = 0; i < text.Length; i++)
    {
        var letter = text[i];
        int index = Array.IndexOf(alphabet, letter);
        int num = index - key;
        int newIndex = 0;

        if (num < 0)
        {
            newIndex = num + 32;
        }
        else
        {
            newIndex = num % 32;
        }

        var correspondingLetter = alphabet[newIndex];
        Console.WriteLine($"[{letter}]\t-\t[{correspondingLetter}]");
    }
}