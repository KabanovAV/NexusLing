using NexusLing.Application.Interfaces;
using NexusLing.Domain.ValueObjects;
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
        private const string FormatMarker = "PBKDF2";

        /// <summary>
        /// Хэширование пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <returns>Возвращает хэшированный пароль</returns>
        public PasswordHash Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
            return PasswordHash.Create($"${FormatMarker}${Algorithm.Name}${Iterations}${Convert.ToHexString(salt)}${Convert.ToHexString(hash)}");
        }

        /// <summary>
        /// Верификаци пароля
        /// </summary>
        /// <param name="password">Пароль</param>
        /// <param name="passwordHash">Хэшированный пароль</param>
        /// <returns>Возвращает true если пароль прошел верификацию, false не прошел</returns>
        public bool Verify(string password, PasswordHash passwordHash)
        {
            if (string.IsNullOrWhiteSpace(passwordHash.Value))
                return false;

            string[] parts = passwordHash.Value.Split('$', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length != 5)
                return false;

            if (parts[0] != FormatMarker)
                return false;

            var algorithm = new HashAlgorithmName(parts[1]);
            var iterations = int.Parse(parts[2]);
            var salt = Convert.FromHexString(parts[3]);
            var hash = Convert.FromHexString(parts[4]);

            byte[] inputHash = Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, algorithm, hash.Length);

            return CryptographicOperations.FixedTimeEquals(hash, inputHash);
        }
    }
}
