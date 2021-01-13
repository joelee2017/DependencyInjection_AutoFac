using Autofac;
using System;

namespace DelegateInjection
{
    public interface IService
    {
        Action<string> CallEvent { get; }
    }

    public class Service : IService
    {
        public Action<string> CallEvent { get; private set; }

        public Service(Action<string> callEvent)
        {
            CallEvent = callEvent;
        }
    }

    class Program
    {
        static IContainer provider;
        static void CompositeRoot()
        {
            var sc = new ContainerBuilder();
            sc.Register<Action<string>>(c => s => Console.WriteLine(s)).InstancePerLifetimeScope();
            sc.RegisterType<Service>().As<IService>().InstancePerLifetimeScope();
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
