namespace CarsFactory.MySQL.Data
{
    using Telerik.OpenAccess;

    using CarsFactory.MySQL.Models;
    using CarsFactory.Data;

    public class CarsFactoryMySQLData
    {
        private const string DatabaseName = "CarsFactoryDB";

        public static void GenerateProducts(CarsFactoryContext carsFactoryContext)
        {
            UpdateDatabase();
            SetData(carsFactoryContext);
        }

        private static void UpdateDatabase()
        {
            using (var context = new CarsFactoryMySQL())
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

        private static void SetData(CarsFactoryContext carsFactoryContext)
        {
            using (carsFactoryContext)
            {
                using (var db = new Models.CarsFactoryMySQL())
                {
                    var products = carsFactoryContext.Products;

                    foreach (var product in products)
                    {
                        var newProduct = new Product
                        {
                            ManufacturerName = product.Manufacturer.Name,
                            Model = product.Model,
                            HorsePower = product.HorsePower,
                            ReleaseYear = product.ReleaseYear,
                            Price = product.Price
                        };

                        db.Add(newProduct);
                    }

                    db.SaveChanges();
                }
            }
        }
    }
}