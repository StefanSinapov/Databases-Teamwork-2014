namespace CarsFactory.Reports.Models
{
    using System.Linq;

    using Telerik.OpenAccess;
    using Telerik.OpenAccess.Metadata;

    public partial class CarsFactoryReports : OpenAccessContext, ICarsFactoryReportsUnitOfWork
    {
        private static string connectionStringName = @"CarsFactoryDB";

        private static BackendConfiguration backend = GetBackendConfiguration();

        private static MetadataSource metadataSource = new CarsFactoryReportsMetadataSource();

        public CarsFactoryReports()
            : base(connectionStringName, backend, metadataSource)
        {
        }

        public CarsFactoryReports(string connection)
            : base(connection, backend, metadataSource)
        {
        }

        public CarsFactoryReports(BackendConfiguration backendConfiguration)
            : base(connectionStringName, backendConfiguration, metadataSource)
        {
        }

        public CarsFactoryReports(string connection, MetadataSource metadataSource)
            : base(connection, backend, metadataSource)
        {
        }

        public CarsFactoryReports(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
            : base(connection, backendConfiguration, metadataSource)
        {
        }

        public IQueryable<Report> Reports
        {
            get
            {
                return this.GetAll<Report>();
            }
        }

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();

            backend.Runtime.AllowCascadeDelete = true;
            backend.Backend = "MySql";
            backend.ProviderName = "MySql.Data.MySqlClient";

            CustomizeBackendConfiguration(ref backend);

            return backend;
        }

        static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);
    }
}