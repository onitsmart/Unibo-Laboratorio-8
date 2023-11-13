using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class AggiornamentoEtichettaMetaGruppoQuery
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task<string> Query(AggiornamentoEtichettaMetaGruppoQuery qry)
        {
            return (await _dbContext.MetaModuli
                .Where(x => x.Id == qry.IdMetaModulo)
                .FirstOrDefaultAsync())?.MetaGruppi
                                        .Where(x => x.Id == qry.IdMetaGruppo)
                                        .Select(x => x.Etichetta)
                                        .FirstOrDefault();
        }
    }
}
