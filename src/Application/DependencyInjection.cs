using System.Reflection;
using FluentValidation;
using JobBet.Application.Common.Behaviours;
using JobBet.Application.Common.Interfaces;
using JobBet.Application.Common.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JobBet.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        services.AddTransient<IFreelancerService, FreelancerService>();
        services.AddTransient<IClientService, ClientService>();
        services.AddScoped<IJobService, JobService>();

        return services;
    }
}