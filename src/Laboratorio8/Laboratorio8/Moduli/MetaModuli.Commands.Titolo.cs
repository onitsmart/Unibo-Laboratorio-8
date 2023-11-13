using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class ModificaTitoloMetaModuloCommand
    {
        public Guid IdMetaModulo { get; set; }

        public string Titolo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task Handle(ModificaTitoloMetaModuloCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.SetTitolo(cmd.Titolo, _dbContext);

            await _dbContext.SaveChangesAsync();
        }
    }
}
