namespace Pronia_HT.Models
{
    public class PlantCategory
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int? PlantId { get; set; }
        public Plant Plant { get; set; }
    }
}
