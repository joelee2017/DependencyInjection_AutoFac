using Autofac;
using System;

namespace GenericInject
{
    public interface IRepository<T>
    {

    }

    public class DataObject
    {

    }

    public class BaseRepository<T> : IRepository<T>
    {
    }

    public class TestGeneric
    {
        IRepository<DataObject> obj;
        public TestGeneric(IRepository<DataObject> obj)
        {
            this.obj = obj;
        }
    }

    class Program
    {
        static IContainer provider;
        static void CompositionRoot()
        {
            var sc = new ContainerBuilder();
            sc.RegisterType<TestGeneric>().SingleInstance();
            sc.RegisterGeneric(typeof(BaseRepository<>)).As(typeof(IRepository<>));
            provider = sc.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot();
            var obj = provider.Resolve<TestGeneric>();
        }
    }
}
