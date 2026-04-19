using FiapCloudGames.Domain.Exceptions;

namespace FiapCloudGames.Domain.Entities
{
    public class Game
    {
        public int Id { get; }
        public string Title { get; private set; } = string.Empty;
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public string? ImageUrl { get; private set; }
        public string? Developer { get; private set; }
        public string? Category { get; private set; }
        public bool IsActive { get; private set; }
        public DateTime CreatedAt { get; private set; }

        protected Game() { }

        public Game(string title, decimal price, string? description = null,
                    string? imageUrl = null, string? developer = null, string? category = null)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Título do jogo é obrigatório.");
            if (price < 0)
                throw new DomainException("Preço não pode ser negativo.");

            Title = title;
            Price = price;
            Description = description;
            ImageUrl = imageUrl;
            Developer = developer;
            Category = category;
            IsActive = true;
            CreatedAt = DateTime.UtcNow;
        }

        public void Update(string title, decimal price, string? description,
                           string? imageUrl, string? developer, string? category)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new DomainException("Título do jogo é obrigatório.");
            if (price < 0)
                throw new DomainException("Preço não pode ser negativo.");

            Title = title;
            Price = price;
            Description = description;
            ImageUrl = imageUrl;
            Developer = developer;
            Category = category;
        }

        public void Activate() => IsActive = true;
        public void Deactivate() => IsActive = false;
    }
}
