using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.Infrastructure
{
    public class Module : Autofac.Module
    {
        public string ConnectionString { get; set; }
        public string ConnectionSqlString { get; set; }
        public string DatabaseName { get; set; }
        public string BooksCollectionNameUser { get; set; }
        public string BooksCollectionNameLog { get; set; }
        public string booksCollectionNameStayHotel { get; set; }
        public string booksCollectionNameUserStay { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            //
            // Register all Types in MongoDataAccess namespace
            //
            builder.RegisterAssemblyTypes(typeof(InfrastructureException).Assembly)
                .Where(type => type.Namespace.Contains("Repositories"))
                .WithParameter("connectionString", ConnectionString)
                .WithParameter("connectionSqlString", ConnectionSqlString)
                .WithParameter("databaseName", DatabaseName)
                .WithParameter("booksCollectionNameUser", BooksCollectionNameUser)
                .WithParameter("booksCollectionNameLog", BooksCollectionNameLog)
                .WithParameter("booksCollectionNameStayHotel", booksCollectionNameStayHotel)
                .WithParameter("booksCollectionNameUserStay", booksCollectionNameUserStay)
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
