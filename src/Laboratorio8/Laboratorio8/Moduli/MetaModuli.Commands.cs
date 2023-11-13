using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class AggiungiNuovoMetaModuloCommand{ }

    public class EliminaMetaModuloCommand
    {
        public Guid IdMetaModulo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task<Guid> Handle(AggiungiNuovoMetaModuloCommand cmd)
        {
            var metaModulo = new MetaModulo
            {
                Id = Guid.NewGuid(),
                DataCreazione = DateTime.Now,
                DataModifica = DateTime.Now
            };

            _dbContext.MetaModuli.Add(metaModulo);
            await _dbContext.SaveChangesAsync();

            return metaModulo.Id;
        }

        public async Task Handle(EliminaMetaModuloCommand cmd)
        {
            var metaModulo = await _dbContext.MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .FirstOrDefaultAsync();

            _dbContext.MetaModuli.Remove(metaModulo);

            await _dbContext.SaveChangesAsync();
        }
    }
}
