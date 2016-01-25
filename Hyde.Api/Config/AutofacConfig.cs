using System;
using System.Linq;
using Autofac;
using Autofac.Integration.WebApi;
using Hyde.Repository;
using Hyde.Context;
using System.Reflection;
using System.Web.Http;
using Hyde.Service;
using Hyde.External.Sanfenqiu;
using Hyde.External;
using Hyde.External.Highwave;

namespace Hyde.Api.Config
{
    public class AutofacConfig
    {
        /// <summary>
        /// Autofac依赖注入配置
        /// </summary>
        /// <param name="Config">Httpconfigration</param>
        public static void AutofacRegsiter(HttpConfiguration Config)
        {
            Assembly assembily = Assembly.GetExecutingAssembly();
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(assembily);
            builder.RegisterType<HydeContext>().As<System.Data.Entity.DbContext>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();
            builder.RegisterType<SanfenqiuOp>().As<ISanfenqiu>().AsImplementedInterfaces();
            builder.RegisterType<HighwaveOp>().As<IHighwave>().AsImplementedInterfaces();
            //业务类注册
            var baseType = typeof(IService);
            var serviceassembily = new Assembly[] { baseType.Assembly };

            builder.RegisterAssemblyTypes(serviceassembily).Where(t => baseType.IsAssignableFrom(t) && t != baseType).AsImplementedInterfaces();

            var container = builder.Build();

            Config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
        /// <summary>
        /// 注册所有实现了IService的服务
        /// </summary>
        /// <param name="types">当前程序集中的类型集合</param>
        /// <param name="builder">autofac构造器</param>
        private static void InitService(Type[] types, ContainerBuilder builder)
        {

            builder.RegisterTypes(types.Where(t => typeof(IService).IsAssignableFrom(t)).ToArray()).AsImplementedInterfaces().InstancePerLifetimeScope();

        }
    }
}