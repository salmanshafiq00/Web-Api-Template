using System.ComponentModel.DataAnnotations;

namespace WebApiUdemy.DTOs
{
    public class CreateOrEditProductDTO
    {
        [Required]
        [MinLength(6, ErrorMessage = "Minimum {1} character is required")]
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
    }
}
