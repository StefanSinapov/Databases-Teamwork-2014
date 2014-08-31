namespace CarsFactory.ConsoleClient
{
    using System;
    using System.Linq;
    using Data;
    using Data.MongoDb;
    using Models;
    using Reports;

    public class EntryPoint
    {
        public static void Main()
        {
            TestMsSqlServer();
            //TestMongoDbSever();
        }

        private static void TestMongoDbSever()
        {
            Console.WriteLine("Connecting to MongoDb Server...");

            var mongoDb = new MongoDbDatabase();
            mongoDb.PrintCollection("Countries");


        }

        private static void LoadDataFromMongoDb(CarsFactoryContext context)
        {
            Console.WriteLine("Loading Data From MongoDB to MS SQL Server...");

            var mongoDb = new MongoDbDatabase();
            mongoDb.LoadAllDataToMsSql(context);
        }

        private static void TestMsSqlServer()
        {
            Console.WriteLine("Connecting to MS SQL Server...");

            var carsFactoryContext = new CarsFactoryContext();
            using (carsFactoryContext)
            {
                LoadDataFromMongoDb(carsFactoryContext);

                //TestAddData(carsFactoryContext);
                //TestReadData(carsFactoryContext);
                //TestRemoveData(carsFactoryContext);

                //JsonRepor.GenerateJsonReports(carsFactoryContext);
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
                Name = "Bulgaria"
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
