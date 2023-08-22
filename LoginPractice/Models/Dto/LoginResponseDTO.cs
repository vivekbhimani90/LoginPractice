using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LoginPractice.Models.Dto
{
    public class LoginResponseDTO
    {
        public LocalUser? User { get; set; }

        public string? Token { get; set; }
    }
}
