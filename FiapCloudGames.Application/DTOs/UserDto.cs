using FiapCloudGames.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGames.Application.DTOs
{
    public record UserDto(
        Guid Id,
        string Name,
        string Email,
        UserRole Role,
        DateTime CreatedAt
    );
}
