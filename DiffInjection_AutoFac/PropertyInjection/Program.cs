using Autofac;
using System;

namespace PropertyInjection
{
    public interface IAccessService
    {

    }

    public class AccessService : IAccessService
    {

    }

    public interface IService
    {
        IAccessService AccessServ { get; set; }
    }

    public class Service1 : IService
    {
        public IAccessService AccessServ { get; set; }
    }

    class Program
    {
        static IContainer provider;
        static void CompositeRoot()
        {
            var sc = new ContainerBuilder();
            sc.RegisterType<AccessService>().As<IAccessService>();
            sc.Register(c =>
            {

                var s = new Service1();
                s.AccessServ = c.Resolve<IAccessService>();
                return s;
            }).As<IService>();
            provider = sc.Build();
        }

        static void Main(string[] args)
        {
            CompositeRoot();
            var s = provider.Resolve<IService>();
            Console.WriteLine("Hello World!");
        }
    }
}
