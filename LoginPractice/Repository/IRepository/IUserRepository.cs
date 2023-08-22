using LoginPractice.Models;
using LoginPractice.Models.Dto;

namespace LoginPractice.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniueUser(string username);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<LocalUser> Register(RegistrationRequestDTO registerationRequestDTO);

    }
}
