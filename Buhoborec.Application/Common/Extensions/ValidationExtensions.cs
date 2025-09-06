using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Buhoborec.Application.Common.Extensions;

public static class ValidationExtensions
{
    public static IServiceCollection AddApplicationValidators(
        this IServiceCollection services,
        Assembly assembly)
    {
        var validatorTypes = assembly.GetTypes()
            .Where(t => !t.IsAbstract && !t.IsInterface &&
                        t.GetInterfaces().Any(i =>
                            i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IValidator<>)))
            .ToList();

        foreach (var validatorType in validatorTypes)
        {
            var interfaceType = validatorType.GetInterfaces()
                .First(i => i.IsGenericType &&
                            i.GetGenericTypeDefinition() == typeof(IValidator<>));

            services.AddScoped(interfaceType, validatorType);
        }

        return services;
    }
}
