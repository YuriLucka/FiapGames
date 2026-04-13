using FiapCloudGames.Application.DTOs;

namespace FiapCloudGames.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(CreateUserDto dto);
        Task<UserDto> UpdateAsync(int id, UpdateUserDto dto);
        Task DeleteAsync(int id);
        Task PromoteToAdminAsync(int id);
        Task<IEnumerable<MeusJogosViewModel>> ObterJogosDoUsuarioAsync(int usuarioId);
    }
}
