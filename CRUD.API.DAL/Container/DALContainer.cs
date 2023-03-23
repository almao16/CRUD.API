using Autofac;
using CRUD.API.DAL.Common;
using CRUD.API.DAL.Connection;
using Microsoft.Extensions.Configuration;

namespace CRUD.API.DAL.Container
{
    public static class DALContainer
    {
        public static IContainer _container;
        public static IConfiguration _configuration;

        public static void Load(IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<DALSQLConnection>().As<IDALConnection<DALSQLConnection>>();
            builder.RegisterType<DALCommon>().As<IDALCommon>();

            _container = builder.Build();
            _configuration = configuration;
        }
    }
}
