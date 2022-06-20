using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pronia_HT.Models
{
    public class Plant
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string SkuCode { get; set; }
        [Required]
        public string Shipping { get; set; }
        [Required]
        public string Request { get; set; }
        [Required]
        public string Guarantee { get; set; }
        public int? ColorId { get; set; }
        public Color Color { get; set; }
        public int? SizeId { get; set; }
        public Size Size { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public List<PlantImage> PlantImages { get; set; }
        public List<PlantCategory> PlantCategories { get; set; }
        [NotMapped]
        public IFormFile MainImage { get; set; }
        [NotMapped]
        public List<IFormFile> AnotherImage { get; set; }
        [NotMapped]
        public List<int> ImageIds { get; set; }

    }
}
