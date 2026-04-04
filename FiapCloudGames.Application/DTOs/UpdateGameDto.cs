using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiapCloudGames.Application.DTOs
{
    public record UpdateGameDto(
        string Title,
        decimal Price,
        string? Description
    );
}
