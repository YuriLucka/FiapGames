using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Utils.DTOs
{
    public class UserDto
    {
        public Guid Id;
        public string Name;
        public string Email;
        public UserRole Role;
        public DateTime CreatedAt;

        public UserDto() { }

        public UserDto(Guid id, string name, string email, UserRole role, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Email = email;
            Role = role;
            CreatedAt = createdAt;
        }
    }
}
