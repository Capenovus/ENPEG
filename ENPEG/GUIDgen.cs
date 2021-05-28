using System;
using System.Security.Cryptography;


namespace ENPEG
{
    public class GUIDgen
    {
        private static readonly char[] AvailableCharacters = {
            'A', 'B', 'C', 'D', 'E', 'F',
            '0', '1', '2', '3', '4', '5', '6', '7', '8', '9'
        };
        private static string RandomString(int length)
        {
            char[] identifier = new char[length];
            byte[] randomData = new byte[length];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomData);
            }

            for (int idx = 0; idx < identifier.Length; idx++)
            {
                int pos = randomData[idx] % AvailableCharacters.Length;
                identifier[idx] = AvailableCharacters[pos];
            }

            return new string(identifier);
        }
        public string Generate()
        {
            return $"{{{RandomString(8)}-{RandomString(4)}-{RandomString(4)}-{RandomString(4)}-{RandomString(12)}}}";
        }
    }
}
