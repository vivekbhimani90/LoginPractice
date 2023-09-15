using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace LoginPractice.Models.Dto
{
    public class GenrateOtpResponseDTO
    {
        public LocalUser? User { get; set; }

        public string Otp { get; set; }
    }
}
