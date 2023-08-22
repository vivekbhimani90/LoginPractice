using LoginPractice.Models.Dto;

namespace LoginPractice.Store
{
    public class PersonalDetailsStore
    {
        public static List<PersonalDetailsDTO> personalDetails = new List<PersonalDetailsDTO>
            {
                  new PersonalDetailsDTO{Id = 1, Name = "Vivek", City = "Jamnagar", Country = "India"},
                  new PersonalDetailsDTO{Id = 2, Name = "Meet", City = "Ahmedabad", Country = "India"}
            };
    }
}
