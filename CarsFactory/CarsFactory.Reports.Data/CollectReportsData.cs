namespace CarsFactory.Reports.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CarsFactory.Data;
    using CarsFactory.Models;

    public static class CollectReportsData
    {
        public static ICollection<Product> CollectDataForJsonReport(CarsFactoryContext carsFactoryContext)
        {
            var productsList = carsFactoryContext.Products.SqlQuery("SELECT * FROM PRODUCTS").ToList();

            return productsList;
        }

        public static dynamic CollectDataForXmlReport(CarsFactoryContext carsFactoryContext)
        {
            var dealers = carsFactoryContext.Dealers.Select(d => new
            {
                Name = d.Name,
                SalesReports = d.SalesReports
            }).ToList();

            return dealers;
        }

        public static ICollection<Product> CollectDataForPdfReport(CarsFactoryContext carsFactoryContext)
        {
            return null;
        }

    }
}