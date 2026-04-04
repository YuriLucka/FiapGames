using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGames.Application.DTOs
{
    public record GameDto(
      Guid Id,
      string Title,
      string? Description,
      decimal Price,
      bool IsActive,
      DateTime CreatedAt
  );
}
