using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class ModificaDescrizioneMetaModuloCommand
    {
        public Guid IdMetaModulo { get; set; }

        public string Descrizione { get; set; }
    }

    public partial class ModuliService
    {
        public async Task Handle(ModificaDescrizioneMetaModuloCommand cmd)
        {
            var modulo = await _dbContext
                .MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .SingleOrDefaultAsync();

            if (modulo == null)
                throw new ServicesExceptions("Impossibile modificare il modulo");

            modulo.SetDescrizione(cmd.Descrizione, _dbContext);

            await _dbContext.SaveChangesAsync();
        }
    }
}
