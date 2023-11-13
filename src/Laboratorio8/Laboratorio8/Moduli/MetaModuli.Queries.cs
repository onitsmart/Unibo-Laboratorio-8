using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class MetaModuliElencoQuery
    {
        public string Filtro { get; set; }
    }

    public class MetaModuloInElencoDTO
    {
        public Guid Id { get; set; }

        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public DateTime DataModifica { get; set; }
    }

    public class MetaModuloInEditQuery
    {
        public Guid Id { get; set; }
    }

    public class MetaModuloInEditDTO
    {
        public Guid Id { get; set; }

        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public DateTime DataModifica { get; set; }

        public IEnumerable<MetaGruppoDTO> MetaGruppi { get; set; }

        public class MetaGruppoDTO
        {
            public int Id { get; set; }

            public string Etichetta { get; set; }

            public int Ordine { get; set; }

            public IEnumerable<MetaCampoDTO> MetaCampi { get; set; }

            public class MetaCampoDTO
            {
                public int Id { get; set; }

                public string Etichetta { get; set; }

                public TipologiaMetaCampo Tipo { get; set; }

                public bool Obbligatorio { get; set; }

                public int Ordine { get; set; }

                public DimensioneCampo Dimensione { get; set; }
            }
        }
    }

    public class MetaModuloPerCompilazioneQuery
    {
        public Guid Id { get; set; }
    }

    public class MetaModuloPerCompilazioneDTO
    {
        public Guid Id { get; set; }

        public string Titolo { get; set; }
        public string Descrizione { get; set; }

        public DateTime DataModifica { get; set; }

        public IEnumerable<MetaGruppoDTO> MetaGruppi { get; set; }

        public class MetaGruppoDTO
        {
            public int Id { get; set; }

            public string Etichetta { get; set; }

            public int Ordine { get; set; }

            public IEnumerable<MetaCampoDTO> MetaCampi { get; set; }

            public class MetaCampoDTO
            {
                public int Id { get; set; }

                public string Etichetta { get; set; }

                public TipologiaMetaCampo Tipo { get; set; }

                public bool Obbligatorio { get; set; }

                public int Ordine { get; set; }

                public DimensioneCampo Dimensione { get; set; }
            }
        }
    }

    public class MetaModuloPerElencoModuliQuery
    {
        public Guid Id { get; set; }
    }

    public class MetaModuloPerElencoModuliDTO
    {
        public Guid Id { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
    }

    public partial class ModuliService
    {
        public IQueryable<MetaModulo> IQueryableMetaModuli(MetaModuliElencoQuery qry)
        {
            var queryable = _dbContext.MetaModuli.AsQueryable();

            if (string.IsNullOrWhiteSpace(qry.Filtro) == false)
            {
                foreach (var f in qry.Filtro)
                {
                    queryable = queryable.Where(x =>
                        x.Id.ToString().Contains(f) ||
                        x.Titolo.Contains(f) ||
                        x.Descrizione.Contains(f)
                    );
                }
            }

            return queryable;
        }

        public async Task<IEnumerable<MetaModuloInElencoDTO>> Query(MetaModuliElencoQuery qry)
        {
            return await IQueryableMetaModuli(qry)
                                .Select(x => new MetaModuloInElencoDTO
                                {
                                    Id = x.Id,
                                    Titolo = x.Titolo,
                                    Descrizione = x.Descrizione,
                                    DataModifica = x.DataModifica
                                }).ToArrayAsync();
        }

        public async Task<int> Count(MetaModuliElencoQuery qry)
        {
            return await IQueryableMetaModuli(qry).CountAsync();
        }

        public async Task<MetaModuloInEditDTO> Query(MetaModuloInEditQuery qry)
        {
            var metaModulo = await _dbContext.MetaModuli
                .Where(x => x.Id == qry.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return new MetaModuloInEditDTO
            {
                Id = metaModulo.Id,
                Titolo = metaModulo.Titolo,
                Descrizione = metaModulo.Descrizione,
                DataModifica = metaModulo.DataModifica,
                MetaGruppi = metaModulo.MetaGruppi.Select(x => new MetaModuloInEditDTO.MetaGruppoDTO
                {
                    Id = x.Id,
                    Etichetta = x.Etichetta,
                    Ordine = x.Ordine,
                    MetaCampi = x.MetaCampi?.Select(y => new MetaModuloInEditDTO.MetaGruppoDTO.MetaCampoDTO
                    {
                        Id = y.Id,
                        Tipo = y.Tipo,
                        Etichetta = y.Etichetta,
                        Ordine = y.Ordine,
                        Dimensione = y.Dimensione,
                        Obbligatorio = y.Obbligatorio
                    }) ?? new List<MetaModuloInEditDTO.MetaGruppoDTO.MetaCampoDTO>()
                })
            };
        }

        public async Task<MetaModuloPerCompilazioneDTO> Query(MetaModuloPerCompilazioneQuery qry)
        {
            var metaModulo = await _dbContext.MetaModuli
                .Where(x => x.Id == qry.Id)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            return new MetaModuloPerCompilazioneDTO
            {
                Id = metaModulo.Id,
                Titolo = metaModulo.Titolo,
                Descrizione = metaModulo.Descrizione,
                DataModifica = metaModulo.DataModifica,
                MetaGruppi = metaModulo.MetaGruppi.Select(x => new MetaModuloPerCompilazioneDTO.MetaGruppoDTO
                {
                    Id = x.Id,
                    Etichetta = x.Etichetta,
                    Ordine = x.Ordine,
                    MetaCampi = x.MetaCampi?.Select(y => new MetaModuloPerCompilazioneDTO.MetaGruppoDTO.MetaCampoDTO
                    {
                        Id = y.Id,
                        Tipo = y.Tipo,
                        Etichetta = y.Etichetta,
                        Ordine = y.Ordine,
                        Dimensione = y.Dimensione,
                        Obbligatorio = y.Obbligatorio
                    }) ?? new List<MetaModuloPerCompilazioneDTO.MetaGruppoDTO.MetaCampoDTO>()
                })
            };
        }

        public async Task<MetaModuloPerElencoModuliDTO> Query(MetaModuloPerElencoModuliQuery qry)
        {
            return await _dbContext.MetaModuli
                                .Where(x => x.Id == qry.Id)
                                .Select(x => new MetaModuloPerElencoModuliDTO
                                {
                                    Id = x.Id,
                                    Titolo = x.Titolo,
                                    Descrizione = x.Descrizione
                                }).FirstOrDefaultAsync();
        }
    }
}
