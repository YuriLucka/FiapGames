namespace FiapCloudGames.Application.DTOs
{
    /// <summary>
    /// Dados para atualização de um jogo existente.
    /// </summary>
    public record UpdateGameDto(
        /// <summary>Título do jogo.</summary>
        string Title,
        /// <summary>Preço do jogo.</summary>
        decimal Price,
        /// <summary>Descrição do jogo.</summary>
        string? Description,
        /// <summary>Caminho para imagem do jogo.</summary>
        string? Image,
        /// <summary>Desenvolvedor do Jogo</summary>
        string? Developer,
        /// <summary>Categoria do Jogo</summary>
        string? Category
    );
}
