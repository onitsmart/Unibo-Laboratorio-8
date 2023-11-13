using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class AggiornamentoTitoloMetaModuloQuery
    {
        public Guid IdMetaModulo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task<string> Query(AggiornamentoTitoloMetaModuloQuery qry)
        {
            return await _dbContext.MetaModuli
                .Where(x => x.Id == qry.IdMetaModulo)
                .Select(x => x.Titolo)
                .FirstOrDefaultAsync();
        }
    }
}
