using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Linq;
using System;

namespace SqlServerGraphDb.ExtensionMethods
{
    public static class Extensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            var persistenceServicesAssembly = Assembly.Load("SqlServerGraphDb.Persistence");
            var persistenceServicesInterfaces = persistenceServicesAssembly
                .GetTypes()
                .Where(x =>
                    x.IsInterface &&
                    x.Name.EndsWith("Persistence"))
                .ToList();

            foreach (var intrface in persistenceServicesInterfaces)
            {
                AddImplementations(services, persistenceServicesAssembly, intrface);
            }
        }

        private static void AddImplementations(IServiceCollection services, Assembly assembly, Type intrface)
        {
            var implementingClasses = assembly
                .GetTypes()
                .Where(x => x.Name != intrface.Name && intrface.IsAssignableFrom(x))
                .ToList();

            if (implementingClasses.Count == 1)
            {
                services.AddTransient(intrface, implementingClasses.FirstOrDefault());
            }
            else
            {
                throw new System.Exception($"interface {intrface.Name} should have only single concrete implementation");
            }
        }

    }
}
