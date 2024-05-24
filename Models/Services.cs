using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Boocic.Models
{
    public class Services
    {

        public int Id { get; set; }


        [MinLength(5)]
        [MaxLength(15)]
        public string Category { get; set; }

        public string? PhotoUrl { get; set; }


        [NotMapped]
        public IFormFile ImgFile { get; set; }
    }
}
