using System.Security.Cryptography;
using System.Text;

namespace UniManage.Core.Security;

/// <summary>
/// Encryption/Decryption service for configuration values (e.g., passwords).
/// Uses AES-256 encryption with a key derived from machine-specific data.
/// </summary>
public static class ConfigEncryption
{
    private const string PREFIX = "ENC:";

    // TODO: Replace this with your own encryption key management strategy
    // Options:
    // 1. Store key in Azure Key Vault / AWS KMS
    // 2. Use DPAPI (Windows only): ProtectedData.Protect/Unprotect
    // 3. Use certificate-based encryption
    // 4. Environment variable (better than hardcoded)
    private static readonly byte[] EncryptionKey = DeriveKeyFromMachineId();

    /// <summary>
    /// Encrypt a plain text value
    /// </summary>
    public static string Encrypt(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return plainText;

        using (var aes = Aes.Create())
        {
            aes.Key = EncryptionKey;
            aes.GenerateIV();

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            {
                // Write IV first (needed for decryption)
                ms.Write(aes.IV, 0, aes.IV.Length);

                using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                using (var sw = new StreamWriter(cs))
                {
                    sw.Write(plainText);
                }

                var encrypted = ms.ToArray();
                return PREFIX + Convert.ToBase64String(encrypted);
            }
        }
    }

    /// <summary>
    /// Decrypt an encrypted value (if it starts with PREFIX), otherwise return as-is
    /// </summary>
    public static string? Decrypt(string? encryptedValue)
    {
        if (string.IsNullOrEmpty(encryptedValue))
            return encryptedValue;

        if (!encryptedValue.StartsWith(PREFIX))
            return encryptedValue; // Not encrypted, return plain text

        try
        {
            var cipherText = encryptedValue.Substring(PREFIX.Length);
            var buffer = Convert.FromBase64String(cipherText);

            using (var aes = Aes.Create())
            {
                aes.Key = EncryptionKey;

                using (var ms = new MemoryStream(buffer))
                {
                    // Read IV from the beginning
                    var iv = new byte[aes.IV.Length];
                    ms.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw new CryptographicException("Failed to decrypt configuration value. The encryption key may have changed.", ex);
        }
    }

    /// <summary>
    /// Check if a value is encrypted
    /// </summary>
    public static bool IsEncrypted(string? value)
    {
        return !string.IsNullOrEmpty(value) && value.StartsWith(PREFIX);
    }

    /// <summary>
    /// Derive encryption key from machine-specific identifier
    /// WARNING: This is basic implementation. For production:
    /// - Use Azure Key Vault, AWS KMS, or HashiCorp Vault
    /// - Use DPAPI on Windows
    /// - Store key separately, not in code
    /// </summary>
    private static byte[] DeriveKeyFromMachineId()
    {
        // Use machine name + environment variable as seed
        // IMPORTANT: Set this environment variable on each deployment environment
        var seed = Environment.MachineName + (Environment.GetEnvironmentVariable("UNIMANAGE_ENCRYPTION_SEED") ?? "DefaultSeed2024");

        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(seed));
        }
    }
}

/// <summary>
/// Console utility to encrypt/decrypt configuration values
/// Usage:
///   dotnet run --project UniManage.Core -- encrypt "your_password"
///   dotnet run --project UniManage.Core -- decrypt "ENC:base64string"
/// </summary>
public class ConfigEncryptionTool
{
    public static void Main(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("  Encrypt: dotnet run -- encrypt <plain_text>");
            Console.WriteLine("  Decrypt: dotnet run -- decrypt <encrypted_text>");
            Console.WriteLine();
            Console.WriteLine("Example:");
            Console.WriteLine("  dotnet run -- encrypt \"uni_manager@2024\"");
            return;
        }

        var command = args[0].ToLower();
        var value = args[1];

        try
        {
            switch (command)
            {
                case "encrypt":
                    var encrypted = ConfigEncryption.Encrypt(value);
                    Console.WriteLine("Encrypted value:");
                    Console.WriteLine(encrypted);
                    Console.WriteLine();
                    Console.WriteLine("Use this in appsettings.json:");
                    Console.WriteLine($"\"Password\": \"{encrypted}\"");
                    break;

                case "decrypt":
                    var decrypted = ConfigEncryption.Decrypt(value);
                    Console.WriteLine("Decrypted value:");
                    Console.WriteLine(decrypted);
                    break;

                default:
                    Console.WriteLine($"Unknown command: {command}");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}
