using FiapCloudGames.Domain.Enums;

namespace FiapCloudGames.Application.DTOs
{
    /// <summary>
    /// Dados de retorno de um usuário da plataforma.
    /// </summary>
    public record UserDto(
        /// <summary>Identificador único do usuário.</summary>
        int Id,
        /// <summary>Nome completo do usuário.</summary>
        string Name,
        /// <summary>Email do usuário.</summary>
        string Email,
        /// <summary>Papel do usuário (User/Admin).</summary>
        UserRole Role,
        /// <summary>Data de criação do usuário.</summary>
        DateTime CreatedAt
    );
}
