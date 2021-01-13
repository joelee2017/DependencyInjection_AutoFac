using Autofac;
using Autofac.Builder;
using System;
using System.Linq;
using System.Reflection;

namespace ScanRegisterByName
{
    public interface IService
    {

    }
    public class CustomerService:IService
    {

    }

    public class OrderService:IService
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
                 a.Name.EndsWith("Service"));

            //below will register the Service as IService
            //sc.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).Where(a =>
            //     !a.IsGenericType &&
            //     !a.IsInterface &&
            //     !a.IsAbstract &&
            //     a.Name.EndsWith("Service")).AsImplementedInterfaces();
            provider = sc.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot();
            var obj = provider.Resolve<OrderService>();
            //var obj = provider.Resolve<IService>();
            Console.WriteLine("Hello World!");
        }
    }
}
