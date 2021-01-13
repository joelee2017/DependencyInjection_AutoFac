using Autofac;
using System;

namespace InjectLimitProvider
{
    public interface IService1
    {
    }

    public interface IService2
    {
    }

    public class Service1:IService1
    {

    }

    public class Service2:IService2
    {

    }

    public class LimitServiceProvider<T1, T2>
    {
        IComponentContext provider;
        public T1 GetService1()
        {
            return provider.Resolve<T1>();
        }

        public T2 GetService2()
        {
            return provider.Resolve<T2>();
        }

        public LimitServiceProvider(IComponentContext provider)
        {
            this.provider = provider;
        }
    }

    public class TestLimit
    {
        LimitServiceProvider<IService1, IService2> provider;
        Lazy<IService1> service1;
        Lazy<IService2> service2;
        public void CallLimit(int type)
        {
            if (type == 1)
            {
                Console.WriteLine(service1.GetType());
            }
            else
            {
                Console.WriteLine(service2.GetType());
            }
        }

        public TestLimit(LimitServiceProvider<IService1, IService2> provider)
        {
            this.provider = provider;
            service1 = new Lazy<IService1>(() => this.provider.GetService1());
            service2 = new Lazy<IService2>(() => this.provider.GetService2());
        }
    }
    class Program
    {
        static IContainer provider;

        static void CompositeRoot()
        {
            var sc = new ContainerBuilder();
            sc.RegisterType<Service1>().As<IService1>().SingleInstance();
            sc.RegisterType<Service2>().As<IService2>().SingleInstance();
            sc.Register(c => new LimitServiceProvider<IService1, IService2>(c));
            sc.RegisterType<TestLimit>();
            provider = sc.Build();
        }

        static void Main(string[] args)
        {
            CompositeRoot();
            var obj = provider.Resolve<TestLimit>();
            obj.CallLimit(2);
            Console.ReadLine();
        }
    }
}
