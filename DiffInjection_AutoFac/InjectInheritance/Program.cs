using Autofac;
using System;

namespace InjectInheritance
{
    public abstract class BaseClass
    {
        public abstract void SayHello();
    }

    public class DerivedClass : BaseClass
    {
        public override void SayHello()
        {
            Console.WriteLine("i am Derived");
        }
    }

    public class ConsumerBaseClass
    {
        BaseClass inner;
        public ConsumerBaseClass(BaseClass inner)
        {
            this.inner = inner;
        }
    }

    public class ConsumerDerivedClass
    {
        BaseClass inner;
        public ConsumerDerivedClass(DerivedClass inner)
        {
            this.inner = inner;
        }
    }


    class Program
    {
        static IContainer provider;

        static void CompositionRoot()
        {
            var sc = new ContainerBuilder();
            //because we are register base class, not derivedclass, so if object require derivedclass, will faill.
            sc.RegisterType<DerivedClass>().As<BaseClass>().As<DerivedClass>().InstancePerLifetimeScope();
            
            sc.RegisterType<ConsumerBaseClass>();
            sc.RegisterType<ConsumerDerivedClass>();
            provider = sc.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot();            
            var obj1 = provider.Resolve<ConsumerBaseClass>();
            var obj2 = provider.Resolve<ConsumerDerivedClass>();
            Console.ReadLine();
        }
    }
}
