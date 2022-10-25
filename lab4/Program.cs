var inputFile = @"E:\lpnu\ТЗІ\lab4\input.txt";
var outputFile = @"E:\lpnu\ТЗІ\lab4\output.txt";
var key = "MARKOBOHDAN";

//літери англійського алфавіту
const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

var inputText = File.ReadAllText(inputFile);

var outputText = VigenereAlgorithm(inputText, key);

File.WriteAllText(outputFile, outputText);


//генерація повторюваного пароля
string GetRepeatableKey(string password, int inputTextLength)
{
    var resultKey = password;
    
    while (resultKey.Length < inputTextLength)
    {
        resultKey += resultKey;
    }

    //повертаємо підстрічку сформованого ключа з довжиною вхідного тексту
    //(кількість символів ключа = кількість символів тексту для шифрування)
    return resultKey[..inputTextLength];
}


string VigenereAlgorithm(string inputText, string password, bool encrypting = true)
{
    var key = GetRepeatableKey(password, inputText.Length);
    var resultText = string.Empty;
    var lettersCount = letters.Length; //26

    for (int i = 0; i < inputText.Length; i++)
    {
        //знаходимо індекс символу вхідного тексту в алфавіті
        var letterIndex = letters.IndexOf(inputText[i]);

        //знаходимо індекс символу ключа в алфавіті
        var keyIndex = letters.IndexOf(key[i]);
        
        if (letterIndex == -1)
        {
            //якщо буква не найдена, тоді добавляємо її до результату в незмінному вигляді
            resultText += inputText[i];
        }
        else
        {
            //якщо буква найдена, тоді шифруємо/дешифруємо її
            //шукаємо відповідний індекс символу в алфавіті за формулами:
            
            //Encrypt(mn) = (Q + mn + kn) % Q.
            //Decrypt(cn) = (Q + cn - kn) % Q.
            //Q - кількість символів в алфавіті
            //kn - індекс символу ключа
            //mn - індекс символу тексту для шифрування
            //cn - індекс символу тексту для дешифрування
            
            var resultIndex = (lettersCount + letterIndex + ((encrypting ? 1 : -1) * keyIndex)) % lettersCount;
            resultText += letters[resultIndex];
        }
    }

    return resultText;
}