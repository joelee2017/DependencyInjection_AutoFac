using System;
using System.Reflection;
using System.Linq;
using Autofac.Builder;
using Autofac;

namespace ScanRegisterByInterface
{
    public interface IService
    {

    }
    public class CustomerService : IService
    {

    }

    public class OrderService : IService
    {

    }

    class Program
    {
        static IContainer provider;
        static void CompositionRoot()
        {
            var sc = new ContainerBuilder();
            //you can use Assembly.Load to load external dll.
            sc.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(a => 
                 !a.IsGenericType &&
                 !a.IsInterface &&
                 !a.IsAbstract &&
                 a.GetInterfaces().Any(c => c ==  typeof(IService)));
            provider = sc.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot();
            var obj = provider.Resolve<OrderService>();
            Console.WriteLine("Hello World!");
        }
    }
}
