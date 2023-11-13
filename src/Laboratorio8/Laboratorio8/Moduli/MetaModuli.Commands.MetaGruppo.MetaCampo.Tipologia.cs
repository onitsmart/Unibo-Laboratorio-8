using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class ModificaTipologiaMetaCampoCommand
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }

        public TipologiaMetaCampo Tipologia { get; set; }
    }

    public partial class ModuliService
    {
        public async Task Handle(ModificaTipologiaMetaCampoCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.SetTipologiaMetaCampo(cmd.IdMetaGruppo, cmd.IdMetaCampo, cmd.Tipologia, _dbContext);

            await _dbContext.SaveChangesAsync();
        }
    }
}
