using Laboratorio8.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class ModuliDbContext : DbContext
    {
        private readonly IPublishDomainEvents _publisher;

        public ModuliDbContext(DbContextOptions<ModuliDbContext> options) : base(options) { }

        public ModuliDbContext(DbContextOptions<ModuliDbContext> options, IPublishDomainEvents publisher) : base(options)
        {
            _publisher = publisher;
        }

        public DbSet<MetaModulo> MetaModuli { get; set; }
        public DbSet<Modulo> Moduli { get; set; }

        public void Raise<T>(T evnt) where T : IEvent
        {
            _events.Add(evnt);
        }

        List<IEvent> _events = new List<IEvent>();

        public override int SaveChanges()
        {
            var written = base.SaveChanges();

            if (_publisher != null)
            {
                foreach (var e in _events)
                {
                    _publisher.Publish(e);
                }
            }

            _events.Clear();

            return written;
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var written = await base.SaveChangesAsync(cancellationToken);

            if (_publisher != null)
            {
                foreach (var e in _events)
                {
                    await _publisher.Publish(e);
                }
            }

            _events.Clear();

            return written;
        }
    }
}
