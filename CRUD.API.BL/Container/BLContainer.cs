using Autofac;
using CRUD.API.BL.Clients;
using CRUD.API.BL.Products;
using CRUD.API.BL.Users;
using CRUD.API.DAL.Container;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRUD.API.BL.Container
{
    public static class BLContainer
    {
        public static IContainer _container;
        public static IConfiguration _configuration;

        public static void Load(IConfiguration configuration)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<BLUsers>().As<IBLUsers>();
            builder.RegisterType<BLProducts>().As<IBLProducts>();
            builder.RegisterType<BLClients>().As<IBLClients>();
            _container = builder.Build();
            _configuration = configuration;

            DALContainer.Load(configuration);
        }
    }
}
