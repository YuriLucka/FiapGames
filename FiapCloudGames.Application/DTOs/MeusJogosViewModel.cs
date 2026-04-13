namespace FiapCloudGames.Application.DTOs
{
    /// <summary>
    /// Dados de retorno de um jogo presente na biblioteca do usuário autenticado.
    /// </summary>
    public record MeusJogosViewModel(
        /// <summary>Identificador único do jogo.</summary>
        int Id,
        /// <summary>Título do jogo.</summary>
        string Titulo,
        /// <summary>Descrição do jogo.</summary>
        string? Descricao,
        /// <summary>Preço do jogo.</summary>
        decimal Preco,
        /// <summary>Data de criação do jogo na plataforma.</summary>
        DateTime DataAquisicao
    );
}
