using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class ModificaObbligatorietaMetaCampoCommand
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }

        public bool Obbligatorio { get; set; }
    }

    public partial class ModuliService
    {
        public async Task Handle(ModificaObbligatorietaMetaCampoCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.SetObbligatorietaMetaCampo(cmd.IdMetaGruppo, cmd.IdMetaCampo, cmd.Obbligatorio, _dbContext);

            await _dbContext.SaveChangesAsync();
        }
    }
}
