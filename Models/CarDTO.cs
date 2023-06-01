using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CarTrader.Models
{
    public class CarDTO
    {
        public int Id { get; set; }

        [Display(Name = "Published at")]
        public DateTime PublishedAt { get; set; }

        [Display(Name = "Sold at")]
        public DateTime? SoldAt { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        [Range(0, 999999999)]
        public int Price { get; set; }
        [Range(1900, 2100)]
        public int Year { get; set; }
        [NotMapped]
        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
    }
}
