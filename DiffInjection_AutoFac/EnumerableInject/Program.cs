using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnumerableInject
{
    public interface IInjection
    { 
    }

    public class Injection : IInjection
    {
    }

    public class Injection1 : IInjection
    {
    }

    public class CollectionInjection
    {

        IEnumerable<IInjection> objs;

        public CollectionInjection(IEnumerable<IInjection> objs)
        {
            this.objs = objs;
        }
    }

    class Program
    {
        static IContainer provider;
        static void CompositionRoot()
        {
            var sc = new ContainerBuilder();
            //collection injection.
            sc.RegisterType<Injection>().As<IInjection>();
            sc.RegisterType<Injection1>().As<IInjection>();
            sc.RegisterType<CollectionInjection>();

            provider = sc.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot();
            var cols = provider.Resolve<CollectionInjection>();
            Console.ReadLine();
        }
    }
}
