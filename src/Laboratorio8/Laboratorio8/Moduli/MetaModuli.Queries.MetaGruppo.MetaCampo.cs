using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Laboratorio8.Moduli.MetaModuloInEditDTO.MetaGruppoDTO;

namespace Laboratorio8.Moduli
{
    public class AggiornamentoMetaCampoInMetaGruppoQuery
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
        public int IdMetaCampo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task<MetaCampoDTO> Query(AggiornamentoMetaCampoInMetaGruppoQuery qry)
        {
            return (await _dbContext.MetaModuli
                .Where(x => x.Id == qry.IdMetaModulo)
                .AsNoTracking()
                .FirstOrDefaultAsync())?.MetaGruppi
                                        .Where(x => x.Id == qry.IdMetaGruppo)
                                        .FirstOrDefault()?.MetaCampi
                                                          .Where(x => x.Id == qry.IdMetaCampo)
                                                          .Select(x => new MetaCampoDTO
                                                          {
                                                              Id = x.Id,
                                                              Etichetta = x.Etichetta,
                                                              Tipo = x.Tipo,
                                                              Dimensione = x.Dimensione,
                                                              Obbligatorio = x.Obbligatorio,
                                                              Ordine = x.Ordine
                                                          }).FirstOrDefault();
        }
    }
}
