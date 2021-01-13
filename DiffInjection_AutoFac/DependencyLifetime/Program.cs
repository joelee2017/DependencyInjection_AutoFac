using Autofac;
using System;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;

namespace DependencyLifetime
{
    public class MyService : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~MyService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            Console.WriteLine("object dispose.");
            GC.SuppressFinalize(this);
        }
    }

    public class MyServiceHold
    {
        MyService service;
        public MyServiceHold(MyService service)
        {
            this.service = service;
        }
    }

    class Program
    {
        static IContainer provider;
        static void CompositeRoot()
        {
            var sc = new ContainerBuilder();
            sc.RegisterType<MyService>().InstancePerDependency();
            //sc.RegisterType<MyService>().InstancePerLifetimeScope();
            //sc.RegisterType<MyService>().SingleInstance();
            sc.RegisterType<MyServiceHold>().InstancePerLifetimeScope();
            provider = sc.Build();
        }

        static void TestScopeWithTransientWrong()
        {
            using (var scope = provider.BeginLifetimeScope())
            {
                //it's use outside provider, not scope provider.
                var obj = provider.Resolve<MyServiceHold>();
            }
        }

        static void TestScopeWithTransient()
        {
            using(var scope = provider.BeginLifetimeScope())
            {
                //the MyService will dispose when register with Transient or Scope but not Singleton.
                var obj = scope.Resolve<MyServiceHold>();
            }
        }

        static void Main(string[] args)
        {
            CompositeRoot();
            TestScopeWithTransient();
            Console.ReadLine();
        }
    }
}
