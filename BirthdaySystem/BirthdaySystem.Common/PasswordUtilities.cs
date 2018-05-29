using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace BirthdaySystem.Common
{
    public static class PasswordUtilities
    {
        private const string SaltCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        private static RandomNumberGenerator randomGenerator;
        private static SHA256 hashingAlgorithm;

        static PasswordUtilities()
        {
            randomGenerator = new RNGCryptoServiceProvider();
            hashingAlgorithm = new SHA256Managed();
        }

        public static string GeneratePasswordSalt(int length = AuthConstants.DefaultSaltLength)
        {
            return new string(Enumerable.Repeat(SaltCharacters, length)
                  .Select(s => s[GetRandomIntegerBetween(0, s.Length)])
                  .ToArray());
        }

        public static string GeneratePasswordHash(string password, string salt)
        {
            byte[] passwordHashBytes = hashingAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(password + salt));
            string passwordHash = string.Join(string.Empty, passwordHashBytes.Select(b => b.ToString("x2")));
            return passwordHash;
        }

        private static int GetRandomIntegerBetween(int min, int max)
        {
            using (randomGenerator)
            {
                uint scale = uint.MaxValue;

                while (scale == uint.MaxValue)
                {
                    byte[] fourBytes = new byte[4];
                    randomGenerator.GetBytes(fourBytes);

                    scale = BitConverter.ToUInt32(fourBytes, 0);
                }

                int randomInteger = (int)((min + (max - min)) * (scale / (double)uint.MaxValue));
                return randomInteger;
            }
        }
    }
}