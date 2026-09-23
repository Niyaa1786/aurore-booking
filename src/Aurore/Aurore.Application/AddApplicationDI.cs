using Aurore.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Aurore.Application
{
    public static class AddApplicationDI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<IUnitOfWork>();

            services.Scan(scan =>
                scan.FromAssemblyOf<IUnitOfWork>().AddClasses(c => c.Where(c => c.Name.EndsWith("UseCase")), publicOnly: false)
                .AsSelf()
                .WithScopedLifetime());

            return services;
        }
    }
}
