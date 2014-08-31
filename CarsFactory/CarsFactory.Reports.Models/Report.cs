namespace CarsFactory.Reports.Models
{
    using System;

    public class Report
    {
        public int ID { get; set; }

        public string ManufacturerName { get; set; }

        public string Model { get; set; }

        public int HorsePower { get; set; }

        public DateTime ReleaseYear { get; set; }

        public decimal Price { get; set; }
    }
}