using System.ComponentModel.DataAnnotations;

namespace LoginPractice.Models.Dto
{
    public class PersonalDetailsUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string City { get; set; }
        public string Country { get; set; }
    }
}
