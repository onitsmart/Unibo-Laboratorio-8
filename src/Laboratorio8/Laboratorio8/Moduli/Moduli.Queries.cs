using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class ModuliQuery
    {
        public Guid IdMetaModulo { get; set; }
    }

    public class ModuloInElencoDTO
    {
        public Guid Id { get; set; }
        public Guid IdMetaModulo { get; set; }
        public DateTime DataCompilazione { get; set; }

        public string Titolo { get; set; }
        public string Descrizione { get; set; }
    }

    public class DettaglioModuloCompilatoQuery
    {
        public Guid Id { get; set; }
    }

    public class DettaglioModuloCompilatoDTO
    {
        public Guid Id { get; set; }

        public Guid IdMetaModulo { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public DateTime DataCompilazione { get; set; }

        public IEnumerable<GruppoDTO> Gruppi { get; set; }

        public class GruppoDTO
        {
            public int Id { get; set; }

            public string Etichetta { get; set; }

            public int Ordine { get; set; }

            public IEnumerable<CampoDTO> Campi { get; set; }

            public class CampoDTO
            {
                public int Id { get; set; }

                public string Etichetta { get; set; }

                public TipologiaMetaCampo Tipo { get; set; }

                public bool Obbligatorio { get; set; }

                public int Ordine { get; set; }

                public DimensioneCampo Dimensione { get; set; }

                public string Contenuto { get; set; }
            }
        }
    }

    public partial class ModuliService
    {
        public async Task<IEnumerable<ModuloInElencoDTO>> Query(ModuliQuery qry)
        {
            return await _dbContext.Moduli
                .Where(x => x.IdMetaModulo == qry.IdMetaModulo)
                .Select(x => new ModuloInElencoDTO
                {
                    Id = x.Id,
                    Titolo = x.Titolo,
                    Descrizione = x.Descrizione,
                    IdMetaModulo = x.IdMetaModulo.Value,
                    DataCompilazione = x.DataCompilazione
                })
                .ToArrayAsync();
        }

        public async Task<int> Count(ModuliQuery qry)
        {
            return await _dbContext.Moduli
                .Where(x => x.IdMetaModulo == qry.IdMetaModulo)
                .CountAsync();
        }

        public async Task<DettaglioModuloCompilatoDTO> Query(DettaglioModuloCompilatoQuery qry)
        {
            var metaModulo = await _dbContext.Moduli
                .Where(x => x.Id == qry.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return new DettaglioModuloCompilatoDTO
            {
                Id = metaModulo.Id,
                Titolo = metaModulo.Titolo,
                Descrizione = metaModulo.Descrizione,
                DataCompilazione = metaModulo.DataCompilazione,
                Gruppi = metaModulo.Contenuto.Select(x => new DettaglioModuloCompilatoDTO.GruppoDTO
                {
                    Id = x.Id,
                    Etichetta = x.Etichetta,
                    Ordine = x.Ordine,
                    Campi = x.Campi?.Select(y => new DettaglioModuloCompilatoDTO.GruppoDTO.CampoDTO
                    {
                        Id = y.Id,
                        Tipo = y.Tipo,
                        Etichetta = y.Etichetta,
                        Ordine = y.Ordine,
                        Dimensione = y.Dimensione,
                        Obbligatorio = y.Obbligatorio,
                        Contenuto = y.Contenuto
                    }) ?? new List<DettaglioModuloCompilatoDTO.GruppoDTO.CampoDTO>()
                })
            };
        }
    }
}
