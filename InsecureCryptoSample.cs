using System;
using System.Security.Cryptography;
using System.Text;

namespace InsecureCryptoSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Insecure cryptography: DES is outdated and insecure
            string plainText = "SensitiveData";
            byte[] key = Encoding.UTF8.GetBytes("12345678"); // DES requires 8-byte key
            byte[] iv = Encoding.UTF8.GetBytes("12345678");

            using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
            {
                ICryptoTransform encryptor = des.CreateEncryptor(key, iv);
                byte[] inputBytes = Encoding.UTF8.GetBytes(plainText);
                byte[] encrypted = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
                Console.WriteLine("Encrypted (base64): " + Convert.ToBase64String(encrypted));
            }
        }
    }
}
