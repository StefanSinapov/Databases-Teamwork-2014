namespace CarsFactory.ConsoleClient
{
    using System;
    using System.Linq;
    using Data;
    using Models;

    class EntryPoint
    {
        static void Main()
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
                TestRemoveData(carsFactoryContext);
            }
        }

        private static void TestRemoveData(CarsFactoryContext carsFactoryContext)
        {
            var coutry = carsFactoryContext.Countries.FirstOrDefault();
            carsFactoryContext.Countries.Remove(coutry);
            var changes = carsFactoryContext.SaveChanges();
            Console.WriteLine("{0} row(s) removed.", changes);
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
