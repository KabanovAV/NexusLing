using NexusLing.Application.Interfaces;
using System.Security.Cryptography;

namespace NexusLing.Infrastructure.Authentications
{
    /// <summary>
    /// Интерфейс сервиса хэширования пароля
    /// </summary>
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 500000;

        private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA512;

        /// <summary>
        /// Хэширование пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Возвращает хэшированный пароль</returns>
        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            return $"{Convert.ToHexString(hash)}-{Convert.ToHexString(salt)}";
        }

        /// <summary>
        /// Верификаци пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="passwordHash">Хэшированный пароль</param>
        /// <returns>Возвращает true если пароль прошел верификацию, false не прошел</returns>
        public bool Verify(string password, string passwordHash)
        {
            string[] parts = passwordHash.Split('-');
            byte[] hash = Convert.FromHexString(parts[0]);
            byte[] salt = Convert.FromHexString(parts[1]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
