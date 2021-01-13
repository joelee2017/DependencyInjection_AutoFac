using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace InjectMultipeSameInterface
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

    public class TestMultiple
    {
        IService orderService;
        IService customerService;
        public TestMultiple(IService orderService, IService customerService)
        {
            this.orderService = orderService;
            this.customerService = customerService; 
        }
    }
    class Program
    {
        static IContainer provider;
        static void CompositionRoot()
        {
            var sc = new ContainerBuilder();
            //you can use Assembly.Load to load external dll.
            sc.RegisterType<CustomerService>().As<IService>().Named<IService>("customers");
            sc.RegisterType<OrderService>().As<IService>().Named<IService>("orders");
            sc.RegisterType<TestMultiple>().WithParameter((p, c) => p.Name == "orderService", 
                (p, c) => c.ResolveNamed<IService>("orders")).WithParameter(
                (p, c) => p.Name == "customerService", (p, c) => c.ResolveNamed<IService>("customers"));
            provider = sc.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot();
            var obj = provider.Resolve<TestMultiple>();
            Console.WriteLine("Hello World!");
        }
    }
}
