using System.Security.Cryptography;
using System;
using System.Text;

namespace OnlineSubscriptionBackEnd
{
public class LicenseGenerator
{
    // Generate Product Key
    public static string GenerateProductKey()
    {
        var random = new Random();
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        StringBuilder productKey = new StringBuilder();

        for (int i = 0; i < 25; i++)
        {
            productKey.Append(chars[random.Next(chars.Length)]);
            if ((i + 1) % 5 == 0 && i != 24)
                productKey.Append("-");
        }

        return productKey.ToString();
    }

    // Generate Client Key
    public static string GenerateClientKey(string clientIdentifier)
    {
        string salt = "SALT1234"; // Add a unique salt
        string data = clientIdentifier + salt;

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hash).Replace("-", "").ToLower();
        }
    }

    // Generate Validity Key
    public static string GenerateValidityKey(string productKey, string clientKey, DateTime expirationDate)
    {
        string data = $"{productKey}|{clientKey}|{expirationDate:yyyyMMdd}";
        return EncryptionHelper.Encrypt(data);
    }

    public static (string ProductKey, string ClientKey, DateTime ExpirationDate) DecodeValidityKey(string validityKey)
    {
        string decryptedData = EncryptionHelper.Decrypt(validityKey);
        string[] parts = decryptedData.Split('|');
        return (parts[0], parts[1], DateTime.ParseExact(parts[2], "yyyyMMdd", null));
    }
}

public class EncryptionHelper
{
    private static readonly Aes aes = Aes.Create();

    static EncryptionHelper()
    {
        aes.Key = Encoding.UTF8.GetBytes("YourSecretKey1234"); // 16-byte key
        aes.IV = Encoding.UTF8.GetBytes("YourIV1234567890"); // 16-byte IV
    }

    public static string Encrypt(string plainText)
    {
        using (var encryptor = aes.CreateEncryptor())
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
            return Convert.ToBase64String(encryptedBytes);
        }
    }

    public static string Decrypt(string encryptedText)
    {
        using (var decryptor = aes.CreateDecryptor())
        {
            byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
            byte[] decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}

public class LicenseValidator
{
    public static bool IsSoftwareValid(string validityKey)
    {
        try
        {
            var (_, _, expirationDate) = LicenseGenerator.DecodeValidityKey(validityKey);
            return DateTime.Now <= expirationDate;
        }
        catch
        {
            return false; // Invalid key or expired
        }
    }
}

}