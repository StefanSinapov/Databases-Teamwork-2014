namespace CarsFactory.Loaders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using CarsFactory.Models;
    using CarsFactory.Data;
    using Data.MongoDb;

    public class XmlLoader
    {
        private const string fileName = @"..\..\..\Xml-Data-Load\Manufacturers-Expenses.xml";
        private const string manufacturerTagName = "manufacturer";
        private const string expensesTagName = "expenses";
        private const string nameAttribute = "name";
        private const string monthAttribute = "month";

        public static void LoadXmlFile(CarsFactoryContext context, MongoDbDatabase mongoDb)
        {
            using (XmlReader reader = XmlReader.Create(fileName))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == manufacturerTagName))
                    {
                        var manufacturerName = reader.GetAttribute(nameAttribute);
                        AddManufacturer(context, mongoDb, manufacturerName);

                        while (reader.Read())
                        {
                            if ((reader.NodeType == XmlNodeType.Element) &&
                                (reader.Name == expensesTagName))
                            {
                                var monthName = reader.GetAttribute(monthAttribute);
                                AddMonth(context, mongoDb, monthName);

                                var expenseValue = decimal.Parse(reader.ReadElementString());

                                var manufacturer = context.Manufacturers.First(m => m.Name == manufacturerName);
                                var selectedMonth = context.Months.First(m => m.Name == monthName);

                                AddExpense(context, mongoDb, manufacturer, selectedMonth, expenseValue);
                            }

                            else if ((reader.NodeType == XmlNodeType.EndElement) &&
                                    (reader.Name == manufacturerTagName))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }

        private static void AddManufacturer(CarsFactoryContext context, MongoDbDatabase mongoDb, string manufacturerName)
        {
            var manufacturerExists = context.Manufacturers.Any(m => m.Name == manufacturerName);

            if (!manufacturerExists)
            {
                var manafacturer = new Manufacturer
                {
                    Name = manufacturerName
                };

                context.Manufacturers.Add(manafacturer);
                context.SaveChanges();
                mongoDb.UploadManufacturer(manafacturer);
            }
        }

        private static void AddMonth(CarsFactoryContext context, MongoDbDatabase mongoDb, string monthName)
        {
            var monthExists = context.Months.Any(m => m.Name == monthName);

            if (!monthExists)
            {
                var month = new Month
                {
                    Name = monthName
                };

                context.Months.Add(month);
                context.SaveChanges();
                mongoDb.UploadMonth(month);
            }
        }

        private static void AddExpense(CarsFactoryContext context, MongoDbDatabase mongoDb,
                    Manufacturer manufacturer,  Month selectedMonth, decimal expenseValue)
        {
            var expenseExists = context.Expenses.Any(e => e.ManafacturerId == manufacturer.Id &&
                                                                         e.MonthId == selectedMonth.MonthId &&
                                                                         e.Value == expenseValue);

            if (!expenseExists)
            {
                var expense = new Expense
                {
                    MonthId = selectedMonth.MonthId,
                    ManafacturerId = manufacturer.Id,
                    Value = expenseValue
                };

                context.Expenses.Add(expense);
                context.SaveChanges();
                mongoDb.UploadExpense(expense);
            }
        }
    }
}