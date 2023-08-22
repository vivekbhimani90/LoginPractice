using System.ComponentModel.DataAnnotations.Schema;

namespace LoginPractice.Models.Dto
{
    public class CreateFilesModel
    {
       
        public string PersonName { get; set; }
        public string City { get; set; }

        [NotMapped]
        public IFormFile UploadedFile { get; set; } 
       
    }
}
