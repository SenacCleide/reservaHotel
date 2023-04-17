using Autofac;
using Microsoft.AspNetCore.Hosting;

namespace Hotel.WebApi
{
    public class Module : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .AsSelf()
                .InstancePerLifetimeScope();
        }
    }
}
