namespace CarsFactory.Reports.Models
{
    using System.Collections.Generic;

    using Telerik.OpenAccess.Metadata;
    using Telerik.OpenAccess.Metadata.Fluent;

    public partial class CarsFactoryReportsMetadataSource : FluentMetadataSource
    {
        protected override IList<MappingConfiguration> PrepareMapping()
        {
            List<MappingConfiguration> configurations = new List<MappingConfiguration>();

            var reportMapping = new MappingConfiguration<Report>();

            reportMapping.MapType(report => new
            {
                ID = report.ID,
                ManufacturerName = report.ManufacturerName,
                Model = report.Model,
                HorsePower = report.HorsePower,
                ReleaseYear = report.ReleaseYear,
                Price = report.Price
            }).ToTable("Reports");

            reportMapping.HasProperty(r => r.ID).IsIdentity(KeyGenerator.Autoinc);

            configurations.Add(reportMapping);

            return configurations;
        }

        protected override void SetContainerSettings(MetadataContainer container)
        {
            container.Name = "CarsFactoryReports";
            container.DefaultNamespace = "CarsFactory.Reports.Models";
            container.NameGenerator.SourceStrategy = Telerik.OpenAccess.Metadata.NamingSourceStrategy.Property;
            container.NameGenerator.RemoveCamelCase = false;
        }
    }
}