using System.ComponentModel.DataAnnotations;

namespace CarManufacturers.Models
{
    public class Model
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Maker { get; set; }
        public Version Version { get; set; }
        public Type Type { get; set; }
    }

    public enum Type
    {
        Coupe, PickUp, MiniBus, Sedan, SUV
    }

    public enum Version
    {
        Automatic, Mechanic
    }

}
