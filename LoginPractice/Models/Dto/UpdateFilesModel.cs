using System.ComponentModel.DataAnnotations.Schema;

namespace LoginPractice.Models.Dto
{
    public class UpdateFilesModel
    {
       
        public string Id { get; set; }
        public string PersonName { get; set; }
        public string City { get; set; }

        [NotMapped]
        public IFormFile UploadedFile { get; set; } 
       
    }
}
