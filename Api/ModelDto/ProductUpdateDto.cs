using System.ComponentModel.DataAnnotations;

namespace Api.ModelDto
{
    public class ProductUpdateDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public string SpetialTag { get; set; }
        public string Category { get; set; }
        [Range(1, 10000000)]
        public decimal Price { get; set; }
        public string Image { get; set; }
    }
}
