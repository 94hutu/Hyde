using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hyde.External.Highwave;
using Autofac;
using System.Reflection;
using Hyde.Context;
using Hyde.Repository;
using System.Data.Entity;
using Hyde.Service;
using Hyde.External.Highwave.Models;
using Hyde.Domain.Model;
using Hyde.External.Sanfenqiu;
using System.Configuration;
namespace Hyde.Transfer
{
    class Program
    {
        static void Main(string[] args)
        {
            string apikey = ConfigurationManager.AppSettings["apikey"];

            var DAL = GetProvider<ISanfenqiu>();

            var stock = DAL.GetStock(apikey, 338679);   

            Console.ReadLine();
        }

        private static T GetProvider<T>()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<HydeContext>().As<DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<HighwaveOp>().As<IHighwave>().AsImplementedInterfaces();
            builder.RegisterType<SanfenqiuOp>().As<ISanfenqiu>().AsImplementedInterfaces();
            var container = builder.Build();

            return container.Resolve<T>();
        }
    }
}
