using System;


public static class Crypto
{
    #region Methods

    public static string CryptoXOR(string text, int key = 42)
    {
        var result = String.Empty;
        foreach (var symbol in text)
        {
            result += (char)(symbol ^ key);
        }
        return result;
    }


    #endregion
}
