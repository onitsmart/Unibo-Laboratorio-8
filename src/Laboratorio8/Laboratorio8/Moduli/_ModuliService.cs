using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace Laboratorio8.Moduli
{
    public class ComandoSincronizzato
    {
        public Guid IdMetaModulo { get; set; }

        public Func<Task> Comando { get; set; }
    }

    public partial class ModuliService
    {
        private static readonly ConcurrentDictionary<Guid, SemaphoreSlim> semaphores = new ConcurrentDictionary<Guid, SemaphoreSlim>();
        private readonly ModuliDbContext _dbContext;

        public ModuliService(ModuliDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task Handle(ComandoSincronizzato cmd)
        {
            var semaphore = semaphores.GetOrAdd(cmd.IdMetaModulo, new SemaphoreSlim(1, 1));

            semaphore.Wait();

            try
            {
                await cmd.Comando();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                semaphore.Release();
            }
        }
    }
}
