namespace CarsFactory.ConsoleClient
{
    using System;
    using System.Linq;

    using CarsFactory.MySQL.Data;
    using Data;
    using Data.MongoDb;
    using Loaders;
    using Models;
    using CarsFactory.Reports;
    using MySQL;
    using CarsFactory.Reports.Data;

    public class EntryPoint
    {
        public static void Main()
        {
            TestMsSqlServer();
        }

        private static void TestMsSqlServer()
        {
            var carsFactoryContext = new CarsFactoryContext();
            var collector = new ReportsDataCollector();

            using (carsFactoryContext)
            {
                //Console.WriteLine("Connecting to MS SQL Server...");
                //LoadDataFromMongoDb(carsFactoryContext);
                //ZipReadingAndImporting(carsFactoryContext);
                //TestAddData(carsFactoryContext);
                //TestReadData(carsFactoryContext);
                //TestRemoveData(carsFactoryContext);
                //
                //PdfReport.GeneratePdfReport(carsFactoryContext, collector);
                //JsonRepor.GenerateJsonReports(carsFactoryContext, collector);
                //XmlReport.GenerateXmlReports(carsFactoryContext, collector);
                //LoadXmlFileToSqlAndMongo(carsFactoryContext);
                //CarsFactoryMySQLData.GenerateProducts(carsFactoryContext);
                //ExcelReport.GenerateExcelReports();
            }
        }

        private static void LoadXmlFileToSqlAndMongo(CarsFactoryContext carsFactoryContext)
        {
            var mongoDb = new MongoDbDatabase();
            XmlLoader.LoadXmlFile(carsFactoryContext, mongoDb);
        }

        private static void LoadDataFromMongoDb(CarsFactoryContext context)
        {
            Console.WriteLine("Loading Data From MongoDB to MS SQL Server...");

            var mongoDb = new MongoDbDatabase();
            mongoDb.LoadAllDataToMsSql(context);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("     Done");
            Console.ResetColor();
        }

        private static void ZipReadingAndImporting(CarsFactoryContext carsFactoryContext)
        {
            var zipPath = @"..\..\..\Sample-Sales-Reports.zip";
            var unzipDirectory = @"..\..\..\TempExtractZip";

            var zipReader = new ZipImporter(zipPath, unzipDirectory);

            zipReader.ReadAndImport(carsFactoryContext);
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