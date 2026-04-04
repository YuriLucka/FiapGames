using System;

namespace FiapCloudGames.Domain.Entities
{
    internal class Game
    {
        public Guid Id { get; private set; }
        public string Title { get; private set; }
        public string? Description { get; private set; }
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; }

        public Game(string title, decimal price, string? description = null, bool isActive = true)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException("Título é obrigatório.", nameof(title));
            if (price <= 0)
                throw new ArgumentException("Preço deve ser maior que zero.", nameof(price));

            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            Price = price;
            IsActive = isActive;
        }

        public void SetActive(bool active)
        {
            IsActive = active;
        }
    }
}
