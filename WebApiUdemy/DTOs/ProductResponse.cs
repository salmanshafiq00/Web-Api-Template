using WebApiUdemy.Model;

namespace WebApiUdemy.DTOs
{
    public class ProductResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string PhotoUrl { get; set; }

        // Navigational Property
        public Category Category { get; set; }
    }
}
