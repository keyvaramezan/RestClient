using System.ComponentModel.DataAnnotations;

namespace RestClient.Models
{
    public class AddProductDto
    {
        [Required(ErrorMessage ="Name is Required!")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Minimum character is 3 and maximum character 20 must be are letters ")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Price is Required!")]
        [Range(100, 1000, ErrorMessage = "The Range is between 100, 1000")]
        public int? Price { get; set; }

        [StringLength(30, MinimumLength = 0)]
        public string? Description { get; set; }
    }
}
