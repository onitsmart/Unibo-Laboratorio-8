using Laboratorio8.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace Laboratorio8.Web
{
    public class WebContainer
    {
        public static void RegisterTypes(IServiceCollection container)
        {
            container.AddScoped<IPublishDomainEvents, SignalR.SignalrPublishDomainEvents>();
        }
    }
}
