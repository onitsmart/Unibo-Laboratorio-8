using System.Threading.Tasks;

namespace Laboratorio8.Infrastructure
{
    public interface IPublishDomainEvents
    {
        Task Publish(object evnt);
    }
}
