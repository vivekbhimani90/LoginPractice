using LoginPractice.Models;
using LoginPractice.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace LoginPractice.Repository.IRepository
{
    public interface IUserRepository
    {
        bool IsUniueUser(string username);

        Task<GenrateOtpResponseDTO> GenrateOtp(LoginRequestDTO loginRequestDTO);

        Task<LoginResponseDTO> VerifyOtp(string email);

        Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDTO);

        Task<LocalUser> Register(RegistrationRequestDTO registerationRequestDTO);

    }
}
