namespace CarsFactory.ConsoleClient
{
    using System;
    using System.Linq;

    using CarsFactory.Reports.Client;
    using Data;
    using Loaders;
    using Models;
    using Reports;

    public class EntryPoint
    {
        public static void Main()
        {
            TestMsSqlServer();
        }

        private static void TestMsSqlServer()
        {
            var carsFactoryContext = new CarsFactoryContext();
            using (carsFactoryContext)
            {
                TestAddData(carsFactoryContext);
                TestReadData(carsFactoryContext);
                //TestRemoveData(carsFactoryContext);

                JsonRepor.GenerateJsonReports(carsFactoryContext);
                XmlReport.GenerateXmlReports(carsFactoryContext);
                XmlLoader.LoadXmlFile(carsFactoryContext);
                //CarsFactoryReportsClient.GenerateReports(carsFactoryContext);
            }
        }

        private static void TestRemoveData(CarsFactoryContext carsFactoryContext)
        {
            var coutry = carsFactoryContext.Countries.FirstOrDefault();
            carsFactoryContext.Countries.Remove(coutry);
            try
            {
                var changes = carsFactoryContext.SaveChanges();
                Console.WriteLine("{0} row(s) removed.", changes);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void TestAddData(CarsFactoryContext carsFactoryContext)
        {
            var country = new Country
            {
                Name = "Sofia"
            };
            carsFactoryContext.Countries.Add(country);
            var changes = carsFactoryContext.SaveChanges();
            Console.WriteLine("{0} row(s) added.", changes);
        }

        private static void TestReadData(CarsFactoryContext carsFactoryContext)
        {
            var countries = carsFactoryContext.Countries;

            foreach (var country in countries)
            {
                Console.WriteLine("Id: {0}, Name: {1}", country.Id, country.Name);
            }
        }
    }
}