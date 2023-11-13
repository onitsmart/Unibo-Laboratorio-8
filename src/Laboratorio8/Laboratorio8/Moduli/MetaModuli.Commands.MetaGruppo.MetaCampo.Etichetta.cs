using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class ModificaEtichettaMetaCampoCommand
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }

        public string Etichetta { get; set; }
    }

    public partial class ModuliService
    {
        public async Task Handle(ModificaEtichettaMetaCampoCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.SetEtichettaMetaCampo(cmd.IdMetaGruppo, cmd.IdMetaCampo, cmd.Etichetta, _dbContext);

            await _dbContext.SaveChangesAsync();
        }
    }
}
