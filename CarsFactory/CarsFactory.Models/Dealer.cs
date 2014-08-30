namespace CarsFactory.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Dealer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [ForeignKey("Address")]
        public int AddressId { get; set; }

        public virtual Address Address { get; set; }
    }
}
