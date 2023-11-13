using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using static Laboratorio8.Moduli.MetaModuloInEditDTO;

namespace Laboratorio8.Moduli
{
    public class AggiornamentoMetaGruppoInMetaModuloQuery
    {
        public Guid IdMetaModulo { get; set; }
        public int IdMetaGruppo { get; set; }
    }

    public partial class ModuliService
    {
        public async Task<MetaGruppoDTO> Query(AggiornamentoMetaGruppoInMetaModuloQuery qry)
        {
            return (await _dbContext.MetaModuli
                .Where(x => x.Id == qry.IdMetaModulo)
                .AsNoTracking()
                .FirstOrDefaultAsync())?.MetaGruppi
                                        .Where(x => x.Id == qry.IdMetaGruppo)
                                        .Select(x => new MetaGruppoDTO
                                        {
                                            Id = x.Id,
                                            Etichetta = x.Etichetta,
                                            Ordine = x.Ordine,
                                            MetaCampi = x.MetaCampi.Select(y => new MetaGruppoDTO.MetaCampoDTO
                                            {
                                                Id = y.Id,
                                                Tipo = y.Tipo,
                                                Etichetta = y.Etichetta,
                                                Ordine = y.Ordine,
                                                Dimensione = y.Dimensione,
                                                Obbligatorio = y.Obbligatorio
                                            })
                                        }).FirstOrDefault();
        }
    }
}
