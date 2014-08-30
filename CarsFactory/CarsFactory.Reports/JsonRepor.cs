namespace CarsFactory.Reports
{
    using System;
    using System.Linq;
    using Data;
    using Newtonsoft.Json;
    using System.IO;

    public static class JsonRepor
    {
        public static void GenerateJsonReports(CarsFactoryContext carsFactoryContext)
        {
           // var carsFactoryContext = new CarsFactoryContext();
            
            using (carsFactoryContext)
            {
                var productsList = carsFactoryContext.Products.SqlQuery("SELECT * FROM PRODUCTS").ToList();

                foreach (var item in productsList)
                {
                    int id = item.Id;
                    string jsonReportPath = @"..\..\..\Json-Reports\" + id + ".json";

                    StreamWriter writer = new StreamWriter(jsonReportPath, false);

                    string productAsJson = JsonConvert.SerializeObject(item, Formatting.Indented);

                    using (writer)
                    {
                        writer.WriteLine(productAsJson);
                    }
                }
            }
        }
    }
}