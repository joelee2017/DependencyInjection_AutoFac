using Autofac;
using System;

namespace MethodInjection
{
    public interface IHello
    {
        void SayHello();
    }

    public interface IService
    {
        void CallDependency();
    }

    public class Service : IService
    {
        IHello helloservice;
        public void SetCallDependency(IHello helloService)
        {
            this.helloservice = helloService;
        }

        public void CallDependency()
        {
            if (helloservice == null)
                throw new ArgumentException("hello service not available.");
            helloservice.SayHello();
        }
    }

    public class Hello : IHello
    {
        public void SayHello()
        {
            Console.WriteLine("hello");
        }
    }


    class Program
    {
        static IContainer provider;
        static void CompositionRoot()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<Hello>().As<IHello>();
            cb.Register(c =>
            {
                var s = new Service();
                s.SetCallDependency(c.Resolve<IHello>());
                return s;
            }).As<IService>();
            provider = cb.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot();
            var obj = provider.Resolve<IService>();
            obj.CallDependency();
            Console.ReadLine();
        }
    }
}
