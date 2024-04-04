using System.ComponentModel.DataAnnotations;

namespace CarManufacturers.Models
{
    public class CarMaker
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Since { get; set; }

    }
}
