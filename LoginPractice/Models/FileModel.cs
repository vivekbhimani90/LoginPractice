using System.ComponentModel.DataAnnotations.Schema;

namespace LoginPractice.Models
{
    public class FileModel
    {
        public int Id { get; set; }
        public string PersonName { get; set; }
        public string City { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }

        [NotMapped]
        public IFormFile UploadedFile { get; set; } 
        public DateTime UploadDate { get; set; }
    }
}
