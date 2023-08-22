using System.ComponentModel.DataAnnotations;

namespace LoginPractice.Models.Dto
{
    public class PersonalDetailsDTO
    {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        
    }
}
