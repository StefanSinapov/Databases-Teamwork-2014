namespace CarsFactory.Data.MongoDb
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using Models;
    using MongoDB.Bson;
    using MongoDB.Driver;

    public class MongoDbDatabase
    {
        private CarsFactoryContext context;

        private readonly string connectionString;
        private readonly string databaseName;

        private const string DefaultDatabaseName = "carsfactory";

        public MongoDbDatabase()
            : this(ConfigurationManager.ConnectionStrings["CarsFactoryMongo"].ConnectionString,
            DefaultDatabaseName)
        { }

        public MongoDbDatabase(string connectionString, string databaseName)
        {
            this.connectionString = connectionString;
            this.databaseName = databaseName;
        }

        public void PrintCollection(string collectionName)
        {
            var database = this.GetDatabase(this.databaseName);
            var collection = database.GetCollection(collectionName);

            foreach (var item in collection.FindAll())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine();
        }

        public void LoadAllDataToMsSql(CarsFactoryContext context)
        {
            this.context = context;

            LoadCountries();
            LoadTowns();
            LoadAddresses();
            LoadManufacturers();
            LoadCarTypes();
            LoadEngineTypes();
            LoadDealers();
            LoadProducts();

            context.SaveChanges();
        }

        private void LoadEngineTypes()
        {
            if (this.context.EngineTypes.Any())
            {
                return;
            }

            var engineTypes = GetItemsFromCollection("EngineTypes");
            foreach (var engineType in engineTypes)
            {
                this.context.EngineTypes.Add(
                    new EngineType
                    {
                        Id = engineType["EngineTypeId"].ToInt32(),
                        Name = engineType["Name"].ToString()
                    });
            }
        }

        private void LoadCarTypes()
        {
            if (this.context.CarTypes.Any())
            {
                return;
            }

            var carTypes = GetItemsFromCollection("CarTypes");
            foreach (var carType in carTypes)
            {
                this.context.CarTypes.Add(
                    new CarType
                    {
                        Id = carType["CarTypeId"].ToInt32(),
                        Name = carType["Name"].ToString()
                    });
            }
        }

        private void LoadProducts()
        {
            if (this.context.Products.Any())
            {
                return;
            }

            var products = GetItemsFromCollection("Products");
            foreach (var product in products)
            {
                this.context.Products.Add(
                    new Product
                    {
                        Id = product["ProductId"].ToInt32(),
                        Model = product["Model"].ToString(),
                        HorsePower = product["HorsePower"].ToInt32(),
                        ManufacturerId = product["ManufacturerId"].ToInt32(),
                        CarTypeId = product["CarTypeId"].ToInt32(),
                        EngineTypeId = product["EngineTypeId"].ToInt32(),
                        ReleaseYear = product["ReleaseYear"].ToInt32(),
                        Price = (decimal)product["Price"]
                    });
            }
        }

        private void LoadDealers()
        {
            if (this.context.Dealers.Any())
            {
                return;
            }

            var dealers = GetItemsFromCollection("Dealers");
            foreach (var dealer in dealers)
            {
                this.context.Dealers.Add(
                    new Dealer
                    {
                        Id = dealer["DealerId"].ToInt32(),
                        Name = dealer["Name"].ToString(),
                        AddressId = dealer["AddressId"].ToInt32()
                    });
            }
        }

        private void LoadManufacturers()
        {
            if (this.context.Manufacturers.Any())
            {
                return;
            }

            var manufacturers = GetItemsFromCollection("Manufacturers");
            foreach (var manufacturer in manufacturers)
            {
                this.context.Manufacturers.Add(
                    new Manufacturer
                    {
                        Id = manufacturer["ManufacturerId"].ToInt32(),
                        Name = manufacturer["Name"].ToString()
                    });
            }
        }

        private void LoadAddresses()
        {
            if (this.context.Addresses.Any())
            {
                return;
            }

            var addresses = GetItemsFromCollection("Addresses");

            foreach (var address in addresses)
            {
                this.context.Addresses.Add(
                    new Address
                    {
                        Id = address["AddressId"].ToInt32(),
                        AddressText = address["AddressText"].ToString(),
                        TownId = address["TownId"].ToInt32()
                    });
            }
        }

        private void LoadTowns()
        {
            if (this.context.Towns.Any())
            {
                return;
            }

            var towns = GetItemsFromCollection("Towns");
            foreach (var town in towns)
            {
                this.context.Towns.Add(
                    new Town
                    {
                        Id = town["TownId"].ToInt32(),
                        Name = town["Name"].ToString(),
                        CountryId = town["CountryId"].ToInt32()
                    });
            }
        }

        private void LoadCountries()
        {
            if (this.context.Countries.Any())
            {
                return;
            }

            var countries = GetItemsFromCollection("Countries");

            foreach (var country in countries)
            {
                this.context.Countries.Add(
                    new Country
                    {
                        Id = country["CountryId"].ToInt32(),
                        Name = country["Name"].ToString()
                    });
            }
        }

        private IEnumerable<BsonDocument> GetItemsFromCollection(string collectionName)
        {
            var database = this.GetDatabase(this.databaseName);
            var collection = database.GetCollection(collectionName);

            return collection.FindAll();
        }

        private MongoDatabase GetDatabase(string databaseName)
        {
            var mongoClient = new MongoClient(this.connectionString);
            var mongoServer = mongoClient.GetServer();

            var database = mongoServer.GetDatabase(databaseName);
            return database;
        }

    }
}