using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class AggiungiMetaGruppoInMetaModuloCommand
    {
        public Guid IdMetaModulo { get; set; }
    }

    public class EliminaMetaGruppoInMetaModuloCommand
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task Handle(AggiungiMetaGruppoInMetaModuloCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.AggiungiMetaGruppo(_dbContext);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Handle(EliminaMetaGruppoInMetaModuloCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.EliminaMetaGruppo(cmd.IdMetaGruppo, _dbContext);

            await _dbContext.SaveChangesAsync();
        }
    }
}
