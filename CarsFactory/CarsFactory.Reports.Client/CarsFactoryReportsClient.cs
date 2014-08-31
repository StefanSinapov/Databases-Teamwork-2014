namespace CarsFactory.Reports.Client
{
    using Telerik.OpenAccess;

    using CarsFactory.Reports.Models;
    using CarsFactory.Data;

    public class CarsFactoryReportsClient
    {
        private const string DatabaseName = "CarsFactoryDB";

        public static void GenerateReports(CarsFactoryContext carsFactoryContext)
        {
            UpdateDatabase();
            SetDataToReports(carsFactoryContext);
        }

        private static void UpdateDatabase()
        {
            using (var context = new CarsFactoryReports())
            {
                var schemaHandler = context.GetSchemaHandler();

                EnsureDB(schemaHandler);
            }
        }

        private static void EnsureDB(ISchemaHandler schemaHandler)
        {
            if (schemaHandler.DatabaseExists())
            {
                schemaHandler.ExecuteDDLScript(string.Format("DROP DATABASE IF EXISTS {0};", DatabaseName));
            }

            schemaHandler.CreateDatabase();
            string script = schemaHandler.CreateDDLScript();

            if (!string.IsNullOrEmpty(script))
            {
                schemaHandler.ExecuteDDLScript(script);
            }
        }

        private static void SetDataToReports(CarsFactoryContext carsFactoryContext)
        {
            using (carsFactoryContext)
            {
                using (var db = new Models.CarsFactoryReports())
                {
                    var products = carsFactoryContext.Products;

                    foreach (var product in products)
                    {
                        var report = new Report
                        {
                            ManufacturerName = product.Manufacturer.Name,
                            Model = product.Model,
                            HorsePower = product.HorsePower,
                            ReleaseYear = product.ReleaseYear,
                            Price = product.Price
                        };

                        db.Add(report);
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}