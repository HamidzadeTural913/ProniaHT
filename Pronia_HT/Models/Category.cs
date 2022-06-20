using System.Collections.Generic;

namespace Pronia_HT.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PlantCategory> PlantCategories { get; set; } 

    }
}
