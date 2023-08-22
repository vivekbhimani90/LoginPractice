using LoginPractice.Models;

namespace LoginPractice.Repository.IRepository
{
    public interface IPersonalDetailsRepository : IRepository<PersonalDetails>
    {
        Task<PersonalDetails> UpdateAsync(PersonalDetails personalDetails);
    }
}
