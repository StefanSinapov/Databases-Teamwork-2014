namespace CarsFactory.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Model { get; set; }

        public int HorsePower { get; set; }

        public DateTime ReleaseYear { get; set; }

        public decimal Price { get; set; }

        [ForeignKey("Manufacturer")]
        public int ManufacturerId { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public virtual EngineType EngineType { get; set; }

        public virtual CarType CarType { get; set; }
    }
}
