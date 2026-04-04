using FiapCloudGames.Domain.Enums;
using FiapCloudGames.Domain.ValueObjects;

namespace FiapCloudGames.Domain.Entities
{
    internal class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Email Email { get; private set; }
        public string PasswordHash { get; private set; }
        public UserRole Role { get; private set; }
        public List<Game> AcquiredGames { get; private set; }

        public User(string name, Email email, string passwordHash, UserRole role)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome é obrigatório.", nameof(name));

            if (passwordHash == null || !IsValidPassword(passwordHash))
                throw new ArgumentException("Senha inválida. Deve conter no mínimo 8 caracteres, letras, números e caracteres especiais.", nameof(passwordHash));

            Id = Guid.NewGuid();
            Name = name;
            Email = email ?? throw new ArgumentNullException(nameof(email));
            PasswordHash = passwordHash;
            Role = role;
            AcquiredGames = new List<Game>();
        }

        private bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;
            bool hasLetter = false, hasDigit = false, hasSpecial = false;
            foreach (var c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (!char.IsWhiteSpace(c)) hasSpecial = true;
            }
            return hasLetter && hasDigit && hasSpecial;
        }
    }
}