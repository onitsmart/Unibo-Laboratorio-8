using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class AggiornamentoTipologiaMetaCampoQuery
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task<TipologiaMetaCampo?> Query(AggiornamentoTipologiaMetaCampoQuery qry)
        {
            return (await _dbContext.MetaModuli
                .Where(x => x.Id == qry.IdMetaModulo)
                .FirstOrDefaultAsync())?.MetaGruppi
                                        .Where(x => x.Id == qry.IdMetaGruppo)
                                        .FirstOrDefault()?.MetaCampi
                                                          .Where(x => x.Id == qry.IdMetaCampo)
                                                          .Select(x => x.Tipo)
                                                          .FirstOrDefault();
        }
    }
}
