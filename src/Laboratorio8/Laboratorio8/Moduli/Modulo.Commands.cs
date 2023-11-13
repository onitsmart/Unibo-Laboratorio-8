using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio8.Moduli
{
    public class CompilaModuloCommand
    {
        public Guid IdMetaModulo { get; set; }
        public IEnumerable<Gruppo> Gruppi { get; set; }

        public class Gruppo
        {
            public int IdMetaGruppo { get; set; }
            public IEnumerable<Campo> Campi { get; set; }

            public class Campo
            {
                public int IdMetaCampo { get; set; }
                public string Contenuto { get; set; }
            }
        }
    }

    public partial class ModuliService
    {
        public async Task<Guid?> Handle(CompilaModuloCommand cmd)
        {
            var metaModulo = await _dbContext.MetaModuli
                .Where(x => x.Id == cmd.IdMetaModulo)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            Guid? id = null;

            if (metaModulo != null)
            {
                var modulo = new Modulo
                {
                    Id = Guid.NewGuid(),
                    Titolo = metaModulo.Titolo,
                    Descrizione = metaModulo.Descrizione,
                    IdMetaModulo = metaModulo.Id,
                    DataCompilazione = DateTime.Now
                };

                var contenuto = new List<Gruppo>();
                foreach (var metaGruppo in metaModulo.MetaGruppi)
                {
                    var gruppo = cmd.Gruppi.Where(x => x.IdMetaGruppo == metaGruppo.Id).FirstOrDefault();

                    var gruppoDaAggiungere = new Gruppo
                    {
                        Id = metaGruppo.Id,
                        Etichetta = metaGruppo.Etichetta,
                        Ordine = metaGruppo.Ordine
                    };

                    var campi = new List<Campo>();
                    foreach (var metaCampo in metaGruppo.MetaCampi)
                    {
                        var campoDaAggiungere = new Campo
                        {
                            Id = metaCampo.Id,
                            Etichetta = metaCampo.Etichetta,
                            Dimensione = metaCampo.Dimensione,
                            Tipo = metaCampo.Tipo,
                            Obbligatorio = metaCampo.Obbligatorio,
                            Ordine = metaCampo.Ordine
                        };

                        if (gruppo != null)
                        {
                            var campo = gruppo.Campi.Where(x => x.IdMetaCampo == metaCampo.Id).FirstOrDefault();

                            if (campo != null)
                            {
                                campoDaAggiungere.Contenuto = campo.Contenuto;
                            }
                        }

                        campi.Add(campoDaAggiungere);
                    }

                    gruppoDaAggiungere.Campi = campi;
                    contenuto.Add(gruppoDaAggiungere);
                }

                modulo.Contenuto = contenuto;

                _dbContext.Moduli.Add(modulo);

                await _dbContext.SaveChangesAsync();
                
                id = modulo.Id;

            }
            return id;
        }
    }
}
