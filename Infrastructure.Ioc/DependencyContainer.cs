using Core.Bus.Domain.Bus;
using Employer.Application.Interfaces;
using Employer.Application.Services;
using Employer.Domain.Events.CreateEmployer;
using Employer.Domain.Interfaces;
using Employer.Infrastructure.Data;
using Employer.Infrastructure.Data.Repositories;
using Infrastructure.Bus;
using Management.Application.Interfaces;
using Management.Application.Services;
using Management.Domain.Commands.CreateUser;
using Management.Domain.Interfaces;
using Management.Infrastructure.Data;
using Management.Infrastructure.Data.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Ioc
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            services.AddSingleton<IEventBus, RabbitMqBus>(sp =>
            {
                var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                return new RabbitMqBus(sp.GetService<IMediator>(), scopeFactory);
            });

            //Subscriptions
            services.AddTransient<EmployerCreatedEventHandler>();

            //Domain Commands
            services.AddTransient<IRequestHandler<CreateUserCommand, bool>, CreateUserCommandHandler>();

            //Domain Events
            services.AddTransient<IEventHandler<EmployerCreatedEvent>, EmployerCreatedEventHandler>();

            //Application Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IEmployerService, EmployerService>();

            //Data Layer
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<DataContext>();
            services.AddTransient<IEmployerRepository, EmployerRepository>();
            services.AddTransient<EmployerContext>();
        }
    }
}
