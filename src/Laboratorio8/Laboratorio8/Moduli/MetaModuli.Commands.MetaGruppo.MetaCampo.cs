using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class AggiungiMetaCampoInMetaGruppoCommand
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
    }

    public class EliminaMetaCampoInMetaGruppoCommand
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task Handle(AggiungiMetaCampoInMetaGruppoCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.AggiungiMetaCampoInMetaGruppo(cmd.IdMetaGruppo, _dbContext);

            await _dbContext.SaveChangesAsync();
        }

        public async Task Handle(EliminaMetaCampoInMetaGruppoCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.EliminaMetaCampoInMetaGruppo(cmd.IdMetaGruppo, cmd.IdMetaCampo, _dbContext);

            await _dbContext.SaveChangesAsync();
        }
    }
}
