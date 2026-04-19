namespace FiapCloudGames.Utils.DTOs
{
    public class GameDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? ImageUrl { get; set; }
        public string? Developer { get; set; }
        public string? Category { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt {  get; set; }

        public GameDto() { }

        public GameDto(Guid id, string title, string? description, decimal price, bool isActive, DateTime createdAt)
        {
            Id = id;
            Title = title;
            Description = description;
            Price = price;
            IsActive = isActive;
            CreatedAt = createdAt;
        }
    }
}
