using Laboratorio8.Infrastructure;
using Laboratorio8.Moduli;
using Microsoft.Extensions.DependencyInjection;

namespace Laboratorio8
{
    public class Container
    {
        public static void RegisterTypes(IServiceCollection container)
        {
            container.AddScoped<ModuliService>();
        }
    }
}
