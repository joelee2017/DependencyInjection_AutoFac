using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;

namespace UseConfigurationFile
{
    public interface IService
    {

    }

    public class Service : IService
    {

    }

    class Program
    {
        static IContainer provider;
        static void CompositeRoot()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            var builder = new ContainerBuilder();
            builder.RegisterModule(new ConfigurationModule(config.Build()));
            provider = builder.Build();
        }

        static void Main(string[] args)
        {
            CompositeRoot();            
            var s = provider.Resolve<IService>();
            Console.WriteLine("Hello World!");
        }
    }
}
