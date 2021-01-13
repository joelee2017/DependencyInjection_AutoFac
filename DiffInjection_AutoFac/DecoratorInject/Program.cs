using Autofac;
using System;

namespace DecoratorInject
{

    public interface IValueObject
    {

    }

    public class ValueObject : IValueObject
    {

    }

    public class ValueObjectDecorator : IValueObject
    {
        IValueObject obj;
        public ValueObjectDecorator(IValueObject obj)
        {
            this.obj = obj;
        }
    }

    public class TestDecorator
    {
        IValueObject obj;
        public TestDecorator(IValueObject obj)
        {
            this.obj = obj;
        }
    }

    class Program
    {
        static IContainer provider;
        static void CompositionRoot()
        {
            var cb = new ContainerBuilder();
            cb.RegisterType<ValueObject>().As<IValueObject>();
            cb.RegisterDecorator<ValueObjectDecorator, IValueObject>();
            cb.RegisterType<TestDecorator>();
            provider = cb.Build();
        }

        static void Main(string[] args)
        {
            CompositionRoot();
            var obj = provider.Resolve<TestDecorator>();
            Console.ReadLine();
        }
    }
}
