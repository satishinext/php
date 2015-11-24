using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

public sealed class StringCipher
{
	private static readonly StringCipher instance = new StringCipher();
	
    private int    keysize;    // This constant is used to determine the keysize of the encryption algorithm.
    private string initVector; // This constant string is used as a "salt" value for the PasswordDeriveBytes function calls.
	
    static StringCipher() 
	{
	}
	
    private StringCipher() 
	{
		keysize    = 256;
		initVector = MakeIV();
	}

    public static StringCipher Instance 
	{ 
		get { return instance; }
	}
	
    public string Encrypt(string plainText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
        byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream();
        CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
        cryptoStream.FlushFinalBlock();
        byte[] cipherTextBytes = memoryStream.ToArray();
        memoryStream.Close();
        cryptoStream.Close();
        return Convert.ToBase64String(cipherTextBytes);
    }

    public string Decrypt(string cipherText, string passPhrase)
    {
        byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
        byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, null);
        byte[] keyBytes = password.GetBytes(keysize / 8);
        RijndaelManaged symmetricKey = new RijndaelManaged();
        symmetricKey.Mode = CipherMode.CBC;
        ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
        MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
        CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
        byte[] plainTextBytes = new byte[cipherTextBytes.Length];
        int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
        memoryStream.Close();
        cryptoStream.Close();
        return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
    }
	
	private string MakeIV()
    {
		int    ivLength = keysize / 16; // This size of the IV (in bytes) must = (keysize / 8). Default keysize is 256, so the IV must be 32 bytes long. Using a 16 character string here gives us 32 bytes when converted to a byte array.
		string uniqId   = "o" + ReverseString(SystemInfo.deviceUniqueIdentifier);
		
        return uniqId.PadRight(ivLength, 'u').Substring(0, ivLength);
    }

	private string ReverseString(string s)
	{
		char[] charArray = s.ToCharArray();
		System.Array.Reverse( charArray );
		return new string(charArray);
	}
}